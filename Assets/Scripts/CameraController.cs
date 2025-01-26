using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;
using Input = UnityEngine.Input;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;  // Reference to Cinemachine FreeLook Camera
    public float mouseSensitivity = 2f;  // Mouse sensitivity for camera rotation

    private void Start()
    {
        // Ensure the CinemachineFreeLook camera is assigned
        if (freeLookCam == null)
        {
            freeLookCam = FindObjectOfType<CinemachineFreeLook>();
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Get mouse input for X (horizontal) and Y (vertical) axis
        //float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //// Adjust the FreeLook camera's rotation based on mouse input
        //freeLookCam.m_XAxis.Value += mouseX;  // Rotate the camera horizontally
        //freeLookCam.m_YAxis.Value -= mouseY;  // Rotate the camera vertically (invert the Y axis if needed)
    }
}
