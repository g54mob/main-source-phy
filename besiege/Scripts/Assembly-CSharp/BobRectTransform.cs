using System;
using System.Collections;
using UnityEngine;

public class BobRectTransform : MonoBehaviour
{
	public RectTransform animatingObject;

	public float duration = 0.45f;

	public float animationScale = 1.2f;

	public float squash = 0.05f;

	public float offset;

	private bool animate;

	private Coroutine plusAnim;

	private void Start()
	{
		animate = true;
		if (plusAnim == null)
		{
			plusAnim = ReferenceMaster.Instance.StartCoroutine(Animate());
		}
	}

	private void Update()
	{
		if (!animatingObject.gameObject.activeInHierarchy && animate)
		{
			animate = false;
		}
	}

	private IEnumerator Animate()
	{
		RectTransform rect = animatingObject;
		Vector2 pos = rect.anchoredPosition;
		yield return new WaitForSeconds(offset * duration);
		while (animate)
		{
			for (float t = 0f; t < duration; t += Time.unscaledDeltaTime)
			{
				if (!animate)
				{
					break;
				}
				float pct = t / duration;
				float sin = Mathf.Sin(pct * (float)Math.PI);
				float invSin = 1f - sin;
				if (rect == null)
				{
					plusAnim = null;
					yield break;
				}
				rect.anchoredPosition = pos + new Vector2(0f, sin * 14f - 9f) * animationScale;
				rect.localScale = new Vector3(1f + Mathf.Pow(invSin, 3f) * 1.6f * squash, 1f + sin * 1.2f * squash, 1f);
				yield return null;
			}
		}
		rect.anchoredPosition = pos;
		rect.localScale = Vector3.one;
		plusAnim = null;
	}
}
