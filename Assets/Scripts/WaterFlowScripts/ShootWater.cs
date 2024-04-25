using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using TMPro;

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
        if (Input.GetMouseButton(0))
        {
            timeSinceLastDrop = 0f;
            animator.SetBool("isPouring", true);
            startImage.SetActive(false);
            fillMoreText.gameObject.SetActive(false);
           

            if (readyToPour)
            {
                Instantiate(waterPrefab, firePoint.transform.position, Quaternion.identity);
                if (!pouringSoundPlaying)
                {
                    AudioManager.instance.Play("PourWater");
                    pouringSoundPlaying = true;
                }

            }
        }
        else
        {
            timeSinceLastDrop += Time.deltaTime;
            AudioManager.instance.Stop("PourWater");
            pouringSoundPlaying = false;
            animator.SetBool("isPouring", false);
            readyToPour = false;
            if (timeSinceLastDrop > 1f)
            {
                if (bottomFill.GetComponent<BottomFill>().IsBottomFilled())
                {
                    if (targetFill.GetComponent<TargetFill>().IsTargetFilled())
                    {
                        if (topFill.GetComponent<TopFill>().IsTopFilled())
                        {
                            LoseCondition();
                        }
                        else
                        {
                            WinCondition();
                        }
                    }
                    else
                    {
                        fillMoreText.gameObject.SetActive(true);
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
