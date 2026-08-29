using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class VRInput : IInputActionCollection2, IInputActionCollection, IEnumerable<InputAction>, IEnumerable, IDisposable
{
	public struct LeftHandActions
	{
		private VRInput m_Wrapper;

		public InputAction Trigger => m_Wrapper.m_LeftHand_Trigger;

		public InputAction Grip => m_Wrapper.m_LeftHand_Grip;

		public InputAction PrimaryButton => m_Wrapper.m_LeftHand_PrimaryButton;

		public InputAction PrimaryTouch => m_Wrapper.m_LeftHand_PrimaryTouch;

		public InputAction SecondaryButton => m_Wrapper.m_LeftHand_SecondaryButton;

		public InputAction SecondaryTouch => m_Wrapper.m_LeftHand_SecondaryTouch;

		public InputAction Primary2DAxis => m_Wrapper.m_LeftHand_Primary2DAxis;

		public InputAction Primary2DAxisClick => m_Wrapper.m_LeftHand_Primary2DAxisClick;

		public InputAction Primary2DAxisTouch => m_Wrapper.m_LeftHand_Primary2DAxisTouch;

		public InputAction Secondary2DAxis => m_Wrapper.m_LeftHand_Secondary2DAxis;

		public InputAction Secondary2DAxisClick => m_Wrapper.m_LeftHand_Secondary2DAxisClick;

		public InputAction Secondary2DAxisTouch => m_Wrapper.m_LeftHand_Secondary2DAxisTouch;

		public InputAction PointerPosition => m_Wrapper.m_LeftHand_PointerPosition;

		public InputAction PointerPositionQuest => m_Wrapper.m_LeftHand_PointerPositionQuest;

		public InputAction PointerRotation => m_Wrapper.m_LeftHand_PointerRotation;

		public InputAction PointerRotationQuest => m_Wrapper.m_LeftHand_PointerRotationQuest;

		public InputAction RawPosition => m_Wrapper.m_LeftHand_RawPosition;

		public InputAction RawRotation => m_Wrapper.m_LeftHand_RawRotation;

		public InputAction Haptic => m_Wrapper.m_LeftHand_Haptic;

		public InputAction Scroll => m_Wrapper.m_LeftHand_Scroll;

		public InputAction Menu => m_Wrapper.m_LeftHand_Menu;

		public bool enabled => Get().enabled;

		public LeftHandActions(VRInput wrapper)
		{
			m_Wrapper = wrapper;
		}

		public InputActionMap Get()
		{
			return m_Wrapper.m_LeftHand;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(LeftHandActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(ILeftHandActions instance)
		{
			if (instance != null && !m_Wrapper.m_LeftHandActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_LeftHandActionsCallbackInterfaces.Add(instance);
				Trigger.started += instance.OnTrigger;
				Trigger.performed += instance.OnTrigger;
				Trigger.canceled += instance.OnTrigger;
				Grip.started += instance.OnGrip;
				Grip.performed += instance.OnGrip;
				Grip.canceled += instance.OnGrip;
				PrimaryButton.started += instance.OnPrimaryButton;
				PrimaryButton.performed += instance.OnPrimaryButton;
				PrimaryButton.canceled += instance.OnPrimaryButton;
				PrimaryTouch.started += instance.OnPrimaryTouch;
				PrimaryTouch.performed += instance.OnPrimaryTouch;
				PrimaryTouch.canceled += instance.OnPrimaryTouch;
				SecondaryButton.started += instance.OnSecondaryButton;
				SecondaryButton.performed += instance.OnSecondaryButton;
				SecondaryButton.canceled += instance.OnSecondaryButton;
				SecondaryTouch.started += instance.OnSecondaryTouch;
				SecondaryTouch.performed += instance.OnSecondaryTouch;
				SecondaryTouch.canceled += instance.OnSecondaryTouch;
				Primary2DAxis.started += instance.OnPrimary2DAxis;
				Primary2DAxis.performed += instance.OnPrimary2DAxis;
				Primary2DAxis.canceled += instance.OnPrimary2DAxis;
				Primary2DAxisClick.started += instance.OnPrimary2DAxisClick;
				Primary2DAxisClick.performed += instance.OnPrimary2DAxisClick;
				Primary2DAxisClick.canceled += instance.OnPrimary2DAxisClick;
				Primary2DAxisTouch.started += instance.OnPrimary2DAxisTouch;
				Primary2DAxisTouch.performed += instance.OnPrimary2DAxisTouch;
				Primary2DAxisTouch.canceled += instance.OnPrimary2DAxisTouch;
				Secondary2DAxis.started += instance.OnSecondary2DAxis;
				Secondary2DAxis.performed += instance.OnSecondary2DAxis;
				Secondary2DAxis.canceled += instance.OnSecondary2DAxis;
				Secondary2DAxisClick.started += instance.OnSecondary2DAxisClick;
				Secondary2DAxisClick.performed += instance.OnSecondary2DAxisClick;
				Secondary2DAxisClick.canceled += instance.OnSecondary2DAxisClick;
				Secondary2DAxisTouch.started += instance.OnSecondary2DAxisTouch;
				Secondary2DAxisTouch.performed += instance.OnSecondary2DAxisTouch;
				Secondary2DAxisTouch.canceled += instance.OnSecondary2DAxisTouch;
				PointerPosition.started += instance.OnPointerPosition;
				PointerPosition.performed += instance.OnPointerPosition;
				PointerPosition.canceled += instance.OnPointerPosition;
				PointerPositionQuest.started += instance.OnPointerPositionQuest;
				PointerPositionQuest.performed += instance.OnPointerPositionQuest;
				PointerPositionQuest.canceled += instance.OnPointerPositionQuest;
				PointerRotation.started += instance.OnPointerRotation;
				PointerRotation.performed += instance.OnPointerRotation;
				PointerRotation.canceled += instance.OnPointerRotation;
				PointerRotationQuest.started += instance.OnPointerRotationQuest;
				PointerRotationQuest.performed += instance.OnPointerRotationQuest;
				PointerRotationQuest.canceled += instance.OnPointerRotationQuest;
				RawPosition.started += instance.OnRawPosition;
				RawPosition.performed += instance.OnRawPosition;
				RawPosition.canceled += instance.OnRawPosition;
				RawRotation.started += instance.OnRawRotation;
				RawRotation.performed += instance.OnRawRotation;
				RawRotation.canceled += instance.OnRawRotation;
				Haptic.started += instance.OnHaptic;
				Haptic.performed += instance.OnHaptic;
				Haptic.canceled += instance.OnHaptic;
				Scroll.started += instance.OnScroll;
				Scroll.performed += instance.OnScroll;
				Scroll.canceled += instance.OnScroll;
				Menu.started += instance.OnMenu;
				Menu.performed += instance.OnMenu;
				Menu.canceled += instance.OnMenu;
			}
		}

		private void UnregisterCallbacks(ILeftHandActions instance)
		{
			Trigger.started -= instance.OnTrigger;
			Trigger.performed -= instance.OnTrigger;
			Trigger.canceled -= instance.OnTrigger;
			Grip.started -= instance.OnGrip;
			Grip.performed -= instance.OnGrip;
			Grip.canceled -= instance.OnGrip;
			PrimaryButton.started -= instance.OnPrimaryButton;
			PrimaryButton.performed -= instance.OnPrimaryButton;
			PrimaryButton.canceled -= instance.OnPrimaryButton;
			PrimaryTouch.started -= instance.OnPrimaryTouch;
			PrimaryTouch.performed -= instance.OnPrimaryTouch;
			PrimaryTouch.canceled -= instance.OnPrimaryTouch;
			SecondaryButton.started -= instance.OnSecondaryButton;
			SecondaryButton.performed -= instance.OnSecondaryButton;
			SecondaryButton.canceled -= instance.OnSecondaryButton;
			SecondaryTouch.started -= instance.OnSecondaryTouch;
			SecondaryTouch.performed -= instance.OnSecondaryTouch;
			SecondaryTouch.canceled -= instance.OnSecondaryTouch;
			Primary2DAxis.started -= instance.OnPrimary2DAxis;
			Primary2DAxis.performed -= instance.OnPrimary2DAxis;
			Primary2DAxis.canceled -= instance.OnPrimary2DAxis;
			Primary2DAxisClick.started -= instance.OnPrimary2DAxisClick;
			Primary2DAxisClick.performed -= instance.OnPrimary2DAxisClick;
			Primary2DAxisClick.canceled -= instance.OnPrimary2DAxisClick;
			Primary2DAxisTouch.started -= instance.OnPrimary2DAxisTouch;
			Primary2DAxisTouch.performed -= instance.OnPrimary2DAxisTouch;
			Primary2DAxisTouch.canceled -= instance.OnPrimary2DAxisTouch;
			Secondary2DAxis.started -= instance.OnSecondary2DAxis;
			Secondary2DAxis.performed -= instance.OnSecondary2DAxis;
			Secondary2DAxis.canceled -= instance.OnSecondary2DAxis;
			Secondary2DAxisClick.started -= instance.OnSecondary2DAxisClick;
			Secondary2DAxisClick.performed -= instance.OnSecondary2DAxisClick;
			Secondary2DAxisClick.canceled -= instance.OnSecondary2DAxisClick;
			Secondary2DAxisTouch.started -= instance.OnSecondary2DAxisTouch;
			Secondary2DAxisTouch.performed -= instance.OnSecondary2DAxisTouch;
			Secondary2DAxisTouch.canceled -= instance.OnSecondary2DAxisTouch;
			PointerPosition.started -= instance.OnPointerPosition;
			PointerPosition.performed -= instance.OnPointerPosition;
			PointerPosition.canceled -= instance.OnPointerPosition;
			PointerPositionQuest.started -= instance.OnPointerPositionQuest;
			PointerPositionQuest.performed -= instance.OnPointerPositionQuest;
			PointerPositionQuest.canceled -= instance.OnPointerPositionQuest;
			PointerRotation.started -= instance.OnPointerRotation;
			PointerRotation.performed -= instance.OnPointerRotation;
			PointerRotation.canceled -= instance.OnPointerRotation;
			PointerRotationQuest.started -= instance.OnPointerRotationQuest;
			PointerRotationQuest.performed -= instance.OnPointerRotationQuest;
			PointerRotationQuest.canceled -= instance.OnPointerRotationQuest;
			RawPosition.started -= instance.OnRawPosition;
			RawPosition.performed -= instance.OnRawPosition;
			RawPosition.canceled -= instance.OnRawPosition;
			RawRotation.started -= instance.OnRawRotation;
			RawRotation.performed -= instance.OnRawRotation;
			RawRotation.canceled -= instance.OnRawRotation;
			Haptic.started -= instance.OnHaptic;
			Haptic.performed -= instance.OnHaptic;
			Haptic.canceled -= instance.OnHaptic;
			Scroll.started -= instance.OnScroll;
			Scroll.performed -= instance.OnScroll;
			Scroll.canceled -= instance.OnScroll;
			Menu.started -= instance.OnMenu;
			Menu.performed -= instance.OnMenu;
			Menu.canceled -= instance.OnMenu;
		}

		public void RemoveCallbacks(ILeftHandActions instance)
		{
			if (m_Wrapper.m_LeftHandActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(ILeftHandActions instance)
		{
			foreach (ILeftHandActions leftHandActionsCallbackInterface in m_Wrapper.m_LeftHandActionsCallbackInterfaces)
			{
				UnregisterCallbacks(leftHandActionsCallbackInterface);
			}
			m_Wrapper.m_LeftHandActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public struct RightHandActions
	{
		private VRInput m_Wrapper;

		public InputAction Trigger => m_Wrapper.m_RightHand_Trigger;

		public InputAction Grip => m_Wrapper.m_RightHand_Grip;

		public InputAction PrimaryButton => m_Wrapper.m_RightHand_PrimaryButton;

		public InputAction PrimaryTouch => m_Wrapper.m_RightHand_PrimaryTouch;

		public InputAction SecondaryButton => m_Wrapper.m_RightHand_SecondaryButton;

		public InputAction SecondaryTouch => m_Wrapper.m_RightHand_SecondaryTouch;

		public InputAction Primary2DAxis => m_Wrapper.m_RightHand_Primary2DAxis;

		public InputAction Primary2DAxisClick => m_Wrapper.m_RightHand_Primary2DAxisClick;

		public InputAction Primary2DAxisTouch => m_Wrapper.m_RightHand_Primary2DAxisTouch;

		public InputAction Secondary2DAxis => m_Wrapper.m_RightHand_Secondary2DAxis;

		public InputAction Secondary2DAxisClick => m_Wrapper.m_RightHand_Secondary2DAxisClick;

		public InputAction Secondary2DAxisTouch => m_Wrapper.m_RightHand_Secondary2DAxisTouch;

		public InputAction PointerPosition => m_Wrapper.m_RightHand_PointerPosition;

		public InputAction PointerPositionQuest => m_Wrapper.m_RightHand_PointerPositionQuest;

		public InputAction PointerRotation => m_Wrapper.m_RightHand_PointerRotation;

		public InputAction PointerRotationQuest => m_Wrapper.m_RightHand_PointerRotationQuest;

		public InputAction RawPosition => m_Wrapper.m_RightHand_RawPosition;

		public InputAction RawRotation => m_Wrapper.m_RightHand_RawRotation;

		public InputAction Haptic => m_Wrapper.m_RightHand_Haptic;

		public InputAction Scroll => m_Wrapper.m_RightHand_Scroll;

		public InputAction Menu => m_Wrapper.m_RightHand_Menu;

		public bool enabled => Get().enabled;

		public RightHandActions(VRInput wrapper)
		{
			m_Wrapper = wrapper;
		}

		public InputActionMap Get()
		{
			return m_Wrapper.m_RightHand;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(RightHandActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(IRightHandActions instance)
		{
			if (instance != null && !m_Wrapper.m_RightHandActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_RightHandActionsCallbackInterfaces.Add(instance);
				Trigger.started += instance.OnTrigger;
				Trigger.performed += instance.OnTrigger;
				Trigger.canceled += instance.OnTrigger;
				Grip.started += instance.OnGrip;
				Grip.performed += instance.OnGrip;
				Grip.canceled += instance.OnGrip;
				PrimaryButton.started += instance.OnPrimaryButton;
				PrimaryButton.performed += instance.OnPrimaryButton;
				PrimaryButton.canceled += instance.OnPrimaryButton;
				PrimaryTouch.started += instance.OnPrimaryTouch;
				PrimaryTouch.performed += instance.OnPrimaryTouch;
				PrimaryTouch.canceled += instance.OnPrimaryTouch;
				SecondaryButton.started += instance.OnSecondaryButton;
				SecondaryButton.performed += instance.OnSecondaryButton;
				SecondaryButton.canceled += instance.OnSecondaryButton;
				SecondaryTouch.started += instance.OnSecondaryTouch;
				SecondaryTouch.performed += instance.OnSecondaryTouch;
				SecondaryTouch.canceled += instance.OnSecondaryTouch;
				Primary2DAxis.started += instance.OnPrimary2DAxis;
				Primary2DAxis.performed += instance.OnPrimary2DAxis;
				Primary2DAxis.canceled += instance.OnPrimary2DAxis;
				Primary2DAxisClick.started += instance.OnPrimary2DAxisClick;
				Primary2DAxisClick.performed += instance.OnPrimary2DAxisClick;
				Primary2DAxisClick.canceled += instance.OnPrimary2DAxisClick;
				Primary2DAxisTouch.started += instance.OnPrimary2DAxisTouch;
				Primary2DAxisTouch.performed += instance.OnPrimary2DAxisTouch;
				Primary2DAxisTouch.canceled += instance.OnPrimary2DAxisTouch;
				Secondary2DAxis.started += instance.OnSecondary2DAxis;
				Secondary2DAxis.performed += instance.OnSecondary2DAxis;
				Secondary2DAxis.canceled += instance.OnSecondary2DAxis;
				Secondary2DAxisClick.started += instance.OnSecondary2DAxisClick;
				Secondary2DAxisClick.performed += instance.OnSecondary2DAxisClick;
				Secondary2DAxisClick.canceled += instance.OnSecondary2DAxisClick;
				Secondary2DAxisTouch.started += instance.OnSecondary2DAxisTouch;
				Secondary2DAxisTouch.performed += instance.OnSecondary2DAxisTouch;
				Secondary2DAxisTouch.canceled += instance.OnSecondary2DAxisTouch;
				PointerPosition.started += instance.OnPointerPosition;
				PointerPosition.performed += instance.OnPointerPosition;
				PointerPosition.canceled += instance.OnPointerPosition;
				PointerPositionQuest.started += instance.OnPointerPositionQuest;
				PointerPositionQuest.performed += instance.OnPointerPositionQuest;
				PointerPositionQuest.canceled += instance.OnPointerPositionQuest;
				PointerRotation.started += instance.OnPointerRotation;
				PointerRotation.performed += instance.OnPointerRotation;
				PointerRotation.canceled += instance.OnPointerRotation;
				PointerRotationQuest.started += instance.OnPointerRotationQuest;
				PointerRotationQuest.performed += instance.OnPointerRotationQuest;
				PointerRotationQuest.canceled += instance.OnPointerRotationQuest;
				RawPosition.started += instance.OnRawPosition;
				RawPosition.performed += instance.OnRawPosition;
				RawPosition.canceled += instance.OnRawPosition;
				RawRotation.started += instance.OnRawRotation;
				RawRotation.performed += instance.OnRawRotation;
				RawRotation.canceled += instance.OnRawRotation;
				Haptic.started += instance.OnHaptic;
				Haptic.performed += instance.OnHaptic;
				Haptic.canceled += instance.OnHaptic;
				Scroll.started += instance.OnScroll;
				Scroll.performed += instance.OnScroll;
				Scroll.canceled += instance.OnScroll;
				Menu.started += instance.OnMenu;
				Menu.performed += instance.OnMenu;
				Menu.canceled += instance.OnMenu;
			}
		}

		private void UnregisterCallbacks(IRightHandActions instance)
		{
			Trigger.started -= instance.OnTrigger;
			Trigger.performed -= instance.OnTrigger;
			Trigger.canceled -= instance.OnTrigger;
			Grip.started -= instance.OnGrip;
			Grip.performed -= instance.OnGrip;
			Grip.canceled -= instance.OnGrip;
			PrimaryButton.started -= instance.OnPrimaryButton;
			PrimaryButton.performed -= instance.OnPrimaryButton;
			PrimaryButton.canceled -= instance.OnPrimaryButton;
			PrimaryTouch.started -= instance.OnPrimaryTouch;
			PrimaryTouch.performed -= instance.OnPrimaryTouch;
			PrimaryTouch.canceled -= instance.OnPrimaryTouch;
			SecondaryButton.started -= instance.OnSecondaryButton;
			SecondaryButton.performed -= instance.OnSecondaryButton;
			SecondaryButton.canceled -= instance.OnSecondaryButton;
			SecondaryTouch.started -= instance.OnSecondaryTouch;
			SecondaryTouch.performed -= instance.OnSecondaryTouch;
			SecondaryTouch.canceled -= instance.OnSecondaryTouch;
			Primary2DAxis.started -= instance.OnPrimary2DAxis;
			Primary2DAxis.performed -= instance.OnPrimary2DAxis;
			Primary2DAxis.canceled -= instance.OnPrimary2DAxis;
			Primary2DAxisClick.started -= instance.OnPrimary2DAxisClick;
			Primary2DAxisClick.performed -= instance.OnPrimary2DAxisClick;
			Primary2DAxisClick.canceled -= instance.OnPrimary2DAxisClick;
			Primary2DAxisTouch.started -= instance.OnPrimary2DAxisTouch;
			Primary2DAxisTouch.performed -= instance.OnPrimary2DAxisTouch;
			Primary2DAxisTouch.canceled -= instance.OnPrimary2DAxisTouch;
			Secondary2DAxis.started -= instance.OnSecondary2DAxis;
			Secondary2DAxis.performed -= instance.OnSecondary2DAxis;
			Secondary2DAxis.canceled -= instance.OnSecondary2DAxis;
			Secondary2DAxisClick.started -= instance.OnSecondary2DAxisClick;
			Secondary2DAxisClick.performed -= instance.OnSecondary2DAxisClick;
			Secondary2DAxisClick.canceled -= instance.OnSecondary2DAxisClick;
			Secondary2DAxisTouch.started -= instance.OnSecondary2DAxisTouch;
			Secondary2DAxisTouch.performed -= instance.OnSecondary2DAxisTouch;
			Secondary2DAxisTouch.canceled -= instance.OnSecondary2DAxisTouch;
			PointerPosition.started -= instance.OnPointerPosition;
			PointerPosition.performed -= instance.OnPointerPosition;
			PointerPosition.canceled -= instance.OnPointerPosition;
			PointerPositionQuest.started -= instance.OnPointerPositionQuest;
			PointerPositionQuest.performed -= instance.OnPointerPositionQuest;
			PointerPositionQuest.canceled -= instance.OnPointerPositionQuest;
			PointerRotation.started -= instance.OnPointerRotation;
			PointerRotation.performed -= instance.OnPointerRotation;
			PointerRotation.canceled -= instance.OnPointerRotation;
			PointerRotationQuest.started -= instance.OnPointerRotationQuest;
			PointerRotationQuest.performed -= instance.OnPointerRotationQuest;
			PointerRotationQuest.canceled -= instance.OnPointerRotationQuest;
			RawPosition.started -= instance.OnRawPosition;
			RawPosition.performed -= instance.OnRawPosition;
			RawPosition.canceled -= instance.OnRawPosition;
			RawRotation.started -= instance.OnRawRotation;
			RawRotation.performed -= instance.OnRawRotation;
			RawRotation.canceled -= instance.OnRawRotation;
			Haptic.started -= instance.OnHaptic;
			Haptic.performed -= instance.OnHaptic;
			Haptic.canceled -= instance.OnHaptic;
			Scroll.started -= instance.OnScroll;
			Scroll.performed -= instance.OnScroll;
			Scroll.canceled -= instance.OnScroll;
			Menu.started -= instance.OnMenu;
			Menu.performed -= instance.OnMenu;
			Menu.canceled -= instance.OnMenu;
		}

		public void RemoveCallbacks(IRightHandActions instance)
		{
			if (m_Wrapper.m_RightHandActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(IRightHandActions instance)
		{
			foreach (IRightHandActions rightHandActionsCallbackInterface in m_Wrapper.m_RightHandActionsCallbackInterfaces)
			{
				UnregisterCallbacks(rightHandActionsCallbackInterface);
			}
			m_Wrapper.m_RightHandActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public struct HMDActions
	{
		private VRInput m_Wrapper;

		public InputAction hmdPosition => m_Wrapper.m_HMD_hmdPosition;

		public InputAction hmdRotation => m_Wrapper.m_HMD_hmdRotation;

		public bool enabled => Get().enabled;

		public HMDActions(VRInput wrapper)
		{
			m_Wrapper = wrapper;
		}

		public InputActionMap Get()
		{
			return m_Wrapper.m_HMD;
		}

		public void Enable()
		{
			Get().Enable();
		}

		public void Disable()
		{
			Get().Disable();
		}

		public static implicit operator InputActionMap(HMDActions set)
		{
			return set.Get();
		}

		public void AddCallbacks(IHMDActions instance)
		{
			if (instance != null && !m_Wrapper.m_HMDActionsCallbackInterfaces.Contains(instance))
			{
				m_Wrapper.m_HMDActionsCallbackInterfaces.Add(instance);
				hmdPosition.started += instance.OnHmdPosition;
				hmdPosition.performed += instance.OnHmdPosition;
				hmdPosition.canceled += instance.OnHmdPosition;
				hmdRotation.started += instance.OnHmdRotation;
				hmdRotation.performed += instance.OnHmdRotation;
				hmdRotation.canceled += instance.OnHmdRotation;
			}
		}

		private void UnregisterCallbacks(IHMDActions instance)
		{
			hmdPosition.started -= instance.OnHmdPosition;
			hmdPosition.performed -= instance.OnHmdPosition;
			hmdPosition.canceled -= instance.OnHmdPosition;
			hmdRotation.started -= instance.OnHmdRotation;
			hmdRotation.performed -= instance.OnHmdRotation;
			hmdRotation.canceled -= instance.OnHmdRotation;
		}

		public void RemoveCallbacks(IHMDActions instance)
		{
			if (m_Wrapper.m_HMDActionsCallbackInterfaces.Remove(instance))
			{
				UnregisterCallbacks(instance);
			}
		}

		public void SetCallbacks(IHMDActions instance)
		{
			foreach (IHMDActions hMDActionsCallbackInterface in m_Wrapper.m_HMDActionsCallbackInterfaces)
			{
				UnregisterCallbacks(hMDActionsCallbackInterface);
			}
			m_Wrapper.m_HMDActionsCallbackInterfaces.Clear();
			AddCallbacks(instance);
		}
	}

	public interface ILeftHandActions
	{
		void OnTrigger(InputAction.CallbackContext context);

		void OnGrip(InputAction.CallbackContext context);

		void OnPrimaryButton(InputAction.CallbackContext context);

		void OnPrimaryTouch(InputAction.CallbackContext context);

		void OnSecondaryButton(InputAction.CallbackContext context);

		void OnSecondaryTouch(InputAction.CallbackContext context);

		void OnPrimary2DAxis(InputAction.CallbackContext context);

		void OnPrimary2DAxisClick(InputAction.CallbackContext context);

		void OnPrimary2DAxisTouch(InputAction.CallbackContext context);

		void OnSecondary2DAxis(InputAction.CallbackContext context);

		void OnSecondary2DAxisClick(InputAction.CallbackContext context);

		void OnSecondary2DAxisTouch(InputAction.CallbackContext context);

		void OnPointerPosition(InputAction.CallbackContext context);

		void OnPointerPositionQuest(InputAction.CallbackContext context);

		void OnPointerRotation(InputAction.CallbackContext context);

		void OnPointerRotationQuest(InputAction.CallbackContext context);

		void OnRawPosition(InputAction.CallbackContext context);

		void OnRawRotation(InputAction.CallbackContext context);

		void OnHaptic(InputAction.CallbackContext context);

		void OnScroll(InputAction.CallbackContext context);

		void OnMenu(InputAction.CallbackContext context);
	}

	public interface IRightHandActions
	{
		void OnTrigger(InputAction.CallbackContext context);

		void OnGrip(InputAction.CallbackContext context);

		void OnPrimaryButton(InputAction.CallbackContext context);

		void OnPrimaryTouch(InputAction.CallbackContext context);

		void OnSecondaryButton(InputAction.CallbackContext context);

		void OnSecondaryTouch(InputAction.CallbackContext context);

		void OnPrimary2DAxis(InputAction.CallbackContext context);

		void OnPrimary2DAxisClick(InputAction.CallbackContext context);

		void OnPrimary2DAxisTouch(InputAction.CallbackContext context);

		void OnSecondary2DAxis(InputAction.CallbackContext context);

		void OnSecondary2DAxisClick(InputAction.CallbackContext context);

		void OnSecondary2DAxisTouch(InputAction.CallbackContext context);

		void OnPointerPosition(InputAction.CallbackContext context);

		void OnPointerPositionQuest(InputAction.CallbackContext context);

		void OnPointerRotation(InputAction.CallbackContext context);

		void OnPointerRotationQuest(InputAction.CallbackContext context);

		void OnRawPosition(InputAction.CallbackContext context);

		void OnRawRotation(InputAction.CallbackContext context);

		void OnHaptic(InputAction.CallbackContext context);

		void OnScroll(InputAction.CallbackContext context);

		void OnMenu(InputAction.CallbackContext context);
	}

	public interface IHMDActions
	{
		void OnHmdPosition(InputAction.CallbackContext context);

		void OnHmdRotation(InputAction.CallbackContext context);
	}

	private readonly InputActionMap m_LeftHand;

	private List<ILeftHandActions> m_LeftHandActionsCallbackInterfaces = new List<ILeftHandActions>();

	private readonly InputAction m_LeftHand_Trigger;

	private readonly InputAction m_LeftHand_Grip;

	private readonly InputAction m_LeftHand_PrimaryButton;

	private readonly InputAction m_LeftHand_PrimaryTouch;

	private readonly InputAction m_LeftHand_SecondaryButton;

	private readonly InputAction m_LeftHand_SecondaryTouch;

	private readonly InputAction m_LeftHand_Primary2DAxis;

	private readonly InputAction m_LeftHand_Primary2DAxisClick;

	private readonly InputAction m_LeftHand_Primary2DAxisTouch;

	private readonly InputAction m_LeftHand_Secondary2DAxis;

	private readonly InputAction m_LeftHand_Secondary2DAxisClick;

	private readonly InputAction m_LeftHand_Secondary2DAxisTouch;

	private readonly InputAction m_LeftHand_PointerPosition;

	private readonly InputAction m_LeftHand_PointerPositionQuest;

	private readonly InputAction m_LeftHand_PointerRotation;

	private readonly InputAction m_LeftHand_PointerRotationQuest;

	private readonly InputAction m_LeftHand_RawPosition;

	private readonly InputAction m_LeftHand_RawRotation;

	private readonly InputAction m_LeftHand_Haptic;

	private readonly InputAction m_LeftHand_Scroll;

	private readonly InputAction m_LeftHand_Menu;

	private readonly InputActionMap m_RightHand;

	private List<IRightHandActions> m_RightHandActionsCallbackInterfaces = new List<IRightHandActions>();

	private readonly InputAction m_RightHand_Trigger;

	private readonly InputAction m_RightHand_Grip;

	private readonly InputAction m_RightHand_PrimaryButton;

	private readonly InputAction m_RightHand_PrimaryTouch;

	private readonly InputAction m_RightHand_SecondaryButton;

	private readonly InputAction m_RightHand_SecondaryTouch;

	private readonly InputAction m_RightHand_Primary2DAxis;

	private readonly InputAction m_RightHand_Primary2DAxisClick;

	private readonly InputAction m_RightHand_Primary2DAxisTouch;

	private readonly InputAction m_RightHand_Secondary2DAxis;

	private readonly InputAction m_RightHand_Secondary2DAxisClick;

	private readonly InputAction m_RightHand_Secondary2DAxisTouch;

	private readonly InputAction m_RightHand_PointerPosition;

	private readonly InputAction m_RightHand_PointerPositionQuest;

	private readonly InputAction m_RightHand_PointerRotation;

	private readonly InputAction m_RightHand_PointerRotationQuest;

	private readonly InputAction m_RightHand_RawPosition;

	private readonly InputAction m_RightHand_RawRotation;

	private readonly InputAction m_RightHand_Haptic;

	private readonly InputAction m_RightHand_Scroll;

	private readonly InputAction m_RightHand_Menu;

	private readonly InputActionMap m_HMD;

	private List<IHMDActions> m_HMDActionsCallbackInterfaces = new List<IHMDActions>();

	private readonly InputAction m_HMD_hmdPosition;

	private readonly InputAction m_HMD_hmdRotation;

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

	public LeftHandActions LeftHand => new LeftHandActions(this);

	public RightHandActions RightHand => new RightHandActions(this);

	public HMDActions HMD => new HMDActions(this);

	public VRInput()
	{
		asset = InputActionAsset.FromJson("{\n    \"name\": \"VRInput\",\n    \"maps\": [\n        {\n            \"name\": \"LeftHand\",\n            \"id\": \"272f6d14-89ba-496f-b7ff-215263d3219f\",\n            \"actions\": [\n                {\n                    \"name\": \"Trigger\",\n                    \"type\": \"Value\",\n                    \"id\": \"5c8cf249-a01c-4b19-8eea-152b951f6a5a\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Grip\",\n                    \"type\": \"Value\",\n                    \"id\": \"26a9b492-9ead-4e86-9484-d69dc44c512e\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PrimaryButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"204073bf-582e-48f4-a392-6eaf1e637387\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"PrimaryTouch\",\n                    \"type\": \"Value\",\n                    \"id\": \"48a4f982-c501-412c-a715-b9201cb08c37\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"SecondaryButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"1414e4ef-05d7-40b1-b759-aa2c2c9cee56\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SecondaryTouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"83a06fa6-147b-41c5-9841-1fd2bbb33c31\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Primary2DAxis\",\n                    \"type\": \"Value\",\n                    \"id\": \"0e72db49-759e-4b56-853f-a5e3b9bf0e04\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Primary2DAxisClick\",\n                    \"type\": \"Button\",\n                    \"id\": \"d36e4b01-3a25-4dc6-8094-179ddb10bd71\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Primary2DAxisTouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"416d6df4-7244-4b5c-986c-11881e0eac57\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Secondary2DAxis\",\n                    \"type\": \"Value\",\n                    \"id\": \"344eec1c-660f-4d4a-8b5b-1a2362d9b2de\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Secondary2DAxisClick\",\n                    \"type\": \"Button\",\n                    \"id\": \"927e0cfe-b2ed-4a00-8048-3cd36742a8e3\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Secondary2DAxisTouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"28bf5850-459e-4b46-af7a-24a5a64fdf99\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"PointerPosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"aa016bcd-24a3-44e5-bddf-44f908b7f793\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"TrackedPosition\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerPositionQuest\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"ecf25ea0-a8cc-4dac-a732-b9af58a3937a\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"TrackedPosition\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerRotation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"9cdd0812-f4ce-4aac-b182-7930625c4d99\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"TrackedRotation\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerRotationQuest\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"66cf83bb-8cc6-4af5-9388-b73f61569d51\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"TrackedRotation\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RawPosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"8cb9b631-0165-4029-abdd-a55487e69093\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RawRotation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"f34fdded-d931-4709-80d2-913469699dc8\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Haptic\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"76e395dd-d7df-4654-9787-fb936b50b7e5\",\n                    \"expectedControlType\": \"Haptic\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Scroll\",\n                    \"type\": \"Value\",\n                    \"id\": \"a9a9abe7-dbc7-4a3b-8346-0b14b3bf9a5b\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"Scroll(scrollMultiplier=0.25)\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Menu\",\n                    \"type\": \"Button\",\n                    \"id\": \"e91ffee7-ec69-40fb-8663-4ebf07153bfe\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"ec353a5a-6a7a-4ee6-a33e-d12a8c0b705a\",\n                    \"path\": \"<XRController>{LeftHand}/{trigger}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Trigger\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1a98fe1d-5477-4473-9ed1-05d2decb2c15\",\n                    \"path\": \"<XRController>{LeftHand}/{primaryButton}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PrimaryButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3dfde460-9bef-4bd9-943d-170adc171ff8\",\n                    \"path\": \"<XRController>{LeftHand}/{PrimaryTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PrimaryTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"54ac7ab0-8ca9-4cbc-a6d1-da5ee0b055cb\",\n                    \"path\": \"<XRController>{LeftHand}/{primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Primary2DAxis\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"85d45408-e658-4df8-ab60-de9273ceb30c\",\n                    \"path\": \"<XRController>{LeftHand}/{primary2DAxisClick}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Primary2DAxisClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a07f0bb7-9d68-4288-b0f1-78c62a995fa4\",\n                    \"path\": \"<XRController>{LeftHand}/{primary2DAxisTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Primary2DAxisTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cb03eabc-18e2-447a-a308-dc36cf9ec38e\",\n                    \"path\": \"<XRController>{LeftHand}/{secondary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Secondary2DAxis\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"fadeed58-e6fc-4cc4-9fdc-5cfae044209c\",\n                    \"path\": \"<XRController>{LeftHand}/{secondary2DAxisClick}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Secondary2DAxisClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"b74594f8-e87a-43ae-82ec-47612e07bfb3\",\n                    \"path\": \"<XRController>{LeftHand}/{secondary2DAxisTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Secondary2DAxisTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"2e0bfaeb-6ce1-49fe-9fbe-69651c239f6c\",\n                    \"path\": \"<XRController>{LeftHand}/pointerPosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7eaabcc7-41cd-41fc-a31c-b35b4e61efd9\",\n                    \"path\": \"<XRController>{LeftHand}/pointerRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerRotation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5e867cab-2f35-436f-9c34-174590993836\",\n                    \"path\": \"<XRController>{LeftHand}/haptic\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Haptic\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"8f283b99-d4ca-4570-8070-de21772b79b5\",\n                    \"path\": \"<XRController>{LeftHand}/{Primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Scroll\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"776710f2-2f33-43fc-a617-36e87aea52eb\",\n                    \"path\": \"<XRController>{LeftHand}/pointerRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RawRotation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"23abbc03-06e7-4091-9c95-c016770e462d\",\n                    \"path\": \"<XRController>{LeftHand}/pointerPosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RawPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9603ae96-fef3-432c-88a0-282232c1368d\",\n                    \"path\": \"<XRController>{LeftHand}/menu\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"171f2010-aff7-4c02-8e86-44c84eba40e6\",\n                    \"path\": \"<XRController>{LeftHand}/system\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"69ac9869-2e13-461f-bb82-793d87467244\",\n                    \"path\": \"<ValveIndexController>{LeftHand}/gripForce\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e6f97c20-2af3-48bc-8c3c-c8d221f20974\",\n                    \"path\": \"<OculusTouchController>{LeftHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"bfb5d73b-f231-42ca-bd04-845fe8e34b00\",\n                    \"path\": \"<ViveController>{LeftHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"aa25ec9b-fcec-4f63-83de-df44d665697d\",\n                    \"path\": \"<QuestProTouchController>{LeftHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7c1e027a-d423-49da-bef5-057a3a477b0a\",\n                    \"path\": \"<VRSimulatorController>{LeftHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0c3f7ce0-b62f-44e2-a1e1-11f143de72d7\",\n                    \"path\": \"<XRController>{LeftHand}/{secondaryButton}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"SecondaryButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"6cfe2a5d-28c1-425b-998e-810968e19292\",\n                    \"path\": \"<XRController>{LeftHand}/{secondaryTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"SecondaryTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a51edf9f-fbad-442d-8018-3e8fd8a7675b\",\n                    \"path\": \"<ViveController>{LeftHand}/menu\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PrimaryButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"357ee63c-a929-4b8a-8334-9f61acbaf07d\",\n                    \"path\": \"<OculusTouchController>{LeftHand}/{DevicePosition}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerPositionQuest\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"157a5744-208c-40d4-af8f-2b32715ec296\",\n                    \"path\": \"<OculusTouchController>{LeftHand}/{DeviceRotation}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerRotationQuest\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"RightHand\",\n            \"id\": \"fd87d126-5dc2-445c-82df-1abc6c9f8076\",\n            \"actions\": [\n                {\n                    \"name\": \"Trigger\",\n                    \"type\": \"Value\",\n                    \"id\": \"2fccaf18-d578-4ac9-9d0b-08db4a2e1dc7\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Grip\",\n                    \"type\": \"Value\",\n                    \"id\": \"f17f4516-fcdf-4891-9411-fee4983cd798\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PrimaryButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"b87d0657-0e2f-41f9-92ee-6c7f0b0fa4f8\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"PrimaryTouch\",\n                    \"type\": \"Value\",\n                    \"id\": \"7612bd2d-ec25-4cb7-8eb2-3a4b7704b025\",\n                    \"expectedControlType\": \"Axis\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"SecondaryButton\",\n                    \"type\": \"Button\",\n                    \"id\": \"cba87b5e-c43b-4c38-b5dc-320d8fbb6136\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"SecondaryTouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"b20f3aa0-32c6-486d-a9b2-4d8baca0799f\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Primary2DAxis\",\n                    \"type\": \"Value\",\n                    \"id\": \"273526bd-c3ba-4f35-ac7a-840c1ed6ab6d\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Primary2DAxisClick\",\n                    \"type\": \"Button\",\n                    \"id\": \"1c531107-9819-4f1f-a0e7-f11166016436\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Primary2DAxisTouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"4de79a39-d622-4859-8f67-791365c0b780\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Secondary2DAxis\",\n                    \"type\": \"Value\",\n                    \"id\": \"5c85476c-f285-4a0a-8ed8-31466beae969\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Secondary2DAxisClick\",\n                    \"type\": \"Button\",\n                    \"id\": \"83227e26-e8f1-416e-a4a6-b70394fe5ea5\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Secondary2DAxisTouch\",\n                    \"type\": \"Button\",\n                    \"id\": \"05c6375e-e244-427a-8e61-cd465ffa62ba\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"PointerPosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"dfe9bf01-d1b6-4869-947f-775e28857768\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"TrackedPosition(isRightController=true)\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerPositionQuest\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"281632e8-c87a-4699-b337-dfc32b9e7636\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"TrackedPosition(isRightController=true)\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerRotation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"6af87a70-4748-417c-a266-b6651d3617cf\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"TrackedRotation(isRightController=true)\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"PointerRotationQuest\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"0429a7fe-8b52-4695-a61d-fabfd7170a70\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"TrackedRotation(isRightController=true)\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RawPosition\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"7d7bc22f-d36a-4901-848a-bfaf364a2c08\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"RawRotation\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"d6bd30fb-428e-4afc-9134-2b7f6197ffff\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Haptic\",\n                    \"type\": \"PassThrough\",\n                    \"id\": \"d58abe56-873a-44a7-93b1-40630a1829dd\",\n                    \"expectedControlType\": \"Haptic\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                },\n                {\n                    \"name\": \"Scroll\",\n                    \"type\": \"Value\",\n                    \"id\": \"6357ec98-dae4-4b43-93a8-37057a2eb4e0\",\n                    \"expectedControlType\": \"Vector2\",\n                    \"processors\": \"Scroll(scrollMultiplier=0.25)\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"Menu\",\n                    \"type\": \"Button\",\n                    \"id\": \"561d782c-0eab-4674-80c5-7a8eb70cf246\",\n                    \"expectedControlType\": \"Button\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": false\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"d1bf452d-b059-4c1e-9c23-3be1c6be789e\",\n                    \"path\": \"<XRController>{RightHand}/{trigger}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Trigger\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d09e2fdd-08b3-42eb-86c8-5c3a8f4a03c5\",\n                    \"path\": \"<XRController>{RightHand}/{primaryButton}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PrimaryButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0657f0e8-4c3b-45d4-a00b-fda175a6684d\",\n                    \"path\": \"<XRController>{RightHand}/{PrimaryTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PrimaryTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"488857b2-965c-486c-b24e-e21628ad2a07\",\n                    \"path\": \"<XRController>{RightHand}/{primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Primary2DAxis\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"feccb40e-4fa1-488f-a2f1-f44c2b80ae37\",\n                    \"path\": \"<XRController>{RightHand}/{primary2DAxisClick}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Primary2DAxisClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"62761c89-0d6d-40f1-ae88-88fa58d7ff02\",\n                    \"path\": \"<XRController>{RightHand}/{primary2DAxisTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Primary2DAxisTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e40a1a86-f851-421e-86c4-aec93c7f1d9b\",\n                    \"path\": \"<XRController>{RightHand}/{secondary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Secondary2DAxis\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"d094a06a-f8ca-4c85-b478-a5276f7da3e4\",\n                    \"path\": \"<XRController>{RightHand}/{secondary2DAxisClick}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Secondary2DAxisClick\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"75e038e5-ec3b-417e-adf4-77881b52a1ff\",\n                    \"path\": \"<XRController>{RightHand}/{secondary2DAxisTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Secondary2DAxisTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"ba89ffae-14bc-4a81-944e-ca0649618084\",\n                    \"path\": \"<XRController>{RightHand}/pointerRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerRotation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"adfd3ab1-a822-4dea-9d94-e110b2cdaa99\",\n                    \"path\": \"<XRController>{RightHand}/pointerPosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"1d456cea-e5d1-4181-9d80-a876eb828922\",\n                    \"path\": \"<XRController>{RightHand}/haptic\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Haptic\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c8d69564-58d2-437e-be3a-7e3f5d8d74c4\",\n                    \"path\": \"<XRController>{RightHand}/{primary2DAxis}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Scroll\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"cb9e4e2d-024e-434e-8ead-ccbcf1e829b4\",\n                    \"path\": \"<XRController>{RightHand}/pointerRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RawRotation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"5bd760e3-b971-43ae-9f92-5108c131b030\",\n                    \"path\": \"<XRController>{RightHand}/pointerPosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"RawPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"c3d9acf1-00ab-4616-951f-ef81d04d4d58\",\n                    \"path\": \"<XRController>{RightHand}/menu\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"369648ca-da2c-4cb7-bb0c-8b7dcab3b6b5\",\n                    \"path\": \"<XRController>{RightHand}/system\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Menu\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"a5e7dae2-203a-4b9f-acd2-db774c7acd3f\",\n                    \"path\": \"<ValveIndexController>{RightHand}/gripForce\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"e41562ed-e732-41bb-8edc-43f7968ee655\",\n                    \"path\": \"<OculusTouchController>{RightHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"56ccedae-987f-4fad-9ce6-2a083ac64e82\",\n                    \"path\": \"<ViveController>{RightHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"7b2e1c84-5249-42e3-a37e-9c735498d4e7\",\n                    \"path\": \"<QuestProTouchController>{RightHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"3cfea08e-9f95-4f3a-8e8b-e403b1c96245\",\n                    \"path\": \"<VRSimulatorController>{RightHand}/grip\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"Grip\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9d0fd645-57e1-48c3-9393-701a1d96c9a2\",\n                    \"path\": \"<XRController>{RightHand}/{secondaryButton}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"SecondaryButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"0d034f83-a2ec-4c56-8a45-74443b36bb77\",\n                    \"path\": \"<XRController>{RightHand}/{secondaryTouch}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"SecondaryTouch\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"9fcb2db7-aa7c-4c99-972a-34819a47e213\",\n                    \"path\": \"<ViveController>{RightHand}/menu\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PrimaryButton\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"03b23685-0c9e-4063-b881-48244e99024b\",\n                    \"path\": \"<OculusTouchController>{RightHand}/{DevicePosition}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerPositionQuest\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"59e96231-00b8-4a30-940a-5337f7f919b4\",\n                    \"path\": \"<OculusTouchController>{RightHand}/{DeviceRotation}\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"PointerRotationQuest\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        },\n        {\n            \"name\": \"HMD\",\n            \"id\": \"21d522e0-0295-43ef-b60a-3b355794717b\",\n            \"actions\": [\n                {\n                    \"name\": \"hmdPosition\",\n                    \"type\": \"Value\",\n                    \"id\": \"36a3121d-efce-478c-867c-a8d354777b78\",\n                    \"expectedControlType\": \"Vector3\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                },\n                {\n                    \"name\": \"hmdRotation\",\n                    \"type\": \"Value\",\n                    \"id\": \"4e56a29d-b0cc-42fd-a087-552950afac95\",\n                    \"expectedControlType\": \"Quaternion\",\n                    \"processors\": \"\",\n                    \"interactions\": \"\",\n                    \"initialStateCheck\": true\n                }\n            ],\n            \"bindings\": [\n                {\n                    \"name\": \"\",\n                    \"id\": \"08f50b61-5d92-42ac-bdb9-b898a4f0d4a7\",\n                    \"path\": \"<XRHMD>/devicePosition\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"hmdPosition\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                },\n                {\n                    \"name\": \"\",\n                    \"id\": \"09769104-56ee-4fae-9065-07f530388e75\",\n                    \"path\": \"<XRHMD>/deviceRotation\",\n                    \"interactions\": \"\",\n                    \"processors\": \"\",\n                    \"groups\": \"\",\n                    \"action\": \"hmdRotation\",\n                    \"isComposite\": false,\n                    \"isPartOfComposite\": false\n                }\n            ]\n        }\n    ],\n    \"controlSchemes\": []\n}");
		m_LeftHand = asset.FindActionMap("LeftHand", throwIfNotFound: true);
		m_LeftHand_Trigger = m_LeftHand.FindAction("Trigger", throwIfNotFound: true);
		m_LeftHand_Grip = m_LeftHand.FindAction("Grip", throwIfNotFound: true);
		m_LeftHand_PrimaryButton = m_LeftHand.FindAction("PrimaryButton", throwIfNotFound: true);
		m_LeftHand_PrimaryTouch = m_LeftHand.FindAction("PrimaryTouch", throwIfNotFound: true);
		m_LeftHand_SecondaryButton = m_LeftHand.FindAction("SecondaryButton", throwIfNotFound: true);
		m_LeftHand_SecondaryTouch = m_LeftHand.FindAction("SecondaryTouch", throwIfNotFound: true);
		m_LeftHand_Primary2DAxis = m_LeftHand.FindAction("Primary2DAxis", throwIfNotFound: true);
		m_LeftHand_Primary2DAxisClick = m_LeftHand.FindAction("Primary2DAxisClick", throwIfNotFound: true);
		m_LeftHand_Primary2DAxisTouch = m_LeftHand.FindAction("Primary2DAxisTouch", throwIfNotFound: true);
		m_LeftHand_Secondary2DAxis = m_LeftHand.FindAction("Secondary2DAxis", throwIfNotFound: true);
		m_LeftHand_Secondary2DAxisClick = m_LeftHand.FindAction("Secondary2DAxisClick", throwIfNotFound: true);
		m_LeftHand_Secondary2DAxisTouch = m_LeftHand.FindAction("Secondary2DAxisTouch", throwIfNotFound: true);
		m_LeftHand_PointerPosition = m_LeftHand.FindAction("PointerPosition", throwIfNotFound: true);
		m_LeftHand_PointerPositionQuest = m_LeftHand.FindAction("PointerPositionQuest", throwIfNotFound: true);
		m_LeftHand_PointerRotation = m_LeftHand.FindAction("PointerRotation", throwIfNotFound: true);
		m_LeftHand_PointerRotationQuest = m_LeftHand.FindAction("PointerRotationQuest", throwIfNotFound: true);
		m_LeftHand_RawPosition = m_LeftHand.FindAction("RawPosition", throwIfNotFound: true);
		m_LeftHand_RawRotation = m_LeftHand.FindAction("RawRotation", throwIfNotFound: true);
		m_LeftHand_Haptic = m_LeftHand.FindAction("Haptic", throwIfNotFound: true);
		m_LeftHand_Scroll = m_LeftHand.FindAction("Scroll", throwIfNotFound: true);
		m_LeftHand_Menu = m_LeftHand.FindAction("Menu", throwIfNotFound: true);
		m_RightHand = asset.FindActionMap("RightHand", throwIfNotFound: true);
		m_RightHand_Trigger = m_RightHand.FindAction("Trigger", throwIfNotFound: true);
		m_RightHand_Grip = m_RightHand.FindAction("Grip", throwIfNotFound: true);
		m_RightHand_PrimaryButton = m_RightHand.FindAction("PrimaryButton", throwIfNotFound: true);
		m_RightHand_PrimaryTouch = m_RightHand.FindAction("PrimaryTouch", throwIfNotFound: true);
		m_RightHand_SecondaryButton = m_RightHand.FindAction("SecondaryButton", throwIfNotFound: true);
		m_RightHand_SecondaryTouch = m_RightHand.FindAction("SecondaryTouch", throwIfNotFound: true);
		m_RightHand_Primary2DAxis = m_RightHand.FindAction("Primary2DAxis", throwIfNotFound: true);
		m_RightHand_Primary2DAxisClick = m_RightHand.FindAction("Primary2DAxisClick", throwIfNotFound: true);
		m_RightHand_Primary2DAxisTouch = m_RightHand.FindAction("Primary2DAxisTouch", throwIfNotFound: true);
		m_RightHand_Secondary2DAxis = m_RightHand.FindAction("Secondary2DAxis", throwIfNotFound: true);
		m_RightHand_Secondary2DAxisClick = m_RightHand.FindAction("Secondary2DAxisClick", throwIfNotFound: true);
		m_RightHand_Secondary2DAxisTouch = m_RightHand.FindAction("Secondary2DAxisTouch", throwIfNotFound: true);
		m_RightHand_PointerPosition = m_RightHand.FindAction("PointerPosition", throwIfNotFound: true);
		m_RightHand_PointerPositionQuest = m_RightHand.FindAction("PointerPositionQuest", throwIfNotFound: true);
		m_RightHand_PointerRotation = m_RightHand.FindAction("PointerRotation", throwIfNotFound: true);
		m_RightHand_PointerRotationQuest = m_RightHand.FindAction("PointerRotationQuest", throwIfNotFound: true);
		m_RightHand_RawPosition = m_RightHand.FindAction("RawPosition", throwIfNotFound: true);
		m_RightHand_RawRotation = m_RightHand.FindAction("RawRotation", throwIfNotFound: true);
		m_RightHand_Haptic = m_RightHand.FindAction("Haptic", throwIfNotFound: true);
		m_RightHand_Scroll = m_RightHand.FindAction("Scroll", throwIfNotFound: true);
		m_RightHand_Menu = m_RightHand.FindAction("Menu", throwIfNotFound: true);
		m_HMD = asset.FindActionMap("HMD", throwIfNotFound: true);
		m_HMD_hmdPosition = m_HMD.FindAction("hmdPosition", throwIfNotFound: true);
		m_HMD_hmdRotation = m_HMD.FindAction("hmdRotation", throwIfNotFound: true);
	}

	~VRInput()
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
