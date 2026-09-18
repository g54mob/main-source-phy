using System;
using System.Collections;
using UnityEngine;

public class ShelveHighlight : MonoBehaviour
{
	[SerializeField]
	private Outline outline;

	[SerializeField]
	private float fadeDuration = 0.35f;

	public void SetShelveHighlight(bool isCompleted)
	{
		Color color = (isCompleted ? Color.green : Color.red);
		color.a = 0f;
		Color endColor = color;
		endColor.a = 1f;
		StopAllCoroutines();
		if (isCompleted)
		{
			StartCoroutine(ChangeColor(color, endColor, delegate
			{
				DisableHighlight(isCompleted: true);
			}));
		}
		else
		{
			StartCoroutine(ChangeColor(color, endColor));
		}
	}

	public void DisableHighlight(bool isCompleted)
	{
		Color color = (isCompleted ? Color.green : Color.red);
		color.a = 1f;
		Color endColor = color;
		endColor.a = 0f;
		StopAllCoroutines();
		StartCoroutine(ChangeColor(color, endColor, delegate
		{
			base.gameObject.SetActive(value: false);
		}));
	}

	private IEnumerator ChangeColor(Color startColor, Color endColor, Action Oncomplete = null)
	{
		outline.outlineFillMaterial.SetColor("_OutlineColor", startColor);
		float timer = 0f;
		while (timer < fadeDuration)
		{
			timer += Time.deltaTime;
			outline.outlineFillMaterial.SetColor("_OutlineColor", Color.Lerp(startColor, endColor, timer / fadeDuration));
			yield return null;
		}
		outline.outlineFillMaterial.SetColor("_OutlineColor", endColor);
		Oncomplete?.Invoke();
	}
}
