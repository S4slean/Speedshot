using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public bool debugMode;
    private TeamEnum previousValue = TeamEnum.TEAM1;

    [HideInInspector] public Player[] players = new Player[4];


    public IPlayerSetter PlayerSetter { get; set; }

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
            DontDestroyOnLoad(this.gameObject);
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
                if(debugMode)
                {
                    previousValue = (TeamEnum)((int)(previousValue + 1) % 2);
                    players[i] = new Player(i, previousValue, newPlayer);
                }
                else
                    players[i] = new Player(i, TeamEnum.NONE, newPlayer);

                newPlayer.gameObject.transform.parent = transform;
                newPlayer.gameObject.name = "Player " + (i + 1);

                SetupPlayer(i);
                break;
            }
        }
    }

    public void RemovePlayer(PlayerInput playerInput)
    {
        for(int i = 0; i< players.Length; i++)
        {
            if(players[i].PlayerInput == playerInput)
            {
                UnsetupPlayer(i);
                players[i] = null;
                
                Destroy(playerInput.gameObject);
            }
        }
    }

    public void SetupPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
                SetupPlayer(i);
        }
    }

    public void UnsetupPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
                UnsetupPlayer(i);
        }
    }

    private void SetupPlayer(int playerID)
    {
        PlayerSetter?.SetupPlayer(playerID);
    }

    private void UnsetupPlayer(int playerID)
    {
        PlayerSetter?.UnsetupPlayer(playerID);
    }
}
