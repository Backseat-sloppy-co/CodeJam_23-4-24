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
    [SerializeField] TMP_Text winText, loseText, fillMoreText, startText;
    Animator animator;
    private float timeSinceLastDrop = 0f;
    private bool readyToPour = false;
    private float nextSceneTime = 1.5f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            startText.gameObject.SetActive(false);
            fillMoreText.gameObject.SetActive(false);

            if (readyToPour)
            {
                Instantiate(waterPrefab, firePoint.transform.position, Quaternion.identity);
            }
        }
        else
        {
            timeSinceLastDrop += Time.deltaTime;
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
        GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));
    }

    void LoseCondition()
    {
        loseText.gameObject.SetActive(true);
        GameManager.instance.StartCoroutine(GameManager.instance.NextRandomScene(nextSceneTime));
    }


}
