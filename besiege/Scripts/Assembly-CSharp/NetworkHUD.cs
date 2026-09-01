using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using BesiegeDlc;
using GameGrind;
using InternalModding.Loading;
using InternalModding.Mods;
using InternalModding.UI;
using Localisation;
using Modding;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

[AddComponentMenu("UI/Multiplayer/Network HUD")]
public class NetworkHUD : MonoBehaviour
{
	public GameObject multiverseUI;

	public UIButton abortBtn;

	public static bool connecting;

	public GameObject connectingWidget;

	public TextMesh connectingText;

	public PlayerLabelManager playerLabelManager;

	public PlayerViewer playerViewer;

	public LevelSettingsScreen levelSettingsScreen;

	public GameObject[] hideInMenu;

	public Scoreboard scoreBoard;

	public MachineInfoHUD infoHUD;

	public GameObject playlistEditorBtn;

	public ServerPasswordDialog passwordDialog;

	public FadeInOnEnable levelMenu;

	public GameObject[] hideOnReset;

	public GameObject canvasObj;

	public GameObject loadLastMachineWarning;

	public TextMesh loadLastMachineText;

	public UIButton loadLastAcceptButton;

	public ToggleSpectator toggleSpectatorButton;

	public GameObject clearLevelWarning;

	public UIButton clearLevelAcceptButton;

	public LevelPlaylistManager playlistManager;

	public AllowedMachinesWindow allowedMachinesWindow;

	[Header("Top Bar References")]
	public TranslateButton translateButton;

	public MachineRotation rotateButton;

	public MirrorButton mirrorButton;

	public SymmetryButton symmetryButton;

	public EraseButton eraseButton;

	public KeyMapModeButton keymapButton;

	public BinButton binButton;

	public ToggleSettings statsButton;

	public DisableBoundsButton boundsButton;

	public ToggleSettings settingsButton;

	[Header("God Tools References")]
	public ToggleInfiniteAmmo infiniteAmmo;

	public ToggleInvincible invincibility;

	public ToggleExplodingCannonballs cannonballs;

	public ToggleZeroG zeroG;

	public ToggleDragMode dragMode;

	public TogglePYRO pyro;

	[Header("Options References")]
	public ToggleClusterHighlight clusterView;

	[HideInInspector]
	public MachineInfo prevBuild;

	private float pingUpdateInterval = 0.5f;

	private float currentPingDelta;

	private StatMaster.Tool prevState = StatMaster.Tool.None;

	private bool prevSymmetry;

	private bool prevFreeBuild;

	private bool prevInfAmmo;

	private bool prevInvincible;

	private bool prevCannons;

	private Vector3 prevBuildPos = Vector3.zero;

	private Quaternion prevBuildRot = Quaternion.identity;

	private bool prevClusterView;

	private LevelEditor levelEditor;

	private NetworkAuxAddPiece auxAddPiece;

	private NetworkAddPiece addPiece;

	private Regex ipRegex;

	private bool uiActive;

	private Coroutine conwidgetrt;

	public Vector3 LastBuildPosition
	{
		get
		{
			return prevBuildPos;
		}
	}

	public Quaternion LastBuildRotation
	{
		get
		{
			return prevBuildRot;
		}
	}

	public void Awake()
	{
		loadLastAcceptButton.Click += OnLastLoadAccept;
		clearLevelAcceptButton.Click += OnClearLevelAccept;
		ReferenceMaster.onJoin = OnJoinClicked;
		ReferenceMaster.onJoinPF = OnJoinPFClicked;
		ReferenceMaster.onHost = OnHostClicked;
		ipRegex = new Regex("([0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3})", RegexOptions.None);
	}

	public bool IsValidIP(string ipAddressString)
	{
		IPAddress address;
		return ipRegex.IsMatch(ipAddressString) && IPAddress.TryParse(ipAddressString, out address);
	}

