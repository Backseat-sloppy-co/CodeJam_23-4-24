using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomFill : FillArea
{
     public bool IsBottomFilled()
    {
        if (isFilled)
        {
            Debug.Log("Bottom is filled");
            return true;
        }
        return false;
    }
}
