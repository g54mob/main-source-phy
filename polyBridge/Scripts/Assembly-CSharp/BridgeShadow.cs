using System.Collections.Generic;
using UnityEngine;

public class BridgeShadow
{
	private static List<ClipboardEdge> m_Edges = new List<ClipboardEdge>();

	private static List<ClipboardJoint> m_Joints = new List<ClipboardJoint>();

	private static List<ClipboardBridgePillar> m_BridgePillars = new List<ClipboardBridgePillar>();

	private static Dictionary<string, ClipboardJoint> m_JointMap = new Dictionary<string, ClipboardJoint>();

	private static GameObject m_BridgeShadowContainer;

	public static void OnLayoutLoaded()
	{
		Clear();
	}

	public static bool IsActive()
	{
		if (m_Joints.Count <= 0 && m_Edges.Count <= 0)
		{
			return m_BridgePillars.Count > 0;
		}
		return true;
	}

	public static void Clear()
	{
		for (int i = 0; i < m_Edges.Count; i++)
		{
			Object.Destroy(m_Edges[i].gameObject);
		}
		for (int j = 0; j < m_Joints.Count; j++)
		{
			Object.Destroy(m_Joints[j].gameObject);
		}
		for (int k = 0; k < m_BridgePillars.Count; k++)
		{
			Object.Destroy(m_BridgePillars[k].gameObject);
		}
		m_Edges.Clear();
		m_Joints.Clear();
		m_BridgePillars.Clear();
		m_JointMap.Clear();
	}

	public static void Show(BridgeSaveData bridgeSaveData)
	{
		if (bridgeSaveData == null)
		{
			return;
		}
		foreach (BridgeJointProxy bridgeJoint in bridgeSaveData.m_BridgeJoints)
		{
			AddJoint(bridgeJoint);
		}
		foreach (BridgeJointProxy anchor in bridgeSaveData.m_Anchors)
		{
			ClipboardJoint clipboardJoint = AddJoint(anchor);
			if (clipboardJoint != null)
			{
				clipboardJoint.gameObject.SetActive(value: false);
			}
		}
		foreach (BridgeEdgeProxy bridgeEdge in bridgeSaveData.m_BridgeEdges)
		{
			AddEdge(bridgeEdge);
		}
		foreach (BridgePillarProxy bridgePillar in bridgeSaveData.m_BridgePillars)
		{
			AddBridgePillar(bridgePillar);
		}
	}

