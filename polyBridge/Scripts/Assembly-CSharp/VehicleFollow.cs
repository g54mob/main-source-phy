using System.Collections.Generic;
using Poly.Game;
using UnityEngine;

public class VehicleFollow
{
	public static Vector3 m_LastVehicleOffsetFromCamera;

	public static float m_LastOrthographicSize;

	public static int m_VehicleIndex;

	public static Dictionary<EventStage, string> m_PreferredVehicleInStage = new Dictionary<EventStage, string>();

	public static readonly float VEHICLE_FOLLOW_DEFAULT_ORTHOGRAHPHIC_SIZE = 4.45f;

	public static void Reset()
	{
		m_VehicleIndex = 0;
	}

	public static void CycleNextVehicle()
	{
		if (!Profiles.m_ActiveProfile.m_FollowCar || Vehicles.m_Vehicles.Count < 2)
		{
			return;
		}
		int vehicleIndex = m_VehicleIndex;
		for (int i = 1; i <= Vehicles.m_Vehicles.Count; i++)
		{
			int num = (vehicleIndex + i) % Vehicles.m_Vehicles.Count;
			if (!Vehicles.m_Vehicles[num].m_ReachedVictoryFlag)
			{
				m_VehicleIndex = num;
				break;
			}
		}
		if (vehicleIndex == m_VehicleIndex || m_VehicleIndex < 0 || m_VehicleIndex >= Vehicles.m_Vehicles.Count)
		{
			return;
		}
		EventStage stageWithUnit = EventTimelines.GetStageWithUnit(Vehicles.m_Vehicles[m_VehicleIndex].gameObject);
		if (stageWithUnit != null)
		{
			if (m_PreferredVehicleInStage.ContainsKey(stageWithUnit))
			{
				m_PreferredVehicleInStage[stageWithUnit] = Vehicles.m_Vehicles[m_VehicleIndex].m_Guid;
			}
			else
			{
				m_PreferredVehicleInStage.Add(stageWithUnit, Vehicles.m_Vehicles[m_VehicleIndex].m_Guid);
			}
		}
		StartVehicleFollowFromDefaultOffset(Vehicles.m_Vehicles[m_VehicleIndex], Cameras.MainCamera().orthographicSize);
	}

	public static void MaybeStartFollowing(Vehicle vehicle)
	{
		if (Profiles.m_ActiveProfile.m_FollowCar)
		{
			Vehicle vehicleBeingFollowed = GetVehicleBeingFollowed();
			if (vehicleBeingFollowed != null)
			{
				vehicleBeingFollowed.m_PrevFollow = vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position;
				ForceFollowVehicle(vehicle);
			}
		}
	}

	public static void Toggle()
	{
		Profiles.m_ActiveProfile.m_FollowCar = !Profiles.m_ActiveProfile.m_FollowCar;
		if (!Profiles.m_ActiveProfile.m_FollowCar)
		{
			return;
		}
		Vehicle vehicleBeingFollowedFirst = GetVehicleBeingFollowedFirst();
		if (!(vehicleBeingFollowedFirst == null))
		{
			if (m_LastVehicleOffsetFromCamera.magnitude > 0.01f)
			{
				StartVehicleFollow(vehicleBeingFollowedFirst);
			}
			else
			{
				StartVehicleFollowFromDefaultOffset(vehicleBeingFollowedFirst, VEHICLE_FOLLOW_DEFAULT_ORTHOGRAHPHIC_SIZE);
			}
		}
	}

	public static void StartVehicleFollow(Vehicle followVehicle)
	{
		Vector3 position = followVehicle.m_VehicleSyncTargetChassis.transform.position;
		Vector3 pos = position - m_LastVehicleOffsetFromCamera;
		Quaternion rot = Quaternion.LookRotation(m_LastVehicleOffsetFromCamera.normalized);
		PointsOfView.Set(PointOfViewType.SIM_CUSTOM, position, pos, rot, m_LastOrthographicSize);
		PointsOfView.RotateTo(PointOfViewType.SIM_CUSTOM, 0f);
		m_VehicleIndex = Vehicles.m_Vehicles.IndexOf(followVehicle);
	}

	public static void SetVehicleOffsetFromCamera()
	{
		Vehicle vehicleBeingFollowed = GetVehicleBeingFollowed();
		if (vehicleBeingFollowed != null && vehicleBeingFollowed.m_VehicleSyncTargetChassis != null)
		{
			vehicleBeingFollowed.m_FollowOffset = new Vector2(vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position.x - Cameras.MainCamera().transform.position.x, vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position.y - Cameras.MainCamera().transform.position.y);
			m_LastVehicleOffsetFromCamera = vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position - Cameras.MainCamera().transform.position;
			m_LastOrthographicSize = Cameras.MainCamera().orthographicSize;
		}
	}

	public static Vehicle GetVehicleBeingFollowed()
	{
		if (Vehicles.m_Vehicles.Count == 0)
		{
			return null;
		}
		int index = Mathf.Clamp(m_VehicleIndex, 0, Vehicles.m_Vehicles.Count - 1);
		return Vehicles.m_Vehicles[index];
	}

