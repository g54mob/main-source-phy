using System;
using UnityEngine;

public class SineBobOnVelocity : MonoBehaviour
{
	public Rigidbody myRigidbody;

	public Transform visObject;

	public Transform visObject2;

	public bool canBob;

	public float bobAmount;

	public float bobRate;

	public float secondaryObjOffset = 0.2f;

	public float startPosY;

	public float secondaryStartPosY;

	private float phi;

	private float amplitude;

	private float startOffset;

	private void Start()
	{
		startOffset = UnityEngine.Random.value * ((float)Math.PI / 2f);
		startPosY = visObject.localPosition.y;
		secondaryStartPosY = visObject2.localPosition.y;
	}

	private void Update()
	{
		if (canBob && StatMaster.levelSimulating)
		{
			Bob(visObject, 0f, startPosY);
			Bob(visObject2, secondaryObjOffset, secondaryStartPosY);
		}
	}

	private void Bob(Transform obj, float offset, float yPos)
	{
		phi = (Time.time + startOffset + offset) / bobRate * (float)Math.PI * 2f;
		amplitude = Mathf.Cos(phi) * 0.5f + 0.5f;
		obj.localPosition = new Vector3(obj.localPosition.x, yPos + amplitude * bobAmount * Mathf.Clamp(myRigidbody.velocity.sqrMagnitude * 10f, 0f, 5f), obj.localPosition.z);
	}
}
