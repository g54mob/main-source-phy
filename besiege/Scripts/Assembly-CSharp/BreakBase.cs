using System;
using System.Collections.Generic;
using UnityEngine;

public class BreakBase : SimBehaviour, ITarget
{
	public static HashSet<BreakBase> currentTargets = new HashSet<BreakBase>();

	private Transform _transform;

	public Action<BreakBase> OnBreakTrigger;

	public virtual Vector3 Center()
	{
		return _transform.position;
	}

	protected override void Awake()
	{
		base.Awake();
		_transform = base.transform;
	}

	public virtual void OnBreak()
	{
	}

	protected virtual void OnDestroy()
	{
	}
}
