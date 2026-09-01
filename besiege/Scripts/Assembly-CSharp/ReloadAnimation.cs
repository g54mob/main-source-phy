using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class ReloadAnimation
{
	public GameObject parent;

	public Renderer[] ammoObjects;

	public Renderer glow;

	public AnimationCurve alphaCurve;

	public AudioSource reloadSound;

	public float duration = 0.5f;

	public float soundOffset = 0.3f;

	public float ammoOffset = 0.1f;

	[HideInInspector]
	public Vector3[] startPos;

	[HideInInspector]
	public Color[] startColor;

	[HideInInspector]
	public Color glowColor;

	protected Coroutine reloadAnim;

	protected Coroutine[] routines = new Coroutine[0];

	protected MonoBehaviour source;

	public void Awake(MonoBehaviour obj)
	{
		source = obj;
		startPos = new Vector3[ammoObjects.Length];
		startColor = new Color[ammoObjects.Length];
		for (int i = 0; i < ammoObjects.Length; i++)
		{
			startPos[i] = ammoObjects[i].transform.localPosition;
			startColor[i] = ammoObjects[i].material.color;
		}
		glowColor = glow.material.GetColor("_TintColor");
		parent.SetActive(false);
	}

	public void AnimateReload(int count)
	{
		if (count != 0 && reloadAnim == null)
		{
			reloadAnim = source.StartCoroutine(AnimateReloadRoutine(duration, count));
		}
	}

	protected IEnumerator AnimateReloadRoutine(float duration, int count)
	{
		parent.SetActive(true);
		int max = ((count >= ammoObjects.Length) ? ammoObjects.Length : count);
		source.StartCoroutine(AnimateGlow(duration + (float)(max - 1) * ammoOffset));
		for (int i = 0; i < ammoObjects.Length; i++)
		{
			Color c = startColor[i];
			ammoObjects[i].material.color = new Color(c.r, c.g, c.b, 0f);
		}
		routines = new Coroutine[max];
		for (int j = 0; j < max; j++)
		{
			routines[j] = source.StartCoroutine(AnimateAmmo(j, duration));
			if (j + 1 < max)
			{
				yield return new WaitForSecondsRealtime(ammoOffset);
			}
		}
		yield return routines[routines.Length - 1];
		parent.SetActive(false);
		for (int k = 0; k < max; k++)
		{
			ammoObjects[k].material.color = startColor[k];
			ammoObjects[k].transform.localPosition = startPos[k];
		}
		reloadAnim = null;
		routines = new Coroutine[0];
	}

	protected IEnumerator AnimateGlow(float duration)
	{
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			Color c = glowColor;
			glow.material.SetColor("_TintColor", new Color(c.r, c.g, c.b, Mathf.Lerp(c.a, 0f, pct)));
			yield return null;
		}
	}

	protected IEnumerator AnimateAmmo(int i, float duration)
	{
		bool played = false;
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			if (pct > soundOffset && !played)
			{
				reloadSound.Stop();
				reloadSound.Play();
				played = true;
			}
			Color c = startColor[i];
			ammoObjects[i].material.color = new Color(c.r, c.g, c.b, alphaCurve.Evaluate(pct) * c.a);
			ammoObjects[i].transform.localPosition = Vector3.Lerp(startPos[i], Vector3.zero, pct);
			yield return null;
		}
	}

	public void StopReloadAnim()
	{
		if (reloadAnim != null)
		{
			source.StopCoroutine(reloadAnim);
			reloadAnim = null;
		}
		for (int i = 0; i < routines.Length; i++)
		{
			if (routines[i] != null)
			{
				source.StopCoroutine(routines[i]);
			}
		}
		routines = new Coroutine[0];
		parent.SetActive(false);
		for (int j = 0; j < ammoObjects.Length; j++)
		{
			ammoObjects[j].material.color = startColor[j];
			ammoObjects[j].transform.localPosition = startPos[j];
		}
	}
}
