using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_SettingsTwitch : MonoBehaviour
{
	public TextMeshProUGUI m_URL;

	[Header("Key/Link")]
	public TMP_InputField m_KeyInputField;

	public Button m_KeyInputFieldGamepadButton;

	public Button m_KeyLinkButton;

	public GameObject m_LinkNotEnabledParent;

	public GameObject m_LinkEnabledParent;

	public GameObject m_LinkingAnimation;

	public PointerEvents m_KeyInputFieldPointerEvents;

	[Header("Status")]
	public Button m_EnableIntegrationButton;

	public TextMeshProUGUI m_EnableIntegrationStatusText;

	public TextMeshProUGUI m_EnableIntegrationButtonText;

	public TextMeshProUGUI m_TwitchUserName;

	[Header("Session")]
	public GameObject m_SessionContainer;

	public TextMeshProUGUI m_SessionActive;

	public Button m_StartSessionButton;

	public TextMeshProUGUI m_StartingSessionText;

	public TextMeshProUGUI m_StartSessionButtonText;

	private readonly string TWITCH_MORE_INFO_URL = "https://dashboard.twitch.tv/extensions/rfn0bf8415wza13lbenwpkhv017ja5";

	private string m_KeyFromUser;

	public void Init()
	{
		m_SessionContainer.SetActive(value: false);
		m_KeyInputField.characterLimit = 100;
		m_KeyInputField.caretWidth = 1;
		m_KeyInputField.selectionColor = GameUI.m_Instance.m_InputFieldSelectColor;
		m_KeyInputField.text = string.Empty;
		m_KeyLinkButton.onClick.AddListener(OnKeyLink);
		m_KeyInputFieldGamepadButton.onClick.AddListener(OnKeyInputFieldGamepadButton);
		m_LinkingAnimation.SetActive(value: false);
		m_LinkNotEnabledParent.SetActive(value: true);
		m_LinkEnabledParent.gameObject.SetActive(value: false);
	}

	public void UpdateManual()
	{
		if (GameInput.GetMouseButtonJustPressed(1) && m_KeyInputFieldPointerEvents.m_IsHovering && GameUI.m_TextEditor.CanPaste())
		{
			GameUI.m_TextEditor.Paste();
			m_KeyInputField.text = GameUI.m_TextEditor.text;
			GameUI.m_TextEditor.text = string.Empty;
		}
		UpdateURL();
		m_SessionContainer.SetActive(PolyTwitch.m_Authorized);
		m_SessionActive.text = (PolyTwitch.m_StreamStarted ? GameUI.MarkupForGold(Localize.Get("UI_YES")) : GameUI.MarkupForGold(Localize.Get("UI_NO")));
		m_EnableIntegrationStatusText.text = (PolyTwitch.m_Authorized ? GameUI.MarkupForGold(Localize.Get("UI_YES")) : GameUI.MarkupForGold(Localize.Get("UI_NO")));
		m_EnableIntegrationButtonText.text = (PolyTwitch.m_Authorized ? Localize.Get("UI_UNLINK") : Localize.Get("UI_ENABLE"));
		string rawText = (string.IsNullOrEmpty(Profiles.m_ActiveProfile.m_TwitchUsername) ? string.Empty : $" [{Profiles.m_ActiveProfile.m_TwitchUsername}]");
		GameUI.SetAndEnableText(m_TwitchUserName, rawText);
		m_StartingSessionText.gameObject.SetActive(PolyTwitch.m_StreamStarting);
		m_StartingSessionText.text = (PolyTwitch.m_StreamStarting ? Localize.Get("UI_SETTINGS_TWITCH_SESSION_STARTING") : Localize.Get("UI_SETTINGS_TWITCH_SESSION_STOPPING"));
		m_StartSessionButton.gameObject.SetActive(!m_StartingSessionText.gameObject.activeInHierarchy);
		m_StartSessionButtonText.text = (PolyTwitch.m_StreamStarted ? Localize.Get("UI_SETTINGS_TWITCH_STOP_SESSION") : Localize.Get("UI_SETTINGS_TWITCH_START_SESSION"));
	}

	public void OnEnableManual()
	{
		m_StartSessionButton.onClick.AddListener(OnStartSession);
		m_EnableIntegrationButton.onClick.AddListener(OnEnableIntegration);
		m_LinkNotEnabledParent.SetActive(!PolyTwitch.m_Authorized);
		m_KeyLinkButton.gameObject.SetActive(!PolyTwitch.m_Authorized);
		m_LinkEnabledParent.gameObject.SetActive(PolyTwitch.m_Authorized);
		UpdateManual();
	}

	public void OnDisableManual()
	{
		m_StartSessionButton.onClick.RemoveAllListeners();
		m_EnableIntegrationButton.onClick.RemoveAllListeners();
		m_LinkingAnimation.SetActive(value: false);
	}

	public void UpdateForCurrentDevice()
	{
		m_KeyInputField.interactable = !GamepadVirtualKeyboard.IsSupported();
		m_KeyInputFieldGamepadButton.gameObject.SetActive(GamepadVirtualKeyboard.IsSupported());
	}

	public void Apply()
	{
	}

	public void StartSessionCallback(bool success)
	{
		m_LinkingAnimation.SetActive(value: false);
		if (success)
		{
			m_LinkNotEnabledParent.SetActive(value: false);
			m_LinkEnabledParent.gameObject.SetActive(value: true);
			try
			{
				File.WriteAllText(PolyTwitch.GetCachedKeyPathAndFilename(), PolyTwitch.m_Key);
				return;
			}
			catch (Exception ex)
			{
				Debug.LogWarningFormat("Exception '{0}' trying to write key to '{1}'", ex.Message.ToString(), PolyTwitch.GetCachedKeyPathAndFilename());
				return;
			}
		}
		PolyTwitch.DeAuthorize();
		m_LinkNotEnabledParent.SetActive(value: true);
		m_LinkEnabledParent.gameObject.SetActive(value: false);
		m_KeyLinkButton.gameObject.SetActive(value: true);
	}

	private void UpdateURL()
	{
		FontStyles fontStyle = m_URL.fontStyle;
		m_URL.fontStyle = (IsPointerOverURL() ? FontStyles.Underline : FontStyles.Normal);
		if (fontStyle != m_URL.fontStyle && m_URL.fontStyle == FontStyles.Underline)
		{
			InterfaceAudio.Play("ui_menu_hover");
		}
		if (GameInput.GetMouseButtonJustPressed(0) && IsPointerOverURL())
		{
			InterfaceAudio.Play("ui_menu_select");
			Application.OpenURL(TWITCH_MORE_INFO_URL);
		}
	}

	private bool IsPointerOverURL()
	{
		if (m_URL.gameObject.activeInHierarchy)
		{
			return TMP_TextUtilities.IsIntersectingRectTransform(m_URL.rectTransform, GameInput.GetMousePosition(), null);
		}
		return false;
	}

	private void OnStartSession()
	{
		if (!PolyTwitch.m_Authorized)
		{
			PolyTwitch.AuthorizeWithKey(m_KeyInputField.text.Trim());
		}
		if (!PolyTwitch.m_StreamStarted)
		{
			PolyTwitch.StartStream();
			m_LinkingAnimation.SetActive(value: true);
		}
		else
		{
			PolyTwitch.StopStream();
		}
	}

	private void OnEnableIntegration()
	{
		if (PolyTwitch.m_Authorized)
		{
			if (PolyTwitch.m_StreamStarted)
			{
				PolyTwitch.StopStream();
			}
			PolyTwitch.DeAuthorize();
			PolyTwitch.DeleteCachedToken();
			m_LinkNotEnabledParent.SetActive(value: true);
			m_KeyLinkButton.gameObject.SetActive(value: true);
			m_LinkEnabledParent.gameObject.SetActive(value: false);
		}
	}

	private void OnKeyLink()
	{
		if (string.IsNullOrEmpty(m_KeyInputField.text.Trim()))
		{
			PopUpMessage.DisplayWarning(Localize.Get("WARN_POLYTWITCH_PASTE_KEY"), useYesNoLables: false, null);
			return;
		}
		OnStartSession();
		m_KeyLinkButton.gameObject.SetActive(value: false);
	}

	private void OnKeyInputFieldGamepadButton()
	{
		GamepadVirtualKeyboard.MaybeOpenVirtualKeyboard(m_KeyInputField.text, m_KeyInputField.characterLimit, string.Empty, multiline: false, OnKeyEntered);
	}

	private void OnKeyEntered(string text)
	{
		if (text != null)
		{
			m_KeyInputField.text = text;
		}
	}
}
