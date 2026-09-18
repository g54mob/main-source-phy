using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUI : MonoBehaviour
{
	[SerializeField]
	private GameObject abilityActivePanel;

	[Space]
	[SerializeField]
	private GameObject abilityTimerPanel;

	[SerializeField]
	private Image timerImage;

	[SerializeField]
	private TextMeshProUGUI timerText;

	[Space]
	[SerializeField]
	private GameObject lockedPanel;

	[SerializeField]
	private TextMeshProUGUI neededCabinetsText;

	public void ActivatePanel()
	{
		DisablePanels();
		abilityActivePanel.SetActive(value: true);
	}

	public void SetTimer(int seconds, float percent)
	{
		DisablePanels();
		abilityTimerPanel.SetActive(value: true);
		timerImage.fillAmount = 1f - percent;
		timerText.text = seconds.ToString();
	}

	public void SetLocked(int neededCabinets)
	{
		DisablePanels();
		lockedPanel.SetActive(value: true);
		neededCabinetsText.text = neededCabinets.ToString();
	}

	private void DisablePanels()
	{
		abilityTimerPanel.SetActive(value: false);
		abilityActivePanel.SetActive(value: false);
		lockedPanel.SetActive(value: false);
	}
}
