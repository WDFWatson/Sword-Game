using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int health = 5;

    [HideInInspector] public int score = 0;
    [HideInInspector]  int streak = 0;

    public static PlayerManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void TakeDamage()
    {
        health--;
        streak = 0;
        if (health <= 0)
        {
            Debug.Log("You Died");
        }
        Debug.Log(health);
    }

    public void Score()
    {
        score++;
        streak++;
        Debug.Log(score);
        Debug.Log(streak);
    }
    
}
