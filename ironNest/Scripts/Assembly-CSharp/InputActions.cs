using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct PlayerActions
	{
		private InputActions m_Wrapper;

		public InputAction Move => null;

		public InputAction Look => null;

		public InputAction Fire => null;

		public InputAction Jump => null;

		public InputAction Sprint => null;

		public InputAction Crouch => null;

		public InputAction Activate => null;

		public InputAction Freecam => null;

		public bool enabled => false;

		public PlayerActions(InputActions wrapper)
		{
			m_Wrapper = null;
		}

		public InputActionMap Get()
		{
			return null;
		}

		public void Enable()
		{
		}

		public void Disable()
		{
		}

		public static implicit operator InputActionMap(PlayerActions set)
		{
			return null;
		}

		public void AddCallbacks(IPlayerActions instance)
		{
		}

		private void UnregisterCallbacks(IPlayerActions instance)
		{
		}

		public void RemoveCallbacks(IPlayerActions instance)
		{
		}

		public void SetCallbacks(IPlayerActions instance)
		{
		}
	}

	public struct UIActions
	{
		private InputActions m_Wrapper;

		public InputAction Click => null;

		public InputAction Point => null;

		public InputAction Navigate => null;

		public InputAction MoveUI => null;

		public InputAction Submit => null;

		public InputAction Cancel => null;

		public InputAction ScrollWheel => null;

		public InputAction MiddleClick => null;

		public InputAction TrackedDevicePosition => null;

		public InputAction TrackedDeviceOrientation => null;

		public InputAction Up => null;

		public InputAction Down => null;

		public bool enabled => false;

		public UIActions(InputActions wrapper)
		{
			m_Wrapper = null;
		}

		public InputActionMap Get()
		{
			return null;
		}

		public void Enable()
		{
		}

		public void Disable()
		{
		}

		public static implicit operator InputActionMap(UIActions set)
		{
			return null;
		}

		public void AddCallbacks(IUIActions instance)
		{
		}

		private void UnregisterCallbacks(IUIActions instance)
		{
		}

		public void RemoveCallbacks(IUIActions instance)
		{
		}

		public void SetCallbacks(IUIActions instance)
		{
		}
	}

	public struct UniversalActions
	{
		private InputActions m_Wrapper;

		public InputAction PointerDelta => null;

		public InputAction Navigate => null;

		public InputAction PointerPosition => null;

		public InputAction PrimaryClick => null;

		public InputAction SecondaryClick => null;

		public InputAction Tertiaryclick => null;

		public InputAction ToggleClipboard => null;

		public InputAction FocuseClipboard => null;

		public InputAction Escape => null;

		public InputAction FreecamScrollWheel => null;

		public InputAction UnequipGasmask => null;

		public InputAction CinamaticHideCursorToggle => null;

		public InputAction CinamaticAutoReload => null;

		public InputAction CinamaticLightSwitch => null;

		public InputAction CinamaticSwingForce => null;

		public InputAction CheatRevealallonmap => null;

		public InputAction CheatImpactF9 => null;

		public InputAction CheatImpactF10 => null;

		public InputAction CheatImpactF11 => null;

		public InputAction RotateLeft => null;

		public InputAction RotateRight => null;

		public InputAction Cinamatic4kScreenshot => null;

		public InputAction ContinueEnter => null;

		public InputAction PickUp => null;

		public InputAction Interact => null;

		public InputAction SlowCursor => null;

		public bool enabled => false;

		public UniversalActions(InputActions wrapper)
		{
			m_Wrapper = null;
		}

		public InputActionMap Get()
		{
			return null;
		}

		public void Enable()
		{
		}

		public void Disable()
		{
		}

		public static implicit operator InputActionMap(UniversalActions set)
		{
			return null;
		}

		public void AddCallbacks(IUniversalActions instance)
		{
		}

		private void UnregisterCallbacks(IUniversalActions instance)
		{
		}

		public void RemoveCallbacks(IUniversalActions instance)
		{
		}

		public void SetCallbacks(IUniversalActions instance)
		{
		}
	}

	public interface IPlayerActions
	{
		void OnMove(InputAction.CallbackContext context);

		void OnLook(InputAction.CallbackContext context);

		void OnFire(InputAction.CallbackContext context);

		void OnJump(InputAction.CallbackContext context);

		void OnSprint(InputAction.CallbackContext context);

		void OnCrouch(InputAction.CallbackContext context);

		void OnActivate(InputAction.CallbackContext context);

		void OnFreecam(InputAction.CallbackContext context);
	}

	public interface IUIActions
	{
		void OnClick(InputAction.CallbackContext context);

		void OnPoint(InputAction.CallbackContext context);

		void OnNavigate(InputAction.CallbackContext context);

		void OnMoveUI(InputAction.CallbackContext context);

		void OnSubmit(InputAction.CallbackContext context);

		void OnCancel(InputAction.CallbackContext context);

		void OnScrollWheel(InputAction.CallbackContext context);

		void OnMiddleClick(InputAction.CallbackContext context);

		void OnTrackedDevicePosition(InputAction.CallbackContext context);

		void OnTrackedDeviceOrientation(InputAction.CallbackContext context);

		void OnUp(InputAction.CallbackContext context);

		void OnDown(InputAction.CallbackContext context);
	}

	public interface IUniversalActions
	{
		void OnPointerDelta(InputAction.CallbackContext context);

		void OnNavigate(InputAction.CallbackContext context);

		void OnPointerPosition(InputAction.CallbackContext context);

		void OnPrimaryClick(InputAction.CallbackContext context);

		void OnSecondaryClick(InputAction.CallbackContext context);

		void OnTertiaryclick(InputAction.CallbackContext context);

		void OnToggleClipboard(InputAction.CallbackContext context);

		void OnFocuseClipboard(InputAction.CallbackContext context);

		void OnEscape(InputAction.CallbackContext context);

		void OnFreecamScrollWheel(InputAction.CallbackContext context);

		void OnUnequipGasmask(InputAction.CallbackContext context);

		void OnCinamaticHideCursorToggle(InputAction.CallbackContext context);

		void OnCinamaticAutoReload(InputAction.CallbackContext context);

		void OnCinamaticLightSwitch(InputAction.CallbackContext context);

		void OnCinamaticSwingForce(InputAction.CallbackContext context);

		void OnCheatRevealallonmap(InputAction.CallbackContext context);

		void OnCheatImpactF9(InputAction.CallbackContext context);

		void OnCheatImpactF10(InputAction.CallbackContext context);

		void OnCheatImpactF11(InputAction.CallbackContext context);

		void OnRotateLeft(InputAction.CallbackContext context);

		void OnRotateRight(InputAction.CallbackContext context);

		void OnCinamatic4kScreenshot(InputAction.CallbackContext context);

		void OnContinueEnter(InputAction.CallbackContext context);

		void OnPickUp(InputAction.CallbackContext context);

		void OnInteract(InputAction.CallbackContext context);

		void OnSlowCursor(InputAction.CallbackContext context);
	}

	private readonly InputActionMap m_Player;

	private List<IPlayerActions> m_PlayerActionsCallbackInterfaces;

	private readonly InputAction m_Player_Move;

	private readonly InputAction m_Player_Look;

	private readonly InputAction m_Player_Fire;

	private readonly InputAction m_Player_Jump;

	private readonly InputAction m_Player_Sprint;

	private readonly InputAction m_Player_Crouch;

	private readonly InputAction m_Player_Activate;

	private readonly InputAction m_Player_Freecam;

	private readonly InputActionMap m_UI;

	private List<IUIActions> m_UIActionsCallbackInterfaces;

	private readonly InputAction m_UI_Click;

	private readonly InputAction m_UI_Point;

	private readonly InputAction m_UI_Navigate;

	private readonly InputAction m_UI_MoveUI;

	private readonly InputAction m_UI_Submit;

	private readonly InputAction m_UI_Cancel;

	private readonly InputAction m_UI_ScrollWheel;

	private readonly InputAction m_UI_MiddleClick;

	private readonly InputAction m_UI_TrackedDevicePosition;

	private readonly InputAction m_UI_TrackedDeviceOrientation;

	private readonly InputAction m_UI_Up;

	private readonly InputAction m_UI_Down;

	private readonly InputActionMap m_Universal;

	private List<IUniversalActions> m_UniversalActionsCallbackInterfaces;

	private readonly InputAction m_Universal_PointerDelta;

	private readonly InputAction m_Universal_Navigate;

	private readonly InputAction m_Universal_PointerPosition;

	private readonly InputAction m_Universal_PrimaryClick;

	private readonly InputAction m_Universal_SecondaryClick;

	private readonly InputAction m_Universal_Tertiaryclick;

	private readonly InputAction m_Universal_ToggleClipboard;

	private readonly InputAction m_Universal_FocuseClipboard;

	private readonly InputAction m_Universal_Escape;

	private readonly InputAction m_Universal_FreecamScrollWheel;

	private readonly InputAction m_Universal_UnequipGasmask;

	private readonly InputAction m_Universal_CinamaticHideCursorToggle;

	private readonly InputAction m_Universal_CinamaticAutoReload;

	private readonly InputAction m_Universal_CinamaticLightSwitch;

	private readonly InputAction m_Universal_CinamaticSwingForce;

	private readonly InputAction m_Universal_CheatRevealallonmap;

	private readonly InputAction m_Universal_CheatImpactF9;

	private readonly InputAction m_Universal_CheatImpactF10;

	private readonly InputAction m_Universal_CheatImpactF11;

	private readonly InputAction m_Universal_RotateLeft;

	private readonly InputAction m_Universal_RotateRight;

	private readonly InputAction m_Universal_Cinamatic4kScreenshot;

	private readonly InputAction m_Universal_ContinueEnter;

	private readonly InputAction m_Universal_PickUp;

	private readonly InputAction m_Universal_Interact;

	private readonly InputAction m_Universal_SlowCursor;

	private int m_KeyboardMouseSchemeIndex;

	private int m_GamepadSchemeIndex;

	private int m_TouchSchemeIndex;

	private int m_JoystickSchemeIndex;

	private int m_XRSchemeIndex;

	public InputActionAsset asset { get; }

	public InputBinding? bindingMask
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ReadOnlyArray<InputControlScheme> controlSchemes => default(ReadOnlyArray<InputControlScheme>);

	public IEnumerable<InputBinding> bindings => null;

	public PlayerActions Player => default(PlayerActions);

	public UIActions UI => default(UIActions);

	public UniversalActions Universal => default(UniversalActions);

	public InputControlScheme KeyboardMouseScheme => default(InputControlScheme);

	public InputControlScheme GamepadScheme => default(InputControlScheme);

	public InputControlScheme TouchScheme => default(InputControlScheme);

	public InputControlScheme JoystickScheme => default(InputControlScheme);

	public InputControlScheme XRScheme => default(InputControlScheme);

	~InputActions()
	{
	}

	public void Dispose()
	{
	}

	public bool Contains(InputAction action)
	{
		return false;
	}

	public IEnumerator<InputAction> GetEnumerator()
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public void Enable()
	{
	}

	public void Disable()
	{
	}

	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return null;
	}

	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		action = null;
		return 0;
	}
}
