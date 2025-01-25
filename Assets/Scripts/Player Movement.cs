using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform m_transform;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.MovePosition(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.MovePosition(Vector3.back);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.MovePosition(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.MovePosition(Vector3.right);
        }
        //player jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(Vector3.up);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
