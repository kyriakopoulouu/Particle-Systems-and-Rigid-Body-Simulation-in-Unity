using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartA : MonoBehaviour
{
    public GameObject particlePrefab;
    public List<Rigidbody> particleRigidbodies = new List<Rigidbody>();
    private Vector3 velocity;
    private Vector3 particleAcceleration;
    public Vector3 gravity = new Vector3(0, -9.81f, 0);
    public Vector3 spawnPoint = Vector3.zero; // Set the spawn point at (0,0,0)
    public int lifetime = 5; //destroy particles after some time to avoid memory deplition
    //public int maxParticles = 10;
    public int spawnRate = 50; //set the spawnrate, make it public for inspector testing
    public int maxVelocity = 50; // Set the maximum velocity in the Inspector
    private float timeSinceLastSpawn; //
    public float forceDragCoef = 150f;
    public float particleMass = 1f;


    void Start()
    {
        // Spawn particles using the ParticleSpawner function
        //ParticleSpawner();

    }

    void Update()
    {

        //spawn for each frame

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= 1f / spawnRate)
        {
            timeSinceLastSpawn = 0f;
            ParticleSpawner();
            
        }
        // Change the velocity of each particle
        

        foreach (Rigidbody rb in particleRigidbodies)
        {

            particleRigidbodies.RemoveAll(rb => rb = null); //remove dead refferences 


            if (rb != null)
            {
                if (Mathf.Abs(rb.transform.position.x ) > 5 )
                {
                    Vector3 newVelocity = new Vector3(-rb.velocity.x*0.7f, rb.velocity.y, rb.velocity.z);
                    rb.velocity = newVelocity;
                }
                if ( Mathf.Abs(rb.transform.position.y) > 5 )
                {
                    Vector3 newVelocity = new Vector3(rb.velocity.x, -rb.velocity.y * 0.7f, rb.velocity.z);
                    rb.velocity = newVelocity;
                }
                if (Mathf.Abs(rb.transform.position.z) > 5)
                {
                    Vector3 newVelocity = new Vector3(rb.velocity.x, rb.velocity.y, -rb.velocity.z * 0.7f);
                    rb.velocity = newVelocity;
                }
            }



        }

    }

    private void FixedUpdate()
    {

        // update the velocity and position of the object
        velocity += particleAcceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

/*    private void AddForce(Vector3 force)
    {
        particleAcceleration += force / particleMass;
    }*/

    void ParticleSpawner()
    {
        // Spawn particles
        GameObject particle = Instantiate(particlePrefab);
        Rigidbody rb = particle.GetComponent<Rigidbody>();

        
        //particleRigidbodies.Add(rb);


        // this fixes an error occured as the particle clones appeared empty 
        if (rb == null)
        {
            rb = particle.AddComponent<Rigidbody>(); 
        }

        //spawn position at (0,0,0)
        particle.transform.position = spawnPoint;

        //spawn with random velocities and set them to the particle's velocity 
        Vector3 velocity = Random.insideUnitSphere * maxVelocity;
        rb.velocity = velocity;


        // set the mass of the particle
        rb.mass=particleMass;

        //add gravity 
        //rb.AddForce(gravity);
        rb.velocity += gravity * Time.deltaTime;
        // add custom drag force

        Vector3 forceDrag = -forceDragCoef * rb.velocity;

        rb.AddForce(forceDrag);

        // Add particles to the list from which they will be called for collision check 
        particleRigidbodies.Add(rb);

        //destroy particle after some time to avoid framebdrops and clutering
        Destroy(particle, lifetime);

    }
}