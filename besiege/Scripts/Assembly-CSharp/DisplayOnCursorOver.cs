using System;
using System.Collections;
using UnityEngine;

public class DisplayOnCursorOver : MonoBehaviour
{
	public CursorHoverHook[] triggers;

	public GameObject parent;

	public Renderer[] renderers;

	private float[] defaultAlpha;

	private float currentAlpha;

	private int hovers;

	private Coroutine evaluater;

	private Coroutine fader;

	private void Awake()
	{
		defaultAlpha = new float[renderers.Length];
		for (int i = 0; i < renderers.Length; i++)
		{
			defaultAlpha[i] = GetColor(renderers[i]).a;
		}
		parent.SetActive(true);
		SetAlpha(0f);
		CursorHoverHook[] array = triggers;
		foreach (CursorHoverHook cursorHoverHook in array)
		{
			cursorHoverHook.onCursorEnter = (Action)Delegate.Combine(cursorHoverHook.onCursorEnter, new Action(HoverIncrease));
			cursorHoverHook.onCursorExit = (Action)Delegate.Combine(cursorHoverHook.onCursorExit, new Action(HoverDecrease));
		}
	}

	private void OnDestroy()
	{
		CursorHoverHook[] array = triggers;
		foreach (CursorHoverHook cursorHoverHook in array)
		{
			if ((bool)cursorHoverHook)
			{
				cursorHoverHook.onCursorEnter = (Action)Delegate.Remove(cursorHoverHook.onCursorEnter, new Action(HoverIncrease));
				cursorHoverHook.onCursorExit = (Action)Delegate.Remove(cursorHoverHook.onCursorExit, new Action(HoverDecrease));
			}
		}
		if ((bool)parent)
		{
			parent.SetActive(false);
		}
	}

	private void HoverIncrease()
	{
		hovers++;
		Evaluate();
	}

	private void HoverDecrease()
	{
		hovers--;
		Evaluate();
	}

	private void Evaluate()
	{
		if (evaluater != null)
		{
			StopCoroutine(evaluater);
		}
		evaluater = StartCoroutine(EvaluateAtEnd());
	}

	private IEnumerator EvaluateAtEnd()
	{
		yield return new WaitForEndOfFrame();
		if (hovers < 0)
		{
			hovers = 0;
		}
		if (hovers > triggers.Length)
		{
			hovers = triggers.Length;
		}
		bool display = hovers > 0;
		bool displaying = currentAlpha > 0f;
		if (display != displaying)
		{
			if (display)
			{
				FadeIn();
			}
			else
			{
				FadeOut();
			}
		}
		evaluater = null;
	}

	private void FadeIn()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (fader != null)
			{
				StopCoroutine(fader);
			}
			fader = StartCoroutine(Fade(0.15f, 1f));
		}
		else
		{
			SetAlpha(1f);
		}
	}

	private void FadeOut()
	{
		if (base.gameObject.activeInHierarchy)
		{
			if (fader != null)
			{
				StopCoroutine(fader);
			}
			fader = StartCoroutine(Fade(0.15f, 0f));
		}
		else
		{
			SetAlpha(0f);
		}
	}

	private IEnumerator Fade(float duration, float targetAlpha)
	{
		float startAlpha = currentAlpha;
		float other = ((!(targetAlpha > 0f)) ? 1f : 0f);
		duration = Mathf.InverseLerp(other, targetAlpha, targetAlpha) * duration;
		for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
		{
			float pct = t / duration;
			SetAlpha(Mathf.Lerp(startAlpha, targetAlpha, pct));
			yield return null;
		}
		SetAlpha(targetAlpha);
		fader = null;
	}

	public void SetAlpha(float alpha)
	{
		currentAlpha = alpha;
		SetRenderers(currentAlpha);
	}

	private void SetRenderers(float alpha)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			SetRenderer(i, alpha);
		}
	}

	private void SetRenderer(int i, float alpha)
	{
		Renderer ren = renderers[i];
		Color color = GetColor(ren);
		color.a = alpha * defaultAlpha[i];
		SetColor(ren, color);
	}

	private Color GetColor(Renderer ren)
	{
		if (ren.material.HasProperty("_Color"))
		{
			return ren.material.color;
		}
		if (ren.material.HasProperty("_TintColor"))
		{
			return ren.material.GetColor("_TintColor");
		}
		return Color.white;
	}

	private void SetColor(Renderer ren, Color c)
	{
		if (ren.material.HasProperty("_Color"))
		{
			ren.material.color = c;
		}
		else if (ren.material.HasProperty("_TintColor"))
		{
			ren.material.SetColor("_TintColor", c);
		}
	}
}
