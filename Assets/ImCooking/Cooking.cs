using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{

    //Add a gameobject to the script of the steak
    public GameObject steak;
    public GameObject pan;




    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Cook());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Cook()
    {
        steak.GetComponent<Renderer>().material.color = Color.red;
        yield return null;
    }

}
