using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public Sprite ideal;
    public Sprite move;
    public Sprite jump;
    public Sprite moveSide;
    public Sprite holdGunMid;
    public Sprite holdGUnLow;
    public SpriteRenderer spriteRenderer;
    public PlayerMovement playerMovement;
    private bool isHoldGun;
    private float delayHold = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX= false;
        if (!playerMovement.isGrounded)
        {
            spriteRenderer.sprite = jump;
            return;
        }
        if (isHoldGun)
        {
            spriteRenderer.sprite = holdGunMid;
            return;
        }
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(setHoldGun());
        }
        spriteRenderer.sprite = ideal;
        if (Input.GetKey(KeyCode.W))
        {
            spriteRenderer.sprite = move;
        }
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.sprite = moveSide;
            spriteRenderer.flipX = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.sprite = moveSide;
        }
        
    }
    private IEnumerator setHoldGun()
    {
        isHoldGun = true;
        yield return new WaitForSeconds(delayHold); 
        isHoldGun = false; 
    }
}
