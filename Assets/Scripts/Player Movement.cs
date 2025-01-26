using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform planet; // Reference to the planet
    public float moveSpeed = 5f; // Player movement speed
    public float rotationSpeed = 10f; // Player rotation speed
    public float jumpForce = 8f; // Jump force
    public float groundDistanceThreshold = 1.2f; // Distance to detect "grounded" state

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
        CheckGround();
        MovePlayer();
        Jump();
    }

    void CheckGround()
    {
        if (!planet) return;

        // Calculate distance from the player to the planet’s surface
        float distanceToPlanet = Vector3.Distance(transform.position, planet.position);
        float planetRadius = planet.localScale.x * 0.5f; // Assuming the planet is a sphere

        // Player is grounded if they are close enough to the planet's surface
        isGrounded = distanceToPlanet <= (planetRadius + groundDistanceThreshold);
    }

    void MovePlayer()
    {
        if (!mainCamera) return;

        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate camera-relative movement
        Vector3 camForward = Vector3.ProjectOnPlane(transform.forward, transform.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(transform.right, transform.up).normalized;
        Vector3 moveDirection = (camForward * vertical + camRight * horizontal).normalized;

        // Apply movement
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

        // Rotate player towards movement direction
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Get gravity direction (opposite of the planet's pull)
            Vector3 gravityDirection = (transform.position - planet.position).normalized;
            rb.AddForce(gravityDirection * jumpForce, ForceMode.Impulse);
        }
    }
}
