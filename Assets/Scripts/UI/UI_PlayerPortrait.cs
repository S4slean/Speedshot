﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerPortrait : MonoBehaviour
{
    [Header("UI Ref")]
    public GameObject holdingSprite;
    public Image playerNumber;

    public void BuildPortrait(int playerIndex, TeamEnum playerTeam)
    {
        playerNumber.sprite = UIManager.instance.indexPlayerSprite[playerIndex];

        SetPortraitPos(playerIndex, playerTeam);
    }

    public void SetPortraitPos(int playerIndex, TeamEnum playerTeam)
    {
        switch (playerTeam)
        {
            case TeamEnum.TEAM1:
                transform.position = UIManager.instance.bluePlayerPortraitSlots[playerIndex].position;
                break;

            case TeamEnum.TEAM2:
                transform.position = UIManager.instance.RedPlayerPortraitSlots[playerIndex].position;
                break;
        }
    }

    public void UpdateHoldingSprite(bool holding)
    {
        holdingSprite.SetActive(holding);
    }
}
