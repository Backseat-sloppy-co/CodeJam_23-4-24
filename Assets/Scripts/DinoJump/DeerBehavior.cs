using System.Collections;
using System.Collections.Generic;
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

    public Sound[] horseSound;

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
                jump();

            if (Input.GetMouseButtonDown(0))
                jump();
        }
        void jump()
        {
            rb.velocity = Vector3.up * jumpForce;
        }

        // force the deer to be at the same z and x position
        transform.position = new Vector3(0, transform.position.y, 0);
        transform.rotation = rotation;

        if (lifeCounter == 0)
        {
            Debug.Log("Game Over!");

     

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
           // horseSound[Random.Range(0, horseSound.Length)].Play();

        }
    }
}