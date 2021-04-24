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
    }

    private void Awake()
    {
        //wordChecker.SetConditions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
