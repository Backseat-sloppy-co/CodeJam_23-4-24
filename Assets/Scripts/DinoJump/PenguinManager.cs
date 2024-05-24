using UnityEngine;

public class PenguinManager : MonoBehaviour
{
    private int penguinSize;

    [SerializeField] private GameObject penguinPrefab;
    private CapsuleCollider penguinCollider;
    private int SpawnRate;
    private Vector3 penguinSpawnPoint = new Vector3(12.7f, -1.42f, 0.28f);

    private int spawnRMax = 6;
    private int spawnRMin = 1;
    private int rotationMax = 300;
    private int rotationMin = 50;
    private int speedMax = 3;
    private int speedMin = 5;
    private int sizeMax = 1;
    private int sizeMin = 4;



    void Start()
    {
        penguinCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        // first spawn will always take 6 seconds
        if (Time.time < spawnRMax)
        {
            SpawnRate = spawnRMax;
        }

        //spawn penguin at random interval between 1 and 6 seconds
        if (Time.time > SpawnRate)
        {
            SpawnRate = Time.time + Random.Range(spawnRMin, spawnRMax);
            SpawnPenguin();
        }
    }

    void SpawnPenguin()
    {
        GameObject newPenguin = Instantiate(penguinPrefab, penguinSpawnPoint, Quaternion.Euler(0, 0, 0));
        PenguinBehavior penguinBehavior = newPenguin.GetComponent<PenguinBehavior>();

        // Set the speed and size of the new penguin here
        penguinBehavior.penguinRotaionSpeed = Random.Range(rotationMin, rotationMax);
        penguinBehavior.penguinSpeed = Random.Range(speedMin, speedMax);

        int size = Random.Range(speedMin, speedMax);
        newPenguin.transform.localScale = new Vector3(size, size, 1);

        Debug.Log("Penguin Found! Contact Authorities!!");
    }
}
// this code was written by me with the help of copilot.