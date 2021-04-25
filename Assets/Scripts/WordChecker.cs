using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordChecker : MonoBehaviour
{
    private string pathToEnglish = "Assets/Resources/Dictionary/words_alpha.txt";

    public string[] acceptedWords;
    public List<string> acceptedWordsTrimmed;
    public string[] mostCommonWords;
    public List<string> mostCommonWordsTrimmed;
    public TextAsset dictionary;
    public TextAsset mostCommonWordsDictionary;

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
        LoadWords();
        Debug.Log(acceptedWords.Length);
        usedWords = new List<string>();
        
        SetConditions();
        
    }

    private void LoadWords()
    {
        acceptedWordsTrimmed = new List<string>();
        mostCommonWordsTrimmed = new List<string>();
        
        acceptedWords = dictionary.ToString().Split('\n');
        foreach (var word in acceptedWords)
        {
            acceptedWordsTrimmed.Add(word.Trim());
        }
        
        mostCommonWords = mostCommonWordsDictionary.ToString().Split('\n');
        foreach (var word in acceptedWords)
        {
            mostCommonWordsTrimmed.Add(word.Trim());
        }
    }

    public void SetConditions()
    {
        var availableConsonants = new List<char>(consonants);
        mustContain.Clear();
        cantContain.Clear();
        minLength = Random.Range(0, 4) + gameManager.currentSleepDepth;
        for (int i = 0; i < Random.Range(1, gameManager.currentSleepDepth+1); i++)
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

        bool isValid = false;
        foreach (var word in mostCommonWordsTrimmed)
        {
            bool wordOkay = !(word.Length < minLength);

            foreach (var letter in mustContain)
            {
                if (!word.Contains(letter))
                {
                    wordOkay = false;
                    break;
                }
                
            }

            foreach (var letter in cantContain)
            {
                if (word.Contains(letter))
                {
                    wordOkay = false;
                    break;
                }
                
            }
            if (wordOkay)
            {
                isValid = true;
                Debug.Log(word);
                break;
            }
        }

        if (isValid)
        {
            textManager.mustContainContent.text = string.Join(", ", mustContain);
            textManager.cantContainContent.text = string.Join(", ", cantContain);
            textManager.minLengthContent.text = minLength.ToString();
        }
        else
        {
            SetConditions();
            return;
        }

    }
    
    //This is a nightmare, but it is MY nightmare
    public void CheckWord(string answer)
    {
        if (usedWords.Contains(answer))
        {
            Debug.Log("FAILURE - Already used");
            textManager.FailureComment("used", '0');
            gameManager.FailWord();
        }
        else if (answer.Length < minLength)
        {
            Debug.Log("FAILURE - Too short");
            textManager.FailureComment("short", '0');
            gameManager.FailWord();
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
                    gameManager.FailWord();
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
                    gameManager.FailWord();
                    break;
                }
            }

            if (willSucceed && acceptedWordsTrimmed.Contains(answer)/*Array.Find(acceptedWords, e => e == answer) != null*/)
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
            else if (!acceptedWordsTrimmed.Contains(answer)/*Array.Find(acceptedWords, e => e == answer) == null*/)
            {
                Debug.Log("FAILURE - Invalid word");
                textManager.FailureComment("invalid", '0');
                gameManager.FailWord();
            }
        }
    }
}
