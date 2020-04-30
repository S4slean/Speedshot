using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrameHandler : MonoBehaviour, IPlayerSetter
{
    public PlayerFrame[] playersFrame;

    private void Awake()
    {
        PlayerManager.instance.PlayerSetter = this;
    }

    public void SetupPlayer(int playerID)
    {
        playersFrame[playerID].menuInputHandler.SetPlayerInput(PlayerManager.instance.players[playerID].PlayerInput, playerID);
    }

    public void UnsetupPlayer(int playerID)
    {
        playersFrame[playerID].menuInputHandler.UnsetPlayerInput();
    }
}
