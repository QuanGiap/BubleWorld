using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform planet; // Reference to the planet
    public float moveSpeed = 5f; // Player movement speed
    public float rotationSpeed = 10f; // Player rotation speed
    public float jumpForce = 8f; // Jump force

    private Rigidbody rb;
    private Camera mainCamera;
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();
    }


    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isGrounded=false;
    }

    void MovePlayer()
    {
        if (!mainCamera) return;

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if(vertical<0)vertical = 0;
       
        Vector3 moveDirection = ((transform.forward * vertical )+ (transform.right * horizontal)).normalized;
        

        // Apply movement
        rb.MovePosition(rb.position + moveDirection * (moveSpeed * Time.fixedDeltaTime));

        // Rotate player towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Get gravity direction (opposite of the planet's pull)
            Vector3 gravityDirection = (transform.position - planet.position).normalized;
            rb.AddForce(gravityDirection * jumpForce, ForceMode.Impulse);
        }
    }
}
