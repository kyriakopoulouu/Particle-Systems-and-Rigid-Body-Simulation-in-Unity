using UnityEngine;
using System.Collections;

public class Gravity : MonoBehaviour
{
    public float gravity = -9.81f; 
    public float mass = 1f; 

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb)
        {
            Vector3 force = mass * gravity * Vector3.up;//f=m*a
            rb.velocity += force * Time.deltaTime; // update velocity whith gravity 
        }
    }
}