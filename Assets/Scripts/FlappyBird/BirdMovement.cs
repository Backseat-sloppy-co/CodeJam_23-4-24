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
    private float time;
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

            AudioManager.instance.Play("Whoosh");
        }

       time += Time.deltaTime;
        var intTime = (int)time;
        text.text = intTime.ToString();

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
            
            Instantiate(deathPrefab, transform.position, Quaternion.identity);

            AudioManager.instance.Play("Explosion");

            GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(1.75f));
        }
    }
}
