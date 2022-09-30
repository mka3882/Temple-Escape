using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public static Camera FindMainCamera()
    {
        return Camera.main ? Camera.main : Camera.current;
    }

    public static Camera FindFPSCamera()
    {
        return GameObject.Find("FirstPersonCharacter") ? GameObject.Find("FirstPersonCharacter").GetComponent<Camera>() : Camera.main;
    }

    public static Camera FindTopDownCamera()
    {
        return GameObject.Find("TopDownCamera") ? GameObject.Find("TopDownCamera").GetComponent<Camera>() : Camera.main;
    }

    public static Camera FindSceneCamera()
    {
        return GameObject.Find("Main Camera") ? GameObject.Find("Main Camera").GetComponent<Camera>() : Camera.main;
    }

    public static GameObject FindLightParent()
    {
        return GameObject.Find("Lights") ? GameObject.Find("Lights") : null;
    }

    public static GameObject FindEnvironmentParent()
    {
        return GameObject.Find("Environment") ? GameObject.Find("Environment") : null;
    }

    public static GameObject FindWorldObjectsParent()
    {
        return GameObject.Find("WorldObjects") ? GameObject.Find("AvatarObjects") : null;
    }

    public static AudioSource FindWorldAudioSource()
    {
        return GameObject.Find("WorldAudioSource") ? GameObject.Find("WorldAudioSource").GetComponent<AudioSource>() : null;
    }

    public static AudioSource FindMusicAudioSource()
    {
        return GameObject.Find("MusicAudioSource") ? GameObject.Find("MusicAudioSource").GetComponent<AudioSource>() : null;
    }

    // MISC. Script
    // CAN DELETE IF NEEDED
    public static ParticleSystem FindFXManager()
    {
        GameObject pObject = GameObject.Find("FXManager");
        ParticleSystem particleManager = pObject.GetComponent<ParticleSystem>();
        pObject.SetActive(false);
        return particleManager;
    }
}
