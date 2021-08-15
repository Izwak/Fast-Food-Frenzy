using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// This Sound class is for 2d sounds or sounds that dont take place in 3d space and aren't tied 2 a perticular object
[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;


    [HideInInspector]
    public bool isDoingSomin = false; // I AM A HACK LEMMI WRITRE BAD CODE
}
