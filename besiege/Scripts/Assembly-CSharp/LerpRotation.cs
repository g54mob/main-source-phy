using System.Collections;
using UnityEngine;

public class LerpRotation : MonoBehaviour
{
	public Transform targetObj;

	public float[] lerpSpeeds;

	public Transform[] targetRotations;

	private IEnumerator lerpCoroutine;

	public void ReturnToStartAnim()
	{
		lerpCoroutine = LerpRot(0);
		StartCoroutine(lerpCoroutine);
	}

	public void Anim1()
	{
		lerpCoroutine = LerpRot(1);
		StartCoroutine(lerpCoroutine);
	}

	public void Anim2()
	{
		lerpCoroutine = LerpRot(2);
		StartCoroutine(lerpCoroutine);
	}

	public void StopRotation()
	{
		if (lerpCoroutine != null)
		{
			StopCoroutine(lerpCoroutine);
		}
	}

	private IEnumerator LerpRot(int index)
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeeds[index];
		Quaternion startRot = targetObj.localRotation;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			targetObj.localRotation = Quaternion.Lerp(startRot, targetRotations[index].localRotation, cTime);
			yield return null;
		}
	}
}
