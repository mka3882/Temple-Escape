using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Controller
{
    [SerializeField] List<AudioClip> WorldSoundLibrary;
    [SerializeField] List<AudioClip> MusicLibrary;
    static List<AudioClip> s_WorldLibrary;
    static AudioSource s_worldSoundAudioSource;
    AudioSource musicSoundAudioSource;

    public AudioClip PlayMusicAudio(int sound)
    {
        musicSoundAudioSource.clip = MusicLibrary[sound];
        musicSoundAudioSource.Play();
        return musicSoundAudioSource.clip;
    }

    //: Base Single Audio Instance
    public static void PlayOneInstanceAudio(int sound)
    {
        try
        {
            AudioClip clip = s_WorldLibrary[sound];
            s_worldSoundAudioSource.PlayOneShot(clip);
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public override void Init()
    {
        base.Init();

        // Find AudioSources
        s_worldSoundAudioSource = Environment.FindWorldAudioSource();
        musicSoundAudioSource = Environment.FindMusicAudioSource();

        // Set Music Library to Static for Fluent Audio Shot Calls
        // Set PlayOneInstanceAudio for Use
        s_WorldLibrary = WorldSoundLibrary;
        PlayMusicAudio(0);
    }
}
