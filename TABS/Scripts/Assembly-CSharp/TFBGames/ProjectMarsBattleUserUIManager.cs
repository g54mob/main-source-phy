using System;
using BitCode.Graphics;
using BitCode.Users;
using Landfall.TABS;
using Photon.Bolt;
using UnityEngine;

namespace TFBGames
{
	public class ProjectMarsBattleUserUIManager : GlobalEventListener
	{
		public class PlayerInfo
		{
			public PlayerProfile Profile;

			public ProjectMarsBattleUserUI UserUI;

			public MultiplayerPlatform? Platform;
		}

		private const string DefaultRedPlayerName = "MP_LABEL_PLAYER_ONE";

		private const string DefaultBluePlayerName = "MP_LABEL_PLAYER_TWO";

		private const string DefaultStatusString = "MP_LABEL_NOT_YET_JOINED";

		private const string ReadyStatusString = "MP_LABEL_READY";

		private const string NotReadyStatusString = "MP_LABEL_BUILDING_ARMY";

		private const string DisconnectedStatusString = "MP_LABEL_DISCONNECTED";

		private static readonly PlayerInfo[] m_playerInfo = new PlayerInfo[2];

		private AccountManager m_accountManager;

		private INetworkService m_networkService;

		private NetworkBattleController m_networkBattle;

		private INetworkDataCourier m_dataCourier;

		private MultiplayerPlatformIconsController m_platformIcons;

		private Texture2D m_playerTexture;

		private bool m_didSendPlayerTexture;

		private bool m_didReceiveRemotePlayerTexture;

		private NetworkBattleController NetworkBattle
		{
			get
			{
				if (m_networkBattle == null)
				{
					m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
				}
				return m_networkBattle;
			}
		}

		private void Start()
		{
			m_accountManager = ServiceLocator.GetService<AccountManager>();
			m_networkService = ServiceLocator.GetService<INetworkService>();
			m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
			m_dataCourier = ServiceLocator.GetService<INetworkDataCourier>();
			m_platformIcons = ServiceLocator.GetService<MultiplayerPlatformIconsController>();
			NetworkBattle.PhaseChanged += OnPhaseChanged;
			NetworkBattle.RemotePhaseChanged += OnRemotePhaseChanged;
			NetworkBattle.PlayerIsReadyChanged += OnPlayerIsReadyChanged;
			m_dataCourier.TextureReceived += OnTextureReceived;
			PlacementUI instance = PlacementUI.Instance;
			InitializePlayerInfo(Team.Red, "MP_LABEL_PLAYER_ONE", instance.RedUserUI);
			InitializePlayerInfo(Team.Blue, "MP_LABEL_PLAYER_TWO", instance.BlueUserUI);
			if (m_networkService.IsClient)
			{
				InitializeClient();
			}
			SetLocalPlayerData();
			if (NetworkBattle.AreBothPlayersInBattleScene)
			{
				OnBothPlayersEnteredBattleScene();
			}
			else
			{
				NetworkBattle.BothPlayersEnteredBattleScene += OnBothPlayersEnteredBattleScene;
			}
		}

		private void OnDestroy()
		{
			if (NetworkBattle != null)
			{
				NetworkBattle.PhaseChanged -= OnPhaseChanged;
				NetworkBattle.RemotePhaseChanged -= OnRemotePhaseChanged;
				NetworkBattle.PlayerIsReadyChanged -= OnPlayerIsReadyChanged;
				NetworkBattle.BothPlayersEnteredBattleScene -= OnBothPlayersEnteredBattleScene;
			}
			if (m_dataCourier != null)
			{
				m_dataCourier.TextureReceived -= OnTextureReceived;
			}
			ILocalAccount localAccount = ((m_accountManager != null) ? m_accountManager.ActiveAccount : null);
			if (localAccount != null)
			{
				localAccount.Name.ValueChanged -= OnLocalPlayerNameChanged;
			}
		}

		public override void OnEvent(PlayerInfoEvent infoEvent)
		{
			base.OnEvent(infoEvent);
			Team remotePlayerTeam = m_networkService.RemotePlayerTeam;
			MultiplayerPlatform multiplayerPlatform = (MultiplayerPlatform)infoEvent.MultiplayerPlatform;
			SetPlayerName(remotePlayerTeam, infoEvent.PlayerName);
			SetPlayerPlatform(remotePlayerTeam, multiplayerPlatform);
			if (!m_didReceiveRemotePlayerTexture)
			{
				SetPlayerIconSprite(remotePlayerTeam, m_platformIcons.GetIcon(multiplayerPlatform));
			}
			SendPlayerIcon();
		}

