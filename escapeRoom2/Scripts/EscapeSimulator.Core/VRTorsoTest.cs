using System;
using UnityEngine;

public class VRTorsoTest : MonoBehaviour
{
	public Transform head;

	public Transform neck;

	public Transform torso;

	private Quaternion topViewRotationLastUpdate;

	private Vector3 topViewHeadForwardLastUpdate;

	private bool topViewRotationInited;

	public float maxVectorUpAngle = 60f;

	private void Update()
	{
		torso.transform.position = neck.transform.position;
		Quaternion rotation = head.rotation;
		if (Vector3.Angle(Vector3.up, head.up) > maxVectorUpAngle)
		{
			Vector3 normalized = Vector3.ProjectOnPlane(head.up, Vector3.up).normalized;
			normalized.y = Mathf.Tan((90f - maxVectorUpAngle) * (MathF.PI / 180f));
			head.up = normalized.normalized;
		}
		updateTopView();
		updateFrontView();
		updateSideView();
		head.rotation = rotation;
	}

	private void updateTopView()
	{
		Vector3 forward = Vector3.forward;
		Vector3 vector = Vector3.ProjectOnPlane(head.forward, Vector3.up);
		float angle = Vector3.SignedAngle(forward, vector, Vector3.up);
		Vector3 to = Vector3.ProjectOnPlane(topViewHeadForwardLastUpdate, Vector3.up);
		if (!topViewRotationInited)
		{
			torso.rotation = Quaternion.AngleAxis(angle, Vector3.up);
			topViewHeadForwardLastUpdate = head.forward;
			topViewRotationInited = true;
		}
		else if (Vector3.Angle(vector, to) > 45f)
		{
			Quaternion b = Quaternion.AngleAxis(angle, Vector3.up);
			torso.rotation = Quaternion.Slerp(topViewRotationLastUpdate, b, Time.deltaTime * 10f);
			topViewHeadForwardLastUpdate = Vector3.Slerp(topViewHeadForwardLastUpdate, head.forward, Time.deltaTime * 10f);
		}
		else
		{
			torso.rotation = topViewRotationLastUpdate;
		}
		topViewRotationLastUpdate = torso.rotation;
	}

	private void updateFrontView()
	{
		Vector3 vector = Vector3.ProjectOnPlane(Vector3.up, head.forward);
		Vector3 to = Vector3.ProjectOnPlane(head.up, head.forward);
		float num = Mathf.Clamp(Vector3.SignedAngle(vector, to, head.forward), -90f, 90f);
		torso.Rotate(new Vector3(0f, 0f, num / 3f), Space.Self);
	}

	private void updateSideView()
	{
		Vector3 vector = Vector3.ProjectOnPlane(Vector3.up, head.right);
		Vector3 to = Vector3.ProjectOnPlane(head.up, head.right);
		float num = Mathf.Clamp(Vector3.SignedAngle(vector, to, head.right), 0f, 90f);
		torso.Rotate(new Vector3(num / 2f, 0f, 0f), Space.Self);
	}
}
