using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI longestWord;

    public Slider sleepDepthBar;
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
        finalScore.text = gameManager.score.ToString();
        multiplier.text = $"x{gameManager.currentScoreMultiplier.ToString("0.0")}";
        sleepDepth.text = gameManager.currentSleepDepth.ToString();
        wordStreak.text = gameManager.wordStreak.ToString();
    }

    // I could just overload this method but ehhhhhhhhh
    public void FailureComment(string cause, char missingLetter)
    {
        answerComment.color = Color.red;
        CommentPop();

        switch (cause)
        {
            case "letterMust":
                answerComment.text = $"Must contain {missingLetter}";
                break;
            case "letterCant":
                answerComment.text = $"Can't contain {missingLetter}";
                break;
            case "short":
                answerComment.text = "Word too short";
                break;
            case "used":
                answerComment.text = "Word already used";
                break;
            case "invalid":
                answerComment.text = "Invalid word";
                break;
        }
        
    }

    public void SuccessComment(string word)
    {
        answerComment.color = Color.green;
        answerComment.text = word.ToUpper();
        CommentPop();
    }

    public void IncreaseSlider()
    {
        
        if (gameManager.currentSleepDepth >= 5 && sleepDepthBar.value >= 5)
        {
            sleepDepthBar.value = 5;
            return;
        } 
        
        if (sleepDepthBar.value >= 5)
        {
            sleepDepthBar.value = 0;
            gameManager.currentSleepDepth += 1;
            TextPop(sleepDepth);
        }
        else
        {
            sleepDepthBar.value += 1;
        }
    }

    public void TextPop(TextMeshProUGUI targetText)
    {
        targetText.fontSize += 15;
        LeanTween.delayedCall(0.5f, () => targetText.fontSize -= 15);
    }

    private void CommentPop()
    {
        LeanTween.delayedCall(0.8f, () =>
        {
            answerComment.text = "";
        });
    }

    public void SetLongestWord()
    {
        string currentLongestWord = "";
        foreach (var word in wordChecker.usedWords)
        {
            if (word.Length > currentLongestWord.Length)
            {
                currentLongestWord = word;
            }
        }

        longestWord.text = currentLongestWord;
    }
}
