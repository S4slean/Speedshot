using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager2D : MonoBehaviour
{
    public static AudioManager2D instance;

	public SoundPool[] soundPools;

    private Dictionary<string, SoundPool> soundDictionary;

    private void Awake()
    {
        instance = this;

        InitSoundDictionary();
    }

    private void InitSoundDictionary()
    {
        soundDictionary = new Dictionary<string, SoundPool>();

        foreach(SoundPool soundPool in soundPools)
        {
            soundPool.Initialize();
            string soundTag = soundPool.soundPoolName;
            if (!soundDictionary.ContainsKey(soundTag))
            {
                soundDictionary.Add(soundTag, soundPool);
            }
            else
            {
                Debug.LogError("AudioManager2D: SoundPoolKey " + soundTag + " already Exist!");
            }
        }
    }

    public void PlaySound(string soundPoolName, Vector3 spawnPosition)                 
    {
        if (soundDictionary.ContainsKey(soundPoolName))
        {
            if (Time.time < soundDictionary[soundPoolName].NextPlayableTime)
            {
                //Debug.Log("SoundPool " + soundPoolName + " is on cooldown!");
                return;
            }

            GameObject audioSourceGameObject = (GameObject)PoolManager2D.instance?.SpawnFromPool("AudioSource", spawnPosition, Quaternion.identity);
            AudioSource audioSource = audioSourceGameObject?.GetComponent<AudioSource>();

            soundDictionary[soundPoolName].PlayRandomSound(audioSource);
        }
        else
        {
            Debug.LogError("Sound tag " + soundPoolName + " has no SoundPool associated with!");
        }
    }
}
