using System.Collections;
using UnityEngine;

public class SwappingCanvasGroups : MonoBehaviour
{
	public CanvasGroup[] groups;

	public float wait = 5f;

	public float transition = 1f;

	private int current;

	private void OnEnable()
	{
		StartCoroutine(Animate());
	}

	private IEnumerator Animate()
	{
		yield return new WaitForSecondsRealtime(wait / 2f);
		while (base.enabled)
		{
			yield return new WaitForSecondsRealtime(wait);
			int next = (current + 1) % groups.Length;
			for (float t = 0f; t < transition; t += Time.unscaledDeltaTime)
			{
				float pct = t / transition;
				groups[current].alpha = Mathf.Lerp(1f, 0f, pct);
				groups[next].alpha = Mathf.Lerp(0f, 1f, pct);
				yield return null;
			}
			groups[current].alpha = 0f;
			groups[next].alpha = 1f;
			current = next;
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < groups.Length; i++)
		{
			groups[i].alpha = ((i != current) ? 0f : 1f);
		}
	}
}
