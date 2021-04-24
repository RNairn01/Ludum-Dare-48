using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class TakeInput : MonoBehaviour

{
    public TextMeshProUGUI textMesh;
    private Regex rx;
    private List<string> currentInput;
    private LTDescr blinkCall;
    private Timer timer;
    private WordChecker wordChcker;
    
    // Start is called before the first frame update
    void Start()
    {
        rx = new Regex("[A-Za-z]");
        timer = FindObjectOfType<Timer>();
        wordChcker = FindObjectOfType<WordChecker>();
        currentInput = new List<string>();
        currentInput.Add("|");
    }

    private void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInput();
        if (willBlink) CursorBlink();
        
        
        
        textMesh.text = string.Join("", currentInput); 
    }
    
    private bool canBackspace = true;
    private bool willBlink = true;
    private bool canBlink = true;
    private void ManageInput()
    {

        string charToTest = Input.inputString;
        if (rx.IsMatch(charToTest))
        {
            currentInput[currentInput.Count - 1] = charToTest;
            currentInput.Add("|");
        }

        if (Input.GetKey(KeyCode.Backspace) && canBackspace && currentInput.Count > 1)
        {
            canBackspace = false;
            currentInput.RemoveAt(currentInput.Count - 2);
            LeanTween.delayedCall(0.08f, () => canBackspace = true);

        };

        if (Input.GetKeyDown(KeyCode.Return))
        {
            string submittedString = string.Join("",currentInput.GetRange(0, currentInput.Count-1));
            currentInput.RemoveRange(0, currentInput.Count - 1);
            Debug.Log(submittedString);
            timer.ResetTimer();
            //TODO: Implement submission checking against dictionary
            CheckWord(submittedString);
        }
    }

    private void CursorBlink()
    {
        if (canBlink)
        {
            currentInput[currentInput.Count - 1] = " ";
            canBlink = false;
            LeanTween.delayedCall(0.5f, () =>
            {
                currentInput[currentInput.Count - 1] = "|";
                LeanTween.delayedCall(0.5f, () => canBlink = true);
            });
        }

    }

    private void CheckWord(string answer)
    {
        if (wordChcker.usedWords.Contains(answer))
        {
            Debug.Log("already used");
        }
        else if (Array.Find(wordChcker.acceptedWords, e => e == answer) != null)
        {
            Debug.Log("yes");
            wordChcker.usedWords.Add(answer);
        }
        else
        {
            Debug.Log("no");
        }
    }
}
