using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] soundInfos;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
            
        
        foreach (var s in soundInfos)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Bgm");
    }

    public void Play(string soundNameToPlay)
    {
        Sound s = Array.Find(soundInfos, sound => sound.audioName == soundNameToPlay);
        if (s == null) // Can't find the sound
        {
            Debug.LogWarning("Sound: "+soundNameToPlay+" not found!");
            return;
        }
        s.source.Play();
    }
}
