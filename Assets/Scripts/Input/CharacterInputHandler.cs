using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class CharacterInputHandler : MonoBehaviour
{
    private Vector2 movementAxis;
    public Vector2 MovementAxis { get => movementAxis; private set => movementAxis = value; }
    private bool rightButtonDown = false;
    public bool RightButtonDown
    {
        get
        {
            bool returnValue = false;
            if (rightButtonDown)
            {
                returnValue = true;
                rightButtonDown = false;
            }
            return returnValue;
        }
    }
    private bool leftButtonDown = false;
    public bool LeftButtonDown
    {
        get
        {
            bool returnValue = false;
            if (leftButtonDown)
            {
                returnValue = true;
                leftButtonDown = false;
            }
            return returnValue;
        }
    }
    private bool jumpButtonDown = false;
    public bool JumpButtonDown
    {
        get
        {
            //bool returnValue = false;
            //if(jumpButtonDown)
            //{
            //    returnValue = true;
            //    jumpButtonDown = false;
            //}
            //return returnValue;
            return ConsumeInpute(ref jumpButtonDown);
        }
    }
    private bool actionButtonDown = false;
    public bool ActionButtonDown
    {
        get
        {
            bool returnValue = false;
            if (actionButtonDown)
            {
                returnValue = true;
                actionButtonDown = false;
            }
            return returnValue;
        }
    }

    private PlayerInput _playerInput;
    private InputAction _verticalAxisAction;
    private InputAction _horizontalAxisAction;
    private InputAction _startAction;
    private InputAction _aButtonAction;
    private InputAction _bButtonAction;



    public void SetPlayerInput(PlayerInput playerInput)
    {
        _playerInput = playerInput;

        _horizontalAxisAction = _playerInput.actions["HorizontalAxis"];
        _verticalAxisAction = _playerInput.actions["VerticalAxis"];
        _startAction = _playerInput.actions["Start"];
        _aButtonAction = _playerInput.actions["A"];
        _bButtonAction = _playerInput.actions["B"];

        _horizontalAxisAction.performed += HorizontalMovement;
        _horizontalAxisAction.performed += HorizontalKeyPress;
        _horizontalAxisAction.canceled += HorizontalMovement;
        _verticalAxisAction.performed += VerticalMovement;
        _verticalAxisAction.canceled += VerticalMovement;
        _startAction.performed += StartButtonPress;
        _aButtonAction.performed += AButtonPress;
        _bButtonAction.performed += BButtonPress;
    }

    public void UnsetPlayerInput()
    {
        _horizontalAxisAction.performed -= HorizontalMovement;
        _horizontalAxisAction.performed -= HorizontalKeyPress;
        _horizontalAxisAction.canceled -= HorizontalMovement;
        _verticalAxisAction.performed -= VerticalMovement;
        _verticalAxisAction.canceled -= VerticalMovement;
        _startAction.performed -= StartButtonPress;
        _aButtonAction.performed -= AButtonPress;
        _bButtonAction.performed -= BButtonPress;

        _horizontalAxisAction = null;
        _verticalAxisAction = null;
        _startAction = null;
        _aButtonAction = null;
        _bButtonAction = null;

        _playerInput = null;
    }

    private void HorizontalMovement(CallbackContext context)
    {
        movementAxis.x = context.ReadValue<int>();
    }

    private void HorizontalKeyPress(CallbackContext context)
    {
        if (context.ReadValue<int>() < 0)
            leftButtonDown = true;
        else if (context.ReadValue<int>() > 0)
            rightButtonDown = true;
    }

    private void VerticalMovement(CallbackContext context)
    {
        movementAxis.y = context.ReadValue<int>(); 
    }

    private void StartButtonPress(CallbackContext context)
    {
        
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
