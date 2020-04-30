// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controller.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controller : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""InputMap"",
            ""id"": ""62ea3567-64fa-496f-a554-d51e15ce979c"",
            ""actions"": [
                {
                    ""name"": ""VerticalAxis"",
                    ""type"": ""Button"",
                    ""id"": ""37a9f40d-82bf-4577-86fa-6117cdb630a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HorizontalAxis"",
                    ""type"": ""Button"",
                    ""id"": ""a5e21c89-2af1-4b5e-9eec-f94afe186252"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""88eeaf2c-9cb8-4a6c-b6f2-6b3eea9f2d45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""afcc2527-950d-4747-a004-7748cf003a80"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""B"",
                    ""type"": ""Button"",
                    ""id"": ""955be0d6-eb0c-41a5-a84a-f7122709d157"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""5116c3ba-c997-4980-837c-d68e3cff0c6a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0cc729f0-8a53-4db2-a73c-102c47e4c683"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cab88203-aed0-4eb0-90f0-407d19615c64"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""1625b60f-689e-4598-81d0-599276852fec"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""956eff5c-00f1-4e60-ab64-f161ae4ac8ff"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6e93cc93-85e3-417e-b38e-aa17822546b5"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""811e3343-04c3-4f37-8ab8-70a560b6354e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4a47298b-d67e-4681-b4ce-51863ae8a35e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""6c722999-21d0-41d6-b83b-518e64d581c8"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""42ce3c4e-ef3f-4172-b1d7-ae591046482f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fa7162e5-9421-468c-8bd9-bd7a32f8305c"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""7ab7af90-ec44-4966-bcef-0b137b913fc6"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DPad"",
                    ""id"": ""852a7d0b-9f7e-422a-91f1-95b4ec9ce50b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""c299414d-1c08-4d6b-84ca-009afb08989a"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c1f67eba-b379-45cb-b551-ada35ab8f0cd"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""201816c8-5c80-4c5e-a332-f6a25d058f19"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5c1b1643-80c4-46b4-93fa-190777ff2b10"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""16119717-2b99-443c-944e-aae8506fc449"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalAxis"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6fe4005f-1932-4d21-9ae7-655ab640270b"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f878d2be-8983-4ae2-82c9-054289efab0a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""97d4708a-820c-44ad-a720-361c28a6cdee"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c702e7a-4064-4547-9375-f1dc8d3e57ea"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3e068fa-38f2-41c0-81b4-2e7c57148fa9"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6eea6289-0269-4e9e-ada2-1bce44e6113b"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""B"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // InputMap
        m_InputMap = asset.FindActionMap("InputMap", throwIfNotFound: true);
        m_InputMap_VerticalAxis = m_InputMap.FindAction("VerticalAxis", throwIfNotFound: true);
        m_InputMap_HorizontalAxis = m_InputMap.FindAction("HorizontalAxis", throwIfNotFound: true);
        m_InputMap_Start = m_InputMap.FindAction("Start", throwIfNotFound: true);
        m_InputMap_A = m_InputMap.FindAction("A", throwIfNotFound: true);
        m_InputMap_B = m_InputMap.FindAction("B", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // InputMap
    private readonly InputActionMap m_InputMap;
    private IInputMapActions m_InputMapActionsCallbackInterface;
    private readonly InputAction m_InputMap_VerticalAxis;
    private readonly InputAction m_InputMap_HorizontalAxis;
    private readonly InputAction m_InputMap_Start;
    private readonly InputAction m_InputMap_A;
    private readonly InputAction m_InputMap_B;
    public struct InputMapActions
    {
        private @Controller m_Wrapper;
        public InputMapActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @VerticalAxis => m_Wrapper.m_InputMap_VerticalAxis;
        public InputAction @HorizontalAxis => m_Wrapper.m_InputMap_HorizontalAxis;
        public InputAction @Start => m_Wrapper.m_InputMap_Start;
        public InputAction @A => m_Wrapper.m_InputMap_A;
        public InputAction @B => m_Wrapper.m_InputMap_B;
        public InputActionMap Get() { return m_Wrapper.m_InputMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InputMapActions set) { return set.Get(); }
        public void SetCallbacks(IInputMapActions instance)
        {
            if (m_Wrapper.m_InputMapActionsCallbackInterface != null)
            {
                @VerticalAxis.started -= m_Wrapper.m_InputMapActionsCallbackInterface.OnVerticalAxis;
                @VerticalAxis.performed -= m_Wrapper.m_InputMapActionsCallbackInterface.OnVerticalAxis;
                @VerticalAxis.canceled -= m_Wrapper.m_InputMapActionsCallbackInterface.OnVerticalAxis;
                @HorizontalAxis.started -= m_Wrapper.m_InputMapActionsCallbackInterface.OnHorizontalAxis;
                @HorizontalAxis.performed -= m_Wrapper.m_InputMapActionsCallbackInterface.OnHorizontalAxis;
                @HorizontalAxis.canceled -= m_Wrapper.m_InputMapActionsCallbackInterface.OnHorizontalAxis;
                @Start.started -= m_Wrapper.m_InputMapActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_InputMapActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_InputMapActionsCallbackInterface.OnStart;
                @A.started -= m_Wrapper.m_InputMapActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_InputMapActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_InputMapActionsCallbackInterface.OnA;
                @B.started -= m_Wrapper.m_InputMapActionsCallbackInterface.OnB;
                @B.performed -= m_Wrapper.m_InputMapActionsCallbackInterface.OnB;
                @B.canceled -= m_Wrapper.m_InputMapActionsCallbackInterface.OnB;
            }
            m_Wrapper.m_InputMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @VerticalAxis.started += instance.OnVerticalAxis;
                @VerticalAxis.performed += instance.OnVerticalAxis;
                @VerticalAxis.canceled += instance.OnVerticalAxis;
                @HorizontalAxis.started += instance.OnHorizontalAxis;
                @HorizontalAxis.performed += instance.OnHorizontalAxis;
                @HorizontalAxis.canceled += instance.OnHorizontalAxis;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @B.started += instance.OnB;
                @B.performed += instance.OnB;
                @B.canceled += instance.OnB;
            }
        }
    }
    public InputMapActions @InputMap => new InputMapActions(this);
    public interface IInputMapActions
    {
        void OnVerticalAxis(InputAction.CallbackContext context);
        void OnHorizontalAxis(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnB(InputAction.CallbackContext context);
    }
}
