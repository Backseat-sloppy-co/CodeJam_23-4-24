using UnityEngine;

public class PenguinManager : MonoBehaviour
{
    private int penguinSize;

    [SerializeField] private GameObject penguinPrefab;
    private CapsuleCollider penguinCollider;
    private float SpawnRate;
    private Vector3 penguinSpawnPoint = new Vector3(12.7f, -1.42f, 0.28f);

    void Start()
    {
        penguinCollider = gameObject.GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        // first spawn will always take 6 seconds
        if (Time.time < 6)
        {
            SpawnRate = 6;
        }

        //spawn penguin at random interval between 1 and 6 seconds
        if (Time.time > SpawnRate)
        {
            SpawnRate = Time.time + Random.Range(1, 6);
            SpawnPenguin();
        }
    }

    void SpawnPenguin()
    {
        GameObject newPenguin = Instantiate(penguinPrefab, penguinSpawnPoint, Quaternion.Euler(0, 0, 0));
        PenguinBehavior penguinBehavior = newPenguin.GetComponent<PenguinBehavior>();

        // Set the speed and size of the new penguin here
        penguinBehavior.penguinRotaionSpeed = Random.Range(50f,300f);
        penguinBehavior.penguinSpeed = Random.Range(3f,5f);

        float size = Random.Range(1f,4f);
        newPenguin.transform.localScale = new Vector3(size, size, 1);

        Debug.Log("Penguin Found! Contact Authorities!!");
    }
}
