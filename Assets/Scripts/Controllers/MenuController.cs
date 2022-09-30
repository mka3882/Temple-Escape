using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool start;
    public bool levelselect;

    // Start is called before the first frame update
    void Start()
    {
   //     Camera.main.orthographicSize = (20.0f / Screen.width * Screen.height / 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter()
    {
        
    }

    void OnMouseDown()
    {
        if (start)
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (levelselect)
        {
            SceneManager.LoadScene("LevelSelect");
        }
        else
        {
            UnityEngine.Application.Quit();
        }
    }
}
