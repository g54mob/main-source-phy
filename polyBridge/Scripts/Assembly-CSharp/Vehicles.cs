using System.Collections.Generic;
using Poly.Base;
using Poly.Collide;
using Poly.Determinism;
using Poly.Game;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

public class Vehicles
{
	public static float m_PointScale = 1f;

	public static float m_SpotScale = 1f;

	public static List<Vehicle> m_Vehicles = new List<Vehicle>();

	public static float VEHICLE_FAIL_Y_POS = -5f;

	public static float MIN_MASS = 0.4f;

	public static float MAX_MASS = 1000f;

	public static float MIN_SPEED = 0f;

	public static float MAX_SPEED = 100f;

	public static float MIN_DESIRED_ACCELERATION = 0f;

	public static float MAX_DESIRED_ACCELERATION = 100f;

	public static float MIN_HORSEPOWER = 0f;

	public static float MAX_HORSEPOWER = 100f;

	public static float MIN_BRAKING_INTENSITY = 0.1f;

	public static float MAX_BREAKING_INTENSITY = 100f;

	public static float MIN_SHOCKS_MULTIPLIER = 0.1f;

	public static float MAX_SHOCKS_MULTIPLIER = 10f;

	public static float MIN_NORMALIZED_SCALE = 0.2f;

	public static float MIN_NORMALIZED_SCALE_SLIDER = 0.5f;

	public static float MAX_NORMALIZED_SCALE_SLIDER = 2f;

	public static float MAX_NORMALIZED_SCALE = 4f;

	public static float FORCE_SHOW_VEHICLE_MESH_SECONDS = 2f;

