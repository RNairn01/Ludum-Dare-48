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
    private AudioManager audioManager;
    private bool canGameEnd = true;

    //private WordChecker wordChecker;
    // Start is called before the first frame update
    void Start()
    {
        GameSettings.ImplementGameSettings();
        audioManager = FindObjectOfType<AudioManager>();
        currentSleepDepth = 1;
        currentLives = GameSettings.lives;
        wordStreak = 0;
        totalCorrectWords = 0;
        currentScoreMultiplier = GameSettings.scoreMultiplier;
    }

    private void Update()
    {
        if (currentLives <= 0 && canGameEnd)
        {
            canGameEnd = false;
            GameOver();
        }
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

    public void FailWord()
    {
        wordStreak = 0;
        CheckMultiplier(false);
        currentLives -= 1;
        audioManager.Play("wordFail");
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }
}
