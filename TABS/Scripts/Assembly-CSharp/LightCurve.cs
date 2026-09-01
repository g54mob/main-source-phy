using System.Collections;
using UnityEngine;

public class LightCurve : MonoBehaviour
{
	private Light light;

	public AnimationCurve curve;

	private void Start()
	{
		light = GetComponent<Light>();
	}

	public void DoAnim()
	{
		StartCoroutine(PlayCurve());
	}

	private IEnumerator PlayCurve()
	{
		float c = 0f;
		float t = curve.keys[curve.keys.Length - 1].time;
		while (c < t)
		{
			c += Time.deltaTime;
			light.intensity = curve.Evaluate(c);
			yield return null;
		}
	}
}
