using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public Transform planet; // Reference to the planet
    public float gravityStrength = 10f; // Strength of gravity pull
    public float rotationSpeed = 10f; // Speed of alignment with gravity

    private Rigidbody rb;

    void Awake()
    {
        planet = GameObject.Find("icosphere").transform;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Disable Unity's default gravity
        rb.constraints = RigidbodyConstraints.FreezeRotation; // Prevent unwanted rotation
    }

    void FixedUpdate()
    {
        if (!planet) return;

        // Calculate direction towards the planet's center
        Vector3 gravityDirection = (planet.position - transform.position).normalized;

        // Apply gravity force
        rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);

        // Align player "up" direction with planet's gravity
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
