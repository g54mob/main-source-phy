using System;
using System.Collections;
using UnityEngine;

public class TimeSliderView : MonoBehaviour
{
	public static TimeSliderView Instance;

	public Camera hudCam;

	public Transform sliderHandle;

	public Tooltip tooltip;

	public bool instanceUpdating;

	public SimpleUIButton autoToggle;

	public MeshRenderer clockIcon;

	public MeshRenderer autoIcon;

	public Transform sliderStart;

	public Transform sliderEnd;

	public Renderer knobRenderer;

	public bool useArrowSystem = true;

	public float arrowTreshold = 0.67f;

	public GameObject ArrowParent;

	public float minSpeedToGo = 0.1f;

	public TextMesh TextMeshy;

	public DynamicText textNew;

	public PauseTime pauseTimeButton;

	public float sensitivity = 2f;

	public Color textOriginalCol;

	public Renderer resetIcon;

	public bool paused;

	public float smoothPauseSpeed = 3f;

	public TimeSliderObject sliderKnob;

	public TimeSlider timeSlider;

	protected bool isClicked;

	private bool showingArrows;

	private Vector3 startPos;

	private float sliderWidth;

	private float lastSliderPosX = -1f;

	private bool hasFadedAlpha;

	private float fadeAlpha = 0.2f;

	private float normalAlpha = 0.5f;

	private bool autoModeActive = true;

	private float prevTime;

	private float timeSinceArrowOff = 100f;

	private float deadzoneArrows = 10f;

	private float lastTextNumber = -1f;

	private float timer;

	private bool autoAssigned;

	private bool SliderActive
	{
		get
		{
			return !StatMaster.isMP || StatMaster.isHosting || !StatMaster.Mode.LevelEditor.clientGlobalSim || StatMaster.InLocalPlayMode;
		}
	}

	private float sliderStartX
	{
		get
		{
			return sliderStart.position.x;
		}
	}

	private float sliderEndX
	{
		get
		{
			return sliderEnd.position.x;
		}
	}

	private bool autoScale
	{
		get
		{
			return OptionsMaster.BesiegeConfig.AutoTimeScale;
		}
		set
		{
			OptionsMaster.BesiegeConfig.AutoTimeScale = value;
		}
	}

	private void Start()
	{
		Instance = this;
		timeSlider = SingleInstanceFindOnly<AddPiece>.Instance.timeSlider;
		TimeSlider obj = timeSlider;
		obj.onScaleChanged = (Action<float>)Delegate.Combine(obj.onScaleChanged, new Action<float>(OnScaleChanged));
		sliderWidth = sliderEndX - sliderStartX;
		TimeSliderObject timeSliderObject = sliderKnob;
		timeSliderObject.onClicked = (Action)Delegate.Combine(timeSliderObject.onClicked, new Action(OnKnobClicked));
		TimeSliderObject timeSliderObject2 = sliderKnob;
		timeSliderObject2.onClickHeld = (Action)Delegate.Combine(timeSliderObject2.onClickHeld, new Action(OnKnobHeld));
		TimeSliderObject timeSliderObject3 = sliderKnob;
		timeSliderObject3.onClickReleased = (Action)Delegate.Combine(timeSliderObject3.onClickReleased, new Action(OnKnobReleased));
		TimeSliderObject timeSliderObject4 = sliderKnob;
		timeSliderObject4.onScroll = (Action<float>)Delegate.Combine(timeSliderObject4.onScroll, new Action<float>(OnScroll));
		if (!StatMaster.isMP)
		{
			SetUpAutoScale();
		}
		else
		{
			ReferenceMaster.OnConnect += SetUpAutoScale;
		}
		if (hudCam == null)
		{
			hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
			if (hudCam == null)
			{
				Debug.Log("Could not find hud camera in TimeSlider");
				return;
			}
		}
		OnScaleChanged(timeSlider.percentagey);
		if ((bool)TextMeshy)
		{
			textOriginalCol = TextMeshy.color;
		}
	}

	private void OnDestroy()
	{
		ReferenceMaster.OnConnect -= SetUpAutoScale;
	}

	private void SetUpAutoScale()
	{
		if (autoToggle != null)
		{
			if (!autoAssigned)
			{
				autoToggle.Click += ToggleAuto;
			}
			autoAssigned = true;
			SetAuto(autoScale, true);
			return;
		}
		if (autoAssigned)
		{
			autoToggle.Click -= ToggleAuto;
		}
		autoAssigned = false;
		clockIcon.enabled = true;
		autoIcon.enabled = false;
	}

	public void ToggleAuto()
	{
		if (!StatMaster.isMP || StatMaster.isHosting || StatMaster.IsLevelEditorOnly)
		{
			SetAuto(!autoScale);
			return;
		}
		clockIcon.enabled = true;
		autoIcon.enabled = false;
	}

	public void SetAuto(bool b, bool force = false)
	{
		if (autoScale != b || force)
		{
			autoScale = b;
			clockIcon.enabled = !b;
			autoIcon.enabled = b;
			timeSlider.SetAuto(b);
		}
	}

	public void ChangePercentage(float delta)
	{
		float percentage = Mathf.Clamp(TimeSlider.Instance.delegateTimeScale + delta, 0f, 2f);
		SetPercentage(percentage);
	}

