using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    //all code is written with the help of copilot and the unity documentation.

    private GameBoardBehaviour gameBoard;
    [HideInInspector] public GameObject ball;
    private int tries = 0;

    [SerializeField] private BoxCollider winBox;
    [SerializeField] private BoxCollider deathBox;
    [SerializeField] private TMP_Text triesText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button goNext;

    private bool deathNext;

    // Start is called before the first frame update
    void Start()
    {
        deathNext = false;
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
        //this is done in this script because i didnt want 2 scripts with one function on using collision enter for the win and lose trigger boxes.

        if (winBox.bounds.Contains(ball.transform.position))
        {
            Debug.Log("enterend win box");
            Win();
        }
        else if (deathBox.bounds.Contains(ball.transform.position))
        {
            gameBoard.resetBoard();
            triesText.text = "Forsøg: " + ++tries;
            deathNext = true;
        }

        //when ball enters deathbox collition trigger go to next level.
        if (deathNext)
        {
            NextLevel();
            deathNext = false;
            return;
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
