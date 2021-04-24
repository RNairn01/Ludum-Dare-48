using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentSleepDepth;

    //private WordChecker wordChecker;
    // Start is called before the first frame update
    void Start()
    {
        //wordChecker = FindObjectOfType<WordChecker>();
        currentSleepDepth = 1;
        
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
