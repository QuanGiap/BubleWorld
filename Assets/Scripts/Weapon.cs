using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform player;  // The target (player) that the weapon will aim at
    public float rotationSpeed = 5f;  // Speed of weapon rotation
    public float coolDown = 2f;
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;  // Try to find player by tag if not assigned
        }
    }

    // Method to rotate the weapon to face the player
    public void RotateToPlayer()
    {
        if (player != null)
        {
            // Get direction to the player
            Vector3 direction = player.position - transform.position;

            // Calculate rotation towards the player
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Smoothly rotate the weapon to face the player
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    // This method will be added to the EnemyAI's UseWeapon function
    public void UseWeapon()
    {
        RotateToPlayer();
        // Here you can add the logic to "fire" the weapon, such as spawning bullets or dealing damage
        Debug.Log("Weapon used against player!");
    }
}
