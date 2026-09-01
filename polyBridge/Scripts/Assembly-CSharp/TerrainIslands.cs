using System.Collections.Generic;
using Poly.Collide;
using UnityEngine;

public class TerrainIslands
{
	public static List<TerrainIsland> m_Terrains = new List<TerrainIsland>();

	public static readonly float GRID_ALIGN_OFFSET = 0.1f;

	public static readonly float DEFAULT_HEIGHT = 10f + GRID_ALIGN_OFFSET;

	public static readonly float MIN_HEIGHT = 1f + GRID_ALIGN_OFFSET;

	public static readonly float MIN_HEIGHT_SLIDER = 1f + GRID_ALIGN_OFFSET;

	public static readonly float MAX_HEIGHT_SLIDER = 20f + GRID_ALIGN_OFFSET;

	public static readonly float MAX_HEIGHT = 150f + GRID_ALIGN_OFFSET;

	public static readonly float MAX_SEPARATION_X = 250f;

	public static bool m_Hide;

	private static float OVERLAP_EPSILON = 0.0001f;

	private static List<TerrainIsland> m_OverlappingTerrains = new List<TerrainIsland>();

	public static TerrainIsland CreateTerrain(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		TerrainIsland component = gameObject.GetComponent<TerrainIsland>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		m_Terrains.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.gameObject.SetActive(value: false);
			Object.Destroy(terrain.gameObject);
		}
		m_Terrains.Clear();
	}

	public static void DestroyTerrain(TerrainIsland terrain)
	{
		terrain.gameObject.SetActive(value: false);
		if (m_Terrains.Contains(terrain))
		{
			m_Terrains.Remove(terrain);
		}
		Object.Destroy(terrain.gameObject);
	}

	public static void AddToSimulation()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy)
			{
				TerrainIslandSimulate.AddToSimulation(terrain);
			}
		}
	}

	public static void DisableOutlines()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy)
			{
				terrain.DisableOutline();
			}
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy)
			{
				terrain.EnableMeshRendering();
			}
		}
	}

	public static void UpdateOutlines()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy)
			{
				terrain.UpdateOutline();
			}
		}
	}

	public static TerrainIsland GetRightTerrain()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend && terrain.m_Flipped)
			{
				return terrain;
			}
		}
		return null;
	}

	public static TerrainIsland GetLeftTerrain()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend && !terrain.m_Flipped)
			{
				return terrain;
			}
		}
		return null;
	}

	public static float DistanceBetweenBookends()
	{
		TerrainIsland leftTerrain = GetLeftTerrain();
		TerrainIsland rightTerrain = GetRightTerrain();
		if (!leftTerrain || !rightTerrain)
		{
			return float.MaxValue;
		}
		return Mathf.Abs(rightTerrain.transform.position.x - leftTerrain.transform.position.x);
	}

	public static int GetNumMiddleIslands()
	{
		int num = 0;
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy && terrain.m_TerrainIslandType == TerrainIslandType.Middle)
			{
				num++;
			}
		}
		return num;
	}

	public static bool OverlapsPolygonShape(PolygonShape shape)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.gameObject.activeInHierarchy && !terrain.m_Hidden && terrain.OverlapsPolygonShape(shape))
			{
				return true;
			}
		}
		return false;
	}

	public static List<TerrainIsland> GetTerrainsThatOverlapPolygonShape(PolygonShape shape)
	{
		m_OverlappingTerrains.Clear();
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.m_TerrainIslandType == TerrainIslandType.Middle && terrain.gameObject.activeInHierarchy && terrain.OverlapsPolygonShape(shape))
			{
				m_OverlappingTerrains.Add(terrain);
			}
		}
		foreach (TerrainIsland terrain2 in m_Terrains)
		{
			if (terrain2.m_TerrainIslandType == TerrainIslandType.Bookend && terrain2.gameObject.activeInHierarchy && terrain2.OverlapsPolygonShape(shape))
			{
				m_OverlappingTerrains.Add(terrain2);
			}
		}
		return m_OverlappingTerrains;
	}

	public static void UpdatePolygonShapes()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.UpdatePolygonShapes();
		}
	}

	public static Vector3 GetAveragePositionOfBookendSpawnPoints()
	{
		TerrainIsland leftTerrain = GetLeftTerrain();
		TerrainIsland rightTerrain = GetRightTerrain();
		if (!leftTerrain || !rightTerrain || !leftTerrain.m_SpawnPoint || !rightTerrain.m_SpawnPoint)
		{
			return Vector3.zero;
		}
		return (leftTerrain.m_SpawnPoint.transform.position + rightTerrain.m_SpawnPoint.transform.position) / 2f;
	}

	public static List<TerrainIslandProxy> Serialize()
	{
		List<TerrainIslandProxy> list = new List<TerrainIslandProxy>();
		foreach (TerrainIsland terrain in m_Terrains)
		{
			list.Add(new TerrainIslandProxy(terrain));
		}
		return list;
	}

	public static void Deserialize(List<TerrainIslandProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		DestroyBookends();
		foreach (TerrainIslandProxy proxy in proxies)
		{
			CreateTerrainFromProxy(proxy);
		}
	}

	public static TerrainIsland CreateTerrainFromProxy(TerrainIslandProxy proxy)
	{
		TerrainIsland terrainIsland = null;
		GameObject bestTerrainIslandPrefabMatch = Theme.m_Instance.GetBestTerrainIslandPrefabMatch(proxy.m_TerrainIslandType, proxy.m_VariantIndex);
		if (!bestTerrainIslandPrefabMatch)
		{
			return null;
		}
		terrainIsland = CreateTerrain(bestTerrainIslandPrefabMatch, proxy.m_Pos, Quaternion.identity);
		if ((bool)terrainIsland)
		{
			if (GameStateManager.GetState() == GameState.SANDBOX)
			{
				terrainIsland.ShrinkForSandboxMode(shrink: true);
			}
			ApplyProxyToTerrain(terrainIsland, proxy);
			if (Theme.m_Instance != null)
			{
				terrainIsland.UpdateShaderProperties(buildMode: false, CuttingPlanes.m_Instance.m_Floor);
			}
		}
		return terrainIsland;
	}

	public static void ApplyProxyToTerrain(TerrainIsland terrain, TerrainIslandProxy proxy)
	{
		terrain.transform.position = proxy.m_Pos;
		terrain.m_HeightAdded = proxy.m_HeightAdded;
		terrain.m_RightEdgeWaterHeight = proxy.m_RightEdgeWaterHeight;
		terrain.m_LockPosition = proxy.m_LockPosition;
		terrain.m_Hidden = proxy.m_Hidden;
		if (proxy.m_Flipped && !terrain.m_Flipped)
		{
			terrain.Flip();
		}
		terrain.SetHeight(proxy.m_Height);
	}

	public static void Hide(bool hide)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.HideAllMeshRenderers(hide);
		}
	}

	public static void HideSecondPassMeshRenderers(bool hide)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.HideSecondPassMeshRenderers(hide);
		}
	}

	public static float GetNorthEdgeZ()
	{
		if (m_Terrains.Count == 0)
		{
			return 0f;
		}
		return m_Terrains[0].GetNorthEdgeZ();
	}

	public static float GetSouthEdgeZ()
	{
		if (m_Terrains.Count == 0)
		{
			return 0f;
		}
		return m_Terrains[0].GetSouthEdgeZ();
	}

	public static void UpdateShaderProperties(bool buildMode, MeshRenderer plane)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.UpdateShaderProperties(buildMode, plane);
			terrain.UpdateStencilShaderProperties(buildMode, Theme.m_Instance.m_ThemeStub.m_BuildModeHoleColor);
		}
	}

	public static void UpdateWaterfallsInverseTimeScale(float timescale)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.UpdateWaterfallsInverseTimeScale(timescale);
		}
	}

	public static void MaybeAdjustTerrainVisualHeight()
	{
		TerrainIsland leftTerrain = GetLeftTerrain();
		TerrainIsland terrainIsland = TerrainAdjacentToEdge(leftTerrain.transform.position.x, leftTerrain);
		if ((bool)terrainIsland)
		{
			AdjustAdjacentTerrain(leftTerrain, terrainIsland);
			leftTerrain.m_SandboxItem.SetOutlineDirty(dirty: true);
			terrainIsland.m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		TerrainIsland rightTerrain = GetRightTerrain();
		terrainIsland = TerrainAdjacentToEdge(leftTerrain.transform.position.x, leftTerrain);
		if ((bool)terrainIsland)
		{
			AdjustAdjacentTerrain(rightTerrain, terrainIsland);
			rightTerrain.m_SandboxItem.SetOutlineDirty(dirty: true);
			terrainIsland.m_SandboxItem.SetOutlineDirty(dirty: true);
		}
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.m_TerrainIslandType == TerrainIslandType.Middle)
			{
				float num = terrain.m_BoxCollider.size.x / 2f;
				terrainIsland = TerrainAdjacentToEdge(terrain.transform.position.x - num, terrain);
				if ((bool)terrainIsland)
				{
					AdjustAdjacentTerrain(terrain, terrainIsland);
					terrain.m_SandboxItem.SetOutlineDirty(dirty: true);
					terrainIsland.m_SandboxItem.SetOutlineDirty(dirty: true);
				}
				terrainIsland = TerrainAdjacentToEdge(terrain.transform.position.x + num, terrain);
				if ((bool)terrainIsland)
				{
					AdjustAdjacentTerrain(terrain, terrainIsland);
					terrain.m_SandboxItem.SetOutlineDirty(dirty: true);
					terrainIsland.m_SandboxItem.SetOutlineDirty(dirty: true);
				}
			}
		}
	}

	public static void EnableCollisionMeshRenderer(bool on)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.EnableCollisionMeshRenderer(on);
		}
	}

	public static void ClearDisplayVariantTimer()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.ClearDisplayVariantTimer();
		}
	}

	public static void StartParticleSystems()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.StartParticleSystems();
		}
	}

	public static void StopParticleSystems()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.StopParticleSystems();
		}
	}

	public static void StartWaterFalls()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.StartWaterfalls();
		}
	}

	public static void StopWaterFalls()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.StopWaterfalls();
		}
	}

	public static void PauseParticleSystems(bool pause)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.PauseParticleSystems(pause);
		}
	}

	public static void PauseWaterfalls(bool pause)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.PauseWaterfalls(pause);
		}
	}

	public static void ShrinkForSandboxMode(bool shrink)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.ShrinkForSandboxMode(shrink);
		}
	}

	private static void AdjustAdjacentTerrain(TerrainIsland terrain, TerrainIsland adjacentTerrain)
	{
		if (Mathf.Approximately(terrain.GetHeight(), adjacentTerrain.GetHeight()))
		{
			adjacentTerrain.m_MeshRenderer.transform.localPosition = new Vector3(adjacentTerrain.m_MeshRenderer.transform.localPosition.x, Random.Range(-1E-05f, 1E-05f), adjacentTerrain.m_MeshRenderer.transform.localPosition.z);
		}
	}

	private static TerrainIsland TerrainAdjacentToEdge(float edgeX, TerrainIsland exclude)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain == exclude)
			{
				continue;
			}
			if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend)
			{
				if (Mathf.Abs(terrain.transform.position.x - edgeX) < OVERLAP_EPSILON)
				{
					return terrain;
				}
				continue;
			}
			float num = terrain.m_BoxCollider.size.x / 2f;
			if (Mathf.Abs(terrain.transform.position.x - num - edgeX) < OVERLAP_EPSILON || Mathf.Abs(terrain.transform.position.x + num - edgeX) < OVERLAP_EPSILON)
			{
				return terrain;
			}
		}
		return null;
	}

	public static void UndoAdjustmentForTerrainVisualHeight()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.m_MeshRenderer.transform.localPosition = new Vector3(terrain.m_MeshRenderer.transform.localPosition.x, 0f, terrain.m_MeshRenderer.transform.localPosition.z);
		}
	}

	public static void SetHeight(float height)
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			terrain.SetHeight(height);
		}
	}

	public static void SetActiveBasedOnHiddenFlag()
	{
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.m_Hidden && GameStateManager.GetState() != GameState.SANDBOX)
			{
				terrain.gameObject.SetActive(value: false);
			}
			else
			{
				terrain.gameObject.SetActive(value: true);
			}
		}
	}

	public static float GetMinHeight()
	{
		float num = float.MaxValue;
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.GetHeight() < num)
			{
				num = terrain.GetHeight();
			}
		}
		return num;
	}

	public static float GetMaxHeight()
	{
		float num = 0f;
		foreach (TerrainIsland terrain in m_Terrains)
		{
			if (terrain.GetHeight() > num)
			{
				num = terrain.GetHeight();
			}
		}
		return num;
	}

	public static List<int> GetMeshBottomVertIndicies(Mesh mesh)
	{
		List<int> list = new List<int>();
		Vector3[] vertices = mesh.vertices;
		float num = float.MaxValue;
		for (int i = 0; i < vertices.Length; i++)
		{
			if (vertices[i].y < num)
			{
				num = vertices[i].y;
			}
		}
		for (int j = 0; j < vertices.Length; j++)
		{
			if (vertices[j].y < num + 0.1f)
			{
				list.Add(j);
			}
		}
		return list;
	}

	private static void DestroyBookends()
	{
		TerrainIsland leftTerrain = GetLeftTerrain();
		if ((bool)leftTerrain)
		{
			DestroyTerrain(leftTerrain);
		}
		TerrainIsland rightTerrain = GetRightTerrain();
		if ((bool)rightTerrain)
		{
			DestroyTerrain(rightTerrain);
		}
	}
}
