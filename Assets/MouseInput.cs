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
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Skill_1 = m_Player.FindAction("Skill_1", throwIfNotFound: true);
        m_Player_Skill_2 = m_Player.FindAction("Skill_2", throwIfNotFound: true);
        m_Player_Skill_3 = m_Player.FindAction("Skill_3", throwIfNotFound: true);
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
    public struct PlayerActions
    {
        private @MouseInput m_Wrapper;
        public PlayerActions(@MouseInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Skill_1 => m_Wrapper.m_Player_Skill_1;
        public InputAction @Skill_2 => m_Wrapper.m_Player_Skill_2;
        public InputAction @Skill_3 => m_Wrapper.m_Player_Skill_3;
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
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_InputControllerSchemeIndex = -1;
    public InputControlScheme InputControllerScheme
    {
        get
        {
            if (m_InputControllerSchemeIndex == -1) m_InputControllerSchemeIndex = asset.FindControlSchemeIndex("InputController");
            return asset.controlSchemes[m_InputControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSkill_1(InputAction.CallbackContext context);
        void OnSkill_2(InputAction.CallbackContext context);
        void OnSkill_3(InputAction.CallbackContext context);
    }
}
