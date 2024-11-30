using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Smoke : MonoBehaviour
{
    public ParticleSystem ps;
    private ParticleSystemRenderer psr;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var main = ps.main;
        main.startSpeed = 2;
        main.startSize = 4;
        
        var emission = ps.emission;
        emission.rateOverTime = 50;

        //var particleSize = psr.maxParticleSize;
        //particleSize = 3;

        var shape = ps.shape;
        shape.radius = 30;
        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.red, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.5f), new GradientAlphaKey(0.5f, 1.0f) });

        colorOverLifetime.color = grad;
    }
}
