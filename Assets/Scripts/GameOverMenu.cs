using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxStreakText;

    public void setScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void setMaxStreak(int maxStreak)
    {
        maxStreakText.text = "Max Streak: " + maxStreak;
    }
}