	public void ToggleAbortButton(bool toggle)
	{
		if (toggle != abortBtn.gameObject.activeSelf)
		{
			if (toggle)
			{
				abortBtn.Click += OnAbortConnection;
			}
			else
			{
				abortBtn.Click -= OnAbortConnection;
			}
			abortBtn.gameObject.SetActive(toggle);
		}
	}

	public void Start()
	{
		ToggleAbortButton(false);
		auxAddPiece = NetworkAuxAddPiece.Instance;
		levelEditor = LevelEditor.Instance;
		multiverseUI.SetActive(false);
		uiActive = false;
		ToggleMultiverseOptions(true);
		if (auxAddPiece != null)
		{
			auxAddPiece.hud = this;
		}
		allowedMachinesWindow.Init(this);
		addPiece = NetworkAddPiece.Instance;
	}

	private void OnAbortConnection()
	{
		ToggleAbortButton(false);
		NetworkScene.Instance.ForceAbort();
	}

	public void UpdatePlayers()
	{
		if (!StatMaster.isHeadless)
		{
			playerLabelManager.Clear();
			int num = 0;
			for (num = 0; num < Playerlist.Players.Count; num++)
			{
				PlayerData player = Playerlist.Players[num];
				playerLabelManager.UpdateLabel(num, player);
			}
			playerViewer.ClearObsoletePlayers(num);
		}
		if (OptionsMaster.votingEnabled)
		{
			addPiece.RefreshPlayerViewer();
		}
	}

	public void SetOwner(ushort owner)
	{
		playerLabelManager.SetOwner(owner);
	}

	public void OnUpdateLevelSettings(LevelSettings settings)
	{
		if (levelSettingsScreen.gameObject.activeSelf)
		{
			levelSettingsScreen.ApplySettings(settings);
		}
		if (!StatMaster.Mode.levelEdit)
		{
			UpdateRule(zeroG, settings.IsRuleEnabled(zeroG.GetModeName()));
			UpdateRule(dragMode, settings.IsRuleEnabled(dragMode.GetModeName()));
			UpdateRule(pyro, settings.IsRuleEnabled(pyro.GetModeName()));
		}
		else
		{
			zeroG.UpdateVisual();
			dragMode.UpdateVisual();
			pyro.UpdateVisual();
		}
		UpdatePlayers();
	}

	private bool UpdateRule(ToggleGodModeButton godModeButton, bool toggle)
	{
		if (godModeButton.IsRuleOn() != toggle)
		{
			godModeButton.Set();
			return true;
		}
		godModeButton.UpdateVisual();
		return false;
	}

	public void ShowMessage(string message)
	{
		SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(message, 4f, GenericUIPopup.PopupPosition.Bottom);
	}

	public void ReceivedPassword(bool correct)
	{
		if (!correct)
		{
			ShowPasswordDialog();
		}
	}

	public void ShowPasswordDialog()
	{
		passwordDialog.gameObject.SetActive(true);
	}

	public void Update()
	{
		if (!StatMaster.isClient)
		{
			ServerHealth.Instance.SetPing(0);
			return;
		}
		currentPingDelta += Time.unscaledDeltaTime;
		if (currentPingDelta >= pingUpdateInterval)
		{
			ServerHealth.Instance.SetPing(BesiegeNetworkManager.Instance.Ping);
			currentPingDelta = 0f;
		}
	}

	private void SavePlayerName(string playerName)
	{
		OptionsMaster.BesiegeConfig.PlayerName = playerName;
	}

	private bool StartClient(string playerName)
	{
		Machine machine = Machine.Active();
		if ((bool)machine && machine.isSimulating)
		{
			ShowMessage("Please stop the simulation first!");
			return false;
		}
		if (string.IsNullOrEmpty(playerName))
		{
			ShowMessage("Invalid player name!");
			return false;
		}
		SavePlayerName(playerName);
		return true;
	}

