using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class Panel_GalleryVideo : MonoBehaviour
{
	[Header("Buttons")]
	public TwoStateButton m_PlayButton;

	public Button m_NextButton;

	public Button m_PrevButton;

	[Header("Frame")]
	public RawImage m_RawImage;

	public RectTransform m_RawImageRectTransform;

	public PlayReplayButton m_PlayFromImage;

	public GameObject m_WaitAnimation;

	[Header("Header")]
	public TextMeshProUGUI m_CreatedByText;

	public Button m_CancelButton;

	[Header("Footer")]
	public Image m_Progress;

	public TextMeshProUGUI m_NameText;

	public TextMeshProUGUI m_BudgetText;

	public TextMeshProUGUI m_MaxStressText;

	public TextMeshProUGUI m_DurationText;

	public TextMeshProUGUI m_DateText;

	public TextMeshProUGUI m_CounterText;

	public Image m_WinIcon;

	public Image m_CheatIcon;

	public Image m_BreaksIcon;

	[Header("Buttons")]
	public Button m_ClipboardButton;

	public Button m_AllLevelsButton;

	public Button m_CreatedByButton;

	public Button m_WorkshopButton;

	public Button m_TrashButton;

	[Header("Playback")]
	public VideoPlayer m_VideoPlayer;

	private RenderTexture m_RenderTexture;

	private ulong m_FrameCount;

	private double m_LengthSeconds;

	private bool m_MusicPaused;

	private int m_VideoIndex;

	private int m_NumVideos;

	private GallerySlot m_Slot;

	private float m_PrepareStartTime;

	private bool m_Initialized;

	private bool m_Preparing;

	private string m_LevelID;

	private RectTransform m_CreatedByButtonRectTransform;

	private RectTransform m_AllLevelsButtonRectTransform;

	private void Awake()
	{
		m_CreatedByButtonRectTransform = m_CreatedByButton.GetComponent<RectTransform>();
		m_AllLevelsButtonRectTransform = m_AllLevelsButton.GetComponent<RectTransform>();
	}

	private void Init()
	{
		if (!m_Initialized)
		{
			m_VideoPlayer.loopPointReached += LoopPointReached;
			m_VideoPlayer.prepareCompleted += PrepareCompleted;
			m_VideoPlayer.waitForFirstFrame = true;
			AllocateRenderTexture();
			m_Progress.fillAmount = 0f;
			m_LevelID = string.Empty;
			m_PlayButton.m_Button.onClick.AddListener(OnPlay);
			m_CancelButton.onClick.AddListener(OnCancel);
			m_PlayFromImage.m_Button.onClick.AddListener(OnPlay);
			m_NextButton.onClick.AddListener(OnNext);
			m_PrevButton.onClick.AddListener(OnPrev);
			m_ClipboardButton.onClick.AddListener(OnClipboard);
			m_AllLevelsButton.onClick.AddListener(OnAllLevels);
			m_CreatedByButton.onClick.AddListener(OnCreatedBy);
			m_TrashButton.onClick.AddListener(OnTrash);
			m_WorkshopButton.onClick.AddListener(OnWorkshop);
			m_CounterText.text = string.Empty;
			m_Initialized = true;
		}
	}

	private void OnEnable()
	{
		m_WaitAnimation.SetActive(value: false);
		m_Preparing = false;
		ActivePanels.Add(base.gameObject);
		ShowGamepadLegend();
		m_ClipboardButton.gameObject.SetActive(!Game.IsRunningOnSteamDeck() && GameInput.GetActiveGameDevice() == GameDevice.KeyboardAndMouse);
		m_TrashButton.gameObject.SetActive(!GameManager.IsSteamOffline());
		m_CreatedByButton.gameObject.SetActive(!GameManager.IsSteamOffline());
	}

	private void OnDisable()
	{
		m_VideoPlayer.Stop();
		ActivePanels.Remove(base.gameObject);
	}

	private void Update()
	{
		UpdateHeader(m_Slot);
		ProcessInput();
		if (m_VideoPlayer.isPlaying)
		{
			m_RawImage.enabled = true;
			UpdateProgressBar();
			UpdateDurationText();
		}
		m_NextButton.gameObject.SetActive(!WorkshopPanelActive() && m_VideoIndex < m_NumVideos - 1);
		m_PrevButton.gameObject.SetActive(!WorkshopPanelActive() && m_VideoIndex > 0);
		m_CounterText.text = $"{m_VideoIndex + 1} / {m_NumVideos}";
		if (ActivePanels.IsTopPanel(base.gameObject))
		{
			ShowGamepadLegend();
		}
		float num = Time.unscaledTime - m_PrepareStartTime;
		m_WaitAnimation.gameObject.SetActive(m_Preparing && num > 0.5f);
	}

	public void AllocateRenderTexture()
	{
		if (m_RenderTexture == null)
		{
			m_RenderTexture = new RenderTexture(Gallery.VIDEO_PREVIEW_WIDTH, Gallery.VIDEO_PREVIEW_HEIGHT, 16);
		}
	}

	public void Open(GallerySlot slot, int videoIndex, int numVideos)
	{
		if (base.gameObject.activeInHierarchy)
		{
			Debug.Log("Video player already open");
			return;
		}
		base.gameObject.SetActive(value: true);
		Init();
		OpenInternal(slot, videoIndex, numVideos);
	}

	public void Close()
	{
		if (base.gameObject.activeInHierarchy)
		{
			m_VideoPlayer.Stop();
			m_VideoPlayer.frame = 0L;
			m_WaitAnimation.SetActive(value: false);
			if (m_MusicPaused)
			{
				Music.UnPause();
			}
			base.gameObject.SetActive(value: false);
		}
	}

	public void OnPlay()
	{
		OnPlayInternal(silent: false);
	}

	public void OnPlayInternal(bool silent)
	{
		if (string.IsNullOrEmpty(m_Slot.GetGalleryItem.GetVideoUrl()))
		{
			return;
		}
		UpdateProgressBar();
		UpdateDurationText();
		m_Preparing = false;
		if (!m_VideoPlayer.isPlaying && !m_VideoPlayer.isPaused)
		{
			m_VideoPlayer.url = m_Slot.GetGalleryItem.GetVideoUrl();
			m_VideoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			m_VideoPlayer.targetTexture = m_RenderTexture;
			m_VideoPlayer.isLooping = true;
			m_VideoPlayer.Stop();
			m_VideoPlayer.Prepare();
			m_PlayFromImage.DoPlayIconAnimation();
			m_PlayButton.TurnOn(on: false);
			m_PrepareStartTime = Time.unscaledTime;
			m_Preparing = true;
			if (!silent)
			{
				InterfaceAudio.Play("ui_menubar_gen_on");
			}
		}
		else if (m_VideoPlayer.isPlaying)
		{
			m_VideoPlayer.Pause();
			m_PlayFromImage.DoPauseIconAnimation();
			m_PlayButton.TurnOn(on: true);
			if (!silent)
			{
				InterfaceAudio.Play("ui_menubar_gen_off");
			}
		}
		else
		{
			m_VideoPlayer.Play();
			m_PlayFromImage.DoPlayIconAnimation();
			m_PlayButton.TurnOn(on: false);
			if (!silent)
			{
				InterfaceAudio.Play("ui_menubar_gen_on");
			}
		}
	}

	public string GetCreatedByToolTipText()
	{
		if (m_Slot == null)
		{
			return string.Empty;
		}
		string displayName = SteamPersonas.GetDisplayName(m_Slot.GetGalleryItem.GetOwnerId());
		if (string.IsNullOrEmpty(displayName))
		{
			return string.Empty;
		}
		return string.Format(Localize.Get("UI_CLICK_TO_VIEW_ALL_VIDEOS_BY"), GameUI.GOLD_COLOR_HEX_TAG + displayName);
	}

	public string GetAllLevelVideosToolTipText()
	{
		if (m_Slot == null)
		{
			return string.Empty;
		}
		string levelNameNoPrefix = m_Slot.GetGalleryItem.GetLevelNameNoPrefix();
		if (string.IsNullOrEmpty(levelNameNoPrefix))
		{
			return string.Empty;
		}
		return string.Format(Localize.Get("UI_CLICK_TO_VIEW_ALL_VIDEOS"), GameUI.GOLD_COLOR_HEX_TAG + levelNameNoPrefix + GameUI.WHITE_COLOR_HEX_TAG);
	}

	public bool IsPointerOverCreatedByButton()
	{
		if (m_CreatedByButtonRectTransform.gameObject.activeInHierarchy)
		{
			return TMP_TextUtilities.IsIntersectingRectTransform(m_CreatedByButtonRectTransform, GameInput.GetMousePosition(), null);
		}
		return false;
	}

	public bool IsPointerOverAllLevelsButton()
	{
		if (m_AllLevelsButtonRectTransform.gameObject.activeInHierarchy)
		{
			return TMP_TextUtilities.IsIntersectingRectTransform(m_AllLevelsButtonRectTransform, GameInput.GetMousePosition(), null);
		}
		return false;
	}

	private void OnCancel()
	{
		InterfaceAudio.Play("ui_window_close");
		Close();
	}

	private void OnWorkshop()
	{
		if (m_VideoPlayer.isPlaying)
		{
			m_VideoPlayer.Pause();
			m_PlayFromImage.DoPauseIconAnimation();
			m_PlayButton.TurnOn(on: true);
		}
		if (GameUI.m_Instance.m_Workshop.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_Workshop.m_RootPanel.gameObject.SetActive(value: true);
			GameUI.m_Instance.m_Workshop.m_Ducking.gameObject.SetActive(value: true);
		}
		else
		{
			GameUI.m_Instance.m_Workshop.Open(WorkshopView.LEVELS_AND_CAMPAIGNS, m_LevelID, OpenWorkshopCallback);
		}
	}

	private void OpenWorkshopCallback(bool success)
	{
		if (!success)
		{
			PopUpMessage.DisplayWarningOkOnly(string.Format(Localize.Get("UI_FAILED_TO_FIND_BY_LEVEL_ID", m_LevelID)));
			GameUI.m_Instance.m_Workshop.Close();
		}
	}

	private void OnClipboard()
	{
		if (m_Slot == null || m_Slot.GetGalleryItem == null)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		string levelNameWithoutColorizationTags = m_Slot.GetGalleryItem.GetLevelNameWithoutColorizationTags();
		string budget = m_Slot.GetGalleryItem.GetBudget();
		string maxStress = m_Slot.GetGalleryItem.GetMaxStress();
		string videoUrl = m_Slot.GetGalleryItem.GetVideoUrl();
		string displayName = SteamPersonas.GetDisplayName(m_Slot.GetGalleryItem.GetOwnerId());
		string levelID = m_Slot.GetGalleryItem.GetLevelID();
		if (!string.IsNullOrEmpty(levelID) && levelID.Length > 3)
		{
			GameUI.CopyToClipboard(levelNameWithoutColorizationTags + " | " + levelID + " | $" + budget + " | " + maxStress + " | " + displayName + "\n" + videoUrl);
		}
		else
		{
			GameUI.CopyToClipboard(levelNameWithoutColorizationTags + " | $" + budget + " | " + maxStress + " | " + displayName + "\n" + videoUrl);
		}
	}

	private void OnNext()
	{
		int slotIndex = SlotIndexFromVideoIndex(m_VideoIndex);
		if (IsLastSlot(slotIndex) && !GameUI.m_Instance.m_Gallery.OnLastPage())
		{
			GameUI.m_Instance.m_Gallery.MoveToNextPage();
		}
		m_VideoIndex++;
		slotIndex = SlotIndexFromVideoIndex(m_VideoIndex);
		GallerySlot slotForIndex = GameUI.m_Instance.m_Gallery.GetSlotForIndex(slotIndex);
		if (!slotForIndex)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		m_VideoPlayer.Stop();
		OpenInternal(slotForIndex, m_VideoIndex, m_NumVideos);
		InterfaceAudio.Play("ui_menu_select");
	}

	private void OnPrev()
	{
		if (SlotIndexFromVideoIndex(m_VideoIndex) == 0 && !GameUI.m_Instance.m_Gallery.OnFirstPage())
		{
			GameUI.m_Instance.m_Gallery.MoveToPreviousPage();
		}
		m_VideoIndex--;
		int index = SlotIndexFromVideoIndex(m_VideoIndex);
		GallerySlot slotForIndex = GameUI.m_Instance.m_Gallery.GetSlotForIndex(index);
		if (!slotForIndex)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		m_VideoPlayer.Stop();
		OpenInternal(slotForIndex, m_VideoIndex, m_NumVideos);
		InterfaceAudio.Play("ui_menu_select");
	}

	private int SlotIndexFromVideoIndex(int videoIndex)
	{
		return videoIndex % Gallery.NUM_SLOTS_PER_PAGE;
	}

	private bool IsLastSlot(int slotIndex)
	{
		return slotIndex == Gallery.NUM_SLOTS_PER_PAGE - 1;
	}

	private void LoopPointReached(VideoPlayer videoPlayer)
	{
	}

	private void PrepareCompleted(VideoPlayer videoPlayer)
	{
		_ = Time.realtimeSinceStartup;
		_ = m_PrepareStartTime;
		m_Preparing = false;
		m_FrameCount = videoPlayer.frameCount;
		m_LengthSeconds = videoPlayer.length;
		m_WaitAnimation.SetActive(value: false);
		videoPlayer.Play();
	}

	private void UpdateProgressBar()
	{
		m_Progress.fillAmount = GetNormalizedProgress();
	}

	private void UpdateDurationText()
	{
		float num = GetNormalizedProgress() * (float)m_LengthSeconds;
		float num2 = (float)m_LengthSeconds;
		int num3 = Mathf.FloorToInt(num / 60f);
		int num4 = Mathf.FloorToInt(num) % 60;
		int num5 = Mathf.FloorToInt(num2 / 60f);
		int num6 = Mathf.FloorToInt(num2) % 60;
		m_DurationText.text = $"{num3}:{num4:D2}  /  {num5}:{num6:D2}";
	}

	private float GetNormalizedProgress()
	{
		if (m_FrameCount == 0L)
		{
			return 1f;
		}
		return Mathf.Clamp01((float)(m_VideoPlayer.frame + 1) / (float)m_FrameCount);
	}

	private void UpdateVideoFooter(GallerySlot slot)
	{
		m_NameText.color = Color.white;
		m_NameText.text = slot.GetGalleryItem.GetLevelNameFormatted();
		string maxStress = slot.GetGalleryItem.GetMaxStress();
		m_MaxStressText.text = maxStress;
		m_BudgetText.text = slot.GetGalleryItem.GetBudget();
		m_DateText.text = slot.m_DateText.text;
		m_WinIcon.gameObject.SetActive(slot.m_WinIcon.gameObject.activeInHierarchy);
		m_CheatIcon.gameObject.SetActive(slot.m_CheatIcon.gameObject.activeInHierarchy);
		bool active = slot.GetGalleryItem.HasBreaks();
		m_BreaksIcon.gameObject.SetActive(active);
		m_LevelID = slot.GetGalleryItem.GetLevelID();
		m_WorkshopButton.gameObject.SetActive(m_LevelID.Length > 3 && !WeeklyChallenges.IsAWeeklyChallenge(m_LevelID));
		m_AllLevelsButton.gameObject.SetActive(m_LevelID.Length > 0);
		string ownerId = m_Slot.GetGalleryItem.GetOwnerId();
		m_TrashButton.gameObject.SetActive(!string.IsNullOrEmpty(ownerId) && ownerId == SteamUtils.GetSteamId() && !GameManager.IsSteamOffline());
		m_CreatedByButton.gameObject.SetActive(!string.IsNullOrEmpty(ownerId) && ownerId != SteamUtils.GetSteamId() && !GameManager.IsSteamOffline());
	}

	private void OnGalleryDeleteComplete(string failureMessage)
	{
		if (!string.IsNullOrEmpty(failureMessage))
		{
			GameUI.m_Instance.m_Status.Complete(string.Format(Localize.Get("UI_GALLERY_ITEM_DELETE_FAIL"), failureMessage));
			Debug.LogWarningFormat("Gallery Item Delete Failure: {0}", failureMessage);
		}
		else
		{
			GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_GALLERY_ITEM_DELETED"));
			m_Slot.gameObject.SetActive(value: false);
		}
	}

	private void OnAllLevels()
	{
		if (GameUI.m_Instance.m_Gallery.SetLevelFilterForSlot(m_Slot))
		{
			InterfaceAudio.Play("ui_menu_select");
			Close();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	private void OnCreatedBy()
	{
		string ownerId = m_Slot.GetGalleryItem.GetOwnerId();
		string text = m_CreatedByText.text;
		if (!string.IsNullOrEmpty(ownerId) && !string.IsNullOrEmpty(text))
		{
			InterfaceAudio.Play("ui_menu_select");
			GameUI.m_Instance.m_Gallery.DownloadAllItemsByOwnerAsync(ownerId, text);
			Close();
		}
		else
		{
			InterfaceAudio.PlayErrorBeep();
		}
	}

	public WorkshopItemSlot AllocateWorkshopSlot(WorkshopItem item)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_WorkshopItemSlotPrefab);
		if (!gameObject)
		{
			return null;
		}
		WorkshopItemSlot component = gameObject.GetComponent<WorkshopItemSlot>();
		if (!component)
		{
			return null;
		}
		component.m_Item = item;
		component.UpdateFields();
		return component;
	}

	private bool WorkshopPanelActive()
	{
		return GameUI.m_Instance.m_Workshop.m_WorkshopItemPanel.gameObject.activeInHierarchy;
	}

	public void OpenInternal(GallerySlot slot, int videoIndex, int numVideos)
	{
		if (slot.GetGalleryItem == null)
		{
			Debug.LogWarning("Gallery item no longer available, closing");
			Close();
			return;
		}
		m_VideoIndex = videoIndex;
		m_NumVideos = numVideos;
		m_Slot = slot;
		UpdateHeader(slot);
		UpdateVideoFooter(slot);
		m_PlayButton.TurnOn(on: true);
		m_NextButton.gameObject.SetActive(m_VideoIndex < m_NumVideos - 1);
		m_PrevButton.gameObject.SetActive(m_VideoIndex > 0);
		m_RawImage.texture = m_RenderTexture;
		if (slot.PreviewTexture != null)
		{
			m_RawImage.enabled = true;
			Graphics.Blit(slot.PreviewTexture, m_RenderTexture);
		}
		else
		{
			m_RawImage.enabled = false;
			RenderTexture active = RenderTexture.active;
			RenderTexture.active = m_RenderTexture;
			GL.Clear(clearDepth: true, clearColor: true, Color.black);
			RenderTexture.active = active;
		}
		OnPlayInternal(silent: true);
	}

	private void ProcessInput()
	{
		if (!GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject))
		{
			if (Input.GetKeyDown(KeyCode.Escape) || GamepadManager.ButtonJustPressed(GamepadButtonType.EAST))
			{
				OnCancel();
			}
			if (Input.GetKeyDown(KeyCode.RightArrow) && m_NextButton.gameObject.activeInHierarchy && m_NextButton.interactable)
			{
				OnNext();
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow) && m_PrevButton.gameObject.activeInHierarchy && m_PrevButton.interactable)
			{
				OnPrev();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_RIGHT))
			{
				CycleToNextVideo();
			}
			if (GamepadManager.ButtonJustPressed(GamepadButtonType.DPAD_LEFT))
			{
				CycleToPrevVideo();
			}
		}
	}

	public void CycleToNextVideo()
	{
		if (m_VideoIndex >= m_NumVideos - 1)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			ExecuteEvents.Execute(m_NextButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	public void CycleToPrevVideo()
	{
		if (m_VideoIndex == 0)
		{
			InterfaceAudio.PlayErrorBeep();
		}
		else
		{
			ExecuteEvents.Execute(m_PrevButton.gameObject, new BaseEventData(Main.m_Instance.m_EventSystem), ExecuteEvents.submitHandler);
		}
	}

	private void OnTrash()
	{
		PopUpMessage.DisplayWarning(Localize.Get("POPUP_GALLERY_DELETE"), useYesNoLables: true, OnDeleteConfirm);
	}

	private void OnDeleteConfirm()
	{
		GameUI.m_Instance.m_Status.Open(Localize.Get("UI_DELETING_GALLERY_ITEM"));
		CloudinaryManager.DeleteVideoAsync(m_Slot.GetGalleryItem.GetId(), m_Slot.GetGalleryItem.GetResourceType(), OnDeleteCompleted);
	}

	private async void OnDeleteCompleted(string result)
	{
		if (!string.IsNullOrEmpty(result))
		{
			GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_GALLERY_ITEM_DELETE_FAIL"));
			Debug.LogWarning("Gallery Item delete failed due to: " + result);
			return;
		}
		Close();
		Gallery.RegisterDeleteItem();
		await Task.Delay(1000);
		GameUI.m_Instance.m_Gallery.ForceRefresh();
		GameUI.m_Instance.m_Status.Complete(Localize.Get("UI_GALLERY_ITEM_DELETED"));
	}

	private void UpdateHeader(GallerySlot slot)
	{
		if (slot != null && slot.GetGalleryItem != null)
		{
			string displayName = SteamPersonas.GetDisplayName(slot.GetGalleryItem.GetOwnerId());
			if (string.IsNullOrEmpty(displayName))
			{
				m_CreatedByText.gameObject.SetActive(value: false);
				return;
			}
			m_CreatedByText.text = Localize.Get("UI_WORKSHOP_BY", displayName);
			m_CreatedByText.gameObject.SetActive(value: true);
		}
	}

	private void ShowGamepadLegend()
	{
		if (m_NumVideos > 1)
		{
			GameUI.m_Instance.m_GamepadLegend.ShowButtonsLeft(GamepadButtonType.DPAD_HORIZONTAL, Localize.Get("UI_CHANGE_VIDEO"));
		}
		else
		{
			GameUI.m_Instance.m_GamepadLegend.HideButtonsLeft();
		}
		GameUI.m_Instance.m_GamepadLegend.ShowButtons(GamepadButtonType.SOUTH, Localize.Get("UI_SELECT"), GamepadButtonType.EAST, Localize.Get("UI_CLOSE"));
	}
}
