using System;
using UnityEngine;

public class CannonChainBall : MonoBehaviour
{
	public Rigidbody rb;

	public Transform chain;

	public Transform leftBall;

	public Transform rightBall;

	public float defaultLength = 4f;

	public float radius = 0.35f;

	public float time = 1f;

	private float currentTime;

	private float currentFixedTime;

	public CapsuleCollider ballCollider;

	private Vector3 rightBallStart = new Vector3(0f, -0.1f, 0f);

	private Vector3 leftBallStart = new Vector3(0f, 0.1f, 0f);

	private Vector3 chainStart = new Vector3(1f, 0.3f, 1f);

	private Vector3 chainEnd;

	private Vector3 leftBallEnd;

	private Vector3 rightBallEnd;

	private float startHeight = 0.701f;

	private float endHeight;

	private bool firstTime = true;

	private bool lerping;

	private bool collapsing;

	private Vector3 velocity = Vector3.one;

	private Quaternion lastRotation;

	public void SetSize(float length)
	{
		length = Mathf.Clamp(length * defaultLength, radius * 2f + 0.1f, 10f);
		if (rb != null)
		{
			ballCollider.height = startHeight;
			rb.inertiaTensor = Vector3.one * Mathf.Clamp(0.4f + length * 0.05f, 0.25f, 2f);
			rb.maxAngularVelocity = 20f;
		}
		chainEnd = new Vector3(1f, (length - radius * 4f + 0.1f) / 2.7f, 1f);
		float num = length * 0.5f - radius;
		rightBallEnd = new Vector3(0f, 0f - num, 0f);
		leftBallEnd = new Vector3(0f, num, 0f);
		endHeight = length;
		firstTime = false;
		if (!base.enabled)
		{
			base.enabled = true;
		}
		lerping = false;
		UpdateVisual(0f);
		ResetTime();
	}

	public void ResetTime()
	{
		currentTime = 0f;
		currentFixedTime = 0f;
	}

	private void OnEnable()
	{
		if (firstTime)
		{
			SetSize(1f);
		}
	}

	private void Update()
	{
		if (lerping)
		{
			float pct = Mathf.Sqrt(currentTime / time);
			UpdateVisual(pct);
			currentTime += Time.deltaTime;
		}
	}

	private void UpdateVisual(float pct)
	{
		leftBall.localPosition = Vector3.Lerp(leftBallStart, leftBallEnd, pct);
		rightBall.localPosition = Vector3.Lerp(rightBallStart, rightBallEnd, pct);
		chain.localScale = new Vector3(chainEnd.x, Mathf.Lerp(chainStart.y, chainEnd.y, pct), chainEnd.z);
	}

	private void FixedUpdate()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		lerping = true;
		if (!StatMaster.isMP || StatMaster.isHosting || (StatMaster.isClient && StatMaster.isLocalSim))
		{
			float t = Mathf.Sqrt(currentFixedTime / time);
			ballCollider.height = Mathf.Lerp(startHeight, endHeight, t);
		}
		if (currentFixedTime >= time)
		{
			if (!StatMaster.isMP || collapsing)
			{
				base.enabled = false;
			}
			else if (GetSqrAngularVelocity() < 20f)
			{
				Collapse();
			}
		}
		else
		{
			currentFixedTime += Time.fixedDeltaTime;
		}
		lastRotation = base.transform.rotation;
	}

	public float GetSqrAngularVelocity()
	{
		Quaternion rotation = base.transform.rotation;
		float angle;
		(rotation * Quaternion.Inverse(lastRotation)).ToAngleAxis(out angle, out velocity);
		velocity = velocity * angle * ((float)Math.PI / 180f) / Time.fixedDeltaTime;
		return velocity.sqrMagnitude;
	}

	public void InvokeCollapse()
	{
		if (!StatMaster.isMP && !(currentFixedTime < time * 0.5f))
		{
			Collapse();
		}
	}

	public void Collapse()
	{
		if (!collapsing)
		{
			base.enabled = true;
			collapsing = true;
			lerping = true;
			ResetTime();
			leftBallEnd = leftBallStart;
			rightBallEnd = rightBallStart;
			chainEnd = chainStart;
			endHeight = startHeight;
			leftBallStart = leftBall.localPosition;
			rightBallStart = rightBall.localPosition;
			chainStart = chain.localScale;
			startHeight = ballCollider.height;
		}
	}
}
