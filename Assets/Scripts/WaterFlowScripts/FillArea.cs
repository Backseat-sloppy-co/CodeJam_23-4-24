using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillArea : MonoBehaviour
{

    protected bool isFilled = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            isFilled = true;
        }
        else
        {
            isFilled = false;
        }
    }
}
