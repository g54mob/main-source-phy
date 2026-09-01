using System.Collections;
using UnityEngine;

public class LerpScale : MonoBehaviour
{
	public Transform targetObj;

	public float lerpSpeed;

	public Vector3 targetScale;

	private void Start()
	{
		if (targetObj == null)
		{
			targetObj = base.transform;
		}
		StartCoroutine(LerpSc());
	}

	private IEnumerator LerpSc()
	{
		float cTime = 0f;
		float rate = 1f / lerpSpeed;
		Vector3 startScale = targetObj.localScale;
		while (cTime < 1f)
		{
			cTime += Time.deltaTime * rate;
			targetObj.localScale = Vector3.Lerp(startScale, targetScale, cTime);
			yield return null;
		}
	}
}
