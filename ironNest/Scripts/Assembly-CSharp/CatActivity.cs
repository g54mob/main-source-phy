using System;
using UnityEngine;

[Serializable]
public class CatActivity
{
	public string activityName;

	public string animationTrigger;

	[Header("Not looping activities have the same Min and Max durations")]
	public float durationMin;

	public float durationMax;

	public float weight;

	[Header("For looping activities they can have different min and max durations (but dont go below 20s for minimum)")]
	public bool loop;

	[Header("Its preset value and shouldn't be changed")]
	public float afterLoopDuration;
}