	public void ToggleMultiverseOptions(bool toggle)
	{
		if (multiverseUI.activeSelf == toggle)
		{
			return;
		}
		ModMismatchUI.Hide();
		if (StatMaster.isHeadless || !StatMaster.isMP)
		{
			DisableConnectionWidget();
			return;
		}
		if (!StatMaster.isHeadless)
		{
			if (toggle)
			{
				levelEditor.environmentManager.SetEnvironment(LevelSettings.LevelEnvironment.LoadingMultiverse);
				GameObject[] array = hideInMenu;
				foreach (GameObject gameObject in array)
				{
					gameObject.SetActive(false);
				}
				playlistEditorBtn.SetActive(false);
				scoreBoard.Hide();
				infoHUD.Hide();
				DisableConnectionWidget();
			}
			else
			{
				EnableConnectionWidget();
			}
		}
		if (toggle)
		{
			NetworkScene.ResetMPSettings();
		}
		uiActive = toggle;
		multiverseUI.SetActive(toggle);
		StatMaster.StopHotKeys(toggle);
		StatMaster.SetInMenu(toggle);
	}

	public void SetLoadingText(string text)
	{
		if (connectingText != null)
		{
			connectingText.text = text;
		}
	}

	public void OnJoinClicked(string playerName, string serverIP, int serverPort)
	{
		if (!StartClient(playerName))
		{
			return;
		}
		if (!IsValidIP(serverIP))
		{
			string translation = LocalisationManager.GetTranslation(3554);
			ShowMessage(translation);
			return;
		}
		if (!serverIP.Equals(NetworkScene.LastIP) && !Machine.IsStartMachine(MachineObjectTracker.lastBuild))
		{
			prevBuild = MachineObjectTracker.lastBuild;
		}
		EnableConnectionWidget();
		ToggleMultiverseOptions(false);
		NetworkScene.Instance.Join(serverIP, serverPort);
	}

	public void OnJoinPFClicked(string playerName, string pfNetworkId)
	{
		if (!StartClient(playerName))
		{
			return;
		}
		if (pfNetworkId.Length < 100)
		{
			string translation = LocalisationManager.GetTranslation(17);
			ShowMessage(translation);
			return;
		}
		if (0 == 0 && !Machine.IsStartMachine(MachineObjectTracker.lastBuild))
		{
			prevBuild = MachineObjectTracker.lastBuild;
		}
		EnableConnectionWidget();
		ToggleMultiverseOptions(false);
		NetworkScene.Instance.Join(pfNetworkId);
	}

	private bool IsPortAvailable(int port)
	{
		bool result = false;
		if (!NetworkTransport.IsStarted)
		{
			NetworkTransport.Init();
		}
		HostTopology topology = new HostTopology(new ConnectionConfig(), 1);
		int num = NetworkTransport.AddHost(topology, port);
		if (num != -1)
		{
			result = true;
			NetworkTransport.RemoveHost(num);
		}
		NetworkTransport.Shutdown();
		return result;
	}

	public void OnHostClicked(string playerName, bool useLevelEditor, string serverPassword, int serverPort)
	{
		if (!StartClient(playerName))
		{
			return;
		}
		if (serverPort == 0)
		{
			ShowMessage(LocalisationManager.GetTranslation(2902));
			return;
		}
		if (!IsPortAvailable(serverPort))
		{
			ShowMessage(LocalisationManager.GetTranslation(2022));
			return;
		}
		StatMaster.initializingHostEnvironment = true;
		ServerSettings serverSettings = new ServerSettings();
		serverSettings.levelEditor = useLevelEditor;
		serverSettings.password = serverPassword;
		List<uint> localDlcTypes = DlcManager.Instance.GetLocalDlcTypes(true);
		serverSettings.dlcMask = DlcManager.Instance.GetMaskFromDlcTypes(localDlcTypes);
		Debug.Log("Setting dlcMask to " + serverSettings.dlcMask + " dlcCount=" + localDlcTypes.Count);
		serverSettings.useUPNP = ReferenceMaster.UPNPStatus == UPNPStatus.PortforwardingSucceeded;
		if (!useLevelEditor)
		{
			serverSettings.playList.Clear();
			serverSettings.playList.AddRange(playlistManager.GetPaths());
			serverSettings.playListIndex = 0;
		}
		EnableConnectionWidget();
		ToggleMultiverseOptions(false);
		prevBuild = (Machine.IsStartMachine(MachineObjectTracker.lastBuild) ? null : MachineObjectTracker.lastBuild);
		NetworkScene.Instance.Host(serverSettings, serverPort);
		StatMaster.initializingHostEnvironment = false;
	}

