using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinBehavior : MonoBehaviour
{
    [SerializeField] private float penguinSpeed = 5f;
    [SerializeField] private float penguinRotaionSpeed = 5f;

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
