using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWater : MonoBehaviour
{
    [SerializeField] GameObject waterPrefab;
    [SerializeField] GameObject topFill, targetFill, bottomFill;
    private float timeSinceLastDrop = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Instantiate(waterPrefab, transform.position, Quaternion.identity);
            timeSinceLastDrop = 0f;
        }
        else
        {
            timeSinceLastDrop += Time.deltaTime;
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
