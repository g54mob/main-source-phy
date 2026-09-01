using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class EventStage : MonoBehaviour
{
	public RectTransform m_RectTransform;

	public RectTransform m_IconsParent;

	public GridLayoutGroup m_GridLayoutGroup;

	public TextMeshProUGUI m_Header;

	public Image m_Outline;

	public int m_StageWidth;

	public int m_StageHeight;

	public int m_IconWidth;

	public int m_IconHeight;

	[Header("Colorized Images")]
	public Image m_BackgroundImage;

	public Image m_ForegroundImage;

	public Image m_HeaderImage;

	[NonSerialized]
	public List<EventUnit> m_Units = new List<EventUnit>();

	[NonSerialized]
	public EventTimeline m_ParentTimeline;

	[NonSerialized]
	public bool m_Started;

	[NonSerialized]
	public bool m_SkipHungCheck;

	[NonSerialized]
	public int m_AbsoluteStageIndex;

	[NonSerialized]
	public Vector3 m_OffsetFromPointer;

	[NonSerialized]
	public Vector2 m_StartMovementPos;

	private EventEditorColorMode m_ColorMode;

	private string[] m_StageLabelLookupTable = new string[26]
	{
		"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
		"K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
		"U", "V", "W", "X", "Y", "Z"
	};

	private void Awake()
	{
		m_ColorMode = EventEditorColorMode.BLUEPRINT;
	}

	private void Update()
	{
		ResizeForIcons();
	}

	public void SetColorMode(EventEditorColorMode colorMode)
	{
		m_ColorMode = colorMode;
		m_BackgroundImage.color = ((colorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageBackgroundColor_Blueprint : GameUI.m_Instance.m_EventStageBackgroundColor);
		m_ForegroundImage.color = ((colorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageForegroundColor_Blueprint : GameUI.m_Instance.m_EventStageForegroundColor);
		m_HeaderImage.color = ((colorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageHeaderColor_Blueprint : GameUI.m_Instance.m_EventStageHeaderColor);
	}

	public void MakeMaskable(bool maskable)
	{
		m_Header.maskable = maskable;
		Image[] componentsInChildren = base.gameObject.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].maskable = maskable;
		}
	}

	public void MakeRaycastTarget(bool raycastTarget)
	{
		m_Header.raycastTarget = raycastTarget;
		Image[] componentsInChildren = base.gameObject.GetComponentsInChildren<Image>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].raycastTarget = raycastTarget;
		}
	}

	public void ResizeForIcons()
	{
		int num = 0;
		for (int i = 0; i < m_IconsParent.childCount; i++)
		{
			if (m_IconsParent.GetChild(i).gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		int numColumns = GetNumColumns(num);
		float x = (float)m_StageWidth + (float)(numColumns - 1) * ((float)m_IconWidth + m_GridLayoutGroup.spacing.x);
		float y = ((num > 1) ? ((float)(m_IconHeight * 2 + m_GridLayoutGroup.padding.top) + m_GridLayoutGroup.spacing.y + (float)m_GridLayoutGroup.padding.bottom) : ((float)m_StageHeight));
		m_RectTransform.sizeDelta = new Vector2(x, y);
	}

	private void OnDestroy()
	{
		if ((bool)m_ParentTimeline && m_ParentTimeline.m_Stages.Contains(this))
		{
			m_ParentTimeline.m_Stages.Remove(this);
		}
		if (VehicleFollow.m_PreferredVehicleInStage.ContainsKey(this))
		{
			VehicleFollow.m_PreferredVehicleInStage.Remove(this);
		}
	}

	public void MoveUnit(EventUnit unit, EventStage destStage)
	{
		m_Units.Remove(unit);
		destStage.m_Units.Add(unit);
		Vector3 localScale = unit.transform.localScale;
		unit.transform.SetParent(destStage.m_IconsParent.transform);
		unit.transform.localScale = localScale;
		unit.m_ParentStage = destStage;
		unit.transform.localPosition = Vector3.zero;
	}

	public EventUnit AddUnit(GameObject gameObject, EventUnitType type)
	{
		EventUnit eventUnit = CreateUnit();
		m_Units.Add(eventUnit);
		eventUnit.name = $"Unit {m_Units.IndexOf(eventUnit)}";
		eventUnit.m_SourceObject = gameObject;
		eventUnit.m_Type = type;
		eventUnit.transform.localPosition = Vector3.zero;
		eventUnit.SetSprite(gameObject);
		LayoutRebuilder.MarkLayoutForRebuild(m_ParentTimeline.m_HorizontalLayoutGroup.GetComponent<RectTransform>());
		return eventUnit;
	}

	public EventUnit CreateUnit()
	{
		GameObject obj = UnityEngine.Object.Instantiate(Prefabs.m_Instance.m_EventIcon);
		Vector3 localScale = obj.transform.localScale;
		obj.transform.SetParent(m_IconsParent.transform);
		obj.transform.localScale = localScale;
		EventUnit component = obj.GetComponent<EventUnit>();
		component.m_ParentStage = this;
		component.m_IconBackground.sprite = ((m_ColorMode == EventEditorColorMode.BLUEPRINT) ? GameUI.m_Instance.m_EventStageIconBackgroundSprite_Blueprint : GameUI.m_Instance.m_EventStageIconBackgroundSprite);
		return component;
	}

	public void StartSimulation()
	{
		m_Started = true;
		m_SkipHungCheck = false;
	}

	public void UpdateManual()
	{
		HightlightOff();
		UpdateLabel();
		UpdateIcons();
	}

	public void AssignParentTimeline(EventTimeline timeline)
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.SetParent(timeline.m_HorizontalLayoutGroup.transform);
		base.transform.localScale = localScale;
		m_ParentTimeline = timeline;
	}

	public void UpdateLabel()
	{
		m_Header.text = GetStageLabel();
		m_Header.color = Color.white;
	}

	public void Pulse()
	{
		PulseIcons.Pulse(m_Outline, 1f, 1.2f, GetDefaultOutlineColor(), Color.yellow);
	}

	public string GetStageLabel()
	{
		if (EventEditor.m_PendingStage == this)
		{
			return string.Empty;
		}
		return FormatStageLabel(m_AbsoluteStageIndex);
	}

	private string FormatStageLabel(int stageNumber)
	{
		int num = stageNumber / 26;
		stageNumber %= 26;
		if (num == 0)
		{
			return m_StageLabelLookupTable[stageNumber];
		}
		return m_StageLabelLookupTable[num - 1] + m_StageLabelLookupTable[stageNumber];
	}

	private void UpdateIcons()
	{
		foreach (EventUnit unit in m_Units)
		{
			if (unit.m_SourceObject != null)
			{
				SandboxItem component = unit.m_SourceObject.GetComponent<SandboxItem>();
				if (component != null && component.m_Type == SandboxItemType.VEHICLE)
				{
					unit.m_Icon.sprite = unit.m_SourceObject.GetComponent<SandboxItem>().GetSpriteForEventViewer();
				}
			}
		}
	}

	public void FixedUpdate_Manual()
	{
		if (!m_Started)
		{
			return;
		}
		foreach (EventUnit unit in m_Units)
		{
			unit.FixedUpdate_Manual();
		}
	}

	public bool IsComplete()
	{
		foreach (EventUnit unit in m_Units)
		{
			if (!unit.IsComplete())
			{
				return false;
			}
		}
		return true;
	}

	public bool IsHung()
	{
		if (m_SkipHungCheck)
		{
			return false;
		}
		foreach (EventUnit unit in m_Units)
		{
			if (unit.GetHungVehicle() != null)
			{
				return true;
			}
		}
		return false;
	}

	public Vehicle GetHungVehicle()
	{
		foreach (EventUnit unit in m_Units)
		{
			Vehicle hungVehicle = unit.GetHungVehicle();
			if (hungVehicle != null)
			{
				return hungVehicle;
			}
		}
		return null;
	}

	public void CullEmptyUnits()
	{
		for (int num = m_Units.Count - 1; num >= 0; num--)
		{
			if (m_Units[num].IsEmpty())
			{
				m_Units.RemoveAt(num);
			}
		}
	}

	public bool IsEmpty()
	{
		return m_Units.Count == 0;
	}

	public int GetNumUnitsWithLabel()
	{
		int num = 0;
		foreach (EventUnit unit in m_Units)
		{
			if ((bool)unit.GetVehicle() || (bool)unit.GetZedAxisVehicle())
			{
				num++;
			}
		}
		return num;
	}

	public bool ContainsHydraulicsPhase()
	{
		foreach (EventUnit unit in m_Units)
		{
			if ((bool)unit.GetHydraulicsPhase())
			{
				return true;
			}
		}
		return false;
	}

	public void HightlightOn(Color color)
	{
		m_Outline.color = color;
	}

	public void HightlightOff()
	{
		m_Outline.color = GetDefaultOutlineColor();
	}

	public Color GetDefaultOutlineColor()
	{
		if (m_ColorMode != EventEditorColorMode.BLUEPRINT)
		{
			return GameUI.m_Instance.m_EventStageBackgroundColor;
		}
		return GameUI.m_Instance.m_EventStageBackgroundColor_Blueprint;
	}

	public void Clear()
	{
		foreach (EventUnit unit in m_Units)
		{
			UnityEngine.Object.Destroy(unit.gameObject);
		}
		m_Units.Clear();
	}

	public void Restore()
	{
		m_Started = false;
		foreach (EventUnit unit in m_Units)
		{
			unit.Restore();
		}
	}

	public void DestroyUnit(EventUnit unit)
	{
		if (m_Units.Contains(unit))
		{
			UnityEngine.Object.Destroy(unit.gameObject);
			m_Units.Remove(unit);
		}
	}

	public void ClearAndDestroySourceObjects()
	{
		foreach (EventUnit unit in m_Units)
		{
			if ((bool)unit.m_SourceObject)
			{
				Vehicle component = unit.m_SourceObject.GetComponent<Vehicle>();
				if ((bool)component)
				{
					VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.FindTriggerThatStopsVehicle(component.m_Guid);
					if ((bool)vehicleStopTrigger)
					{
						UnityEngine.Object.Destroy(vehicleStopTrigger.gameObject);
					}
				}
				UnityEngine.Object.Destroy(unit.m_SourceObject);
				unit.m_SourceObject = null;
			}
			UnityEngine.Object.Destroy(unit.gameObject);
		}
		m_Units.Clear();
	}

	private int GetNumColumns(int iconCount)
	{
		return 1 + Mathf.FloorToInt(iconCount - 1) / 2;
	}
}
