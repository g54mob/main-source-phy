using System;
using UnityEngine.Events;

[Serializable]
public class EventSequenceInstance
{
	public float timeBeforeCall;

	public UnityEvent eventToCall;
}
