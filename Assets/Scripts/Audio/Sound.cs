using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public AudioClip clip;

    [Range(0, 256)]
    public int priority;

    [Range(0.0f, 1.0f)]
    public float volume;

    [Range(-3.0f, 3.0f)]
    public float pitch;

    [Range(-1.0f, 1.0f)]
    public float stereoPan;

    [Range(0.0f, 1.0f)]
    public float spatialBlend;

    [Range(0.0f, 1.1f)]
    public float reverbZoneMix;
}
