using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxStreakText;

    public void Initialize(int score, int maxStreak)
    {
        PlayfabManager.instance.ModifyScore(score);
        PlayfabManager.instance.ModifyMaxStreak(maxStreak);
        int pbScore = PlayfabManager.instance.GetScore();
        int pbMaxStreak = PlayfabManager.instance.GetScore();

        scoreText.text = $"Score: {score}\nPersonal Best: {pbScore}";
        maxStreakText.text = $"Max Streak: {maxStreak}\nPersonal Best: {pbMaxStreak}";
    }
}
