using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector] public List<string> sceneNames = new List<string>();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator NextRandomScene()
    {
        yield return 0;

        sceneNames.Remove(SceneManager.GetActiveScene().name);

        if (sceneNames.Count == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            var randomScene = Random.Range(0, sceneNames.Count);
            SceneManager.LoadScene(sceneNames[randomScene]);
        }
    }

    public IEnumerator NextRandomScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        sceneNames.Remove(SceneManager.GetActiveScene().name);

        if (sceneNames.Count == 0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            var randomScene = Random.Range(0, sceneNames.Count);
            SceneManager.LoadScene(sceneNames[randomScene]);
        }
    }


    public IEnumerator GoToMainMenu(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator GoToMainMenu()
    {
        yield return 0;

        SceneManager.LoadScene("MainMenu");
    }
}
