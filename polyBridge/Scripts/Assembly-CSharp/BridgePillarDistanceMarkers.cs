using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BridgePillarDistanceMarkers
{
	private static List<BridgePillarDistanceMarker> m_Markers = new List<BridgePillarDistanceMarker>();

	private static List<BridgePillarDistanceMarker> m_MarkersPool = new List<BridgePillarDistanceMarker>();

	public static string ENDCAP_NAME = "Arrow";

	public static void Init()
	{
		VectorLine.SetEndCap(ENDCAP_NAME, EndCap.Mirror, -1f, GameUI.m_Instance.m_ChalkLine2D, GameUI.m_Instance.m_ChalkArrow2D);
	}

	public static void Show(HashSet<BridgePillar> bridgePillars)
	{
		for (int num = m_Markers.Count - 1; num >= 0; num--)
		{
			BridgePillarDistanceMarker bridgePillarDistanceMarker = m_Markers[num];
			bridgePillarDistanceMarker.Hide(hide: true);
			m_MarkersPool.Add(bridgePillarDistanceMarker);
		}
		m_Markers.Clear();
		foreach (BridgePillar bridgePillar in bridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				ShowMarkers(bridgePillar.transform.position + Vector3.up);
			}
		}
		if (BridgePillarPlacement.InPlacementMode())
		{
			ShowMarkers(BridgePillarPlacement.GetPlacementPos() + Vector3.up);
		}
		ClipboardManager.ShowBridgePillarMarkers();
	}

	public static void HideAll(bool hide)
	{
		foreach (BridgePillarDistanceMarker marker in m_Markers)
		{
			marker.Hide(hide);
		}
	}

	public static void ShowMarkers(Vector3 origin)
	{
		if (Physics.Raycast(origin, Vector3.left, out var hitInfo, float.MaxValue, Utils.TERRAIN_LAYER_MASK | Utils.BRIDGE_PILLAR_LAYER_MASK))
		{
			if (hitInfo.collider.gameObject.layer == Utils.BRIDGE_PILLAR_LAYER)
			{
				ShowMarker(origin, new Vector3(hitInfo.collider.transform.position.x, 1f, 0f), endIsTerrain: false);
			}
			else
			{
				ShowMarker(origin, new Vector3(GetTerrainCollisionX(hitInfo.point, hitInfo.collider.transform.parent.GetComponent<TerrainIsland>()), 1f, 0f), endIsTerrain: true);
			}
		}
		if (Physics.Raycast(origin, Vector3.right, out var hitInfo2, float.MaxValue, Utils.TERRAIN_LAYER_MASK | Utils.BRIDGE_PILLAR_LAYER_MASK))
		{
			if (hitInfo2.collider.gameObject.layer == Utils.BRIDGE_PILLAR_LAYER)
			{
				ShowMarker(origin, new Vector3(hitInfo2.collider.transform.position.x, 1f, 0f), endIsTerrain: false);
			}
			else
			{
				ShowMarker(origin, new Vector3(GetTerrainCollisionX(hitInfo2.point, hitInfo2.collider.transform.parent.GetComponent<TerrainIsland>()), 1f, 0f), endIsTerrain: true);
			}
		}
	}

	public static void ShowMarker(Vector3 start, Vector3 end, bool endIsTerrain)
	{
		if (!MarkerPositonTaken((start + end) / 2f))
		{
			BridgePillarDistanceMarker marker = GetMarker();
			marker.Hide(hide: false);
			marker.UpdateManual(start, end, startIsTerrain: false, endIsTerrain);
			m_Markers.Add(marker);
		}
	}

	private static bool MarkerPositonTaken(Vector3 pos)
	{
		foreach (BridgePillarDistanceMarker marker in m_Markers)
		{
			if (Mathf.Approximately(Vector3.Distance(marker.m_Pos, pos), 0f))
			{
				return true;
			}
		}
		return false;
	}

	private static BridgePillarDistanceMarker GetMarker()
	{
		if (m_MarkersPool.Count > 0)
		{
			BridgePillarDistanceMarker bridgePillarDistanceMarker = m_MarkersPool[m_MarkersPool.Count - 1];
			m_MarkersPool.Remove(bridgePillarDistanceMarker);
			return bridgePillarDistanceMarker;
		}
		return new BridgePillarDistanceMarker();
	}

	private static float GetTerrainCollisionX(Vector3 hitPoint, TerrainIsland terrain)
	{
		if (terrain == null)
		{
			return 0f;
		}
		if (terrain.m_TerrainIslandType == TerrainIslandType.Bookend)
		{
			return terrain.transform.position.x;
		}
		if (hitPoint.x > terrain.transform.position.x)
		{
			return terrain.transform.position.x + terrain.m_BoxCollider.size.x / 2f;
		}
		return terrain.transform.position.x - terrain.m_BoxCollider.size.x / 2f;
	}
}
