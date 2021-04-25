using System;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private AudioManager audioManager;
    private WordChecker wordChecker;
    private TakeInput takeInput;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        wordChecker = FindObjectOfType<WordChecker>();
        audioManager = FindObjectOfType<AudioManager>();
        takeInput = FindObjectOfType<TakeInput>();
        gameManager = FindObjectOfType<GameManager>();
        timerText = GetComponent<TextMeshProUGUI>();
        timerText.text = GameSettings.timerLength.ToString();
        ResetTimer();
        InvokeRepeating("DecrementTimer", 2, 1);
    }

    private void DecrementTimer()
    {
        if (!gameManager.isGameOver)
        {
            int newValue = Int32.Parse(timerText.text) - 1;
            if (newValue < 1)
            {
                ResetTimer();
                takeInput.ClearText();
                gameManager.wordStreak = 0;
                gameManager.currentLives -= 1;
                //this sound effect should be temporary
                //I cannot remember why that sound effect was supposed to be temporary because I like it
                audioManager.Play("wordFail");
            }
            else
            {
                switch (newValue)
                {
                    case 3:
                        audioManager.Play("timerThree");
                        break;
                    case 2:
                        audioManager.Play("timerTwo");
                        break;
                    case 1:
                        audioManager.Play("timerOne");
                        break;
                }

                timerText.text = newValue.ToString();
            }
        }
        else return;
    }

    public void ResetTimer()
    {
        timerText.text = GameSettings.timerLength.ToString();
        wordChecker.SetConditions();
    }
}
