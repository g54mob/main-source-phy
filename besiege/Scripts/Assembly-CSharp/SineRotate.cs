using System;
using UnityEngine;

public class SineRotate : MonoBehaviour
{
	public Transform visObject;

	public EntityAI aiCode;

	public ParticleSystem nodes;

	public bool canBob;

	public float bobAmount;

	public float bobRate;

	public float startPosY;

	public bool rotateOnX = true;

	public float xMultiplier = 1f;

	public float timeCount;

	public bool flip;

	private float phi;

	private float amplitude;

	private float startOffset;

	private float sine;

	private float sine2;

	private float offset;

	private void Start()
	{
		startOffset = UnityEngine.Random.value * 10f;
		startPosY = visObject.localPosition.y;
		if (flip)
		{
			offset = 180f;
		}
		if (aiCode == null)
		{
			UnityEngine.Object.Destroy(this);
		}
	}

	private void Update()
	{
		if (aiCode.isDead)
		{
			UnityEngine.Object.Destroy(this);
			if (nodes != null)
			{
				nodes.Stop();
			}
		}
		timeCount += Time.deltaTime;
		sine = Mathf.Sin(startOffset + timeCount * bobRate) * bobAmount;
		if (rotateOnX)
		{
			sine2 = Mathf.Sin(startOffset + timeCount * bobRate / 2f) * bobAmount;
			visObject.localRotation = Quaternion.Euler(sine2 / 3f * xMultiplier, sine + offset, 0f);
		}
		else
		{
			visObject.localRotation = Quaternion.Euler(0f, sine + offset, 0f);
		}
	}

	private void Bob(Transform obj, float offset, float yPos)
	{
		phi = (Time.time + startOffset + offset) / bobRate * (float)Math.PI * 2f;
		amplitude = Mathf.Cos(phi) * 0.5f + 0.5f;
		obj.localRotation = new Quaternion(obj.localRotation.x, 0f + amplitude * bobAmount, obj.localRotation.z, obj.localRotation.w);
	}
}
