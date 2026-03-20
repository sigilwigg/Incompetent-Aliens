using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Music
{
    public string musicID;
    public AudioClip clip;
    [HideInInspector] public AudioSource source;
}
