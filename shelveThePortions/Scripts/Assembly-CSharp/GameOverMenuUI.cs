using TMPro;
using UnityEngine;

public class GameOverMenuUI : PanelUI<GameOverMenuUI>
{
	[SerializeField]
	private TextMeshProUGUI timeText;

	[SerializeField]
	private TextMeshProUGUI catPettedText;

	[SerializeField]
	private TextMeshProUGUI abilitiesUsedText;

	private void Start()
	{
		EventManager.GameEnd += UpdateUI;
	}

	private void OnDisable()
	{
		EventManager.GameEnd -= UpdateUI;
	}

	public void UpdateUI()
	{
		timeText.text = SceneSingleton<GameTimeManager>.Instance.GetTimerString();
		catPettedText.text = SceneSingleton<CatsManager>.Instance.catsPettedCount.ToString();
		abilitiesUsedText.text = SceneSingleton<AbilitiesManager>.Instance.abilitiesUsed.ToString();
	}

	public void ContinueGame()
	{
		EventManager.ActivateEvent(EventTypes.GameResume);
	}

	public void GoToMainMenu()
	{
		SceneSingleton<GameManager>.Instance.GoBackToMainMenu();
	}
}
