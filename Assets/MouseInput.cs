// GENERATED AUTOMATICALLY FROM 'Assets/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MouseInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MouseInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""e75cc2da-314a-400a-b4e6-51e23354bae5"",
            ""actions"": [
                {
                    ""name"": ""Skill_1"",
                    ""type"": ""Button"",
                    ""id"": ""9d470720-c566-40be-95d1-6b37410a446e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill_2"",
                    ""type"": ""Button"",
                    ""id"": ""2b16c059-a1ac-47fc-af4f-1e0e84cdda0e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill_3"",
                    ""type"": ""Button"",
                    ""id"": ""941bd0dd-fec5-4a7c-ac27-6c834ef921ea"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill_4"",
                    ""type"": ""Button"",
                    ""id"": ""b974fd9f-a8c6-4f14-8dd7-6242895712e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""84155b17-e059-465d-a547-e8ca730a668f"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skill_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d48b1b5c-6d1c-446c-8371-90be5eeceb8d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""InputController"",
                    ""action"": ""Skill_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f95e41e-0ae3-48a2-81c3-d307db25e02b"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""InputController"",
                    ""action"": ""Skill_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8925a7f2-e7d7-43a7-84fc-cbc05e0557f7"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""InputController"",
                    ""action"": ""Skill_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""d65d2cdd-2b5f-4896-a576-106bd0940c8b"",
            ""actions"": [
                {
                    ""name"": ""ESC"",
                    ""type"": ""Button"",
                    ""id"": ""4ae79793-1b48-4ed5-b721-6e18aa0f7db8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""32d1217a-94cd-4a5b-beac-9a7f56b11321"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ESC"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""2a3c3a9e-5936-40d2-b72a-3d875401bd35"",
            ""actions"": [
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""17d2e456-aa73-4c73-b4a5-c2d93a62bd72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""170ac9dd-150f-4060-a8c2-0cd38c397fff"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""InputController"",
            ""bindingGroup"": ""InputController"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": [
                {
                    ""devicePath"": ""<Touchscreen>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Skill_1 = m_Player.FindAction("Skill_1", throwIfNotFound: true);
        m_Player_Skill_2 = m_Player.FindAction("Skill_2", throwIfNotFound: true);
        m_Player_Skill_3 = m_Player.FindAction("Skill_3", throwIfNotFound: true);
        m_Player_Skill_4 = m_Player.FindAction("Skill_4", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_ESC = m_UI.FindAction("ESC", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Shift = m_Mouse.FindAction("Shift", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Skill_1;
    private readonly InputAction m_Player_Skill_2;
    private readonly InputAction m_Player_Skill_3;
    private readonly InputAction m_Player_Skill_4;
    public struct PlayerActions
    {
        private @MouseInput m_Wrapper;
        public PlayerActions(@MouseInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skill_1 => m_Wrapper.m_Player_Skill_1;
        public InputAction @Skill_2 => m_Wrapper.m_Player_Skill_2;
        public InputAction @Skill_3 => m_Wrapper.m_Player_Skill_3;
        public InputAction @Skill_4 => m_Wrapper.m_Player_Skill_4;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Skill_1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_1;
                @Skill_1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_1;
                @Skill_1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_1;
                @Skill_2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_2;
                @Skill_2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_2;
                @Skill_2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_2;
                @Skill_3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_3;
                @Skill_3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_3;
                @Skill_3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_3;
                @Skill_4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_4;
                @Skill_4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_4;
                @Skill_4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSkill_4;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Skill_1.started += instance.OnSkill_1;
                @Skill_1.performed += instance.OnSkill_1;
                @Skill_1.canceled += instance.OnSkill_1;
                @Skill_2.started += instance.OnSkill_2;
                @Skill_2.performed += instance.OnSkill_2;
                @Skill_2.canceled += instance.OnSkill_2;
                @Skill_3.started += instance.OnSkill_3;
                @Skill_3.performed += instance.OnSkill_3;
                @Skill_3.canceled += instance.OnSkill_3;
                @Skill_4.started += instance.OnSkill_4;
                @Skill_4.performed += instance.OnSkill_4;
                @Skill_4.canceled += instance.OnSkill_4;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_ESC;
    public struct UIActions
    {
        private @MouseInput m_Wrapper;
        public UIActions(@MouseInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @ESC => m_Wrapper.m_UI_ESC;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @ESC.started -= m_Wrapper.m_UIActionsCallbackInterface.OnESC;
                @ESC.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnESC;
                @ESC.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnESC;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ESC.started += instance.OnESC;
                @ESC.performed += instance.OnESC;
                @ESC.canceled += instance.OnESC;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_Shift;
    public struct MouseActions
    {
        private @MouseInput m_Wrapper;
        public MouseActions(@MouseInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shift => m_Wrapper.m_Mouse_Shift;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @Shift.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnShift;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    private int m_InputControllerSchemeIndex = -1;
    public InputControlScheme InputControllerScheme
    {
        get
        {
            if (m_InputControllerSchemeIndex == -1) m_InputControllerSchemeIndex = asset.FindControlSchemeIndex("InputController");
            return asset.controlSchemes[m_InputControllerSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSkill_1(InputAction.CallbackContext context);
        void OnSkill_2(InputAction.CallbackContext context);
        void OnSkill_3(InputAction.CallbackContext context);
        void OnSkill_4(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnESC(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnShift(InputAction.CallbackContext context);
    }
}
