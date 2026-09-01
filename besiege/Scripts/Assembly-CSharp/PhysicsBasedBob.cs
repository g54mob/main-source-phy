using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBasedBob : SimBehaviour
{
	public float amplitude = 0.5f;

	public Rigidbody myRigidbody;

	public float forceToFollowBob = 1f;

	public float timingOffset;

	private Transform myTransform;

	private float timeCounter;

	private Vector3 targetPosition;

	private float bobSpeed = 1f;

	private List<Joint> joints = new List<Joint>();

	protected override void Start()
	{
		base.Start();
		myTransform = base.transform;
		timingOffset += UnityEngine.Random.Range(0f, 10f);
		bobSpeed = UnityEngine.Random.Range(0.6f, 2.4f);
		if (myRigidbody == null)
		{
			Debug.LogError("Rigidbody of " + base.transform.name + " has not been assigned");
		}
		myRigidbody.useGravity = false;
	}

	private void OnEnable()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	public void OnDisable()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnSimulationToggle(bool toggle)
	{
		if (toggle)
		{
			timingOffset += UnityEngine.Random.Range(0f, 10f);
			bobSpeed = UnityEngine.Random.Range(0.6f, 2.4f);
			myRigidbody.useGravity = false;
		}
	}

	private void FixedUpdate()
	{
		if (myTransform == null || joints.Count > 0)
		{
			return;
		}
		if (myRigidbody.useGravity)
		{
			base.enabled = false;
			return;
		}
		timeCounter += Time.fixedDeltaTime;
		targetPosition = myTransform.position;
		targetPosition.y = base.transform.position.y + Mathf.Sin(timeCounter * bobSpeed + timingOffset) * amplitude * ((!base.SimPhysics) ? 0.01f : 1f);
		if (!base.SimPhysics)
		{
			myTransform.position = targetPosition;
		}
		else
		{
			myRigidbody.AddForce((myTransform.position - targetPosition) * forceToFollowBob - myRigidbody.velocity, ForceMode.VelocityChange);
		}
	}

	public void Joined(Joint joint)
	{
		if (!joints.Contains(joint))
		{
			joints.Add(joint);
		}
	}

	public void Disjoined(Joint joint)
	{
		if (joints.Contains(joint))
		{
			joints.Remove(joint);
		}
	}
}
