using System;
using UnityEngine;

[AddComponentMenu("Simulation/FishAnimationSetter")]
public class FishAnimationSetter : MonoBehaviour
{
	public Animator[] animatorsToEnable;

	private Transform animationParent;

	protected void Start()
	{
		if (StatMaster.levelSimulating)
		{
			OnSimulationToggled(true);
		}
		else
		{
			if (animationParent == null)
			{
				animationParent = new GameObject("animationParent").transform;
				animationParent.parent = base.transform.parent;
				animationParent.position = base.transform.position;
				base.transform.parent = animationParent;
			}
			OnSimulationToggled(false);
		}
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggled));
	}

	private void OnSimulationToggled(bool toggle)
	{
		for (int i = 0; i < animatorsToEnable.Length; i++)
		{
			animatorsToEnable[i].speed = ((!toggle) ? 0f : 1f);
		}
	}
}
