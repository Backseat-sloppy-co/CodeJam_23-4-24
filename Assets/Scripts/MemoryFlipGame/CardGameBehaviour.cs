using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// This script was partially made by following a tutorial on YouTube by Mr. Kaiser
// The link to the tutorial is: https://www.youtube.com/watch?v=bdOeOvvOKl8
// This script was also made with the help of Github Copilot.
public class CardGameBehaviour : MonoBehaviour
{
    [SerializeField] private Button button; // Button for the card
    [SerializeField] private Sprite cardBack; // Card back sprite
    [SerializeField] private Sprite[] cardFace; // Card face sprites
    [SerializeField] private AssignCards assignCards; // AssignCards script
    
    
    private bool isFlipped = false; // bool for if the card is flipped
    private bool isMatched = false; // bool for if the card is matched
    private int faceIndex; // Index for the face of the card

    public void Start()
    {
        button.image.sprite = cardBack; // Set the card to the card back sprite
    }

    public void FlipCard()
    {
        if (!isFlipped && !isMatched) // If the card is not flipped and not matched
        {
            Flip(); // Flip the card
            CheckMatch(); // Check if the card is a match
        }
       
    }
 
    public void CheckMatch() // Check if the card is a match
    {
        if (assignCards.firstCard == null) // If the first card is null
        {
            assignCards.firstCard = this; // Set the first card to this card
        }
        else if (assignCards.firstCard.faceIndex == faceIndex && assignCards.firstCard != this) // If the first card face index is the same as this card face index
                                                                                                // and the first card is not this card
        {
            Match(); // Match this card
            assignCards.firstCard.Match(); // Match the first card
            assignCards.firstCard = null; // Set the first card to null
        }
        else // If the first card is not null and the face indexes are not the same
        {
            Unflip(); // Unflip this card
            assignCards.firstCard.Unflip(); // Unflip the first card
            assignCards.firstCard = null; // Set the first card to null
        }
    }

 
    public void AssignFace(int index) 
    {
        faceIndex = index; // Assign the index gotten from AssignCards to the index in this script
    }
    private void Unflip()
    {
        StartCoroutine(Wait1Second()); // Wait 1 second before flipping the card back
    }

    private void Flip()
    {
        button.image.sprite = cardFace[faceIndex]; // Set the card to the face index
        isFlipped = true; // Set the card to flipped
        assignCards.moves++; // Increase the number of moves
        assignCards.UpdateMoves(); // Update the moves text
    }

    public void Match()
    {
        isMatched = true; // Set the card to matched
        assignCards.flippedCards++; // Increase the number of flipped cards
        assignCards.CheckifDone(); // Check if the game is done
    }

    public IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(1); // Wait for 1 second
        button.image.sprite = cardBack; // Set the card to the card back sprite
        isFlipped = false; // Set the card to not flipped
    }

 


    
}