	public void OnStartHost()
	{
		OnConnected();
	}

	public void OnJoin()
	{
		OnConnected();
	}

	public void OnClientStop()
	{
		if (StatMaster.IsLevelEditorOnly || StartGameButton.isLoadingLevel)
		{
			NetworkScene.ResetMPSettings();
			return;
		}
		ResetUI();
		UpdatePlayers();
		ToggleMultiverseOptions(true);
		BesiegeNetworkManager instance = BesiegeNetworkManager.Instance;
		if (instance.disconnectMessage != string.Empty)
		{
			if (instance.mismatchedMods != null)
			{
				ModMismatchUI.Show(instance.mismatchedMods, true);
			}
			else
			{
				ShowMessage(instance.disconnectMessage);
			}
		}
	}

	public void HandleMismatchedMods()
	{
		BesiegeNetworkManager instance = BesiegeNetworkManager.Instance;
		bool flag = false;
		bool flag2 = false;
		try
		{
			foreach (ModList.Mod mismatchedMod in instance.mismatchedMods)
			{
				ModContainer modById = ModIds.GetModById(mismatchedMod.Id, true);
				if (mismatchedMod.Mismatch == ModList.MismatchType.MissingLocally && modById != null)
				{
					ModStatus.EnableMod(modById);
				}
				else if (mismatchedMod.Mismatch == ModList.MismatchType.MissingOnServer)
				{
					if (!ModStatus.DisableMod(modById))
					{
						flag = true;
						flag2 = true;
					}
				}
				else
				{
					flag = true;
				}
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			flag = true;
		}
		if (flag)
		{
			ModMismatchUI.Show(instance.mismatchedMods, false);
			if (flag2)
			{
				SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(LocalisationManager.GetTranslation(3570), 10f, GenericUIPopup.PopupPosition.Bottom);
			}
			return;
		}
		Debug.Log("[NetworkHUD] All mismatched mods automatically resolved, rejoining server");
		if (!string.IsNullOrEmpty(NetworkScene.LastPlayfabServerID))
		{
			OnJoinPFClicked(OptionsMaster.BesiegeConfig.PlayerName, NetworkScene.LastIP);
		}
		else if (NetworkScene.LastSteamLobbyID.HasValue)
		{
			NetworkScene.Instance.ConnectToLobby(NetworkScene.LastSteamLobbyID.Value, NetworkScene.LastPassword);
		}
		else if (NetworkScene.LastSteamServerID.HasValue)
		{
			NetworkScene.Instance.ConnectToServer(NetworkScene.LastSteamServerID.Value, NetworkScene.LastPassword);
		}
		else
		{
			OnJoinClicked(OptionsMaster.BesiegeConfig.PlayerName, NetworkScene.LastIP, NetworkScene.LastPort);
		}
	}

	public void OnToggleSpectator()
	{
		toggleSpectatorButton.Set();
	}

	public void OnConnected()
	{
		DisableConnectionWidget();
		GameObject[] array = hideInMenu;
		foreach (GameObject gameObject in array)
		{
			if (!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
			}
		}
	}

	public void DisableConnectionWidget()
	{
		connecting = false;
		if (conwidgetrt != null)
		{
			StopCoroutine(conwidgetrt);
			conwidgetrt = null;
		}
		connectingWidget.SetActive(false);
	}

	public void EnableConnectionWidget(float delay = 0f)
	{
		connecting = true;
		if (StatMaster.isHeadless)
		{
			return;
		}
		if (delay <= 0f)
		{
			if (conwidgetrt != null)
			{
				StopCoroutine(conwidgetrt);
				conwidgetrt = null;
			}
			connectingWidget.SetActive(true);
		}
		else if (conwidgetrt != null)
		{
			conwidgetrt = StartCoroutine(IEEnableConWidget(delay));
		}
	}

	private IEnumerator IEEnableConWidget(float delay)
	{
		yield return new WaitForSecondsRealtime(delay);
		connectingWidget.SetActive(true);
		conwidgetrt = null;
	}

	public void CloseAllowedMachines()
	{
		auxAddPiece.StopLoadLocalMachine();
		allowedMachinesWindow.Close();
	}

	public void ShowAllowedMachines(ServerMachine activeMachine)
	{
		List<LevelSettings.LevelMachine> allowedMachines = levelEditor.Settings.AllowedMachines;
		int count = allowedMachines.Count;
		if (levelEditor.fileBrowserView.IsOpen)
		{
			levelEditor.fileBrowserView.Close();
		}
		switch (count)
		{
		case 0:
			activeMachine.ToggleModification(!StatMaster.LimitMachineModification);
			auxAddPiece.StopLoadLocalMachine();
			CloseAllowedMachines();
			return;
		case 1:
			if (StatMaster.LimitMachineModification || activeMachine.player.allowedMachineIndex == -1)
			{
				auxAddPiece.PickAllowedMachine(0);
				return;
			}
			break;
		}
		bool flag = activeMachine.player.allowedMachineIndex != -1;
		if (!flag)
		{
			activeMachine.ToggleModification(false);
		}
		allowedMachinesWindow.ShowMachines(flag);
	}

	public void OnGameStateReceived()
	{
		auxAddPiece.receivedGameState = true;
		ToggleAbortButton(false);
		StatMaster.Mode.LevelEditor.isSelectingLevel = false;
		ServerSettings serverSettings = NetworkScene.ServerSettings;
		if (StatMaster.isHosting)
		{
			playlistEditorBtn.SetActive(true);
			if (!serverSettings.levelEditor && serverSettings.playList.Count > 0)
			{
				levelEditor.LoadPlaylistLevel(0);
			}
			else
			{
				LevelSettings levelSettings = new LevelSettings();
				levelSettings.Environment = StatMaster.DefaultLevelEnvironment;
				LevelEditor.Instance.UpdateLevelSettings(levelSettings);
				if (!string.IsNullOrEmpty(StatMaster.DefaultMPLevel))
				{
					levelEditor.LoadCustomLevel(StatMaster.DefaultMPLevel);
					StatMaster.DefaultMPLevel = null;
				}
			}
			if (StatMaster.isHeadless)
			{
				DynamicText[] array = UnityEngine.Object.FindObjectsOfType<DynamicText>();
				DynamicText[] array2 = array;
				foreach (DynamicText dynamicText in array2)
				{
					dynamicText.enabled = false;
				}
				UIButton[] array3 = UnityEngine.Object.FindObjectsOfType<UIButton>();
				UIButton[] array4 = array3;
				foreach (UIButton uIButton in array4)
				{
					uIButton.enabled = false;
				}
				LevelEditorUI instance = SingleInstanceFindOnly<LevelEditorUI>.Instance;
				instance.Toggle(false);
				instance.container.SetActive(false);
				EventSystem eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
				if (eventSystem != null)
				{
					eventSystem.gameObject.SetActive(false);
				}
			}
		}
		else if (!string.IsNullOrEmpty(NetworkScene.LastIP) && !NetworkScene.LastIP.Equals(OptionsMaster.BesiegeConfig.LastConnectedAddress))
		{
			OptionsMaster.BesiegeConfig.LastConnectedAddress = NetworkScene.LastIP;
		}
		else if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("[NetworkHUD] OnGameStateReceived Not incrementing Social One { LastConnectedAddress=" + OptionsMaster.BesiegeConfig.LastConnectedAddress + " LastIP=" + NetworkScene.LastIP + " }");
		}
		UpdatePlayers();
		PlayerData localPlayer = PlayerData.localPlayer;
		if (localPlayer.isSpectator)
		{
			if (StatMaster.isHosting)
			{
				NetworkScene.Instance.OnHostReady();
			}
			addPiece.UpdateBarController();
			if (BesiegeLogFilter.logDebug)
			{
				Debug.Log("Setup done for local spectator " + localPlayer.networkId + " " + localPlayer.name);
			}
			return;
		}
		if (StatMaster.isHosting)
		{
			levelEditor.UpdatePlayerStates();
			NetworkScene.Instance.OnHostReady();
		}
		if (BesiegeLogFilter.logDebug)
		{
			Debug.Log("Setup done for local player " + localPlayer.networkId + " " + localPlayer.name);
		}
		if (ReferenceMaster.OnGameStateReceived != null)
		{
			ReferenceMaster.OnGameStateReceived();
		}
		SingleInstance<Events>.Instance.Connected();
	}

