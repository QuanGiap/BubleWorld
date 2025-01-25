using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform m_transform;
    Rigidbody rb;
    public float speed = 1.0f;
    public Camera playerCamera;
    public float jumpForce = 5.0f;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;

        // Keep the movement on the horizontal plane
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized;
        rb.AddForce(movement * speed);
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    void OnCollisionStay(Collision collision)
    {
        // Check if the player is on the ground
        isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the player is not on the ground
        isGrounded = false;
    }
}
