using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardBehaviour : MonoBehaviour
{
    //all code is written with the help of copilot and the unity documentation.

    [Header("ref")]
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private TMP_Text gyroText;
    [SerializeField] private GameObject gizmo;
    private GameObject ball;
    public GameObject ballPrefab;

    [Header("values")]
    public float InputSpeed = 1f;
    public float manuelDamp = 10f;
    public float gyroDamp = 5f;

    [HideInInspector] public Vector3 manuelInput;
    [HideInInspector] public GameLogic gameLogic;
    
    // Start is called before the first frame update
    void Start()
    {

        gameLogic = GetComponent<GameLogic>();

        //if the game is running on android enable gyro

        if (Application.platform == RuntimePlatform.Android)
        {
            Input.gyro.enabled = true;
            Debug.Log("Gyro Enabled");
            //Camera.main.transform.SetParent(cameraPivot);
        }
        else
        {
            Input.gyro.enabled = false;
            Debug.Log("Gyro Disabled");
        }
    }

    //this runnes when the calibrations is done, was also used when testing the game on the computer.
    public void resetBoard()
    {
        Destroy(ball);
        ball = Instantiate(ballPrefab, Camera.main.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().sleepThreshold = 0.0f;

        gameLogic.ball = ball;

    }

    private void FixedUpdate()
    {
        //if gyro is enabled use gyro controlles else use keyboard input.
        if (Input.gyro.enabled) 
        {
            RuntimeGyroBoard();
            
            gyroText.text = "Gyro: " + Input.gyro.attitude.eulerAngles;
        }
        else if (!Input.gyro.enabled)
        {
            RuntimeInputBoard();
            gyroText.text = "Gyro: " + manuelInput;
        }
        //reset board with space for debugging purposes.
        if (Input.GetKey(KeyCode.Space))
        {
            resetBoard();
        }
    }

    private void RuntimeGyroBoard()
    {
        //get gyro input and transform it to a vector3

        float x, y, z;
        Vector3 gyro = Input.gyro.attitude.eulerAngles;

        x = -gyro.x;
        y = 0;
        z = -gyro.y;

        //remap the x,y,z to based on gyro input with help from the unity documentation.

        Vector3 inputGyro = new Vector3(x, y, z);

        //rotate board based on gyro input over time.
        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Quaternion.Euler(inputGyro), Time.deltaTime * gyroDamp);

        gizmo.transform.rotation = Quaternion.Slerp(gizmo.transform.rotation, Quaternion.Euler(inputGyro), Time.deltaTime * gyroDamp);
    }

    private void RuntimeInputBoard()
    {
        //uses arrow keys to move the board on two axis, x and z.
        if (Input.GetKey(KeyCode.UpArrow))
        {
            manuelInput.x += 1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            manuelInput.x += -1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            manuelInput.z += 1f * InputSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            manuelInput.z += -1f * InputSpeed;
        }

        //rotate board based on input over time.
        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Quaternion.Euler(manuelInput), Time.deltaTime * manuelDamp);
        //rotates debugging gizmo based on input.
        gizmo.transform.rotation = Quaternion.Slerp(gizmo.transform.rotation, Quaternion.Euler(manuelInput), Time.deltaTime * manuelDamp);
    }
}
