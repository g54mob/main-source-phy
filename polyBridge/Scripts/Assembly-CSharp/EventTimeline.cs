using System;
using System.Collections.Generic;
using Poly.Determinism;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventTimeline : MonoBehaviour
{
	public TextMeshProUGUI m_Header;

	public RectTransform m_RectTransform;

	public RectTransform m_OutlineRectTransform;

	public HorizontalLayoutGroup m_HorizontalLayoutGroup;

	[Header("Checkpoint Trigger")]
	public GameObject m_CheckpointTrigger;

	public Image m_Outline;

	public Image m_ForegroundImage;

	public Image m_HeaderImage;

	public Image m_Icon;

	[Header("Colors")]
	public Color m_OutlineColor;

	public Color m_OutlineDisabledColor;

	public Color m_OutlineHightlightColor;

	public Color m_OutlineErrorColor;

	[NonSerialized]
	public Checkpoint m_Checkpoint;

	[NonSerialized]
	public Checkpoint m_SourceCheckpoint;

	[NonSerialized]
	public List<EventStage> m_Stages = new List<EventStage>();

	[NonSerialized]
	public EventStage m_ActiveStage;

	[NonSerialized]
	public GameObject m_Divider;

	[NonSerialized]
	public RectTransform m_DividerRectTransform;

	[NonSerialized]
	public bool m_Started;

	[NonSerialized]
	public bool m_Complete;

	private EventEditorColorMode m_ColorMode;

	private void Awake()
	{
		m_ColorMode = EventEditorColorMode.BLUEPRINT;
	}

	private void OnDestroy()
	{
		if (EventTimelines.m_Timelines.Contains(this))
		{
			EventTimelines.m_Timelines.Remove(this);
		}
		DestroyManual();
	}

	public void Clear()
	{
		foreach (EventStage stage in m_Stages)
		{
			UnityEngine.Object.Destroy(stage.gameObject);
			stage.Clear();
		}
		m_Stages.Clear();
		m_Started = false;
		m_Complete = false;
		m_ActiveStage = null;
	}

	public void DestroyManual()
	{
		if ((bool)m_Divider)
		{
			UnityEngine.Object.Destroy(m_Divider);
			m_Divider = null;
		}
	}

	public void Restore()
	{
		foreach (EventStage stage in m_Stages)
		{
			stage.Restore();
		}
		m_Started = false;
		m_Complete = false;
		m_ActiveStage = null;
	}

	public void CopyFrom(EventTimeline source)
	{
		Clear();
		if ((bool)source.m_Checkpoint && (bool)source.m_Checkpoint.m_Timeline && source.m_Checkpoint.m_TriggerTimeline)
		{
			m_Checkpoint = Checkpoints.CreateCheckpoint(Prefabs.m_Instance.m_CheckpointStar, source.m_Checkpoint.m_Color, Vector3.zero, Quaternion.identity, Utils.GenerateUniqueId());
			m_Checkpoint.m_Timeline = this;
			m_Checkpoint.m_TriggerTimeline = true;
			m_Checkpoint.m_VehicleGuid = source.m_Checkpoint.m_VehicleGuid;
			m_Checkpoint.m_StopVehicle = source.m_Checkpoint.m_StopVehicle;
			m_Checkpoint.m_ReverseVehicleOnRestart = source.m_Checkpoint.m_ReverseVehicleOnRestart;
			m_Checkpoint.gameObject.SetActive(value: false);
			m_Checkpoint.m_SandboxItem.m_Label.m_Text.text = source.m_Checkpoint.m_SandboxItem.m_Label.m_Text.text;
			m_SourceCheckpoint = source.m_Checkpoint;
			SetColorMode(EventEditorColorMode.REGULAR);
			SetCheckpointSprite();
			m_Checkpoint.InstantiatePickupFX();
			m_Checkpoint.EnterGameState(GameStateManager.GetState());
		}
		foreach (EventStage stage in source.m_Stages)
		{
			EventStage eventStage = AddStage();
			eventStage.m_Header.text = stage.m_Header.text;
			eventStage.m_ParentTimeline = this;
			eventStage.SetColorMode(EventEditorColorMode.REGULAR);
			foreach (EventUnit unit in stage.m_Units)
			{
				if (!(unit == null) && !(unit.m_SourceObject == null))
				{
					EventUnit eventUnit = eventStage.AddUnit(unit.m_SourceObject, unit.m_Type);
					if (unit.m_Type == EventUnitType.VEHICLE_RESTART_PHASE)
					{
						eventUnit.SetText(unit.m_Text.text);
					}
				}
			}
		}
	}

	public void SetColorMode(EventEditorColorMode colorMode)
	{
		m_ColorMode = colorMode;
		m_Outline.color = ((colorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageBackgroundColor_Blueprint : GameUI.m_Instance.m_EventStageBackgroundColor);
		m_ForegroundImage.color = ((colorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageForegroundColor_Blueprint : GameUI.m_Instance.m_EventStageForegroundColor);
		m_HeaderImage.color = ((colorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageHeaderColor_Blueprint : GameUI.m_Instance.m_EventStageHeaderColor);
	}

	public EventStage AddStage()
	{
		EventStage eventStage = CreateStage();
		m_Stages.Add(eventStage);
		eventStage.name = $"Stage {m_Stages.IndexOf(eventStage)}";
		return eventStage;
	}

	public EventStage InsertStage(int index)
	{
		EventStage eventStage = CreateStage();
		m_Stages.Insert(index, eventStage);
		eventStage.name = $"Stage {m_Stages.IndexOf(eventStage)}";
		return eventStage;
	}

	public void SyncHierarchy()
	{
		for (int i = 0; i < m_Stages.Count; i++)
		{
			m_Stages[i].transform.SetSiblingIndex(i);
		}
	}

	public void StartSimulation()
	{
		m_Started = true;
		m_Complete = false;
		DeterminismLog.LogEvent(null, Poly.Determinism.EventType.EventTimelineStartSimulation);
		StartStageSimulation((m_Stages.Count == 0) ? null : m_Stages[0]);
	}

	public void ForceUpdate()
	{
		foreach (EventStage stage in m_Stages)
		{
			stage.ResizeForIcons();
		}
		UpdateManual();
	}

	public void UpdateManual()
	{
		CullEmptyStages(EventEditor.m_PendingStage);
		UpdateUI();
		foreach (EventStage stage in m_Stages)
		{
			stage.UpdateManual();
		}
		UpdateSize();
	}

	public void UpdateSize()
	{
		LayoutRebuilder.ForceRebuildLayoutImmediate(m_HorizontalLayoutGroup.GetComponent<RectTransform>());
		float x = Mathf.Max(m_HorizontalLayoutGroup.GetComponent<RectTransform>().sizeDelta.x, m_CheckpointTrigger.GetComponent<RectTransform>().sizeDelta.x);
		float y = Mathf.Max(m_HorizontalLayoutGroup.GetComponent<RectTransform>().sizeDelta.y, m_CheckpointTrigger.GetComponent<RectTransform>().sizeDelta.y);
		m_RectTransform.sizeDelta = new Vector2(x, y);
		if (EventTimelines.m_Timelines.IndexOf(this) == 0)
		{
			m_HorizontalLayoutGroup.padding.left = 10;
		}
	}

	public void FixedUpdate_Manual()
	{
		if (!m_Started || m_Complete || !(m_ActiveStage != null))
		{
			return;
		}
		m_ActiveStage.FixedUpdate_Manual();
		if (m_ActiveStage.IsComplete())
		{
			int num = m_Stages.IndexOf(m_ActiveStage);
			if (num == m_Stages.Count - 1)
			{
				m_Complete = true;
				m_ActiveStage = null;
			}
			else
			{
				StartStageSimulation(m_Stages[num + 1]);
			}
		}
	}

	public void CullEmptyStages(EventStage ignore)
	{
		for (int num = m_Stages.Count - 1; num >= 0; num--)
		{
			EventStage eventStage = m_Stages[num];
			if (eventStage != ignore && eventStage.m_Units.Count == 0)
			{
				m_Stages.Remove(eventStage);
				UnityEngine.Object.Destroy(eventStage.gameObject);
			}
		}
	}

	public void SetCheckpointSprite()
	{
		if ((bool)m_Checkpoint)
		{
			m_CheckpointTrigger.SetActive(value: true);
			m_Outline.gameObject.SetActive(value: true);
			m_Icon.gameObject.SetActive(value: true);
			if (m_Checkpoint.m_StopVehicle && m_Checkpoint.m_ReverseVehicleOnRestart)
			{
				m_Icon.sprite = GameUI.m_Instance.m_EventEditor.m_ReverseSprite;
				m_Icon.transform.localScale = new Vector3(GameUI.m_Instance.m_EventEditor.m_ReverseSpriteScale, GameUI.m_Instance.m_EventEditor.m_ReverseSpriteScale, 1f);
			}
			else if (m_Checkpoint.m_StopVehicle)
			{
				m_Icon.sprite = GameUI.m_Instance.m_EventEditor.m_StopSprite;
				m_Icon.transform.localScale = new Vector3(GameUI.m_Instance.m_EventEditor.m_StopSpriteScale, GameUI.m_Instance.m_EventEditor.m_StopSpriteScale, 1f);
			}
			else
			{
				m_Icon.sprite = GameUI.m_Instance.m_EventEditor.m_StarFilled;
				m_Icon.transform.localScale = new Vector3(GameUI.m_Instance.m_EventEditor.m_StarFilledScale, GameUI.m_Instance.m_EventEditor.m_StarFilledScale, 1f);
			}
			m_Icon.color = m_Checkpoint.m_Color;
		}
		else
		{
			m_CheckpointTrigger.SetActive(value: false);
		}
	}

	public void MoveStagesToStartTimeline()
	{
		if (EventTimelines.m_Timelines.Count <= 0)
		{
			return;
		}
		EventTimeline eventTimeline = EventTimelines.m_Timelines[0];
		foreach (EventStage stage in m_Stages)
		{
			eventTimeline.m_Stages.Add(stage);
			stage.AssignParentTimeline(eventTimeline);
		}
		m_Stages.Clear();
	}

	public bool HasChild(EventTimeline child)
	{
		foreach (EventStage stage in m_Stages)
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
					if (checkpoint.m_Timeline == child)
					{
						return true;
					}
					if (checkpoint.m_Timeline != null)
					{
						return checkpoint.m_Timeline.HasChild(child);
					}
				}
			}
		}
		return false;
	}

	public void AddDivider(GameObject prefab, Transform parent)
	{
		if (!m_Divider)
		{
			m_Divider = UnityEngine.Object.Instantiate(prefab, parent);
			if ((bool)m_Divider)
			{
				m_DividerRectTransform = m_Divider.GetComponent<RectTransform>();
			}
		}
	}

	public bool ContainsHydraulicPhase()
	{
		foreach (EventStage stage in m_Stages)
		{
			if (stage.ContainsHydraulicsPhase())
			{
				return true;
			}
		}
		return false;
	}

	private void UpdateUI()
	{
		Color color = ((m_ColorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageBackgroundColor_Blueprint : GameUI.m_Instance.m_EventStageBackgroundColor);
		m_Outline.color = (((bool)m_Checkpoint && EventEditor.SelectedUnitIsVehicleWithCheckpoint(m_Checkpoint)) ? m_OutlineHightlightColor : color);
		if ((bool)m_Checkpoint)
		{
			m_Header.text = (m_SourceCheckpoint ? m_SourceCheckpoint.m_SandboxItem.m_Label.m_Text.text : m_Checkpoint.m_SandboxItem.m_Label.m_Text.text);
		}
	}

	private void StartStageSimulation(EventStage stage)
	{
		m_ActiveStage = stage;
		if (m_ActiveStage != null)
		{
			m_ActiveStage.StartSimulation();
		}
	}

	private EventStage CreateStage()
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_EventStage, base.transform.position, Quaternion.identity);
		if (!gameObject)
		{
			Debug.LogWarningFormat("Failed to instantiate stage");
			return null;
		}
		EventStage component = gameObject.GetComponent<EventStage>();
		component.AssignParentTimeline(this);
		return component;
	}
}
