using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Text;
using CielaSpike;
using FFmpeg;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Panel_ShareReplay : MonoBehaviour, IFFmpegHandler
{
	public RectTransform m_RectTransform;

	public TextMeshProUGUI m_ReplaySavingText;

	public Panel_Replay m_Replay;

	public Panel_ShareReplayStatus m_Status;

	[Header("Buttons")]
	public Button m_Cancel;

	[Header("Description")]
	public TextMeshProUGUI m_CharCountText;

	public InputField m_InputField;

	[Header("Share Buttons")]
	public Button m_SaveLocal;

	public Button m_Gallery;

	[NonSerialized]
	public GameObject m_ActivateOnExit;

	[NonSerialized]
	public bool m_UnPauseOnExit;

	[NonSerialized]
	public bool m_ResumeRecordingReplayOnExit;

	private static string rawVideoName = "video.raw";

	private bool m_MovieCompressed;

	private string m_FullPathMovieJustCompressed;

	private ShareReplayFlags m_ShareReplayFlags;

	private float m_ShareTimeSeconds;

	private bool m_SharingCompleted;

	private string m_SharingCompleteMessage;

	private bool m_CompressionFailed;

	private bool m_SharingFailed;

	private readonly float SHARE_DURATION_MIN_SECONDS = 1f;

	private readonly int MAX_DESCRIPTION_CHARS = 140;

	private bool m_RestoreSimPanels;

	private bool m_Compressing;

	private ReplayConvert m_ReplayConvert;

	private void Awake()
	{
		m_InputField.characterLimit = 140;
		m_Status.gameObject.SetActive(value: false);
		m_Cancel.onClick.AddListener(OnCancel);
		m_SaveLocal.onClick.AddListener(OnSaveLocal);
		m_Gallery.onClick.AddListener(OnGallery);
		m_ReplayConvert = new ReplayConvert();
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		m_RectTransform.anchoredPosition = new Vector2(0f, 0f);
		UpdateDescriptionHeader();
		m_InputField.Select();
		m_InputField.text = Localize.Get("UI_SHAREREPLAY_DEFAULT_MESSAGE");
		m_SharingCompleted = false;
		m_UnPauseOnExit = false;
		m_ActivateOnExit = null;
		m_ResumeRecordingReplayOnExit = false;
		m_MovieCompressed = false;
		m_Compressing = false;
		FFmpegParser.Handler = this;
		m_ReplaySavingText.gameObject.SetActive(value: false);
		bool active = m_Replay.Show(play: true);
		m_Replay.gameObject.SetActive(active);
		m_RestoreSimPanels = GameUI.m_Instance.m_LiveStress.gameObject.activeInHierarchy || GameUI.m_Instance.m_SimToolBar.gameObject.activeInHierarchy;
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_SimToolBar.gameObject.SetActive(value: false);
		GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		m_SaveLocal.gameObject.SetActive(!Game.IsRunningOnSteamDeck());
		if (Game.IsRunningOnSteamDeck())
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_SHARE_GALLERY"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.WEST, Localize.Get("TOOLTIP_SAVE_LOCAL"), GamepadButtonType.NORTH, Localize.Get("TOOLTIP_SHARE_GALLERY"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
		}
		ActivePanels.Add(base.gameObject);
	}

	private void OnDisable()
	{
		if ((bool)m_ActivateOnExit)
		{
			m_ActivateOnExit.SetActive(value: true);
			m_ActivateOnExit = null;
		}
		if (m_UnPauseOnExit)
		{
			GameUI.m_Instance.m_TopBar.OnUnPauseSim();
			m_UnPauseOnExit = false;
		}
		if (m_ResumeRecordingReplayOnExit)
		{
			Cameras.ResumeRecording();
			m_ResumeRecordingReplayOnExit = false;
		}
		GameUI.m_Instance.m_LiveStress.gameObject.SetActive(m_RestoreSimPanels && !GameUI.m_DisableHud);
		GameUI.m_Instance.m_SimToolBar.gameObject.SetActive(m_RestoreSimPanels && !GameUI.m_DisableHud);
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		ProcessInput();
		if (m_MovieCompressed)
		{
			m_ShareTimeSeconds += Time.unscaledDeltaTime;
			if (m_SharingCompleted && m_ShareTimeSeconds > SHARE_DURATION_MIN_SECONDS)
			{
				SharingComplete(m_SharingCompleteMessage);
				m_SharingCompleted = false;
			}
		}
		UpdateDescriptionHeader();
	}

	public bool InputFieldHasFocus()
	{
		if (m_InputField.gameObject.activeInHierarchy)
		{
			return m_InputField.isFocused;
		}
		return false;
	}

	public void SharingComplete(string title)
	{
		string directoryName = Path.GetDirectoryName(m_FullPathMovieJustCompressed);
		string message = ((m_ShareReplayFlags == ShareReplayFlags.Local) ? Localize.Get("UI_REPLAY_SAVE_LOCAL") : Localize.Get("UI_REPLAY_UPLOAD_SUCCESS"));
		if (m_UnPauseOnExit)
		{
			m_Status.Complete(!m_SharingFailed, title, message, directoryName, UnPause);
			m_UnPauseOnExit = false;
		}
		else
		{
			m_Status.Complete(!m_SharingFailed, title, message, directoryName, null);
		}
		if (m_ShareReplayFlags.HasFlag(ShareReplayFlags.Gallery))
		{
			GameAchievements.UnlockAchievement(GameAchievement.UI_SharingIsCaring);
		}
		Close();
	}

	private IEnumerator StartSaveVideo(ShareReplayFlags shareReplayFlags, string outputPathAndFilename)
	{
		this.StartCoroutineAsync(SaveVideo(shareReplayFlags, outputPathAndFilename), out var task);
		yield return StartCoroutine(task.Wait());
	}

	private IEnumerator SaveVideo(ShareReplayFlags shareReplayFlags, string outputPathAndFilename)
	{
		yield return Ninja.JumpToUnity;
		_ = FFmpegCommands.Wrapper;
		string watermarkPath = Path.Combine(Application.persistentDataPath, "watermark_PB3.png");
		string basePath = Cameras.m_AsyncCapture.GetReplayFramesFullPath();
		string rawVideoPath = Path.Combine(basePath, rawVideoName);
		Utils.DeleteFile(rawVideoPath);
		if (GameStateSim.m_CapturingReplayForSolution)
		{
			watermarkPath = string.Empty;
		}
		else if (!Utils.FileExists(watermarkPath))
		{
			byte[] bytes = GameUI.m_Instance.m_Watermark.EncodeToPNG();
			Utils.WriteBytes(watermarkPath, bytes);
		}
		yield return Ninja.JumpBack;
		FileStream fileStream = new FileStream(rawVideoPath, FileMode.CreateNew);
		int num = ((m_Replay.timelineFrom != null) ? m_Replay.IndexFromValue(m_Replay.timelineFrom.value) : m_Replay.IndexFromValue(0f));
		int num2 = ((m_Replay.timelineTo != null) ? m_Replay.IndexFromValue(m_Replay.timelineTo.value) : m_Replay.IndexFromValue(1f));
		Cameras.m_AsyncCapture.Async_CompleteAllWriteToFileJobs();
		for (int i = num; i <= num2; i++)
		{
			int index = (m_Replay._asyncCapture.m_StartIndex + i) % m_Replay._asyncCapture.m_MaxFrames;
			byte[] frame = Cameras.m_AsyncCapture.GetFrame(index);
			if (frame != null)
			{
				fileStream.Write(frame, 0, frame.Length);
			}
		}
		fileStream.Close();
		DoFFMpeg(shareReplayFlags, outputPathAndFilename, basePath, watermarkPath);
	}

	private void DoFFMpeg(ShareReplayFlags shareReplayFlags, string outputPathAndFilename, string inputBasePath, string watermarkPath)
	{
		outputPathAndFilename = ((shareReplayFlags != ShareReplayFlags.Local || DumpReplays.m_Dumping) ? Path.ChangeExtension(outputPathAndFilename, ".webm") : Path.ChangeExtension(outputPathAndFilename, ".mp4"));
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("-y -framerate ").Append(m_Replay._asyncCapture.framerate.ToString());
		stringBuilder.Append(" -f rawvideo -pixel_format rgb24 -video_size " + m_Replay._asyncCapture.width + "x" + m_Replay._asyncCapture.height + " -i ");
		stringBuilder.Append(Utils.AddQuotation(Path.Combine(inputBasePath, rawVideoName)));
		if (!string.IsNullOrEmpty(watermarkPath))
		{
			stringBuilder.Append(" -i ").Append(Utils.AddQuotation(watermarkPath)).Append(" -filter_complex \"[0:v]vflip[bg];[bg][1:v]overlay = main_w - overlay_w - 5:5");
		}
		else
		{
			stringBuilder.Append(" -filter_complex \"vflip");
		}
		string text = (outputPathAndFilename.EndsWith("mp4") ? "libx264" : "libvpx");
		if (outputPathAndFilename.EndsWith("mp4"))
		{
			stringBuilder.Append("\" -vcodec " + text + " -crf 26 -b:v 2M ");
			stringBuilder.Append(Utils.AddQuotation(outputPathAndFilename));
		}
		else
		{
			string path = Path.ChangeExtension(outputPathAndFilename, ".mp4");
			stringBuilder.Append(",split[local][online]\" -map \"[local]\" -c:v libx264 -crf 26 " + Utils.AddQuotation(path) + " -map \"[online]\" -c:v libvpx -crf 26 -b:v 2M -deadline good -cpu-used 4 " + Utils.AddQuotation(outputPathAndFilename));
		}
		FFmpegCommands.DirectInput(stringBuilder.ToString());
		m_FullPathMovieJustCompressed = outputPathAndFilename;
	}

	private void Share(ShareReplayFlags flags)
	{
		string defaultOutputFilenameNoExt = GetDefaultOutputFilenameNoExt();
		string text = Replays.GetReplaysPath();
		try
		{
			if (!Directory.Exists(text))
			{
				Utils.CreateDirectory(text);
			}
			if (!Directory.Exists(text))
			{
				text = Replays.GetDefaultReplaysPath();
				Utils.CreateDirectory(text);
			}
			if (!Directory.Exists(text))
			{
				PopUpMessage.DisplayErrorOkOnly(string.Format(Localize.Get("WARN_REPLAY_CREATE_DIR_FAIL"), text));
				return;
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception validating Replay directory: " + ex.Message);
			text = Replays.GetDefaultReplaysPath();
		}
		string outputPathAndFilename = Path.Combine(text, defaultOutputFilenameNoExt);
		m_CompressionFailed = false;
		m_ShareReplayFlags = flags;
		if (m_ShareReplayFlags == ShareReplayFlags.None)
		{
			PopUpMessage.DisplayWarningOkOnly(Localize.Get("WARN_REPLAY_SHARE"));
			return;
		}
		_ = m_Replay.timelineFrom.value / (float)m_Replay._asyncCapture.framerate;
		_ = m_Replay.timelineTo.value / (float)m_Replay._asyncCapture.framerate;
		StartCoroutine(StartSaveVideo(flags, outputPathAndFilename));
		m_Status.Open(Localize.Get("UI_STATUS_COMPRESSING_REPLAY"), OnCancel);
		m_Compressing = true;
		m_RectTransform.anchoredPosition = new Vector2(0f, -5000f);
		InterfaceAudio.Play("ui_menu_accept");
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	public void OnSaveLocal()
	{
		if (Cameras.m_AsyncCapture.Aysnc_CaptureStillHasWorkToDo())
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			SaveLocal();
		}
	}

	public bool IsCompressing()
	{
		return m_Compressing;
	}

	private void SaveLocal()
	{
		Share(ShareReplayFlags.Local);
	}

	private void OnGallery()
	{
		if (Cameras.m_AsyncCapture.Aysnc_CaptureStillHasWorkToDo())
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			Share(ShareReplayFlags.Local | ShareReplayFlags.Gallery);
		}
	}

	private string GetDefaultOutputFilenameNoExt()
	{
		string empty = string.Empty;
		empty = ((GameManager.GetGameMode() == GameMode.CAMPAIGN && Campaign.m_CurrentLevel != null) ? (Campaign.m_CurrentLevel.GetPrefix() + "_" + Campaign.m_CurrentLevel.GetLocalizedDisplayNameWithoutPrefix()) : ((Workshop.m_LastPlayedWorkshopItem == null) ? ((!string.IsNullOrEmpty(SandboxSettings.m_Title)) ? SandboxSettings.m_Title : Localize.Get("MAINMENU_SANDBOX")) : (string.IsNullOrEmpty(Workshop.m_LastPlayedWorkshopItem.GetId()) ? ((!string.IsNullOrEmpty(SandboxSettings.m_Title)) ? SandboxSettings.m_Title : Localize.Get("MAINMENU_SANDBOX")) : ((!string.IsNullOrEmpty(SandboxSettings.m_Title)) ? (Workshop.m_LastPlayedWorkshopItem.GetId() + "_" + SandboxSettings.m_Title) : (Workshop.m_LastPlayedWorkshopItem.GetId() ?? "")))));
		empty = Utils.GetFileSafePreviewUrl(empty);
		string replaysPath = Replays.GetReplaysPath();
		string text = empty;
		for (int i = 1; i <= 100; i++)
		{
			if (!Utils.FileExists(Path.ChangeExtension(Path.Combine(replaysPath, text), ".mp4")))
			{
				break;
			}
			text = ((i != 99) ? $"{empty}({i})" : (empty + "_" + Utils.GenerateUniqueId()));
		}
		return text;
	}

	public void OnStart()
	{
	}

	public void OnProgress(string msg)
	{
	}

	public void OnFailure(string msg)
	{
		Debug.LogWarning("Failure " + msg);
		m_Status.Complete(success: false, Localize.Get("UI_SHAREREPLAY_FAILED"), msg, string.Empty, UnPause);
		m_Compressing = false;
		m_CompressionFailed = true;
		FFmpegCommands.Abort();
		Close();
	}

	public void OnSuccess(string msg)
	{
		m_CompressionFailed = false;
	}

	public void OnFinish()
	{
		if (!m_CompressionFailed)
		{
			m_ShareTimeSeconds = 0f;
			m_MovieCompressed = true;
			m_Compressing = false;
			m_Status.m_Title.text = Localize.Get("UI_SHAREREPLAY_SHARING");
			m_Status.m_Cancel.gameObject.SetActive(value: false);
			if (m_ShareReplayFlags.HasFlag(ShareReplayFlags.Gallery))
			{
				m_SharingFailed = false;
				string publicID = CloudinaryManager.GeneratePublicId(GameStateSim.m_BudgetUsed);
				UploadToAWSAsync(m_FullPathMovieJustCompressed, publicID, GenerateMetaData(), GenerateTags(), OnShareGalleryComplete);
			}
			else
			{
				m_SharingCompleted = true;
				m_SharingCompleteMessage = Localize.Get("UI_SHAREREPLAY_SUCCESS");
			}
		}
	}

	private byte[] GetImageBytes()
	{
		string replayFrameFullPath = Cameras.m_AsyncCapture.GetReplayFrameFullPath(0);
		if (string.IsNullOrEmpty(replayFrameFullPath))
		{
			return null;
		}
		byte[] array = File.ReadAllBytes(replayFrameFullPath);
		if (array == null || array.Length == 0)
		{
			return null;
		}
		try
		{
			Texture2D texture2D = new Texture2D(Cameras.m_AsyncCapture.width, Cameras.m_AsyncCapture.height, TextureFormat.RGB24, mipChain: false);
			texture2D.LoadRawTextureData(array);
			texture2D.Apply();
			if (Cameras.m_AsyncCapture.width > 640)
			{
				texture2D = Utils.ScaleTexture(texture2D, 640, 360);
			}
			return texture2D.EncodeToJPG();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught excaption trying to prepare image for replay: " + ex.Message);
			return null;
		}
	}

	private async void UploadToAWSAsync(string videoFullPath, string publicID, string metaData, string tags, Action<string> callback)
	{
		try
		{
			if (SteamManager.IsLoggedOn() && !SteamManager.HasAuthTicket())
			{
				AuthTicket authTicket = await SteamUser.GetAuthSessionTicketAsync();
				if (authTicket != null)
				{
					SteamManager.RegisterTicket(authTicket);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception trying to get AuthSessionTicket: " + ex.Message);
		}
		if (!SteamManager.IsLoggedOn() || !SteamManager.HasAuthTicket())
		{
			callback?.Invoke("Not Authenticated");
			return;
		}
		byte[] array = Utils.ReadAllBytes(videoFullPath);
		if (array == null || array.Length == 0)
		{
			callback?.Invoke("Failed to read video");
			return;
		}
		byte[] imageBytes = GetImageBytes();
		if (imageBytes == null || imageBytes.Length == 0)
		{
			callback?.Invoke("Failed to read image");
			return;
		}
		MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
		multipartFormDataContent.Add(new ByteArrayContent(array, 0, array.Length), "video", Utils.GenerateUniqueId());
		multipartFormDataContent.Add(new ByteArrayContent(imageBytes, 0, imageBytes.Length), "image", Utils.GenerateUniqueId());
		multipartFormDataContent.Add(new StringContent(publicID), "publicID");
		multipartFormDataContent.Add(new StringContent(tags), "tags");
		multipartFormDataContent.Add(new StringContent(metaData), "metaData");
		if (SteamManager.IsLoggedOn())
		{
			multipartFormDataContent.Add(new StringContent(SteamUtils.GetSteamId()), "steamid");
			multipartFormDataContent.Add(new StringContent(SteamManager.GetTicket()), "ticket");
		}
		try
		{
			HttpResponseMessage httpResponseMessage = await Game.m_HttpClient.PostAsync(Game.GALLERY_UPLOAD_URL, multipartFormDataContent);
			if (httpResponseMessage.IsSuccessStatusCode)
			{
				callback?.Invoke(string.Empty);
			}
			else
			{
				callback?.Invoke($"Failed with status code: {httpResponseMessage.StatusCode}");
			}
		}
		catch (Exception ex2)
		{
			Debug.Log("UploadToAWSAsync failed due to exception: " + ex2.Message);
			callback?.Invoke("Failed due to exception: " + ex2.Message);
		}
	}

	public string GetFullPathOfLastCompressedMovie()
	{
		return m_FullPathMovieJustCompressed;
	}

	private string GenerateTags()
	{
		string text = GalleryFilterParameters.CLOUDFLARE_TAG;
		if (!GameStateSim.m_LevelPassed)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.FAIL_TAG : ("," + GalleryFilterParameters.FAIL_TAG));
		}
		if (BridgeCheat.m_Cheated || Mods.m_IsUsingGameplayMod)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.CHEAT_TAG : ("," + GalleryFilterParameters.CHEAT_TAG));
		}
		if (GameStateSim.m_BudgetUsed <= Mathf.RoundToInt(Budget.m_CashBudget))
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.UNDERBUDGET_TAG : ("," + GalleryFilterParameters.UNDERBUDGET_TAG));
		}
		if (GameStateSim.m_NumBridgeBreaks == 0)
		{
			text += (string.IsNullOrEmpty(text) ? GalleryFilterParameters.UNBREAKING_TAG : ("," + GalleryFilterParameters.UNBREAKING_TAG));
		}
		return text;
	}

	private string GenerateMetaData()
	{
		string steamId = SteamUtils.GetSteamId();
		string levelId = Game.GetLevelId();
		string worldId = ((GameManager.GetGameMode() == GameMode.CAMPAIGN) ? Game.GetWorldIdWithLevel(Game.GetLevelId()) : string.Empty);
		string maxStress = GameLeaderboards.ConvertStressToScore(StressSamples.m_MaxStressNormalized).ToString();
		string budget = GameStateSim.m_BudgetUsed.ToString();
		string levelTitle = Game.GetLevelTitle();
		return GalleryMetaData.Create(steamId, levelId, worldId, maxStress, budget, levelTitle);
	}

	private void OnShareGalleryComplete(string failureMessage)
	{
		if (!string.IsNullOrEmpty(failureMessage))
		{
			m_SharingCompleteMessage = Localize.Get("UI_SHAREREPLAY_FAILED");
			m_SharingFailed = true;
			Debug.LogWarningFormat("Failed to share replay: {0}", failureMessage);
		}
		else
		{
			m_SharingCompleteMessage = Localize.Get("UI_SHAREREPLAY_SUCCESS");
			m_SharingFailed = false;
			Replays.RegisterUpload();
		}
		m_SharingCompleted = true;
		Utils.DeleteFile(Path.ChangeExtension(m_FullPathMovieJustCompressed, ".webm"));
		if (Game.IsRunningOnSteamDeck())
		{
			Utils.DeleteFile(Path.ChangeExtension(m_FullPathMovieJustCompressed, ".mp4"));
		}
		Pause();
	}

	private void UpdateDescriptionHeader()
	{
		int num = Mathf.Clamp(m_InputField.text.Length, 0, MAX_DESCRIPTION_CHARS);
		m_CharCountText.text = $"{num}/{MAX_DESCRIPTION_CHARS}";
	}

	private void ProcessInput()
	{
		if (GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			return;
		}
		if (GameInput.GetMouseButtonJustPressed(0) && !GameUI.PointerOver(typeof(Panel_ShareReplay)))
		{
			OnCancel();
		}
		else if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
		{
			OnCancel();
		}
		if (Game.IsRunningOnSteamDeck())
		{
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
			{
				OnGallery();
			}
		}
		else if (GamepadManager.ButtonJustPressed(GamepadButtonType.NORTH))
		{
			OnGallery();
		}
		else if (GamepadManager.ButtonJustPressed(GamepadButtonType.WEST))
		{
			OnSaveLocal();
		}
	}

	private void UnPause()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			Time.timeScale = BridgeSimSpeed.GetTimeScaleForSimulation();
			AudioMixerManager.UnPauseSimulationSFX();
			GameUI.m_Instance.m_TopBar.m_PausedSim = false;
		}
	}

	private void Pause()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			Time.timeScale = 0f;
			AudioMixerManager.PauseSimulationSFX();
		}
	}

	private void Close()
	{
		base.gameObject.SetActive(value: false);
	}
}
