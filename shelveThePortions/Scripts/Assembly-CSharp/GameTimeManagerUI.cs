using TMPro;
using UnityEngine;

public class GameTimeManagerUI : SceneSingleton<GameTimeManagerUI>
{
	[SerializeField]
	private CanvasGroup timerPanelCG;

	[SerializeField]
	private TextMeshProUGUI inGameTimer;

	private void Start()
	{
		RefreshTimerUI();
	}

	public void RefreshTimerUI()
	{
		TweenController.KillTweens(timerPanelCG.gameObject);
		if (SaveSystem.GetTimeToggleSetting())
		{
			TweenController.CanvasGroupAlpha(timerPanelCG, timerPanelCG.alpha, 1f, TweenDuration.Super_Short);
		}
		else
		{
			TweenController.CanvasGroupAlpha(timerPanelCG, timerPanelCG.alpha, 0f, TweenDuration.Super_Short);
		}
	}

	public void SetTimer(string time)
	{
		inGameTimer.text = time;
	}
}
