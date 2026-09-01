using System;
using UnityEngine;

public class LaserTargetCheck : MonoBehaviour
{
	public float meltingTime = 3f;

	[HideInInspector]
	public bool isHittingTarget;

	protected float timer;

	public Action OnMelted;

	protected void Update()
	{
		if (StatMaster.levelSimulating && !WinCondition.hasWon)
		{
			Progress();
		}
	}

	protected virtual void Progress()
	{
		if (isHittingTarget)
		{
			timer += Time.deltaTime;
		}
		else
		{
			timer -= Time.deltaTime;
		}
		timer = Mathf.Clamp(timer, 0f, meltingTime);
		if (timer == meltingTime)
		{
			base.enabled = false;
			if (OnMelted != null)
			{
				OnMelted();
			}
		}
	}

	public void CheckParameters(string target)
	{
		if (target == base.gameObject.name)
		{
			isHittingTarget = true;
		}
		else
		{
			isHittingTarget = false;
		}
	}
}