		private void InitializeClient()
		{
			NetworkSession currentSession = m_networkService.GetCurrentSession();
			if (currentSession != null && currentSession.Metadata != null)
			{
				Team remotePlayerTeam = m_networkService.RemotePlayerTeam;
				SetPlayerName(remotePlayerTeam, currentSession.Metadata.HostPlayerDisplayName);
				SetPlayerPlatform(remotePlayerTeam, currentSession.Metadata.HostPlatform);
				if (!m_didReceiveRemotePlayerTexture)
				{
					SetPlayerIconSprite(remotePlayerTeam, m_platformIcons.GetIcon(currentSession.Metadata.HostPlatform));
				}
			}
		}

		private void OnBothPlayersEnteredBattleScene()
		{
			if (m_networkService.IsClient)
			{
				SendPlayerInfoEvent();
			}
		}

		private void InitializePlayerInfo(Team team, string playerName, ProjectMarsBattleUserUI userUI)
		{
			string statusString = "MP_LABEL_NOT_YET_JOINED";
			if (team == m_networkService.PlayerTeam)
			{
				statusString = BuildStatusString(team, NetworkBattle.Phase);
			}
			else if (team == m_networkService.RemotePlayerTeam)
			{
				statusString = BuildStatusString(team, NetworkBattle.RemotePhase);
			}
			PlayerInfo playerInfo = new PlayerInfo();
			m_playerInfo[(int)team] = playerInfo;
			playerInfo.Profile = new PlayerProfile(playerName, null, statusString, team, null);
			playerInfo.UserUI = userUI;
			playerInfo.UserUI.SetPlayerProfile(playerInfo.Profile);
		}

		private void SetPlayerName(Team team, string playerName)
		{
			if (!string.IsNullOrEmpty(playerName))
			{
				m_playerInfo[(int)team].UserUI.SetPlayerName(playerName);
			}
		}

		private void SetPlayerIconSprite(Team team, Sprite sprite)
		{
			m_playerInfo[(int)team].UserUI.SetPlayerSprite(sprite);
		}

		private void SetPlayerPlatform(Team team, MultiplayerPlatform platform)
		{
			m_playerInfo[(int)team].Platform = platform;
		}

		public static PlayerInfo GetLocalPlayerInfo()
		{
			Team playerTeam = ServiceLocator.GetService<INetworkService>().PlayerTeam;
			return m_playerInfo[(int)playerTeam];
		}

		public static PlayerInfo GetRemotePlayerInfo()
		{
			Team remotePlayerTeam = ServiceLocator.GetService<INetworkService>().RemotePlayerTeam;
			return m_playerInfo[(int)remotePlayerTeam];
		}

		private void SetLocalPlayerData()
		{
			Team playerTeam = m_networkService.PlayerTeam;
			SetPlayerPlatform(playerTeam, NetworkSessionHelper.GetMultiplayerPlatform());
			ILocalAccount localAccount = ((m_accountManager != null) ? m_accountManager.ActiveAccount : null);
			if (localAccount != null)
			{
				if (localAccount.Name.Status != UserAccountPropertyStatus.Loaded)
				{
					localAccount.Name.SetTracked(track: true);
					localAccount.Name.ValueChanged += OnLocalPlayerNameChanged;
				}
				else
				{
					SetPlayerName(playerTeam, localAccount.Name.Value);
				}
				LoadLocalPlayerIcon();
			}
		}

		private void OnLocalPlayerNameChanged(IUserAccount user)
		{
			ILocalAccount localAccount = ((m_accountManager != null) ? m_accountManager.ActiveAccount : null);
			if (localAccount != null)
			{
				localAccount.Name.ValueChanged -= OnLocalPlayerNameChanged;
			}
			if (localAccount != null && localAccount.Name.Status == UserAccountPropertyStatus.Loaded)
			{
				Team playerTeam = m_networkService.PlayerTeam;
				SetPlayerName(playerTeam, localAccount.Name.Value);
			}
		}

