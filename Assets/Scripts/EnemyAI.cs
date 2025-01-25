using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform player;  // Reference to the player
    public Transform planet;  // Reference to the planet
    public float moveSpeed = 3f;  // Movement speed
    public float gravityStrength = 10f;  // Gravity strength
    public float rotationSpeed = 5f;  // Rotation speed for smooth turning
    public float stopDistance = 2f;  // Distance to stop before considering "reached"
    public bool startChase= false;  // Distance to stop before considering "reached"

    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody for physics-based movement
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Disable Unity's gravity
        rb.constraints = RigidbodyConstraints.FreezeRotation;  // Prevent unwanted rotations
    }

    void FixedUpdate()
    {
        if (player == null || planet == null)
            return;
        if(startChase){
            // Move towards the player
            MoveTowardsPlayer();
        }
    }

    // Move the enemy towards the player while staying on the planet's surface
    void MoveTowardsPlayer()
    {
        // Get the direction from the enemy to the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Project the direction onto the planet's surface (align with gravity)
        Vector3 surfaceDirection = Vector3.ProjectOnPlane(directionToPlayer, transform.up).normalized;

        // Move the enemy towards the player
        rb.MovePosition(transform.position + surfaceDirection * moveSpeed * Time.fixedDeltaTime);

        // Rotate towards the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(surfaceDirection, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Check if we are close enough to the player to stop
        if (Vector3.Distance(transform.position, player.position) < stopDistance)
        {
            // You can add behavior for what happens when the enemy reaches the player (e.g., attack)
            Debug.Log("Enemy reached the player!");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            startChase= true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            startChase = false;
        }
    }
}