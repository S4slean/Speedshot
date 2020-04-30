using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player
{
    public int PlayerID { get; set; }

    public TeamEnum PlayerTeam { get; set; }

    public PlayerInput PlayerInput { get; set; }

    public Player(int playerID, TeamEnum playerTeam, PlayerInput playerInput)
    {
        PlayerID = playerID;
        PlayerTeam = playerTeam;
        PlayerInput = playerInput;
    }
}
