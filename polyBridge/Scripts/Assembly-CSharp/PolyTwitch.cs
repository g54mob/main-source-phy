using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Poly.PortToJS;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Networking;

public class PolyTwitch
{
	public static bool m_IsSerializing;

	public static bool m_IsTakingScreenshot;

	public static bool m_Authorized;

	public static bool m_Authorizing;

	public static bool m_StreamStarting;

	public static bool m_StreamStarted;

	public static bool m_StreamStopping;

	public static PolyTwitchSuggestion m_LastLoadedSuggestion;

	public static string m_BridgeHashForSimulation;

	public static string m_Key;

	public static string m_ChannelName;

	public static readonly int VIEWER_COOLDOWN_SECONDS_MIN = 5;

	public static readonly int VIEWER_COOLDOWN_SECONDS_MAX = 60;

	public static readonly Vector2 DEFAULT_STREAMER_WINDOW_POS = new Vector2(-418.5f, 33.6f);

	public static readonly Vector2 DEFAULT_AUTHOR_WINDOW_POS = new Vector2(0f, 242f);

	public static readonly float DEFAULT_STREAMER_WINDOW_HEIGHT = 295f;

	public static readonly string WEBREQUEST_BASE_URL = "https://api-t3.drycactus.com/v1/";

	private static readonly float PUSH_LAYOUT_INTERVAL_SECONDS = 3.5f;

	private static readonly float PULL_SUGGESTIONS_INTERVAL_SECONDS = 3.5f;

	private static readonly int CONSUME_PENDING_TIMEOUT_SECONDS = 10;

	private static readonly float SEND_STREAM_OPTIONS_INTERVAL_SECONDS = 3.5f;

	private static readonly float GRAB_BAN_LIST_INTERVAL_SECONDS = 10f;

	private static readonly float GRAB_MOD_LIST_INTERVAL_SECONDS = 5000f;

	private static readonly float FORCE_SEND_OPTIONS_INTERVAL_SECONDS = 60f;

	private static readonly string CACHED_TOKEN_FILENAME = "ti.token";

	private static float m_LastPushLayoutTime;

	private static float m_LastPullSuggestionsTime;

	private static string m_LastPushMD5 = string.Empty;

	private static string m_LastPushLevelHash = string.Empty;

	private static bool m_StreamOptionsSent;

	private static float m_NextStreamOptionCheckTime;

	private static bool m_LastAllowSuggestions;

	private static int m_LastCooldownSeconds;

	private static bool m_LastSubscribersOnly;

	private static bool m_LastModerated;

	private static bool m_LastBitsEnabled;

	private static bool m_LastBitsMandatory;

	private static float m_GrabBanListTimer = 1f;

	private static float m_GrabModListTimer = 1f;

	private static float m_ForceSendOptionsTimer = 1f;

	private static int m_SetOptionsRetryCounter = 0;

	private static bool m_ConsumePending;

	private static bool m_CanUseBits;

	private static List<ConsumeReply> m_ConsumeReplyQueue = new List<ConsumeReply>();

	private static Action<string> m_UnBanAllPlayersCompletedCallback;

	public static void Init(Vector2 authorWindowPos, Vector2 mainWindowPos, float mainWindowHeight, bool windowCollapsed)
	{
		LoadCachedKey();
		if (windowCollapsed)
		{
			GameUI.m_Instance.m_PolyTwitchMain.Collapse();
		}
		else
		{
			GameUI.m_Instance.m_PolyTwitchMain.UnCollapse();
		}
		GameUI.m_Instance.m_PolyTwitchMain.m_AuthorPanel.m_PanelRectTransform.anchoredPosition = authorWindowPos;
		GameUI.m_Instance.m_PolyTwitchMain.m_PanelRectTransform.anchoredPosition = mainWindowPos;
		GameUI.m_Instance.m_PolyTwitchMain.SetHeight(mainWindowHeight);
		m_NextStreamOptionCheckTime = Time.realtimeSinceStartup + SEND_STREAM_OPTIONS_INTERVAL_SECONDS;
		m_ConsumePending = false;
	}

	public static void AuthorizeWithKey(string key)
	{
		m_Authorized = true;
		m_Key = key;
	}

	public static void DeAuthorize()
	{
		m_Authorized = false;
		m_Key = string.Empty;
	}

