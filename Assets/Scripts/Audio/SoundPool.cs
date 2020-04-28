using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundPool
{
    public string soundPoolName;
    public float cooldown;

    public Sound[] sounds;
    
    private float lastPlayTime = 0;

    public float NextPlayableTime => lastPlayTime + cooldown;

    public void Initialize()
    {
        lastPlayTime = Time.time - cooldown;
    }

    public void PlayRandomSound(AudioSource audioSource)
    {
        Sound selectedSound = SelectRandomSound();

        SetAudioSource(audioSource, selectedSound);

        audioSource.Play();

        lastPlayTime = Time.time;
    }

    private void SetAudioSource(AudioSource source, Sound sound)
    {
        source.clip = sound.clip;
        source.loop = false;
        source.priority = sound.priority;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.panStereo = sound.stereoPan;
        source.spatialBlend = sound.spatialBlend;
        source.reverbZoneMix = sound.reverbZoneMix;
    }

    private Sound SelectRandomSound()
    {
        return sounds[Random.Range(0, sounds.Length)];
    }
}
