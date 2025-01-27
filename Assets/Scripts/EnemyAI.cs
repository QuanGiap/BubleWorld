using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public Transform planet;  // Reference to the planet
    public float moveSpeed = 3f;  // Movement speed
    public float gravityStrength = 10f;  // Gravity strength
    public float rotationSpeed = 5f;  // Rotation speed for smooth turning
    public float stopDistance = 2f;  // Distance to stop before considering "reached"
    public float attachDelay = 2;
    public int damage = 10;
    public int health= 40;
    private bool startChase= false;  // Distance to stop before considering "reached"
    private bool enoughDistant= false;  // Distance to stop before considering "reached"
    public LayerMask playerMask;
    private Rigidbody rb;
    private GameObject player;
    private Camera mainCamera;
    
    // use weapons action to register
    public Weapon weapon;
    void Awake()
    {
        planet = GameObject.Find("icosphere").transform;
        // Get the Rigidbody for physics-based movement
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Disable Unity's gravity
        rb.constraints = RigidbodyConstraints.FreezeRotation;  // Prevent unwanted rotations
        player = GameObject.Find("Player Model");
        mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        FaceCamera();
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
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Project the direction onto the planet's surface (align with gravity)
        Vector3 surfaceDirection = Vector3.ProjectOnPlane(directionToPlayer, transform.up).normalized;
        if(!enoughDistant){
            // Move the enemy towards the player
            rb.MovePosition(transform.position + surfaceDirection * moveSpeed * Time.fixedDeltaTime);
        }
        // Rotate towards the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(surfaceDirection, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        // Check if we are close enough to the player to stop
        if (Vector3.Distance(transform.position, player.transform.position) < stopDistance)
        {
            enoughDistant = true;
            StartCoroutine(attach());
        }
        else
        {
            enoughDistant = false;
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
    private IEnumerator attach()
    {
        yield return new WaitForSeconds(attachDelay);
        Debug.Log("Attach!");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, playerMask))
        {
            Debug.Log("Hit!");
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

    }
    public void ReduceHealth(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FaceCamera()
    {
        if (mainCamera != null)
        {
            transform.LookAt(mainCamera.transform); // Rotate towards camera
            transform.Rotate(0, 180, 0);  // Flip if needed (adjust based on your sprite)
        }
    }
}