﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrameHandler : MonoBehaviour, IPlayerSetter
{
    public PlayerFrame[] playersFrame;

    public void SetupPlayer(int playerID)
    {
        throw new System.NotImplementedException();
    }

    public void UnsetupPlayer(int playerID)
    {
        throw new System.NotImplementedException();
    }
}
