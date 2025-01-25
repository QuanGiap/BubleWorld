using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;
using Input = UnityEngine.Input;

public class CameraController : MonoBehaviour
{
    public Transform player;    // Player reference
    public Transform planet;    // Planet reference for gravity
    public float defaultDistance = 5f; // Default camera distance from player
    public float minDistance = 1.5f; // Minimum camera distance to avoid clipping
    public float mouseSensitivity = 2f; // Mouse sensitivity
    public float smoothSpeed = 5f; // Smoothing speed
    public LayerMask obstacleMask; // LayerMask for obstacles (set this to "Planet" or "Default")

    private float yaw = 0f;  // Horizontal rotation
    private float pitch = 0f; // Vertical rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center
    }

    void LateUpdate()
    {
        if (!player || !planet) return;

        // Mouse movement input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, -80f, 80f); // Limit vertical rotation

        // Desired camera rotation based on mouse movement
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Desired camera position behind the player
        Vector3 desiredPosition = player.position - (rotation * Vector3.forward * defaultDistance);

        // Adjust camera position using raycast to prevent clipping
        float adjustedDistance = defaultDistance;
        RaycastHit hit;
        if (Physics.Raycast(player.position, (desiredPosition - player.position).normalized, out hit, defaultDistance, obstacleMask))
        {
            adjustedDistance = Mathf.Clamp(hit.distance * 0.9f, minDistance, defaultDistance);
        }

        // Final camera position
        Vector3 finalPosition = player.position - (rotation * Vector3.forward * adjustedDistance);

        // Align camera "up" direction with planet gravity
        Vector3 gravityUp = (player.position - planet.position).normalized;
        Quaternion planetRotation = Quaternion.FromToRotation(transform.up, gravityUp) * transform.rotation;

        // Apply smooth camera movement
        transform.position = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * smoothSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, planetRotation, Time.deltaTime * smoothSpeed);
    }
}
