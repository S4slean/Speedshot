using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrame : MonoBehaviour
{
    [HideInInspector]public MenuInputHandler menuInputHandler;
	public Image img;
	public Text txt;
	public GameObject arrows;
	public enum PlayerStatus { NotAssigned, Waiting, Ready};
	public PlayerStatus playerStatus;
	private int iconIndex = 0;

    private void Awake()
    {
        menuInputHandler = GetComponent<MenuInputHandler>();
    }

    public void OnPlayerAssigned()
    {
		img.gameObject.SetActive(true);
		SetPlayerStatus(PlayerStatus.Waiting);
    }

    public void OnPlayerUnassigned()
    {
		img.gameObject.SetActive(false);
		SetPlayerStatus(PlayerStatus.NotAssigned);
	}

	public void Update()
	{
		if ( playerStatus == PlayerStatus.Waiting && menuInputHandler.LeftButtonDown)
		{
			iconIndex += 4;
			iconIndex -= 1;
			iconIndex = iconIndex % 4;
			UpdateSprite();
			PlayerManager.instance.players[menuInputHandler.CurrentPlayerID].skinIndex = iconIndex;
			PlayerManager.instance.players[menuInputHandler.CurrentPlayerID].PlayerTeam = iconIndex < 2 ? TeamEnum.TEAM1 : TeamEnum.TEAM2;

			
		}

		if (playerStatus == PlayerStatus.Waiting && menuInputHandler.RightButtonDown)
		{
			iconIndex += 1;
			iconIndex = iconIndex % 4;
			UpdateSprite();
			PlayerManager.instance.players[menuInputHandler.CurrentPlayerID].skinIndex = iconIndex;
			PlayerManager.instance.players[menuInputHandler.CurrentPlayerID].PlayerTeam = iconIndex < 2 ? TeamEnum.TEAM1 : TeamEnum.TEAM2;
		}

		if (playerStatus == PlayerStatus.Waiting && menuInputHandler.JumpButtonDown)
		{
			SetPlayerStatus(PlayerStatus.Ready);
		}

		if(playerStatus == PlayerStatus.Ready && menuInputHandler.ActionButtonDown)
		{
			SetPlayerStatus(PlayerStatus.Waiting);
		}

		if (menuInputHandler.StartButtonDown)
		{
			Debug.Log("Start");
			PlayerFrameHandler.instance.CheckIfEveryoneIsReady();
		}

	}

	public void SetPlayerStatus(PlayerStatus newStatus)
	{
		playerStatus = newStatus;
		arrows.SetActive(playerStatus == PlayerStatus.Waiting);
		img.gameObject.SetActive(playerStatus != PlayerStatus.NotAssigned);

		switch (playerStatus)
		{
			case PlayerStatus.NotAssigned:
				txt.text = "Press Start to Join.";
				break;

			case PlayerStatus.Waiting:
				txt.text = "Select your character";
				break;

			case PlayerStatus.Ready:
				txt.text = "Ready";
				break;

		}
	}

	public void UpdateSprite()
	{
		img.sprite = PlayerFrameHandler.instance.sprites[iconIndex];
	}
}
