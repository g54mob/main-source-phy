using System;
using Localisation;
using UnityEngine;

public class ClientSettingsScreen : MonoBehaviour
{
	public GameObject clientSettings;

	public GameObject serverSettings;

	public UIButton acceptButton;

	public UIButton discardButton;

	public UIButton copyIPButton;

	public TextMesh clientText;

	public UIButton disconnectButton;

	public TextMesh hostText;

	public GameObject spectatorButton;

	public SimpleMenuSlider sendRateSlider;

	public SimpleMenuSlider camUpdateSlider;

	public SimpleMenuDropDown skipChildDropDown;

	public SimpleMenuSlider vecThresholdSlider;

	public SimpleMenuSlider rotThresholdSlider;

	private NetworkAuxAddPiece auxAddPiece;

	private FloatExtraOption sendRateSettings;

	private FloatExtraOption camUpdateSettings;

	private FloatExtraOption vecThresholdSettings;

	private FloatExtraOption rotThresholdSettings;

	private ExtraOption skipChildCountSettings;

	public SimpleMenuSlider smoothnessSlider;

	private FloatExtraOption smoothnessSettings;

	private bool isApplyingSettings;

	private bool settingsChanged;

	private ServerSettings settings;

	private bool isServer;

	private string currentIP;

	private NetworkAddPiece addPiece;

	private bool addedToSimulate;

	protected void Awake()
	{
		isApplyingSettings = true;
		acceptButton.Click += OnAccept;
		discardButton.Click += OnDiscard;
		copyIPButton.Click += OnCopyIP;
		disconnectButton.Click += OnDisconnect;
		sendRateSettings = new FloatExtraOption(string.Empty, OptionsMaster.defaultSendRate, string.Empty, OnSettingsChanged);
		sendRateSlider.floatExtraOption = sendRateSettings;
		sendRateSlider.min = OptionsMaster.minSendRate;
		sendRateSlider.max = OptionsMaster.maxSendRate;
		skipChildCountSettings = new ExtraOption(string.Empty, new object[4] { 0, 1, 2, 3 }, new string[4] { "0", "1", "2", "3" }, OnSettingsChanged);
		skipChildCountSettings.resetIndex = OptionsMaster.defaultSkipChildCount;
		skipChildDropDown.extraOption = skipChildCountSettings;
		camUpdateSettings = new FloatExtraOption(string.Empty, OptionsMaster.defaultCamUpdateRate, string.Empty, OnSettingsChanged);
		camUpdateSlider.floatExtraOption = camUpdateSettings;
		camUpdateSlider.min = OptionsMaster.minCamUpdateRate;
		camUpdateSlider.max = OptionsMaster.maxCamUpdateRate;
		vecThresholdSettings = new FloatExtraOption(string.Empty, OptionsMaster.defaultVecThreshold, string.Empty, OnSettingsChanged);
		vecThresholdSlider.floatExtraOption = vecThresholdSettings;
		vecThresholdSlider.min = OptionsMaster.minVecThreshold;
		vecThresholdSlider.max = OptionsMaster.maxVecThreshold;
		rotThresholdSettings = new FloatExtraOption(string.Empty, OptionsMaster.defaultRotThreshold, string.Empty, OnSettingsChanged);
		rotThresholdSlider.floatExtraOption = rotThresholdSettings;
		rotThresholdSlider.min = OptionsMaster.minRotThreshold;
		rotThresholdSlider.max = OptionsMaster.maxRotThreshold;
		smoothnessSettings = new FloatExtraOption(string.Empty, OptionsMaster.defaultSmoothness, string.Empty, OnSettingsChanged);
		smoothnessSlider.floatExtraOption = smoothnessSettings;
		smoothnessSlider.min = OptionsMaster.minSmoothness;
		smoothnessSlider.max = OptionsMaster.maxSmoothness;
		isApplyingSettings = false;
	}

	protected void OnEnable()
	{
		isServer = StatMaster.isHosting;
		clientSettings.SetActive(StatMaster.isClient);
		serverSettings.SetActive(StatMaster.isHosting);
		clientText.text = ((!isServer) ? LocalisationManager.GetTranslation(2036) : LocalisationManager.GetTranslation(2035));
		settingsChanged = false;
		ApplySettings(NetworkScene.ServerSettings);
		StatMaster.SetInMenu(true);
		AddToSimulateMessage();
		OnSimulationToggled(OptionsMaster.votingEnabled && StatMaster.levelSimulating);
	}

	protected void OnDisable()
	{
		if (addedToSimulate)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
			addedToSimulate = false;
		}
	}

	private void AddToSimulateMessage()
	{
		if (!addedToSimulate)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
			addedToSimulate = true;
		}
	}

	protected void Start()
	{
		auxAddPiece = NetworkAuxAddPiece.Instance;
	}

	public void OnSimulationToggled(bool toggle)
	{
		if (toggle)
		{
			if (OptionsMaster.votingEnabled)
			{
				spectatorButton.SetActive(false);
			}
		}
		else
		{
			spectatorButton.SetActive(true);
		}
	}

	private void UpdateIP(string ip)
	{
		if (string.IsNullOrEmpty(ip))
		{
			ip = LocalisationManager.GetTranslation(1934);
		}
		hostText.text = string.Format(LocalisationManager.GetTranslation(2034), ip);
		currentIP = ip;
	}

	private void OnCopyIP()
	{
		GUIUtility.systemCopyBuffer = currentIP;
	}

	public void ApplySettings(ServerSettings serverSettings)
	{
		settings = serverSettings;
		isApplyingSettings = true;
		currentIP = ((!isServer) ? OptionsMaster.BesiegeConfig.LastConnectedAddress : BesiegeNetworkManager.Instance.ExternalIP);
		UpdateIP(currentIP);
		if (isServer)
		{
			sendRateSlider.SetValue(settings.sendRate);
			camUpdateSlider.SetValue(settings.camUpdateRate);
			skipChildDropDown.SetValue(settings.skipChildCount.ToString());
			vecThresholdSlider.SetValue(settings.vecThreshold);
			rotThresholdSlider.SetValue(settings.rotThreshold);
		}
		else
		{
			smoothnessSlider.SetValue(settings.smoothness);
		}
		isApplyingSettings = false;
	}

	private void OnDisconnect()
	{
		CloseWindow();
		NetworkScene instance = NetworkScene.Instance;
		instance.ManualStop();
	}

	private void OnSettingsChanged(object obj)
	{
		if (!isApplyingSettings)
		{
			settingsChanged = true;
		}
	}

	private void CloseWindow()
	{
		settingsChanged = false;
		StatMaster.SetInMenu(false);
		base.gameObject.SetActive(false);
	}

	private void OnAccept()
	{
		if (settingsChanged)
		{
			if (isServer)
			{
				settings.sendRate = sendRateSlider.GetValue();
				settings.camUpdateRate = camUpdateSlider.GetValue();
				settings.skipChildCount = Convert.ToInt32(skipChildDropDown.GetValue());
				settings.vecThreshold = vecThresholdSlider.GetValue();
				settings.rotThreshold = rotThresholdSlider.GetValue();
				auxAddPiece.OnServerSettingsChanged();
			}
			else
			{
				settings.smoothness = smoothnessSlider.GetValue();
			}
		}
		CloseWindow();
	}

	private void OnDiscard()
	{
		CloseWindow();
	}
}
