using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardGameBehaviour : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite cardFace;
    [SerializeField] private AssignCards assignCards;
    
    private bool isFlipped = false;
    private bool isMatched = false;
    private int faceIndex;
    private int amountofCardsFlipped = 0;

    public void FlipCard()
    {
        if (!isFlipped && !isMatched)
        {
            button.image.sprite = cardFace;
            isFlipped = true;
        }
        else if (isFlipped && !isMatched)
        {
            button.image.sprite = cardBack;
            isFlipped = false;
        }

        if (amountofCardsFlipped < 2)
        {
            amountofCardsFlipped++;
        }
        else
        {
            amountofCardsFlipped = 0;
        }
    }

    public int GetFaceIndex()
    {
        return faceIndex;
    }

    public void Unflip()
    {
        button.image.sprite = cardBack;
        isFlipped = false;
    }

    public void Match()
    {
        isMatched = true;
    }

 


    
}
