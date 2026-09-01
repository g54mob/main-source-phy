using System.Collections.Generic;
using UnityEngine;

public class Checkpoints
{
	public static List<Checkpoint> m_Checkpoints = new List<Checkpoint>();

	private static List<Checkpoint> m_CastRayCandidates = new List<Checkpoint>();

	public static Checkpoint CreateCheckpoint(GameObject prefab, Color color, Vector3 pos, Quaternion rot, string guid)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		Checkpoint component = gameObject.GetComponent<Checkpoint>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		component.m_Guid = guid;
		component.m_VehicleGuid = string.Empty;
		component.m_VehicleRestartPhaseGuid = string.Empty;
		component.SetColor(color);
		component.m_IndexInScene = m_Checkpoints.Count;
		m_Checkpoints.Add(component);
		return component;
	}

	public static void DestroyCheckpoint(Checkpoint checkpoint)
	{
		checkpoint.DestroyManual();
	}

	public static void Restore()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.Restore();
		}
	}

	public static void DestroyAll()
	{
		for (int num = m_Checkpoints.Count - 1; num >= 0; num--)
		{
			m_Checkpoints[num].m_IndexInScene = -1;
			DestroyCheckpoint(m_Checkpoints[num]);
		}
		m_Checkpoints.Clear();
	}

	public static Checkpoint FindByGuid(string guid)
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.m_Guid == guid)
			{
				return checkpoint;
			}
		}
		return null;
	}

	public static Checkpoint FindCheckpointForVehicle(string vehicleGuid)
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.m_VehicleGuid == vehicleGuid)
			{
				return checkpoint;
			}
		}
		return null;
	}

	public static Checkpoint FindCheckpointForVehicleRestartPhase(string phaseGuid)
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.m_VehicleRestartPhaseGuid == phaseGuid)
			{
				return checkpoint;
			}
		}
		return null;
	}

	public static void DisableOutlines()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.gameObject.activeInHierarchy)
			{
				checkpoint.DisableOutline();
			}
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.gameObject.activeInHierarchy)
			{
				checkpoint.EnableMeshRendering();
			}
		}
	}

	public static void UpdateOutlines()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.gameObject.activeInHierarchy)
			{
				checkpoint.UpdateOutline();
			}
		}
	}

	public static void UpdateFloatingText()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.gameObject.activeInHierarchy)
			{
				checkpoint.UpdateFloatingText();
			}
		}
	}

	public static void ResetScale()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.ResetScale();
		}
	}

	public static List<CheckpointProxy> Serialize()
	{
		List<CheckpointProxy> list = new List<CheckpointProxy>();
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.gameObject.activeInHierarchy)
			{
				list.Add(new CheckpointProxy(checkpoint));
			}
		}
		return list;
	}

	public static void Deserialize(List<CheckpointProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (CheckpointProxy proxy in proxies)
		{
			CreateCheckpointFromProxy(proxy);
		}
	}

	public static Checkpoint CreateCheckpointFromProxy(CheckpointProxy proxy)
	{
		if (!Prefabs.m_PrefabsDict.ContainsKey(proxy.m_PrefabName))
		{
			Debug.LogWarningFormat("Could not find prefab {0} in Prefab Dictionary", proxy.m_PrefabName);
			return null;
		}
		GameObject gameObject = Prefabs.m_PrefabsDict[proxy.m_PrefabName];
		Vehicle vehicle = Vehicles.FindByGuid(proxy.m_VehicleGuid);
		if (!vehicle)
		{
			return null;
		}
		Checkpoint checkpoint = CreateCheckpoint(gameObject, vehicle.GetFlagColor(), proxy.m_Pos, gameObject.transform.rotation, proxy.m_Guid);
		if ((bool)checkpoint)
		{
			ApplyProxyToCheckpoint(checkpoint, proxy);
		}
		return checkpoint;
	}

	public static void ApplyProxyToCheckpoint(Checkpoint checkpoint, CheckpointProxy proxy)
	{
		checkpoint.m_VehicleGuid = proxy.m_VehicleGuid;
		checkpoint.m_VehicleRestartPhaseGuid = proxy.m_VehicleRestartPhaseGuid;
		checkpoint.m_TriggerTimeline = proxy.m_TriggerTimeline;
		checkpoint.m_StopVehicle = proxy.m_StopVehicle;
		checkpoint.m_ReverseVehicleOnRestart = proxy.m_ReverseVehicleOnRestart;
		checkpoint.m_InvisibleInSim = proxy.m_InvisibleInSim;
		checkpoint.RefreshMesh();
		checkpoint.InstantiatePickupFX();
		checkpoint.EnterGameState(GameStateManager.GetState());
	}

	public static void DesaturateAllExceptForVehicle(string guid)
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.m_VehicleGuid == guid)
			{
				checkpoint.m_SandboxItem.Desaturate(on: false);
				checkpoint.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, -0.01f);
			}
			else
			{
				checkpoint.m_SandboxItem.Desaturate(on: true);
				checkpoint.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, 0f);
			}
			checkpoint.SetOutlineColor();
		}
	}

	public static Checkpoint CastRay(Ray ray)
	{
		m_CastRayCandidates.Clear();
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			if (checkpoint.m_Hotspot != null && checkpoint.m_Hotspot.bounds.IntersectRay(ray))
			{
				m_CastRayCandidates.Add(checkpoint);
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
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.EnableHotspotCollider(on);
		}
	}

	public static void EnterGameState(GameState gameState)
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.EnterGameState(gameState);
		}
	}

	public static void SetOutlineColor()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.SetOutlineColor();
		}
	}

	public static void DisableMeshes()
	{
		foreach (Checkpoint checkpoint in m_Checkpoints)
		{
			checkpoint.DisableMeshes();
		}
	}

	private static Checkpoint GetClosestToWorldPos(Vector3 worldPos, List<Checkpoint> checkpoints)
	{
		Checkpoint result = null;
		float num = float.MaxValue;
		foreach (Checkpoint checkpoint in checkpoints)
		{
			float num2 = Vector2.Distance(checkpoint.transform.position, worldPos);
			if (num2 < num)
			{
				result = checkpoint;
				num = num2;
			}
		}
		return result;
	}
}
