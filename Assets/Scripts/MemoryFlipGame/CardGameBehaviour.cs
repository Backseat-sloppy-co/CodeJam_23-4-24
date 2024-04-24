using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// This script was partially made by following a tutorial on YouTube by Mr. Kaiser
// The link to the tutorial is: https://www.youtube.com/watch?v=bdOeOvvOKl8
// This script was also made with the help of Github Copilot.
public class CardGameBehaviour : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private Sprite[] cardFace;
    [SerializeField] private AssignCards assignCards;
    
    private bool isFlipped = false;
    private bool isMatched = false;
    private int faceIndex;

    public void Start()
    {
        button.image.sprite = cardBack;
    }

    public void FlipCard()
    {
        if (!isFlipped && !isMatched)
        {
            Flip();
            CheckMatch();
        }
       
    }
 
    public void CheckMatch()
    {
        if (assignCards.firstCard == null)
        {
            assignCards.firstCard = this;
        }
        else if (assignCards.firstCard.faceIndex == faceIndex && assignCards.firstCard != this)
        {
            Match();
            assignCards.firstCard.Match();
            assignCards.firstCard = null;
        }
        else
        {
            Unflip();
            assignCards.firstCard.Unflip();
            assignCards.firstCard = null;
        }
    }

 
    public void AssignFace(int index)
    {
        faceIndex = index;
    }
    private void Unflip()
    {
        StartCoroutine(Wait1Second());
    }

    private void Flip()
    {
        button.image.sprite = cardFace[faceIndex];
        isFlipped = true;
    }

    public void Match()
    {
        isMatched = true;
        assignCards.flippedCards++;
        assignCards.CheckifDone();
    }

    public IEnumerator Wait1Second()
    {
        yield return new WaitForSeconds(1);
        button.image.sprite = cardBack;
        isFlipped = false;
    }

 


    
}
