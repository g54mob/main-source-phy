using System;
using DM;
using GamepadUI.StateManager.Core;
using Landfall.TABS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TFBGames
{
	public class ProjectMarsWaitingScreen : UISubMenu
	{
		[SerializeField]
		protected TMP_Text martianInfoLabel;

		[SerializeField]
		protected SessionInfoUI sessionInfo;

		[SerializeField]
		protected GameObject hostingUI;

		[SerializeField]
		protected GameObject joiningUI;

		[SerializeField]
		protected GameObject findingUI;

		[SerializeField]
		protected GameObject crossPlayWarning;

		[SerializeField]
		protected TMP_Text subtitleLabel;

		[Header("Background glow settings")]
		[SerializeField]
		protected Image backgroundGlow;

		[SerializeField]
		protected float glowAlternateSpeed = 1f;

		[SerializeField]
		protected float glowSizeMin = 0.85f;

		[SerializeField]
		protected float glowSizeMax = 1f;

		[SerializeField]
		[Range(0f, 1f)]
		protected float glowAlphaMin = 0.05f;

		[SerializeField]
		[Range(0f, 1f)]
		protected float glowAlphaMax = 0.15f;

		private GlobalSettingsHandler settingsHandler;

		private const string HostingTitle = "MP_LABEL_CREATING";

		private const string JoiningTitle = "MP_LABEL_JOINING";

		private const string FindingTitle = "MP_LABEL_QUICK_MATCH";

		private const string WaitingForFindingSubtitle = "MP_LABEL_PLEASE_WAIT";

		private const string FindingSubtitle = "MP_LABEL_SEARCHING";

		private const string LoadingTitle = "MP_LABEL_LOADING";

		private INetworkService networkService;

		private LandingScreenMode currentMode;

		private NetworkSession currentSession;

		private const float joinGameDelay = 5f;

		private float joinGameTimer;

		private bool isJoiningSession;

		private event Action<NetworkSession> JoiningSessionCallback;

		private event Action<NetworkException> JoiningSessionErrorCallback;

		protected override void Awake()
		{
			base.Awake();
			settingsHandler = ServiceLocator.GetService<GlobalSettingsHandler>();
			networkService = ServiceLocator.GetService<INetworkService>();
		}

		public override void OnParentClose()
		{
			base.OnParentClose();
			isJoiningSession = false;
		}

		public void SetSessionInfo(NetworkSession session)
		{
			if (session != null)
			{
				MapAsset mapAssetByTypeAndMapIndex = ContentDatabase.Instance().GetMapAssetByTypeAndMapIndex(session.Metadata.RoomMapType, session.Metadata.RoomMapIndex);
				sessionInfo.SetMapInfo(mapAssetByTypeAndMapIndex);
				sessionInfo.SetHostInfo(session.Metadata.HostPlayerDisplayName, session.Metadata.HostPlatform);
			}
		}

		public void ShowSessionInfo(NetworkSession session, Action<NetworkSession> delayLoadCallback, Action<NetworkException> errorCallback)
		{
			currentSession = session;
			SetSessionInfo(currentSession);
			this.JoiningSessionCallback = delayLoadCallback;
			this.JoiningSessionErrorCallback = errorCallback;
			joinGameTimer = 5f;
			isJoiningSession = true;
		}

		public void SetMode(LandingScreenMode mode)
		{
			currentMode = mode;
			hostingUI.SetActive(value: false);
			joiningUI.SetActive(value: false);
			findingUI.SetActive(value: false);
			subtitleLabel.gameObject.SetActive(value: true);
			isJoiningSession = false;
			switch (currentMode)
			{
			case LandingScreenMode.Hosting:
				ShowHostingUI();
				break;
			case LandingScreenMode.Joining:
				ShowJoiningUI();
				break;
			case LandingScreenMode.WaitingForFinding:
				ShowWaitingForFindingUI();
				break;
			case LandingScreenMode.Finding:
				ShowFindingGameUI();
				break;
			case LandingScreenMode.JoiningFromInvite:
				ShowJoiningFromInviteUI();
				break;
			case LandingScreenMode.AuthenticatingUser:
				ShowAuthenticatingUserUI();
				break;
			default:
				throw new ArgumentOutOfRangeException("currentMode", currentMode, null);
			}
		}

		public void ShowHostingUI()
		{
			submenuTitle = "MP_LABEL_CREATING";
			hostingUI.SetActive(value: true);
			SetSubtitle("MP_LABEL_CREATING");
		}

		public void ShowJoiningUI()
		{
			submenuTitle = "MP_LABEL_JOINING";
			joiningUI.SetActive(value: true);
			SetSubtitle("MP_LABEL_JOINING");
		}

		public void ShowWaitingForFindingUI()
		{
			submenuTitle = "MP_LABEL_QUICK_MATCH";
			findingUI.SetActive(value: true);
			crossPlayWarning.SetActive(value: false);
			SetSubtitle("MP_LABEL_PLEASE_WAIT");
		}

		public void ShowFindingGameUI()
		{
			submenuTitle = "MP_LABEL_QUICK_MATCH";
			findingUI.SetActive(value: true);
			crossPlayWarning.SetActive(CheckForCrossplayWarning());
			SetSubtitle("MP_LABEL_SEARCHING");
		}

		public void ShowJoiningFromInviteUI()
		{
			submenuTitle = "MP_LABEL_JOINING";
			findingUI.SetActive(value: true);
			crossPlayWarning.SetActive(CheckForCrossplayWarning());
			SetSubtitle("MP_LABEL_JOINING");
		}

		public void ShowAuthenticatingUserUI()
		{
			submenuTitle = "MP_LABEL_LOADING";
			subtitleLabel.gameObject.SetActive(value: false);
		}

		private void SetSubtitle(string key)
		{
			string singlePhrase = Localizer.GetSinglePhrase(key);
			subtitleLabel.text = singlePhrase;
		}

		protected override void Update()
		{
			base.Update();
			if (!base.IsOpen)
			{
				return;
			}
			AnimateBackgroundGlow();
			if (isJoiningSession)
			{
				joinGameTimer -= Time.unscaledDeltaTime;
				if (networkService != null && (!networkService.IsRunning || !networkService.IsConnected))
				{
					Action<NetworkException> action = this.JoiningSessionErrorCallback;
					this.JoiningSessionErrorCallback = null;
					this.JoiningSessionCallback = null;
					currentSession = null;
					isJoiningSession = false;
					action?.Invoke(new NetworkException(NetworkErrorCode.Disconnected));
				}
				else if (joinGameTimer <= 0f && this.JoiningSessionCallback != null && currentSession != null)
				{
					Action<NetworkSession> action2 = this.JoiningSessionCallback;
					this.JoiningSessionCallback = null;
					action2?.Invoke(currentSession);
					currentSession = null;
					isJoiningSession = false;
				}
			}
		}

		private void AnimateBackgroundGlow()
		{
			float t = (Mathf.Sin(Time.unscaledTime * glowAlternateSpeed) + 1f) * 0.5f;
			Color white = Color.white;
			white.a = glowAlphaMin;
			Color white2 = Color.white;
			white2.a = glowAlphaMax;
			backgroundGlow.color = Color.Lerp(white, white2, t);
			backgroundGlow.transform.localScale = Vector3.Lerp(Vector3.one * glowSizeMin, Vector3.one * glowSizeMax, t);
		}

		private bool CheckForCrossplayWarning()
		{
			return false;
		}
	}
}
