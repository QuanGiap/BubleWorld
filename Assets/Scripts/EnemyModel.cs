using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public Sprite ideal;
    public Sprite move;
    public Sprite attach;
    public SpriteRenderer spriteRenderer;
    Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = ideal;
        if (rb.velocity.magnitude >= 0.2)
        {
            spriteRenderer.sprite = move;

        }
    }
}
