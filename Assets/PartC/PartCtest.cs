using UnityEngine;
using System.Collections;

public class PartCtest : MonoBehaviour
{
    
    public GameObject particlePrefab;

    // max velocity of particles and acceleration
    public float maxVelocity = 10f;
    public float maxAcceleration = 5f;

    // spawn interval
    public float spawnInterval = 0.2f;


    // color list of 3 colors 
    public Color[] colors = { Color.blue, Color.green, Color.red };

    // variable that determines when to spawn
    private float timeUntilNextSpawn;

    // particles alive counter
    private int particleCount = 0;

    void Start()
    {
        // Initialize the time until next spawn
        timeUntilNextSpawn = spawnInterval;
    }

    void Update()
    {
        // declare time of next spawn
        timeUntilNextSpawn -= Time.deltaTime;

        // we want to spawn 3 particles with a time interval step until we reach 3 particles
        if (timeUntilNextSpawn <= 0f && particleCount < 3)
        {
            SpawnParticle();     
            particleCount++; //every spawn adds 1 to the count
            timeUntilNextSpawn = spawnInterval;//after each spawn reset the time to spawn interval
        }
    }

    void SpawnParticle() //function that handles the spawning of the particles 
    {
        
        GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);//instantiate the particles at fixed position

        Vector3 velocity = Random.insideUnitSphere * maxVelocity;

        particle.GetComponent<Renderer>().material.color = colors[particleCount];// assign unique color to each particle from the list 

        
        Rigidbody rb = particle.AddComponent<Rigidbody>(); //  particle  rigidbody component and set velocity
        rb.velocity = velocity;

        
        StartCoroutine(SpawnPairParticle(particle)); // After a delay, spawn a pair-particle for this particle
    }

    IEnumerator SpawnPairParticle(GameObject originalParticle) // enumerator that handles the delayed spawn of 2nd triplet
    {
        yield return new WaitForSeconds(4f);

        // Spawn a pair-particle for the original particle
        GameObject pairParticle = Instantiate(particlePrefab, transform.position, Quaternion.identity);

        // Get & Set the color of the original particle to the newlly spawned particles

        Color originalColor = originalParticle.GetComponent<Renderer>().material.color; 
        pairParticle.GetComponent<Renderer>().material.color = originalColor;

        Rigidbody rb = pairParticle.AddComponent<Rigidbody>();
        
        // Seek the original particle
        while (true)
        {
            
            Vector3 direction = originalParticle.transform.position - pairParticle.transform.position; //direction to the og particle

            Vector3 normalizedDirection = direction.normalized; //normalization

            Vector3 acceleration = normalizedDirection * maxAcceleration; // seeking acceleration

            
            pairParticle.GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime; // set the acceleration to the pair-particle

            
            yield return null; //wait for next frame
        }
    }
}

