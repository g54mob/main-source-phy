using System;
using System.Collections;
using UnityEngine;

public class GoldBowlTarget : LaserTargetCheck
{
	private float lerpStep;

	[Header("Visuals")]
	[SerializeField]
	private MeshRenderer goldPile;

	[SerializeField]
	private MeshRenderer bowl;

	[SerializeField]
	private Color emissiveColor;

	[SerializeField]
	private ParticleSystem bubbles;

	[SerializeField]
	[Header("Fire")]
	private FireTag fire;

	private bool hasPlayedPayoff;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			bowl.material.SetColor("_EmissCol", emissiveColor);
			bowl.material.SetFloat("_EmissivePower", 0f);
			OnMelted = (Action)Delegate.Combine(OnMelted, new Action(Melted));
		}
	}

	protected override void Progress()
	{
		base.Progress();
		lerpStep = timer / meltingTime;
		bowl.material.SetFloat("_EmissivePower", Mathf.Lerp(0f, 5f, lerpStep));
		if (!isHittingTarget)
		{
			fire.fireControllerCode.DouseFire();
		}
		else
		{
			fire.Ignite(1f);
		}
	}

	private void Melted()
	{
		if (!hasPlayedPayoff)
		{
			WinCondition.currentObjsCompleted++;
			hasPlayedPayoff = true;
			StartCoroutine(Payoff());
		}
	}

	private IEnumerator Payoff()
	{
		float newStep = 0f;
		bubbles.Play();
		for (float t = 0f; t < 5f; t += Time.deltaTime)
		{
			newStep = t / 5f;
			goldPile.material.SetFloat("_TextureLerp", Mathf.Lerp(0f, 1f, newStep));
			goldPile.gameObject.transform.localScale = new Vector3(3.9f, Mathf.Lerp(3.9f, 0.75f, newStep), 3.9f);
			yield return null;
		}
	}
}
