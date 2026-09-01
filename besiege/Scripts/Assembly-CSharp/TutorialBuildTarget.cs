using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/Tutorial/Build Target")]
public class TutorialBuildTarget : MonoBehaviour
{
	public static List<TutorialBuildTarget> targets = new List<TutorialBuildTarget>();

	public static Coroutine routine = null;

	private static int frame = 0;

	private Machine machine;

	private Vector3 machinePos = Vector3.zero;

	private Quaternion machineRot = Quaternion.identity;

	private Vector3 offset = Vector3.zero;

	private bool running;

	private void OnEnable()
	{
		if (!running)
		{
			ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Combine(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(DisableATarget));
			GetComponent<MeshRenderer>().enabled = true;
			targets.Add(this);
			running = true;
		}
		machine = Machine.Active();
		if (machine != null)
		{
			machinePos = machine.BuildingMachine.position;
			machineRot = machine.BuildingMachine.rotation;
			offset = base.transform.position - machinePos;
		}
	}

	private void Update()
	{
		if (!running)
		{
			return;
		}
		if (machine == null)
		{
			OnEnable();
			return;
		}
		if (machinePos != machine.BuildingMachine.position)
		{
			machinePos = machine.BuildingMachine.position;
			base.transform.position = machinePos + offset;
		}
		if (machineRot != machine.BuildingMachine.rotation)
		{
			OnDisable();
		}
	}

	private static void DisableATarget(BlockBehaviour b)
	{
		if (!(b == null))
		{
			if (routine != null)
			{
				frame = Time.frameCount + 1;
				return;
			}
			frame = Time.frameCount + 1;
			routine = ReferenceMaster.Instance.StartCoroutine(IEDisableATarget(b));
		}
	}

	private static IEnumerator IEDisableATarget(BlockBehaviour b)
	{
		if (Time.frameCount < frame)
		{
			yield return null;
		}
		if (b == null)
		{
			routine = null;
			yield break;
		}
		TutorialBuildTarget closest = null;
		float dist = float.MaxValue;
		foreach (TutorialBuildTarget target in targets)
		{
			float d = Vector3.Distance(target.transform.position, b.transform.position);
			if (d < dist)
			{
				dist = d;
				closest = target;
			}
		}
		if ((bool)closest)
		{
			closest.OnDisable();
		}
		routine = null;
	}

	private void OnDisable()
	{
		if (running)
		{
			ReferenceMaster.onBlockPlaced = (Action<BlockBehaviour>)Delegate.Remove(ReferenceMaster.onBlockPlaced, new Action<BlockBehaviour>(DisableATarget));
			GetComponent<MeshRenderer>().enabled = false;
			targets.Remove(this);
			running = false;
		}
	}
}
