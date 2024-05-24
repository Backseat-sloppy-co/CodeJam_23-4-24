using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using TMPro;

// This script has been made with the assistance of Github Copilot

public class ShootWater : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject topFill, targetFill, bottomFill;
    [SerializeField] GameObject firePoint;
    [SerializeField] TMP_Text winText, loseText, fillMoreText;
    [SerializeField] GameObject startImage;
    Animator animator;
    private float timeSinceLastDrop = 0f;
    private bool readyToPour = false;
    private bool changedScene = false;
    private bool pouringSoundPlaying = false;
    private float nextSceneTime = 4f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AudioManager.instance.Play("Flyby");
    }

    void WaterAnimationDone()
    {
        readyToPour = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0)) // This is true if the left mouse button is pressed in the editor, or screen is touched on mobile
        {
            timeSinceLastDrop = 0f; // Reset the time since last drop to 0
            animator.SetBool("isPouring", true); // Set the animator bool to true, starting the pouring animation  
            startImage.SetActive(false); // Hide the start image
            fillMoreText.gameObject.SetActive(false); // Hide the fill more text
           

            if (readyToPour) // If the animation is done, instantiate the water prefab at the fire point
            {
                Instantiate(waterPrefab, firePoint.transform.position, Quaternion.identity); // Instantiate the water prefab at the fire point
                if (!pouringSoundPlaying) // If the pouring sound is not playing, play it
                {
                    AudioManager.instance.Play("PourWater"); // Play the pouring water sound
                    pouringSoundPlaying = true; // Set the pouring sound playing bool to true
                }

            }
        }
        else
        {
            timeSinceLastDrop += Time.deltaTime; // Increment the time since last drop by the time since the last frame
            AudioManager.instance.Stop("PourWater"); // Stop the pouring water sound
            pouringSoundPlaying = false; // Set the pouring sound playing bool to false
            animator.SetBool("isPouring", false); // Set the animator bool to false, stopping the pouring animation
            readyToPour = false; // Set the ready to pour bool to false
            if (timeSinceLastDrop > 1f) // If the time since the last drop is greater than 1 second, check how much water the player has poured
            {
                if (bottomFill.GetComponent<BottomFill>().IsBottomFilled()) // If the bottom fill is filled, check the target fill
                {
                    if (targetFill.GetComponent<TargetFill>().IsTargetFilled()) // If the target fill is filled, check the top fill
                    {
                        if (topFill.GetComponent<TopFill>().IsTopFilled()) // If the top fill is filled, the player has poured too much, so they lose
                        {
                            LoseCondition();
                        }
                        else // If the top fill is not filled, the player has poured the correct amount, so they win
                        {
                            WinCondition();
                        }
                    }
                    else // If the bottom fill is filled but the target fill is not, the player has not poured enough, prompt them to fill more
                    {
                        fillMoreText.gameObject.SetActive(true); // Show the fill more text
                    }
                }
            }
        }
    }

    void WinCondition()
    {
        winText.gameObject.SetActive(true);
        if (!changedScene)
        {
            AudioManager.instance.Play("WinFanfare");
            GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));
            changedScene = true;
        }
    }

    void LoseCondition()
    {
        loseText.gameObject.SetActive(true);
        if (!changedScene)
        {
            AudioManager.instance.Play("LoseSound");
            GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));
            changedScene = true;
        }
    }


}
