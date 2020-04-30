using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerSetter
{
    void SetupPlayer(int playerID);
    void UnsetupPlayer(int playerID);
}
