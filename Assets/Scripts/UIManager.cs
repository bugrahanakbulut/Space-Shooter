using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // 0 for no lives, 1 for one life and so on . . .
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    // health bar
    public Slider healthSlider;

    public int score;

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
}