	public static ClipboardJoint AddJoint(BridgeJointProxy bridgeJointProxy)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_ShadowJoint, GetBridgeShadowContainer());
		gameObject.transform.position = new Vector3(bridgeJointProxy.m_Pos.x, bridgeJointProxy.m_Pos.y, 0f);
		ClipboardJoint component = gameObject.GetComponent<ClipboardJoint>();
		if (bridgeJointProxy.m_IsSplit)
		{
			component.DrawAsSplitJoint();
		}
		m_Joints.Add(component);
		if (!m_JointMap.ContainsKey(bridgeJointProxy.m_Guid))
		{
			m_JointMap.Add(bridgeJointProxy.m_Guid, component);
		}
		return component;
	}

	public static ClipboardEdge AddEdge(BridgeEdgeProxy bridgeEdgeProxy)
	{
		GameObject gameObject = Object.Instantiate(GetShadowPrefabForEdge(bridgeEdgeProxy.m_Material), Vector3.zero, Quaternion.identity, GetBridgeShadowContainer());
		ClipboardJoint clipboardJointFromGuid = GetClipboardJointFromGuid(bridgeEdgeProxy.m_NodeA_Guid);
		ClipboardJoint clipboardJointFromGuid2 = GetClipboardJointFromGuid(bridgeEdgeProxy.m_NodeB_Guid);
		if (!clipboardJointFromGuid || !clipboardJointFromGuid2)
		{
			return null;
		}
		Vector3 position = clipboardJointFromGuid.transform.position;
		Vector3 position2 = clipboardJointFromGuid2.transform.position;
		Vector3 vector = position2 - position;
		gameObject.transform.position = (position + position2) / 2f;
		gameObject.transform.Translate(0f, 0f, 0.5f);
		gameObject.transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Atan2(vector.y, vector.x) * 57.29578f);
		gameObject.transform.localScale = new Vector3(vector.magnitude, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		ClipboardEdge component = gameObject.GetComponent<ClipboardEdge>();
		component.m_JointA = clipboardJointFromGuid;
		component.m_JointB = clipboardJointFromGuid2;
		component.m_BridgeMaterialType = bridgeEdgeProxy.m_Material;
		m_Edges.Add(component);
		return component;
	}

	public static void AddBridgePillar(BridgePillarProxy bridgePillarProxy)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_BridgePillarClipboard, Vector3.zero, Quaternion.identity, GetBridgeShadowContainer());
		gameObject.transform.localPosition = new Vector3(bridgePillarProxy.m_Pos.x, bridgePillarProxy.m_Pos.y, 0f);
		ClipboardBridgePillar component = gameObject.GetComponent<ClipboardBridgePillar>();
		component.SetTopHeightBasedOnTotalHeight(bridgePillarProxy.m_Height);
		component.m_Joint.gameObject.SetActive(value: true);
		component.m_Outline.Destroy();
		component.m_Outline = null;
		m_BridgePillars.Add(component);
	}

	public static ClipboardJoint GetClipboardJointFromGuid(string guid)
	{
		if (m_JointMap.ContainsKey(guid))
		{
			return m_JointMap[guid];
		}
		return null;
	}

	public static void Hide(bool hide)
	{
		GetBridgeShadowContainer().gameObject.SetActive(!hide);
	}

	public static bool PositionsMatchEdge(Vector3 A, Vector3 B)
	{
		float num = 0.01f;
		foreach (ClipboardEdge edge in m_Edges)
		{
			if (Vector2.Distance(edge.m_JointA.transform.position, A) < num && Vector2.Distance(edge.m_JointB.transform.position, B) < num)
			{
				return true;
			}
			if (Vector2.Distance(edge.m_JointA.transform.position, B) < num && Vector2.Distance(edge.m_JointB.transform.position, A) < num)
			{
				return true;
			}
		}
		return false;
	}

	public static bool PositionMatchesNode(Vector3 pos)
	{
		foreach (ClipboardJoint joint in m_Joints)
		{
			if (Utils.ApproximatelyEquals(pos, joint.transform.position))
			{
				return true;
			}
		}
		return false;
	}

	public static bool IsBuiltOver()
	{
		foreach (ClipboardEdge edge in m_Edges)
		{
			if (!BridgeEdges.EdgeExistsWithNodePositions(edge.m_JointA.transform.position, edge.m_JointB.transform.position, edge.m_BridgeMaterialType))
			{
				return false;
			}
		}
		return true;
	}

	public static GameObject GetShadowPrefabForEdge(BridgeMaterialType bridgeMaterialType)
	{
		switch (bridgeMaterialType)
		{
		case BridgeMaterialType.CABLE:
			return Prefabs.m_Instance.m_ShadowCable;
		case BridgeMaterialType.HYDRAULICS:
			return Prefabs.m_Instance.m_ShadowHydraulics;
		case BridgeMaterialType.REINFORCED_ROAD:
			return Prefabs.m_Instance.m_ShadowReinforcedRoad;
		case BridgeMaterialType.ROAD:
			return Prefabs.m_Instance.m_ShadowRoad;
		case BridgeMaterialType.ROPE:
			return Prefabs.m_Instance.m_ShadowRope;
		case BridgeMaterialType.SPRING:
			return Prefabs.m_Instance.m_ShadowSpring;
		case BridgeMaterialType.STEEL:
			return Prefabs.m_Instance.m_ShadowSteel;
		case BridgeMaterialType.WOOD:
			return Prefabs.m_Instance.m_ShadowWood;
		default:
			Debug.LogErrorFormat("Unexpected material {0} in GetShadowPrefabForEdge");
			return null;
		}
	}

	public static ClipboardJoint FindClosestJoint(Vector2 pos)
	{
		ClipboardJoint result = null;
		float num = float.MaxValue;
		foreach (ClipboardJoint joint in m_Joints)
		{
			float num2 = Vector2.Distance(pos, joint.transform.position);
			if (num2 < num)
			{
				result = joint;
				num = num2;
			}
		}
		return result;
	}

	private static Transform GetBridgeShadowContainer()
	{
		if (!m_BridgeShadowContainer)
		{
			m_BridgeShadowContainer = new GameObject("BridgeShadow");
		}
		return m_BridgeShadowContainer.transform;
	}
}
