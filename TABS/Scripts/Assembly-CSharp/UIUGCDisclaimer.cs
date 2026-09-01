using System.Collections;
using Landfall.TABS;
using TFBGames;
using UnityEngine;

public class UIUGCDisclaimer : MonoBehaviour
{
	private const string DisclaimerLocalizationKey = "DESC_TERMS_OF_USE";

	[SerializeField]
	protected string showDisclaimerKey;

	[SerializeField]
	protected string allowUGCKey;

	[SerializeField]
	[Tooltip("Seconds to delay until the popup opens")]
	protected int delayOpen;

	[Multiline]
	[SerializeField]
	protected string disclaimerText;

	[SerializeField]
	protected float maxFontSize = 44f;

	[SerializeField]
	protected UINavigationGroupManager navigationGroupManager;

	[SerializeField]
	protected SettingsInstance.Platform displayPlatforms;

	[SerializeField]
	protected string termsURL;

	[SerializeField]
	protected WorkshopCustomContentLoadingScreen workshopLoadingScreen;

	private IPlayerPrefsPlatform m_PlayerPrefs;

	private void Awake()
	{
		m_PlayerPrefs = ServiceLocator.GetService<IPlayerPrefsPlatform>();
	}

	private void Start()
	{
		SettingsInstance.Platform currentPlatform = GlobalSettingsHandler.CurrentPlatform;
		if ((displayPlatforms & currentPlatform) == 0)
		{
			return;
		}
		if (!string.IsNullOrWhiteSpace(showDisclaimerKey))
		{
			if (m_PlayerPrefs.HasKey(showDisclaimerKey))
			{
				return;
			}
			m_PlayerPrefs.SetInt(showDisclaimerKey, 1);
		}
		StartCoroutine(DelayPopup());
	}

	private void Open()
	{
		if (navigationGroupManager != null)
		{
			navigationGroupManager.SetAutoSelectInAllGroups(autoselect: false);
		}
		else
		{
			navigationGroupManager = Object.FindObjectOfType<UINavigationGroupManager>();
			navigationGroupManager.SetAutoSelectInAllGroups(autoselect: false);
		}
		ServiceLocator.GetService<ModalPanel>().PopUp("DESC_TERMS_OF_USE", "BUTTON_YES", "BUTTON_NO", "LABEL_TERMS_OF_USE", AllowUGC, Close, OpenURL, maxFontSize, isNegativeAction: false, hasCustomAction: true);
	}

	private void Close()
	{
		if (navigationGroupManager != null)
		{
			navigationGroupManager.SetAutoSelectInAllGroups(autoselect: true);
		}
	}

	private void AllowUGC()
	{
		if (!string.IsNullOrWhiteSpace(allowUGCKey))
		{
			m_PlayerPrefs.SetInt(allowUGCKey, 0);
		}
		Close();
	}

	private void OpenURL()
	{
		Application.OpenURL(termsURL);
	}

	private IEnumerator DelayPopup()
	{
		yield return new WaitForSeconds(delayOpen);
		Open();
	}
}
