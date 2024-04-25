using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopFill : FillArea
{
    public bool IsTopFilled()
    {
        if (isFilled)
        {
            Debug.Log("Top is filled");
            return true;
        }

        return false;
    }
}
