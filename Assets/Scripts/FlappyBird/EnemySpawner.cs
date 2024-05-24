using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float defSpawnRate = 2f;//needs to be larger than 1
    private float spawnRate;
    private float negativeSpawnRate = -1f;
    public float timer;

    private float outOfBounds = -10f;

    public float enemySpeed = 5f;

    private List<GameObject> enemies = new List<GameObject>();

    public Transform upperSpawn;
    public Transform lowerSpawn;

    private Vector3 upperOffset = new Vector3(0, 90, 180);
    private Vector3 lowerOffset = new Vector3(0, 90, 0);

    [Range(1, 8)]
    public int barrelCountMin = 1;
    [Range(1, 8)]
    public int barrelCountMax = 5;

    private int lowerInRow = 0;
    private  int upperInRow = 0;
    private int lowerInRowMax = 2;
    private int upperInRowMax = 2;

    private void Start()
    {
        spawnRate = defSpawnRate;
    }

    private void SpawnBarrel(Vector3 pos, bool isUpper)
    {
        GameObject enemy = Instantiate(new GameObject("BarrelHolder"), pos, Quaternion.identity);

        var randomBarrelCount = Random.Range(barrelCountMin, barrelCountMax);
        for (int i = 0; i < randomBarrelCount; i++)
        {
            if (isUpper)
            {
                Instantiate(enemyPrefab, new Vector3(enemy.transform.position.x, enemy.transform.position.y - i, enemy.transform.position.z + i / 2f), Quaternion.Euler(upperOffset), enemy.transform);
            }
            else
            {
                Instantiate(enemyPrefab, new Vector3(enemy.transform.position.x, enemy.transform.position.y + i, enemy.transform.position.z + i / 2f), Quaternion.Euler(lowerOffset), enemy.transform);
            }
            
        }

        enemies.Add(enemy);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawnRate)
        {
            var rand = Random.value;
            if(upperInRow >= upperInRowMax)
            {
                SpawnBarrel(lowerSpawn.position, false);
                lowerInRow++;
                upperInRow = 0;
            }
            else if(lowerInRow >= lowerInRowMax)
            {
                SpawnBarrel(upperSpawn.position, true);
                upperInRow++;
                lowerInRow = 0;
            }
            else if(rand < 0.5f)
            {
                SpawnBarrel(lowerSpawn.position, false);
                lowerInRow++;
                upperInRow = 0;
            }
            else if(rand >= 0.5f)
            {
                SpawnBarrel(upperSpawn.position, true);
                upperInRow++;
                lowerInRow = 0;
            }

            spawnRate = Random.Range(negativeSpawnRate + defSpawnRate, defSpawnRate);
            timer = 0;
        }

        foreach(var enemy in enemies)
        {
            if(enemy.transform.position.x < outOfBounds)
            {
                Destroy(enemy);
                enemies.Remove(enemy);
                break;
            }

            enemy.transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);
        }
    }
}
