using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerBehavior : MonoBehaviour
{
// this script enables the Deer to jump when touching a colider with the Ground tag, using rigid body.
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    public bool isGrounded = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        Debug.Log(isGrounded);
      if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Jump");
            rb.velocity = Vector2.up * jumpForce;
        }

    }
}
