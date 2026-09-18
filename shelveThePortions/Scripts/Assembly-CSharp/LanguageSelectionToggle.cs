using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class LanguageSelectionToggle : MonoBehaviour
{
	[ReadOnly]
	public Languages toggleLanguage;

	[SerializeField]
	private TextMeshProUGUI text;

	[SerializeField]
	private LocalizationTextActivator localizationTextActivator;

	[SerializeField]
	private Button toggleButton;

	[Space]
	[SerializeField]
	private GameObject tickObject;

	public void SetText(Languages language)
	{
		toggleLanguage = language;
		localizationTextActivator.UpdateText("Option_LanguageName", toggleLanguage);
	}

	public void SetToggle(bool flag)
	{
		tickObject.SetActive(flag);
	}

	public void SetSelectionInteractable(bool flag)
	{
		toggleButton.interactable = flag;
	}

	public void ToggleClicked()
	{
		SceneSingleton<UILocalizationManager>.Instance.ApplyLanguage(toggleLanguage);
	}
}