	public void ApplyMachineRules(ServerMachine machine)
	{
		if (!StatMaster.Mode.levelEdit)
		{
			bool flag = false;
			if (UpdateRule(infiniteAmmo, levelEditor.Settings.IsRuleEnabled(infiniteAmmo.GetModeName())))
			{
				flag = true;
			}
			if (UpdateRule(invincibility, levelEditor.Settings.IsRuleEnabled(invincibility.GetModeName())))
			{
				flag = true;
			}
			if (UpdateRule(cannonballs, levelEditor.Settings.IsRuleEnabled(cannonballs.GetModeName())))
			{
				flag = true;
			}
			if (flag)
			{
				machine.UpdateGodMode();
			}
			UpdateRule(boundsButton, levelEditor.Settings.IsRuleEnabled(boundsButton.GetModeName()));
		}
		else
		{
			infiniteAmmo.UpdateVisual();
			invincibility.UpdateVisual();
			cannonballs.UpdateVisual();
			boundsButton.UpdateVisual();
		}
	}

	public void TurnOffMachineRules()
	{
		UpdateRule(infiniteAmmo, false);
		UpdateRule(invincibility, false);
		UpdateRule(cannonballs, false);
		UpdateRule(boundsButton, false);
	}

	public void OnClearLevel()
	{
		clearLevelWarning.SetActive(true);
	}

