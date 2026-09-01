using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnEnable : MonoBehaviour
{
	public Renderer[] renderers;

	public TextMesh[] textMeshes;

	public float lerpInSpeed = 0.8f;

	public bool blurCamAfter;

	public bool unityDeltaTime;

	private List<Color> textMeshOnColours = new List<Color>();

	private List<Color> textMeshOffColours = new List<Color>();

	private List<Color> rendererOnCols = new List<Color>();

	private List<Color> rendererOffCols = new List<Color>();

	private void Awake()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			if (renderers[i].material.HasProperty("_TintColor"))
			{
				rendererOnCols.Add(renderers[i].material.GetColor("_TintColor"));
				Color color = renderers[i].material.GetColor("_TintColor");
				color.a = 0f;
				rendererOffCols.Add(color);
			}
		}
		List<TextMesh> list = new List<TextMesh>();
		for (int j = 0; j < textMeshes.Length; j++)
		{
			if (!(textMeshes[j] == null))
			{
				list.Add(textMeshes[j]);
				textMeshOnColours.Add(textMeshes[j].color);
				Color color2 = textMeshes[j].color;
				color2.a = 0f;
				textMeshOffColours.Add(color2);
			}
		}
		textMeshes = list.ToArray();
		SetAllRenderersOff();
	}

	private void OnEnable()
	{
		StartFade();
	}

	public IEnumerator Disable()
	{
		EndFade();
		yield return new WaitForSeconds(lerpInSpeed);
		base.gameObject.SetActive(false);
	}

	private void StartFade()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			StartCoroutine(FadeIn(i));
		}
		for (int j = 0; j < textMeshes.Length; j++)
		{
			StartCoroutine(FadeInText(j));
		}
	}

	private void EndFade()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			StartCoroutine(FadeOut(i));
		}
		for (int j = 0; j < textMeshes.Length; j++)
		{
			StartCoroutine(FadeOutText(j));
		}
	}

	private IEnumerator FadeIn(int index)
	{
		renderers[index].enabled = true;
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = rendererOffCols[index];
		while (cTime < 1f)
		{
			cTime = ((TimeSlider.Instance != null && !unityDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			Color delegateColor = Color.Lerp(startCol, rendererOnCols[index], cTime);
			renderers[index].material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
		if (blurCamAfter)
		{
			Camera mainCamera = Camera.main;
			Blur blur = mainCamera.GetComponent<Blur>();
			if (blur != null)
			{
				blur.enabled = true;
			}
		}
	}

	private IEnumerator FadeOut(int index)
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = renderers[index].material.GetColor("_TintColor");
		while (cTime < 1f)
		{
			cTime = ((!unityDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			Color delegateColor = Color.Lerp(startCol, rendererOffCols[index], cTime);
			renderers[index].material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
		renderers[index].enabled = false;
	}

	private IEnumerator FadeInText(int index)
	{
		textMeshes[index].GetComponent<Renderer>().enabled = true;
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = textMeshOffColours[index];
		while (cTime < 1f)
		{
			cTime = ((!unityDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			textMeshes[index].color = Color.Lerp(startCol, textMeshOnColours[index], cTime);
			yield return null;
		}
	}

	private IEnumerator FadeOutText(int index)
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = textMeshes[index].color;
		while (cTime < 1f)
		{
			cTime = ((!unityDeltaTime) ? (cTime + TimeSlider.Instance.deltaTime * rate) : (cTime + Time.deltaTime * rate));
			textMeshes[index].color = Color.Lerp(startCol, textMeshOffColours[index], cTime);
			yield return null;
		}
		textMeshes[index].GetComponent<Renderer>().enabled = false;
	}

	private void SetAllRenderersOff()
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = false;
			renderers[i].material.SetColor("_TintColor", rendererOffCols[i]);
		}
		for (int j = 0; j < textMeshes.Length; j++)
		{
			textMeshes[j].color = textMeshOffColours[j];
			textMeshes[j].GetComponent<Renderer>().enabled = false;
		}
	}
}
