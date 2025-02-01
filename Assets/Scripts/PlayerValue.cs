using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValue : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 10;
    public Action die;
    public void ReduceHealth(int amount)
    {
        health -= amount;
        if(health < 0)
        {
            die?.Invoke();
        }
    }
}
