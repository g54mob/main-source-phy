using System.Collections;
using UnityEngine;

public class StampFanfare : MonoBehaviour, ILevelCompletionAnim
{
	public Transform stampObj;

	public Renderer stampRendy;

	public Renderer[] extra;

	private float[] extraAlpha;

	public float scaleUpAmount = 1.5f;

	public float scaleDuration = 0.2f;

	public float fadeDuration = 0.1f;

	public ParticleSystem dustParticles;

	public float startAlpha;

	public float startWaitDuration = 1f;

	private bool reachedEndLast = true;

	private Vector3 startScale;

	private Color startColour;

	private void Start()
	{
		startScale = stampObj.localScale;
		stampObj.localScale = startScale * scaleUpAmount;
		startAlpha = stampRendy.material.GetColor("_TintColor").a;
		stampRendy.enabled = false;
	}

	public void LevelCompleted()
	{
		StopAllCoroutines();
		if (!reachedEndLast)
		{
			LevelReset();
		}
		StartCoroutine(Stamp());
	}

	public void LevelReset()
	{
		StopAllCoroutines();
		stampObj.localScale = startScale * scaleUpAmount;
		stampRendy.enabled = false;
	}

	private IEnumerator Stamp()
	{
		extraAlpha = new float[extra.Length];
		for (int i = 0; i < extra.Length; i++)
		{
			Color c = extra[i].material.GetColor("_TintColor");
			extraAlpha[i] = c.a;
			c.a = 0f;
			extra[i].material.SetColor("_TintColor", c);
		}
		reachedEndLast = false;
		yield return new WaitForSecondsRealtime(startWaitDuration);
		stampRendy.enabled = true;
		yield return StartCoroutine(LerpAlpha());
		GetComponent<AudioSource>().Play();
		yield return StartCoroutine(LerpSize());
		reachedEndLast = true;
	}

	private IEnumerator LerpSize()
	{
		float cTime = 0f;
		float rate = 1f / scaleDuration;
		Vector3 sizeToBe = startScale * scaleUpAmount;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			stampObj.localScale = Vector3.Lerp(sizeToBe, startScale, cTime);
			yield return null;
		}
		dustParticles.playbackSpeed = 1f / Mathf.Max(Time.timeScale, 0.001f);
		dustParticles.Play();
	}

	private IEnumerator LerpAlpha()
	{
		float cTime = 0f;
		float rate = 1f / fadeDuration;
		startColour = stampRendy.material.GetColor("_TintColor");
		startColour.a = startAlpha;
		Color newCol = startColour;
		newCol.a = 0f;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			stampRendy.material.SetColor("_TintColor", Color.Lerp(newCol, startColour, cTime));
			for (int i = 0; i < extra.Length; i++)
			{
				Color c = extra[i].material.GetColor("_TintColor");
				c.a = extraAlpha[i] * cTime;
				extra[i].material.SetColor("_TintColor", c);
			}
			yield return null;
		}
	}
}
