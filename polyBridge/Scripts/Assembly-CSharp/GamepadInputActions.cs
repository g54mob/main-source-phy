using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class GamepadInputActions : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct GameplayActions
	{
		private GamepadInputActions m_Wrapper;

		public InputAction ButtonSouth => m_Wrapper.m_Gameplay_ButtonSouth;

		public InputAction ButtonNorth => m_Wrapper.m_Gameplay_ButtonNorth;

		public InputAction ButtonWest => m_Wrapper.m_Gameplay_ButtonWest;

		public InputAction ButtonEast => m_Wrapper.m_Gameplay_ButtonEast;

		public InputAction DpadDown => m_Wrapper.m_Gameplay_DpadDown;

		public InputAction DpadUp => m_Wrapper.m_Gameplay_DpadUp;

		public InputAction DpadLeft => m_Wrapper.m_Gameplay_DpadLeft;

		public InputAction DpadRight => m_Wrapper.m_Gameplay_DpadRight;

		public InputAction ShoulderLeft => m_Wrapper.m_Gameplay_ShoulderLeft;

		public InputAction ShoulderRight => m_Wrapper.m_Gameplay_ShoulderRight;

		public InputAction TriggerLeft => m_Wrapper.m_Gameplay_TriggerLeft;

		public InputAction TriggerRight => m_Wrapper.m_Gameplay_TriggerRight;

		public InputAction Select => m_Wrapper.m_Gameplay_Select;

		public InputAction Start => m_Wrapper.m_Gameplay_Start;

		public InputAction LeftStickButton => m_Wrapper.m_Gameplay_LeftStickButton;

		public InputAction RightStickButton => m_Wrapper.m_Gameplay_RightStickButton;

		public InputAction LeftStick => m_Wrapper.m_Gameplay_LeftStick;

		public InputAction RightStick => m_Wrapper.m_Gameplay_RightStick;

		public bool enabled => Get().enabled;

		public GameplayActions(GamepadInputActions wrapper)
		{
			m_Wrapper = wrapper;
		}

		public InputActionMap Get()
		{
			return m_Wrapper.m_Gameplay;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(GameplayActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(IGameplayActions instance)
		{
			if (instance != null && !m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
				ButtonSouth.started += instance.OnButtonSouth;
				ButtonSouth.performed += instance.OnButtonSouth;
				ButtonSouth.canceled += instance.OnButtonSouth;
				ButtonNorth.started += instance.OnButtonNorth;
				ButtonNorth.performed += instance.OnButtonNorth;
				ButtonNorth.canceled += instance.OnButtonNorth;
				ButtonWest.started += instance.OnButtonWest;
				ButtonWest.performed += instance.OnButtonWest;
				ButtonWest.canceled += instance.OnButtonWest;
				ButtonEast.started += instance.OnButtonEast;
				ButtonEast.performed += instance.OnButtonEast;
				ButtonEast.canceled += instance.OnButtonEast;
				DpadDown.started += instance.OnDpadDown;
				DpadDown.performed += instance.OnDpadDown;
				DpadDown.canceled += instance.OnDpadDown;
				DpadUp.started += instance.OnDpadUp;
				DpadUp.performed += instance.OnDpadUp;
				DpadUp.canceled += instance.OnDpadUp;
				DpadLeft.started += instance.OnDpadLeft;
				DpadLeft.performed += instance.OnDpadLeft;
				DpadLeft.canceled += instance.OnDpadLeft;
				DpadRight.started += instance.OnDpadRight;
				DpadRight.performed += instance.OnDpadRight;
				DpadRight.canceled += instance.OnDpadRight;
				ShoulderLeft.started += instance.OnShoulderLeft;
				ShoulderLeft.performed += instance.OnShoulderLeft;
				ShoulderLeft.canceled += instance.OnShoulderLeft;
				ShoulderRight.started += instance.OnShoulderRight;
				ShoulderRight.performed += instance.OnShoulderRight;
				ShoulderRight.canceled += instance.OnShoulderRight;
				TriggerLeft.started += instance.OnTriggerLeft;
				TriggerLeft.performed += instance.OnTriggerLeft;
				TriggerLeft.canceled += instance.OnTriggerLeft;
				TriggerRight.started += instance.OnTriggerRight;
				TriggerRight.performed += instance.OnTriggerRight;
				TriggerRight.canceled += instance.OnTriggerRight;
				Select.started += instance.OnSelect;
				Select.performed += instance.OnSelect;
				Select.canceled += instance.OnSelect;
				Start.started += instance.OnStart;
				Start.performed += instance.OnStart;
				Start.canceled += instance.OnStart;
				LeftStickButton.started += instance.OnLeftStickButton;
				LeftStickButton.performed += instance.OnLeftStickButton;
				LeftStickButton.canceled += instance.OnLeftStickButton;
				RightStickButton.started += instance.OnRightStickButton;
				RightStickButton.performed += instance.OnRightStickButton;
				RightStickButton.canceled += instance.OnRightStickButton;
				LeftStick.started += instance.OnLeftStick;
				LeftStick.performed += instance.OnLeftStick;
				LeftStick.canceled += instance.OnLeftStick;
				RightStick.started += instance.OnRightStick;
				RightStick.performed += instance.OnRightStick;
				RightStick.canceled += instance.OnRightStick;
			}
		}

		private void UnregisterCallbacks(IGameplayActions instance)
		{
			ButtonSouth.started -= instance.OnButtonSouth;
			ButtonSouth.performed -= instance.OnButtonSouth;
			ButtonSouth.canceled -= instance.OnButtonSouth;
			ButtonNorth.started -= instance.OnButtonNorth;
			ButtonNorth.performed -= instance.OnButtonNorth;
			ButtonNorth.canceled -= instance.OnButtonNorth;
			ButtonWest.started -= instance.OnButtonWest;
			ButtonWest.performed -= instance.OnButtonWest;
			ButtonWest.canceled -= instance.OnButtonWest;
			ButtonEast.started -= instance.OnButtonEast;
			ButtonEast.performed -= instance.OnButtonEast;
			ButtonEast.canceled -= instance.OnButtonEast;
			DpadDown.started -= instance.OnDpadDown;
			DpadDown.performed -= instance.OnDpadDown;
			DpadDown.canceled -= instance.OnDpadDown;
			DpadUp.started -= instance.OnDpadUp;
			DpadUp.performed -= instance.OnDpadUp;
			DpadUp.canceled -= instance.OnDpadUp;
			DpadLeft.started -= instance.OnDpadLeft;
			DpadLeft.performed -= instance.OnDpadLeft;
			DpadLeft.canceled -= instance.OnDpadLeft;
			DpadRight.started -= instance.OnDpadRight;
			DpadRight.performed -= instance.OnDpadRight;
			DpadRight.canceled -= instance.OnDpadRight;
			ShoulderLeft.started -= instance.OnShoulderLeft;
			ShoulderLeft.performed -= instance.OnShoulderLeft;
			ShoulderLeft.canceled -= instance.OnShoulderLeft;
			ShoulderRight.started -= instance.OnShoulderRight;
			ShoulderRight.performed -= instance.OnShoulderRight;
			ShoulderRight.canceled -= instance.OnShoulderRight;
			TriggerLeft.started -= instance.OnTriggerLeft;
			TriggerLeft.performed -= instance.OnTriggerLeft;
			TriggerLeft.canceled -= instance.OnTriggerLeft;
			TriggerRight.started -= instance.OnTriggerRight;
			TriggerRight.performed -= instance.OnTriggerRight;
			TriggerRight.canceled -= instance.OnTriggerRight;
			Select.started -= instance.OnSelect;
			Select.performed -= instance.OnSelect;
			Select.canceled -= instance.OnSelect;
			Start.started -= instance.OnStart;
			Start.performed -= instance.OnStart;
			Start.canceled -= instance.OnStart;
			LeftStickButton.started -= instance.OnLeftStickButton;
			LeftStickButton.performed -= instance.OnLeftStickButton;
			LeftStickButton.canceled -= instance.OnLeftStickButton;
			RightStickButton.started -= instance.OnRightStickButton;
			RightStickButton.performed -= instance.OnRightStickButton;
			RightStickButton.canceled -= instance.OnRightStickButton;
			LeftStick.started -= instance.OnLeftStick;
			LeftStick.performed -= instance.OnLeftStick;
			LeftStick.canceled -= instance.OnLeftStick;
			RightStick.started -= instance.OnRightStick;
			RightStick.performed -= instance.OnRightStick;
			RightStick.canceled -= instance.OnRightStick;
		}

		public void RemoveCallbacks(IGameplayActions instance)
		{
			if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(IGameplayActions instance)
		{
			foreach (IGameplayActions gameplayActionsCallbackInterface in m_Wrapper.m_GameplayActionsCallbackInterfaces)
			{
				UnregisterCallbacks(gameplayActionsCallbackInterface);
			}
			m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public interface IGameplayActions
	{
		void OnButtonSouth(InputAction.CallbackContext context);

		void OnButtonNorth(InputAction.CallbackContext context);

		void OnButtonWest(InputAction.CallbackContext context);

		void OnButtonEast(InputAction.CallbackContext context);

		void OnDpadDown(InputAction.CallbackContext context);

		void OnDpadUp(InputAction.CallbackContext context);

		void OnDpadLeft(InputAction.CallbackContext context);

		void OnDpadRight(InputAction.CallbackContext context);

		void OnShoulderLeft(InputAction.CallbackContext context);

		void OnShoulderRight(InputAction.CallbackContext context);

		void OnTriggerLeft(InputAction.CallbackContext context);

		void OnTriggerRight(InputAction.CallbackContext context);

		void OnSelect(InputAction.CallbackContext context);

		void OnStart(InputAction.CallbackContext context);

		void OnLeftStickButton(InputAction.CallbackContext context);

		void OnRightStickButton(InputAction.CallbackContext context);

		void OnLeftStick(InputAction.CallbackContext context);

		void OnRightStick(InputAction.CallbackContext context);
	}

	private readonly InputActionMap m_Gameplay;

	private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();

	private readonly InputAction m_Gameplay_ButtonSouth;

	private readonly InputAction m_Gameplay_ButtonNorth;

	private readonly InputAction m_Gameplay_ButtonWest;

	private readonly InputAction m_Gameplay_ButtonEast;

	private readonly InputAction m_Gameplay_DpadDown;

	private readonly InputAction m_Gameplay_DpadUp;

	private readonly InputAction m_Gameplay_DpadLeft;

	private readonly InputAction m_Gameplay_DpadRight;

	private readonly InputAction m_Gameplay_ShoulderLeft;

	private readonly InputAction m_Gameplay_ShoulderRight;

	private readonly InputAction m_Gameplay_TriggerLeft;

	private readonly InputAction m_Gameplay_TriggerRight;

	private readonly InputAction m_Gameplay_Select;

	private readonly InputAction m_Gameplay_Start;

	private readonly InputAction m_Gameplay_LeftStickButton;

	private readonly InputAction m_Gameplay_RightStickButton;

	private readonly InputAction m_Gameplay_LeftStick;

	private readonly InputAction m_Gameplay_RightStick;

	public InputActionAsset asset { get; }

	public InputBinding? bindingMask
	{
		get
		{
			return asset.bindingMask;
		}
		set
		{
			asset.bindingMask = value;
		}
	}

	public ReadOnlyArray<InputDevice>? devices
	{
		get
		{
			return asset.devices;
		}
		set
		{
			asset.devices = value;
		}
	}

	public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

	public IEnumerable<InputBinding> bindings => asset.bindings;

	public GameplayActions Gameplay => new GameplayActions(this);

	public GamepadInputActions()
	{
		asset = InputActionAsset.FromJson("{\r\n    \"name\": \"GamepadInputActions\",\r\n    \"maps\": [\r\n        {\r\n            \"name\": \"Gameplay\",\r\n            \"id\": \"4bab5278-95d3-4540-99e8-f494a394470c\",\r\n            \"actions\": [\r\n                {\r\n                    \"name\": \"ButtonSouth\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"b2def3fc-4691-4777-b8b2-b66923a2af46\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"ButtonNorth\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"80b80674-6ada-42aa-b264-071d2e560359\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"ButtonWest\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"65e21bd9-1159-401d-9367-96de3b13722c\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"ButtonEast\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"481bf0cf-282d-43ae-80e7-9d2647ed1cd8\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"DpadDown\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"af132b86-2b45-4b85-bec8-f5e186c3ee29\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"DpadUp\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"19640614-8c27-4423-9493-f973b135dff7\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"DpadLeft\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"510d62cd-9476-490f-8f06-1117458f1b85\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"DpadRight\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"57633155-99d4-4523-a398-1ed373ea4649\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"ShoulderLeft\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"0ca69e4e-0b0a-49ba-ad7a-8aae8792124e\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"ShoulderRight\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"49bebc07-7682-44c9-80db-67aa6e6f6734\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"TriggerLeft\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"2f86e7ec-2147-4c82-847c-db16f395582d\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"TriggerRight\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"5e687342-3001-42e6-b1f8-0444bd8b72d4\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Select\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"da6af8f3-8931-47e0-b5c3-86be4506ca9f\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"Start\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"d7180d3d-023f-4ab2-85d9-b1f636e34e78\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"LeftStickButton\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"eaf9f457-d7ae-4f94-a0e2-e1e9664817bd\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"RightStickButton\",\r\n                    \"type\": \"Button\",\r\n                    \"id\": \"83e09ac6-ec56-405b-9b81-66b8711103a0\",\r\n                    \"expectedControlType\": \"Button\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": false\r\n                },\r\n                {\r\n                    \"name\": \"LeftStick\",\r\n                    \"type\": \"Value\",\r\n                    \"id\": \"c9acb92a-66db-4ef6-9d12-baceb6a1e368\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                },\r\n                {\r\n                    \"name\": \"RightStick\",\r\n                    \"type\": \"Value\",\r\n                    \"id\": \"e3872402-adfa-4734-b1f6-7adcbfb66f4e\",\r\n                    \"expectedControlType\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"interactions\": \"\",\r\n                    \"initialStateCheck\": true\r\n                }\r\n            ],\r\n            \"bindings\": [\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"b94aea66-df97-4519-a30c-9e69b7d79a5f\",\r\n                    \"path\": \"<Gamepad>/buttonSouth\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"ButtonSouth\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"e58bae88-5b16-47ce-bb8d-f6f01b46d39b\",\r\n                    \"path\": \"<Gamepad>/buttonNorth\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"ButtonNorth\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"48b41e62-e5f4-457f-8cde-4c642f921d2f\",\r\n                    \"path\": \"<Gamepad>/buttonWest\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"ButtonWest\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"4001e48b-f0ff-4d60-915a-b240e015d51d\",\r\n                    \"path\": \"<Gamepad>/buttonEast\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"ButtonEast\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"d78ccf75-70bd-4ee4-9725-89873e831f72\",\r\n                    \"path\": \"<Gamepad>/dpad/down\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"DpadDown\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"60b3a460-6c78-4944-aa0d-25ae440ba7cf\",\r\n                    \"path\": \"<Gamepad>/dpad/up\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"DpadUp\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"e686cca8-b662-4731-b730-725f07eae2fd\",\r\n                    \"path\": \"<Gamepad>/dpad/left\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"DpadLeft\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"969d46fe-1060-4673-8e7c-54df7d0d632e\",\r\n                    \"path\": \"<Gamepad>/dpad/right\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"DpadRight\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"b4f7a95c-5ec9-4ae3-b1f8-f3774b2ff62a\",\r\n                    \"path\": \"<Gamepad>/leftShoulder\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"ShoulderLeft\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"eabd1587-31fd-453f-9903-b0a176203ba0\",\r\n                    \"path\": \"<Gamepad>/rightShoulder\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"ShoulderRight\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"48e626a5-6d40-428a-9723-f3d3a5c48b65\",\r\n                    \"path\": \"<Gamepad>/leftTrigger\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"TriggerLeft\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"f563543a-cbd6-48c4-9efe-5ee129baae71\",\r\n                    \"path\": \"<Gamepad>/rightTrigger\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"TriggerRight\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"38e3ff8c-a061-4d54-a8f1-040ce36b3b75\",\r\n                    \"path\": \"<Gamepad>/select\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Select\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"ccbc048d-0ea8-4696-9121-60b0b19d244f\",\r\n                    \"path\": \"<Gamepad>/start\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"Start\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"5509b053-d945-49be-92f7-bc11220d7091\",\r\n                    \"path\": \"<Gamepad>/leftStickPress\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"LeftStickButton\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"f627931d-36de-4e26-9eb1-a3a714e2af90\",\r\n                    \"path\": \"<Gamepad>/rightStickPress\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"RightStickButton\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"ee10ebbd-2d95-435f-847c-85dff041740c\",\r\n                    \"path\": \"<Gamepad>/leftStick\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"LeftStick\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                },\r\n                {\r\n                    \"name\": \"\",\r\n                    \"id\": \"6f3ed8e6-9845-4a01-add9-5b6bed7896ce\",\r\n                    \"path\": \"<Gamepad>/rightStick\",\r\n                    \"interactions\": \"\",\r\n                    \"processors\": \"\",\r\n                    \"groups\": \"\",\r\n                    \"action\": \"RightStick\",\r\n                    \"isComposite\": false,\r\n                    \"isPartOfComposite\": false\r\n                }\r\n            ]\r\n        }\r\n    ],\r\n    \"controlSchemes\": []\r\n}");
		m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
		m_Gameplay_ButtonSouth = m_Gameplay.FindAction("ButtonSouth", throwIfNotFound: true);
		m_Gameplay_ButtonNorth = m_Gameplay.FindAction("ButtonNorth", throwIfNotFound: true);
		m_Gameplay_ButtonWest = m_Gameplay.FindAction("ButtonWest", throwIfNotFound: true);
		m_Gameplay_ButtonEast = m_Gameplay.FindAction("ButtonEast", throwIfNotFound: true);
		m_Gameplay_DpadDown = m_Gameplay.FindAction("DpadDown", throwIfNotFound: true);
		m_Gameplay_DpadUp = m_Gameplay.FindAction("DpadUp", throwIfNotFound: true);
		m_Gameplay_DpadLeft = m_Gameplay.FindAction("DpadLeft", throwIfNotFound: true);
		m_Gameplay_DpadRight = m_Gameplay.FindAction("DpadRight", throwIfNotFound: true);
		m_Gameplay_ShoulderLeft = m_Gameplay.FindAction("ShoulderLeft", throwIfNotFound: true);
		m_Gameplay_ShoulderRight = m_Gameplay.FindAction("ShoulderRight", throwIfNotFound: true);
		m_Gameplay_TriggerLeft = m_Gameplay.FindAction("TriggerLeft", throwIfNotFound: true);
		m_Gameplay_TriggerRight = m_Gameplay.FindAction("TriggerRight", throwIfNotFound: true);
		m_Gameplay_Select = m_Gameplay.FindAction("Select", throwIfNotFound: true);
		m_Gameplay_Start = m_Gameplay.FindAction("Start", throwIfNotFound: true);
		m_Gameplay_LeftStickButton = m_Gameplay.FindAction("LeftStickButton", throwIfNotFound: true);
		m_Gameplay_RightStickButton = m_Gameplay.FindAction("RightStickButton", throwIfNotFound: true);
		m_Gameplay_LeftStick = m_Gameplay.FindAction("LeftStick", throwIfNotFound: true);
		m_Gameplay_RightStick = m_Gameplay.FindAction("RightStick", throwIfNotFound: true);
	}

	~GamepadInputActions()
	{
	}

	public void Dispose()
	{
		UnityEngine.Object.Destroy(asset);
	}

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

	public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
	{
		return asset.FindAction(actionNameOrId, throwIfNotFound);
	}

	public int FindBinding(InputBinding bindingMask, out InputAction action)
	{
		return asset.FindBinding(bindingMask, out action);
	}
}
