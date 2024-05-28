using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 5.0f;

    [Header("Jumping")]
    [SerializeField] float jumpForce = 0f;
    [SerializeField] LayerMask jumpableLayers = new LayerMask();
    [SerializeField] List<GameObject> jumpableObject = new List<GameObject>();

    [Header("Mouse")]
    [SerializeField] float mouseSensativity = 1f;

    Vector3 moveInput;

    float totalRotationX = 0;
    float totalRotationY = 0;

    float forwardMovement;
    float backwardMovement;

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
        playerRigidbody.AddForce(Camera.main.transform.right.normalized * forwardMovement + 
            Camera.main.transform.forward.normalized * backwardMovement, ForceMode.VelocityChange);

        if (Mathf.Abs(playerRigidbody.velocity.x) > movementSpeed)
        {
            playerRigidbody.velocity = new Vector3(Mathf.Sign(playerRigidbody.velocity.x) * movementSpeed, playerRigidbody.velocity.y, 
                playerRigidbody.velocity.z);
        }
        if (Mathf.Abs(playerRigidbody.velocity.z) > movementSpeed)
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, playerRigidbody.velocity.y,
                Mathf.Sign(playerRigidbody.velocity.z) * movementSpeed);
        }
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (CanJump())
            {
                PlayerJump();
            }
        }
    }

    void PlayerJump()
    {
        playerRigidbody.velocity += new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
    }
    
    public bool CanJump()
    {
        return jumpableObject.Count > 0;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerJump();
        }

        //if (((1 << collision.gameObject.layer) & jumpableLayers) != 0)
        //{
        //    jumpableObject.Add(collision.gameObject);
        //}
    }

    //void OnCollisionExit(Collision collision)
    //{
    //    if (((1 << collision.gameObject.layer) & jumpableLayers) != 0)
    //    {
    //        jumpableObject.Remove(collision.gameObject);
    //    }
    //}
}

//Previuos tries
//Camera.main.transform.localRotation = Quaternion.Euler(0, totalRotationX, 0);
//playerRigidbody.rotation = Quaternion.Euler(totalRotationY, 0, 0);
//Debug.Log(value.Get<Vector3>());
//Debug.Log("I am walking forward");
//Debug.Log("I am walking backwards");
//Debug.Log("Moving forward");
