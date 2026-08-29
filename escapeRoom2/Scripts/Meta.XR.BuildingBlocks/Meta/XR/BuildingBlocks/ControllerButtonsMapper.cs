using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Meta.XR.BuildingBlocks
{
	public class ControllerButtonsMapper : MonoBehaviour
	{
		[Serializable]
		public struct ButtonClickAction
		{
			public enum ButtonClickMode
			{
				OnButtonUp = 0,
				OnButtonDown = 1,
				OnButton = 2
			}

			public string Title;

			public OVRInput.Button Button;

			public ButtonClickMode ButtonMode;

			public InputActionReference InputActionReference;

			public UnityEvent Callback;
		}

		[SerializeField]
		private List<ButtonClickAction> _buttonClickActions;

		internal const bool UseNewInputSystem = true;

		internal const bool UseLegacyInputSystem = true;

		public List<ButtonClickAction> ButtonClickActions
		{
			get
			{
				return _buttonClickActions;
			}
			set
			{
				_buttonClickActions = value;
			}
		}

		private void OnEnable()
		{
			foreach (ButtonClickAction buttonClickAction in ButtonClickActions)
			{
				buttonClickAction.InputActionReference?.action.Enable();
			}
		}

		private void OnDisable()
		{
			foreach (ButtonClickAction buttonClickAction in ButtonClickActions)
			{
				buttonClickAction.InputActionReference?.action.Disable();
			}
		}

		private void Update()
		{
			foreach (ButtonClickAction buttonClickAction in ButtonClickActions)
			{
				if (IsActionTriggered(buttonClickAction))
				{
					buttonClickAction.Callback?.Invoke();
				}
			}
		}

		private static bool IsActionTriggered(ButtonClickAction buttonClickAction)
		{
			if (!IsLegacyInputActionTriggered(buttonClickAction.ButtonMode, buttonClickAction.Button))
			{
				return IsNewInputSystemActionTriggered(buttonClickAction);
			}
			return true;
		}

		private static bool IsLegacyInputActionTriggered(ButtonClickAction.ButtonClickMode buttonMode, OVRInput.Button button)
		{
			if (button == OVRInput.Button.None)
			{
				return false;
			}
			return buttonMode switch
			{
				ButtonClickAction.ButtonClickMode.OnButtonUp => OVRInput.GetUp(button), 
				ButtonClickAction.ButtonClickMode.OnButtonDown => OVRInput.GetDown(button), 
				ButtonClickAction.ButtonClickMode.OnButton => OVRInput.Get(button), 
				_ => false, 
			};
		}

		private static bool IsNewInputSystemActionTriggered(ButtonClickAction buttonClickAction)
		{
			if (buttonClickAction.InputActionReference != null)
			{
				return buttonClickAction.InputActionReference.action.triggered;
			}
			return false;
		}
	}
}
