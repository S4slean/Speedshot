using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [HideInInspector] public PlayerInput[] players = new PlayerInput[4];

    public IInputSetter inputSetter { get; set; }

    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void AllowPlayerToJoin(bool isAllowed)
    {
        if (isAllowed && !playerInputManager.joiningEnabled)
            playerInputManager.EnableJoining();
        else if (!isAllowed && playerInputManager.joiningEnabled)
            playerInputManager.DisableJoining();
    }

    public void AddPlayer(PlayerInput newPlayer)
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] == null)
            {
                players[i] = newPlayer;

                newPlayer.gameObject.transform.parent = transform;
                newPlayer.gameObject.name = "Player " + (i + 1);

                SetupInput(i);
                break;
            }
        }
    }

    public void RemovePlayer(PlayerInput player)
    {
        for(int i = 0; i< players.Length; i++)
        {
            if(players[i] == player)
            {
                UnsetupInput(i);
                players[i] = null;
                
                Destroy(player.gameObject);
            }
        }
    }

    public void SetupInput()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
                SetupInput(i);
        }
    }

    public void UnsetupInput()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
                UnsetupInput(i);
        }
    }

    private void SetupInput(int playerID)
    {
        inputSetter?.SetupInput(playerID);
    }

    private void UnsetupInput(int playerID)
    {
        inputSetter?.UnsetupInput(playerID);
    }
}
