using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuBehaviour : MonoBehaviour
{
    [Header("Create a scriptable object for each scene")]
    public List<GameElementScriptableObject> gameElements = new List<GameElementScriptableObject>();

    [Header("In-Scene references")]
    public Button playButton;
    public Button selectButton;
    public Button cancelButton;
    public Transform leftPivot;
    public Transform rightPivot;
    public Transform mainPivot;
    public Transform mainCanvas;
    public Transform selectCanvas;

    public GameObject gameElementPrefab;

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        selectButton.onClick.AddListener(Select);
        cancelButton.onClick.AddListener(Cancel);


        //setting up the buttonts gamelements
        foreach (var gameElement in gameElements)
        {
            var ge = Instantiate(gameElementPrefab, selectCanvas.GetChild(0));
            ge.GetComponentInChildren<TMP_Text>().text = gameElement.title;

            var image = ge.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
            image.sprite = gameElement.icon;
            
            ge.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadScene(gameElement.sceneName);
            });
        }
    }

    private void Play()
    {
        var randomScene = Random.Range(0, gameElements.Count);
        SceneManager.LoadScene(gameElements[randomScene].sceneName);
    }

    private void Select()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTowards(mainCanvas, leftPivot));
        StartCoroutine(MoveTowards(selectCanvas, mainPivot));
    }

    private void Cancel()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTowards(selectCanvas, rightPivot));
        StartCoroutine(MoveTowards(mainCanvas, mainPivot));
    }

    private IEnumerator MoveTowards(Transform target, Transform pivot)
    {
        while (Vector3.Distance(target.position, pivot.position) > 0.1f)
        {
            target.position = Vector3.Lerp(target.position, pivot.position, Time.deltaTime * 5);
            yield return null;
        }
    }
}
