using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootWater : MonoBehaviour
{

    [SerializeField] GameObject waterDropPrefab;
    [SerializeField] GameObject topFill, bottomFill, targetFill;

    private float timeSinceLastDrop = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // the water drop prefab will be instantiated at the position of the object that this script is attached to
        if (Input.GetMouseButton(0))
        {
            Instantiate(waterDropPrefab, transform.position, Quaternion.identity);
            timeSinceLastDrop = 0f; 
        }
        else
        {
            timeSinceLastDrop += Time.deltaTime;
            if (timeSinceLastDrop > 2f)
            {
                CheckBottomFillLevel();
            }
        }

    }

    void CheckBottomFillLevel()
    {
        //This method should be called when the user stops shooting water
        //It will check if the player has filled the bottomFill, targetFill and topFill with water, which is defined by game objects with the tag "Water"
        //If the player has filled the bottomFill only, a meassage will be displayed to the player
        //If the player has filled the bottomFill and targetFill, a message will be displayed to the player
        //If the player has filled the bottomFill, targetFill and topFill, a message will be displayed to the player
        //Please begin writing the method
        Debug.Log("Checking fill level");

        GameObject[] bottomFillWater = GameObject.FindGameObjectsWithTag("Water");
        


        if (bottomFillWater.Length > 0)
        {
            Debug.Log("You have filled the bottomFill with water");
            CheckTargetFillLevel();
        }
        else
        {
            Debug.Log("You have not filled any of the fills with water");
        }
    }

    void CheckTargetFillLevel()
    {
        GameObject[] targetFillWater = GameObject.FindGameObjectsWithTag("Water");

        if (targetFillWater.Length > 0)
        {
            Debug.Log("You have filled the targetFill with water");
            CheckTopFillLevel();
        }
        else
        {
            Debug.Log("You have not filled the targetFill with water");
        }
    }

    void CheckTopFillLevel()
    {
        GameObject[] topFillWater = GameObject.FindGameObjectsWithTag("Water");

        if (topFillWater.Length > 0)
        {
            Debug.Log("You have filled the topFill with water");
            CheckTopFillLevel();
        }
        else
        {
            Debug.Log("You have not filled the topFill with water");
        }
    }

    void WinCondition()
    {

    }

    void LoseCondition()
    {

    }

}
