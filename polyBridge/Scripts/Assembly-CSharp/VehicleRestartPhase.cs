using System;
using UnityEngine;

public class VehicleRestartPhase : MonoBehaviour
{
	public Sprite m_Sprite;

	[NonSerialized]
	public float m_TimeDelaySeconds;

	[NonSerialized]
	public string m_Guid;

	[NonSerialized]
	public SandboxItem m_SandboxItem;

	[NonSerialized]
	public string m_VehicleGuid;

	private void Awake()
	{
		m_SandboxItem = GetComponent<SandboxItem>();
	}

	private void OnDestroy()
	{
		if (VehicleRestartPhases.m_Phases.Contains(this))
		{
			VehicleRestartPhases.m_Phases.Remove(this);
		}
	}

	public void StartSimulation()
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_VehicleGuid);
		if (!vehicle)
		{
			return;
		}
		Checkpoint checkpoint = Checkpoints.FindCheckpointForVehicleRestartPhase(m_Guid);
		if ((bool)checkpoint && !vehicle.m_ReachedVictoryFlag)
		{
			if (checkpoint.m_ReverseVehicleOnRestart)
			{
				vehicle.PhysicsVehicleFlip();
			}
			vehicle.SetPhysicsVehicleTargetSpeed(vehicle.m_TargetSpeed);
			vehicle.m_ReachedStopCheckpoint = false;
		}
	}

	public bool IsComplete()
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_VehicleGuid);
		if (!vehicle)
		{
			return false;
		}
		if (vehicle.m_ReachedVictoryFlag || vehicle.m_ReachedStopCheckpoint)
		{
			return true;
		}
		Checkpoint checkpoint = Checkpoints.FindCheckpointForVehicleRestartPhase(m_Guid);
		if ((bool)checkpoint && vehicle.HasPickedUpCheckpoint(checkpoint) && HasPendingRestartForVehicle(vehicle))
		{
			return true;
		}
		return false;
	}

	public void RefreshEventUnitLabel()
	{
		EventUnit unitMatchingVehicleRestartPhase = EventTimelines.GetUnitMatchingVehicleRestartPhase(this);
		if ((bool)unitMatchingVehicleRestartPhase)
		{
			Checkpoint checkpoint = Checkpoints.FindCheckpointForVehicleRestartPhase(m_Guid);
			if ((bool)checkpoint)
			{
				unitMatchingVehicleRestartPhase.SetText(checkpoint.GetComponent<SandboxItem>().m_Label.m_Text.text);
			}
		}
	}

	public void AddToEventEdtior()
	{
		Vehicle vehicle = Vehicles.FindByGuid(m_VehicleGuid);
		if (!vehicle)
		{
			return;
		}
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(vehicle.gameObject);
		if ((bool)stageWithUnit && EventTimelines.CalculateNumStages() < EventTimelines.MAX_STAGES)
		{
			EventStage eventStage = stageWithUnit.m_ParentTimeline.AddStage();
			if ((bool)eventStage)
			{
				eventStage.AddUnit(base.gameObject, EventUnitType.VEHICLE_RESTART_PHASE);
			}
		}
	}

	private bool HasPendingRestartForVehicle(Vehicle vehicle)
	{
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(base.gameObject);
		if (!stageWithUnit)
		{
			return false;
		}
		EventTimeline parentTimeline = stageWithUnit.m_ParentTimeline;
		int num = parentTimeline.m_Stages.IndexOf(stageWithUnit);
		if (num == -1 || num == parentTimeline.m_Stages.Count - 1)
		{
			return false;
		}
		foreach (EventUnit unit in parentTimeline.m_Stages[num + 1].m_Units)
		{
			if (EventUnitIsPendingRestartForVehicle(unit, vehicle))
			{
				return true;
			}
		}
		return false;
	}

	private bool EventUnitIsPendingRestartForVehicle(EventUnit unit, Vehicle vehicle)
	{
		if (unit.m_SourceObject != null && unit.m_Type == EventUnitType.VEHICLE_RESTART_PHASE && unit.gameObject != base.gameObject)
		{
			VehicleRestartPhase component = unit.m_SourceObject.GetComponent<VehicleRestartPhase>();
			if (component.m_VehicleGuid != vehicle.m_Guid)
			{
				return false;
			}
			Checkpoint checkpoint = Checkpoints.FindCheckpointForVehicleRestartPhase(component.m_Guid);
			if ((bool)checkpoint && vehicle.HasPickedUpCheckpoint(checkpoint))
			{
				return true;
			}
		}
		return false;
	}
}
