using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    // 0 for no lives, 1 for one life and so on . . .
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    // health bar
    public Slider healthSlider;

    public int score;

    private bool paused = false;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GameFlowController();
    }

    public void UpdatePlayerLives(int playerLives)
    {
        livesImageDisplay.sprite = lives[playerLives];
    }

    public void UpdatePlayerScore()
    {
        score += 1;
        scoreText.text = "Score : " + score;
    }

    public void UpdatePlayerHealth(float health)
    {
        healthSlider.value = health / 100.0f;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameFlowController()
    {
        if (paused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Time.timeScale = 0;
            paused = true;
        }
    }

    public void ResumeGame()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Time.timeScale = 1;
            paused = false;
        }
    }
}

