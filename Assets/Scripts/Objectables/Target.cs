using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// End Console/Target/Collider in a level
// Can have multiple in the level
public class Target : Objectable
{
    Vector3 position;
    public bool reached;
    public List<ParticleSystem> particleSystems;

    public bool Reached
    {
        get
        {
            return reached;
        }
    }

    private void Start()
    {
        
    }

    public void EndCollision()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.clear;
        reached = false;
    }

    public void PlayHitSystem()
    {
        Debug.Log(particleSystems[1].name);
        particleSystems[1].Play();
    }

    public void PlayCompletedSystem()
    {
        particleSystems[0].Play();
    }
}
