using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Panel_HelpSlot : MonoBehaviour
{
	public int m_SlotIndex;

	public GameObject m_PanelRoot;

	[Header("Buttons")]
	public TwoStateButton m_PlayButton;

	[Header("Frame")]
	public Image m_Highlight;

	public RawImage m_RawImage;

	public RectTransform m_RawImageRectTransform;

	public PlayReplayButton m_PlayFromImage;

	[Header("Footer")]
	public Image m_Progress;

	public Button m_LoadPreviewButton;

	public TextMeshProUGUI m_LoadPreviewButtonText;

	public TextMeshProUGUI m_DurationText;

	[Header("Playback")]
	public VideoPlayer m_VideoPlayer;

	[NonSerialized]
	public bool m_WaitingForPrepare;

	[NonSerialized]
	public string m_PreparedForLevelID;

	private string m_VideoFilename;

	private RenderTexture m_RenderTexture;

	private ulong m_FrameCount;

	private double m_LengthSeconds;

	private string m_GeneratingForLevelID;

	private bool m_Initialized;

	private Action<Panel_HelpSlot> m_LoadPreviewCallback;

	private Action<Panel_HelpSlot, string> m_GeneratedPreviewCallback;

	private BridgeSaveData m_BridgeSaveData;

	private void OnEnable()
	{
		UpdateButton();
	}

	private void OnDisable()
	{
		m_VideoPlayer.Stop();
	}

	private void Update()
	{
		ProcessInput();
		UpdateButton();
		if (m_VideoPlayer.isPlaying)
		{
			m_RawImage.enabled = true;
			UpdateProgressBar();
			UpdateDurationText();
		}
	}

	public void AllocateRenderTexture()
	{
		m_RenderTexture = new RenderTexture(Gallery.VIDEO_PREVIEW_WIDTH, Gallery.VIDEO_PREVIEW_HEIGHT, 16);
	}

	public bool PreviewFilenameExists(int index, string levelFilename)
	{
		return Utils.FileExists(GetSlotFilename(index, levelFilename));
	}

	public bool GeneratePreview(int index, string levelID, string levelFilename, Action<Panel_HelpSlot, string> generatedCallback, Action<Panel_HelpSlot> clickCallback)
	{
		m_LoadPreviewCallback = clickCallback;
		m_GeneratedPreviewCallback = generatedCallback;
		m_GeneratingForLevelID = levelID;
		string slotFilename = GetSlotFilename(index, levelFilename);
		if (!Utils.FileExists(slotFilename))
		{
			return false;
		}
		if (!LoadShadowBridgeData(slotFilename))
		{
			return false;
		}
		Init();
		string videoFilename = GetVideoFilename(index, levelFilename);
		if (!PrepareForPlayback(videoFilename))
		{
			return false;
		}
		return true;
	}

	public void OnPlay()
	{
		OnPlaySilent();
		if (m_VideoPlayer.isPaused)
		{
			InterfaceAudio.Play("ui_menubar_gen_off");
		}
		else
		{
			InterfaceAudio.Play("ui_menubar_gen_on");
		}
	}

	public void OnPlaySilent()
	{
		if (!string.IsNullOrEmpty(m_VideoFilename))
		{
			GameUI.m_Instance.m_Help.PauseHelpSlotsExcept(this);
			UpdateProgressBar();
			UpdateDurationText();
			if (!m_VideoPlayer.isPlaying && !VideoIsPaused())
			{
				m_VideoPlayer.source = VideoSource.Url;
				m_VideoPlayer.url = m_VideoFilename;
				m_VideoPlayer.audioOutputMode = VideoAudioOutputMode.None;
				m_VideoPlayer.targetTexture = m_RenderTexture;
				m_VideoPlayer.Stop();
				m_VideoPlayer.Prepare();
				m_PlayFromImage.HideButton();
			}
			else if (!VideoIsPaused())
			{
				m_VideoPlayer.Pause();
				m_PlayFromImage.DoPauseIconAnimation();
				m_PlayButton.TurnOn(on: true);
			}
			else
			{
				m_VideoPlayer.playbackSpeed = 1f;
				m_VideoPlayer.Play();
				m_PlayFromImage.DoPlayIconAnimation();
				m_PlayButton.TurnOn(on: false);
			}
		}
	}

	public bool IsHighlighted()
	{
		return m_Highlight.gameObject.activeInHierarchy;
	}

	public void SetHighlight(bool on)
	{
		m_Highlight.gameObject.SetActive(on);
		m_LoadPreviewButtonText.text = (on ? Localize.Get("UI_CLEAR_HELP") : Localize.Get("UI_LOAD_PREVIEW"));
	}

	public BridgeSaveData GetBridgeSaveData()
	{
		return m_BridgeSaveData;
	}

	public bool IsVideoPrepared()
	{
		return m_VideoPlayer.isPrepared;
	}

	public void Hide(bool hide)
	{
		m_PanelRoot.SetActive(!hide);
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
			m_PlayFromImage.m_Button.onClick.AddListener(OnPlay);
			m_PlayButton.m_Button.onClick.AddListener(OnPlay);
			m_LoadPreviewButton.onClick.AddListener(OnLoadPreview);
			m_Initialized = true;
		}
	}

	private bool PrepareForPlayback(string videoFilename)
	{
		if (string.IsNullOrEmpty(videoFilename))
		{
			Debug.LogWarning("Slot filename not found");
			return false;
		}
		m_VideoFilename = videoFilename;
		m_PlayButton.TurnOn(on: true);
		m_PlayFromImage.HideButton();
		m_RawImage.texture = m_RenderTexture;
		m_RawImage.enabled = false;
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = m_RenderTexture;
		GL.Clear(clearDepth: true, clearColor: true, Color.black);
		RenderTexture.active = active;
		m_VideoPlayer.playbackSpeed = 1f;
		OnPlaySilent();
		m_VideoPlayer.playbackSpeed = 1E-05f;
		return true;
	}

	private bool LoadShadowBridgeData(string slotFilename)
	{
		BridgeSaveSlotData bridgeSaveSlotData = BridgeSaveSlots.Load(slotFilename);
		if (bridgeSaveSlotData == null)
		{
			Debug.LogWarning("Failed to load bridge slot: " + slotFilename);
			m_BridgeSaveData = null;
			return false;
		}
		m_BridgeSaveData = new BridgeSaveData();
		int offset = 0;
		m_BridgeSaveData.DeserializeBinary(bridgeSaveSlotData.m_Bridge, ref offset);
		return true;
	}

	private void OnLoadPreview()
	{
		m_LoadPreviewCallback?.Invoke(this);
	}

	private void LoopPointReached(VideoPlayer videoPlayer)
	{
		m_VideoPlayer.Play();
	}

	private void PrepareCompleted(VideoPlayer videoPlayer)
	{
		m_FrameCount = videoPlayer.frameCount;
		m_LengthSeconds = videoPlayer.length;
		videoPlayer.Play();
		m_GeneratedPreviewCallback?.Invoke(this, m_GeneratingForLevelID);
	}

	private void UpdateButton()
	{
		if (!BridgeShadow.IsActive())
		{
			SetHighlight(on: false);
		}
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
		if (m_VideoPlayer.frame == 0L)
		{
			return 0f;
		}
		return Mathf.Clamp01((float)(m_VideoPlayer.frame + 1) / (float)m_FrameCount);
	}

	private void ProcessInput()
	{
		GameStateCommonInput.IgnoreKeyboardInputForPanel(base.gameObject);
	}

	private string GetSlotFilename(int index, string filename)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
		string arg = Path.Combine(Application.streamingAssetsPath, "LevelSolutions", "Slots", fileNameWithoutExtension);
		return $"{arg}_{index + 1}.slot";
	}

	private string GetVideoFilename(int index, string filename)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
		string arg = Path.Combine(Application.streamingAssetsPath, "LevelSolutions", "Videos", fileNameWithoutExtension);
		return $"{arg}_{index + 1}.webm";
	}

	private bool VideoIsPaused()
	{
		if (!m_VideoPlayer.isPaused)
		{
			return m_VideoPlayer.playbackSpeed < 1f;
		}
		return true;
	}
}
