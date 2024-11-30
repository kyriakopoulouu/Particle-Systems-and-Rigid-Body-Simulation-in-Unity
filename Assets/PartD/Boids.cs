using UnityEngine;
using System.Collections;

public class Boids : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 accForce;
    public float mass;
    public float maxSpeed;
    public float maxForce;

    // Cohesion variables
    public float cohesionRadius;
    public float cohesionWeight;

    // Separation variables
    public float separationRadius;
    public float separationWeight;

    // Alignment variables
    public float alignmentRadius;
    public float alignmentWeight;

    void Start()
    {
        velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        acceleration = new Vector3(0, 0, 0);
        mass = 5;
        maxSpeed = 50;
        maxForce = 2f;

       
        cohesionRadius = 10;
        cohesionWeight = 3;

        
        separationRadius = 10;
        separationWeight = 5;

        
        alignmentRadius = 10;
        alignmentWeight = 5;
    }

    void Update()
    {
        Vector3 cohesion = Cohesion() * cohesionWeight;
        Vector3 separation = Separation() * separationWeight;
        Vector3 alignment = Alignment() * alignmentWeight;
        ApplyForce(cohesion + separation + alignment);
        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
        acceleration = Vector3.zero;
    }

    void ApplyForce(Vector3 f)
    {
        accForce = f / mass;
        acceleration += accForce;
    }

    Vector3 Cohesion()
    {
        Vector3 sum = new Vector3(0, 0, 0);
        int count = 0;
        foreach (GameObject other in GameObject.FindGameObjectsWithTag("Boid"))
        {
            float d = Vector3.Distance(transform.position, other.transform.position);
            if (d > 0 && d < cohesionRadius)
            {
                sum += other.transform.position;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return Steer(sum);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    Vector3 Separation()
    {
        Vector3 sum = new Vector3(0, 0, 0);
        int count = 0;
        foreach (GameObject other in GameObject.FindGameObjectsWithTag("Boid"))
        {
            float d = Vector3.Distance(transform.position, other.transform.position);
            if (d > 0 && d < separationRadius)
            {
                Vector3 diff = transform.position - other.transform.position;
                diff.Normalize();
                diff /= d;
                sum += diff;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return sum;
        }
        else
        {
            return new Vector3(0, 0, 0);
        }


    }
    Vector3 Alignment()
    {
        Vector3 sum = new Vector3(0, 0, 0);
        int count = 0;
        foreach (GameObject other in GameObject.FindGameObjectsWithTag("Boid"))
        {
            float d = Vector3.Distance(transform.position, other.transform.position);
            if (d > 0 && d < alignmentRadius)
            {
                sum += other.GetComponent<Boids>().velocity;
                count++;
            }
        }
        if (count > 0)
        {
            sum /= count;
            return Steer(sum);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }

    Vector3 Steer(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired = desired.normalized * maxSpeed;
        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }
}