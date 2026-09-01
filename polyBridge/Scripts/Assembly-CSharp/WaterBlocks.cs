using System;
using System.Collections.Generic;
using System.Linq;
using Poly.Base;
using Poly.Game;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

public class WaterBlocks
{
	public static List<WaterBlock> m_WaterBlocks = new List<WaterBlock>();

	public static float MIN_HEIGHT = 0.5f;

	public static float MIN_HEIGHT_SLIDER = 0.5f;

	public static float MIN_WIDTH = 0.05f;

	public static float DEFAULT_WIDTH = 12f;

	public static float DEFAULT_HEIGHT = 3f;

	public static float DEFAULT_WAVE_HEIGHT = 0.1f;

	public static float MAX_DISTANCE_BELOW_TERRAIN = 0.5f;

	private static List<TerrainIsland> m_TempTerrainList = new List<TerrainIsland>();

	public static WaterBlock CreateWaterBlock(GameObject prefab)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
		if (!gameObject)
		{
			return null;
		}
		WaterBlock component = gameObject.GetComponent<WaterBlock>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		return component;
	}

	public static WaterBlock CreateDynamicWaterBlock(int index, TerrainIsland edgeStartTerrain, float startX, float endX, float scaleZ, float offsetZ)
	{
		WaterBlock waterBlock = null;
		float num = DEFAULT_HEIGHT;
		if (index < m_WaterBlocks.Count)
		{
			waterBlock = m_WaterBlocks[index];
			waterBlock.gameObject.SetActive(value: true);
			num = ((!Mathf.Approximately(edgeStartTerrain.m_RightEdgeWaterHeight, 0f)) ? edgeStartTerrain.m_RightEdgeWaterHeight : waterBlock.m_Height);
		}
		else
		{
			waterBlock = CreateWaterBlock(Theme.m_Instance.m_ThemeStub.m_WaterPrefab);
			if ((bool)waterBlock)
			{
				edgeStartTerrain.m_RightEdgeWaterHeight = DEFAULT_HEIGHT;
			}
		}
		if ((bool)waterBlock)
		{
			waterBlock.transform.position = new Vector3((startX + endX) / 2f, num / 2f, 0f);
			waterBlock.m_Width = Mathf.Abs(endX - startX);
			waterBlock.m_Height = num;
			if (!Mathf.Approximately(scaleZ, 0f))
			{
				waterBlock.SetScaleZ(scaleZ);
			}
			if (!Mathf.Approximately(offsetZ, 0f))
			{
				waterBlock.SetOffsetZ(offsetZ);
			}
		}
		return waterBlock;
	}

	public static float GetHeight()
	{
		if (m_WaterBlocks.Count <= 0)
		{
			return 0f;
		}
		return m_WaterBlocks[0].m_Height;
	}

	public static float GetMaxHeight()
	{
		if (m_WaterBlocks.Count <= 0)
		{
			return 0f;
		}
		return m_WaterBlocks[0].GetMaxHeight();
	}

	public static float GetCombinedWidth()
	{
		if (m_WaterBlocks.Count <= 0)
		{
			return 0f;
		}
		return m_WaterBlocks[0].m_Width;
	}

	public static void RebuildMesh()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.RebuildMesh();
			}
		}
	}

	public static void EnableMeshRenderers(bool enable)
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.EnableMeshRenderers(enable);
			}
		}
	}

	public static void DestroyWaterBlock(WaterBlock waterBlock)
	{
		waterBlock.gameObject.SetActive(value: false);
		UnityEngine.Object.Destroy(waterBlock.gameObject);
	}

	public static void DestroyAll()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				DestroyWaterBlock(waterBlock);
			}
		}
		m_WaterBlocks.Clear();
	}

	public static void DisableAll()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.gameObject.SetActive(value: false);
			}
		}
	}

	public static void UpdateManual()
	{
		DisableAll();
		CreateWaterBlock();
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock && waterBlock.gameObject.activeInHierarchy)
			{
				waterBlock.RefreshPosition();
			}
		}
	}

	public static float GetWaterHeightForTerrainRightEdge(TerrainIsland terrain)
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock && waterBlock.gameObject.activeInHierarchy && waterBlock.m_LeftTerrain == terrain)
			{
				return waterBlock.m_Height;
			}
		}
		return 0f;
	}

	public static WaterBlock PositionInWater(Vector3 pos)
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock && waterBlock.gameObject.activeInHierarchy && waterBlock.PositionInWater(pos))
			{
				return waterBlock;
			}
		}
		return null;
	}

	public static void DisableWaves()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.SetWaveHeight(0f);
			}
		}
	}

	public static void EnableWaves()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.SetWaveHeight(DEFAULT_WAVE_HEIGHT);
			}
		}
	}

	public static void StartSimulation()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.StartSimulation();
			}
		}
	}

	public static void CreateWave(float x, float force)
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.CreateSplash(x, force);
			}
		}
	}

	public static WaterBlock GetWaterBlockThatIntersectsVerticalLine(float x)
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock && waterBlock.m_BoxCollider.bounds.Contains(new Vector3(x, 0.05f, 0f)))
			{
				return waterBlock;
			}
		}
		return null;
	}

	public static List<WaterBlockProxy> Serialize()
	{
		List<WaterBlockProxy> list = new List<WaterBlockProxy>();
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock && waterBlock.gameObject.activeInHierarchy)
			{
				list.Add(new WaterBlockProxy(waterBlock));
			}
		}
		return list;
	}

	public static void Deserialize(List<WaterBlockProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		DestroyAll();
		foreach (WaterBlockProxy proxy in proxies)
		{
			CreateWaterBlockFromProxy(proxy);
		}
	}

	public static WaterBlock CreateWaterBlockFromProxy(WaterBlockProxy proxy)
	{
		WaterBlock waterBlock = CreateWaterBlock(Theme.m_Instance.m_ThemeStub.m_WaterPrefab);
		if ((bool)waterBlock)
		{
			ApplyProxyToWaterBlock(waterBlock, proxy);
		}
		return waterBlock;
	}

	public static void ApplyProxyToWaterBlock(WaterBlock waterBlock, WaterBlockProxy proxy)
	{
		waterBlock.transform.position = proxy.m_Pos;
		waterBlock.m_Height = proxy.m_Height;
		waterBlock.m_Width = proxy.m_Width;
		waterBlock.m_LockPosition = proxy.m_LockPosition;
		waterBlock.RefreshPosition();
		waterBlock.RebuildMesh();
	}

	public static void Hide(bool hidden)
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.m_SurfaceMeshRenderer.enabled = !hidden;
				waterBlock.m_SidesMeshRenderer.enabled = !hidden;
				waterBlock.m_FloorMeshRenderer.enabled = !hidden;
			}
		}
	}

	public static void RefreshScale()
	{
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				waterBlock.RefreshScale();
			}
		}
	}

	public static void AddToSimulation()
	{
		FastAabbTrigger[] array = m_WaterBlocks.Select((WaterBlock block) => new FastAabbTrigger(block.m_BoxCollider.bounds)).ToArray();
		Array.Sort(array);
		World instance = SingletonBehaviour<World>.instance;
		float num = 0.176f + 0.5f * instance.settings.collisionTolerance;
		FastAabbTrigger[] array2 = array;
		foreach (FastAabbTrigger fastAabbTrigger in array2)
		{
			fastAabbTrigger.bounds.max.y -= num;
			fastAabbTrigger.layer = Layer.WaterBlock_Trigger;
			fastAabbTrigger.nodeOverlapCallback = BridgeUnderWater.Add;
			fastAabbTrigger.bodyOverlapCallback = BridgeUnderWater.Add;
			instance.AddFastTrigger(fastAabbTrigger);
		}
		Bounds bounds = default(Bounds);
		bounds.Encapsulate(GetBounds());
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			bounds.Encapsulate(terrain.m_BoxCollider.bounds);
		}
		Bounds2 bounds2 = bounds;
		bounds2.max.y = -2f * num;
		bounds2.min.y = instance.bounds.min.y;
		FastAabbTrigger fastAabbTrigger2 = new FastAabbTrigger(bounds2);
		fastAabbTrigger2.layer = Layer.WaterBlock_Trigger;
		fastAabbTrigger2.nodeOverlapCallback = BridgeUnderWater.Add;
		fastAabbTrigger2.bodyOverlapCallback = BridgeUnderWater.Add;
		instance.AddFastTrigger(fastAabbTrigger2);
	}

	public static float GetNorthEdgeZ()
	{
		if (m_WaterBlocks.Count == 0)
		{
			return TerrainIslands.GetNorthEdgeZ();
		}
		return m_WaterBlocks[0].m_SurfaceMeshRenderer.bounds.center.z + m_WaterBlocks[0].m_SurfaceMeshRenderer.bounds.size.z / 2f - 0.1f;
	}

	public static float GetSouthEdgeZ()
	{
		if (m_WaterBlocks.Count == 0)
		{
			return TerrainIslands.GetSouthEdgeZ();
		}
		return m_WaterBlocks[0].m_SurfaceMeshRenderer.bounds.center.z - m_WaterBlocks[0].m_SurfaceMeshRenderer.bounds.size.z / 2f + 0.1f;
	}

	public static SandboxItem GetSandboxItem()
	{
		if (m_WaterBlocks.Count == 0)
		{
			return null;
		}
		return m_WaterBlocks[0].m_SandboxItem;
	}

	public static Bounds GetBounds()
	{
		Bounds result = new Bounds(Vector3.zero, Vector3.one);
		foreach (WaterBlock waterBlock in m_WaterBlocks)
		{
			result.Encapsulate(waterBlock.m_BoxCollider.bounds);
		}
		return result;
	}

	private static void CreateWaterBlock()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
		float startX = GetStartX(leftTerrain, Theme.m_Instance.m_ThemeStub.m_LeftEdgeOffsetX);
		float endX = GetEndX(rightTerrain, Theme.m_Instance.m_ThemeStub.m_RightEdgeOffsetX);
		WaterBlock waterBlock = CreateDynamicWaterBlock(0, leftTerrain, startX, endX, Theme.m_Instance.m_ThemeStub.m_WaterScaleZ, Theme.m_Instance.m_ThemeStub.m_WaterOffsetZ);
		if ((bool)waterBlock)
		{
			waterBlock.m_LeftTerrain = leftTerrain;
			waterBlock.m_RightTerrain = rightTerrain;
		}
	}

	private static float GetStartX(TerrainIsland leftIsland, float offset)
	{
		if (Mathf.Approximately(offset, 0f))
		{
			return leftIsland.transform.position.x - (leftIsland.m_MeshRenderer.bounds.size.x + leftIsland.m_MeshRenderer.bounds.size.x / 4f);
		}
		return leftIsland.transform.position.x - offset;
	}

	private static float GetEndX(TerrainIsland rightIsland, float offset)
	{
		if (Mathf.Approximately(offset, 0f))
		{
			return rightIsland.transform.position.x + (rightIsland.m_MeshRenderer.bounds.size.x + rightIsland.m_MeshRenderer.bounds.size.x / 4f);
		}
		return rightIsland.transform.position.x + offset;
	}
}
