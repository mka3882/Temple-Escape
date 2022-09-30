using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : Controller
{
    bool created = false;
    public bool completed = false;
    public EmitterMatricies matricies;
    public TargetController targetController;
    EmitterController emitterController;
    public BoxController boxController;
    public ReflectorController reflectorController;
    [SerializeField] TextAsset matrixSet;
    public GameObject door;
    public GameObject targetUI;
    int totalTargets = 0;
    int numHit = 0;
    public bool checkTargets = true;

    void Update()
    {
        completed = GetIfWorldIsComplete();
        if (completed && door != null)
        {
            door.GetComponent<Door>().PuzzleSolved();
        }
        if(!completed && door != null && door.GetComponent<Door>().open)
        {
            door.GetComponent<Door>().CloseDoor();
        }
        targetUI.GetComponent<Text>().text = "Targets: " + numHit +"/" + totalTargets;
    }

    public EmitterMatricies Matricies
    {
        get
        {
            return matricies;
        }
    }

    public override void Init()
    {
        base.Init();

        if (matricies == null)
        {
            // Set Matrix Set
            matricies = new EmitterMatricies();
            matricies.Init();
        }

        if (targetController == null)
        {
            targetController = new TargetController();
        }

        if (emitterController == null)
        {
            emitterController = new EmitterController();
        }

        if (reflectorController == null)
        {
            reflectorController = new ReflectorController();
        }

        if (boxController == null)
        {
            boxController = new BoxController();
        }

        InitScenePuzzle();
    }

    void InitScenePuzzle()
    {
        created = false;
        completed = false;

        GetWorldPuzzleObjects();
    }

    void GetWorldPuzzleObjects()
    {
        // Get Objectables Controller Lists
        Target[] targets = FindObjectsOfType<Target>();
        targetController.targetsInScene = targets;
        totalTargets = targetController.targetsInScene.Length;
        foreach (var target in targetController.targetsInScene)
        {
            foreach (var particleSystem in target.GetComponentsInChildren<ParticleSystem>())
            {
                if (particleSystem.gameObject.name == "Flame Enchant" || particleSystem.gameObject.name == "Boom")
                {
                    target.particleSystems.Add(particleSystem);
                }
            }
        }
        
        Reflector[] reflectors = FindObjectsOfType<Reflector>();
        reflectorController.reflectorsInScene = reflectors;

        Box[] boxes = FindObjectsOfType<Box>();
        boxController.boxesInScene = boxes;

        Emitter[] emitters = FindObjectsOfType<Emitter>();
        emitterController.emittersInScene = emitters;
        for (int i = 0; i < emitterController.emittersInScene.Length; i++)
        {
            // This Creates/Resets the Emitter Laser Information
            // See "matrix.json" for more information if you need help
            emitterController.emittersInScene[i].SetEmitterInfo();
        }
    }

    public void CheckBoxes(GameObject reflector)
    {
        if (boxController.boxesInScene != null)
        {
            foreach (Box box in boxController.boxesInScene)
            {
                box.CheckBox(reflector);
            }
        }
    }

    // Check if all Targets are Reached
    bool GetIfWorldIsComplete()
    {
        bool result = true;
        numHit = 0;
        foreach (Target target in targetController.targetsInScene)
        {
            if (target.Reached == false)
            {
                result = false;
            }
            else
            {
                numHit++;
            }
        }
        return result;
    }
}

public class BoxController : Controller
{
    public Box[] boxesInScene;
}

public class ReflectorController : Controller
{
    public Reflector[] reflectorsInScene;
    public Reflector reflector;
}

public class TargetController : Controller {
    public Target[] targetsInScene;
}

public class EmitterController : Controller
{
    public Emitter[] emittersInScene;
}