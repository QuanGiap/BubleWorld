using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravityForce = 9.81f;

    // Update is called once per frame
    void Update()
    {
        // No need to update anything here for gravity
    }

    void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 directionToCenter = (transform.position - other.transform.position).normalized;
            rb.AddForce(directionToCenter * gravityForce * rb.mass);
        }
    }
}
