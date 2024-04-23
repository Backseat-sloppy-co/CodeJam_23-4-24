using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinManager : MonoBehaviour
{
    private int penguinSize;

    [SerializeField] private GameObject penguinPrefab;
    private CapsuleCollider penguinCollider;
    private float SpawnRate;
    private Vector3 penguinSpawnPoint = new Vector3(12.7f, -0.42f, 0.28f);


    void Start()
    {
        penguinCollider = gameObject.GetComponent<CapsuleCollider>();


    }

    private void Update()
    {
        //spawn penguin at random interval between 1 and 6 seconds
        if (Time.time > SpawnRate)
        {
            SpawnRate = Time.time + Random.Range(1, 6);
            SpawnPenguin();

        }

    }

    void SpawnPenguin()
    {
        Instantiate(penguinPrefab, penguinSpawnPoint, Quaternion.Euler(0,0,0));
        Debug.Log("Penguin Found! Contact Authorities!!");

    }
}
