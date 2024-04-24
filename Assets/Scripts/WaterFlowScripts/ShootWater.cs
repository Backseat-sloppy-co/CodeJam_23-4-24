using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class ShootWater : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject topFill, targetFill, bottomFill;
    [SerializeField] GameObject firePoint;
    Animator animator;
    private float timeSinceLastDrop = 0f;
    private bool readyToPour = false;


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
                            Debug.Log("You lose!");
                        }
                        else
                        {
                            Debug.Log("You win!");
                        }
                    }
                    else
                    {
                        Debug.Log("Fill more!");
                    }
                }
            }
        }
    }

}