	private void OnScroll(float delta)
	{
		SetAuto(false);
		ChangePercentage(delta);
	}

	private void OnKnobClicked()
	{
		if (SliderActive)
		{
			SetAuto(false);
			timer = 0f;
			startPos = sliderHandle.position;
			autoModeActive = false;
		}
	}

	private void OnKnobHeld()
	{
		if (SliderActive)
		{
			timer += Time.unscaledDeltaTime;
			if (timer > 0.1f)
			{
				Vector3 vector = hudCam.ScreenToWorldPoint(InputManager.CursorPosition());
				sliderHandle.position = new Vector3(Mathf.Clamp(vector.x, sliderStartX, sliderEndX), startPos.y, startPos.z);
				CalculatePercentage();
			}
		}
	}

	private void OnKnobReleased()
	{
		if (!autoModeActive && SliderActive)
		{
			if (timer <= 0.1f)
			{
				Vector3 vector = hudCam.ScreenToWorldPoint(InputManager.CursorPosition());
				sliderHandle.position = new Vector3(Mathf.Clamp(vector.x, sliderStartX, sliderEndX), startPos.y, startPos.z);
				CalculatePercentage();
			}
			timeSlider.SendTimeScale(false);
			autoModeActive = true;
		}
	}

	private void OnEnable()
	{
		instanceUpdating = true;
	}

	private void OnDisable()
	{
		instanceUpdating = false;
	}

	private void Update()
	{
		if (!StatMaster.isMP)
		{
			return;
		}
		bool flag = !SliderActive;
		if (flag != hasFadedAlpha)
		{
			knobRenderer.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, (!flag) ? normalAlpha : fadeAlpha));
			hasFadedAlpha = flag;
		}
		if (StatMaster.isHosting && useArrowSystem && !autoScale)
		{
			if (timeSinceArrowOff > deadzoneArrows && StatMaster.InGlobalPlayMode && timeSlider.percentagey > 0.25f && ServerHealth.Instance.Health <= arrowTreshold)
			{
				ToggleArrows(true);
			}
			else
			{
				ToggleArrows(false);
			}
			timeSinceArrowOff += Time.unscaledDeltaTime;
		}
	}

	public void ToggleArrows(bool b)
	{
		if (ArrowParent == null)
		{
			if (tooltip != null)
			{
				tooltip.enabled = false;
			}
		}
		else
		{
			if (b == showingArrows)
			{
				return;
			}
			if (!b)
			{
				timeSinceArrowOff = 0f;
				ArrowParent.SetActive(false);
				if (tooltip != null)
				{
					tooltip.enabled = false;
				}
				showingArrows = false;
			}
			else
			{
				ArrowParent.SetActive(true);
				if (tooltip != null)
				{
					tooltip.enabled = true;
				}
				showingArrows = true;
			}
		}
	}

	private void OnScaleChanged(float perc)
	{
		sliderHandle.position = new Vector3(sliderWidth * perc + sliderStartX, sliderHandle.position.y, sliderHandle.position.z);
		float x = sliderHandle.position.x;
		lastSliderPosX = x;
		SetText();
	}

	public void SetPercentage(float pct)
	{
		if (autoModeActive && SliderActive)
		{
			sliderHandle.position = new Vector3(pct / 2f * sliderWidth + sliderStartX, sliderHandle.position.y, sliderHandle.position.z);
			timeSlider.SetPercentage(pct / 2f);
			timeSlider.SendTimeScale(false);
		}
	}

	private void CalculatePercentage()
	{
		float x = sliderHandle.position.x;
		if (lastSliderPosX != x)
		{
			timeSlider.SetPercentage((x - sliderStartX) / sliderWidth);
			lastSliderPosX = x;
		}
	}

	public void SetText()
	{
		float delegateTimeScale = timeSlider.delegateTimeScale;
		if (lastTextNumber != delegateTimeScale)
		{
			string text = (delegateTimeScale * 100f).ToString("f0") + "%";
			if ((bool)TextMeshy)
			{
				TextMeshy.text = text;
			}
			if (textNew != null)
			{
				ReferenceMaster.SetDynamicText(textNew, text);
			}
			lastTextNumber = delegateTimeScale;
		}
	}

	public void PauseTime()
	{
		if (!paused)
		{
			Pause();
		}
		else
		{
			UnPause();
		}
		SetText();
	}

	private IEnumerator SmoothSlowTime()
	{
		float cTime = 0f;
		float rate = 1f / smoothPauseSpeed;
		float startSpeed = timeSlider.percentagey;
		while (cTime < 1f)
		{
			cTime += timeSlider.deltaTime * rate;
			timeSlider.SetPercentage(Mathf.Lerp(startSpeed, minSpeedToGo, cTime));
			SetText();
			yield return null;
		}
	}

	private void Pause()
	{
		paused = true;
		prevTime = timeSlider.percentagey;
		timeSlider.SetPercentage(5E-05f);
		pauseTimeButton.Pause();
	}

	private void UnPause()
	{
		paused = false;
		timeSlider.SetPercentage(prevTime);
		pauseTimeButton.UnPause();
	}
}
