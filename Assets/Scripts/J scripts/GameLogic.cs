using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private GameBoardBehaviour gameBoard;
    [HideInInspector] public GameObject ball;
    private int tries = 0;

    [SerializeField] private BoxCollider winBox;
    [SerializeField] private BoxCollider deathBox;
    [SerializeField] private TMP_Text triesText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button goNext;

    // Start is called before the first frame update
    void Start()
    {
        winPanel.SetActive(false);
        gameBoard = GetComponent<GameBoardBehaviour>();

        triesText.text = "Atempts: " + tries;

        goNext.onClick.AddListener(NextLevel);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ball == null)
        {
            return;
        }
        //when ball enters winbox collition trigger
        if (winBox.bounds.Contains(ball.transform.position))
        {
            Debug.Log("enterend win box");
            Win();
        }
        else if (deathBox.bounds.Contains(ball.transform.position))
        {
            gameBoard.resetBoard();
            triesText.text = "Atempts: " + ++tries;
        }
    }
    private void Win()
    {
        winPanel.SetActive(true);
    }
    private void NextLevel()
    {
        GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene());
    }
}
