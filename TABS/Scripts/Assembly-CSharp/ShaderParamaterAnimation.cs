using System.Collections;
using UnityEngine;

public class ShaderParamaterAnimation : MonoBehaviour
{
	public AnimationCurve parameterCurve;

	public string paramaterName;

	public Material material;

	private float currentTime;

	public void Animate()
	{
		StopAllCoroutines();
		StartCoroutine(AnimateCorutine());
	}

	public void AnimateBackwards()
	{
		StopAllCoroutines();
		StartCoroutine(AnimateBackwardsCorutine());
	}

	private void Start()
	{
		material.SetFloat(paramaterName, parameterCurve.Evaluate(0f));
	}

	private IEnumerator AnimateCorutine()
	{
		float startTime = Time.time;
		while (startTime + parameterCurve.keys[parameterCurve.keys.Length - 1].time > Time.time)
		{
			float time = (currentTime = Time.time - startTime);
			material.SetFloat(paramaterName, parameterCurve.Evaluate(time));
			yield return null;
		}
	}

	private IEnumerator AnimateBackwardsCorutine()
	{
		_ = parameterCurve.keys[parameterCurve.keys.Length - 1].time;
		float t = currentTime;
		while (t > 0f)
		{
			t = (currentTime = Mathf.Lerp(t, 0f, Time.deltaTime * 6.5f));
			material.SetFloat(paramaterName, parameterCurve.Evaluate(t));
			yield return null;
		}
	}

	public void ResetMaterial()
	{
		StopAllCoroutines();
		material.SetFloat(paramaterName, parameterCurve.Evaluate(0f));
	}
}
