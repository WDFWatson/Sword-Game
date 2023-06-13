using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameOverMenu gameOverMenu;
    public int health = 5;
    

    [HideInInspector] public int score = 0;
    [HideInInspector]  public int streak = 0;
    [HideInInspector] public int maxStreak = 0;

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
        if (streak > maxStreak)
        {
            maxStreak = streak;
        }
        streak = 0;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Score()
    {
        score++;
        streak++;
    }

    public void Die()
    {
        StartCoroutine(gameOverMenu.Initialize(score, maxStreak) );
        TargetArea.instance.gameObject.SetActive(false);
        InputController.instance.sword.transform.position = new Vector3(0, 0, 20);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