	public static void DeleteCachedToken()
	{
		Utils.DeleteFile(GetCachedKeyPathAndFilename());
	}

	public static void UpdateManual()
	{
		if (CampaignTutorial.IsRunning())
		{
			return;
		}
		if (m_StreamStarted && m_StreamOptionsSent && GameStateManager.GetState() == GameState.BUILD)
		{
			MaybePushLayout();
		}
		if (m_StreamStarted && m_StreamOptionsSent && Profiles.m_ActiveProfile.m_TwitchAllowSuggestions)
		{
			MaybePullSuggestions();
		}
		if (m_StreamStarted && m_StreamOptionsSent && Time.realtimeSinceStartup > m_NextStreamOptionCheckTime && StreamOptionsChangedSinceLastSend())
		{
			SetStreamOptions();
		}
		if (m_StreamStarted && (GameStateManager.GetState() == GameState.BUILD || GameStateManager.GetState() == GameState.SANDBOX || GameStateManager.GetState() == GameState.PHOTO))
		{
			if (!GameUI.m_Instance.m_PolyTwitchMain.gameObject.activeInHierarchy)
			{
				GameUI.m_Instance.m_PolyTwitchMain.gameObject.SetActive(value: true);
			}
		}
		else
		{
			GameUI.m_Instance.m_PolyTwitchMain.gameObject.SetActive(value: false);
			GameUI.m_Instance.m_PolyTwitchBridge.gameObject.SetActive(value: false);
		}
		MaybeShowAuthor();
		MaybeStartAutoPlay();
		GameUI.m_Instance.m_PolyTwitchMain.m_AutoPlayPanel.gameObject.SetActive(m_StreamStarted && PolyTwitchAutoPlay.m_Running && GameStateManager.GetState() == GameState.SIM && !GameStateSim.CameraInTransition());
		if (m_StreamStarted)
		{
			m_GrabBanListTimer -= Time.unscaledDeltaTime;
			if (m_StreamStarted && m_StreamOptionsSent && m_GrabBanListTimer < 0f)
			{
				m_GrabBanListTimer = GRAB_BAN_LIST_INTERVAL_SECONDS;
				GetBannedPlayers();
			}
			m_GrabModListTimer -= Time.unscaledDeltaTime;
			if (m_StreamStarted && m_StreamOptionsSent && m_GrabModListTimer < 0f)
			{
				m_GrabModListTimer = GRAB_MOD_LIST_INTERVAL_SECONDS;
				RefreshModeratorList();
			}
			m_ForceSendOptionsTimer -= Time.unscaledDeltaTime;
			if (m_StreamStarted && m_StreamOptionsSent && m_ForceSendOptionsTimer < 0f)
			{
				SetStreamOptions();
				m_ForceSendOptionsTimer = FORCE_SEND_OPTIONS_INTERVAL_SECONDS;
			}
		}
	}

	private static void MaybeShowAuthor()
	{
		bool active = GameStateManager.GetState() == GameState.SIM && BridgeForSimulationMatchesLastSuggestion();
		GameUI.m_Instance.m_PolyTwitchMain.m_AuthorPanel.gameObject.SetActive(active);
		if (m_LastLoadedSuggestion != null)
		{
			GameUI.m_Instance.m_PolyTwitchMain.m_AuthorPanel.SetCurrentSuggestion(m_LastLoadedSuggestion);
		}
	}

	private static bool BridgeForSimulationMatchesLastSuggestion()
	{
		if (m_LastLoadedSuggestion == null)
		{
			return false;
		}
		return m_LastLoadedSuggestion.m_BridgeHash == m_BridgeHashForSimulation;
	}

	private static void MaybeStartAutoPlay()
	{
		if (GameStateManager.GetState() == GameState.BUILD && !GameUI.m_Instance.m_PauseMenu.gameObject.activeInHierarchy)
		{
			PolyTwitchSuggestion firstAutoplaySuggestion = PolyTwitchSuggestions.GetFirstAutoplaySuggestion();
			if (m_StreamStarted && Profiles.m_ActiveProfile.m_TwitchAutoPlay && firstAutoplaySuggestion != null && firstAutoplaySuggestion.m_Slot != null)
			{
				PolyTwitchAutoPlay.Start(firstAutoplaySuggestion);
				PolyTwitchAutoPlay.m_SimStartedAutomatically = true;
				GameUI.m_Instance.m_TopBar.OnSim();
			}
		}
	}

