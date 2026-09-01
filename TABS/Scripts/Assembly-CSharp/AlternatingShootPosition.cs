using System.Collections;
using UnityEngine;

public class AlternatingShootPosition : MonoBehaviour
{
	public bool scaleWithCurve = true;

	public AnimationCurve curve;

	public float delay = 4f;

	private Vector3 startScale;

	private void Start()
	{
		startScale = base.transform.localScale;
	}

	public void Shoot()
	{
		if (scaleWithCurve)
		{
			StartCoroutine(ScaleShootPos());
		}
	}

	private IEnumerator ScaleShootPos()
	{
		base.transform.localScale = Vector3.zero;
		yield return new WaitForSeconds(delay);
		float c = 0f;
		float t = curve[curve.length - 1].time;
		while (c < t)
		{
			c += Time.deltaTime;
			base.transform.localScale = curve.Evaluate(c) * startScale;
			yield return null;
		}
	}
}
