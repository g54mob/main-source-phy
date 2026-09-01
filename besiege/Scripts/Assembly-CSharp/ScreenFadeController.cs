using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFadeController : MonoBehaviour
{
	[SerializeField]
	protected Image image;

	private Coroutine currentFade;

	private static ScreenFadeController Instance;

	protected void Awake()
	{
		Instance = this;
	}

	public static void Fade(float a, float t, Action onFadeComplete)
	{
		if (Instance.currentFade != null)
		{
			Instance.StopCoroutine(Instance.currentFade);
		}
		Instance.currentFade = Instance.StartCoroutine(Instance.IEFade(a, t, onFadeComplete));
	}

	protected IEnumerator IEFade(float a, float t, Action onFadeComplete)
	{
		float c = 0f;
		Color sc = image.color;
		float sa = sc.a;
		while (c < t)
		{
			image.color = new Color(sc.r, sc.g, sc.b, sa);
			sa = Mathf.Lerp(sa, a, Mathf.Clamp01(c / t));
			c += Time.unscaledDeltaTime;
			yield return null;
		}
		image.color = new Color(sc.r, sc.g, sc.b, a);
		onFadeComplete();
	}
}
