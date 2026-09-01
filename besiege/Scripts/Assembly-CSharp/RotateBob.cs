using System;
using UnityEngine;

public class RotateBob : MonoBehaviour
{
	public Vector3 rotateAxis = Vector3.up;

	public float degreesPerSecond = 45f;

	public bool worldSpace;

	public float bobAmount = 0.5f;

	public float bobTime = 1f;

	public bool startCenter;

	public bool localTime;

	private Transform xform;

	private float startPosY;

	private float phi;

	private float amplitude;

	private float time;

	private void Start()
	{
		xform = base.transform;
		startPosY = xform.localPosition.y;
	}

	private void Update()
	{
		xform.Rotate(rotateAxis, degreesPerSecond * Time.deltaTime, (!worldSpace) ? Space.Self : Space.World);
		if (localTime)
		{
			time += TimeSlider.Instance.DeltaTime();
			phi = time / bobTime * 2f * (float)Math.PI;
		}
		else
		{
			phi = Time.time / bobTime * 2f * (float)Math.PI;
		}
		if (startCenter)
		{
			amplitude = Mathf.Sin(phi) * 0.5f;
		}
		else
		{
			amplitude = Mathf.Cos(phi) * 0.5f + 0.5f;
		}
		xform.localPosition = new Vector3(xform.localPosition.x, startPosY + amplitude * bobAmount, xform.localPosition.z);
	}
}
