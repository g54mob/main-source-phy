using System.Collections.Generic;
using Poly.Physics;
using UnityEngine;

public class BridgeRopes
{
	public static List<BridgeRope> m_BridgeRopes = new List<BridgeRope>();

	private static GameObject m_RopesContainer;

	public static void Add(Rope rope)
	{
		rope.visualize = false;
		BridgeEdge bridgeEdge = ((rope.userData != null) ? ((BridgeEdge)rope.userData) : ((BridgeEdge)rope.edge.userData));
		GameObject linkPrefabFromMaterial = BridgeMaterials.GetLinkPrefabFromMaterial(bridgeEdge.m_Material.m_MaterialType);
		m_BridgeRopes.Add(new BridgeRope(rope, bridgeEdge, linkPrefabFromMaterial));
		bridgeEdge.SetStressColor(0f);
	}

	public static void Remove(Rope rope)
	{
		BridgeRope bridgeRope = FindBridgeRopeWithPhysicsRope(rope);
		if (bridgeRope != null)
		{
			bridgeRope.Destroy();
			if (m_BridgeRopes.Contains(bridgeRope))
			{
				m_BridgeRopes.Remove(bridgeRope);
			}
		}
	}

	public static void UpdateManual()
	{
		for (int i = 0; i < m_BridgeRopes.Count; i++)
		{
			m_BridgeRopes[i].UpdateManual();
		}
	}

	public static void FixedUpdateManual()
	{
		for (int i = 0; i < m_BridgeRopes.Count; i++)
		{
			m_BridgeRopes[i].FixedUpdateManual();
		}
	}

	public static void DestroyAll()
	{
		foreach (BridgeRope bridgeRope in m_BridgeRopes)
		{
			bridgeRope.Destroy();
		}
		m_BridgeRopes.Clear();
	}

	public static Transform GetRopesContainerTransform()
	{
		if (!m_RopesContainer)
		{
			m_RopesContainer = new GameObject("CreatedRopes");
		}
		return m_RopesContainer.transform;
	}

	public static void SetStressColorForEdge(BridgeEdge edge, Color stressColor)
	{
		foreach (BridgeRope bridgeRope in m_BridgeRopes)
		{
			if (bridgeRope.m_ParentEdge == edge)
			{
				bridgeRope.SetStressColor(stressColor);
			}
		}
	}

	public static void Desaturate(BridgeEdge edge, bool desaturate)
	{
		foreach (BridgeRope bridgeRope in m_BridgeRopes)
		{
			if (bridgeRope.m_ParentEdge == edge)
			{
				bridgeRope.Desaturate(desaturate);
			}
		}
	}

	public static void DisableRopeForEdge(BridgeEdge edge)
	{
		foreach (BridgeRope bridgeRope in m_BridgeRopes)
		{
			if (bridgeRope.m_ParentEdge == edge)
			{
				bridgeRope.ClearLinksAndDisable();
				break;
			}
		}
	}

	private static BridgeRope FindBridgeRopeWithPhysicsRope(Rope rope)
	{
		foreach (BridgeRope bridgeRope in m_BridgeRopes)
		{
			if (bridgeRope.m_PhysicsRope == rope)
			{
				return bridgeRope;
			}
		}
		return null;
	}
}
