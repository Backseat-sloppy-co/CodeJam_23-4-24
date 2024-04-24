using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillArea : MonoBehaviour
{
    public int areaNumber; // Variable to store the area number

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            Debug.Log("You have filled the bottomFill with water in Area " + areaNumber);
        }
        else
        {
            Debug.Log("No water detected in Area " + areaNumber);
        }
    }
}
