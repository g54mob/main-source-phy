using System.Collections;
using UnityEngine;

public class LerpAndFade : WarningPopupBase
{
	public float lerpOutSpeed = 0.15f;

	public float lerpInSpeed = 0.15f;

	private Color[] offCols;

	private Color textOffCol;

	protected override void Awake()
	{
		fadeSpeed = lerpInSpeed;
		base.Awake();
		offCols = new Color[rendys.Length];
		for (int i = 0; i < rendys.Length; i++)
		{
			offCols[i] = onCols[i];
			offCols[i].a = 0f;
		}
		textOnCol = textMeshy.color;
		textOffCol = textOnCol;
		textOffCol.a = 0f;
		SetAllRenderersOff();
	}

	protected override void Start()
	{
		parentObjStartPos = parentObj.localPosition;
	}

	public void LerpIn()
	{
		StopAllCoroutines();
		StartCoroutine(WarningOn());
	}

	public void LerpOut()
	{
		StopAllCoroutines();
		WarningOff();
	}

	protected override void WarningOff()
	{
		for (int i = 0; i < rendys.Length; i++)
		{
			StartCoroutine(FadeOut(i));
		}
		StartCoroutine(FadeOutText(textMeshy.GetComponent<Renderer>()));
	}

	protected override IEnumerator WarningOn()
	{
		for (int i = 0; i < rendys.Length; i++)
		{
			StartCoroutine(FadeIn(i));
		}
		StartCoroutine(FadeInText(textMeshy.GetComponent<Renderer>()));
		yield return StartCoroutine(LerpPosIn());
	}

	private IEnumerator FadeIn(int index)
	{
		rendys[index].enabled = true;
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = rendys[index].material.GetColor("_TintColor");
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			Color delegateColor = Color.Lerp(startCol, onCols[index], cTime);
			rendys[index].material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
	}

	private IEnumerator FadeOut(int index)
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = rendys[index].material.GetColor("_TintColor");
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			Color delegateColor = Color.Lerp(startCol, offCols[index], cTime);
			rendys[index].material.SetColor("_TintColor", delegateColor);
			yield return null;
		}
		rendys[index].enabled = false;
	}

	private IEnumerator FadeInText(Renderer rendy)
	{
		rendy.enabled = true;
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = textMeshy.color;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			textMeshy.color = Color.Lerp(startCol, textOnCol, cTime);
			yield return null;
		}
	}

	private IEnumerator FadeOutText(Renderer rendy)
	{
		float cTime = 0f;
		float rate = 1f / lerpInSpeed;
		Color startCol = textMeshy.color;
		while (cTime < 1f)
		{
			cTime += TimeSlider.Instance.deltaTime * rate;
			textMeshy.color = Color.Lerp(startCol, textOffCol, cTime);
			yield return null;
		}
		rendy.enabled = false;
	}

	protected override void SetAllRenderersOff()
	{
		for (int i = 0; i < rendys.Length; i++)
		{
			rendys[i].enabled = false;
			rendys[i].material.SetColor("_TintColor", offCols[i]);
		}
		textMeshy.color = textOffCol;
		textMeshy.GetComponent<Renderer>().enabled = false;
	}
}
