using System.Collections.Generic;
using UnityEngine;

public class WaterRulers
{
	public static List<WaterRuler> m_WaterRulers = new List<WaterRuler>();

	private static List<TerrainIsland> m_TempTerrainList = new List<TerrainIsland>();

	public static void Disable()
	{
		foreach (WaterRuler waterRuler in m_WaterRulers)
		{
			waterRuler.Disable();
		}
	}

	public static void UpdateManual(float height)
	{
		Disable();
		if (Mathf.Approximately(TerrainIslands.DistanceBetweenBookends(), 0f) || GameStateSandbox.m_CameraInTransition)
		{
			return;
		}
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		if (!leftTerrain || !rightTerrain)
		{
			return;
		}
		float num = leftTerrain.transform.position.x;
		TerrainIsland edgeStartTerrain = leftTerrain;
		int num2 = 0;
		int num3 = 0;
		while (num3++ <= 100)
		{
			TerrainIsland furthestRightTerrainThatContainsPoint = GetFurthestRightTerrainThatContainsPoint(new Vector3(num, TerrainIslands.MIN_HEIGHT / 2f, 0f));
			if (furthestRightTerrainThatContainsPoint != null)
			{
				num = furthestRightTerrainThatContainsPoint.transform.position.x + furthestRightTerrainThatContainsPoint.GetBoxColliderWidth() / 2f + 0.001f;
				edgeStartTerrain = furthestRightTerrainThatContainsPoint;
				continue;
			}
			if (Physics.Raycast(new Vector3(num, TerrainIslands.MIN_HEIGHT / 2f, 0f), Vector3.right, out var hitInfo, float.MaxValue, Utils.TERRAIN_LAYER_MASK))
			{
				float terrainEdgeClosestToRaycastHit = GetTerrainEdgeClosestToRaycastHit(hitInfo);
				if (Mathf.Abs(num - terrainEdgeClosestToRaycastHit) > WaterBlocks.MIN_WIDTH && (bool)CreateWaterRuler(num2, edgeStartTerrain, num, terrainEdgeClosestToRaycastHit, height))
				{
					num2++;
				}
				TerrainIsland component = hitInfo.transform.parent.GetComponent<TerrainIsland>();
				if (component != null)
				{
					num = hitInfo.transform.position.x + component.GetBoxColliderWidth() / 2f + 0.001f;
					edgeStartTerrain = hitInfo.transform.GetComponentInParent<TerrainIsland>();
				}
			}
			if (!Mathf.Approximately(num, rightTerrain.transform.position.x) && !(num > rightTerrain.transform.position.x))
			{
				continue;
			}
			break;
		}
	}

	private static float GetTerrainEdgeClosestToRaycastHit(RaycastHit hit)
	{
		if (hit.collider == null || hit.collider.transform.parent == null)
		{
			return hit.point.x;
		}
		TerrainIsland component = hit.collider.transform.parent.GetComponent<TerrainIsland>();
		if (component == null)
		{
			return hit.point.x;
		}
		if (component.m_TerrainIslandType == TerrainIslandType.Bookend)
		{
			return component.transform.position.x;
		}
		if (hit.point.x > component.transform.position.x)
		{
			return component.transform.position.x + component.m_BoxCollider.bounds.extents.x;
		}
		return component.transform.position.x - component.m_BoxCollider.bounds.extents.x;
	}

	public static WaterRuler CreateWaterRuler(int index, TerrainIsland edgeStartTerrain, float startX, float endX, float height)
	{
		WaterRuler waterRuler = null;
		if (index < m_WaterRulers.Count)
		{
			waterRuler = m_WaterRulers[index];
			waterRuler.gameObject.SetActive(value: true);
		}
		else
		{
			waterRuler = InstantiateRuler();
			m_WaterRulers.Add(waterRuler);
		}
		if ((bool)waterRuler)
		{
			waterRuler.Enable();
			float width = endX - startX;
			float x = (startX + endX) / 2f;
			Vector3 worldPos = new Vector3(x, height, 0f);
			SandboxItem sandboxItem = WaterBlocks.GetSandboxItem();
			Color color = SandboxItems.GetDefaultOutlineColor(sandboxItem);
			if (SandboxSelectionSet.IsSelected(sandboxItem))
			{
				color = GameUI.m_Instance.m_OutlineSelectedColorSandbox;
			}
			else
			{
				bool num = SandboxItems.m_Hover == sandboxItem;
				bool flag = GroupSelect.IsActive() && sandboxItem.OverlapsRect(GroupSelect.GetRect());
				if (num || flag)
				{
					color = GameUI.m_Instance.m_OutlineHoverColorSandbox;
				}
			}
			waterRuler.UpdateManual(worldPos, width, height, color);
		}
		return waterRuler;
	}

	public static void RefreshAfterOrthographicSizeChange()
	{
		foreach (WaterRuler waterRuler in m_WaterRulers)
		{
			waterRuler.RefreshAfterOrthographicSizeChange();
		}
	}

	private static TerrainIsland GetFurthestRightTerrainThatContainsPoint(Vector3 pos)
	{
		m_TempTerrainList.Clear();
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy && terrain.m_TerrainIslandType == TerrainIslandType.Middle && terrain.m_BoxCollider.bounds.Contains(pos))
			{
				m_TempTerrainList.Add(terrain);
			}
		}
		float num = float.MinValue;
		TerrainIsland result = null;
		foreach (TerrainIsland tempTerrain in m_TempTerrainList)
		{
			if (tempTerrain.transform.position.x > num)
			{
				num = tempTerrain.transform.position.x;
				result = tempTerrain;
			}
		}
		return result;
	}

	private static WaterRuler InstantiateRuler()
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_WaterRuler, GameUI.m_Instance.m_RulerText.transform);
		if (!gameObject)
		{
			return null;
		}
		return gameObject.GetComponent<WaterRuler>();
	}
}
