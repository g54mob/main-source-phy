using System;
using System.Collections;
using UnityEngine;

public class BranchAnimator : MonoBehaviour
{
	public const float GLOBAL_BREATH_SCALE = 1.5f;

	public float spatialSound = 0.99f;

	public AudioSource extend;

	public AudioSource retract;

	public FireTag fire;

	public BranchAnimator[] blockingBranches;

	public float startOffset;

	public float wait = 1f;

	public float duration = 1f;

	public float magnitudeOfBreath = 1f;

	public int breathingOffet = -1;

	public bool globalPosition;

	public Vector3 offsetPosition = Vector3.zero;

	public bool globalRotation;

	public float offsetAngle;

	public Vector3 axis;

	public Vector3 startPos;

	public Quaternion startRot;

	private bool swinging;

	private float stopAnimateBurnThreshold = 0.5f;

	protected float BurnedPct
	{
		get
		{
			return (!fire) ? 0f : ((!fire.hasController) ? 0f : fire.fireControllerCode.fireProgress);
		}
	}

	public bool IsStill
	{
		get
		{
			return BurnedPct > stopAnimateBurnThreshold && !swinging;
		}
	}

	public void Awake()
	{
		if (!StatMaster.levelSimulating)
		{
			startPos = base.transform.position;
			startRot = base.transform.rotation;
			magnitudeOfBreath = ((!((double)(base.transform.GetSiblingIndex() % 2) < 0.5)) ? magnitudeOfBreath : (0f - magnitudeOfBreath));
			if (breathingOffet == -1)
			{
				breathingOffet = (int)(startOffset + wait) % 3;
			}
		}
	}

	public void OnEnable()
	{
		if (!StatMaster.levelSimulating)
		{
			StartCoroutine(Breathe());
		}
		else
		{
			StartCoroutine(Animate());
		}
	}

	public void OnDisable()
	{
		StopAllCoroutines();
	}

	public IEnumerator Breathe()
	{
		Quaternion rot = startRot * Quaternion.AngleAxis(offsetAngle * 0.06f * 1.5f * magnitudeOfBreath, (!globalRotation) ? base.transform.parent.TransformDirection(axis.normalized) : axis.normalized);
		Vector3 pos = startPos + ((!globalPosition) ? base.transform.TransformVector(offsetPosition) : offsetPosition) * 0.1f * 1.5f * magnitudeOfBreath;
		float dur = startOffset + duration + wait;
		int n = (int)(dur / 3f) - breathingOffet;
		if (n < 1)
		{
			n = 1;
		}
		while (!StatMaster.levelSimulating)
		{
			for (float t = 0f; t < dur; t += Time.deltaTime)
			{
				float pct = t / dur;
				float cos = Mathf.Cos((pct * 2f * (float)n - 1f) * (float)Math.PI) * 0.5f + 0.5f;
				base.transform.position = Vector3.Lerp(startPos, pos, cos);
				base.transform.rotation = Quaternion.Slerp(startRot, rot, cos);
				yield return null;
			}
		}
	}

