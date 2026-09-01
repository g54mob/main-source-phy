using System;
using System.Collections.Generic;
using Poly.Collide;
using UnityEngine;
using Vectrosity;

public class BuildZones
{
	public static List<BuildZone> m_BuildZones = new List<BuildZone>();

	public static Vector2 DEFAULT_SIZE = new Vector2(12f, 4f);

	public static float MIN_WIDTH = 0.25f;

	public static float MIN_WIDTH_SLIDER = 1f;

	public static float MAX_WIDTH_SLIDER = 60f;

	public static float MAX_WIDTH = 350f;

	public static float MIN_HEIGHT = 0.25f;

	public static float MIN_HEIGHT_SLIDER = 1f;

	public static float MAX_HEIGHT_SLIDER = 60f;

	public static float MAX_HEIGHT = 250f;

	public static BuildZone Create(GameObject prefab, Vector2 pos, Vector2 size)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab, pos, Quaternion.identity);
		if (!gameObject)
		{
			return null;
		}
		BuildZone component = gameObject.GetComponent<BuildZone>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		component.SetBounds(pos, size);
		m_BuildZones.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			DestroyBuildZone(buildZone);
		}
		m_BuildZones.Clear();
	}

	public static void DestroyBuildZone(BuildZone buildZone)
	{
		buildZone.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(buildZone.gameObject);
	}

	public static int GetActiveCount()
	{
		int num = 0;
		foreach (BuildZone buildZone in m_BuildZones)
		{
			if (buildZone.gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		return num;
	}

	public static void DisableOutlines()
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			buildZone.DisableOutline();
		}
	}

	public static void EnableSpriteRendering(bool enabled)
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			buildZone.EnableSpriteRendering(enabled);
		}
	}

	public static void UpdateOutlines()
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			buildZone.UpdateOutline();
		}
	}

	public static bool OverlapsPolygonShape(PolygonShape shape)
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			if (buildZone.OverlapsPolygonShape(shape))
			{
				return true;
			}
		}
		return false;
	}

	public static BuildZone GetClosestThatContainPoint(Vector2 pos)
	{
		BuildZone result = null;
		float num = float.MaxValue;
		foreach (BuildZone buildZone in m_BuildZones)
		{
			if (buildZone.Contains(pos.x, pos.y))
			{
				float num2 = Vector2.Distance(pos, buildZone.GetPosition());
				if (num2 < num)
				{
					num = num2;
					result = buildZone;
				}
			}
		}
		return result;
	}

	public static List<BuildZoneProxy> Serialize()
	{
		List<BuildZoneProxy> list = new List<BuildZoneProxy>();
		foreach (BuildZone buildZone in m_BuildZones)
		{
			list.Add(new BuildZoneProxy(buildZone));
		}
		return list;
	}

	public static void Deserialize(List<BuildZoneProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (BuildZoneProxy proxy in proxies)
		{
			CreateBuildZoneFromProxy(proxy);
		}
	}

	public static BuildZone CreateBuildZoneFromProxy(BuildZoneProxy proxy)
	{
		BuildZone buildZone = Create(GetPrefabForType(proxy.m_BuildZoneType), proxy.m_Pos, proxy.m_Size);
		if ((bool)buildZone)
		{
			ApplyProxyToBuildZone(buildZone, proxy);
		}
		return buildZone;
	}

	public static void ApplyProxyToBuildZone(BuildZone buildZone, BuildZoneProxy proxy)
	{
		buildZone.SetBounds(proxy.m_Pos, proxy.m_Size);
		buildZone.m_RotationDegrees = proxy.m_RotationDegrees;
		buildZone.m_LockPosition = proxy.m_LockPosition;
		buildZone.m_Type = proxy.m_BuildZoneType;
		if (buildZone.m_Type == BuildZoneType.TRIANGLE)
		{
			buildZone.m_VertsLocalSpace = new Vector3[proxy.m_Verts.Length];
			Array.Copy(proxy.m_Verts, buildZone.m_VertsLocalSpace, proxy.m_Verts.Length);
			buildZone.m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		else
		{
			buildZone.GenerateRectangleVerts(proxy.m_Size);
		}
		buildZone.transform.Rotate(buildZone.transform.forward, 0f - buildZone.m_RotationDegrees, Space.World);
		buildZone.RecalculateGridOffset();
	}

	public static bool ContainsBridgePillar(Vector3 anchorPos, VectorLine vectorLine)
	{
		if (Game.InSandboxGodMode())
		{
			return true;
		}
		if (m_BuildZones.Count == 0)
		{
			return true;
		}
		if (!ContainsJoint(anchorPos))
		{
			return false;
		}
		for (int i = 0; i < vectorLine.points3.Count; i++)
		{
			bool flag = false;
			foreach (BuildZone buildZone in m_BuildZones)
			{
				if (buildZone.Contains(vectorLine.points3[i].x, vectorLine.points3[i].y))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return false;
			}
		}
		float num = GameGrid.m_Spacing / 2f;
		for (int j = 0; j < vectorLine.points3.Count - 1; j++)
		{
			Vector3 vector = vectorLine.points3[j];
			Vector3 vector2 = vectorLine.points3[j + 1];
			BuildZone buildZoneContaining = GetBuildZoneContaining(vector);
			BuildZone buildZoneContaining2 = GetBuildZoneContaining(vector2);
			if (buildZoneContaining == null || buildZoneContaining2 == null)
			{
				return false;
			}
			if (buildZoneContaining == buildZoneContaining2)
			{
				continue;
			}
			int num2 = Mathf.RoundToInt(Vector3.Distance(vector, vector2) / num);
			for (int k = 1; k <= num2; k++)
			{
				Vector3 vector3 = Vector3.Lerp(vector, vector2, Mathf.Clamp01((float)k / (float)num2));
				bool flag2 = false;
				foreach (BuildZone buildZone2 in m_BuildZones)
				{
					if (buildZone2.Contains(vector3.x, vector3.y))
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					return false;
				}
			}
		}
		return true;
	}

	public static BuildZone GetBuildZoneContaining(Vector3 pos)
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			if (buildZone.Contains(pos.x, pos.y))
			{
				return buildZone;
			}
		}
		return null;
	}

	public static bool ContainsJoint(Vector3 pos)
	{
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (Game.InSandboxGodMode())
		{
			return true;
		}
		if (m_BuildZones.Count == 0)
		{
			return true;
		}
		return ContainsPoint(pos);
	}

	public static bool ContainsPoint(Vector3 pos)
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			if (buildZone.Contains(pos.x, pos.y))
			{
				return true;
			}
		}
		return false;
	}

	public static bool ContainsEdge(Vector3 start, Vector3 end)
	{
		if (BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
		{
			return true;
		}
		if (Game.InSandboxGodMode())
		{
			return true;
		}
		if (m_BuildZones.Count == 0)
		{
			return true;
		}
		BuildZone buildZoneContaining = GetBuildZoneContaining(start);
		BuildZone buildZoneContaining2 = GetBuildZoneContaining(end);
		if (buildZoneContaining == null || buildZoneContaining2 == null)
		{
			return false;
		}
		if (buildZoneContaining == buildZoneContaining2)
		{
			return true;
		}
		float num = GameSettings.NodeRadius() / 2f;
		float magnitude = (end - start).magnitude;
		Vector3 normalized = (end - start).normalized;
		for (float num2 = 0f; num2 <= magnitude; num2 += num)
		{
			if (!ContainsJoint(start + normalized * num2))
			{
				return false;
			}
		}
		return true;
	}

	public static void EnterSandboxMode()
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			buildZone.EnterSandboxMode();
		}
	}

	public static void EnterBuildMode()
	{
		foreach (BuildZone buildZone in m_BuildZones)
		{
			buildZone.EnterBuildMode();
		}
	}

	public static GameObject GetPrefabForType(BuildZoneType type)
	{
		switch (type)
		{
		case BuildZoneType.RECTANGLE:
			return Prefabs.m_Instance.m_BuildZoneRect;
		case BuildZoneType.TRIANGLE:
			return Prefabs.m_Instance.m_BuildZoneTriangle;
		default:
			Debug.LogError($"Unexpected BuildZonetype '{type}'");
			return null;
		}
	}

	public static bool IsEditingBuildZone(BuildZone buildZone)
	{
		if (SandboxSelectionSet.IsSelected(buildZone.m_SandboxItem) && GameUI.m_Instance.m_SandboxEditBuildZone.gameObject.activeInHierarchy && GameUI.m_Instance.m_SandboxEditBuildZone.IsEditing())
		{
			return true;
		}
		return false;
	}
}
