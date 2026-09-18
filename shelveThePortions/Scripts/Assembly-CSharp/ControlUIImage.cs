using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlUIImage : MonoBehaviour
{
	[SerializeField]
	private InputActionReference inputActionReference;

	private InputBinding inputBinding;

	private InputAction inputAction;

	private int bindingIndex;

	[Space]
	[SerializeField]
	private Image containerBindingImage;

	[SerializeField]
	private TextMeshProUGUI containerBindingText;

	private void OnEnable()
	{
		EventManager.OnControlsChange += UpdateBindingDisplayUI;
		EventManager.OnControlsChange += UpdateBindingDisplayUI;
		InputDataStore instance = Singleton<InputDataStore>.Instance;
		instance.OnControlApplied = (Action)Delegate.Combine(instance.OnControlApplied, new Action(UpdateBindingDisplayUI));
		UpdateBindingDisplayUI();
	}

	private void OnDisable()
	{
		EventManager.OnControlsChange -= UpdateBindingDisplayUI;
		if (Singleton<EventManager>.Instance != null)
		{
			EventManager.OnControlsChange -= UpdateBindingDisplayUI;
		}
		if (Singleton<InputDataStore>.Instance != null)
		{
			InputDataStore instance = Singleton<InputDataStore>.Instance;
			instance.OnControlApplied = (Action)Delegate.Remove(instance.OnControlApplied, new Action(UpdateBindingDisplayUI));
		}
	}

	public void UpdateBindingDisplayUI(InputActionReference inputActionReference)
	{
		this.inputActionReference = inputActionReference;
		UpdateBindingDisplayUI();
	}

	public void UpdateBindingDisplayUI()
	{
		containerBindingImage.gameObject.SetActive(value: false);
		if (containerBindingText != null)
		{
			containerBindingText.gameObject.SetActive(value: false);
		}
		if (!(inputActionReference == null))
		{
			SetBindingActionInfo();
			Sprite deviceBindingIcon = Singleton<InputDataStore>.Instance.GetDeviceBindingIcon(inputBinding.effectivePath);
			if (deviceBindingIcon != null)
			{
				containerBindingImage.gameObject.SetActive(value: true);
				containerBindingImage.sprite = deviceBindingIcon;
			}
			else if (!(containerBindingText == null))
			{
				containerBindingText.gameObject.SetActive(value: true);
				string humanReadableBinding = InputDataStore.GetHumanReadableBinding(inputBinding);
				containerBindingText.text = "(" + humanReadableBinding + ")";
			}
		}
	}

	private void SetBindingActionInfo()
	{
		if (inputActionReference.action != null)
		{
			inputAction = Singleton<InputDataStore>.Instance.GetInputAction(inputActionReference.action.name);
			if (inputAction.controls.Count > 0)
			{
				bindingIndex = inputAction.GetBindingIndexForControl(inputAction.controls[0]);
				inputBinding = inputAction.bindings[bindingIndex];
			}
			else
			{
				base.gameObject.SetActive(value: false);
			}
		}
		else
		{
			Debug.LogError("InputActionReference is missing from the control container");
		}
	}
}
