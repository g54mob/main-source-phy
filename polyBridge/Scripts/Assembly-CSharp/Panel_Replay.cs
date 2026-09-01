using System;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Replay : MonoBehaviour
{
	public RawImage rawImage;

	public PointerEvents timelineCurrentPointerEvents;

	public Slider timelineCurrent;

	public Slider timelineFrom;

	public Slider timelineTo;

	public PlayReplayButton m_PlayFromImage;

	public TwoStateButton m_PlayFromTimeline;

	[Header("Timeline Markers")]
	public PointerEvents m_TimelineFromPointerEvents;

	public PointerEvents m_TimelineToPointerEvents;

	public Image m_TimeLineFromIcon;

	public Image m_TimeLineToIcon;

	private float m_LastSliderValue;

	[NonSerialized]
	public AsyncCapture _asyncCapture;

	private bool m_Playing;

	private int m_CurrentFrameIndex;

	private float m_ElapsedTime;

	private bool m_ForceCurrentToFrom;

	private bool m_ForceCurrentToEnd;

	private void Awake()
	{
		timelineCurrentPointerEvents.RegisterOnDownDelegate(OnTimelineCurrentDown);
	}

	private void Start()
	{
		timelineCurrent.onValueChanged.AddListener(TimelineCurrentChanged);
		m_PlayFromImage.m_Button.onClick.AddListener(OnPlayFromImage);
		if (timelineFrom != null)
		{
			m_TimelineFromPointerEvents.RegisterOnDownDelegate(OnTimelineFromDown);
			timelineFrom.onValueChanged.AddListener(TimelineFromChanged);
		}
		if (timelineTo != null)
		{
			m_TimelineToPointerEvents.RegisterOnDownDelegate(OnTimelineToDown);
			timelineTo.onValueChanged.AddListener(TimelineToChanged);
		}
		if (m_PlayFromTimeline != null)
		{
			m_PlayFromTimeline.m_Button.onClick.AddListener(OnPlayFromTimeline);
		}
	}

	private void Update()
	{
		float num = 1f / (float)_asyncCapture.framerate;
		if (m_Playing)
		{
			m_ElapsedTime += Time.unscaledDeltaTime;
			timelineCurrent.value = Mathf.Clamp01(m_ElapsedTime / ((float)_asyncCapture.m_NumFrames * num));
			m_CurrentFrameIndex = IndexFromValue(timelineCurrent.value);
			int num2 = ((timelineTo != null) ? IndexFromValue(timelineTo.value) : IndexFromValue(1f));
			if (m_CurrentFrameIndex >= num2)
			{
				m_Playing = false;
				timelineCurrent.value = 1f;
				m_CurrentFrameIndex = ((timelineFrom != null) ? IndexFromValue(timelineFrom.value) : IndexFromValue(0f));
				m_PlayFromImage.DisplayPlayIconStatic();
			}
		}
		if (m_LastSliderValue != timelineCurrent.value && GameInput.GetMouseButtonIsDown(0))
		{
			m_PlayFromImage.HideButton();
		}
		if (timelineTo != null && timelineFrom != null)
		{
			if (timelineFrom.value + Mathf.Epsilon > timelineTo.value)
			{
				timelineFrom.value = timelineTo.value;
			}
			if (timelineTo.value - Mathf.Epsilon < timelineFrom.value)
			{
				timelineTo.value = timelineFrom.value;
			}
			timelineCurrent.value = Mathf.Clamp(timelineCurrent.value, timelineFrom.value, timelineTo.value);
		}
		if (m_ForceCurrentToFrom)
		{
			timelineCurrent.value = timelineFrom.value;
			m_ForceCurrentToFrom = false;
		}
		if (m_ForceCurrentToEnd)
		{
			timelineCurrent.value = timelineTo.value;
			m_ForceCurrentToEnd = false;
		}
		if (GameInput.GetMouseButtonJustReleased(0))
		{
			ResetTimelineMarkersColor();
		}
		m_LastSliderValue = timelineCurrent.value;
		if (m_PlayFromTimeline != null)
		{
			m_PlayFromTimeline.TurnOn(m_Playing);
		}
	}

	public bool Show(bool play)
	{
		if (Cameras.ReplayCamera() == null)
		{
			return false;
		}
		if (!_asyncCapture)
		{
			_asyncCapture = Cameras.m_AsyncCapture;
		}
		if (_asyncCapture.m_NumFrames < 2)
		{
			return false;
		}
		if (!rawImage.texture || rawImage.texture.width != _asyncCapture.width || rawImage.texture.height != _asyncCapture.height)
		{
			if (rawImage.texture != null)
			{
				UnityEngine.Object.Destroy(rawImage.texture);
			}
			rawImage.texture = AllocateRawImageTexture(_asyncCapture.width, _asyncCapture.height);
		}
		if (!rawImage.texture)
		{
			Debug.LogWarningFormat("Failed to allocate {0} x {0} texture for Replay UI", _asyncCapture.width, _asyncCapture.height);
			return false;
		}
		timelineCurrent.minValue = 0f;
		ResetTimelineMarkersColor();
		if (timelineFrom != null)
		{
			timelineFrom.minValue = 0f;
			timelineFrom.maxValue = 1f;
			timelineFrom.value = 0f;
		}
		if (timelineTo != null)
		{
			timelineTo.minValue = 0f;
			timelineTo.maxValue = 1f;
			timelineTo.value = 1f;
		}
		timelineCurrent.minValue = 0f;
		timelineCurrent.maxValue = 1f;
		timelineCurrent.value = 0f;
		m_CurrentFrameIndex = 0;
		m_Playing = play;
		m_ElapsedTime = 0f;
		ShowFrame(0);
		if (play)
		{
			m_PlayFromImage.DoPlayIconAnimation();
		}
		m_LastSliderValue = timelineCurrent.value;
		return true;
	}

	public void Hide()
	{
		m_Playing = false;
	}

	public void DisableTimelineMarkers()
	{
		if (timelineFrom != null)
		{
			timelineFrom.gameObject.SetActive(value: false);
		}
		if (timelineTo != null)
		{
			timelineTo.gameObject.SetActive(value: false);
		}
	}

	public void OnPlayFromImage()
	{
		if (m_Playing)
		{
			m_Playing = false;
			m_PlayFromImage.DoPauseIconAnimation();
			InterfaceAudio.Play("ui_menubar_gen_off");
			return;
		}
		m_Playing = true;
		m_PlayFromImage.DoPlayIconAnimation();
		int num = IndexFromValue(timelineCurrent.value);
		int num2 = ((timelineTo != null) ? IndexFromValue(timelineTo.value) : IndexFromValue(1f));
		if (num >= num2)
		{
			timelineCurrent.value = ((timelineFrom != null) ? timelineFrom.value : 0f);
		}
		m_CurrentFrameIndex = num;
		float num3 = 1f / (float)_asyncCapture.framerate;
		m_ElapsedTime = timelineCurrent.value * ((float)_asyncCapture.m_NumFrames * num3);
		InterfaceAudio.Play("ui_menubar_gen_on");
	}

	public void OnPlayFromTimeline()
	{
		OnPlayFromImage();
	}

	private void ShowFrame(int index)
	{
		_asyncCapture.Async_CompleteAllWriteToFileJobs();
		int index2 = (_asyncCapture.m_StartIndex + index) % _asyncCapture.m_MaxFrames;
		byte[] frame = _asyncCapture.GetFrame(index2);
		if (frame != null)
		{
			((Texture2D)rawImage.texture).LoadRawTextureData(frame);
			((Texture2D)rawImage.texture).Apply();
		}
	}

	public void TimelineCurrentChanged(float ignore)
	{
		int index = IndexFromValue(timelineCurrent.value);
		ShowFrame(index);
	}

	public void TimelineFromChanged(float ignore)
	{
		m_Playing = false;
		m_ForceCurrentToFrom = true;
	}

	public void TimelineToChanged(float ignore)
	{
		m_Playing = false;
		m_ForceCurrentToEnd = true;
	}

	private Texture AllocateRawImageTexture(int width, int height)
	{
		return new Texture2D(width, height, TextureFormat.RGB24, mipChain: false);
	}

	private void OnTimelineCurrentDown()
	{
		m_Playing = false;
	}

	private void OnTimelineFromDown()
	{
		m_TimeLineFromIcon.color = Color.white;
	}

	private void OnTimelineToDown()
	{
		m_TimeLineToIcon.color = Color.white;
	}

	private int GetFrameNumberFromEnd(float backoffSeconds)
	{
		return _asyncCapture.m_NumFrames - 1 - Mathf.RoundToInt(backoffSeconds * (float)_asyncCapture.framerate);
	}

	public int IndexFromValue(float value)
	{
		return Mathf.RoundToInt(value * (float)(_asyncCapture.m_NumFrames - 1));
	}

	private void ResetTimelineMarkersColor()
	{
		if (m_TimeLineFromIcon != null)
		{
			m_TimeLineFromIcon.color = Color.white;
		}
		if (m_TimeLineToIcon != null)
		{
			m_TimeLineToIcon.color = Color.white;
		}
	}
}
