using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Kolla i player movement scripted i grupp 6 projektet.
    // Just change it so it works in a 3D game.

    [Header("Movement")]
    [SerializeField] float movementSpeed = 5.0f;

    [Header("Jumoing")]
    [SerializeField] LayerMask JumpableLayers = new LayerMask();
    [SerializeField] List<GameObject> Jumpables = new List<GameObject>();

    Vector3 moveInput; 

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
        forwardMovement = moveInput.x;
        backwardMovement = moveInput.z;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector3>();
    }

    void MovePlayer()
    {
        playerRigidbody.AddForce(new Vector3(forwardMovement, 0f, backwardMovement));

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
        
    }
    
    public bool CanJump()
    {
        return Jumpables.Count > 0;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & JumpableLayers) != 0)
        {
            Jumpables.Add(collision.gameObject);
        }

        //if (((1 << collision.gameObject.layer) & JumpableLayers) == 0)
        //{
        //    //It wasn't in an ignore layer
        //}
    }

    void OnCollisionExit(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & JumpableLayers) != 0)
        {
            Jumpables.Remove(collision.gameObject);
        }
    }
}
