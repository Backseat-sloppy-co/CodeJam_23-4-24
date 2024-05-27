using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBehavior : MonoBehaviour
{
    public float penguinSpeed;
    public float penguinRotaionSpeed;
    public float penguinSize;

    private float penguinPosition = -15;

    void Update()
    {
        transform.position += Vector3.left * penguinSpeed * Time.deltaTime;

        if (transform.position.x < penguinPosition)
        {
            Destroy(gameObject);
            Debug.Log("Crisis Averted!!!");
        }

        transform.Rotate(Vector3.up * penguinRotaionSpeed * Time.deltaTime);
    }
}
// this code was written with the help of copilot.