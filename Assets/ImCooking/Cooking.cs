using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            FindObjectOfType<AudioManager>().Play("Sizzling");
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
            FindObjectOfType<AudioManager>().Stop("Sizzling");
        }
    }

        private IEnumerator CookingProcess(Food food)
    {

        // Cook the food
        yield return StartCoroutine(CookOverTime(food.gameObject, food.cookingTime, food.materialCooked));
        food.cookingTime -= Time.deltaTime;
        Debug.Log("added score");
        FindObjectOfType<AudioManager>().Play("Succes");
        StartCoroutine(PlayConfetti());
        scoreManager.AddScore(1); // Add points when the food is cooked

        // Burn the food
        yield return StartCoroutine(CookOverTime(food.gameObject, food.burningTime, food.materialBurnt));
        food.burningTime -= Time.deltaTime;
        FindObjectOfType<AudioManager>().Play("Fail");
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
        yield return new WaitForSeconds(2);
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





















    //Move the pan using gyro
    private void MovePan()
    {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        pan.transform.Translate(dir * 10.0f);
    }

    //Rotate the pan using gyro
    private void RotatePan()
    {
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.y;
        dir.z = Input.acceleration.x;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        dir *= Time.deltaTime;
        pan.transform.Rotate(dir * 10.0f);
    }
    //Control the gyro of the pan using the keyboard
    private void ControlPan()
    {
        if (Input.GetKey(KeyCode.W))
        {
            pan.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            pan.transform.Translate(Vector3.back * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            pan.transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pan.transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
    

}
