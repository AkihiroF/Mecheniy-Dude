//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Source/InputSystem/Input.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""b1205e5f-f105-48fa-97d8-361ad362721c"",
            ""actions"": [
                {
                    ""name"": ""Moving"",
                    ""type"": ""Value"",
                    ""id"": ""ffbfbf14-c0f3-4b0d-8a02-2c70a7f74251"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""3932dc5a-39e7-46da-a7a8-ff0b62c13b43"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""5767da77-6ef0-481b-bddd-ad73a0e4028e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FireAutomatic"",
                    ""type"": ""Button"",
                    ""id"": ""038fc5c1-eebe-4fc7-b87a-3c1d44b89593"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.01,behavior=2)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""caa7960e-8190-4513-9847-bd7b64bb066c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Healing"",
                    ""type"": ""Button"",
                    ""id"": ""ff0f2d2a-c4a8-40e0-af52-1a0f0f66576f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interactive"",
                    ""type"": ""Button"",
                    ""id"": ""e32b325e-f9a8-4151-a30a-4ac337130f15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChoosePistol"",
                    ""type"": ""Button"",
                    ""id"": ""9ffcbaff-bec5-4322-84cd-966d66c62584"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChooseShortGun"",
                    ""type"": ""Button"",
                    ""id"": ""dd1666a2-d0ff-4878-a9fe-009d4b4168dc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ChooseRifle"",
                    ""type"": ""Button"",
                    ""id"": ""5ac176db-d06d-4066-93c7-0cb62362cb32"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""VectorMoving"",
                    ""id"": ""726302b6-bb9f-493d-a130-783af42be58b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Moving"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6aef3efe-68f3-4aa2-9d45-47d31766b0b1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""13e25b63-7c6e-4e6b-a37f-7f8906e66d17"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0f0ffe49-7dff-4793-b598-f19356bcddee"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""82d1be34-491a-4d28-92d7-153f6bb25c44"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Moving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""177b67a5-012c-45d6-8ea4-399c1bfb225c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32dc0c0c-a3f0-4bab-b027-9b301af69cef"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bda84f4-fe09-4548-af4e-a8aa47d0efca"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ddcdbb95-c3c8-41f1-9408-61d758980731"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Healing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2503ed3-d68e-4cf2-a5a3-377f1812662d"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interactive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""004733e4-2963-4e9f-94a1-68b4c0865ff0"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChoosePistol"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6cb12b5-8575-4552-b250-40f31e5a87a7"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseRifle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31279711-a3f3-4237-9336-5297391538bc"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChooseShortGun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88ea7f26-6f82-415e-8946-ce58e96b5896"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerSheme"",
                    ""action"": ""FireAutomatic"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interface"",
            ""id"": ""8644aaf3-6d20-497d-ade7-7a94164b1e42"",
            ""actions"": [
                {
                    ""name"": ""Paused"",
                    ""type"": ""Button"",
                    ""id"": ""61e31267-ad8d-411d-8274-5d87f6f16265"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""WeaponPanel"",
                    ""type"": ""Button"",
                    ""id"": ""0c1b5903-50c1-423a-9c28-1ff30317d3c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""db94ca98-649b-4d77-8832-c1b90ea38dc7"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Paused"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55f414d4-c93a-4b4e-a7c5-af4b7aa1d7e6"",
                    ""path"": ""<Keyboard>/backquote"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""WeaponPanel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerSheme"",
            ""bindingGroup"": ""PlayerSheme"",
            ""devices"": [
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
        m_Player_Moving = m_Player.FindAction("Moving", throwIfNotFound: true);
        m_Player_Rotate = m_Player.FindAction("Rotate", throwIfNotFound: true);
        m_Player_Fire = m_Player.FindAction("Fire", throwIfNotFound: true);
        m_Player_FireAutomatic = m_Player.FindAction("FireAutomatic", throwIfNotFound: true);
        m_Player_Reload = m_Player.FindAction("Reload", throwIfNotFound: true);
        m_Player_Healing = m_Player.FindAction("Healing", throwIfNotFound: true);
        m_Player_Interactive = m_Player.FindAction("Interactive", throwIfNotFound: true);
        m_Player_ChoosePistol = m_Player.FindAction("ChoosePistol", throwIfNotFound: true);
        m_Player_ChooseShortGun = m_Player.FindAction("ChooseShortGun", throwIfNotFound: true);
        m_Player_ChooseRifle = m_Player.FindAction("ChooseRifle", throwIfNotFound: true);
        // Interface
        m_Interface = asset.FindActionMap("Interface", throwIfNotFound: true);
        m_Interface_Paused = m_Interface.FindAction("Paused", throwIfNotFound: true);
        m_Interface_WeaponPanel = m_Interface.FindAction("WeaponPanel", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Moving;
    private readonly InputAction m_Player_Rotate;
    private readonly InputAction m_Player_Fire;
    private readonly InputAction m_Player_FireAutomatic;
    private readonly InputAction m_Player_Reload;
    private readonly InputAction m_Player_Healing;
    private readonly InputAction m_Player_Interactive;
    private readonly InputAction m_Player_ChoosePistol;
    private readonly InputAction m_Player_ChooseShortGun;
    private readonly InputAction m_Player_ChooseRifle;
    public struct PlayerActions
    {
        private @Input m_Wrapper;
        public PlayerActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Moving => m_Wrapper.m_Player_Moving;
        public InputAction @Rotate => m_Wrapper.m_Player_Rotate;
        public InputAction @Fire => m_Wrapper.m_Player_Fire;
        public InputAction @FireAutomatic => m_Wrapper.m_Player_FireAutomatic;
        public InputAction @Reload => m_Wrapper.m_Player_Reload;
        public InputAction @Healing => m_Wrapper.m_Player_Healing;
        public InputAction @Interactive => m_Wrapper.m_Player_Interactive;
        public InputAction @ChoosePistol => m_Wrapper.m_Player_ChoosePistol;
        public InputAction @ChooseShortGun => m_Wrapper.m_Player_ChooseShortGun;
        public InputAction @ChooseRifle => m_Wrapper.m_Player_ChooseRifle;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Moving.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoving;
                @Moving.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoving;
                @Moving.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMoving;
                @Rotate.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRotate;
                @Fire.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFire;
                @FireAutomatic.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireAutomatic;
                @FireAutomatic.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireAutomatic;
                @FireAutomatic.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnFireAutomatic;
                @Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                @Healing.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealing;
                @Healing.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealing;
                @Healing.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHealing;
                @Interactive.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractive;
                @Interactive.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractive;
                @Interactive.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInteractive;
                @ChoosePistol.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChoosePistol;
                @ChoosePistol.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChoosePistol;
                @ChoosePistol.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChoosePistol;
                @ChooseShortGun.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChooseShortGun;
                @ChooseShortGun.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChooseShortGun;
                @ChooseShortGun.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChooseShortGun;
                @ChooseRifle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChooseRifle;
                @ChooseRifle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChooseRifle;
                @ChooseRifle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnChooseRifle;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Moving.started += instance.OnMoving;
                @Moving.performed += instance.OnMoving;
                @Moving.canceled += instance.OnMoving;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @FireAutomatic.started += instance.OnFireAutomatic;
                @FireAutomatic.performed += instance.OnFireAutomatic;
                @FireAutomatic.canceled += instance.OnFireAutomatic;
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @Healing.started += instance.OnHealing;
                @Healing.performed += instance.OnHealing;
                @Healing.canceled += instance.OnHealing;
                @Interactive.started += instance.OnInteractive;
                @Interactive.performed += instance.OnInteractive;
                @Interactive.canceled += instance.OnInteractive;
                @ChoosePistol.started += instance.OnChoosePistol;
                @ChoosePistol.performed += instance.OnChoosePistol;
                @ChoosePistol.canceled += instance.OnChoosePistol;
                @ChooseShortGun.started += instance.OnChooseShortGun;
                @ChooseShortGun.performed += instance.OnChooseShortGun;
                @ChooseShortGun.canceled += instance.OnChooseShortGun;
                @ChooseRifle.started += instance.OnChooseRifle;
                @ChooseRifle.performed += instance.OnChooseRifle;
                @ChooseRifle.canceled += instance.OnChooseRifle;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Interface
    private readonly InputActionMap m_Interface;
    private IInterfaceActions m_InterfaceActionsCallbackInterface;
    private readonly InputAction m_Interface_Paused;
    private readonly InputAction m_Interface_WeaponPanel;
    public struct InterfaceActions
    {
        private @Input m_Wrapper;
        public InterfaceActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Paused => m_Wrapper.m_Interface_Paused;
        public InputAction @WeaponPanel => m_Wrapper.m_Interface_WeaponPanel;
        public InputActionMap Get() { return m_Wrapper.m_Interface; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InterfaceActions set) { return set.Get(); }
        public void SetCallbacks(IInterfaceActions instance)
        {
            if (m_Wrapper.m_InterfaceActionsCallbackInterface != null)
            {
                @Paused.started -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnPaused;
                @Paused.performed -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnPaused;
                @Paused.canceled -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnPaused;
                @WeaponPanel.started -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnWeaponPanel;
                @WeaponPanel.performed -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnWeaponPanel;
                @WeaponPanel.canceled -= m_Wrapper.m_InterfaceActionsCallbackInterface.OnWeaponPanel;
            }
            m_Wrapper.m_InterfaceActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Paused.started += instance.OnPaused;
                @Paused.performed += instance.OnPaused;
                @Paused.canceled += instance.OnPaused;
                @WeaponPanel.started += instance.OnWeaponPanel;
                @WeaponPanel.performed += instance.OnWeaponPanel;
                @WeaponPanel.canceled += instance.OnWeaponPanel;
            }
        }
    }
    public InterfaceActions @Interface => new InterfaceActions(this);
    private int m_PlayerShemeSchemeIndex = -1;
    public InputControlScheme PlayerShemeScheme
    {
        get
        {
            if (m_PlayerShemeSchemeIndex == -1) m_PlayerShemeSchemeIndex = asset.FindControlSchemeIndex("PlayerSheme");
            return asset.controlSchemes[m_PlayerShemeSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMoving(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnFireAutomatic(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnHealing(InputAction.CallbackContext context);
        void OnInteractive(InputAction.CallbackContext context);
        void OnChoosePistol(InputAction.CallbackContext context);
        void OnChooseShortGun(InputAction.CallbackContext context);
        void OnChooseRifle(InputAction.CallbackContext context);
    }
    public interface IInterfaceActions
    {
        void OnPaused(InputAction.CallbackContext context);
        void OnWeaponPanel(InputAction.CallbackContext context);
    }
}
