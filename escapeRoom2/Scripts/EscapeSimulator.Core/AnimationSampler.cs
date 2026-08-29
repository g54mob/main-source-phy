using System;
using UnityEngine;

[ExecuteInEditMode]
public class AnimationSampler : MonoBehaviour
{
	public AnimationClip clip;

	[Range(0f, 1f)]
	public float unitTime;

	[NonSerialized]
	public float playSpeed;

	[NonSerialized]
	public float playDelay;

	[NonSerialized]
	public float editor_unitTime;

	private float lastUnitTime = -1f;

	private AnimationClip lastClip;

	private void LateUpdate()
	{
		updateIfPlaying();
		refresh();
	}

	private void updateIfPlaying()
	{
		if (clip == null)
		{
			return;
		}
		if (playDelay > 0f)
		{
			playDelay = Mathf.MoveTowards(playDelay, 0f, Time.deltaTime);
		}
		else if (playSpeed > 0f)
		{
			float length = clip.length;
			float point = UnityUtils.map(0f, 1f, 0f, length, unitTime) + Time.deltaTime * playSpeed;
			unitTime = UnityUtils.map(0f, length, 0f, 1f, point);
			if (!(unitTime <= 1f))
			{
				playSpeed = 0f;
				playDelay = 0f;
			}
		}
	}

	public void play(float speed = 1f, float delay = 0f)
	{
		playSpeed = speed;
		playDelay = delay;
	}

	public void setUnitTime(float time)
	{
		unitTime = time;
		refresh();
	}

	private void refresh()
	{
		if ((lastUnitTime != unitTime || clip != lastClip) && base.enabled)
		{
			sync();
		}
	}

	public void forceSetUnitTime(float time)
	{
		unitTime = time;
		sync();
	}

	public void sync()
	{
		lastClip = clip;
		lastUnitTime = unitTime;
		if (clip != null)
		{
			clip.SampleAnimation(base.gameObject, unitTime * clip.length);
		}
	}
}