	private void OnClearLevelAccept()
	{
		clearLevelWarning.SetActive(false);
		levelEditor.OnClearLevel(false);
		FileBrowserView.SetLastSaveEntry(FileBrowserType.LocalLevels, string.Empty);
	}

	private void OnLastLoadAccept()
	{
		loadLastMachineWarning.SetActive(false);
		LoadLastMachine();
	}

	private void LoadLastMachine()
	{
		if (PlayerData.hasLocalPlayer && !PlayerData.localPlayer.isSpectator)
		{
			auxAddPiece.LoadLocalMachine(prevBuild);
		}
	}

	public void SetBuildzoneTransform(Vector3 pos, Quaternion rot)
	{
		prevBuildPos = pos;
		prevBuildRot = rot;
	}

	public void OnReconnect()
	{
		if (prevBuild != null && !Machine.IsStartMachine(prevBuild))
		{
			loadLastMachineText.text = string.Format(LocalisationManager.GetTranslation(2208), prevBuild.Blocks.Count);
			loadLastMachineWarning.SetActive(true);
		}
		RestorePlayerState();
	}

	private void RestorePlayerState()
	{
		if (prevClusterView)
		{
			clusterView.Set();
		}
		ServerMachine serverMachine = Machine.Active() as ServerMachine;
		if (!(serverMachine == null))
		{
			switch (prevState)
			{
			case StatMaster.Tool.Translate:
				translateButton.TranslateOn();
				break;
			case StatMaster.Tool.Rotate:
				rotateButton.RotateOn();
				break;
			case StatMaster.Tool.Mirror:
				mirrorButton.MirrorOn();
				break;
			case StatMaster.Tool.Modify:
				keymapButton.KeyMapOn();
				break;
			case StatMaster.Tool.Erase:
				eraseButton.EraserOn();
				break;
			case StatMaster.Tool.Paint:
				PaintButton.Instance.PaintToolOn();
				break;
			}
			if (prevSymmetry)
			{
				symmetryButton.DropDown();
			}
			if (!cannonballs.IsRuleLocked())
			{
				UpdateRule(cannonballs, prevCannons);
			}
			if (!invincibility.IsRuleLocked())
			{
				UpdateRule(invincibility, prevInvincible);
			}
			if (!infiniteAmmo.IsRuleLocked())
			{
				UpdateRule(infiniteAmmo, prevInfAmmo);
			}
			serverMachine.UpdateGodMode();
			bool flag = StatMaster.Bounding.Enabled;
			if (!boundsButton.IsRuleLocked())
			{
				UpdateRule(boundsButton, prevFreeBuild);
			}
			if (flag != StatMaster.Bounding.Enabled)
			{
				byte[] messageData = new byte[1] { (byte)(StatMaster.Bounding.Enabled ? 1u : 0u) };
				NetworkAuxAddPiece.Instance.SendNetworkMessage(RPCMessageType.ToggleBounds, messageData);
			}
		}
	}