	public static Vehicle GetVehicleBeingFollowedFirst()
	{
		foreach (EventTimeline timeline in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage in timeline.m_Stages)
			{
				foreach (EventUnit unit in stage.m_Units)
				{
					Vehicle vehicle = unit.GetVehicle();
					if ((bool)vehicle && !vehicle.m_ReachedVictoryFlag && unit.HasStartedSimulation() && m_PreferredVehicleInStage.ContainsKey(stage) && vehicle.m_Guid == m_PreferredVehicleInStage[stage])
					{
						return vehicle;
					}
				}
			}
		}
		foreach (EventTimeline timeline2 in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage2 in timeline2.m_Stages)
			{
				foreach (EventUnit unit2 in stage2.m_Units)
				{
					Vehicle vehicle2 = unit2.GetVehicle();
					if ((bool)vehicle2 && !vehicle2.m_ReachedVictoryFlag && unit2.HasStartedSimulation())
					{
						return vehicle2;
					}
				}
			}
		}
		foreach (EventTimeline timeline3 in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage3 in timeline3.m_Stages)
			{
				foreach (EventUnit unit3 in stage3.m_Units)
				{
					Vehicle vehicle3 = unit3.GetVehicle();
					if ((bool)vehicle3 && !vehicle3.m_ReachedVictoryFlag && m_PreferredVehicleInStage.ContainsKey(stage3) && vehicle3.m_Guid == m_PreferredVehicleInStage[stage3])
					{
						return vehicle3;
					}
				}
			}
		}
		foreach (EventTimeline timeline4 in EventTimelines.m_Timelines)
		{
			foreach (EventStage stage4 in timeline4.m_Stages)
			{
				foreach (EventUnit unit4 in stage4.m_Units)
				{
					Vehicle vehicle4 = unit4.GetVehicle();
					if ((bool)vehicle4 && !vehicle4.m_ReachedVictoryFlag)
					{
						return vehicle4;
					}
				}
			}
		}
		return GetVehicleBeingFollowed();
	}

	public static Vector2 GetVehicleFollowOffset()
	{
		Vehicle vehicleBeingFollowed = GetVehicleBeingFollowed();
		if (!(vehicleBeingFollowed != null))
		{
			return Vector2.zero;
		}
		return vehicleBeingFollowed.m_FollowOffset;
	}

	public static void UpdateManual()
	{
		if (!EnabledWithVehiclesInLevel() || Game.IsCurrentLevelTutorial())
		{
			return;
		}
		Vehicle vehicleBeingFollowed = GetVehicleBeingFollowed();
		if (vehicleBeingFollowed != null && vehicleBeingFollowed.m_VehicleSyncTargetChassis != null && vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position.y > 0f)
		{
			Vector2 vector = Utils.V3toV2(vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position) - vehicleBeingFollowed.m_PrevFollow;
			Cameras.MainCamera().transform.position = new Vector3(Cameras.MainCamera().transform.position.x + vector.x, Cameras.MainCamera().transform.position.y + vector.y, Cameras.MainCamera().transform.position.z);
			if (Cameras.In2DMode())
			{
				Cameras.MainCamera().transform.rotation = Quaternion.identity;
			}
			else
			{
				Vector3 normalized = (vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position - Cameras.MainCamera().transform.position).normalized;
				Cameras.MainCamera().transform.rotation = Quaternion.LookRotation(normalized);
			}
			CameraControl.RegisterTransformUpdate();
			vehicleBeingFollowed.m_PrevFollow = vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position;
			SetVehicleOffsetFromCamera();
		}
	}

	public static void StartVehicleFollow()
	{
		Vehicle vehicleBeingFollowed = GetVehicleBeingFollowed();
		if (vehicleBeingFollowed != null)
		{
			SetVehicleOffsetFromCamera();
			vehicleBeingFollowed.m_PrevFollow = vehicleBeingFollowed.m_VehicleSyncTargetChassis.transform.position;
		}
	}

	public static void StartVehicleFollowFromDefaultOffset(Vehicle followVehicle, float orthographicSize)
	{
		if (followVehicle != null)
		{
			float x = Cameras.MainCamera().transform.eulerAngles.x;
			float y = Cameras.MainCamera().transform.eulerAngles.y;
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_CUSTOM].m_Pitch = x;
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_CUSTOM].m_Yaw = y;
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_CUSTOM].m_OrthographicsSize = orthographicSize;
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_CUSTOM].SetPivot(followVehicle.m_VehicleSyncTargetChassis.transform.position);
			PointsOfView.SnapTo(PointOfViewType.SIM_CUSTOM);
			followVehicle.m_PrevFollow = followVehicle.m_VehicleSyncTargetChassis.transform.position;
			SetVehicleOffsetFromCamera();
			m_VehicleIndex = Vehicles.m_Vehicles.IndexOf(followVehicle);
		}
	}

	public static void ForceFollowVehicle(Vehicle vehicle)
	{
		if (Vehicles.GetVehicleIndex(vehicle) != -1)
		{
			StartVehicleFollowFromDefaultOffset(vehicle, Cameras.MainCamera().orthographicSize);
		}
	}

	public static bool EnabledWithVehiclesInLevel()
	{
		if (Profiles.m_ActiveProfile.m_FollowCar)
		{
			return Vehicles.m_Vehicles.Count > 0;
		}
		return false;
	}
}
