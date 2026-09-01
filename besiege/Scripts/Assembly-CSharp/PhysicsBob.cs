using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBob : SimBehaviour
{
	public bool shouldBob = true;

	public bool inBuildMode;

	public Transform myTransform;

	public float startHeight;

	public Vector3 startPosition;

	public Vector3 targetPosition;

	public float bobSpeed = 2f;

	public float amplitude = 2f;

	public float timingOffset;

	public float timeCounter;

	public float targetHeight;

	public Rigidbody myRigidbody;

	public float forceToFollowBob = 100f;

	private List<Joint> joints = new List<Joint>();

	protected override void Start()
	{
		base.Start();
		if (!base.isSimulating || inBuildMode)
		{
			shouldBob = true;
			myTransform = base.transform;
			myRigidbody = base.gameObject.GetComponent<Rigidbody>();
			if (inBuildMode)
			{
				OnSimulationToggle(true);
			}
		}
	}

	private void OnEnable()
	{
		if (!inBuildMode)
		{
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
		}
	}

	public void OnDisable()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnSimulationToggle(bool toggle)
	{
		if (toggle)
		{
			startHeight = myTransform.position.y;
			startPosition = myTransform.position;
			targetPosition = startPosition;
			timingOffset = UnityEngine.Random.Range(0f, 10f);
			bobSpeed = UnityEngine.Random.Range(0.6f, 2.4f);
		}
	}

	private void FixedUpdate()
	{
		if ((base.SimPhysics || inBuildMode) && shouldBob && !(myTransform == null) && joints.Count <= 0)
		{
			timeCounter += Time.fixedDeltaTime;
			targetHeight = startHeight + Mathf.Sin(timeCounter * bobSpeed + timingOffset) * amplitude;
			targetPosition.y = targetHeight;
			if ((new Vector3(base.transform.position.x, 0f, base.transform.position.z) - new Vector3(startPosition.x, 0f, startPosition.z)).sqrMagnitude > 2f)
			{
				myRigidbody.AddForce((myTransform.position - new Vector3(myTransform.position.x, targetHeight, myTransform.position.z)) * forceToFollowBob);
			}
			else
			{
				myRigidbody.AddForce((myTransform.position - targetPosition) * forceToFollowBob);
			}
		}
	}

	private void Joined(Joint joint)
	{
		if (!joints.Contains(joint))
		{
			joints.Add(joint);
		}
	}

	private void Disjoined(Joint joint)
	{
		if (joints.Contains(joint))
		{
			joints.Remove(joint);
		}
	}
}