	public void DisableMachineTools()
	{
		translateButton.TranslateOff();
		rotateButton.RotateOff();
		mirrorButton.MirrorOff();
		symmetryButton.CloseAll();
		symmetryButton.TurnOffAllAxes();
		eraseButton.OffExternal();
		keymapButton.OffExternal();
	}

	public void ResetUI()
	{
		prevState = StatMaster.Mode.selectedTool;
		prevSymmetry = symmetryButton.activey;
		DisableMachineTools();
		if (statsButton.settingsOn)
		{
			statsButton.Toggle();
		}
		prevFreeBuild = !StatMaster.Bounding.Enabled;
		if (prevFreeBuild)
		{
			boundsButton.Set();
		}
		if (settingsButton.settingsOn)
		{
			settingsButton.Toggle();
		}
		if (levelMenu.gameObject.activeInHierarchy)
		{
			levelMenu.StartCoroutine(levelMenu.Disable());
		}
		for (int i = 0; i < hideOnReset.Length; i++)
		{
			GameObject gameObject = hideOnReset[i];
			if (gameObject != null && gameObject.activeInHierarchy)
			{
				gameObject.SetActive(false);
			}
		}
		FileBrowserView fileBrowserView = UnityEngine.Object.FindObjectOfType<FileBrowserView>();
		if (fileBrowserView != null && fileBrowserView.IsOpen)
		{
			fileBrowserView.Close();
		}
		if (SingleInstance<AchievementUIList>.hasInstance())
		{
			SingleInstance<AchievementUIList>.Instance.Close();
		}
		UploadDialog uploadDialog = UnityEngine.Object.FindObjectOfType<UploadDialog>();
		if (uploadDialog != null)
		{
			UnityEngine.Object.Destroy(uploadDialog.gameObject);
		}
		ChatView chatView = UnityEngine.Object.FindObjectOfType<ChatView>();
		if (chatView != null)
		{
			chatView.Clear();
		}
		if (canvasObj != null)
		{
			canvasObj.SetActive(true);
		}
		prevInfAmmo = StatMaster.GodTools.InfiniteAmmoMode;
		if (prevInfAmmo)
		{
			infiniteAmmo.Set();
		}
		prevInvincible = StatMaster.GodTools.UnbreakableMode;
		if (prevInvincible)
		{
			invincibility.Set();
		}
		prevCannons = StatMaster.GodTools.ExplodingCannonballs;
		if (prevCannons)
		{
			cannonballs.Set();
		}
		if (StatMaster.GodTools.GravityDisabled)
		{
			zeroG.Set();
		}
		if (StatMaster.GodTools.DragMode)
		{
			dragMode.Set();
		}
		if (StatMaster.GodTools.PyroMode)
		{
			pyro.Set();
		}
		prevClusterView = StatMaster.clusterCoded;
		if (prevClusterView)
		{
			clusterView.Set();
		}
	}

	public void OnDisable()
	{
		if (uiActive)
		{
			StatMaster.StopHotKeys(false);
			StatMaster.SetInMenu(false);
		}
		connecting = false;
	}
}
