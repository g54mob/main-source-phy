using System.Collections.Generic;
using UnityEngine;

public class VehicleStopTriggers
{
	public static List<VehicleStopTrigger> m_Triggers = new List<VehicleStopTrigger>();

	public static float MIN_HEIGHT = 1f;

	public static float MIN_HEIGHT_SLIDER = 1f;

	public static float MAX_HEIGHT_SLIDER = 12f;

	public static float MAX_HEIGHT = 100f;

	public static float DEFAULT_POLE_HEIGHT = 1.75f;

	private static List<VehicleStopTrigger> m_CastRayCandidates = new List<VehicleStopTrigger>();

	public static VehicleStopTrigger CreateTrigger(GameObject prefab, Color color, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		VehicleStopTrigger component = gameObject.GetComponent<VehicleStopTrigger>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		component.m_VehicleGuid = string.Empty;
		component.SetFlagColor(color);
		component.m_IndexInScene = m_Triggers.Count;
		m_Triggers.Add(component);
		return component;
	}

	public static void Restore()
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			trigger.Restore();
		}
	}

	public static void DestroyTrigger(VehicleStopTrigger trigger)
	{
		trigger.gameObject.SetActive(value: false);
		Object.Destroy(trigger.gameObject);
	}

	public static void DestroyAll()
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			trigger.m_IndexInScene = -1;
			DestroyTrigger(trigger);
		}
		m_Triggers.Clear();
	}

	public static VehicleStopTrigger FindTriggerThatStopsVehicle(string guid)
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			if (trigger.m_VehicleGuid == guid)
			{
				return trigger;
			}
		}
		return null;
	}

	public static void DisableOutlines()
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			trigger.DisableOutline();
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			trigger.EnableMeshRendering();
		}
	}

	public static void UpdateOutlines()
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			trigger.UpdateOutline();
		}
	}

	public static List<VehicleStopTriggerProxy> Serialize()
	{
		List<VehicleStopTriggerProxy> list = new List<VehicleStopTriggerProxy>();
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			if (trigger.gameObject.activeInHierarchy)
			{
				list.Add(new VehicleStopTriggerProxy(trigger));
			}
		}
		return list;
	}

	public static void Deserialize(List<VehicleStopTriggerProxy> proxies)
	{
		foreach (VehicleStopTriggerProxy proxy in proxies)
		{
			CreateTriggerFromProxy(proxy);
		}
	}

	public static VehicleStopTrigger CreateTriggerFromProxy(VehicleStopTriggerProxy proxy)
	{
		proxy.m_PrefabName = proxy.m_PrefabName.Replace("FlagPoleA", "VictoryFlag");
		if (!Prefabs.m_PrefabsDict.ContainsKey(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		GameObject prefab = Prefabs.m_PrefabsDict[proxy.m_PrefabName];
		Vehicle vehicle = Vehicles.FindByGuid(proxy.m_StopVehicleGuid);
		if (!vehicle)
		{
			return null;
		}
		VehicleStopTrigger vehicleStopTrigger = CreateTrigger(prefab, vehicle.GetFlagColor(), proxy.m_Pos, proxy.m_Rot);
		if ((bool)vehicleStopTrigger)
		{
			ApplyProxyToTrigger(vehicleStopTrigger, proxy);
		}
		return vehicleStopTrigger;
	}

	public static void ApplyProxyToTrigger(VehicleStopTrigger trigger, VehicleStopTriggerProxy proxy)
	{
		trigger.m_VehicleGuid = proxy.m_StopVehicleGuid;
		trigger.m_RotationDegrees = proxy.m_RotationDegrees;
		trigger.m_InvisibleInSim = proxy.m_InvisibleInSim;
		trigger.m_Height = proxy.m_Height;
		if (trigger.m_Height < MIN_HEIGHT)
		{
			trigger.m_Height = DEFAULT_POLE_HEIGHT;
		}
		trigger.SetPoleScaleForHeight(trigger.m_Height);
		trigger.m_Flipped = proxy.m_Flipped;
		if (trigger.m_Flipped)
		{
			trigger.m_PoleAndFlag.transform.localScale = new Vector3(0f - Mathf.Abs(trigger.m_PoleAndFlag.transform.localScale.x), trigger.m_PoleAndFlag.transform.localScale.y, trigger.m_PoleAndFlag.transform.localScale.z);
		}
		else
		{
			trigger.m_PoleAndFlag.transform.localScale = new Vector3(Mathf.Abs(trigger.m_PoleAndFlag.transform.localScale.x), trigger.m_PoleAndFlag.transform.localScale.y, trigger.m_PoleAndFlag.transform.localScale.z);
		}
	}

	public static void DesaturateAllExceptForVehicle(string guid)
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			if (trigger.m_VehicleGuid == guid)
			{
				trigger.m_SandboxItem.Desaturate(on: false);
			}
			else
			{
				trigger.m_SandboxItem.Desaturate(on: true);
			}
		}
	}

	public static VehicleStopTrigger CastRay(Ray ray)
	{
		m_CastRayCandidates.Clear();
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			if (trigger.m_HotSpot.bounds.IntersectRay(ray))
			{
				m_CastRayCandidates.Add(trigger);
			}
		}
		return m_CastRayCandidates.Count switch
		{
			0 => null, 
			1 => m_CastRayCandidates[0], 
			_ => GetClosestToWorldPos(Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()), m_CastRayCandidates), 
		};
	}

	public static void EnableHotspotColliders(bool on)
	{
		foreach (VehicleStopTrigger trigger in m_Triggers)
		{
			trigger.EnableHotspotCollider(on);
		}
	}

	private static VehicleStopTrigger GetClosestToWorldPos(Vector3 worldPos, List<VehicleStopTrigger> triggers)
	{
		VehicleStopTrigger result = null;
		float num = float.MaxValue;
		foreach (VehicleStopTrigger trigger in triggers)
		{
			float num2 = Vector2.Distance(trigger.transform.position, worldPos);
			if (num2 < num)
			{
				result = trigger;
				num = num2;
			}
		}
		return result;
	}
}
