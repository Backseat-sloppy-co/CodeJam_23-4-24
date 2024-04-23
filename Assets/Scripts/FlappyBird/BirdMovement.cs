using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravity = 1f;
    public TMP_Text text;
    public float upperLimit = 6f;
    public float lowerLimit = -4.25f;

    private float magnitude = 0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //transform.Translate(Vector3.up * jumpForce);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        magnitude = Input.acceleration.magnitude;
        magnitude = Mathf.Clamp(magnitude, 0f, 1f);
        //magnitude *= Time.deltaTime;

        
        transform.Translate(Vector3.up * jumpForce * magnitude);
        text.text = "" + magnitude;

        //transform.Translate(Vector3.down * gravity * Time.deltaTime);

        if(transform.position.y > upperLimit)
        {
            transform.position = new Vector3(transform.position.x, upperLimit, transform.position.z);
            rb.velocity = Vector3.zero;
        }else if(transform.position.y < lowerLimit)
        {
            transform.position = new Vector3(transform.position.x, lowerLimit, transform.position.z);
            rb.velocity = Vector3.zero;
        }
    }
}
