using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public TMP_Text scoreText;
    public int cookedFood = 0;
    public GameObject endButton;


    private void Start()
    {
        endButton.SetActive(false);
    }

    private void Update()
    {
        if (cookedFood == 4) //CHANGE MAGIC NUMER TO A VARIABLE
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
        GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(1f));
        endButton.SetActive(false);
    }

}