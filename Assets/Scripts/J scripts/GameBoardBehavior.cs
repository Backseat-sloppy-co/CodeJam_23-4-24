using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBoardBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private TMP_Text gyroText;
    [SerializeField] private Transform cameraPivot;

    public float damp = 0.5f;
    public float InputSpeed = 1f;
    public float stableThreshold = 0.1f;

    private Vector3 manuelInput = new Vector3(0, 0, 0);
    private bool isStable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Input.gyro.enabled = true;
            Debug.Log("Gyro Enabled");
            Camera.main.transform.SetParent(cameraPivot);
        }
        else
        {
            Input.gyro.enabled = false;
            Debug.Log("Gyro Disabled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //make a check to see if device is facing flat up
        //if (Input.gyro.enabled)
        //{
        //    if (Input.gyro.attitude.x < stableThreshold & Input.gyro.attitude.x > -stableThreshold & 
        //        Input.gyro.attitude.y < stableThreshold & Input.gyro.attitude.y > -stableThreshold)
        //    {
        //        isStable = true;
        //    }
        //    else
        //    {
        //        isStable = false;
        //    }
        //}

        if (Input.gyro.enabled) {
            RuntimeGyroBoard();
            gyroText.text = "Gyro: " + Input.gyro.attitude;
        }
        else if (!Input.gyro.enabled)
        {
            RuntimeInputBoard();
            gyroText.text = "Gyro: " + manuelInput;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            gameBoard.transform.rotation = Quaternion.Euler(0, 0, 0);
            manuelInput = new Vector3(0, 0, 0);
        }
    }

    private void RuntimeGyroBoard()
    {
        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Input.gyro.attitude, damp);
    }

    private void RuntimeInputBoard()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            manuelInput.x += 0.1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            manuelInput.x += -0.1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            manuelInput.z += 0.1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            manuelInput.z += -0.1f * InputSpeed;
        }

        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Quaternion.Euler(manuelInput), damp);
    }
}
