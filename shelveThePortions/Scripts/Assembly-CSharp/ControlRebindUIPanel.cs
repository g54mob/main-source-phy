using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlRebindUIPanel : SceneSingleton<ControlRebindUIPanel>
{
	[SerializeField]
	private CanvasGroup panelCG;

	[SerializeField]
	private LocalizationTextActivator textActivator;

	[SerializeField]
	private LocalizationTextActivator panelNotifyTextActivator;

	[SerializeField]
	private TextMeshProUGUI panelBindingText;

	[SerializeField]
	private Image panelBindingImage;

	[SerializeField]
	private GameObject controlCancelUIImage;

	public void ShowRebindPanel(string actionName, string keyCode)
	{
		SetRebindPanel(actionName);
		panelBindingImage.gameObject.SetActive(value: false);
		panelBindingText.gameObject.SetActive(value: true);
		panelBindingText.text = keyCode;
		controlCancelUIImage.SetActive(value: true);
	}

	public void ShowRebindPanel(string actionName, Sprite keySprite)
	{
		SetRebindPanel(actionName);
		panelBindingText.gameObject.SetActive(value: false);
		panelBindingImage.gameObject.SetActive(value: true);
		panelBindingImage.sprite = keySprite;
		controlCancelUIImage.SetActive(value: true);
	}

	private void SetRebindPanel(string actionID)
	{
		SceneSingleton<ControlsUIManager>.Instance.OnRebindStarted();
		InputRebindManager.OnRebindIsDuplicate += OnRebindDuplicate;
		InputRebindManager.OnRebindIsExcluded += OnRebindExcluded;
		panelCG.interactable = true;
		panelCG.blocksRaycasts = true;
		panelNotifyTextActivator.gameObject.SetActive(value: false);
		if (panelCG.alpha != 1f)
		{
			TweenController.DOFloat(base.gameObject, 0f, 1f, TweenDuration.Super_Short, delegate(float f)
			{
				panelCG.alpha = f;
			}, null, ignoreTimeScale: true);
		}
		textActivator.SetIDString(actionID);
	}

	public void HideRebindPanel()
	{
		InputRebindManager.OnRebindIsDuplicate -= OnRebindDuplicate;
		InputRebindManager.OnRebindIsExcluded -= OnRebindExcluded;
		TweenController.DOFloat(base.gameObject, 1f, 0f, TweenDuration.Super_Short, delegate(float f)
		{
			panelCG.alpha = f;
		}, OnHideRebindPanelComplete, ignoreTimeScale: true);
		controlCancelUIImage.SetActive(value: false);
	}

	private void OnHideRebindPanelComplete()
	{
		SceneSingleton<ControlsUIManager>.Instance.OnRebindEnded();
		panelNotifyTextActivator.gameObject.SetActive(value: false);
		panelCG.interactable = false;
		panelCG.blocksRaycasts = false;
	}

	public void OnRebindDuplicate()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
		panelNotifyTextActivator.SetIDString("Controls_KeyAlreadyInUse");
		AnimateNotifyText();
	}

	public void OnRebindExcluded()
	{
		Singleton<AudioManager>.Instance.PlayClip(AudioClipTypes.UI_Error);
		panelNotifyTextActivator.SetIDString("Controls_KeyExcluded");
		AnimateNotifyText();
	}

	private void AnimateNotifyText()
	{
		panelNotifyTextActivator.gameObject.SetActive(value: true);
		TweenController.KillTweens(panelNotifyTextActivator.gameObject);
		TweenController.PunchScale(panelNotifyTextActivator.gameObject, 0.1f, TweenDuration.Very_Short, null, ignoreTimeScale: true);
	}
}
