using System;
using Localisation;
using UnityEngine;
using UnityEngine.UI;

public class MultiverseHostTab : MultiverseTab
{
	[SerializeField]
	private InputField nameField;

	[SerializeField]
	private InputField passwordField;

	[SerializeField]
	private Toggle levelEditorToggle;

	[SerializeField]
	private Toggle UPnPToggle;

	[SerializeField]
	private Button hostBtn;

	[SerializeField]
	private GameObject UPNPIcon;

	[SerializeField]
	private Image UPNPIconImage;

	[SerializeField]
	private Image UPNPButtonBg;

	[SerializeField]
	private Image UPNPToggleBg;

	[SerializeField]
	private GameObject UPNPLoadingIcon;

	[SerializeField]
	private Text UPNPToolTipText;

	[SerializeField]
	private InputField portField;

	[SerializeField]
	private MultiverseConnectionInfo multiverseConnectionInfo;

	[SerializeField]
	private Text hostButtonText;

	[SerializeField]
	private Image hostButtonLoadingImage;

	[SerializeField]
	private GameObject portItem;

	private bool isInitialized;

	private void Start()
	{
		Initialize();
	}

	private void Initialize()
	{
		if (!isInitialized)
		{
			nameField.text = OptionsMaster.BesiegeConfig.PlayerName;
			hostBtn.onClick.AddListener(OnStartHost);
			isReassigning = true;
			UPnPToggle.onValueChanged.AddListener(OnUPnPChanged);
			levelEditorToggle.isOn = OptionsMaster.BesiegeConfig.LevelEditorEnabled;
			levelEditorToggle.onValueChanged.AddListener(OnLevelEditorToggled);
			isReassigning = false;
			UPNPIconImage = UPNPIcon.GetComponent<Image>();
			ReferenceMaster.UPNPStatusChanged = (Action<UPNPStatus>)Delegate.Combine(ReferenceMaster.UPNPStatusChanged, new Action<UPNPStatus>(ToggleInteractable));
			multiverseConnectionInfo.ConnectionTestDone = OnConnectionTestDone;
			portField.onValueChanged.AddListener(OnPortFieldChanged);
			if (!SingleInstanceFindOnly<NetworkAnalyser>.Instance.DoneTesting)
			{
				ToggleHostButton(false);
			}
			else
			{
				ToggleHostButton(true);
			}
			ToggleInteractable(ReferenceMaster.UPNPStatus);
			if (SteamManager.Initialized)
			{
				UPnPToggle.gameObject.SetActive(false);
			}
			isInitialized = true;
		}
	}

	private void OnPortFieldChanged(string newValue)
	{
		int result = 0;
		if (int.TryParse(newValue, out result) && SingleInstanceFindOnly<NetworkAnalyser>.Instance.UPNPController != null)
		{
			SingleInstanceFindOnly<NetworkAnalyser>.Instance.UPNPController.Port = result;
		}
	}

	private void OnEnable()
	{
		Initialize();
		OptionsMaster.BesiegeConfig.LevelEditorEnabled = levelEditorToggle.isOn;
		levelEditorToggle.isOn = OptionsMaster.BesiegeConfig.LevelEditorEnabled;
		OnLevelEditorToggled(OptionsMaster.BesiegeConfig.LevelEditorEnabled);
		UpdateUI();
	}

	public override void UpdateUI()
	{
		portItem.SetActive(OptionsMaster.networkType != PlayerNetworkType.Playfab);
	}

	private void OnLevelEditorToggled(bool toggle)
	{
		OptionsMaster.BesiegeConfig.LevelEditorEnabled = toggle;
		StatMaster.Mode.levelEdit = toggle;
	}

	private void ToggleHostButtonLoader(bool toggleOn)
	{
		hostButtonText.enabled = !toggleOn;
		hostButtonLoadingImage.enabled = toggleOn;
	}

	private void ToggleHostButton(bool toggleOn)
	{
		hostBtn.interactable = toggleOn;
		ScaleOnMouseOverUI component = hostBtn.GetComponent<ScaleOnMouseOverUI>();
		if (component != null)
		{
			component.enabled = toggleOn;
		}
		ToggleHostButtonLoader(!toggleOn);
	}

	private void OnConnectionTestDone()
	{
		ToggleHostButton(true);
	}

	private void ToggleInteractable(UPNPStatus status)
	{
		switch (status)
		{
		case UPNPStatus.Initializing:
		case UPNPStatus.ForwardingPort:
			UPNPIcon.SetActive(false);
			UPnPToggle.enabled = false;
			UPNPLoadingIcon.SetActive(true);
			UPNPToolTipText.text = LocalisationManager.GetTranslation(1908);
			break;
		case UPNPStatus.FailedToInitialize:
			UPNPIcon.SetActive(true);
			UPnPToggle.enabled = false;
			UPNPLoadingIcon.SetActive(false);
			UPNPToolTipText.text = LocalisationManager.GetTranslation(1909);
			UPNPIconImage.color = new Color(0.7f, 0.7f, 0.7f);
			UPNPButtonBg.enabled = false;
			UPNPToggleBg.color = new Color(0.24f, 0.24f, 0.24f);
			break;
		case UPNPStatus.PortforwardingSucceeded:
			UPnPToggle.enabled = true;
			UPnPToggle.isOn = true;
			UPNPIcon.SetActive(true);
			UPNPLoadingIcon.SetActive(false);
			UPNPToolTipText.text = LocalisationManager.GetTranslation(1910);
			break;
		default:
			UPnPToggle.enabled = true;
			UPNPIcon.SetActive(true);
			UPNPLoadingIcon.SetActive(false);
			UPNPToolTipText.text = LocalisationManager.GetTranslation(1910);
			break;
		}
	}

	private void OnUPnPChanged(bool useUPnP)
	{
		if (ReferenceMaster.onTogglePortForwarding == null)
		{
			Debug.LogError("Can not toggle port forwarding because onTogglePortForwarding listener is missing.");
			UPnPToggle.onValueChanged.RemoveListener(OnUPnPChanged);
			UPnPToggle.isOn = !useUPnP;
			UPnPToggle.onValueChanged.AddListener(OnUPnPChanged);
		}
		else
		{
			ReferenceMaster.onTogglePortForwarding(useUPnP);
		}
	}

	private void OnStartHost()
	{
		if (ReferenceMaster.onHost == null)
		{
			Debug.LogError("Could not start host, onHost listener is missing.");
			return;
		}
		int result = 0;
		int.TryParse(portField.text, out result);
		ReferenceMaster.onHost(nameField.text, levelEditorToggle.isOn, passwordField.text, result);
	}

	private void OnDestroy()
	{
		ReferenceMaster.UPNPStatusChanged = (Action<UPNPStatus>)Delegate.Remove(ReferenceMaster.UPNPStatusChanged, new Action<UPNPStatus>(ToggleInteractable));
		UPnPToggle.onValueChanged.RemoveListener(OnUPnPChanged);
		hostBtn.onClick.RemoveListener(OnStartHost);
		portField.onValueChanged.RemoveListener(OnPortFieldChanged);
	}
}
