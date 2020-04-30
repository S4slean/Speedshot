using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _verticalAxisAction;
    private InputAction _horizontalAxisAction;
    private InputAction _startAction;
    private InputAction _aButtunAction;
    private InputAction _bButtunAction;

    public void SetPlayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;

        _verticalAxisAction = _playerInput.actions["VerticalAxis"];
        _horizontalAxisAction = _playerInput.actions["HorizontalAxis"];
        _startAction = _playerInput.actions["Start"];
        _aButtunAction = _playerInput.actions["A"];
        _bButtunAction = _playerInput.actions["B"];
    }
}
