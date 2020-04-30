using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputSetter
{
    void SetupInput(int playerID);
    void UnsetupInput(int playerID);
}
