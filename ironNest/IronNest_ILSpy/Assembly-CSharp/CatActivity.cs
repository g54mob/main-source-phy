using System;

[Serializable]
public class CatActivity
{
	public string activityName;

	public string animationTrigger;

	public float durationMin;

	public float durationMax;

	public float weight = 1f;

	public bool loop;

	public float afterLoopDuration;
}
