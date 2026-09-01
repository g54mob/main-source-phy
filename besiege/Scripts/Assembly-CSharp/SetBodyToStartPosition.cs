using System;
using System.Collections;
using UnityEngine;

public class SetBodyToStartPosition : MonoBehaviour
{
	public Rigidbody body;

	private Vector3 pos;

	private Quaternion rot;

	private bool reset;

	private void Awake()
	{
		pos = body.position;
		rot = body.rotation;
		reset = true;
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulate));
		if (!StatMaster.levelSimulating)
		{
			StartCoroutine(IEAwake());
		}
	}

	private IEnumerator IEAwake()
	{
		yield return new WaitForSecondsRealtime(3f);
		while (StatMaster.levelSimulating || !reset)
		{
			yield return null;
		}
		pos = body.position;
		rot = body.rotation;
		reset = true;
	}

	private void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulate));
	}

	private void OnSimulate(bool simulate)
	{
		if (!simulate)
		{
			body.MovePosition(pos);
			body.MoveRotation(rot);
			Rigidbody rigidbody = body;
			Vector3 zero = Vector3.zero;
			body.angularVelocity = zero;
			rigidbody.velocity = zero;
			reset = true;
		}
		else
		{
			reset = false;
		}
	}
}
