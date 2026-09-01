using System;
using System.Collections;
using UnityEngine;

internal class LaserHitAI : MonoBehaviour, TrophyIncrement
{
	public Transform mesh;

	public GibOnImpact gib;

	public KillingHandler kh;

	private bool playing;

	public Action<MonoBehaviour> trophyIncrease { get; set; }

	internal void LaserHit()
	{
		if (!playing)
		{
			playing = true;
			StartCoroutine(AnimateBlowUp(3f));
		}
	}

	internal IEnumerator AnimateBlowUp(float duration)
	{
		Vector3 s = mesh.transform.localScale;
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float pct = t / duration;
			mesh.transform.localScale = s * (1f + pct * 2f);
			yield return null;
		}
		if (gib != null)
		{
			gib.Gib();
		}
		if (kh != null)
		{
			kh.KillUnit(false, InjuryType.Crushed);
		}
		if (trophyIncrease != null)
		{
			trophyIncrease(this);
		}
	}
}
