using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBoardBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private TMP_Text gyroText;

    public float damp = 0.5f;
    public float InputSpeed = 1f;

    private Vector3 manuelInput = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Input.gyro.enabled = true;
            Debug.Log("Gyro Enabled");
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
        if (Input.gyro.enabled) {
            RuntimeGyroBoard();
            gyroText.text = "Gyro: " + Input.gyro.attitude;
        }
        else
        {
            RuntimeInputBoard();
            gyroText.text = "Gyro: " + manuelInput;
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
            manuelInput.y += 0.1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            manuelInput.y += -0.1f * InputSpeed;
        }

        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Quaternion.Euler(manuelInput), damp);
    }
}
