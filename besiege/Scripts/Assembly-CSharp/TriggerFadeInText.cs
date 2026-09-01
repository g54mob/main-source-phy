using System.Collections;
using UnityEngine;

public class TriggerFadeInText : MonoBehaviour
{
	public Renderer rend;

	public float lerpDuration = 0.5f;

	public float visibleDuration = 3f;

	public Color startCol;

	public int blocksInTrigger;

	private void Start()
	{
		startCol = rend.material.GetColor("_TintColor");
		blocksInTrigger = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody != null && (bool)attachedRigidbody.GetComponent<MyBounds>())
		{
			if (!rend.enabled)
			{
				CheckBlockCount();
			}
			blocksInTrigger++;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if (attachedRigidbody != null && (bool)attachedRigidbody.GetComponent<MyBounds>())
		{
			blocksInTrigger--;
		}
	}

	private void CheckBlockCount()
	{
		if (blocksInTrigger < 1)
		{
			StartCoroutine(FadeInOut());
		}
	}

	private IEnumerator FadeInOut()
	{
		rend.enabled = true;
		StartCoroutine(Lerp(0f, startCol.a, lerpDuration));
		yield return new WaitForSeconds(lerpDuration + visibleDuration);
		StartCoroutine(Lerp(startCol.a, 0f, lerpDuration));
		yield return new WaitForSeconds(lerpDuration);
		rend.enabled = false;
	}

	private IEnumerator Lerp(float startAlpha, float endAlpha, float fadeSpeed)
	{
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		Color newCol = startCol;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			newCol.a = Mathf.Lerp(startAlpha, endAlpha, cTime);
			rend.material.SetColor("_TintColor", newCol);
			yield return null;
		}
	}
}
