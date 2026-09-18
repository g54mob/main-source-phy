using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlUIContainer : MonoBehaviour
{
	[Header("Container Control Action")]
	[SerializeField]
	private InputActionReference inputActionReference;

	[SerializeField]
	private InputActionReference altInputActionReference;

	private InputBinding inputBinding;

	private InputAction inputAction;

	private int bindingIndex;

	[SerializeField]
	private LocalizationTextActivator textActivator;

	[Space]
	[Header("Container Componments")]
	[SerializeField]
	private TextMeshProUGUI containerBindingText;

	[Space]
	[SerializeField]
	private Image containerBindingImage;

	[Space]
	[SerializeField]
	private Button containerRebindButton;

	[Space]
	[Space]
	[SerializeField]
	private bool isDebugOn;

	private void OnEnable()
	{
		EventManager.OnControlsChange += UpdateContainerUI;
		UpdateContainerUI();
	}

	private void OnDisable()
	{
		if (Singleton<EventManager>.Instance != null)
		{
			EventManager.OnControlsChange -= UpdateContainerUI;
		}
	}

	public void UpdateContainerUI()
	{
		if (base.gameObject.activeSelf)
		{
			SetBindingActionInfo();
			textActivator.SetIDString(InputDataStore.GetActionID(inputAction));
			UpdateBindingDisplayUI();
		}
	}

	public void DoRebind()
	{
		if (!SceneSingleton<ControlsUIManager>.Instance.isRebinding)
		{
			containerRebindButton.interactable = false;
			InputRebindManager.OnRebindCanceled += OnRebindCanceled;
			InputRebindManager.OnRebindComplete += OnRebindComplete;
			InputRebindManager.DoRebind(inputAction, bindingIndex);
			if (containerBindingText.gameObject.activeSelf)
			{
				SceneSingleton<ControlRebindUIPanel>.Instance.ShowRebindPanel(InputDataStore.GetActionID(inputAction), containerBindingText.text);
			}
			else
			{
				SceneSingleton<ControlRebindUIPanel>.Instance.ShowRebindPanel(InputDataStore.GetActionID(inputAction), containerBindingImage.sprite);
			}
			if (isDebugOn)
			{
				Debug.Log(inputAction?.ToString() + " ReBind Started " + inputAction.name + " " + InputDataStore.GetActionID(inputAction));
			}
		}
	}

	public void OnRebindComplete()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Click);
		if (altInputActionReference != null)
		{
			InputRebindManager.CopyControlsBinding(inputActionReference, altInputActionReference);
		}
		SceneSingleton<ControlsUIManager>.Instance.ApplyNewRebind();
		OnRebindEnded();
	}

	public void OnRebindCanceled()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
		OnRebindEnded();
	}

	private void OnRebindEnded()
	{
		InputRebindManager.OnRebindCanceled -= OnRebindCanceled;
		InputRebindManager.OnRebindComplete -= OnRebindComplete;
		SceneSingleton<ControlRebindUIPanel>.Instance.HideRebindPanel();
		containerRebindButton.interactable = true;
		UpdateContainerUI();
	}

	private void UpdateBindingDisplayUI()
	{
		Sprite deviceBindingIcon = Singleton<InputDataStore>.Instance.GetDeviceBindingIcon(inputBinding.effectivePath);
		if (deviceBindingIcon != null)
		{
			containerBindingImage.gameObject.SetActive(value: true);
			containerBindingText.gameObject.SetActive(value: false);
			containerBindingImage.sprite = deviceBindingIcon;
		}
		else
		{
			containerBindingImage.gameObject.SetActive(value: false);
			containerBindingText.gameObject.SetActive(value: true);
			string humanReadableBinding = InputDataStore.GetHumanReadableBinding(inputBinding);
			containerBindingText.text = humanReadableBinding;
		}
	}

	private void SetBindingActionInfo()
	{
		if (inputActionReference.action != null)
		{
			inputAction = Singleton<InputDataStore>.Instance.GetInputAction(inputActionReference.action.name);
			bindingIndex = inputAction.GetBindingIndexForControl(inputAction.controls[0]);
			inputBinding = inputAction.bindings[bindingIndex];
		}
		else
		{
			Debug.LogError("InputActionReference is missing from the control container");
		}
	}
}
