using System.Collections;
using UnityEngine;

public class RotateLerpIn : MonoBehaviour
{
	public float duration = 0.3f;

	public float randomRange = 0.08f;

	private Vector3 startScale;

	private void Awake()
	{
		startScale = base.transform.localScale;
		duration += Random.Range(0f - randomRange, randomRange);
	}

	private void Start()
	{
		StartCoroutine(RotateIn());
	}

	private IEnumerator RotateIn()
	{
		float cTime = 0f;
		float rate = 1f / duration;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			base.transform.localScale = Vector3.Lerp(startScale / 3f, startScale, cTime);
			yield return null;
		}
	}
}
