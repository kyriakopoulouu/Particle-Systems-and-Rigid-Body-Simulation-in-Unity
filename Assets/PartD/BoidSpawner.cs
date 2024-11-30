using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boidPrefab;

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject boid = Instantiate(boidPrefab);
            boid.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        }
    }
}