using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_EventEditor : MonoBehaviour
{
	public Button m_CollapseButton;

	public Panel_CollapseBar m_CollapseBar;

	public CollapsePanel m_CollapsePanel;

	public PointerEvents m_PointerEvents;

	[Header("Editor")]
	public RectTransform m_RootRectTransform;

	public RectMask2D m_ViewportRectMask;

	public GameObject m_RootCanvas;

	public Vector2 m_StartTimelineAnchor;

	public VerticalLayoutGroup m_TimelineVerticalLayoutGroup;

	public float m_TimelineVerticalSpacing;

	public RectTransform m_RootCanvasRectTransform;

	public RectTransform m_StagesRectTransform;

	public Button m_AddHydraulicsPhaseButton;

	[Header("Scrolling")]
	public ScrollRect m_ScrollRect;

	public Scrollbar m_VerticalScrollbar;

	public Scrollbar m_HorizontalScrollbar;

	[Header("Insert")]
	public EventEditorInsertLine m_InsertLine;

	[Header("Sizes")]
	public float m_MinAnchorHeight;

	public float m_MaxAnchorHeight;

	[Header("Colors")]
	public Color m_InsertLineColor;

	[Header("Sprites")]
	public Sprite m_StarFilled;

	public float m_StarFilledScale;

	public Sprite m_StopSprite;

	public float m_StopSpriteScale;

	public Sprite m_ReverseSprite;

	public float m_ReverseSpriteScale;

	public float m_ZoomMax;

	public float m_ZoomMin;

	public float m_ZoomIncrement;

	public float m_StageSpacingX;

	public float m_StageHeight;

	public bool m_SetEventEditorToDefaultLocation;

	private const float DEFAULT_ZOOM_SCALE = 1f;

	private float m_ZoomScale;

	private Vector3 m_RootCanvasAnchoredPosition;

	private RectTransform m_RectTransform;

	private List<EventTimeline> m_SortedTimelines = new List<EventTimeline>();

	private void Awake()
	{
		m_RootCanvasAnchoredPosition = m_RootCanvas.GetComponent<RectTransform>().anchoredPosition;
		m_RectTransform = GetComponent<RectTransform>();
		m_ZoomScale = 1f;
	}

	private void Start()
	{
		SetDefaultEventEditorLocation();
	}

	private void OnEnable()
	{
		m_AddHydraulicsPhaseButton.onClick.AddListener(OnAddHydraulicsPhase);
		if (m_SetEventEditorToDefaultLocation)
		{
			SetDefaultEventEditorLocation();
			m_SetEventEditorToDefaultLocation = false;
		}
	}

	private void OnDisable()
	{
		m_AddHydraulicsPhaseButton.onClick.RemoveAllListeners();
	}

	private void LateUpdate()
	{
		m_StagesRectTransform.sizeDelta = new Vector2(m_StagesRectTransform.sizeDelta.x, m_RectTransform.anchoredPosition.y);
		if (!m_CollapsePanel.IsMoving())
		{
			if (m_RectTransform.anchoredPosition.y < 20.01f)
			{
				m_CollapseButton.transform.localScale = Vector3.one;
				m_CollapsePanel.m_CollapseState = PanelCollapseState.COLLAPSED;
			}
			else
			{
				m_CollapseButton.transform.localScale = new Vector3(1f, -1f, 1f);
				m_CollapsePanel.m_CollapseState = PanelCollapseState.UNCOLLAPSED;
			}
		}
		if (m_CollapsePanel.m_CollapseState == PanelCollapseState.UNCOLLAPSED && GameUI.m_Instance.m_LevelInfoLite.gameObject.activeInHierarchy)
		{
			GameUI.m_Instance.m_LevelInfoLite.gameObject.SetActive(value: false);
		}
	}

	public void UpdateManual()
	{
		m_ScrollRect.enabled = !EventEditor.IsIconMoving() && !EventEditor.IsStageMoving() && m_CollapsePanel.m_CollapseState == PanelCollapseState.UNCOLLAPSED;
		m_VerticalScrollbar.gameObject.SetActive(m_ScrollRect.enabled);
		m_HorizontalScrollbar.gameObject.SetActive(m_ScrollRect.enabled);
		if (EventTimelines.m_Timelines.Count > 0)
		{
			m_SortedTimelines.Clear();
			SortTimelines(EventTimelines.m_Timelines[0], m_SortedTimelines);
			EventTimelines.m_Timelines.Clear();
			EventTimelines.m_Timelines.AddRange(m_SortedTimelines);
			EventTimelines.UpdateManual();
			EventTimelines.UpdateDividers(GameUI.m_Instance.m_EventEditor.m_RootCanvas.transform);
			OrderTimelinesAndDividers();
			SetTimelineAnchors();
			if (m_CollapsePanel.m_CollapseState != PanelCollapseState.COLLAPSED)
			{
				VehicleRestartPhases.RefreshEventUnitLabels();
			}
		}
		if (!EventEditor.IsIconMoving() && !EventEditor.IsStageMoving())
		{
			UpdateRootAnchor();
			FitRootCanvasToContent();
		}
	}

	private void OrderTimelinesAndDividers()
	{
		if (m_SortedTimelines.Count == 0)
		{
			return;
		}
		int num = 0;
		m_SortedTimelines[0].transform.SetSiblingIndex(num++);
		for (int i = 1; i < m_SortedTimelines.Count; i++)
		{
			if (m_SortedTimelines[i].m_Divider != null)
			{
				m_SortedTimelines[i].m_Divider.transform.SetSiblingIndex(num++);
			}
			m_SortedTimelines[i].transform.SetSiblingIndex(num++);
		}
	}

	public void OnCollapse()
	{
		switch (m_CollapsePanel.m_CollapseState)
		{
		case PanelCollapseState.UNCOLLAPSED:
			InterfaceAudio.Play("ui_menubar_gen_off");
			Collapse();
			break;
		case PanelCollapseState.COLLAPSED:
			InterfaceAudio.Play("ui_menubar_gen_on");
			UnCollapse(Profiles.m_ActiveProfile.m_EventEditorAnchorYNormalized);
			break;
		}
	}

	public void OnResetView()
	{
		InterfaceAudio.Play("ui_settings_reset");
		m_RootCanvas.transform.localScale = Vector3.one;
		m_RootCanvas.GetComponent<RectTransform>().anchoredPosition = m_RootCanvasAnchoredPosition;
		m_ZoomScale = 1f;
	}

	public void OnZoomOut()
	{
		InterfaceAudio.Play("ui_settings_value_scroll");
		float num = (Input.GetKey(KeyCode.LeftAlt) ? (m_ZoomIncrement / 10f) : m_ZoomIncrement);
		Zoom(0f - num);
	}

	public void OnZoomIn()
	{
		InterfaceAudio.Play("ui_settings_value_scroll");
		float increment = (Input.GetKey(KeyCode.LeftAlt) ? (m_ZoomIncrement / 10f) : m_ZoomIncrement);
		Zoom(increment);
	}

	public void Zoom(float increment)
	{
		m_ZoomScale += increment;
		m_ZoomScale = Mathf.Clamp(m_ZoomScale, m_ZoomMin, m_ZoomMax);
		m_RootCanvas.transform.localScale = new Vector3(m_ZoomScale, m_ZoomScale, 1f);
	}

	public void Collapse()
	{
		m_CollapsePanel.m_CollapseScreenAnchorYNormalized = m_RectTransform.anchoredPosition.y / EventEditor.MAX_ANCHOR_Y;
		m_CollapsePanel.Collapse();
	}

	public void UnCollapse(float anchorYNormalized)
	{
		m_ZoomScale = 1f;
		m_RootCanvas.transform.localScale = new Vector3(m_ZoomScale, m_ZoomScale, 1f);
		m_RootCanvas.GetComponent<RectTransform>().anchoredPosition = m_RootCanvasAnchoredPosition;
		m_CollapsePanel.m_CollapseScreenAnchorYNormalized = anchorYNormalized;
		m_CollapsePanel.UnCollapse();
	}

	public void SetDefaultEventEditorLocation()
	{
		Profiles.m_ActiveProfile.m_EventEditorAnchorYNormalized = EventEditor.DEFAULT_ANCHOR_Y / EventEditor.MAX_ANCHOR_Y;
		UnCollapse(EventEditor.DEFAULT_ANCHOR_Y / EventEditor.MAX_ANCHOR_Y);
		m_CollapsePanel.ForceUpdate();
	}

	public void ToggleViewportRectMask()
	{
		m_ViewportRectMask.gameObject.SetActive(value: false);
		m_ViewportRectMask.gameObject.SetActive(value: true);
	}

	private void SortTimelines(EventTimeline timeline, List<EventTimeline> sortedTimelines)
	{
		sortedTimelines.Add(timeline);
		foreach (EventStage stage in timeline.m_Stages)
		{
			foreach (EventUnit unit in stage.m_Units)
			{
				Vehicle vehicle = unit.GetVehicle();
				if (!vehicle)
				{
					continue;
				}
				foreach (Checkpoint checkpoint in vehicle.m_Checkpoints)
				{
					if (checkpoint.m_TriggerTimeline && (bool)checkpoint.m_Timeline)
					{
						SortTimelines(checkpoint.m_Timeline, sortedTimelines);
					}
				}
			}
		}
	}

	private void SetTimelineAnchors()
	{
		float num = m_TimelineVerticalLayoutGroup.padding.top;
		for (int i = 0; i < m_SortedTimelines.Count; i++)
		{
			EventTimeline eventTimeline = EventTimelines.m_Timelines[i];
			eventTimeline.CullEmptyStages(EventEditor.m_PendingStage);
			if (eventTimeline.m_Divider != null)
			{
				num += m_TimelineVerticalLayoutGroup.spacing;
				eventTimeline.m_DividerRectTransform.anchoredPosition = new Vector2(0f, 0f - (num + 4f));
				num += eventTimeline.m_DividerRectTransform.sizeDelta.y;
				num += m_TimelineVerticalLayoutGroup.spacing;
			}
			eventTimeline.m_RectTransform.anchoredPosition = new Vector2(GetTimelineAnchorXOffset(eventTimeline), 0f - num);
			num += eventTimeline.m_RectTransform.sizeDelta.y;
		}
	}

	public float GetTimelineAnchorXOffset(EventTimeline timeline)
	{
		if (!timeline.m_Checkpoint)
		{
			return m_RootCanvasAnchoredPosition.x;
		}
		Vehicle vehicle = Vehicles.FindByGuid(timeline.m_Checkpoint.m_VehicleGuid);
		if (!vehicle)
		{
			return m_RootCanvasAnchoredPosition.x;
		}
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(vehicle.gameObject);
		if (!(stageWithUnit != null))
		{
			return m_RootCanvasAnchoredPosition.x;
		}
		return stageWithUnit.m_ParentTimeline.m_RectTransform.anchoredPosition.x + stageWithUnit.m_RectTransform.anchoredPosition.x - stageWithUnit.m_RectTransform.sizeDelta.x / 2f;
	}

	public EventTimeline GetClosestTimelineToPointer(Vector2 screenPos)
	{
		EventTimeline result = null;
		float num = float.MaxValue;
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			float num2 = Mathf.Abs(Utils.V3toV2(timeline.m_Icon.transform.position).y - screenPos.y);
			if (num2 < num)
			{
				num = num2;
				result = timeline;
			}
		}
		return result;
	}

	private void OnAddHydraulicsPhase()
	{
		if (EventTimelines.CalculateNumStages() >= EventTimelines.MAX_STAGES)
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		if (EventEditor.IsIconMoving())
		{
			InterfaceAudio.PlayErrorBeep();
			return;
		}
		InterfaceAudio.Play("ui_menu_select");
		HydraulicsPhase hydraulicsPhase = HydraulicsPhases.CreatePhase(Vector2.zero, Utils.GenerateUniqueId());
		if ((bool)hydraulicsPhase && EventTimelines.m_Timelines.Count != 0)
		{
			EventTimelines.m_Timelines[0].AddStage().AddUnit(hydraulicsPhase.gameObject, EventUnitType.HYDRAULICS_PHASE);
			HydraulicsController.AddPhase(hydraulicsPhase, Pistons.m_Pistons, BridgeJoints.GetSplitjoints());
			SandboxUndo.SnapShot();
		}
	}

	private void UpdateRootAnchor()
	{
		if (m_CollapsePanel.m_CollapseState == PanelCollapseState.UNCOLLAPSED)
		{
			float value = ComputeTimelinesHeight() + 20f;
			m_RootRectTransform.anchoredPosition = new Vector2(m_RootRectTransform.anchoredPosition.x, Mathf.Clamp(value, m_MinAnchorHeight, m_MaxAnchorHeight));
		}
	}

	private void FitRootCanvasToContent()
	{
		float x = ComputeTimelinesWidth();
		float y = ComputeTimelinesHeight() - 5f;
		m_RootCanvasRectTransform.sizeDelta = new Vector2(x, y);
	}

	private float ComputeTimelinesWidth()
	{
		float num = 0f;
		for (int i = 0; i < m_SortedTimelines.Count; i++)
		{
			EventTimeline eventTimeline = m_SortedTimelines[i];
			float num2 = eventTimeline.m_RectTransform.anchoredPosition.x + eventTimeline.m_RectTransform.sizeDelta.x + 80f;
			if (num2 > num)
			{
				num = num2;
			}
		}
		return num;
	}

	private float ComputeTimelinesHeight()
	{
		float num = 0f;
		num += (float)m_TimelineVerticalLayoutGroup.padding.top;
		for (int i = 0; i < m_SortedTimelines.Count; i++)
		{
			EventTimeline eventTimeline = m_SortedTimelines[i];
			num += eventTimeline.m_RectTransform.sizeDelta.y;
			if (eventTimeline.m_Divider != null)
			{
				num += m_TimelineVerticalLayoutGroup.spacing;
				num += eventTimeline.m_DividerRectTransform.sizeDelta.y;
				num += m_TimelineVerticalLayoutGroup.spacing;
			}
		}
		return num + (float)m_TimelineVerticalLayoutGroup.padding.bottom;
	}
}
