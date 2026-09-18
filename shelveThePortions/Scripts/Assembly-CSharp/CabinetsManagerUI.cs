using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CabinetsManagerUI : SceneSingleton<CabinetsManagerUI>
{
	[SerializeField]
	private GameObject skillPointsPanel;

	[SerializeField]
	private LocalizationTextActivator shelfsTillNextSkillPointLocalizationTextActivator;

	[SerializeField]
	private TextMeshProUGUI completedShelfsText;

	[SerializeField]
	private TextMeshProUGUI shelfedPotionsText;

	[Space(5f)]
	[SerializeField]
	private Image potionsProgressBar;

	[SerializeField]
	private float potionsProgressBarStartPercent;

	[Space]
	[SerializeField]
	private Image shelvesProgressBar;

	[SerializeField]
	private float shelvesProgressBarStartPercent;

	private PotionsManager potionsManager;

	private CabinetsManager cabinetsManager;

	private UpgradesManager upgradesManager;

	private void Start()
	{
		potionsManager = SceneSingleton<PotionsManager>.Instance;
		cabinetsManager = SceneSingleton<CabinetsManager>.Instance;
		upgradesManager = SceneSingleton<UpgradesManager>.Instance;
		Singleton<LocalizationSystem>.Instance.OnLanguageChange += UpdateText;
		EventManager.GameStart += UpdateShelveUI;
	}

	private void OnDisable()
	{
		EventManager.GameStart -= UpdateShelveUI;
		if (Singleton<LocalizationSystem>.Instance != null)
		{
			Singleton<LocalizationSystem>.Instance.OnLanguageChange -= UpdateText;
		}
	}

	public void UpdateShelveUI()
	{
		int shelvesTillNextSkillPoint = upgradesManager.GetShelvesTillNextSkillPoint();
		if (shelvesTillNextSkillPoint == -1)
		{
			skillPointsPanel.gameObject.SetActive(value: false);
		}
		else
		{
			skillPointsPanel.gameObject.SetActive(value: true);
			shelfsTillNextSkillPointLocalizationTextActivator.UpdateFormatText("Upgrade_NextPoint", shelvesTillNextSkillPoint.ToString(), LocalizeFormatString: false);
		}
		potionsProgressBar.fillAmount = Mathf.Lerp(potionsProgressBarStartPercent, 1f, Mathf.Clamp01(potionsManager.GetPotionsUIPercent()));
		shelvesProgressBar.fillAmount = Mathf.Lerp(shelvesProgressBarStartPercent, 1f, Mathf.Clamp01(cabinetsManager.GetCompletedShelvesUIPercent()));
		shelfedPotionsText.text = potionsManager.GetPotionsUIText();
		completedShelfsText.text = cabinetsManager.GetCompletedShelvesUIText();
	}

	public void UpdateText()
	{
		int shelvesTillNextSkillPoint = upgradesManager.GetShelvesTillNextSkillPoint();
		if (shelvesTillNextSkillPoint != -1)
		{
			shelfsTillNextSkillPointLocalizationTextActivator.UpdateFormatText("Upgrade_NextPoint", shelvesTillNextSkillPoint.ToString(), LocalizeFormatString: false);
		}
	}
}
