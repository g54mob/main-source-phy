using System;
using System.Diagnostics;
using Poly;
using UnityEngine;

[DebuggerDisplay("{type} | {isOnEnter?\"OnEnter\":\"NonEnter\",nq} | {triggerHandler.transform.name} / {triggerHandler.asObject.name} <> {other.transform.name} / {other.attachedRigidbody.name}")]
public struct TriggerEventInfo : IComparable<TriggerEventInfo>
{
	public TriggerType type;

	public bool isOnEnter;

	public ITriggerHandler triggerHandler;

	public Collider other;

	public TriggerEventInfo(TriggerType type, bool isEnter, ITriggerHandler checkpoint, Collider other)
	{
		this.type = type;
		isOnEnter = isEnter;
		triggerHandler = checkpoint;
		this.other = other;
	}

	public int CompareTo(TriggerEventInfo other)
	{
		int num = type.CompareTo(other.type);
		if (num == 0)
		{
			num = -1 * isOnEnter.CompareTo(other.isOnEnter);
		}
		if (num == 0)
		{
			num = ((Vec3)triggerHandler.transform.position).CompareTo(other.triggerHandler.transform.position);
		}
		if (num == 0)
		{
			num = ((Vec3)this.other.transform.position).CompareTo(other.other.transform.position);
		}
		if (num == 0)
		{
			num = triggerHandler.indexInScene.CompareTo(other.triggerHandler.indexInScene);
		}
		return num;
	}
}
