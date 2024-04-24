using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cooking : MonoBehaviour
{

    //Add a gameobject to the script of the steak
    public GameObject steak;
    public GameObject pan;
    public Material materialCooked;
    

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Cook());
    }

    // Update is called once per frame
    void Update()
    {
        ControlPan();
        RotatePan();
        MovePan();
    }

    public IEnumerator Cook()
    {
        steak.GetComponent<Renderer>().material.color = Color.red;
        yield return null;
    }

    //When the steak is colliding with the pan, the steak will change material to a different material
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Food"))
        {
            Debug.Log("Collided with pan");
            //steak.GetComponent<Renderer>().material.color = Color.red;
            //steak.GetComponent<Renderer>().material = materialCooked;
            StartCoroutine(CookOverTime(collision.gameObject, 5.0f));
        }
    }
    public IEnumerator CookOverTime(GameObject steak, float duration)
    {
        float elapsedTime = 0;
        Material originalMaterial = steak.GetComponent<Renderer>().material;
        Material targetMaterial = materialCooked; // Assuming materialCooked is the final cooked material

        while (elapsedTime < duration)
        {
            float lerpValue = elapsedTime / duration;
            Material newMaterial = new Material(originalMaterial);
            newMaterial.Lerp(originalMaterial, targetMaterial, lerpValue);
            steak.GetComponent<Renderer>().material = newMaterial;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        steak.GetComponent<Renderer>().material = targetMaterial;
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
