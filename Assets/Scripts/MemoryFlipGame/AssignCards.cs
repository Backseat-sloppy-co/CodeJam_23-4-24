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

    // CardFaceIndexes
    private int chickenIndex = 0;
    private int deerIndex = 1;
    private int dogIndex = 2;
    private int horseIndex = 3;
    private int penguinIndex = 4;
    private int tigerIndex = 5;


    // Start is called before the first frame update
    void Start()
    {
        flippedCards = 0;
        _transform = GetComponent<Transform>();
        // 6 pairs of cards
        faceIndex = new List<int> { chickenIndex, chickenIndex, deerIndex, deerIndex, dogIndex, dogIndex, horseIndex, horseIndex, penguinIndex, penguinIndex, tigerIndex, tigerIndex }; 

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
