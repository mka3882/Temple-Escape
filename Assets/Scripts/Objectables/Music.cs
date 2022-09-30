using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Music: Objectable
{
    void Awake()
    {
        gameObject.GetComponent<AudioSource>().Play();
    }
}

