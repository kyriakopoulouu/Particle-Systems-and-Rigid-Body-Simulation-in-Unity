using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartC : MonoBehaviour
{

    public GameObject[] prefabs1;
    public GameObject[] prefabs2;

    //private GameObject[] instance2;
    //private GameObject[] instance1;

    public float interval = 0.5f;
    private float timeSinceSpawn;
    public float spawnRangeZ = 2;
    public float spawnPosX = 0;
    public float startDelay = 0f;
    public float spawnInterval = 1f;
    public float seekForce = 100;
    private int prefabIndex;
    //Vector3 startVelocity;
    //Vector3 velocities;

    //rigidbody qualities
    public float mass = 1f;
    public float g = 9.81f;

    Vector3 velocity;
    void Start()
    {
        for (int i = 0; i < prefabs1.Length; i++)
        {
            if (prefabs1[i].GetComponent<Rigidbody>() == null)
            {
                prefabs1[i].AddComponent<Rigidbody>();
            }
        }
        for (int j = 0; j < prefabs2.Length; j++)
        {
            if (prefabs2[j].GetComponent<Rigidbody>() == null)
            {
                prefabs2[j].AddComponent<Rigidbody>();
            }
        }

        StartCoroutine(SpawnFirstParticles());
    }

    IEnumerator SpawnFirstParticles()
    {



        for (int i = 0; i < prefabs1.Length; i++)
        {

            //spawn first triplet 
            GameObject prefab1 = prefabs1[i];

            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2));

            GameObject instance1 = Instantiate(prefab1, randomSpawnPosition, Quaternion.identity);

            Vector3 randomVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));

            //instance1.AddComponent<Rigidbody>();
            instance1.GetComponent<Rigidbody>().velocity = randomVelocity;
            yield return new WaitForSeconds(interval);

            instance1.GetComponent<Rigidbody>().mass = mass;


        }
        for (int j = 0; j < prefabs2.Length; j++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2, 2), 1, Random.Range(-2, 2));
            //spawn second triplet
            GameObject prefab2 = prefabs2[j];
            GameObject instance2 = Instantiate(prefab2, randomSpawnPosition, Quaternion.identity);
            Vector3 randomVelocity = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            instance2.GetComponent<Rigidbody>().velocity = randomVelocity;
            //instance2.AddComponent<Rigidbody>();
            //instance2.GetComponent<Rigidbody>();
            instance2.GetComponent<Rigidbody>().mass = mass;
        }

        

    }


    void Update()
    {
        Seek();
    }

    void Seek()
    {
        for (int i = 0; i < prefabs1.Length; i++)
        {
            // Get the distance between the two GameObjects
            Vector3 distance = prefabs1[i].transform.position - prefabs2[i].transform.position;

            // Normalize the distance vector
            Vector3 normalizedDistance = distance.normalized;

            // Calculate the acceleration to apply to prefabs2[i] to make it seek prefabs1[i]
            Vector3 acceleration = normalizedDistance * this.seekForce / prefabs2[i].GetComponent<Rigidbody>().mass;

            // Calculate the velocity to apply to prefabs2[i] based on the acceleration
            Vector3 velocity = prefabs2[i].GetComponent<Rigidbody>().velocity + acceleration * Time.deltaTime;

            // Set the velocity of prefabs2[i]
            prefabs2[i].GetComponent<Rigidbody>().velocity = velocity;
        }
    }

}
    



