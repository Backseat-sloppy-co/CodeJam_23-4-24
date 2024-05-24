using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//The following script has been made with assistance from GitHub Copilot
public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    public int cookedFood = 0;
    public GameObject endButton;
    public int RequiredFood = 4;

    private void Start()
    {
        endButton.SetActive(false);
    }

    private void Update()
    {
        if (cookedFood == RequiredFood) //CHANGE MAGIC NUMER TO A VARIABLE
        {
            GameOver();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void SubtractScore(int points)
    {
        score -= points;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; 
    }

    public void GameOver()
    {
        // Display game over message
        endButton.SetActive(true);
        scoreText.text = "Game Over! Final Score: " + score;
        
    }

    public void NextScene()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(1f));

    }

}