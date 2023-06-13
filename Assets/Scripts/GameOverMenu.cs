using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxStreakText;
    public RectTransform menu;


    private int currentScore;
    private int currentMaxStreak;
    private bool hasModifiedScore;
    private bool hasModifiedMaxStreak;

    private void Start()
    {
        hasModifiedScore = false;
        hasModifiedMaxStreak = false;
        hasSetStreakText = false;
        hasSetScoreText = false;
        
        PlayfabManager.instance.scoreEvent = null;
        PlayfabManager.instance.maxStreakEvent = null;
        PlayfabManager.instance.ModifyScoreEvent = null;
        PlayfabManager.instance.ModifyMaxStreakEvent = null;

        PlayfabManager.instance.scoreEvent += () => StartCoroutine(SetScoreText());
        PlayfabManager.instance.maxStreakEvent += () => StartCoroutine(SetMaxStreakText());
        PlayfabManager.instance.ModifyScoreEvent += ScoreModified;
        PlayfabManager.instance.ModifyMaxStreakEvent += MaxStreakModified;
        menu.gameObject.SetActive(false);
    }

    public IEnumerator Initialize(int score, int maxStreak)
    {
        PlayfabManager.instance.ModifyScore(score);
        PlayfabManager.instance.ModifyMaxStreak(maxStreak);
        currentScore = score;
        currentMaxStreak = maxStreak;
        yield return new WaitUntil(() => hasModifiedScore && hasModifiedMaxStreak);
        PlayfabManager.instance.GetScore();
        PlayfabManager.instance.GetMaxStreak();
        yield return new WaitUntil(() => hasSetStreakText && hasSetScoreText);
        hasSetStreakText = false;
        hasSetScoreText = false;
        menu.gameObject.SetActive(true);
    }


    private void ScoreModified()
    {
        hasModifiedScore = true;
    }
    
    private void MaxStreakModified()
    {
        hasModifiedMaxStreak = true;
    }

    private bool hasSetScoreText;
    private IEnumerator SetScoreText()
    {
        yield return new WaitUntil(() => hasModifiedScore);
        Debug.Log(PlayfabManager.instance.CurrentScoreStored);
        scoreText.text = $"Score: {currentScore}\nPersonal Best: {PlayfabManager.instance.CurrentScoreStored}";
        hasModifiedScore = false;
        hasSetScoreText = true;
    }
    private bool hasSetStreakText;
    
    private IEnumerator SetMaxStreakText()
    {
        yield return new WaitUntil(() => hasModifiedMaxStreak);
        Debug.Log(PlayfabManager.instance.CurrentMaxStreakStored);
        maxStreakText.text = $"Max Streak: {currentMaxStreak}\nPersonal Best: {PlayfabManager.instance.CurrentMaxStreakStored}";
        hasModifiedMaxStreak = false;
        hasSetStreakText = true;
    }
}
