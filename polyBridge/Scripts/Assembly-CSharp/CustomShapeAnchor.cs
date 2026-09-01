using System;
using UnityEngine;

public class CustomShapeAnchor : MonoBehaviour
{
	public SpriteRenderer m_SpriteRenderer;

	public BoxCollider m_BoxCollider;

	[NonSerialized]
	public string m_BridgeJointGuid;

	public void SetColor(Color color)
	{
		m_SpriteRenderer.color = color;
	}

	public void SetBridgeJointGuid(string guid)
	{
		m_BridgeJointGuid = guid;
	}

	public void InverseScale(Vector3 scale)
	{
		base.transform.localScale = new Vector3(1f / scale.x, 1f / scale.y, 1f / scale.z);
	}
}
