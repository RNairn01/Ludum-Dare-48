using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        audioManager.Play("buttonClick");
    }

    public void LoadDifficultySelect()
    {
        SceneManager.LoadScene("DifficultySelect");
        audioManager.Play("buttonClick");
    }

    public void LoadGameEasy()
    {
        SceneManager.LoadScene("Game");
        audioManager.Play("buttonClick");
        GameSettings.gameDifficulty = Difficulty.Easy;
    }
    
    public void LoadGameNormal()
    {
        SceneManager.LoadScene("Game");
        audioManager.Play("buttonClick");
        GameSettings.gameDifficulty = Difficulty.Normal;
    }
    
    public void LoadGameHard()
    {
        SceneManager.LoadScene("Game");
        audioManager.Play("buttonClick");
        GameSettings.gameDifficulty = Difficulty.Hard;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
