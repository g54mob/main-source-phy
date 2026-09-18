using UnityEngine;

public class AutoSavePanelUI : SceneSingleton<AutoSavePanelUI>
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	public void ShowPanel()
	{
		if (Singleton<GameBuildManager>.Instance != null && Singleton<GameBuildManager>.Instance.IsFreeVersion())
		{
			return;
		}
		TweenController.KillTweens(canvasGroup.gameObject);
		TweenController.DOFloat(canvasGroup.gameObject, 0f, 1f, TweenDuration.Super_Short, delegate(float f)
		{
			if (canvasGroup != null)
			{
				canvasGroup.alpha = f;
			}
		}, null, ignoreTimeScale: true);
		TweenController.DelayedCall(base.gameObject, 5f, HidePanel, ignoreTimeScale: true);
	}

	public void HidePanel()
	{
		TweenController.KillTweens(canvasGroup.gameObject);
		TweenController.DOFloat(canvasGroup.gameObject, 1f, 0f, TweenDuration.Super_Short, delegate(float f)
		{
			if (canvasGroup != null)
			{
				canvasGroup.alpha = f;
			}
		}, null, ignoreTimeScale: true);
	}
}
