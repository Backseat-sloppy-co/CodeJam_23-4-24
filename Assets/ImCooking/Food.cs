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

}

