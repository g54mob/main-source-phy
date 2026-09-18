using System;
using UnityEngine;

public class ConfirmationPanelUI : PanelUI<ConfirmationPanelUI>
{
	[SerializeField]
	private LocalizationTextActivator localizationTextActivator;

	private Action OnYesAction;

	[SerializeField]
	private GameObject panel;

	[SerializeField]
	private CanvasGroup canvasGroup;

	public void SetPanel(string textID, Action OnYesAction)
	{
		panel.SetActive(value: true);
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
		this.OnYesAction = OnYesAction;
		localizationTextActivator.SetIDString(textID);
	}

	public void YesButtonClicked()
	{
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		OnYesAction();
		panel.SetActive(value: false);
	}

	public void DisableUI()
	{
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		panel.SetActive(value: false);
	}

	public void NoButtonClicked()
	{
		UIBackKeyManager.BackUI();
	}
}
