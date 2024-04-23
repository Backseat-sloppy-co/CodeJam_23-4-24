using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerBehavior : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private CapsuleCollider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (Physics.CheckCapsule(col.bounds.min, col.bounds.max, col.radius, LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            if (Input.touchCount > 0)
                jump();

            if (Input.GetMouseButtonDown(0))
                jump();
        }
        void jump()
        {
            rb.velocity = Vector3.up * jumpForce;
        }

  
    }
   void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Penguin"))
            {
                Destroy(collision.gameObject);
                Debug.Log("Penguin Destroyed!");
            }
        }
}