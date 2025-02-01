using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damage = 10;
    public float lifeTime = 5;
    private Camera mainCamera;
    private void Awake()
    {
        Destroy(gameObject, lifeTime);
        mainCamera = Camera.main;
    }
    void Update()
    {
        FaceCamera(); // Make ammo object face the camera
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyAI>().ReduceHealth(damage);
        }
        if (collision.gameObject.tag != "player")
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
