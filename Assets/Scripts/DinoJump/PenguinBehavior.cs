using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBehavior : MonoBehaviour
{
    public float penguinSpeed;
    public float penguinRotaionSpeed;
    public float penguinSize;

    void Update()
    {
        transform.position += Vector3.left * penguinSpeed * Time.deltaTime;

        if (transform.position.x < -15)
        {
            Destroy(gameObject);
            Debug.Log("Crisis Averted!!!");
        }

        transform.Rotate(Vector3.up * penguinRotaionSpeed * Time.deltaTime);
    }
}
