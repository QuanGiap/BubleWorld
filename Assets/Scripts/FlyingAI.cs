using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAI : MonoBehaviour
{
    public Transform player;  // Reference to the player
    public float orbitRadius = 10f;  // Radius of the orbit
    public float orbitSpeed = 2f;  // Speed of orbit
    public float attackRange = 3f;  // Range to start attacking
    public float attackDuration = 2f;  // Time to attack before returning to orbit
    public float attackSpeed = 5f;  // Speed when moving towards the player for attack

    private Rigidbody rb;  // Rigidbody for movement
    private Vector3 orbitCenter;  // The point around which the enemy orbits
    private bool isAttacking = false;  // To track if the enemy is currently attacking

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        orbitCenter = player.position;  // Initial orbit center at player position
    }

    private void Update()
    {
        if (isAttacking)
        {
            // Attack movement logic
            MoveTowardsPlayer();
        }
        else
        {
            // Fly around the player
            FlyAroundPlayer();
            CheckForAttack();
        }
    }

    private void FlyAroundPlayer()
    {
        // Calculate the circular orbit position around the player
        float orbitAngle = Time.time * orbitSpeed;
        Vector3 orbitPosition = orbitCenter + new Vector3(Mathf.Sin(orbitAngle) * orbitRadius, Mathf.Cos(orbitAngle) * orbitRadius, 0);

        // Apply force to move the enemy towards the orbit position
        Vector3 moveDirection = (orbitPosition - transform.position).normalized;
        rb.AddForce(moveDirection * 10f, ForceMode.Force);  // You can adjust force for smoother movement
    }

    private void CheckForAttack()
    {
        // Check if the enemy is close enough to the player to attack
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;  // Start attacking
        yield return new WaitForSeconds(attackDuration);  // Perform attack for a set duration
        isAttacking = false;  // Stop attacking and resume flying
    }

    private void MoveTowardsPlayer()
    {
        // Move the enemy towards the player at attack speed
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + directionToPlayer * attackSpeed * Time.deltaTime);

        // Optionally, add rotation to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
}
