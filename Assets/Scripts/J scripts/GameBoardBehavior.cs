using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private TMP_Text gyroText;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Button reset;
    [SerializeField] private GameObject ball;

    public float damp = 0.5f;
    public float InputSpeed = 1f;
    public float stableThreshold = 0.1f;

    private Vector3 manuelInput = new Vector3(0, 0, 0);
    private bool isStable = false;

    private Vector3 gyroInput = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        SetDefaults();
        GameObject _ball = Instantiate(ball, cameraPivot.position, Quaternion.identity);
        _ball.GetComponent<Rigidbody>().sleepThreshold = 0.0f;  

        reset.onClick.AddListener(resetBoard);

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

    private void resetBoard()
    {
        gameBoard.transform.rotation = Quaternion.Euler(0, 0, 0);
        manuelInput = new Vector3(0, 0, 0);
        Instantiate(ball, cameraPivot.position, Quaternion.identity);
        SetDefaults();
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
            
            gyroText.text = "Gyro: " + Input.gyro.attitude.eulerAngles;
        }
        else if (!Input.gyro.enabled)
        {
            RuntimeInputBoard();
            gyroText.text = "Gyro: " + manuelInput;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            resetBoard();
        }
    }

    private void SetDefaults()
    {
        gyroInput = Input.gyro.attitude.eulerAngles;
        gyroInput.z = gyroInput.y;
        gyroInput.y = 0;
        gyroInput.x *= -1;
        gyroInput.z *= -1;
    }

    private void RuntimeGyroBoard()
    {
        //var diff = Input.gyro.attitude.eulerAngles - gyroInput;
        //gameBoard.transform.rotation = Quaternion.Euler(diff) ;
        //gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Input.gyro.attitude, damp);

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
