using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSkyBox : MonoBehaviour
{
    private float skyBoxSpeed = 7f;


    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);

    }
}
// Made using: https://www.youtube.com/watch?v=TxqSU0hPROEs