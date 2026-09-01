using System;
using UnityEngine;

public class RotateRigidbody : MonoBehaviour
{
	public float speed;

	public Vector3 axis;

	public float sineMag;

	public Rigidbody myBody;

	private Quaternion initialRotation;

	private float rotateTime;

	private bool wasSim;

	private void Awake()
	{
		if (myBody == null)
		{
			myBody = GetComponent<Rigidbody>();
			if (myBody == null)
			{
				base.enabled = false;
			}
		}
		UpdateInitial();
	}

	public void UpdateInitial()
	{
		initialRotation = myBody.rotation;
	}

	private void FixedUpdate()
	{
		if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
		{
			return;
		}
		if (!StatMaster.isMP || StatMaster.levelSimulating)
		{
			rotateTime += Time.fixedDeltaTime + Mathf.Sin(Time.fixedTime * (float)Math.PI * 0.5f) * sineMag * Time.fixedDeltaTime;
		}
		if (!StatMaster.isMP || (myBody.isKinematic && StatMaster.levelSimulating))
		{
			float num = rotateTime * speed;
			float num2 = 360f;
			if (num >= num2)
			{
				num -= num2;
				rotateTime -= num2 / speed;
			}
			else if (num < 0f - num2)
			{
				num += num2;
				rotateTime += num2 / speed;
			}
			myBody.MoveRotation(initialRotation * Quaternion.AngleAxis(num, axis));
			wasSim = true;
		}
		else if (wasSim && !StatMaster.levelSimulating)
		{
			myBody.MoveRotation(initialRotation);
			wasSim = false;
		}
	}
}
