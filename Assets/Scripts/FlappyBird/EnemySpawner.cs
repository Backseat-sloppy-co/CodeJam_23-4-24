using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;

    private void Update()
    {
        if(Time.time % spawnRate == 0)
        {
            Instantiate(enemyPrefab, new Vector3(10, -4.25f, 0), Quaternion.identity);
        }
    }
}
