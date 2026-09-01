using System;
using UnityEngine;

public class Rotate : MonoBehaviour
{
	public Vector3 rotateAxis = Vector3.up;

	public float degreesPerSecond = 45f;

	public bool worldSpace;

	private Transform xform;

	public bool sine;

	public float sineSpeed = 1f;

	public float sineMag = 1f;

	public float sineOffset;

	public bool unscaledTime;

	private Quaternion startRot = Quaternion.identity;

	private void Awake()
	{
		xform = base.transform;
		startRot = xform.rotation;
	}

	private void LateUpdate()
	{
		xform.Rotate(rotateAxis, degreesPerSecond * ((!unscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime) * ((!sine) ? 1f : (Mathf.Sin(Time.unscaledTime * (float)Math.PI * sineSpeed) * sineMag + sineOffset)), (!worldSpace) ? Space.Self : Space.World);
	}

	private void OnDisable()
	{
		if ((bool)xform)
		{
			xform.rotation = startRot;
		}
	}
}
