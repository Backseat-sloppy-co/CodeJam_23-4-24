using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] lifeIcons;
    private float intTime;
    public TMPro.TextMeshProUGUI text;
    private float handIconTime = 2.5f;
    public Image handIcon;

    void Start()
    {
        intTime = 0;
    }

    void Update()
    {

        if (intTime >= handIconTime)
        {
            handIcon.enabled = false;
        }

        intTime += Time.deltaTime;
        var intFloat = (int)intTime;
        text.text = intFloat.ToString();
    }
    public void UpdateLifeIcons(int lifeCount)
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            if (i < lifeCount)
            {
                lifeIcons[i].enabled = true;
            }
            else
            {
                lifeIcons[i].enabled = false;
            }
        }
    }
}
// this code was written with the help of copilot.