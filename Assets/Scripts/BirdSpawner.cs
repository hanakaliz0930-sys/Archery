using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab;

    public Vector3 minSpawnPosition;
    public Vector3 maxSpawnPosition;

    public int totalBirds = 50;
    public float totalTime = 55f; 

    private float nextSpawnTime;
    private float delay;
    private int birdsSpawned = 0;

    public Material birdMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        delay = totalTime / totalBirds;
        nextSpawnTime = Time.time;
    }
    void Update()
    {
        if(birdsSpawned >= totalBirds) return;
        if (Time.time >= nextSpawnTime)
        {
            SpawnBird();
            nextSpawnTime = Time.time + delay;
            birdsSpawned++;
        }
    }
    void SpawnBird()
    {
        float x = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
        float y = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
        float z = Random.Range(minSpawnPosition.z, maxSpawnPosition.z);

        Vector3 spawnPosition = new Vector3(x, y, z);
        GameObject bird = Instantiate(birdPrefab, spawnPosition, birdPrefab.transform.rotation);
        bird.transform.localScale = Vector3.one * 10;
        Renderer birdRenderer = bird.GetComponentInChildren<Renderer>();
        birdRenderer.material = birdMaterial;
    }
}
