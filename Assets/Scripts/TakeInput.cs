using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class TakeInput : MonoBehaviour

{
    public TextMeshProUGUI textMesh;
    private Regex rx;
    private List<string> currentInput;
    private LTDescr blinkCall;
    private Timer timer;
    private WordChecker wordChecker;
    private GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        rx = new Regex("[A-Za-z]");
        timer = FindObjectOfType<Timer>();
        wordChecker = FindObjectOfType<WordChecker>();
        gameManager = FindObjectOfType<GameManager>();
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
    private bool canPressEnter = true;
    private void ManageInput()
    {

        string charToTest = Input.inputString;
        if (rx.IsMatch(charToTest) && !gameManager.isGameOver)
        {
            currentInput[currentInput.Count - 1] = charToTest.ToLower();
            currentInput.Add("|");
        }

        if (Input.GetKey(KeyCode.Backspace) && canBackspace && currentInput.Count > 1)
        {
            canBackspace = false;
            currentInput.RemoveAt(currentInput.Count - 2);
            LeanTween.delayedCall(0.1f, () => canBackspace = true);

        };

        if (Input.GetKeyDown(KeyCode.Return) && !gameManager.isGameOver && canPressEnter)
        {
            canPressEnter = false;
            LeanTween.delayedCall(0.5f, () => canPressEnter = true);
            string submittedString = string.Join("",currentInput.GetRange(0, currentInput.Count-1));
            ClearText();
            Debug.Log(submittedString);
            wordChecker.CheckWord(submittedString);
            timer.ResetTimer();
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

    public void ClearText()
    {
        currentInput.RemoveRange(0, currentInput.Count - 1); 
    }
}