	public IEnumerator Animate()
	{
		Vector3 pos = base.transform.position;
		Quaternion rot = base.transform.rotation;
		float dur = Mathf.Clamp(startOffset / 2f, 1f, UnityEngine.Random.Range(1.5f, 2.5f));
		for (float t = 0f; t < dur; t += Time.deltaTime)
		{
			float pct = t / dur;
			base.transform.position = Vector3.Lerp(pos, startPos, pct);
			base.transform.rotation = Quaternion.Slerp(rot, startRot, pct);
			yield return null;
		}
		rot = startRot * Quaternion.AngleAxis(offsetAngle * 0.06f * 1.5f * magnitudeOfBreath, (!globalRotation) ? base.transform.parent.TransformDirection(axis.normalized) : axis.normalized);
		pos = startPos + ((!globalPosition) ? base.transform.TransformVector(offsetPosition) : offsetPosition) * 0.1f * 1.5f * magnitudeOfBreath;
		dur = startOffset - dur;
		int n = (int)(startOffset / 3f) - breathingOffet;
		if (n < 1)
		{
			n = 1;
		}
		for (float t2 = 0f; t2 < dur; t2 += Time.deltaTime)
		{
			float pct2 = t2 / dur;
			float cos = Mathf.Cos((pct2 * 2f * (float)n - 1f) * (float)Math.PI) * 0.5f + 0.5f;
			base.transform.position = Vector3.Lerp(startPos, pos, cos);
			base.transform.rotation = Quaternion.Slerp(startRot, rot, cos);
			yield return null;
		}
		base.transform.position = startPos;
		base.transform.rotation = startRot;
		while (StatMaster.levelSimulating)
		{
			rot = startRot * Quaternion.AngleAxis(offsetAngle, (!globalRotation) ? base.transform.parent.TransformDirection(axis.normalized) : axis.normalized);
			pos = startPos + ((!globalPosition) ? base.transform.TransformVector(offsetPosition) : offsetPosition);
			if (BurnedPct < stopAnimateBurnThreshold)
			{
				swinging = true;
				if ((bool)extend)
				{
					extend.spatialBlend = spatialSound;
					extend.Play();
				}
				if ((bool)retract)
				{
					retract.spatialBlend = spatialSound;
					retract.PlayDelayed(duration / 2f);
				}
				float pctWhenBlocked = ((!IsBlocked()) ? 1f : 0.2f);
				for (float t3 = 0f; t3 < duration; t3 += Time.deltaTime * (1f + BurnedPct * 0.5f))
				{
					float pct3 = t3 / duration;
					float cos2 = Mathf.Cos((pct3 * 2f - 1f) * (float)Math.PI) * 0.5f + 0.5f;
					base.transform.position = Vector3.Lerp(startPos, pos, cos2 * pctWhenBlocked);
					base.transform.rotation = Quaternion.Slerp(startRot, rot, cos2 * pctWhenBlocked);
					yield return null;
				}
			}
			base.transform.position = startPos;
			base.transform.rotation = startRot;
			swinging = false;
			rot = startRot * Quaternion.AngleAxis(offsetAngle * 0.06f * 1.5f * magnitudeOfBreath, (!globalRotation) ? base.transform.parent.TransformDirection(axis.normalized) : axis.normalized);
			pos = startPos + ((!globalPosition) ? base.transform.TransformVector(offsetPosition) : offsetPosition) * 0.1f * 1.5f * magnitudeOfBreath;
			n = (int)(wait / 3f) - breathingOffet;
			if (n < 1)
			{
				n = 1;
			}
			for (float t4 = 0f; t4 < wait; t4 += Time.deltaTime * (1f - BurnedPct * 0.75f))
			{
				float pct4 = t4 / wait;
				float cos3 = Mathf.Cos((pct4 * 2f * (float)n - 1f) * (float)Math.PI) * 0.5f + 0.5f;
				base.transform.position = Vector3.Lerp(startPos, pos, cos3);
				base.transform.rotation = Quaternion.Slerp(startRot, rot, cos3);
				yield return null;
			}
			base.transform.position = startPos;
			base.transform.rotation = startRot;
		}
	}

	public IEnumerator Reset()
	{
		swinging = false;
		Vector3 pos = base.transform.position;
		Quaternion rot = base.transform.rotation;
		float dur = fire.fireControllerCode.fullFireDuration;
		for (float t = 0f; t < dur; t += Time.deltaTime)
		{
			float pct = t / dur;
			base.transform.position = Vector3.Lerp(pos, startPos, pct);
			base.transform.rotation = Quaternion.Slerp(rot, startRot, pct);
			yield return null;
		}
		base.transform.position = startPos;
		base.transform.rotation = startRot;
	}

	public bool IsBlocked()
	{
		bool result = false;
		for (int i = 0; i < blockingBranches.Length; i++)
		{
			if (blockingBranches[i].IsStill)
			{
				result = true;
				break;
			}
		}
		return result;
	}
}
