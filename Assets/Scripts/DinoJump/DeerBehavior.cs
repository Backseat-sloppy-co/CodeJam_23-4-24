using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.UIElements;

public class DeerBehavior : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody rb;
    private bool isGrounded;
    private CapsuleCollider col;

    public int lifeCounter = 3;

    private UIManager uiManager;

    private Quaternion rotation = Quaternion.Euler(-0.557f, 97.747f, -0.003f);

    private float nextSceneTime = 2f;

    private bool isGameOverStarted = false;



    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
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
                StartCoroutine(jump());

            if (Input.GetMouseButtonDown(0))
                StartCoroutine(jump());
        }
        else
        {
            if (Input.touchCount > 0)
                StartCoroutine(fall());

            if (Input.GetMouseButtonDown(0))
               StartCoroutine(fall());



        }

        IEnumerator jump()
        {
            rb.velocity = Vector3.up * jumpForce;
            AudioManager.instance.Play("Jump");
            yield return new WaitForSeconds(0.5f); // wait for half a second
        }

        IEnumerator fall()
        {
           
            rb.velocity = Vector3.down * jumpForce;
            AudioManager.instance.Play("Down");
            yield return new WaitForSeconds(0.5f); // wait for half a second
        }

        // force the deer to be at the same z and x position
        transform.position = new Vector3(0, transform.position.y, 0);
        transform.rotation = rotation;

        if (lifeCounter == 0 && !isGameOverStarted)
        {
            Debug.Log("Game Over!");

            isGameOverStarted = true;

            GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));



        }

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Penguin"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Penguin Destroyed!");
            lifeCounter--;
            uiManager.UpdateLifeIcons(lifeCounter);

            // Array of sound names
            string[] horseSounds = { "horse1", "horse2", "horse3" };

            // Select a random sound name
            string randomSound = horseSounds[Random.Range(0, horseSounds.Length)];

            // Play the random sound
            AudioManager.instance.Play(randomSound);
        }
    }
}