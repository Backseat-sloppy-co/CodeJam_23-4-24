using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Food : MonoBehaviour
{
    public Material materialCooked;
    public Material materialBurnt;
    public float cookingTime;
    public float burningTime;
    public Transform panTransform;
    public Transform tableTransform;
    public bool isOnPan = false;
    
    public ScoreManager scoreManager; // Reference to the ScoreManager

    private void Awake()
    {
        // Add a Rigidbody component if it doesn't exist
        Rigidbody rb = GetComponent<Rigidbody>();

        // Check if the Rigidbody component exists
        if (rb == null)
        {
            Debug.LogWarning("Rigidbody component not found. Adding one now.");
            rb = gameObject.AddComponent<Rigidbody>();
        }

        // Set the Rigidbody properties
        rb.mass = 0.5f;
        rb.drag = 0.0f;
        rb.useGravity = true;
    }

    private void OnMouseDown()
    {
        if (isOnPan)
        {
            // Move the food to a different location
            transform.position = tableTransform.position; 
            scoreManager.cookedFood++;
        }
        else
        {
            isOnPan = true;
            // Move the food object to the pan when it's clicked
            Vector3 panPosition = panTransform.position;
            panPosition.y += 30.0f; // Adjust the y-axis as needed
            transform.position = panTransform.position;

        }
    }


}

