using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;  // Reference to projectile prefab
    public Transform firePoint;          // The point where the projectile is spawned
    public float projectileSpeed = 10f;  // Speed of the projectile
    public float fixedYPosition = 1f;    // Default Y position if no object is hit
    public Vector3 offset = Vector3.zero;
    public LayerMask playerLayer;
    public AudioSource shootAudio;
    public float speed = 0.5f;
    private float count = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && count <=Time.time){ 
            Shoot();
        }
    }

    void Shoot()
    {
        //play sound
        // Get the mouse click position
        Vector3 targetPosition = GetMouseWorldPosition();

        if (targetPosition == Vector3.zero) return; // Prevent errors if raycasting fails

        // Instantiate the projectile at the firePoint position
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position + offset, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        shootAudio.Play();
        count += speed;
        if (rb != null)
        {
            // Calculate direction to target
            Vector3 direction = (targetPosition - firePoint.position).normalized;
            rb.velocity = direction * projectileSpeed;
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, ~playerLayer))
        {
            return hit.point; // Return the hit point
        }
        else
        {
            // If nothing is hit, project onto a flat plane at fixedYPosition
            Plane groundPlane = new Plane(Vector3.up, new Vector3(0, fixedYPosition, 0));
            float distance;
            if (groundPlane.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance); // Return projected position
            }
        }
        return Vector3.zero; // Default return if nothing found
    }
}
