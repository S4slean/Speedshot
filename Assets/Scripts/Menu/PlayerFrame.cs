using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrame : MonoBehaviour
{
    private MenuInputHandler menuInputHandler;
	public Image img;
	public Text txt;
	public GameObject arrows;
	public enum PlayerStatus { NotAssigned, Waiting, Ready};
	public PlayerStatus playerStatus;
	public Sprite[] sprites;
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
		SetPlayerStatus(PlayerStatus.Waiting);
	}

	public void Update()
	{
		if ( playerStatus == PlayerStatus.Waiting && menuInputHandler.LeftButtonDown)
		{
			iconIndex -= 1;
			if (iconIndex > 3) iconIndex = 0;
			UpdateSprite();
		}

		if (playerStatus == PlayerStatus.Waiting && menuInputHandler.RightButtonDown)
		{
			iconIndex += 1;
			if (iconIndex < 0) iconIndex = 3;
			UpdateSprite();
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
			//Check if everyone ready and start game
		}

	}

	public void SetPlayerStatus(PlayerStatus newStatus)
	{
		playerStatus = newStatus;
		if (playerStatus != PlayerStatus.Ready) arrows.SetActive(false);
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
		img.sprite = sprites[iconIndex];
	}
}
