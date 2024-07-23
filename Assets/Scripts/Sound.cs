using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    public float volume = 1f; // Volumen por defecto a 1 (máximo)
    public float pitch = 1f;  // Pitch por defecto a 1 (normal)
}
