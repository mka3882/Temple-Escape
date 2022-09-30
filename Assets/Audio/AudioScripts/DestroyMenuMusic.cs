using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMenuMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //this literally just nukes the main mennu object so it dosn't keep playing
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        foreach(GameObject g in objs)
        {
            Destroy(g);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
