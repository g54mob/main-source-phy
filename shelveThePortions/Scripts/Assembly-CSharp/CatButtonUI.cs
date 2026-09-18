using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatButtonUI : MonoBehaviour
{
	[SerializeField]
	private Button button;

	[SerializeField]
	private GameObject activePanel;

	[SerializeField]
	private GameObject notActivePanel;

	[SerializeField]
	private TextMeshProUGUI shelvesText;

	[Space]
	[SerializeField]
	private Image cooldownFillImage;

	[SerializeField]
	private TextMeshProUGUI cooldownText;

	public void UpdatePanel(int maxShelvesNeeded, bool canHighlight)
	{
		int completedShelvesCount = SceneSingleton<CabinetsManager>.Instance.GetCompletedShelvesCount();
		if (completedShelvesCount < maxShelvesNeeded)
		{
			activePanel.SetActive(value: false);
			notActivePanel.SetActive(value: true);
			shelvesText.text = completedShelvesCount + "/" + maxShelvesNeeded;
		}
		else
		{
			activePanel.SetActive(value: true);
			notActivePanel.SetActive(value: false);
			button.interactable = canHighlight;
			cooldownFillImage.gameObject.SetActive(!canHighlight);
		}
	}

	public void UpdateTimer(float percent, int secondsLeft)
	{
		cooldownFillImage.gameObject.SetActive(value: true);
		cooldownFillImage.fillAmount = percent;
		cooldownText.text = secondsLeft + "s";
	}
}
