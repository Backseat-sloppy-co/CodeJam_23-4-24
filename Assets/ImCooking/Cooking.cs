using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//The following script has been made with assistance from GitHub Copilot
public class Cooking : MonoBehaviour
{
   
    public GameObject pan;
    public Material materialCooked;
    public Material materialBurnt;
    public bool isCooking = false; // Whether the food is in contact with the pan
    public ParticleSystem confetti; 

    public ScoreManager scoreManager; // Reference to the ScoreManager



    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        confetti.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Food food = collision.gameObject.GetComponent<Food>();
        if (food != null)
        {
            Debug.Log("Collided with " + collision.gameObject.name);
            isCooking = true;
            StartCoroutine(CookingProcess(food));
            AudioManager.instance.Play("Sizzling");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Stop the cooking process when the food is no longer in contact with the pan
        Food food = collision.gameObject.GetComponent<Food>();
        if (food != null)
        {
            isCooking = false;
            StopAllCoroutines();
            AudioManager.instance.Stop("Sizzling");
        }
    }

        private IEnumerator CookingProcess(Food food)
    {

        // Cook the food
        yield return StartCoroutine(CookOverTime(food.gameObject, food.cookingTime, food.materialCooked));
        food.cookingTime -= Time.deltaTime;
        Debug.Log("added score");
        AudioManager.instance.Play("Succes");
        StartCoroutine(PlayConfetti());
        scoreManager.AddScore(1); // Add points when the food is cooked

        // Burn the food
        yield return StartCoroutine(CookOverTime(food.gameObject, food.burningTime, food.materialBurnt));
        food.burningTime -= Time.deltaTime;
        AudioManager.instance.Play("Fail");
        scoreManager.SubtractScore(1); // Subtract points when the food is burnt

        //while (isCooking && food.cookingTime > 0)
        //{
        //    yield return StartCoroutine(CookOverTime(food.gameObject, food.cookingTime, food.materialCooked)); // Cook 
        //    food.cookingTime -= Time.deltaTime;
        //}
        //while (isCooking && food.burningTime > 0)
        //{
        //    yield return StartCoroutine(CookOverTime(food.gameObject, food.burningTime, food.materialBurnt)); // Burn 
        //    food.burningTime -= Time.deltaTime;
        //}

    }


    public IEnumerator PlayConfetti()
    {
        confetti.Play();
        yield return new WaitForSeconds(1);
        confetti.Stop();
    }   


    public IEnumerator CookOverTime(GameObject foodObject, float duration, Material targetMaterial)
    {
        float elapsedTime = 0;
        Material originalMaterial = foodObject.GetComponent<Renderer>().material;

        while (elapsedTime < duration)
        {
            if (!isCooking)
            {
                yield return null;
                continue;
            }


            float lerpValue = elapsedTime / duration;
            Material newMaterial = new Material(originalMaterial);
            newMaterial.Lerp(originalMaterial, targetMaterial, lerpValue);
            foodObject.GetComponent<Renderer>().material = newMaterial;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (isCooking)
        {
            foodObject.GetComponent<Renderer>().material = targetMaterial;
        }
            
    }

}
