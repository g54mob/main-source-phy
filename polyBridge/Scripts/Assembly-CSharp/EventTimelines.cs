using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventTimelines
{
	public static List<EventTimeline> m_Timelines = new List<EventTimeline>();

	public static int MAX_STAGES = 702;

	public static void UpdateManual()
	{
		CullEmptyUnits();
		foreach (EventTimeline timeline in m_Timelines)
		{
			timeline.UpdateManual();
		}
		SetAbsoluteIndexForAllStages();
	}

	public static void UpdateDividers(Transform parent)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
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
							checkpoint.m_Timeline.AddDivider(Prefabs.m_Instance.m_TimelineDivider, parent);
						}
					}
				}
			}
		}
	}

	public static void FixedUpdate_Manual()
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			timeline.FixedUpdate_Manual();
		}
	}

	public static void Clear()
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			timeline.Clear();
			timeline.DestroyManual();
			Object.Destroy(timeline.gameObject);
		}
		m_Timelines.Clear();
	}

	public static void Restore()
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			timeline.Restore();
		}
	}

	public static EventTimeline CreateTimeline()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_EventTimeline, GameUI.m_Instance.m_EventEditor.m_RootCanvas.transform);
		if (!gameObject)
		{
			return null;
		}
		EventTimeline component = gameObject.GetComponent<EventTimeline>();
		if ((bool)component)
		{
			component.m_Header.text = string.Empty;
			component.m_Icon.gameObject.SetActive(value: false);
			component.m_Outline.gameObject.SetActive(value: false);
			m_Timelines.Add(component);
		}
		return component;
	}

	public static void DestroyCheckpointTimeline(Checkpoint checkpoint)
	{
		if ((bool)checkpoint.m_Timeline)
		{
			checkpoint.m_Timeline.MoveStagesToStartTimeline();
			Object.Destroy(checkpoint.m_Timeline.gameObject);
			if (m_Timelines.Contains(checkpoint.m_Timeline))
			{
				m_Timelines.Remove(checkpoint.m_Timeline);
			}
			checkpoint.m_Timeline = null;
		}
	}

	public static void StartSimulation()
	{
		if (m_Timelines.Count > 0)
		{
			m_Timelines[0].StartSimulation();
		}
	}

	public static void CullEmptyUnits()
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				stage.CullEmptyUnits();
			}
		}
	}

	public static void RemoveStage(EventStage stageToRemove)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				if (stage == stageToRemove)
				{
					timeline.m_Stages.Remove(stage);
					return;
				}
			}
		}
	}

	public static EventStage GetStageWithUnit(GameObject gameObject)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
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

	public static EventUnit GetUnitMatchingGameObject(GameObject gameObject)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.m_SourceObject == gameObject)
					{
						return unit;
					}
				}
			}
		}
		return null;
	}

	public static EventUnit GetUnitMatchingVehicleRestartPhase(VehicleRestartPhase phase)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.GetVehicleRestartPhase() == phase)
					{
						return unit;
					}
				}
			}
		}
		return null;
	}

	public static string GetStageLabelForUnit(GameObject gameObject)
	{
		EventStage stageWithUnit = GetStageWithUnit(gameObject);
		if (!stageWithUnit || stageWithUnit.GetNumUnitsWithLabel() == 0)
		{
			return string.Empty;
		}
		if (stageWithUnit.GetNumUnitsWithLabel() == 1)
		{
			return stageWithUnit.m_Header.text;
		}
		int num = 0;
		foreach (EventUnit unit in stageWithUnit.m_Units)
		{
			num++;
			if (unit.m_SourceObject == gameObject)
			{
				return $"{stageWithUnit.m_Header.text}{num}";
			}
		}
		return string.Empty;
	}

	public static void UpdateGameObjectReferences(GameObject oldGameObject, GameObject newGameObject)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.m_SourceObject == oldGameObject)
					{
						unit.m_SourceObject = newGameObject;
						unit.SetSprite(newGameObject);
					}
					VehicleRestartPhase component = unit.m_SourceObject.GetComponent<VehicleRestartPhase>();
					Vehicle component2 = oldGameObject.GetComponent<Vehicle>();
					Vehicle component3 = newGameObject.GetComponent<Vehicle>();
					if (component != null && component2 != null && component3 != null && component.m_VehicleGuid == component2.m_Guid)
					{
						component.m_VehicleGuid = component3.m_Guid;
					}
				}
			}
		}
	}

	public static void UpdateForVehicleSkinChange(Vehicle vehicle)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			timeline.SetCheckpointSprite();
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.m_Type == EventUnitType.VEHICLE_RESTART_PHASE && unit.m_SourceObject != null && unit.m_SourceObject.GetComponent<VehicleRestartPhase>().m_VehicleGuid == vehicle.m_Guid)
					{
						unit.AdjustIconForVehicleRestart();
					}
				}
			}
		}
	}

	public static bool ContainsHydraulicsPhase()
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			if (timeline.ContainsHydraulicPhase())
			{
				return true;
			}
		}
		return false;
	}

	public static Vector2 GetTotalUnitDimensions()
	{
		int num = 0;
		int num2 = 1;
		int num3 = 0;
		foreach (EventTimeline timeline in m_Timelines)
		{
			int num4 = timeline.m_Stages.Count;
			if ((bool)timeline.m_Checkpoint)
			{
				num4++;
				Vehicle vehicle = Vehicles.FindByGuid(timeline.m_Checkpoint.m_VehicleGuid);
				if ((bool)vehicle)
				{
					num4 += GetIndexOfUnitSourceObject(vehicle.gameObject);
				}
			}
			num2 = 1;
			num = Mathf.Max(num, num4);
			foreach (EventStage stage in timeline.m_Stages)
			{
				num2 = Mathf.Max(num2, stage.m_Units.Count);
			}
			num3 += num2;
		}
		return new Vector2(num, num3);
	}

	public static List<EventTimelineProxy> Serialize()
	{
		List<EventTimelineProxy> list = new List<EventTimelineProxy>();
		foreach (EventTimeline timeline in m_Timelines)
		{
			EventTimelineProxy eventTimelineProxy = new EventTimelineProxy(timeline);
			eventTimelineProxy.m_CheckpointGuid = ((timeline.m_Checkpoint != null) ? timeline.m_Checkpoint.m_Guid : string.Empty);
			list.Add(eventTimelineProxy);
		}
		return list;
	}

	public static void Deserialize(List<EventTimelineProxy> proxies)
	{
		Clear();
		if (proxies == null || proxies.Count == 0)
		{
			CreateTimeline();
			return;
		}
		int num = 0;
		foreach (EventTimelineProxy proxy in proxies)
		{
			EventTimeline eventTimeline = CreateTimeline();
			if (!string.IsNullOrEmpty(proxy.m_CheckpointGuid))
			{
				eventTimeline.m_Checkpoint = Checkpoints.FindByGuid(proxy.m_CheckpointGuid);
				if ((bool)eventTimeline.m_Checkpoint)
				{
					eventTimeline.m_Checkpoint.m_Timeline = eventTimeline;
					eventTimeline.SetCheckpointSprite();
				}
			}
			foreach (EventStageProxy stage in proxy.m_Stages)
			{
				if (num >= MAX_STAGES)
				{
					break;
				}
				EventStage eventStage = eventTimeline.AddStage();
				num++;
				foreach (EventUnitProxy unit in stage.m_Units)
				{
					MaybeAddUnit(eventStage, unit);
				}
				eventStage.ResizeForIcons();
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(eventTimeline.m_HorizontalLayoutGroup.GetComponent<RectTransform>());
			eventTimeline.UpdateSize();
		}
		SetAbsoluteIndexForAllStages();
	}

	public static int CalculateNumStages()
	{
		int num = 0;
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				_ = stage;
				num++;
			}
		}
		return num;
	}

	private static void SetAbsoluteIndexForAllStages()
	{
		int num = 0;
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				stage.m_AbsoluteStageIndex = num++;
				stage.UpdateLabel();
			}
		}
	}

	private static void MaybeAddUnit(EventStage stage, EventUnitProxy unitProxy)
	{
		Vehicle vehicle = Vehicles.FindByGuid(unitProxy.m_Guid);
		if ((bool)vehicle)
		{
			stage.AddUnit(vehicle.gameObject, EventUnitType.VEHICLE);
		}
		HydraulicsPhase hydraulicsPhase = HydraulicsPhases.FindByGuid(unitProxy.m_Guid);
		if ((bool)hydraulicsPhase)
		{
			stage.AddUnit(hydraulicsPhase.gameObject, EventUnitType.HYDRAULICS_PHASE);
		}
		VehicleRestartPhase vehicleRestartPhase = VehicleRestartPhases.FindByGuid(unitProxy.m_Guid);
		if ((bool)vehicleRestartPhase)
		{
			stage.AddUnit(vehicleRestartPhase.gameObject, EventUnitType.VEHICLE_RESTART_PHASE);
		}
		ZedAxisVehicle zedAxisVehicle = ZedAxisVehicles.FindByGuid(unitProxy.m_Guid);
		if ((bool)zedAxisVehicle)
		{
			stage.AddUnit(zedAxisVehicle.gameObject, EventUnitType.ZED_AXIS_VEHICLE);
		}
	}

	private static int GetIndexOfUnitSourceObject(GameObject go)
	{
		foreach (EventTimeline timeline in m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					if (unit.m_SourceObject == go)
					{
						return timeline.m_Stages.IndexOf(stage);
					}
				}
			}
		}
		return 0;
	}
}
