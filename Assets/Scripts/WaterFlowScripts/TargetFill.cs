using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFill : FillArea
{
    public bool IsTargetFilled()
    {
        if (isFilled)
        {
            Debug.Log("Target is filled");
            return true;
        }

        return false;
    }
}
