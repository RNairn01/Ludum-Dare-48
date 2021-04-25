using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentSleepDepth;
    public int currentLives;
    public int wordStreak;
    public int totalCorrectWords;
    public int score;
    public float currentScoreMultiplier;
    public bool isGameOver = false;

    //private WordChecker wordChecker;
    // Start is called before the first frame update
    void Start()
    {
        //wordChecker = FindObjectOfType<WordChecker>();
        currentSleepDepth = 1;
        currentLives = GameSettings.lives;
        wordStreak = 0;
        totalCorrectWords = 0;
        currentScoreMultiplier = GameSettings.scoreMultiplier;

    }

    public void IncreaseScore(string answer)
    {
        score += Mathf.RoundToInt((5 +answer.Length) * currentScoreMultiplier);
        if (score > 9999999) score = 9999999;
    }

    public void CheckMultiplier(bool didSucceed)
    {
        if (!didSucceed) currentScoreMultiplier = GameSettings.scoreMultiplier;
        else
        {
            if (wordStreak % 5 == 0) currentScoreMultiplier += 0.5f;
        }

        if (currentScoreMultiplier > 20) currentScoreMultiplier = 20;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
