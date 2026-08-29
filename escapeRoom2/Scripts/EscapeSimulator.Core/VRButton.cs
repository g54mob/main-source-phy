using System;
using UnityEngine;

public class VRButton : MonoBehaviour
{
	public Transform planeTransform;

	public Vector2 planeRectangleExtents;

	public float maxPushInDepth;

	public float cancelInteractionDepth;

	[NonSerialized]
	public Vector3 originalLocalPosition;

	[NonSerialized]
	public Action onClick;

	private void Awake()
	{
		originalLocalPosition = base.transform.localPosition;
	}

	private void OnDrawGizmos()
	{
		if (!(planeTransform == null))
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawSphere(planeTransform.position, 0.01f);
			Vector3 vector = planeTransform.TransformPoint(planeRectangleExtents.x, planeRectangleExtents.y, 0f);
			Vector3 vector2 = planeTransform.TransformPoint(planeRectangleExtents.x, 0f - planeRectangleExtents.y, 0f);
			Vector3 vector3 = planeTransform.TransformPoint(0f - planeRectangleExtents.x, 0f - planeRectangleExtents.y, 0f);
			Vector3 vector4 = planeTransform.TransformPoint(0f - planeRectangleExtents.x, planeRectangleExtents.y, 0f);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
			Gizmos.color = Color.red;
			Gizmos.DrawLine(planeTransform.position, planeTransform.position + planeTransform.forward * cancelInteractionDepth);
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(planeTransform.position, planeTransform.position + planeTransform.forward * maxPushInDepth);
		}
	}
}
