using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
    [Header("calibration")]
    [SerializeField] private GameObject calPanel;
    [SerializeField] private GameObject point;

    private GameBoardBehaviour gameBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        gameBehaviour = GetComponent<GameBoardBehaviour>();

        calPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Calibrating();
    }
    private void Calibrating()
    {
        if (InTarget())
        {
            point.GetComponent<Image>().color = Color.green;
            calPanel.transform.GetChild(1).GetComponent<Image>().color = Color.green;

            StartCoroutine(CalCountDown());
        }
        else
        {
            point.GetComponent<Image>().color = Color.red;
            calPanel.transform.GetChild(1).GetComponent<Image>().color = Color.red;

            StopAllCoroutines();
        }
    }
    IEnumerator CalCountDown()
    {
        yield return new WaitForSeconds(3);
        GameStart();

    }

    private void GameStart()
    {
        calPanel.SetActive(false);
        StopCoroutine(CalCountDown());
    }

    private bool InTarget()
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
            x = Input.gyro.attitude.eulerAngles.y;
            y = Input.gyro.attitude.eulerAngles.x;
            point.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);

            if (x > 340 && x < 20 && y > 340 && y < 20)
            {
                return true;
            }
            else
                return false;
        }

        

    }

}
