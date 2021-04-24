using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WordChecker : MonoBehaviour
{
    private string pathToEnglish = "Assets/Resources/Dictionary/words_alpha.txt";

    public string[] acceptedWords;

    public List<string> usedWords;
    // Start is called before the first frame update
    void Start()
    {
        LoadWords(pathToEnglish);
        usedWords = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadWords(string path)
    {
        acceptedWords = File.ReadAllLines(path);
    }
}
