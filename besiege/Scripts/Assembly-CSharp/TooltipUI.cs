using System.Collections;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private RectTransform widgetTransform;

	public float fadeSpeed = 0.1f;

	public float waitTime = 0.1f;

	public float yOffset = -20f;

	public float xOffset;

	public void MouseOver()
	{
		if (base.isActiveAndEnabled)
		{
			StartCoroutine(FadeTooltip());
		}
	}

	public void MouseExit()
	{
		if (base.isActiveAndEnabled)
		{
			StopAllCoroutines();
			SetProgress(0f);
		}
	}

	private void Awake()
	{
		SetProgress(0f);
	}

	private void SetProgress(float alpha)
	{
		canvasGroup.alpha = alpha;
		float num = 1f - alpha;
		widgetTransform.anchoredPosition = new Vector2(xOffset * num, yOffset * num);
	}

	private IEnumerator FadeTooltip()
	{
		yield return new WaitForSeconds(waitTime);
		float cTime = 0f;
		float rate = 1f / fadeSpeed;
		while (cTime < 1f)
		{
			cTime += Time.unscaledDeltaTime * rate;
			SetProgress(cTime);
			yield return 0;
		}
		SetProgress(1f);
	}

	private void OnDisable()
	{
		StopAllCoroutines();
		SetProgress(0f);
	}
}
