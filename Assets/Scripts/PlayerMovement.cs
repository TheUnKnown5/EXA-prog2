using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 5.0f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] LayerMask jumpableLayers = new LayerMask();
    [SerializeField] List<GameObject> jumpableObject = new List<GameObject>();

    [Header("Mouse")]
    [SerializeField] float mouseSensativity = 1f;

    Vector3 moveInput;

    float totalRotationX = 0;
    float totalRotationY = 0;

    float forwardMovement;
    float backwardMovement;

    bool isGrounded = false;

    Rigidbody playerRigidbody;
    CapsuleCollider playerCollider;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CalculateRotation();

        forwardMovement = moveInput.x;
        backwardMovement = moveInput.z;
    }

    void FixedUpdate()
    {
        ApplyRotation();

        MovePlayer();
    }

    void CalculateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensativity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensativity;
        totalRotationX += mouseX;
        totalRotationY += mouseY;
    }

    void ApplyRotation()
    {
        playerRigidbody.rotation = Quaternion.Euler(0, totalRotationX, 0);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
    }

    void MovePlayer()
    {
        Vector3 movement = Camera.main.transform.right.normalized * forwardMovement + 
            Camera.main.transform.forward.normalized * backwardMovement;

        movement.y = 0;

        playerRigidbody.AddForce(movement * movementSpeed, ForceMode.VelocityChange);

        Vector3 velocity = playerRigidbody.velocity;
        if (Mathf.Abs(velocity.x) > movementSpeed)
        {
            velocity.x = Mathf.Sign(velocity.x) * movementSpeed;
        }
        if (Mathf.Abs(velocity.z) > movementSpeed)
        {
            velocity.z = Mathf.Sign(velocity.z) * movementSpeed;
        }
        playerRigidbody.velocity = velocity;
    }

    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            if (CanJump())
            {
                PlayerJump();
            }
        }
    }

    void PlayerJump()
    {
        playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (((1 << other.gameObject.layer) & jumpableLayers) != 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (((1 << other.gameObject.layer) & jumpableLayers) != 0)
        {
            isGrounded = false;
        }
    }

    public bool CanJump()
    {
        return isGrounded;
    }
}

//Previuos tries
//Camera.main.transform.localRotation = Quaternion.Euler(0, totalRotationX, 0);
//playerRigidbody.rotation = Quaternion.Euler(totalRotationY, 0, 0);
//Debug.Log(value.Get<Vector3>());
//Debug.Log("I am walking forward");
//Debug.Log("I am walking backwards");
//Debug.Log("Moving forward");
//if (((1 << collision.gameObject.layer) & jumpableLayers) != 0)
//{
//    jumpableObject.Add(collision.gameObject);
//}
//void OnCollisionExit(Collision collision)
//{
//    if (((1 << collision.gameObject.layer) & jumpableLayers) != 0)
//    {
//        jumpableObject.Remove(collision.gameObject);
//    }
//}
