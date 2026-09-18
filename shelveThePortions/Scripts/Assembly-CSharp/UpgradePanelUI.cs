using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelUI : MonoBehaviour
{
	[SerializeField]
	private Button button;

	[Space]
	[SerializeField]
	private TextMeshProUGUI currentText;

	[SerializeField]
	private TextMeshProUGUI skillPointsNeededText;

	[SerializeField]
	private TextMeshProUGUI infoText;

	[SerializeField]
	private TextMeshProUGUI shelvesText;

	[Space]
	[SerializeField]
	private GameObject nonCompleteObject;

	[SerializeField]
	private GameObject completeObject;

	[SerializeField]
	private GameObject shelveObject;

	public void SetPanel(int skillPointsNeeded, bool canBuy)
	{
		completeObject.SetActive(value: false);
		shelveObject.SetActive(value: false);
		nonCompleteObject.SetActive(value: true);
		infoText.gameObject.SetActive(value: false);
		skillPointsNeededText.text = skillPointsNeeded.ToString();
		button.interactable = canBuy;
	}

	public void SetShelvePanel(string shelves)
	{
		nonCompleteObject.SetActive(value: false);
		completeObject.SetActive(value: false);
		shelveObject.SetActive(value: true);
		shelvesText.text = shelves;
	}

	public void SetPanel(string info, int skillPointsNeeded, bool canBuy)
	{
		nonCompleteObject.SetActive(value: true);
		completeObject.SetActive(value: false);
		shelveObject.SetActive(value: false);
		infoText.gameObject.SetActive(value: true);
		infoText.text = info;
		skillPointsNeededText.text = skillPointsNeeded.ToString();
		button.interactable = canBuy;
	}

	public void SetPanelComplete()
	{
		infoText.gameObject.SetActive(value: false);
		shelveObject.SetActive(value: false);
		nonCompleteObject.SetActive(value: false);
		completeObject.SetActive(value: true);
	}

	public void SetPanelComplete(string info)
	{
		infoText.gameObject.SetActive(value: true);
		infoText.text = info;
		nonCompleteObject.SetActive(value: false);
		shelveObject.SetActive(value: false);
		completeObject.SetActive(value: true);
	}
}
