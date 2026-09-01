using System;
using UnityEngine;

[Serializable]
public class HoldableDataInstance
{
	public Transform leftHand;

	public Transform rightHand;

	public Vector3 relativePosition = new Vector3(0f, 0.1f, 0.2f);

	public Vector3 forwardRotation = new Vector3(0f, 0f, 1f);

	public Vector3 upRotation = new Vector3(0f, 1f, 0f);

	public float lookAtTargetWhenWithin;

	public float extraGrabDistance;

	public bool setRotation = true;

	public bool snapConnect;
}
