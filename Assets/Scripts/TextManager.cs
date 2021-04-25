using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    private WordChecker wordChecker;
    private GameManager gameManager;
    public TextMeshProUGUI mustContainContent;
    public TextMeshProUGUI cantContainContent;
    public TextMeshProUGUI minLengthContent;
    public TextMeshProUGUI answerComment;
    public TextMeshProUGUI scoreCounter;
    public TextMeshProUGUI multiplier;
    public TextMeshProUGUI sleepDepth;
    public TextMeshProUGUI wordStreak;
    // Start is called before the first frame update
    void Start()
    {
        wordChecker = FindObjectOfType<WordChecker>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreCounter.text = gameManager.score.ToString();
        multiplier.text = $"x{gameManager.currentScoreMultiplier.ToString(".0")}";
        sleepDepth.text = gameManager.currentSleepDepth.ToString();
        wordStreak.text = gameManager.wordStreak.ToString();
    }

    // I could just overload this method but ehhhhhhhhh
    public void FailureComment(string cause, char missingLetter)
    {
        answerComment.color = Color.red;

        switch (cause)
        {
            case "letterMust":
                answerComment.text = $"FAILURE! - Must contain {missingLetter}";
                break;
            case "letterCant":
                answerComment.text = $"FAILURE! - Must contain {missingLetter}";
                break;
            case "short":
                answerComment.text = "FAILURE! - Word too short";
                break;
            case "used":
                answerComment.text = "FAILURE! - Word already used";
                break;
            case "invalid":
                answerComment.text = "FAILURE! - Invalid word";
                break;
        }
        
    }

    public void SuccessComment(string word)
    {
        answerComment.color = Color.green;
        answerComment.text = $"SUCCESS! - {word}";
    }
}
