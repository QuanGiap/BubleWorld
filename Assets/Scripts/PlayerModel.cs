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
    private bool isJumping;
    private bool isHoldGun;
    private float delayHold = 5;
    private float delayJump = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        spriteRenderer.flipX= false;
        if (!isJumping && !isHoldGun)
        {
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
        if (!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            spriteRenderer.sprite = jump;
            StartCoroutine(setJump());
        }
        if (Input.GetMouseButton(0))
        {
            spriteRenderer.sprite = holdGunMid;
            StartCoroutine(setHoldGun());
        }
        
    }
    private IEnumerator setJump()
    {
        isJumping = true;  // Start attacking
        yield return new WaitForSeconds(delayJump);  // Perform attack for a set duration
        isJumping = false;  // Stop attacking and resume flying
    }
    private IEnumerator setHoldGun()
    {
        isJumping = true;  // Start attacking
        yield return new WaitForSeconds(delayHold);  // Perform attack for a set duration
        isJumping = false;  // Stop attacking and resume flying
    }
}
