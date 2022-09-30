using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Objectable
{
    Vector3 InitTargetToVector;
    Vector3 vec_Pos;
    int lineLength = 2;
    public LineRenderer line;
    public Reflector reflector;
    public Target target;

    public LineRenderer Line
    {
        set
        {
            line = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (app.puzzleController.checkTargets)
        {
            RunLineRender();
        }
    }

    void RunLineRender()
    {
        if (line == null)
        {
            line = gameObject.GetComponent<LineRenderer>();
        }

        if (line != null)
        {
            line.SetPosition(0, transform.position);
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (hit.collider)
                {
                    line.SetPosition(1, hit.point);
                }
            }
            else
            {
                line.SetPosition(1, transform.forward * 1000);
            }
        }
    }

    // Check Intersection
    // Checks if line renderer is colliding with another collider
    // runs off Emily's Update
    RaycastHit hit;
    public void CheckIntersection()
    {
        bool reflecting = false;
        bool targeting = false;
        if (line == null)
        {
            line = gameObject.GetComponent<LineRenderer>();
        }
        // Second Check but redundant
        if (line == null) return;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider)
            {
                switch (hit.collider.gameObject.tag)
                {
                    case "reflector":
                        // If LineRenderer Collides with Reflector
                        if (!hit.collider.gameObject.GetComponent<Reflector>().reflecting || line != hit.collider.gameObject.GetComponent<Reflector>().laserObj.GetComponent<Laser>()
                            && hit.collider.gameObject.GetComponent<Reflector>().laserObj == null)
                        {
                            hit.collider.gameObject.GetComponent<Reflector>().Reflect(hit.point);
                            reflecting = true;
                            if(reflector != null && reflector != hit.collider.gameObject.GetComponent<Reflector>())
                            {
                                reflector.EndCollision();
                            }
                            reflector = hit.collider.gameObject.GetComponent<Reflector>();
                        }
                        break;
                    case "target":
                        if (!hit.collider.gameObject.GetComponent<Target>().reached && app.puzzleController.checkTargets)
                        {
                            AudioController.PlayOneInstanceAudio(0);
                            hit.collider.gameObject.GetComponent<Target>().reached = true;
                            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
                            targeting = true;
                            if (target != null && target != hit.collider.gameObject.GetComponent<Target>())
                            {
                                target.EndCollision();
                            }
                            target = hit.collider.gameObject.GetComponent<Target>();
                        }

                        if (target != null)
                        {
                            target.PlayHitSystem();

                            bool allcheck = true;
                            foreach (var target in app.puzzleController.targetController.targetsInScene)
                            {
                                if (target.Reached == false)
                                {
                                    allcheck = false;
                                }
                            }

                            if (allcheck)
                            {
                                foreach (var target in app.puzzleController.targetController.targetsInScene)
                                {
                                    target.PlayCompletedSystem();
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        // Bool Checks if Not True
        if (!reflecting)
        {
            if(reflector != null && reflector.reflecting)
            {
                reflector.EndCollision();
                reflector = null;
            }
        }
        if (!targeting)
        {
            if (target != null && target.reached)
            {
                target.EndCollision();
                target = null;
            }
        }
    }
}