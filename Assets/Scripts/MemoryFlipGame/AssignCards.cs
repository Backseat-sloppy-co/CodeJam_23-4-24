using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This script was partially made by following a tutorial on YouTube by Mr. Kaiser
// The link to the tutorial is: https://www.youtube.com/watch?v=bdOeOvvOKl8
// This script was also made with the help of Github Copilot.

public class AssignCards : MonoBehaviour
{

    [SerializeField] private GameObject card;
    public List<int> faceIndex;
    private int numberofCards = 12;
    private int startnumberofCards;
    private Transform _transform;
    public int flippedCards;
    public CardGameBehaviour firstCard;
    [SerializeField] private TextMeshProUGUI movestext;
    public int moves = 0;
    private float nextSceneTime = 5f;
    [SerializeField] private GameObject confettiScreen;


    // Start is called before the first frame update
    void Start()
    {
        flippedCards = 0;
        _transform = GetComponent<Transform>();
        faceIndex = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };

        startnumberofCards = numberofCards;
        for (int i = 0; i < startnumberofCards; i++)
        {
            int randomIndex = Random.Range(0, faceIndex.Count);
            Debug.Log("Faceindex:" + faceIndex.Count);
            GameObject temp = Instantiate(card, _transform);
            //CardGameBehaviour tempstorage = temp.GetComponentInChildren<CardGameBehaviour>();
            //tempstorage.AssignFace(faceIndex[randomIndex]);
            temp.GetComponent<CardGameBehaviour>().AssignFace(faceIndex[randomIndex]);
            faceIndex.RemoveAt(randomIndex);
            numberofCards--;
            Debug.Log("Number of cards: " + numberofCards);
        }
    }
    public void UpdateMoves()
    {
        movestext.text = moves.ToString();
    }
    

    public void CheckifDone()
    {
        Debug.Log("Flipped cards: " + flippedCards);
        
        if (flippedCards == startnumberofCards) // if win condition met do this
        {
            AudioManager.instance.Play("WinFanfare");
            confettiScreen.SetActive(true);
            GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));
        }
    }
    

}
