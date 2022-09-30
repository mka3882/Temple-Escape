using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : Objectable
{
    bool on = false;
    int numOfLasers = 2;                                            // Set Number of Lasers
    int lengthOfLaser = 1000;                                       // How far the laser will cast out
    int[] emitterMatrix = new int[10];                                            // Matrix to determine where the line renderer is created
    public string matrixDictionary = "simple";                                            // Matrix to determine where the line renderer is created
    public string emitterPattern = "one";                                            // Matrix to determine where the line renderer is created

    [SerializeField] GameObject[] lineNodes;                        // nodes waiting for a line
    LineRenderer[] lines = new LineRenderer[6];                     // array for lines that are children of this gameobject
    List<Laser> childLines = new List<Laser>();                     // nodes waiting for a line

    // SET ROTATIONS FOR BALL
    [SerializeField] float e_XROT;
    [SerializeField] float e_YROT;
    [SerializeField] float e_XZROT;

    // Line Renderers
    public GameObject lineParent;
    LineRenderer line;
    LineRenderer whiteLaser;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Reset Emitter Rotation
    void ResetRotation()
    {
        transform.Rotate(0, 0, 0);
    }

    void OnMouseDown()
    {
        on = true;
        for (int i = 0; i < childLines.Count; i++)
        {
            if (childLines[i].reflector != null)
            {
                childLines[i].reflector.EndCollision();
                childLines[i].reflector = null;
            }

            Debug.Log(childLines[i].reflector);
        }
        lineParent.transform.Rotate(e_XROT, e_YROT, e_XZROT);
        SetMatrix();
        CheckIfChildLineCollided();
    }

    /// <summary>
    /// Set Information for Emitter
    /// </summary>
    /// <param name="array"></param>
    public void SetEmitterInfo()
    {
        switch (matrixDictionary)
        {
            case "simple":
                emitterMatrix = app.puzzleController.matricies.simpleEmittersMatrix[emitterPattern];
                break;
            case "complex":
                emitterMatrix = app.puzzleController.matricies.complexEmittersMatrix[emitterPattern];
                break;
            default:
                break;
        }
        SetMatrix();
        CheckIfChildLineCollided();
    }

    // See if any line renderer children hit a collider
    void CheckIfChildLineCollided()
    {
        for (int i = 0; i < childLines.Count; i++)
        {
            childLines[i].CheckIntersection();

            Debug.Log(childLines[i].reflector);
        }
    }

    // SET MATRIX TO CREATE EMITTER LASER PATTERn
    // !WILL COME BACK TO, FOR EFFECIENCY
    // way better way to construct this
    void SetMatrix()
    {
        // position of the emitter ball mesh/gameobject
        childLines.Clear();

        // Iterate throught the emitter Matrix loop
        // Emitter Matrix information is 1,0 at this point
        for (int i = 0; i < emitterMatrix.Length; i++)
        {
            if (lineNodes[i] == null && i == emitterMatrix.Length - 1)
            {
                return;
            }

            // Get EmitterSubNode for Correct Direction (See Prefab)
            Transform node = transform;
            node = lineNodes[i].transform;

            switch (i)
            {
                case 0:
                    // Forward
                    if (emitterMatrix[i] == 1)
                    {
                        LineRenderer forward = SetLineRenderToEmitterSpawnNode(i);

                        forward.SetPosition(0, node.position);
                        forward.SetPosition(1, new Vector3(node.position.x, node.position.y, node.position.z + lengthOfLaser));
                    }
                    break;
                case 1:
                    // Right
                    if (emitterMatrix[i] == 1)
                    {
                        LineRenderer right = SetLineRenderToEmitterSpawnNode(i);

                        right.SetPosition(0, node.position);
                        right.SetPosition(1, new Vector3(node.position.x + lengthOfLaser, node.position.y, node.position.z));
                    }
                    break;
                case 2:
                    // Back
                    if (emitterMatrix[i] == 1)
                    {
                        LineRenderer back = SetLineRenderToEmitterSpawnNode(i);

                        back.SetPosition(0, node.position);
                        back.SetPosition(1, new Vector3(node.position.x, node.position.y, node.position.z - lengthOfLaser));

                    }
                    break;
                case 3:
                    // Left
                    if (emitterMatrix[i] == 1)
                    {
                        LineRenderer left = SetLineRenderToEmitterSpawnNode(i);

                        left.SetPosition(0, node.position);
                        left.SetPosition(1, new Vector3(node.position.x - lengthOfLaser, node.position.y, node.position.z));

                    }
                    break;
                case 4:
                    // Up
                    if (emitterMatrix[i] == 1)
                    {
                        LineRenderer up = SetLineRenderToEmitterSpawnNode(i);

                        up.SetPosition(0, node.position);
                        up.SetPosition(1, new Vector3(node.position.x, node.position.y + lengthOfLaser, node.position.z));

                    }
                    break;
                case 5:
                    // Down
                    if (emitterMatrix[i] == 1)
                    {
                        LineRenderer back = SetLineRenderToEmitterSpawnNode(i);

                        back.SetPosition(0, node.position);
                        back.SetPosition(1, new Vector3(node.position.x, node.position.y - lengthOfLaser, node.position.z));
                    }
                    break;
                default:
                    break;
            }
        }
    }

    int count = 0;
    [SerializeField] Material mat;
    /// <summary>
    /// Add a line renderer from the middle of the emitter
    /// Add line renderer to one of the line children for
    ///     an emitter (see emitter prefab)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    LineRenderer SetLineRenderToEmitterSpawnNode(int index)
    {
        // Prefab we already have a "Laser.cs" class attached
        line = lineNodes[index].GetComponent<LineRenderer>() ? lineNodes[index].GetComponent<LineRenderer>() : null;

        // Check if the child line is null
        if (line == null && index < lineNodes.Length)
        {
            line = lineNodes[index].AddComponent<LineRenderer>();
        }

        // last null check before setting line renderer information
        if (lineNodes[index] != null)
        {
            line.transform.parent = lineNodes[index].transform;

            line.startColor = Color.white;
            line.endColor = Color.white;
            line.material = mat;
            line.material.color = Color.white;
            line.sortingOrder = 1;
            line.startWidth = 1f;
            line.endWidth = 1f;
            line.receiveShadows = true;
            line.allowOcclusionWhenDynamic = true;
            line.useWorldSpace = true;
            line.motionVectorGenerationMode = MotionVectorGenerationMode.Camera;

            line.positionCount = numOfLasers;
            lines[count] = line;

            count++;
            if (count > lines.Length - 1)
            {
                count = 0;
            }

            Laser laser = line.GetComponent<Laser>();
            childLines.Add(laser);
        }
        return line;
    }

}
