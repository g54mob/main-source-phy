using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Stages : MonoBehaviour
{
	[Header("Panel")]
	public int m_MinPanelWidth;

	public int m_MaxPanelWidth;

	public int m_MinPanelHeight;

	public int m_MaxPanelHeight;

	public RectTransform m_ContentRectTransform;

	public VerticalLayoutGroup m_TimelineVerticalLayoutGroup;

	[NonSerialized]
	public Action m_OnEnableCallback;

	private List<EventTimeline> m_CopiedTimelines = new List<EventTimeline>();

	private Vector3 m_RootCanvasAnchoredPosition;

	private bool m_ForceRebuildLayout;

	private void Awake()
	{
		m_RootCanvasAnchoredPosition = m_ContentRectTransform.anchoredPosition;
	}

	private void Update()
	{
		foreach (EventTimeline copiedTimeline in m_CopiedTimelines)
		{
			copiedTimeline.UpdateManual();
		}
		if (GameStateManager.GetState() == GameState.SIM && Bridge.IsSimulating())
		{
			HighlightActiveStagesInCopiedTimelines();
		}
		OrderTimelinesAndDividers();
		if (m_ForceRebuildLayout)
		{
			ForceRebuildLayout();
			m_ForceRebuildLayout = false;
		}
		SetTimelineAnchors();
		FitRootCanvasToContent();
	}

	private void OnEnable()
	{
		if (EventTimelines.m_Timelines.Count == 0)
		{
			return;
		}
		GameUI.m_Instance.m_EventEditor.UpdateManual();
		VehicleRestartPhases.RefreshEventUnitLabels();
		m_CopiedTimelines.Clear();
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_EventTimeline, m_TimelineVerticalLayoutGroup.transform);
			if ((bool)gameObject)
			{
				EventTimeline component = gameObject.GetComponent<EventTimeline>();
				component.CopyFrom(timeline);
				component.UpdateManual();
				if ((bool)timeline.m_Divider)
				{
					component.AddDivider(Prefabs.m_Instance.m_TimelineDividerGrey, m_ContentRectTransform.transform);
				}
				m_CopiedTimelines.Add(component);
			}
		}
		int num = 0;
		foreach (EventTimeline copiedTimeline in m_CopiedTimelines)
		{
			foreach (EventStage stage in copiedTimeline.m_Stages)
			{
				stage.m_AbsoluteStageIndex = num++;
				stage.UpdateManual();
			}
		}
		m_CopiedTimelines[0].m_Icon.gameObject.SetActive(value: false);
		m_CopiedTimelines[0].m_Outline.gameObject.SetActive(value: false);
		m_CopiedTimelines[0].m_HorizontalLayoutGroup.padding.left = 10;
		m_ForceRebuildLayout = true;
		Update();
		m_OnEnableCallback?.Invoke();
	}

	private void OnDisable()
	{
		DestroyCopiedTimelines();
	}

	public void ForceRebuildLayout()
	{
		foreach (EventTimeline copiedTimeline in m_CopiedTimelines)
		{
			copiedTimeline.ForceUpdate();
		}
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_TimelineVerticalLayoutGroup.GetComponent<RectTransform>());
	}

	public void DestroyCopiedTimelines()
	{
		foreach (EventTimeline copiedTimeline in m_CopiedTimelines)
		{
			if ((bool)copiedTimeline.m_Checkpoint)
			{
				copiedTimeline.m_Checkpoint.m_Timeline = null;
				Checkpoints.DestroyCheckpoint(copiedTimeline.m_Checkpoint);
			}
			UnityEngine.Object.Destroy(copiedTimeline.gameObject);
		}
		m_CopiedTimelines.Clear();
	}

	public float ComputeTimelinesWidth()
	{
		float num = 0f;
		for (int i = 0; i < m_CopiedTimelines.Count; i++)
		{
			EventTimeline eventTimeline = m_CopiedTimelines[i];
			float num2 = eventTimeline.m_RectTransform.anchoredPosition.x + eventTimeline.m_RectTransform.sizeDelta.x + 20f;
			if (num2 > num)
			{
				num = num2;
			}
		}
		return num;
	}

	public EventUnit GetFirstHydraulicsPhase()
	{
		foreach (EventTimeline copiedTimeline in m_CopiedTimelines)
		{
			foreach (EventStage stage in copiedTimeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if ((bool)unit.GetHydraulicsPhase())
					{
						return unit;
					}
				}
			}
		}
		return null;
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

	private void OrderTimelinesAndDividers()
	{
		if (m_CopiedTimelines.Count == 0)
		{
			return;
		}
		int num = 0;
		m_CopiedTimelines[0].transform.SetSiblingIndex(num++);
		for (int i = 1; i < m_CopiedTimelines.Count; i++)
		{
			if (m_CopiedTimelines[i].m_Divider != null)
			{
				m_CopiedTimelines[i].m_Divider.transform.SetSiblingIndex(num++);
			}
			m_CopiedTimelines[i].transform.SetSiblingIndex(num++);
		}
	}

	private void SetTimelineAnchors()
	{
		float num = m_TimelineVerticalLayoutGroup.padding.top;
		for (int i = 0; i < m_CopiedTimelines.Count; i++)
		{
			EventTimeline eventTimeline = m_CopiedTimelines[i];
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
		EventStage stageWithUnit = GetStageWithUnit(vehicle.gameObject);
		if (!(stageWithUnit != null))
		{
			return m_ContentRectTransform.anchoredPosition.x;
		}
		return stageWithUnit.m_ParentTimeline.m_RectTransform.anchoredPosition.x + stageWithUnit.m_RectTransform.anchoredPosition.x - stageWithUnit.m_RectTransform.sizeDelta.x / 2f;
	}

	public void EnableOffIconForStage(EventStage targetStage, bool enable)
	{
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				if (!(stage == targetStage))
				{
					continue;
				}
				int num = EventTimelines.m_Timelines.IndexOf(timeline);
				if (num < 0 || num >= m_CopiedTimelines.Count)
				{
					continue;
				}
				EventTimeline eventTimeline = m_CopiedTimelines[EventTimelines.m_Timelines.IndexOf(timeline)];
				int num2 = timeline.m_Stages.IndexOf(stage);
				if (num2 < 0 || num2 >= eventTimeline.m_Stages.Count)
				{
					continue;
				}
				foreach (EventUnit unit in eventTimeline.m_Stages[timeline.m_Stages.IndexOf(stage)].m_Units)
				{
					if (unit.m_Type == EventUnitType.HYDRAULICS_PHASE)
					{
						unit.m_Off.gameObject.SetActive(enable);
					}
				}
			}
		}
	}

	private void HighlightActiveStagesInCopiedTimelines()
	{
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				int num = EventTimelines.m_Timelines.IndexOf(timeline);
				if (num >= m_CopiedTimelines.Count)
				{
					continue;
				}
				EventTimeline eventTimeline = m_CopiedTimelines[num];
				int num2 = timeline.m_Stages.IndexOf(stage);
				if (num2 < eventTimeline.m_Stages.Count)
				{
					EventStage eventStage = eventTimeline.m_Stages[num2];
					if (timeline.m_Started && timeline.m_ActiveStage == stage)
					{
						eventStage.HightlightOn(GameUI.m_Instance.m_GoldColor);
					}
					else
					{
						eventStage.HightlightOff();
					}
				}
			}
		}
	}

	public EventStage GetStageWithUnit(GameObject gameObject)
	{
		foreach (EventTimeline copiedTimeline in m_CopiedTimelines)
		{
			foreach (EventStage stage in copiedTimeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.m_SourceObject == gameObject)
					{
						return stage;
					}
				}
			}
		}
		return null;
	}

	public void FitRootCanvasToContent()
	{
		float x = ComputeTimelinesWidth();
		float y = ComputeTimelinesHeight() - 10f;
		m_ContentRectTransform.sizeDelta = new Vector2(x, y);
	}

	public float ComputeTimelinesHeight()
	{
		float num = 0f;
		num += (float)m_TimelineVerticalLayoutGroup.padding.top;
		for (int i = 0; i < m_CopiedTimelines.Count; i++)
		{
			EventTimeline eventTimeline = m_CopiedTimelines[i];
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
