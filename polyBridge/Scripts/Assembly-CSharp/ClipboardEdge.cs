using System;
using Poly.Extension;
using UnityEngine;

public class ClipboardEdge : MonoBehaviour
{
	[NonSerialized]
	public ClipboardJoint m_JointA;

	[NonSerialized]
	public ClipboardJoint m_JointB;

	[NonSerialized]
	public BridgeEdge m_SourceBridgeEdge;

	[NonSerialized]
	public BridgeEdge m_PastedBridgeEdge;

	[NonSerialized]
	public BridgeMaterialType m_BridgeMaterialType;

	private MeshRenderer m_MeshRenderer;

	public void Awake()
	{
		m_MeshRenderer = GetComponent<MeshRenderer>();
	}

	public void UpdateTransform()
	{
		Vector3 position = m_JointA.transform.position;
		Vector3 position2 = m_JointB.transform.position;
		Vector3 toDirection = position2 - position;
		float magnitude = toDirection.magnitude;
		base.transform.position = 0.5f * (position + position2);
		base.transform.rotation = Quaternion.FromToRotation(Vector3.right, toDirection);
		m_MeshRenderer.transform.SetLocalScaleX(magnitude);
	}
}
