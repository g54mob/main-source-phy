using UnityEngine;

public class ConversationPanelUI : SceneSingleton<ConversationPanelUI>
{
	[SerializeField]
	private LocalizationTextActivator localizationTextActivator;

	[SerializeField]
	private CanvasGroup canvasGroup;

	public void SetPanel(string id)
	{
		canvasGroup.alpha = 1f;
		localizationTextActivator.SetIDString(id);
	}

	public void HidePanel()
	{
		canvasGroup.alpha = 0f;
	}
}
