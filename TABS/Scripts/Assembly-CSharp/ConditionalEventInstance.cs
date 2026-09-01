using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ConditionalEventInstance
{
	public EventCondition[] conditions;

	public UnityEvent continuousEvent;

	[Space(30f)]
	public bool isOn;

	public float delay;

	public UnityEvent turnOnEvent;

	public UnityEvent turnOffEvent;

	public float stunAllEventsFor;

	public float stopWeaponAttacksFor;

	[HideInInspector]
	public bool checkAutomatically = true;

	[HideInInspector]
	public Move[] moves;

	public int NetworkId { get; set; }
}
