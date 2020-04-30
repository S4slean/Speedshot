using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class MenuInputHandler : MonoBehaviour
{
    private bool upButtonDown = false;
    public bool UpButtonDown { get => ConsumeInpute(ref upButtonDown); }
    private bool downButtonDown = false;
    public bool DownButtonDown { get => ConsumeInpute(ref downButtonDown); }
    private bool rightButtonDown = false;
    public bool RightButtonDown { get => ConsumeInpute(ref rightButtonDown); }
    private bool leftButtonDown = false;
    public bool LeftButtonDown { get => ConsumeInpute(ref leftButtonDown); }
    private bool startButtonDown = false;
    public bool StartButtonDown { get => ConsumeInpute(ref startButtonDown); }
    private bool jumpButtonDown = false;
    public bool JumpButtonDown { get => ConsumeInpute(ref jumpButtonDown); }
    private bool actionButtonDown = false;
    public bool ActionButtonDown { get => ConsumeInpute(ref actionButtonDown); }

    public int CurrentPlayerID { get; private set; }

    private PlayerInput _playerInput;
    private InputAction _verticalAxisAction;
    private InputAction _horizontalAxisAction;
    private InputAction _startAction;
    private InputAction _aButtonAction;
    private InputAction _bButtonAction;

    private PlayerFrame playerFrame;

    private void Awake()
    {
        playerFrame = GetComponent<PlayerFrame>();
    }

    public void SetPlayerInput(PlayerInput playerInput, int playerID)
    {
        _playerInput = playerInput;

        _horizontalAxisAction = _playerInput.actions["HorizontalAxis"];
        _verticalAxisAction = _playerInput.actions["VerticalAxis"];
        _startAction = _playerInput.actions["Start"];
        _aButtonAction = _playerInput.actions["A"];
        _bButtonAction = _playerInput.actions["B"];

        _horizontalAxisAction.performed += HorizontalKeyPress;
        _verticalAxisAction.performed += VerticalKeyPress;
        _startAction.performed += StartButtonPress;
        _aButtonAction.performed += AButtonPress;
        _bButtonAction.performed += BButtonPress;

        playerFrame.OnPlayerAssigned();

        CurrentPlayerID = playerID;
    }

    public void UnsetPlayerInput()
    {
        _horizontalAxisAction.performed -= HorizontalKeyPress;
        _verticalAxisAction.performed -= VerticalKeyPress;
        _startAction.performed -= StartButtonPress;
        _aButtonAction.performed -= AButtonPress;
        _bButtonAction.performed -= BButtonPress;

        _horizontalAxisAction = null;
        _verticalAxisAction = null;
        _startAction = null;
        _aButtonAction = null;
        _bButtonAction = null;

        _playerInput = null;

        playerFrame.OnPlayerUnassigned();

        CurrentPlayerID = -1;
    }


    private void HorizontalKeyPress(CallbackContext context)
    {
        if (Mathf.Sign(context.ReadValue<float>()) < 0)
            leftButtonDown = true;
        else if (Mathf.Sign(context.ReadValue<float>()) > 0)
            rightButtonDown = true;
    }
    
    private void VerticalKeyPress(CallbackContext context)
    {
        if (Mathf.Sign(context.ReadValue<float>()) < 0)
            downButtonDown = true;
        else if (Mathf.Sign(context.ReadValue<float>()) > 0)
            upButtonDown = true;
    }

    private void StartButtonPress(CallbackContext context)
    {
        startButtonDown = true;
    }

    private void AButtonPress(CallbackContext context)
    {
        jumpButtonDown = true;
    }

    private void BButtonPress(CallbackContext context)
    {
        actionButtonDown = true;
    }

    private bool ConsumeInpute(ref bool input)
    {
        bool returnValue = false;
        if (input)
        {
            returnValue = true;
            input = false;
        }
        return returnValue;
    }
}
