using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardBehaviour : MonoBehaviour
{
    [Header("ref")]
    [SerializeField] private GameObject gameBoard;
    [SerializeField] private TMP_Text gyroText;
    [SerializeField] private Button reset;
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
        reset.onClick.AddListener(resetBoard);

        gameLogic = GetComponent<GameLogic>();

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

    public void resetBoard()
    {
        Destroy(ball);
        ball = Instantiate(ballPrefab, Camera.main.transform.position, Quaternion.identity);
        ball.GetComponent<Rigidbody>().sleepThreshold = 0.0f;

        gameLogic.ball = ball;

    }

    private void FixedUpdate()
    {
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

        if (Input.GetKey(KeyCode.Space))
        {
            resetBoard();
        }
    }

    private void RuntimeGyroBoard()
    {
        float x, y, z;
        Vector3 gyro = Input.gyro.attitude.eulerAngles;

        x = -gyro.x;
        y = 0;
        z = -gyro.y;

        Vector3 inputGyro = new Vector3(x, y, z);

        //rotate board based on gyro input in a sertain amount of time
        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Quaternion.Euler(inputGyro), Time.deltaTime * gyroDamp);

        gizmo.transform.rotation = Quaternion.Slerp(gizmo.transform.rotation, Quaternion.Euler(inputGyro), Time.deltaTime * gyroDamp);
    }

    private void RuntimeInputBoard()
    {
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

        gameBoard.transform.rotation = Quaternion.Slerp(gameBoard.transform.rotation, Quaternion.Euler(manuelInput), Time.deltaTime * manuelDamp);

        gizmo.transform.rotation = Quaternion.Slerp(gizmo.transform.rotation, Quaternion.Euler(manuelInput), Time.deltaTime * manuelDamp);
    }
}
