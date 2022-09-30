using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : Objectable
{
    public bool reflecting = false;
    Vector3 laserPosition = Vector3.forward;
    Vector3 laserRotation;
    public GameObject reflector;
    public GameObject laser;
    public GameObject laserObj;
    public bool beingHitByOtherEmitterBeam = false;
    public int laserCount = 0;
    public GameObject[] ChildLasers = new GameObject[0];

    // SET ROTATIONS OF TOPBLOCK Object
    [SerializeField] float e_XROT;
    [SerializeField] float e_YROT;
    [SerializeField] float e_XZROT;
    [SerializeField] float LookAt;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void ResetReflector()
    {
        transform.Rotate(0, 0, 0);
    }

    public void OnMouseDown()
    {
        if (reflecting)
        {
            reflector.transform.Rotate(e_XROT, e_YROT, e_XZROT);

            app.puzzleController.checkTargets = false;
            app.puzzleController.reflectorController.reflector = this.gameObject.GetComponent<Reflector>();
            foreach (var box in app.puzzleController.boxController.boxesInScene)
            {
                box.CheckBox(this.gameObject);
            }
            app.puzzleController.checkTargets = true;

            ResetReflect();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Colliding");
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Not Colliding");
        if (other.GetComponent<Laser>())
        {
            reflecting = false;

            // destroy need laserObj
            Destroy(laserObj);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Colliding");
    }

    public void ResetReflect()
    {
        // Create Line Renderer Properties

        // reset all lines with new positions
        LineRenderer laserLine = laserObj.GetComponent<LineRenderer>();
        //laserLine.SetPosition(0, laserObj.transform.position);
        //laserLine.SetPosition(1, gameObject.transform.forward * 1000);
        laserObj.GetComponent<Laser>().CheckIntersection();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Reflector>())
        {
            reflecting = false;
            Destroy(laserObj);
        }
    }

    // Kills FPS
    // Creates Too Many Lasers
    public void Reflect(Vector3 hitpoint)
    {
        if (reflecting == true)
        {
            reflecting = false;
            Destroy(laserObj);
        }

        reflecting = true;
        laserObj = Instantiate(laser);
        laserObj.transform.parent = reflector.transform;
        //laserObj.GetComponent<Laser>().sourceReflector = gameObject;
        //laserObj.transform.position = new Vector3(hitpoint.x,hitpoint.y + 2,hitpoint.z);
        laserObj.transform.position = reflector.transform.position;
        laserObj.transform.Rotate(reflector.transform.rotation.eulerAngles);

        switch (LookAt)
        {
            case 45:
                laserObj.transform.LookAt(new Vector3(laserObj.transform.position.x + 1, laserObj.transform.position.y, laserObj.transform.position.z + 1));
                break;
            case 46:
                laserObj.transform.LookAt(new Vector3(laserObj.transform.position.x + 1, laserObj.transform.position.y + 1, laserObj.transform.position.z + 1));
                break;
            case 90:
                laserObj.transform.LookAt(new Vector3(laserObj.transform.position.x, laserObj.transform.position.y, laserObj.transform.position.z + 1));
                break;
            case 91:
                laserObj.transform.LookAt(new Vector3(laserObj.transform.position.x, laserObj.transform.position.y, laserObj.transform.position.z + 1));
                break;
            default:
                break;
        }

        // Create Line Renderer Properties
        LineRenderer laserShooting = laserObj.GetComponent<LineRenderer>();
        laserShooting.SetPosition(0, laserObj.transform.position);
        laserShooting.SetPosition(1, gameObject.transform.forward * 1000);
        laserObj.GetComponent<Laser>().CheckIntersection();
    }

    public void EndCollision()
    {
        //Debug.Log("end collision");
        reflecting = false;
        beingHitByOtherEmitterBeam = false;
        if (laserObj != null)
        {
            if (laserObj.GetComponent<Laser>().reflector != null)
            {
                laserObj.GetComponent<Laser>().reflector.EndCollision();
                laserObj.GetComponent<Laser>().reflector = null;
            }
            if (laserObj.GetComponent<Laser>().target != null)
            {
                laserObj.GetComponent<Laser>().target.EndCollision();
                laserObj.GetComponent<Laser>().target = null;
            }
            Destroy(laserObj);
        }
    }
}
