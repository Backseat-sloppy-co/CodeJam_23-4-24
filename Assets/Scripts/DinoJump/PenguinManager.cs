using UnityEngine;

public class PenguinManager : MonoBehaviour
{
    private float penguinSize;

    [SerializeField] private GameObject penguinPrefab;
    private CapsuleCollider penguinCollider;
    private float SpawnRate;
    private Vector3 penguinSpawnPofloat = new Vector3(12.7f, -1.42f, 0.28f);

    private float spawnRMax = 6;
    private float spawnRMin = 1;
    private float rotationMax = 300;
    private float rotationMin = 50;
    private float speedMax = 3;
    private float speedMin = 5;
    private float sizeMax = 1;
    private float sizeMin = 4;



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

        //spawn penguin at random floaterval between 1 and 6 seconds
        if (Time.time > SpawnRate)
        {
            SpawnRate = Time.time + Random.Range(spawnRMin, spawnRMax);
            SpawnPenguin();
        }
    }

    void SpawnPenguin()
    {
        GameObject newPenguin = Instantiate(penguinPrefab, penguinSpawnPofloat, Quaternion.Euler(0, 0, 0));
        PenguinBehavior penguinBehavior = newPenguin.GetComponent<PenguinBehavior>();

        // Set the speed and size of the new penguin here
        penguinBehavior.penguinRotaionSpeed = Random.Range(rotationMin, rotationMax);
        penguinBehavior.penguinSpeed = Random.Range(speedMin, speedMax);

        float size = Random.Range(speedMin, speedMax);
        newPenguin.transform.localScale = new Vector3(size, size, 1);

        Debug.Log("Penguin Found! Contact Authorities!!");
    }
}
// this code was written with the help of copilot.