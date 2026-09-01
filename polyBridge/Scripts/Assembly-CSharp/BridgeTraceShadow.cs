using System.Collections.Generic;
using UnityEngine;

public class BridgeTraceShadow
{
	public static List<ClipboardJoint> m_Joints = new List<ClipboardJoint>();

	private static List<GameObject> m_JointsPool = new List<GameObject>();

	private static GameObject m_BridgeTraceShadowContainer;

	public static void OnLayoutLoaded()
	{
		Clear();
	}

	public static void Clear()
	{
		for (int i = 0; i < m_Joints.Count; i++)
		{
			ReturnJointToPool(m_Joints[i]);
		}
		m_Joints.Clear();
	}

	public static bool IsShowing()
	{
		return m_Joints.Count > 0;
	}

	public static void Show(List<Vector3> positions, BridgeMaterialType bridgeMaterialType)
	{
		CreateJoints(positions);
		_ = m_Joints.Count;
	}

	public static ClipboardJoint AddJoint(Vector3 pos)
	{
		GameObject jointFromPool = GetJointFromPool();
		jointFromPool.transform.position = new Vector3(pos.x, pos.y, 0f);
		ClipboardJoint component = jointFromPool.GetComponent<ClipboardJoint>();
		m_Joints.Add(component);
		return component;
	}

	private static Transform GetBridgeTraceShadowContainer()
	{
		if (!m_BridgeTraceShadowContainer)
		{
			m_BridgeTraceShadowContainer = new GameObject("BridgeTraceShadow");
		}
		return m_BridgeTraceShadowContainer.transform;
	}

	private static void CreateJoints(List<Vector3> positions)
	{
		foreach (Vector3 position in positions)
		{
			AddJoint(position);
		}
	}

	private static GameObject GetJointFromPool()
	{
		if (m_JointsPool.Count == 0)
		{
			return Object.Instantiate(Prefabs.m_Instance.m_ShadowJoint, GetBridgeTraceShadowContainer());
		}
		GameObject gameObject = m_JointsPool[m_JointsPool.Count - 1];
		m_JointsPool.RemoveAt(m_JointsPool.Count - 1);
		gameObject.SetActive(value: true);
		return gameObject;
	}

	private static void ReturnJointToPool(ClipboardJoint joint)
	{
		joint.gameObject.SetActive(value: false);
		m_JointsPool.Add(joint.gameObject);
	}
}
