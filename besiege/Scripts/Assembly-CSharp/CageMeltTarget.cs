using System;
using UnityEngine;

public class CageMeltTarget : LaserTargetCheck
{
	private float lerpStep;

	public float animSpeed = 1f;

	public AnimationCurve boxScale;

	[SerializeField]
	private Animator animation;

	[SerializeField]
	private BoxCollider collider;

	[SerializeField]
	private AudioSource sfx;

	[SerializeField]
	private Transform puddle;

	[SerializeField]
	private Collider trophy;

	private float melt;

	private void Start()
	{
		animation.speed = 0f;
		if (StatMaster.levelSimulating)
		{
			OnMelted = (Action)Delegate.Combine(OnMelted, new Action(Melted));
		}
	}

	protected override void Progress()
	{
		base.Progress();
		lerpStep = timer / meltingTime;
		if (lerpStep > melt)
		{
			melt = lerpStep;
			animation.Play("melt", 0, melt * animSpeed);
			if ((puddle.position - base.transform.position).sqrMagnitude < 1f)
			{
				puddle.localScale = new Vector3(melt, melt * 0.01f, melt);
			}
			Vector3 center = collider.center;
			center.y = Mathf.Lerp(0.5f, 0.16f, boxScale.Evaluate(melt));
			collider.center = center;
			center = collider.size;
			center.y = Mathf.Lerp(1f, 0.32f, boxScale.Evaluate(melt));
			collider.size = center;
		}
	}

	private void Melted()
	{
		sfx.Play();
		trophy.enabled = true;
		trophy.transform.parent = base.transform.parent.parent;
	}
}
