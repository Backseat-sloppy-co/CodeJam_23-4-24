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
    public Transform mainCanvas;
    public Transform selectCanvas;
    public Transform mainPos;
    public Transform rightPos;
    public Transform leftPos;
    public GameObject gameElementPrefab;
    private float lerpSpeed = 5f;

    private void Start()
    {
        GameManager.instance.sceneNames.Clear();
        GameManager.instance.selectedGame = false;

        foreach (var gameElement in gameElements)
        {
            GameManager.instance.sceneNames.Add(gameElement.sceneName);
        }

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

            if(gameElement.icon != null)
            {
                image.sprite = gameElement.icon;
            }
            
            ge.GetComponent<Button>().onClick.AddListener(() =>
            {
                PlaySelected(gameElement.sceneName);
            });
        }
    }

    private void PlaySelected(string sceneName)
    {
        GameManager.instance.selectedGame = true;

        SceneManager.LoadScene(sceneName);
    }

    public void Play()
    {
        var randomScene = Random.Range(0, gameElements.Count);
        SceneManager.LoadScene(gameElements[randomScene].sceneName);
    }

    public void Select()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTowards(mainCanvas, leftPos));
        StartCoroutine(MoveTowards(selectCanvas, mainPos));
    }

    public void Cancel()
    {
        StopAllCoroutines();
        StartCoroutine(MoveTowards(selectCanvas, rightPos));
        StartCoroutine(MoveTowards(mainCanvas, mainPos));
    }

    private IEnumerator MoveTowards(Transform target, Transform pivot)
    {
        while (Vector3.Distance(target.position, pivot.position) > 0.1f)
        {
            target.position = Vector3.Lerp(target.position, pivot.position, Time.deltaTime * lerpSpeed);
            yield return null;
        }
    }
}
