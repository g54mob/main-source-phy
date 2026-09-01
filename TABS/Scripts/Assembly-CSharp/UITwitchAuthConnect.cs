using InControl;
using Landfall.TABS_Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITwitchAuthConnect : MonoBehaviour
{
	public TextMeshProUGUI Text;

	public TMP_InputField InputField;

	private UITwitchConnect connect;

	public Button ConnectButton;

	private LocalizeText connectButtonText;

	public Color DisabledConnectColor = Color.grey;

	public Color EnabledConnectColor = Color.green;

	public Color EnabledConnectTextColor = Color.white;

	public Color DisconnectColor = Color.white;

	public Color DisconnectTextColor = Color.white;

	private bool oldPlayerActionsEnabled;

	private bool oldInputManagerEnabled;

	private readonly bool RequireAuthentication = !Application.isEditor;

	private TwitchHandler m_twitchHandler;

	private void Awake()
	{
		m_twitchHandler = ServiceLocator.GetService<TwitchHandler>();
	}

	public void SelectText()
	{
		oldPlayerActionsEnabled = PlayerActions.Instance.Enabled;
		oldInputManagerEnabled = InputManager.Enabled;
		PlayerActions.Instance.Enabled = false;
		InputManager.Enabled = false;
	}

	public void DeselectText()
	{
		PlayerActions.Instance.Enabled = oldPlayerActionsEnabled;
		InputManager.Enabled = oldInputManagerEnabled;
	}

	public void GetAuthWebsite()
	{
		Application.OpenURL("https://twitchapps.com/tmi/");
	}

	public void TryConnectWithAuth()
	{
		if (!RequireAuthentication || Text.text.Replace("\u200b", "").Length != 0)
		{
			if (m_twitchHandler.isConnected)
			{
				m_twitchHandler.Disconnect();
				ServiceLocator.GetService<TABSTwitchHandler>().Disconnect();
			}
			else
			{
				string text = connect.Text.text.ToLower();
				m_twitchHandler.ConnectToStream(text.ToLower(), Text.text);
			}
		}
	}

	public void Update()
	{
		if (!connect)
		{
			connect = Object.FindObjectOfType<UITwitchConnect>();
			connectButtonText = ConnectButton.GetComponentInChildren<LocalizeText>();
		}
		if ((bool)m_twitchHandler && m_twitchHandler.isConnected)
		{
			ConnectButton.interactable = true;
			ConnectButton.image.color = DisconnectColor;
			connectButtonText.Text.color = DisconnectTextColor;
			connectButtonText.LocaleID = "BUTTON_DISCONNECT";
			return;
		}
		connectButtonText.LocaleID = "BUTTON_CONNECT";
		if (connect.Text.text == "" || connect.Text.text == "\u200b")
		{
			ConnectButton.interactable = false;
			ConnectButton.image.color = DisabledConnectColor;
			connectButtonText.Text.color = DisabledConnectColor;
		}
		else
		{
			ConnectButton.interactable = true;
			ConnectButton.image.color = EnabledConnectColor;
			connectButtonText.Text.color = EnabledConnectTextColor;
		}
	}
}
