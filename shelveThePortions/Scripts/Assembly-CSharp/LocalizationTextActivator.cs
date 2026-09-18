using TMPro;
using UnityEngine;

public class LocalizationTextActivator : MonoBehaviour
{
	private TextMeshProUGUI uIText;

	private TextMeshPro text;

	[SerializeField]
	private string IDString;

	[SerializeField]
	private bool changeTextAlignment;

	private LocalizationSystem ls;

	private TextMeshProUGUI ConnectedUIText
	{
		get
		{
			if (uIText == null)
			{
				uIText = GetComponent<TextMeshProUGUI>();
			}
			return uIText;
		}
	}

	private TextMeshPro ConnectedText
	{
		get
		{
			if (text == null)
			{
				text = GetComponent<TextMeshPro>();
			}
			return text;
		}
	}

	private LocalizationSystem localizationSystem
	{
		get
		{
			if (ls == null)
			{
				ls = Singleton<LocalizationSystem>.Instance;
			}
			return ls;
		}
	}

	public void SetIDString(string IDString)
	{
		this.IDString = IDString;
		UpdateText();
	}

	private void OnEnable()
	{
		localizationSystem.OnLanguageChange += UpdateText;
		UpdateText();
	}

	private void OnDisable()
	{
		if (localizationSystem != null)
		{
			localizationSystem.OnLanguageChange -= UpdateText;
		}
	}

	public void UpdateText(string IDString)
	{
		if (!(IDString == string.Empty) && IDString != null)
		{
			localizationSystem.UpdateText(ConnectedText, IDString, changeTextAlignment);
			localizationSystem.UpdateText(ConnectedUIText, IDString, changeTextAlignment);
		}
	}

	public void UpdateText(string IDString, Languages language)
	{
		if (!(IDString == string.Empty) && IDString != null && language != Languages.None)
		{
			localizationSystem.UpdateText(ConnectedText, IDString, language, changeTextAlignment);
			localizationSystem.UpdateText(ConnectedUIText, IDString, language, changeTextAlignment);
		}
	}

	public void SetText(string s)
	{
		if (ConnectedText != null)
		{
			ConnectedText.text = s;
		}
		if (ConnectedUIText != null)
		{
			ConnectedUIText.text = s;
		}
	}

	public void UpdateFormatText(string IDString, string formatString1, bool LocalizeFormatString1, string formatString2, bool LocalizeFormatString2)
	{
		if (!(IDString == string.Empty) && IDString != null)
		{
			localizationSystem.UpdateFormatText(ConnectedUIText, IDString, formatString1, LocalizeFormatString1, formatString2, LocalizeFormatString2, changeTextAlignment);
			localizationSystem.UpdateFormatText(ConnectedText, IDString, formatString1, LocalizeFormatString1, formatString2, LocalizeFormatString2, changeTextAlignment);
		}
	}

	public void UpdateFormatText(string IDString, string formatString, bool LocalizeFormatString)
	{
		if (!(IDString == string.Empty) && IDString != null)
		{
			if (formatString == "")
			{
				localizationSystem.UpdateText(ConnectedUIText, IDString, changeTextAlignment);
				localizationSystem.UpdateText(ConnectedText, IDString, changeTextAlignment);
			}
			else
			{
				localizationSystem.UpdateFormatText(ConnectedUIText, IDString, formatString, LocalizeFormatString, changeTextAlignment);
				localizationSystem.UpdateFormatText(ConnectedText, IDString, formatString, LocalizeFormatString, changeTextAlignment);
			}
		}
	}

	private void UpdateText()
	{
		if (!(IDString == string.Empty) && IDString != null)
		{
			localizationSystem.UpdateText(ConnectedUIText, IDString, changeTextAlignment);
			localizationSystem.UpdateText(ConnectedText, IDString, changeTextAlignment);
		}
	}
}
