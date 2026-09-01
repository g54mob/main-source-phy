using System.Collections;
using UnityEngine;

public class PlayCurveRotation : MonoBehaviour
{
	public float multiplier = 1f;

	public void Play(AnimationCurve curve, Vector3 angle)
	{
		StopAllCoroutines();
		StartCoroutine(PlayCurve(curve, angle));
	}

	private IEnumerator PlayCurve(AnimationCurve curve, Vector3 angle)
	{
		float t = curve.keys[curve.keys.Length - 1].time;
		float c = 0f;
		while (c < t)
		{
			float num = curve.Evaluate(c);
			c += Time.deltaTime;
			base.transform.localEulerAngles = angle * num * multiplier;
			yield return null;
		}
	}
}
