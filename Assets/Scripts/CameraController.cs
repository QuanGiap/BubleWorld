using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100.0f;
    public float distanceFromPlayer = 5.0f;
    public float rotationSpeed = 5.0f;
    private float pitch = 0.0f;
    private float yaw = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, 0f, 35f);
        transform.position = player.position - transform.forward * distanceFromPlayer;
        transform.LookAt(player.position + player.transform.forward * 2.0f);
        //rotate player
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 direction = transform.forward;
            direction.y = 0; // Keep the direction on the horizontal plane
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            player.rotation = Quaternion.Slerp(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    void LateUpdate()
    {
        
        // Rotate the camera around the player
        transform.position = player.position - Quaternion.Euler(pitch, yaw, 0) * Vector3.forward * distanceFromPlayer;
        transform.LookAt(player.position + Vector3.up * 2.0f);
    }
}
