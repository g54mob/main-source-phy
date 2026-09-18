using TMPro;
using UnityEngine;

public class LocalizationFontChanger : MonoBehaviour
{
	private TextMeshProUGUI ConnectedUIText;

	private TextMeshPro ConnectedText;

	private LocalizationSystem localizationSystem;

	private void Awake()
	{
		ConnectedUIText = GetComponent<TextMeshProUGUI>();
		ConnectedText = GetComponent<TextMeshPro>();
	}

	private void OnEnable()
	{
		if (localizationSystem == null)
		{
			localizationSystem = Singleton<LocalizationSystem>.Instance;
		}
		if (localizationSystem != null)
		{
			localizationSystem.OnLanguageChange += UpdateFont;
			UpdateFont();
		}
	}

	private void OnDisable()
	{
		if (localizationSystem != null)
		{
			localizationSystem.OnLanguageChange -= UpdateFont;
		}
	}

	private void UpdateFont()
	{
		if (ConnectedText != null)
		{
			ConnectedText.font = Singleton<LocalizationFontStore>.Instance?.GetTMP_FontAsset(localizationSystem.GetCurrentLanguage());
		}
		if (ConnectedUIText != null)
		{
			ConnectedUIText.font = Singleton<LocalizationFontStore>.Instance?.GetTMP_FontAsset(localizationSystem.GetCurrentLanguage());
		}
	}
}
