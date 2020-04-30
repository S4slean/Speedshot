using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrameHandler : MonoBehaviour, IPlayerSetter
{
    public PlayerFrame[] playersFrame;
	public Sprite[] sprites;

	public static PlayerFrameHandler instance;

    private void Awake()
    {
        PlayerManager.instance.PlayerSetter = this;

		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
    }

    public void SetupPlayer(int playerID)
    {
        playersFrame[playerID].menuInputHandler.SetPlayerInput(PlayerManager.instance.players[playerID].PlayerInput, playerID);
    }

    public void UnsetupPlayer(int playerID)
    {
        playersFrame[playerID].menuInputHandler.UnsetPlayerInput();
    }

	public void CheckIfEveryoneIsReady()
	{

		foreach(PlayerFrame playerF in playersFrame)
		{
			if (playerF.playerStatus == PlayerFrame.PlayerStatus.Waiting)
				return;
		}

		MainMenuManager.instance.LaunchGame();
	}
}
