using System.Collections.Generic;
using Poly.Collide;
using UnityEngine;

public class ZedAxisVehicles
{
	public static List<ZedAxisVehicle> m_Vehicles = new List<ZedAxisVehicle>();

	public static readonly float DEFAULT_SPAWN_IN_Z = 20f;

	public static readonly float DEFAULT_SPAWN_OUT_Z = -20f;

	public static float MIN_NORMALIZED_SCALE = 0.2f;

	public static float MIN_NORMALIZED_SCALE_SLIDER = 0.5f;

	public static float MAX_NORMALIZED_SCALE_SLIDER = 2f;

	public static float MAX_NORMALIZED_SCALE = 4f;

	public static float MIN_SPEED = 0.1f;

	public static float MAX_SPEED = 100f;

	public static ZedAxisVehicle CreateVehicle(GameObject prefab, string modId, Vector3 pos, Quaternion rot, string guid)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		ZedAxisVehicle component = gameObject.GetComponent<ZedAxisVehicle>();
		if (!component)
		{
			return null;
		}
		component.m_Guid = guid;
		component.name = prefab.name;
		component.m_Speed = component.m_DefaultSpeed;
		component.m_OutlineMeshRenderer.gameObject.SetActive(value: false);
		component.m_ModId = modId;
		if (!string.IsNullOrEmpty(modId))
		{
			component.m_Stub = ZedAxisVehicleStubs.GetStubByAddressable(prefab.name);
		}
		component.m_SnapToWaterLine = component.m_Stub.m_Type == ZedAxisVehicleType.BOAT;
		m_Vehicles.Add(component);
		return component;
	}

	public static void DestroyVehicle(ZedAxisVehicle vehicle)
	{
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
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.UpdateManual();
			}
		}
	}

	public static void FixedUpdateManual()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				vehicle.FixedUpdateManual();
			}
		}
	}

	public static void EnablePhysics()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.EnablePhysics();
		}
	}

	public static void Disable()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.gameObject.SetActive(value: false);
			vehicle.StopLoopSoundImmediate();
		}
	}

	public static void Enable()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.gameObject.SetActive(value: true);
		}
	}

	public static void DestroyAll()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.gameObject.SetActive(value: false);
			Object.Destroy(vehicle.gameObject);
		}
		m_Vehicles.Clear();
	}

	public static void Restore()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.EndSimulation();
			vehicle.Restore();
			vehicle.gameObject.SetActive(value: false);
		}
	}

	public static ZedAxisVehicle FindByGuid(string guid)
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			if (vehicle.m_Guid == guid)
			{
				return vehicle;
			}
		}
		return null;
	}

	public static void PositionAtStartingZ()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			float num = 0f;
			if (vehicle.m_Reverse)
			{
				vehicle.transform.rotation = Quaternion.Euler(0f, 180f, 0f - vehicle.m_RotationDegrees);
				num = DEFAULT_SPAWN_OUT_Z - vehicle.m_MeshRenderer.bounds.size.z / 2f;
				vehicle.m_OutlineMeshFilter.transform.localScale = new Vector3(vehicle.m_OutlineMeshFilter.transform.localScale.x, vehicle.m_OutlineMeshFilter.transform.localScale.y, 0f - Mathf.Abs(vehicle.m_OutlineMeshFilter.transform.localScale.z));
			}
			else
			{
				vehicle.transform.rotation = Quaternion.Euler(0f, 0f, 0f - vehicle.m_RotationDegrees);
				num = DEFAULT_SPAWN_IN_Z + vehicle.m_MeshRenderer.bounds.size.z / 2f;
				vehicle.m_OutlineMeshFilter.transform.localScale = new Vector3(vehicle.m_OutlineMeshFilter.transform.localScale.x, vehicle.m_OutlineMeshFilter.transform.localScale.y, Mathf.Abs(vehicle.m_OutlineMeshFilter.transform.localScale.z));
			}
			vehicle.transform.position = new Vector3(vehicle.transform.position.x, vehicle.transform.position.y, num);
			vehicle.m_SpawnPos = vehicle.transform.position;
		}
	}

	public static void PositionAtCenterAndActivate()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.gameObject.SetActive(value: true);
			vehicle.transform.position = new Vector3(vehicle.transform.position.x, vehicle.transform.position.y, vehicle.m_MeshRenderer.bounds.size.z / 2f);
		}
	}

	public static void DisableOutlines()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.EnableMeshRendering();
		}
	}

	public static void EnableOutlineMeshRendering()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.EnableOutlineMeshRendering();
		}
	}

	public static void Hide(bool hide)
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.Hide(hide);
		}
	}

	public static void UpdateOutlines()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.UpdateOutline();
		}
	}

	public static void UpdatePolygonShapes()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.UpdatePolygonShapes();
		}
	}

	public static void UpdateSpawnTransform()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.m_SpawnPos = vehicle.transform.position;
		}
	}

	public static void LinkToCuttingPlane(GameObject plane1, GameObject plane2)
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.LinkToCuttingPlane(plane1, plane2);
		}
	}

	public static void UnlinkFromCuttingPlane()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.UnLinkFromCuttingPlane();
		}
	}

	public static List<ZedAxisVehicleProxy> Serialize()
	{
		List<ZedAxisVehicleProxy> list = new List<ZedAxisVehicleProxy>();
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			if (vehicle.gameObject.activeInHierarchy)
			{
				list.Add(new ZedAxisVehicleProxy(vehicle));
			}
		}
		return list;
	}

	public static void Deserialize(List<ZedAxisVehicleProxy> proxies, int version)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (ZedAxisVehicleProxy proxy in proxies)
		{
			CreateVehicleFromProxy(proxy, version);
		}
	}

	public static ZedAxisVehicle CreateVehicleFromProxy(ZedAxisVehicleProxy proxy, int version)
	{
		if (proxy.m_PrefabName.StartsWith("VEHICLE_"))
		{
			proxy.m_PrefabName = proxy.m_PrefabName.Substring("VEHICLE_".Length);
		}
		if (!Prefabs.AsyncPrefabExists(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		ZedAxisVehicle zedAxisVehicle = CreateVehicle(Prefabs.GetAsyncPrefab(proxy.m_PrefabName), proxy.m_ModId, proxy.m_Pos, proxy.m_Rot, proxy.m_Guid);
		if (!zedAxisVehicle)
		{
			return null;
		}
		ApplyProxyToVehicle(zedAxisVehicle, proxy, version);
		SandboxItem component = zedAxisVehicle.GetComponent<SandboxItem>();
		if ((bool)component && (bool)component.m_Label)
		{
			component.UpdateFloatingText();
		}
		return zedAxisVehicle;
	}

	public static void ApplyProxyToVehicle(ZedAxisVehicle vehicle, ZedAxisVehicleProxy proxy, int version)
	{
		vehicle.m_TimeDelaySeconds = proxy.m_TimeDelaySeconds;
		vehicle.m_Speed = proxy.m_Speed;
		vehicle.m_SpawnPos = proxy.m_Pos;
		vehicle.m_RotationDegrees = proxy.m_RotationDegrees;
		vehicle.m_Reverse = proxy.m_Reverse;
		vehicle.m_SnapToWaterLine = proxy.m_SnapToWaterLine;
		vehicle.SetUniformScale(proxy.m_UniformScale);
		vehicle.UpdatePolygonShapes();
	}

	public static void Remove(ZedAxisVehicle vehicle)
	{
		if (m_Vehicles.Contains(vehicle))
		{
			m_Vehicles.Remove(vehicle);
		}
	}

	public static ZedAxisVehicle GetClosestThatOverlapPolygonShape(Vector2 pos, PolygonShape shape)
	{
		ZedAxisVehicle result = null;
		float num = float.MaxValue;
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
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

	public static void EnterSandboxMode()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.OnlyDrawOutline();
		}
	}

	public static void SnapToWaterLine()
	{
		foreach (ZedAxisVehicle vehicle in m_Vehicles)
		{
			vehicle.SnapToWaterLine();
		}
	}
}
