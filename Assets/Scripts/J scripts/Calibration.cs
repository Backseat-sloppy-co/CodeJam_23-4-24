using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
    [Header("calibration")]
    [SerializeField] private GameObject calPanel;
    [SerializeField] private GameObject point;

    private GameBoardBehaviour gameBehaviour;

    [HideInInspector] public bool isCalibrated = false;

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        gameBehaviour = GetComponent<GameBoardBehaviour>();

        calPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsCalibrated())
        {
            point.GetComponent<Image>().color = Color.red;
            calPanel.transform.GetChild(1).GetComponent<Image>().color = Color.red;

            StopAllCoroutines();
        }
        else
        {
            point.GetComponent<Image>().color = Color.green;
            calPanel.transform.GetChild(1).GetComponent<Image>().color = Color.green;

            StartCoroutine(CalCountDown());
        }
    }
   
    IEnumerator CalCountDown()
    {
        
        yield return new WaitForSeconds(3);
        StopAllCoroutines();
        calPanel.SetActive(false);
        isCalibrated = true;
        gameBehaviour.resetBoard();
        this.enabled = false;
    }

    public bool IsCalibrated()
    {
        //Vector3 gyro = Input.gyro.attitude.eulerAngles;
        float x, y;
        //get gyro x 0 to 20 transform target between 0 to 10

        if (!Input.gyro.enabled)
        {
            Vector3 gyro = gameBehaviour.manuelInput;

            x = gyro.z;
            y = gyro.x;
            point.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);

            if (x > -20 && x < 20 && y > -20 && y < 20)
            {
                return true;
            }
            else
                return false;
        }
        else
        {
            Vector3 _gyro = Input.gyro.attitude.eulerAngles;

            x = _gyro.y;
            y = _gyro.x;

            point.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
            
            if (((x >= 340f && x <= 360f) || (x >= 0f && x <= 20f)) && ((y >= 340f && y <= 360f) || (y >= 0f && y <= 20f)))
            {
                return true;
            }
            else
                return false;
        }
    }
}
