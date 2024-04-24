using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float jumpForce = 10f;
    public TMP_Text text;
    public float upperLimit = 6f;
    public float lowerLimit = -4.25f;
    private float intTime;
    private Rigidbody rb;

    public GameObject deathPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

       intTime += Time.deltaTime;
        var intFloat = (int)intTime;
        text.text = intFloat.ToString();

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            transform.localScale = new Vector3(5, 0.5f, 3);
            rb.isKinematic = true;
            rb.angularVelocity = Vector3.up * 50f;

            GameObject death = Instantiate(deathPrefab, transform.position, Quaternion.identity);
            var main = death.GetComponent<ParticleSystem>().main;
            main.loop = false;
        }
    }
}
