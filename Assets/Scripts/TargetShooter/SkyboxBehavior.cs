using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SkyboxBehavior : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
  //this script is used to rotate the skybox
   
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
