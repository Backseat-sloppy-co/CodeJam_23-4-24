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
    [SerializeField] private TextMeshProUGUI movestext; // Text for number of moves
    public int moves = 0;
    private float nextSceneTime = 5f;
    [SerializeField] private GameObject confettiScreen; // Canvas with a particle system for confetti

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
        flippedCards = 0; // Set flipped cards to 0
        _transform = GetComponent<Transform>(); // Get the transform of the object
        // 6 pairs of cards
        faceIndex = new List<int> { chickenIndex, chickenIndex, deerIndex, deerIndex, dogIndex, dogIndex, horseIndex, horseIndex, penguinIndex, penguinIndex, tigerIndex, tigerIndex }; 

        startnumberofCards = numberofCards; // Save the number of cards at the start
        for (int i = 0; i < startnumberofCards; i++) // loop through the amount of cards we want
        {
            int randomIndex = Random.Range(0, faceIndex.Count); // Get a random index from the list
            Debug.Log("Faceindex:" + faceIndex.Count); // Debug the amount of faces in the list
            GameObject temp = Instantiate(card, _transform); // Instantiate a card
            //CardGameBehaviour tempstorage = temp.GetComponentInChildren<CardGameBehaviour>();
            //tempstorage.AssignFace(faceIndex[randomIndex]);
            temp.GetComponent<CardGameBehaviour>().AssignFace(faceIndex[randomIndex]); // Assign the index to the card
            faceIndex.RemoveAt(randomIndex); // Remove the index from the list
            numberofCards--; // Decrease the number of cards
            Debug.Log("Number of cards: " + numberofCards); // Debug the number of cards
        }
    }
    public void UpdateMoves() // Update the moves text
    {
        movestext.text = moves.ToString(); 
    }
    

    public void CheckifDone() // Check if the game is done
    {
        Debug.Log("Flipped cards: " + flippedCards); // Debug the flipped cards
        
        if (flippedCards == startnumberofCards) // if win condition met do this
        {
            AudioManager.instance.Play("WinFanfare"); // Play the win fanfare
            confettiScreen.SetActive(true); // Activate the confetti screen
            GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime)); // Load the next scene
        }
    }
    

}
