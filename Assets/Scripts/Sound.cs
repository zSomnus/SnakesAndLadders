﻿using UnityEngine;

[CreateAssetMenu(fileName = "SoundInfo", menuName = "Sound/SoundInfo")]
public class Sound : ScriptableObject
{
    public string audioName;
    public AudioClip audioClip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
    
}