		private void LoadLocalPlayerIcon()
		{
			ILocalAccount localAccount = ((m_accountManager != null) ? m_accountManager.ActiveAccount : null);
			if (localAccount == null)
			{
				return;
			}
			IUserAccountProperty<ImageData> avatarImage;
			try
			{
				avatarImage = localAccount.AvatarImage;
			}
			catch (NotImplementedException)
			{
				return;
			}
			catch (NotSupportedException)
			{
				return;
			}
			if (avatarImage != null && avatarImage.Status == UserAccountPropertyStatus.Loaded)
			{
				Team playerTeam = m_networkService.PlayerTeam;
				Sprite sprite = null;
				if (ServiceLocator.GetService<IPlatformUtils>() is PlatformImageHandling platformImageHandling)
				{
					sprite = platformImageHandling.CreateSpriteFromImageData(avatarImage.Value);
				}
				m_playerTexture = ((sprite != null) ? sprite.texture : null);
				SetPlayerIconSprite(playerTeam, sprite);
				SendPlayerIcon();
			}
		}

		private void SendPlayerIcon()
		{
			MultiplayerPlatform? platform = m_playerInfo[(int)m_networkService.PlayerTeam].Platform;
			MultiplayerPlatform? platform2 = m_playerInfo[(int)m_networkService.RemotePlayerTeam].Platform;
			if (!m_didSendPlayerTexture && !(m_playerTexture == null) && platform.HasValue && platform2.HasValue && m_dataCourier != null)
			{
				m_didSendPlayerTexture = true;
				if (platform.Value == platform2.Value)
				{
					m_dataCourier.SendTexture(NetworkTextureType.UserProfileImage, m_playerTexture);
				}
			}
		}

		private void OnTextureReceived(NetworkTextureType textureType, Texture2D texture)
		{
			if (textureType == NetworkTextureType.UserProfileImage)
			{
				m_didReceiveRemotePlayerTexture = true;
				if (!(texture == null))
				{
					Team remotePlayerTeam = m_networkService.RemotePlayerTeam;
					SetPlayerIconSprite(remotePlayerTeam, Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
				}
			}
		}

		private string BuildStatusString(Team team, NetworkGamePhase phase)
		{
			string text = "";
			switch (phase)
			{
			case NetworkGamePhase.Initializing:
				text = "MP_LABEL_NOT_YET_JOINED";
				break;
			case NetworkGamePhase.Disconnected:
				text = "MP_LABEL_DISCONNECTED";
				break;
			case NetworkGamePhase.Battle:
				text = "MP_LABEL_READY";
				break;
			default:
				text = (NetworkBattle.IsPlayerReady(team) ? "MP_LABEL_READY" : "MP_LABEL_BUILDING_ARMY");
				break;
			}
			return Localizer.GetSinglePhrase(text);
		}

		private void OnPhaseChanged(NetworkGamePhase oldPhase, NetworkGamePhase newPhase)
		{
			Team playerTeam = m_networkService.PlayerTeam;
			m_playerInfo[(int)playerTeam].UserUI.SetPlayerStatus(BuildStatusString(playerTeam, newPhase));
		}

		private void OnRemotePhaseChanged(NetworkGamePhase oldPhase, NetworkGamePhase newPhase)
		{
			Team remotePlayerTeam = m_networkService.RemotePlayerTeam;
			m_playerInfo[(int)remotePlayerTeam].UserUI.SetPlayerStatus(BuildStatusString(remotePlayerTeam, newPhase));
		}

		private void OnPlayerIsReadyChanged(Team team, bool oldIsReady, bool newIsReady)
		{
			if (team == m_networkService.PlayerTeam || team == m_networkService.RemotePlayerTeam)
			{
				PlayerInfo obj = m_playerInfo[(int)team];
				NetworkGamePhase phase = ((team == m_networkService.PlayerTeam) ? NetworkBattle.Phase : NetworkBattle.RemotePhase);
				obj.UserUI.SetPlayerStatus(BuildStatusString(team, phase));
			}
		}

		private void SendPlayerInfoEvent()
		{
			ILocalAccount localAccount = ((m_accountManager != null) ? m_accountManager.ActiveAccount : null);
			string playerName = ((localAccount != null && localAccount.Name.Status == UserAccountPropertyStatus.Loaded) ? localAccount.Name.Value : string.Empty);
			MultiplayerPlatform multiplayerPlatform = NetworkSessionHelper.GetMultiplayerPlatform();
			PlayerInfoEvent playerInfoEvent = PlayerInfoEvent.Create(GlobalTargets.OnlyServer, ReliabilityModes.ReliableOrdered);
			playerInfoEvent.PlayerName = playerName;
			playerInfoEvent.MultiplayerPlatform = (int)multiplayerPlatform;
			playerInfoEvent.Send();
		}
	}
}
