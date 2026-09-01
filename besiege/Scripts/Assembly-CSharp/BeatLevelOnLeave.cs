using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatLevelOnLeave : MonoBehaviour
{
	public List<Rigidbody> targets = new List<Rigidbody>();

	public float delay = 0.5f;

	private bool completing;

	public virtual void OnTriggerExit(Collider col)
	{
		if (StatMaster.levelSimulating)
		{
			Rigidbody attachedRigidbody = col.attachedRigidbody;
			if ((bool)attachedRigidbody && targets.Contains(attachedRigidbody))
			{
				StartCoroutine(Complete());
			}
		}
	}

	public IEnumerator Complete()
	{
		if (!completing)
		{
			completing = true;
			yield return new WaitForSeconds(delay);
			Add(1);
		}
	}

	public void Add(int x)
	{
		WinCondition.currentObjsCompleted += x;
	}
}