	public static Vehicle CreateVehicle(GameObject prefab, string modId, Vector3 pos, Quaternion rot, string guid)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		Vehicle component = gameObject.GetComponent<Vehicle>();
		if (!component)
		{
			return null;
		}
		component.m_Guid = guid;
		component.name = prefab.name;
		component.m_ModId = modId;
		if (component.m_Stub == null)
		{
			component.m_Stub = VehicleStubs.GetStubByAddressable(prefab.name);
		}
		component.m_SkinID = ((component.m_Stub.m_Skins.Length != 0) ? component.m_Stub.m_Skins[0].m_ID : string.Empty);
		VehicleSkin[] skins = component.m_Stub.m_Skins;
		for (int i = 0; i < skins.Length; i++)
		{
			VehicleSkins.Add(skins[i]);
		}
		m_Vehicles.Add(component);
		return component;
	}

	public static void DestroyVehicle(Vehicle vehicle)
	{
		VehicleStopTrigger vehicleStopTrigger = VehicleStopTriggers.FindTriggerThatStopsVehicle(vehicle.m_Guid);
		if ((bool)vehicleStopTrigger)
		{
			Object.Destroy(vehicleStopTrigger.gameObject);
		}
		for (int num = vehicle.m_Checkpoints.Count - 1; num >= 0; num--)
		{
			Checkpoints.DestroyCheckpoint(vehicle.m_Checkpoints[num]);
		}
		vehicle.m_Checkpoints.Clear();
		EventEditor.RemoveUnit(vehicle.gameObject);
		if (m_Vehicles.Contains(vehicle))
		{
			m_Vehicles.Remove(vehicle);
		}
		vehicle.gameObject.SetActive(value: false);
		Object.Destroy(vehicle.gameObject);
	}

	public static void UpdateManual()
	{
		for (int i = 0; i < m_Vehicles.Count; i++)
		{
			m_Vehicles[i].UpdateManual();
		}
	}

	public static void FixedUpdateManual()
	{
		for (int i = 0; i < m_Vehicles.Count; i++)
		{
			m_Vehicles[i].FixedUpdateManual();
		}
	}

	public static void UpdateSpawnTransform()
	{
		for (int i = 0; i < m_Vehicles.Count; i++)
		{
			Vehicle vehicle = m_Vehicles[i];
			vehicle.m_SpawnPos = vehicle.transform.position;
			vehicle.m_SpawnRot = vehicle.transform.rotation;
		}
	}

	public static void EnablePhysics()
	{
		for (int i = 0; i < m_Vehicles.Count; i++)
		{
			m_Vehicles[i].EnablePhysics();
		}
	}

	public static void DestroyAll()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.gameObject.SetActive(value: false);
			Object.Destroy(vehicle.gameObject);
		}
		m_Vehicles.Clear();
	}

	public static void Restore()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.EndSimulation();
			vehicle.TurnLightsOff();
			vehicle.Restore();
		}
	}

	public static void ResetCheckpoints()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.ResetCheckpoints();
		}
	}

	public static Vehicle FindByGuid(string guid)
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.m_Guid == guid && vehicle.gameObject.activeInHierarchy)
			{
				return vehicle;
			}
		}
		return null;
	}

	public static bool AllVehiclesHaveCollectedVictoryFlags()
	{
		if (m_Vehicles.Count == 0)
		{
			return false;
		}
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (!vehicle.m_ReachedVictoryFlag)
			{
				return false;
			}
		}
		return true;
	}

	public static Vehicle GetVehicleThatMeetsFailConditons()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (!vehicle.m_ReachedVictoryFlag)
			{
				if ((bool)vehicle.WheelsUnderWater())
				{
					return vehicle;
				}
				if (vehicle.transform.position.y < VEHICLE_FAIL_Y_POS || !vehicle.m_isRenderingEnabled)
				{
					return vehicle;
				}
			}
		}
		return null;
	}

	public static void DisableOutlines()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.EnableMeshRendering();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.UpdateOutline();
		}
	}

	public static bool OverlapsPolygonShape(PolygonShape shape)
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.OverlapsPolygonShape(shape))
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdatePolygonShapes()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.UpdatePolygonShapes();
		}
	}

	public static void Debug_VisualizePolygonShapes()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.Debug_VisualizePolygonShapes();
		}
	}

	public static void TurnOffMotorForAll()
	{
		DeterminismLog.LogEvent(null, Poly.Determinism.EventType.AllEnginesStop);
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.SetPhysicsVehicleTargetSpeed(0f);
		}
	}

	public static Vehicle GetClosestThatOverlapPolygonShape(Vector2 pos, PolygonShape shape)
	{
		Vehicle result = null;
		float num = float.MaxValue;
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.OverlapsPolygonShape(shape))
			{
				float num2 = Vector2.Distance(pos, vehicle.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = vehicle;
				}
			}
		}
		return result;
	}

	public static List<VehicleProxy> Serialize()
	{
		List<VehicleProxy> list = new List<VehicleProxy>();
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				list.Add(new VehicleProxy(vehicle));
			}
		}
		return list;
	}

	public static void Deserialize(List<VehicleProxy> proxies, int version)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (VehicleProxy proxy in proxies)
		{
			CreateVehicleFromProxy(proxy, version);
		}
	}

	public static Vehicle CreateVehicleFromProxy(VehicleProxy proxy, int version)
	{
		if (!Prefabs.AsyncPrefabExists(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		Vehicle vehicle = CreateVehicle(Prefabs.GetAsyncPrefab(proxy.m_PrefabName), proxy.m_ModId, proxy.m_Pos, proxy.m_Rot, proxy.m_Guid);
		if (!vehicle)
		{
			return null;
		}
		ApplyProxyToVehicle(vehicle, proxy, version);
		SandboxItem component = vehicle.GetComponent<SandboxItem>();
		if ((bool)component && (bool)component.m_Label)
		{
			component.UpdateFloatingText();
		}
		return vehicle;
	}

	public static void ApplyProxyToVehicle(Vehicle vehicle, VehicleProxy proxy, int version)
	{
		if (version >= 2)
		{
			vehicle.m_TargetSpeed = proxy.m_TargetSpeed;
			vehicle.m_Mass = proxy.m_Mass;
			vehicle.m_IdleOnDownhill = proxy.m_IdleOnDownhill;
			vehicle.m_BrakingForceMultiplier = Mathf.Clamp(proxy.m_BrakingForceMultiplier, MIN_BRAKING_INTENSITY, MAX_BREAKING_INTENSITY);
			vehicle.m_Acceleration = Mathf.Clamp(proxy.m_Acceleration, MIN_HORSEPOWER, MAX_HORSEPOWER);
			vehicle.m_DesiredAcceleration = Mathf.Clamp(proxy.m_DesiredAcceleration, MIN_DESIRED_ACCELERATION, MAX_DESIRED_ACCELERATION);
			vehicle.m_ShocksMultiplier = Mathf.Clamp(proxy.m_ShocksMultiplier, MIN_SHOCKS_MULTIPLIER, MAX_SHOCKS_MULTIPLIER);
		}
		vehicle.m_RotationDegrees = Utils.ConvertAngleToMinus180ToPositive180Range(proxy.m_RotationDegrees);
		vehicle.m_TimeDelaySeconds = proxy.m_TimeDelaySeconds;
		vehicle.m_Flipped = proxy.m_Flipped;
		vehicle.SetLocalScale(vehicle.m_Flipped);
		vehicle.m_OrderedCheckpoints = proxy.m_OrderedCheckpoints;
		vehicle.m_SpawnPos = proxy.m_Pos;
		vehicle.m_SpawnRot = proxy.m_Rot;
		if (string.IsNullOrEmpty(proxy.m_SkinID))
		{
			vehicle.m_SkinID = ((vehicle.m_Stub.m_Skins.Length != 0) ? vehicle.m_Stub.m_Skins[0].m_ID : string.Empty);
		}
		else
		{
			vehicle.m_SkinID = proxy.m_SkinID;
		}
		if (!string.IsNullOrEmpty(vehicle.m_SkinID))
		{
			vehicle.MaybeLoadCurrentSkinTexture();
			vehicle.UploadCurrentSkinToShader();
		}
		vehicle.SetUniformScale(proxy.m_UniformScale);
		vehicle.UpdatePolygonShapes();
		if (proxy.m_CheckpointGuids != null)
		{
			vehicle.m_CheckpointGuids.Clear();
			vehicle.m_CheckpointGuids.AddRange(proxy.m_CheckpointGuids);
		}
	}

	public static void ResolveCheckpointGuids()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.ResolveCheckpointGuids();
		}
	}

	public static void Remove(Vehicle vehicle)
	{
		if (m_Vehicles.Contains(vehicle))
		{
			m_Vehicles.Remove(vehicle);
		}
	}

	public static void TurnWheelFillMeshesOn()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.TurnWheelFillMeshOn();
		}
	}

	public static void TurnWheelFillMeshesOff()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.TurnWheelFillMeshOff();
		}
	}

	public static Vehicle FindVehicleWithLabel(string label)
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.GetTextMeshString().ToLower() == label.ToLower())
			{
				return vehicle;
			}
		}
		return null;
	}

	public static void DesaturateAllExcept(string guid)
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.m_Guid == guid)
			{
				vehicle.m_SandboxItem.Desaturate(on: false);
			}
			else
			{
				vehicle.m_SandboxItem.Desaturate(on: true);
			}
		}
	}

	public static void RefreshNightLightIntensity()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.RefreshNightLightIntensity();
		}
	}

	public static void AddVisiblityBlock()
	{
		Bounds2 bounds = SingletonBehaviour<World>.instance.bounds;
		bounds.min.y = SingletonBehaviour<World>.instance.bounds.min.y + 5f;
		FastAabbTrigger fastAabbTrigger = new FastAabbTrigger(bounds);
		fastAabbTrigger.layer = Layer.VisibilityArea_Trigger;
		fastAabbTrigger.bodyOverlapCallback = HideVehiclesOutsideGameplayAreaListener.Add;
		SingletonBehaviour<World>.instance.AddFastTrigger(fastAabbTrigger);
	}

	public static void EnterSandboxMode()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			vehicle.EnterSandboxMode();
		}
	}

	public static void ShowVehicleWheelsLine()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.MaybeShowVehicleWheelsLine();
			}
		}
	}

	public static void TurnOffWheelsLine()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.TurnOffWheelsLine();
			}
		}
	}

	public static void UpdateWheelsLineWidth()
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.UpdateWheelsLineWidth();
			}
		}
	}

	public static int GetVehicleIndex(Vehicle vehicle)
	{
		if (m_Vehicles.Contains(vehicle))
		{
			return m_Vehicles.IndexOf(vehicle);
		}
		return -1;
	}

	public static void ShowCenterOfMass(bool on)
	{
		foreach (Vehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.ShowCenterOfMassIcon(on);
			}
		}
	}
}
