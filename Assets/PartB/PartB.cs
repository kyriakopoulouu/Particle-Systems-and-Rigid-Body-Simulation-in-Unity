using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]

public class PartB : MonoBehaviour
{
    public float radius;
    public Vector3 sphereCenter;
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    Vector3 particleVelocity;
    float force;
    Vector3 prevParticleVelocity;
    Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        var main = ps.main;
        main.maxParticles = 400;
        main.startLifetime = 40;
        main.startSpeed = 0;
        main.loop = false;
        radius = 20.0f;
        sphereCenter = new Vector3(0.0f, 0.0f, 0.0f);
        particleVelocity = new Vector3();

        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnParticles();

        int numParticles = ps.GetParticles(particles);
        for (int i = 0; i < numParticles; i++)
        {
            // Initialize particles position
            if (Time.time < 2)
            {
                particles[i].position = new Vector3(1,1,1);
            }
            // Collision and direction of velocity change according to vector pointing to the center of the sphere
            if ((particles[i].position - sphereCenter).sqrMagnitude > radius*radius )
            {
                prevParticleVelocity = particles[i].velocity;
                particles[i].position = sphereCenter + (particles[i].position - sphereCenter).normalized * radius ;
                prevParticleVelocity = prevParticleVelocity * 0.1f;
                particles[i].velocity = Vector3.Reflect(prevParticleVelocity, ((particles[i].position / radius) - sphereCenter));
            }
            else
            {
                particles[i].velocity = particles[i].velocity;
            }


            // Repulsion force
            for (int j = 0; j < numParticles; j++)
            {
                if (i == j || Time.time < 2)
                    continue;
                distance = particles[i].position - particles[j].position;
                //We let the force be applied even if the particles are far away from each other
                if (distance.magnitude < 0.01f)
                    force = 0;
                else
                    force = 50.0f / distance.magnitude + 0.01f;
                particleVelocity = particles[i].velocity + distance * force * Time.deltaTime;
                particles[i].velocity = particleVelocity;
            }
        }
        ps.SetParticles(particles, numParticles);
    }

    void SpawnParticles()
    {
            ps = GetComponent<ParticleSystem>();
            particles = new ParticleSystem.Particle[ps.main.maxParticles];
        
    }
}