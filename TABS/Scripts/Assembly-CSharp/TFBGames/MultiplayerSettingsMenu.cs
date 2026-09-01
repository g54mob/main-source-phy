using System;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS_Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class MultiplayerSettingsMenu : UIComponentMainMenu, ISettingsItemHandler
	{
		private delegate void GameRulesChanged();

		[SerializeField]
		private LocalMultiplayerSettingsButton turnStyle;

		[SerializeField]
		private LocalMultiplayerSettingsButton splitScreen;

		[SerializeField]
		private LocalMultiplayerSettingsButton maxUnitPoints;

		[SerializeField]
		private LocalMultiplayerSettingsButton timedPlacement;

		[SerializeField]
		private LocalMultiplayerSettingsButton time;

		[SerializeField]
		private LocalMultiplayerSettingsButton unitCap;

		[SerializeField]
		private Button okButton;

		private LocalMultiplayerGameRules localMultiplayerGameRules;

		private PlayerActions playerActions;

		private SettingsProfileManager settingsProfileManager;

		private ProjectMarsGameSettingsAsset onlineGameSettings;

		private INetworkService networkService;

		private NetworkBattleController networkBattleController;

		private IUISelectionItem currentSettingsItem;

		private readonly List<string> boolStrings = new List<string> { "LABEL_DISABLED", "LABEL_ENABLED" };

		private readonly List<string> turnStyleStrings = new List<string> { "MP_SETTING_NORMAL", "MP_SETTING_HALFNHALF" };

		private readonly List<string> splitScreenStrings = new List<string> { "MP_SETTING_HORIZONTAL", "MP_SETTING_VERTICAL" };

		[SerializeField]
		private List<string> onlineTurnStyleStrings;

		private List<Selectable> selectables;

		private GameRulesChanged onGameRulesChanged;

		private ModalPanel modalPanel;

		private int? waitingOpenId;

		private bool IsOnline { get; set; }

		private NetworkBattleController BattleController
		{
			get
			{
				if (networkBattleController == null)
				{
					networkBattleController = ServiceLocator.GetService<NetworkBattleController>();
				}
				return networkBattleController;
			}
		}

		public event Action OnMenuOpen;

		public event Action OnMenuClose;

		protected override void OnOpen()
		{
			base.OnOpen();
			Init();
			this.OnMenuOpen?.Invoke();
		}

		protected override void OnClose()
		{
			base.OnClose();
			this.OnMenuClose?.Invoke();
		}

		public void Init()
		{
			InitServices();
			selectables = new List<Selectable>
			{
				turnStyle.GetSelectable(),
				splitScreen.GetSelectable(),
				maxUnitPoints.GetSelectable(),
				timedPlacement.GetSelectable(),
				time.GetSelectable(),
				unitCap.GetSelectable(),
				okButton
			};
			if (IsOnline)
			{
				LoadOnlineSettings();
			}
			else
			{
				if (ServiceLocator.GetService<GameModeService>().CurrentGameMode is MainMenuGameMode)
				{
					localMultiplayerGameRules.ResetGameRulesToDefault();
				}
				LoadLocalMultiplayerGameRules();
			}
			UpdateVisibleSettings();
			UIHelpers.CreateExplicitLinearNavigation(selectables, horizontal: false);
			LocalMultiplayerSettingsButton localMultiplayerSettingsButton = (IsOnline ? maxUnitPoints : turnStyle);
			if (EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(localMultiplayerSettingsButton.GetSelectable().gameObject);
			}
			currentSettingsItem = localMultiplayerSettingsButton.UISettingsItem;
		}

		private void UpdateVisibleSettings()
		{
			bool isOnline = IsOnline;
			splitScreen.gameObject.SetActive(!isOnline);
			timedPlacement.gameObject.SetActive(!isOnline);
			time.gameObject.SetActive(!isOnline);
			unitCap.gameObject.SetActive(isOnline);
			string text = "";
			text = ((!isOnline) ? "BUTTON_OK" : "MP_BUTTON_PROPOSE_SETTINGS");
			okButton.GetComponentInChildren<LocalizeText>().LocaleID = text;
		}

		protected override void Awake()
		{
			base.Awake();
			IsOnline = ServiceLocator.GetService<GameModeService>()?.CurrentGameMode is OnlineMultiplayerGameMode;
		}

		protected override void Start()
		{
			base.Start();
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			playerActions = PlayerActions.Instance;
			networkService = ServiceLocator.GetService<INetworkService>();
		}

		protected override void Update()
		{
			base.Update();
			if (currentSettingsItem != null)
			{
				currentSettingsItem.HandleInput(playerActions);
			}
			UpdateClosePanel();
		}

		public void SelectedItem(IUISelectionItem item)
		{
			currentSettingsItem = item;
		}

		public void DeselectedItem(IUISelectionItem item)
		{
			currentSettingsItem = null;
		}

		public void SaveSettings()
		{
			if (IsOnline)
			{
				SendSettingsProposal();
			}
			else
			{
				localMultiplayerGameRules.ApplySettings(turnStyle, splitScreen, maxUnitPoints, timedPlacement, time);
			}
		}

		private void UpdateClosePanel()
		{
			if (waitingOpenId.HasValue && (!networkService.IsRunning || !networkService.IsConnected))
			{
				CloseWaitingForResponsePopup();
			}
		}

		public void CloseWaitingForResponsePopup()
		{
			if (waitingOpenId.HasValue)
			{
				if (!modalPanel.IsPopupOpen || waitingOpenId.Value != modalPanel.OpenId)
				{
					waitingOpenId = null;
					return;
				}
				waitingOpenId = null;
				modalPanel.CloseWaitPopup();
			}
		}

		private void SendSettingsProposal()
		{
			bool blindMode = turnStyle.Index == 1;
			BattleController.RequestChangeRule((int)unitCap.SliderData.current, (int)maxUnitPoints.SliderData.current, blindMode);
			base.Close();
			waitingOpenId = modalPanel.WaitPopUp("MP_POPUP_WAITING_FOR_PROPOSAL_ANSWER", -1f, null, null);
		}

		public void ShowProposedMultiplayerSettings(ProjectMarsGameSettingsAsset proposedSettings)
		{
			modalPanel = ServiceLocator.GetService<ModalPanel>();
			modalPanel.ProposeMarsSettings(proposedSettings, AcceptMarsProposal, RejectMarsProposal);
		}

		public void AcceptMarsProposal(ProjectMarsGameSettingsAsset proposedSettings)
		{
			ApplyProjectMarsSettings(proposedSettings);
			base.Close();
		}

		public void RejectMarsProposal()
		{
			BattleController.RespondToRuleChange(status: false, 0, 0, blindMode: false);
			base.Close();
		}

		private void ApplyProjectMarsSettings(ProjectMarsGameSettingsAsset proposedSettings)
		{
			int maxUnits = Mathf.RoundToInt(proposedSettings.UnitCap.current);
			int maxBudget = Mathf.RoundToInt(proposedSettings.GameBudget.current);
			bool blindMode = proposedSettings.ProjectMarsTurnStyle == ProjectMarsGameSettingsAsset.TurnStyle.Blind;
			bool status = proposedSettings != null;
			BattleController.RespondToRuleChange(status, maxUnits, maxBudget, blindMode);
		}

		private void LoadLocalMultiplayerGameRules()
		{
			turnStyle.Init(this, turnStyleStrings, (int)localMultiplayerGameRules.TurnStyle);
			splitScreen.Init(this, splitScreenStrings, (int)localMultiplayerGameRules.SplitScreenStyle);
			maxUnitPoints.Init(this, localMultiplayerGameRules.MaxUnitPoints);
			timedPlacement.Init(this, boolStrings, localMultiplayerGameRules.IsTimerToPlaceUnitsOn ? 1 : 0);
			time.Init(this, localMultiplayerGameRules.TimeToPlaceUnits);
		}

		private void LoadOnlineSettings()
		{
			turnStyle.Init(this, onlineTurnStyleStrings, (int)onlineGameSettings.ProjectMarsTurnStyle);
			maxUnitPoints.Init(this, onlineGameSettings.GameBudget);
			unitCap.Init(this, onlineGameSettings.UnitCap);
		}

		private void InitServices()
		{
			localMultiplayerGameRules = ((localMultiplayerGameRules == null) ? ServiceLocator.GetService<LocalMultiplayerGameRules>() : localMultiplayerGameRules);
			settingsProfileManager = ((settingsProfileManager == null) ? ServiceLocator.GetService<SettingsProfileManager>() : settingsProfileManager);
			onlineGameSettings = ((onlineGameSettings == null) ? ServiceLocator.GetService<ProjectMarsGameSettingsAsset>() : onlineGameSettings);
		}
	}
}
