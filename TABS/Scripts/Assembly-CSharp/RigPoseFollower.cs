using System;
using UnityEngine;

[Serializable]
public class RigPoseFollower
{
	public Rigidbody rig;

	public Transform target;

	public Vector3 velocity;

	public Vector3 torque;

	public Vector3 currentPosition;

	public Vector3 currentRotation;

	public Vector3 currentRotationUp;

	public bool isUsed;
}
