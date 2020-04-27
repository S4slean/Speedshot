using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event Action<bool> PauseGameEvent;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        
    }

    private void SetupGame()
    {

    }

    public void StartGame()
    {

    }

    public void PauseGame(bool isPaused)
    {
        PauseGameEvent?.Invoke(isPaused);
        // Afficher Menu
    }

    public void LeaveGame()
    {

    }
}
