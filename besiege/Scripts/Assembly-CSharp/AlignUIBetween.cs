using System;
using System.Collections;
using UnityEngine;

public class AlignUIBetween : AlignUI
{
	public AlignUI target2;

	public Transform quad2;

	public Horizontal target2X = Horizontal.Middle;

	public Vertical target2Y = Vertical.Middle;

	public AlignUI[] updateHook;

	private bool isDirty;

	private Vector3 TransformPoint(Transform target, Horizontal targetX, Vertical targetY)
	{
		float num = 0f;
		float num2 = 0f;
		num = target.position.x;
		num2 = target.position.y;
		Vector3 vector = target.lossyScale / 2f;
		switch (targetX)
		{
		case Horizontal.Left:
			num -= vector.x;
			break;
		case Horizontal.Right:
			num += vector.x;
			break;
		}
		switch (targetY)
		{
		case Vertical.Bottom:
			num2 -= vector.y;
			break;
		case Vertical.Top:
			num2 += vector.y;
			break;
		}
		return new Vector3(num, num2);
	}

	private Vector3 TargetPoint(AlignUI target, Horizontal targetX, Vertical targetY)
	{
		float x = 0f;
		float y = 0f;
		switch (targetX)
		{
		case Horizontal.Left:
			x = target.leftMost;
			break;
		case Horizontal.Right:
			x = target.rightMost;
			break;
		}
		switch (targetY)
		{
		case Vertical.Bottom:
			y = target.bottomMost;
			break;
		case Vertical.Top:
			y = target.topMost;
			break;
		}
		return new Vector3(x, y);
	}

	protected override void Start()
	{
		base.Start();
		if (mode == Mode.Component)
		{
			AlignUI alignUI = target2;
			alignUI.OnAlign = (Action)Delegate.Combine(alignUI.OnAlign, new Action(Align));
			return;
		}
		AlignUI[] array = updateHook;
		foreach (AlignUI alignUI2 in array)
		{
			alignUI2.OnAlign = (Action)Delegate.Combine(alignUI2.OnAlign, new Action(Align));
		}
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (!started)
		{
			return;
		}
		if (mode == Mode.Component)
		{
			AlignUI alignUI = target2;
			alignUI.OnAlign = (Action)Delegate.Remove(alignUI.OnAlign, new Action(Align));
			return;
		}
		AlignUI[] array = updateHook;
		foreach (AlignUI alignUI2 in array)
		{
			alignUI2.OnAlign = (Action)Delegate.Remove(alignUI2.OnAlign, new Action(Align));
		}
	}

	private void OnEnable()
	{
		if (isDirty)
		{
			StopAllCoroutines();
			StartCoroutine(DelayAlign());
		}
	}

	public override void AttemptAlign()
	{
		if (base.Auto)
		{
			ScheduleAlign();
		}
	}

	public void ScheduleAlign()
	{
		isDirty = true;
		if (!(base.gameObject == null) && base.gameObject.activeInHierarchy)
		{
			StopAllCoroutines();
			StartCoroutine(DelayAlign());
		}
	}

	private IEnumerator DelayAlign()
	{
		yield return new WaitForEndOfFrame();
		Align();
	}

	public override void Align()
	{
		isDirty = false;
		Vector3 vector;
		Vector3 vector2;
		switch (mode)
		{
		case Mode.Transform:
			vector = TransformPoint(quad, targetX, targetY);
			vector2 = TransformPoint(quad2, target2X, target2Y);
			break;
		case Mode.Component:
			if (!Application.isPlaying)
			{
				target.Align();
				target2.Align();
			}
			vector = TargetPoint(target, targetX, targetY);
			vector2 = TargetPoint(target2, target2X, target2Y);
			break;
		default:
			vector = (vector2 = base.transform.localPosition);
			break;
		}
		Vector3 vector3 = (vector + vector2) / 2f;
		Vector3 vector4 = vector2 - vector;
		base.transform.position = new Vector3(moveHorizontally ? vector3.x : base.transform.position.x, moveVertically ? vector3.y : base.transform.position.y, base.transform.position.z);
		base.transform.localScale = new Vector3(moveHorizontally ? vector4.x : base.transform.localScale.x, moveVertically ? vector4.y : base.transform.localScale.y, base.transform.localScale.z);
		if (OnAlign != null)
		{
			OnAlign();
		}
	}
}
