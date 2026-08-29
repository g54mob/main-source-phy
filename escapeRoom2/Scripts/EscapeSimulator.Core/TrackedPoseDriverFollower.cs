using System;
using UnityEngine;

public class TrackedPoseDriverFollower : MonoBehaviour
{
	public Transform toFollow;

	public float positionSmoothing = 5f;

	public float rotationSmoothing = 5f;

	[NonSerialized]
	public Vector3 positionOffset;

	[NonSerialized]
	public Vector3 rotationOffset;

	private void LateUpdate()
	{
		Vector3 vector = Matrix4x4.TRS(Vector3.zero, toFollow.rotation * Quaternion.Euler(rotationOffset), Vector3.one).MultiplyVector(positionOffset);
		Vector3 vector2 = toFollow.position + vector;
		Quaternion quaternion = toFollow.rotation * Quaternion.Euler(rotationOffset);
		Game.HandSmoothingType handSmoothingType = PlayerSave.getSettings().handSmoothingType;
		if (handSmoothingType == Game.HandSmoothingType.Hand || handSmoothingType == Game.HandSmoothingType.HandAndItem)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, vector2, Time.deltaTime * positionSmoothing);
			base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, Time.deltaTime * rotationSmoothing);
		}
		else
		{
			base.transform.position = vector2;
			base.transform.rotation = quaternion;
		}
	}
}
