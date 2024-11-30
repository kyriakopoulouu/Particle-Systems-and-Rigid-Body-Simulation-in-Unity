using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekScript : MonoBehaviour
{
    public Color[] colors = { Color.blue, Color.red, Color.green };

    // The prefab for the particle
    public GameObject particlePrefab;

    // The force of gravity
    public float gravity = 9.81f;

    // The maximum initial velocity for the particles
    public float maxVelocity = 5f;

    // The maximum time interval between particle spawns
    public float maxInterval = 2f;

    // The maximum position offset for the particles
    public float maxPositionOffset = 5f;

    // The maximum acceleration for the particles
    public float maxAcceleration = 5f;

    // The time between particle spawns
    private float timeInterval;

    // The time until the next particle spawn
    private float timeUntilNextSpawn;

    // The number of particles that have been created
    private int particleCount = 0;

    void Start()
    {
        // Initialize the time interval and time until next spawn
        timeInterval = Random.Range(0f, maxInterval);
        timeUntilNextSpawn = timeInterval;
    }

    void Update()
    {
        // Decrement the time until the next particle spawn
        timeUntilNextSpawn -= Time.deltaTime;

        // If it's time to spawn a new particle and we haven't reached the maximum particle count
        if (timeUntilNextSpawn <= 0f && particleCount < 3)
        {
            // Spawn a new particle
            SpawnParticle();


            // Increment the particle count
            particleCount++;

            // Reset the time interval and time until next spawn
            timeInterval = Random.Range(0f, maxInterval);
            timeUntilNextSpawn = timeInterval;
        }
    }

    void SpawnParticle()
    {
        // Instantiate the particle at a random position
        Vector3 position = transform.position + Random.insideUnitSphere * maxPositionOffset;

        //keeps the particles within specified space
        position.x = Mathf.Clamp(position.x, -5f, 5f);
        position.y = Mathf.Clamp(position.y, -5f, 5f);
        position.z = Mathf.Clamp(position.z, -5f, 5f);

        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
        particle.GetComponent<Renderer>().material.color = colors[particleCount];

        // Give the particle a random initial velocity
        Vector3 velocity = Random.insideUnitSphere * maxVelocity;
        particle.GetComponent<Rigidbody>().velocity = velocity;

        // After 1 second, spawn a pair-particle for this particle
        StartCoroutine(SpawnPairParticle(particle));
    }

   IEnumerator SpawnPairParticle(GameObject originalParticle)
{
    // Wait for 5 second
    yield return new WaitForSeconds(5f);

    // Spawn a pair-particle for the original particle
    Vector3 position = transform.position + Random.insideUnitSphere * maxPositionOffset;
    GameObject pairParticle = Instantiate(particlePrefab, position, Quaternion.identity);

    // Get the color of the original particle
    Color originalColor = originalParticle.GetComponent<Renderer>().material.color;

    // Set the color of the pair particle to be the same as the original particle
    pairParticle.GetComponent<Renderer>().material.color = originalColor;

    // Seek the original particle
    while (true)
    {
        // Calculate the direction to the original particle
        Vector3 direction = originalParticle.transform.position - pairParticle.transform.position;

        // Normalize the direction
        Vector3 normalizedDirection = direction.normalized;

        // Calculate the acceleration to apply
        Vector3 acceleration = normalizedDirection * maxAcceleration;

        // Apply the acceleration to the pair-parti cle
        pairParticle.GetComponent<Rigidbody>().velocity += acceleration * Time.deltaTime;

        // Wait for the next frame
        yield return null;
    }
}
        
}