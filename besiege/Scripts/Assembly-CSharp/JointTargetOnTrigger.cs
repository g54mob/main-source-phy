using System;
using UnityEngine;

public class JointTargetOnTrigger : MonoBehaviour
{
	public ConfigurableJoint joint;

	public Vector3 target;

	public float duration = 0.2f;

	public float delayAfterTrigger;

	public ParticleSystem particleSys;

	private bool isDone;

	private bool isMoving;

	private float pct;

	private void OnEnable()
	{
		delayAfterTrigger *= UnityEngine.Random.Range(0.7f, 1.3f);
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	public void OnDisable()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnSimulationToggle(bool toggle)
	{
		if (!toggle)
		{
			Reset();
		}
	}

	private void Reset()
	{
		isMoving = false;
		isDone = false;
		pct = 0f;
		joint.targetPosition = Vector3.zero;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!StatMaster.isClient && StatMaster.levelSimulating && !isMoving && !isDone && (bool)other.attachedRigidbody)
		{
			isMoving = true;
			if (particleSys != null)
			{
				particleSys.Play();
			}
		}
	}

	private void FixedUpdate()
	{
		if (!StatMaster.isClient && StatMaster.levelSimulating && isMoving && !isDone)
		{
			pct += Time.fixedDeltaTime / duration;
			if (pct > 1f)
			{
				pct = 1f;
				isMoving = false;
				isDone = true;
			}
			joint.targetPosition = Vector3.Lerp(Vector3.zero, target, pct);
		}
	}
}
