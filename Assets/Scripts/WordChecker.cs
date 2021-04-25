using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordChecker : MonoBehaviour
{
    private string pathToEnglish = "Assets/Resources/Dictionary/words_alpha.txt";

    public string[] acceptedWords;

    public List<string> usedWords;

    public List<char> mustContain;
    public List<char> cantContain;

    public int minLength;

    private GameManager gameManager;
    private TextManager textManager;
    private AudioManager audioManager;
    
    private char[] consonants = "bcdfghjklmnpqrstvwxyz".ToCharArray();
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        textManager = FindObjectOfType<TextManager>();
        audioManager = FindObjectOfType<AudioManager>();
        LoadWords(pathToEnglish);
        usedWords = new List<string>();
        
        SetConditions();
    }

    private void LoadWords(string path)
    {
        acceptedWords = File.ReadAllLines(path);
    }

    public void SetConditions()
    {
        var availableConsonants = new List<char>(consonants);
        mustContain.Clear();
        cantContain.Clear();
        minLength = Random.Range(1, 4) + gameManager.currentSleepDepth;
        for (int i = 0; i < Random.Range(0, gameManager.currentSleepDepth+1); i++)
        {
            int rand = Random.Range(0, availableConsonants.Count);
            mustContain.Add(availableConsonants[rand]);
            availableConsonants.RemoveAt(rand);
        }
        for (int i = 0; i < Random.Range(0, gameManager.currentSleepDepth+1); i++)
        {
            int rand = Random.Range(0, availableConsonants.Count);
            cantContain.Add(availableConsonants[rand]);
            availableConsonants.RemoveAt(rand);
        }

        textManager.mustContainContent.text = string.Join(", ", mustContain);
        textManager.cantContainContent.text = string.Join(", ", cantContain);
        textManager.minLengthContent.text = minLength.ToString();
    }
    
    //This is a nightmare, but it is MY nightmare
    public void CheckWord(string answer)
    {
        if (usedWords.Contains(answer))
        {
            Debug.Log("FAILURE - Already used");
            textManager.FailureComment("used", '0');
            gameManager.wordStreak = 0;
            gameManager.CheckMultiplier(false);
            gameManager.currentLives -= 1;
            audioManager.Play("wordFail");
        }
        else if (answer.Length < minLength)
        {
            Debug.Log("FAILURE - Too short");
            textManager.FailureComment("short", '0');
            gameManager.wordStreak = 0;
            gameManager.CheckMultiplier(false);
            gameManager.currentLives -= 1;
            audioManager.Play("wordFail");
        } 
        else
        {
            var answerArr = answer.ToLower().ToCharArray();
            bool willSucceed = true;
            
            foreach (var mustLetter in mustContain)
            {
                if (!answerArr.Contains(mustLetter))
                {
                    Debug.Log($"FAILURE - Must contain {mustLetter}");
                    textManager.FailureComment("letterMust", mustLetter);
                    willSucceed = false;
                    gameManager.wordStreak = 0;
                    gameManager.CheckMultiplier(false);
                    gameManager.currentLives -= 1;
                    audioManager.Play("wordFail");
                    break;
                }
            }

            foreach (var cantLetter in cantContain)
            {
                if (answerArr.Contains(cantLetter))
                {
                    Debug.Log($"FAILURE - Can't contain {cantLetter}");
                    textManager.FailureComment("letterCant", cantLetter);
                    willSucceed = false;
                    gameManager.wordStreak = 0;
                    gameManager.CheckMultiplier(false);
                    gameManager.currentLives -= 1;
                    audioManager.Play("wordFail");
                    break;
                }
            }

            if (willSucceed && Array.Find(acceptedWords, e => e == answer) != null)
            {
                Debug.Log("SUCCESS!");
                textManager.SuccessComment(answer);
                gameManager.IncreaseScore(answer);
                gameManager.totalCorrectWords++;
                gameManager.wordStreak++;
                gameManager.CheckMultiplier(true);
                audioManager.Play("wordSuccess");
                textManager.IncreaseSlider();
                usedWords.Add(answer);
            } 
            else if (Array.Find(acceptedWords, e => e == answer) == null)
            {
                Debug.Log("FAILURE - Invalid word");
                textManager.FailureComment("invalid", '0');
                gameManager.wordStreak = 0;
                gameManager.CheckMultiplier(false);
                gameManager.currentLives -= 1;
                audioManager.Play("wordFail");
            }
        }
    }
}
