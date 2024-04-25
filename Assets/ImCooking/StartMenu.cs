using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public Camera startMenuCamera; // Reference to the start menu camera

    public void StartGame()
    {
        // Enable the main camera and disable the start menu camera
        mainCamera.depth = 1;
        startMenuCamera.depth = -1;
    }
}