	public static bool SessionIsActiveWithUnviewedSuggestions()
	{
		if (m_StreamStarted)
		{
			return PolyTwitchSuggestions.GetNumberOfUnseenNotifications() > 0;
		}
		return false;
	}

	public static void ConfirmLeaveLevel(Action okDelegate)
	{
		if (Profiles.m_ActiveProfile.m_NeverShowAgain.Contains(PopUpWarningCategory.UNSEEN_NOTIFICATIONS))
		{
			okDelegate();
			return;
		}
		PolyTwitchSuggestions.GetNumberOfUnseenNotifications();
		PopUpMessage.Display(Localize.Get("POPUP_POLYTWITCH_CONFIRM"), okDelegate, null);
	}

	public static void OnLayoutLoaded()
	{
		PolyTwitchAutoSaves.DeleteAll();
		GameUI.m_Instance.m_PolyTwitchMain.OnLayoutLoaded();
	}

	public static void OnEnterMainMenuState()
	{
		if (m_StreamStarted && m_StreamOptionsSent)
		{
			byte[] array = Utils.ZipPayload(new List<byte>().ToArray());
			string payload_md = Utils.MD5HashFor(array);
			PushLayout(array, payload_md, "mainMenu");
		}
	}

	public static void StartStream()
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/start", m_Key).SendWebRequest().completed += OnStartStreamComplete;
		m_StreamStarting = true;
		m_ConsumePending = false;
	}

	public static string GetBitsString(int numBits)
	{
		string text = "";
		string text2 = "";
		if (numBits >= 10000)
		{
			text = "bitsRed";
			text2 = "#f43021";
		}
		else if (numBits >= 5000)
		{
			text = "bitsBlue";
			text2 = "#0099fe";
		}
		else if (numBits >= 1000)
		{
			text = "bitsGreen";
			text2 = "#1db2a5";
		}
		else if (numBits >= 100)
		{
			text = "bitsPurple";
			text2 = "#9c3ee8";
		}
		else
		{
			text = "bitsGrey";
			text2 = "#979797";
		}
		return $"<size=140%><sprite name=\"{text}\"><color={text2}>{numBits}</color></size>";
	}

	private static void OnStartStreamComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError || ContainsErrorText(unityWebRequestAsyncOperation.webRequest.downloadHandler.text))
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			PopUpMessage.DisplayWarningOkOnly(string.Format("{0}\n{1}", Localize.Get("WARN_FAILED_SESSION_START"), errorMessage));
			GameUI.m_Instance.m_Settings.m_TwitchPanel.StartSessionCallback(success: false);
		}
		else
		{
			Debug.Log("streamer/stream/start success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
			m_StreamStarted = true;
			StartStreamResponse startStreamResponse = JsonUtility.FromJson<StartStreamResponse>(MaybeTrimJsonText(unityWebRequestAsyncOperation.webRequest.downloadHandler.text));
			m_CanUseBits = startStreamResponse.can_use_bits;
			m_ChannelName = startStreamResponse.twitch_channel_name;
			Profiles.m_ActiveProfile.m_TwitchUsername = m_ChannelName;
			Profiles.SaveActiveProfile();
			SetStreamOptions();
			GameUI.m_Instance.m_Settings.m_TwitchPanel.StartSessionCallback(success: true);
		}
		m_StreamStarting = false;
	}

	private static void SetStreamOptions()
	{
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddField("enabled", Profiles.m_ActiveProfile.m_TwitchAllowSuggestions ? "1" : "0");
		wWWForm.AddField("submissions_cooldown", Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds.ToString());
		wWWForm.AddField("subscribers_only", Profiles.m_ActiveProfile.m_TwitchSuscribersOnly ? "1" : "0");
		wWWForm.AddField("chat_bot_enabled", Profiles.m_ActiveProfile.m_TwitchBitsEnabled ? "1" : "0");
		wWWForm.AddField("moderated", Profiles.m_ActiveProfile.m_TwitchModerated ? "1" : "0");
		wWWForm.AddField("chat_bot_interval", "30");
		if (m_CanUseBits)
		{
			wWWForm.AddField("bits_enabled", Profiles.m_ActiveProfile.m_TwitchBitsEnabled ? "1" : "0");
			wWWForm.AddField("bits_only", Profiles.m_ActiveProfile.m_TwitchBitsMandatory ? "1" : "0");
		}
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/set/options", m_Key, wWWForm).SendWebRequest().completed += OnSetStreamOptionsComplete;
		m_StreamOptionsSent = true;
		m_NextStreamOptionCheckTime = Time.realtimeSinceStartup + SEND_STREAM_OPTIONS_INTERVAL_SECONDS;
		m_ForceSendOptionsTimer += SEND_STREAM_OPTIONS_INTERVAL_SECONDS;
		m_LastAllowSuggestions = Profiles.m_ActiveProfile.m_TwitchAllowSuggestions;
		m_LastCooldownSeconds = Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds;
		m_LastSubscribersOnly = Profiles.m_ActiveProfile.m_TwitchSuscribersOnly;
		m_LastModerated = Profiles.m_ActiveProfile.m_TwitchModerated;
		m_LastBitsEnabled = Profiles.m_ActiveProfile.m_TwitchBitsEnabled;
		m_LastBitsMandatory = Profiles.m_ActiveProfile.m_TwitchBitsMandatory;
	}

	private static void OnSetStreamOptionsComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			if (m_SetOptionsRetryCounter++ > 3)
			{
				m_SetOptionsRetryCounter = 0;
				string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
				PopUpMessage.DisplayWarning(string.Format("{0}\n{1}", Localize.Get("WARN_FAILED_STREAM_ENABLE"), errorMessage), useYesNoLables: false, null);
			}
			else
			{
				m_ForceSendOptionsTimer = SEND_STREAM_OPTIONS_INTERVAL_SECONDS;
			}
		}
		else
		{
			m_SetOptionsRetryCounter = 0;
			Debug.Log("streamer/stream/set/options success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	private static void RefreshModeratorList()
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/moderators/refresh", m_Key).SendWebRequest().completed += OnModComplete;
	}

	private static void OnModComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			Debug.LogWarning("streamer/stream/moderators fail: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
		else
		{
			Debug.Log("streamer/stream/moderators success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	private static void GetBannedPlayers()
	{
		WebRequest.Get(WEBREQUEST_BASE_URL + "streamer/stream/users/banned", m_Key).SendWebRequest().completed += OnGetBannedPlayersComplete;
	}

	private static void OnGetBannedPlayersComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("stream/users/banned failure: " + errorMessage);
		}
		else
		{
			if (!(unityWebRequestAsyncOperation.webRequest.downloadHandler.text != "[]"))
			{
				return;
			}
			string text = MaybeTrimJsonText(unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
			BanListEntryArray banListEntryArray = JsonUtility.FromJson<BanListEntryArray>("{\"entries\":" + text + "}");
			if (banListEntryArray != null && banListEntryArray.entries != null && banListEntryArray.entries.Length != 0)
			{
				for (int i = 0; i < banListEntryArray.entries.Length; i++)
				{
					PolyTwitchBans.MutePlayer(banListEntryArray.entries[i].username, banListEntryArray.entries[i].id);
				}
			}
		}
	}

	public static void StopStream()
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/stop", m_Key).SendWebRequest().completed += OnStopStreamComplete;
		m_LastPushLevelHash = string.Empty;
		m_StreamStarted = false;
	}

	private static void OnStopStreamComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			string.Format("{0}\n{1}", Localize.Get("WARN_FAILED_STREAM_STOP"), errorMessage);
		}
		else
		{
			Debug.Log("streamer/stream/stop success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	public static void StopStreamSilent()
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/stop", m_Key).SendWebRequest().completed += OnCloseStreamCompleteSilent;
		m_StreamStarted = false;
	}

	private static void OnCloseStreamCompleteSilent(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.Log("Failed to stop stream.\n" + errorMessage);
		}
		else
		{
			Debug.Log("streamer/stream/stop success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	private static void MaybePushLayout()
	{
		if (Time.realtimeSinceStartup - m_LastPushLayoutTime > PUSH_LAYOUT_INTERVAL_SECONDS)
		{
			PushLayout();
			m_LastPushLayoutTime = Time.realtimeSinceStartup;
		}
	}

	private static void MaybePullSuggestions()
	{
		float num = Time.realtimeSinceStartup - m_LastPullSuggestionsTime;
		if (!m_ConsumePending && num > PULL_SUGGESTIONS_INTERVAL_SECONDS)
		{
			PullSuggestions();
			m_ConsumePending = true;
			m_LastPullSuggestionsTime = Time.realtimeSinceStartup;
		}
		if (m_ConsumePending && num > (float)CONSUME_PENDING_TIMEOUT_SECONDS)
		{
			m_ConsumePending = false;
		}
	}

	private static void PushLayout()
	{
		if (Sandbox.m_CurrentLayoutData == null)
		{
			Debug.LogWarningFormat("No sandbox layout to push from streamer");
			return;
		}
		m_IsSerializing = true;
		Sandbox.m_CurrentLayoutData = SandboxLayout.SerializeToProxies();
		Sandbox.m_CurrentLayoutData.m_Ramps = Ramps.Serialize();
		Sandbox.m_CurrentLayoutHash = Utils.MD5HashFor(Sandbox.m_CurrentLayoutData.SerializeWithoutBridgeBinary());
		Sandbox.m_CurrentLayoutData.m_Bridge = BridgeSave.Serialize();
		List<byte> list = new List<byte>();
		list.AddRange(Sandbox.m_CurrentLayoutData.SerializeBinary());
		list.AddRange(PolygonData.SerializeAllPolygons());
		list.AddRange(ByteSerializer.SerializeString(Game.GetLevelTitle()));
		if (GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null && !string.IsNullOrEmpty(Campaign.m_CurrentLevel.GetLocalizedDescription()))
		{
			list.AddRange(ByteSerializer.SerializeString(Campaign.m_CurrentLevel.GetLocalizedDescription()));
		}
		else if (GameManager.GetGameMode() == GameMode.WORKSHOP && Workshop.m_LastPlayedWorkshopItem != null && !string.IsNullOrEmpty(Workshop.m_LastPlayedWorkshopItem.GetDescription()))
		{
			list.AddRange(ByteSerializer.SerializeString(Workshop.m_LastPlayedWorkshopItem.GetDescription()));
		}
		else
		{
			list.AddRange(ByteSerializer.SerializeString(""));
		}
		byte[] array = Utils.ZipPayload(list.ToArray());
		string text = Utils.MD5HashFor(array);
		if (Utils.MD5HashesMatch(text, m_LastPushMD5) && Utils.MD5HashesMatch(Sandbox.m_CurrentLayoutHash, m_LastPushLevelHash))
		{
			m_IsSerializing = false;
			return;
		}
		m_LastPushMD5 = text;
		m_LastPushLevelHash = Sandbox.m_CurrentLayoutHash;
		PushLayout(array, text, Sandbox.m_CurrentLayoutHash);
		m_IsSerializing = false;
	}

	private static void PushLayout(byte[] payload, string payload_md5, string layout_md5)
	{
		WWWForm wWWForm = new WWWForm();
		wWWForm.AddBinaryData("payload", payload, "file.zip", "zip");
		wWWForm.AddField("payload_hash", payload_md5);
		wWWForm.AddField("level_hash", layout_md5);
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/push", m_Key, wWWForm).SendWebRequest().completed += OnPushLayoutComplete;
	}

	private static void OnPushLayoutComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("streamer/stream/push failed with: " + errorMessage);
			m_LastPushMD5 = string.Empty;
			m_LastPushLevelHash = string.Empty;
		}
	}

	private static void PullSuggestions()
	{
		WebRequest.Get(WEBREQUEST_BASE_URL + "streamer/stream/submissions/consume", m_Key).SendWebRequest().completed += OnPullSuggestionsComplete;
	}

	private static void OnPullSuggestionsComplete(AsyncOperation asyncOperation)
	{
		m_ConsumePending = false;
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("streamer/stream/submissions/consume failure: " + errorMessage);
		}
		else
		{
			if (!(unityWebRequestAsyncOperation.webRequest.downloadHandler.text != "[]"))
			{
				return;
			}
			try
			{
				string text = MaybeTrimJsonText(unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
				ConsumeReply[] entries = JsonUtility.FromJson<ConsumeReplyArray>("{\"entries\":" + text + "}").entries;
				foreach (ConsumeReply consumeReply in entries)
				{
					if (consumeReply != null)
					{
						Debug.LogFormat("Received suggestion with layout md5: {0}", consumeReply.level_hash);
						m_ConsumeReplyQueue.Add(consumeReply);
						UnityWebRequest.Get(consumeReply.payload).SendWebRequest().completed += OnPayloadDownloadComplete;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogWarningFormat("OnPullSuggestionsComplete caught exception: {0}", ex.Message);
			}
		}
	}

	private static void OnPayloadDownloadComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Download suggestion payload failure: " + errorMessage);
		}
		else
		{
			if (unityWebRequestAsyncOperation.webRequest.downloadHandler.data == null)
			{
				return;
			}
			Debug.Log("Download suggestion payload success");
			byte[] bridgeBytes;
			using (MemoryStream stream = new MemoryStream(unityWebRequestAsyncOperation.webRequest.downloadHandler.data))
			{
				using GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
				using MemoryStream memoryStream = new MemoryStream();
				gZipStream.CopyTo(memoryStream);
				bridgeBytes = memoryStream.ToArray();
			}
			for (int i = 0; i < m_ConsumeReplyQueue.Count; i++)
			{
				if (m_ConsumeReplyQueue[i].payload == unityWebRequestAsyncOperation.webRequest.url)
				{
					ConsumeSuggestion(m_ConsumeReplyQueue[i], bridgeBytes);
					m_ConsumeReplyQueue.RemoveAt(i);
					break;
				}
			}
		}
	}

	public static void BanPlayer(string ownerId)
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + $"streamer/stream/user/{ownerId}/ban", m_Key).SendWebRequest().completed += OnBanPlayerComplete;
	}

	private static void OnBanPlayerComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Ban failed on back end.\n" + errorMessage);
		}
		else
		{
			Debug.Log("banned player success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	public static void UnBanPlayer(string ownerId)
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + $"streamer/stream/user/{ownerId}/ban/lift", m_Key).SendWebRequest().completed += OnUnBanPlayerComplete;
	}

	private static void OnUnBanPlayerComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Lift ban failed on back end.\n" + errorMessage);
		}
		else
		{
			Debug.Log("lift ban for player success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	public static void UnBanAllPlayers(Action<string> callback)
	{
		m_UnBanAllPlayersCompletedCallback = callback;
		WebRequest.Post(WEBREQUEST_BASE_URL + "streamer/stream/users/banned/flush", m_Key).SendWebRequest().completed += OnUnBanAllPlayersComplete;
	}

	private static void OnUnBanAllPlayersComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			m_UnBanAllPlayersCompletedCallback(errorMessage);
			Debug.LogWarning("Unban all players failed on back end.\n" + errorMessage);
		}
		else
		{
			Debug.Log("Unban for all players success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
			m_UnBanAllPlayersCompletedCallback(string.Empty);
		}
	}

	public static void AcceptSubmission(string fileId)
	{
		WebRequest.Post(WEBREQUEST_BASE_URL + $"streamer/stream/submission/{fileId}/accept", m_Key).SendWebRequest().completed += OnAcceptSubmissionComplete;
	}

	private static void OnAcceptSubmissionComplete(AsyncOperation asyncOperation)
	{
		UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = (UnityWebRequestAsyncOperation)asyncOperation;
		if (unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ConnectionError || unityWebRequestAsyncOperation.webRequest.result == UnityWebRequest.Result.ProtocolError)
		{
			string errorMessage = WebRequest.GetErrorMessage(unityWebRequestAsyncOperation.webRequest);
			Debug.LogWarning("Accept Submission failed on back end.\n" + errorMessage);
		}
		else
		{
			Debug.Log("Accept submission success: " + unityWebRequestAsyncOperation.webRequest.downloadHandler.text);
		}
	}

	public static bool SetStatusForLastLoadedSuggestion(PolyTwitchSuggestionStatus status, string currentBridgeHash)
	{
		if (m_LastLoadedSuggestion == null)
		{
			return false;
		}
		if (currentBridgeHash == m_LastLoadedSuggestion.m_BridgeHash)
		{
			m_LastLoadedSuggestion.SetStatus(status);
			if (status == PolyTwitchSuggestionStatus.PASSED)
			{
				AcceptSubmission(m_LastLoadedSuggestion.m_FileId);
				return true;
			}
		}
		return false;
	}

	public static void MarkLastLoadedSuggestionAsSimulated(string currentBridgeHash)
	{
		if (m_LastLoadedSuggestion != null && !m_LastLoadedSuggestion.HasBeenSimulated() && currentBridgeHash == m_LastLoadedSuggestion.m_BridgeHash)
		{
			m_LastLoadedSuggestion.SetStatus(PolyTwitchSuggestionStatus.SIMULATED);
		}
	}

	public static string GetCachedKeyPathAndFilename()
	{
		return Path.Combine(Application.persistentDataPath, CACHED_TOKEN_FILENAME);
	}

	public static bool CanUseBits()
	{
		return m_CanUseBits;
	}

	private static void ConsumeSuggestion(ConsumeReply consumeReply, byte[] bridgeBytes)
	{
		string id = consumeReply.id;
		string username = consumeReply.owner.username;
		string id2 = consumeReply.owner.id;
		string level_hash = consumeReply.level_hash;
		int twitch_bits_used = consumeReply.twitch_bits_used;
		if (!Utils.MD5HashesMatch(level_hash, Sandbox.m_CurrentLayoutHash))
		{
			return;
		}
		try
		{
			BridgeSaveSlotData bridgeSaveSlotData = SerializationUtility.DeserializeValue<BridgeSaveSlotData>(bridgeBytes, DataFormat.Binary);
			if (bridgeSaveSlotData != null)
			{
				if (!m_LastBitsEnabled || bridgeSaveSlotData.m_LevelID != Game.GetLevelId())
				{
					return;
				}
				bridgeBytes = bridgeSaveSlotData.m_Bridge;
			}
		}
		catch
		{
		}
		if (twitch_bits_used > 0)
		{
			InterfaceAudio.Play("ui_twitch_bits");
		}
		string bridgeHash = Utils.MD5HashFor(bridgeBytes);
		if (PolyTwitchSuggestions.SuggestionFromSameOwnerExists(id2, bridgeHash))
		{
			PolyTwitchSuggestions.UpdateSuggestionTimeAndBits(id2, bridgeHash, twitch_bits_used);
			return;
		}
		int offset = 0;
		BridgeSaveData bridgeSaveData = new BridgeSaveData();
		try
		{
			bridgeSaveData.DeserializeBinary(bridgeBytes, ref offset);
		}
		catch
		{
			return;
		}
		PolyTwitchSuggestions.Create(username, id2, id, bridgeSaveData, bridgeHash, level_hash, PolyTwitchSuggestionTag.NONE, twitch_bits_used);
	}

	private static bool StreamOptionsChangedSinceLastSend()
	{
		if (m_LastAllowSuggestions != Profiles.m_ActiveProfile.m_TwitchAllowSuggestions)
		{
			return true;
		}
		if (m_LastCooldownSeconds != Profiles.m_ActiveProfile.m_TwitchViewerCooldownSeconds)
		{
			return true;
		}
		if (m_LastSubscribersOnly != Profiles.m_ActiveProfile.m_TwitchSuscribersOnly)
		{
			return true;
		}
		if (m_LastModerated != Profiles.m_ActiveProfile.m_TwitchModerated)
		{
			return true;
		}
		if (m_LastBitsEnabled != Profiles.m_ActiveProfile.m_TwitchBitsEnabled)
		{
			return true;
		}
		if (m_LastBitsMandatory != Profiles.m_ActiveProfile.m_TwitchBitsMandatory)
		{
			return true;
		}
		return false;
	}

	private static void LoadCachedKey()
	{
		try
		{
			string path = Path.Combine(Application.persistentDataPath, CACHED_TOKEN_FILENAME);
			if (File.Exists(path))
			{
				AuthorizeWithKey(File.ReadAllText(path).Trim());
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("LoadCachedKey caught exception: {0}", ex.Message);
		}
	}

	private static string MaybeTrimJsonText(string jsonText)
	{
		if (jsonText.StartsWith("{\"data\":"))
		{
			return jsonText.Substring(8, jsonText.Length - 9);
		}
		return jsonText;
	}

	private static bool ContainsErrorText(string responseText)
	{
		if (responseText.Contains("error") && responseText.Contains("Unauthorized") && responseText.Contains("status_code"))
		{
			return true;
		}
		return false;
	}
}
