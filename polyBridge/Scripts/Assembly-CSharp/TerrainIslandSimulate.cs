using Poly.Collide.Unity;
using UnityEngine;

public class TerrainIslandSimulate
{
	private static GameObject m_TerrainNodesContainer;

	public static void AddToSimulation(TerrainIsland terrain)
	{
		TerrainCollisionInfo componentInChildren = terrain.GetComponentInChildren<TerrainCollisionInfo>();
		if ((bool)componentInChildren)
		{
			componentInChildren.OnAddedToWorld(terrain.m_Flipped).transform.parent = GetTerrainNodesContainerTransform();
			return;
		}
		PlaceableCollisionInfo componentInChildren2 = terrain.GetComponentInChildren<PlaceableCollisionInfo>();
		if ((bool)componentInChildren2)
		{
			componentInChildren2.isTerrainIsland = true;
			componentInChildren2.isMiddleIsland = terrain.m_TerrainIslandType == TerrainIslandType.Middle;
			componentInChildren2.OnAddedToWorld();
		}
	}

	private static Transform GetTerrainNodesContainerTransform()
	{
		if (!m_TerrainNodesContainer)
		{
			m_TerrainNodesContainer = new GameObject("SimTerrain");
		}
		return m_TerrainNodesContainer.transform;
	}
}
