using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxStreakText;


    private int currentScore;
    private int currentMaxStreak;

    private void Start()
    {
        PlayfabManager.instance.scoreEvent += SetScoreText;
        PlayfabManager.instance.maxStreakEvent += SetMaxStreakText;
    }

    public void Initialize(int score, int maxStreak)
    {
        PlayfabManager.instance.ModifyScore(score);
        PlayfabManager.instance.ModifyMaxStreak(maxStreak);
        currentScore = score;
        currentMaxStreak = maxStreak;
        PlayfabManager.instance.GetScore();
        PlayfabManager.instance.GetMaxStreak();
    }

    private void SetScoreText()
    {
        scoreText.text = $"Score: {currentScore}\nPersonal Best: {PlayfabManager.instance.CurrentScoreStored}";
    }
    
    private void SetMaxStreakText()
    {
        maxStreakText.text = $"Score: {currentMaxStreak}\nPersonal Best: {PlayfabManager.instance.CurrentMaxStreakStored}";
    }
}
