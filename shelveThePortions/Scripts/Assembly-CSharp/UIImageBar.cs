using UnityEngine;

public class UIImageBar : MonoBehaviour
{
	[SerializeField]
	private bool isVertical = true;

	[SerializeField]
	private float minPos;

	[SerializeField]
	private float maxPos;

	[SerializeField]
	private float minSize;

	[SerializeField]
	private float maxSize;

	[SerializeField]
	private RectTransform rectTransform;

	public void SetPercent(float percent)
	{
		percent = Mathf.Clamp01(percent);
		Vector3 localPosition = rectTransform.localPosition;
		Vector2 sizeDelta = rectTransform.sizeDelta;
		if (isVertical)
		{
			localPosition.y = Mathf.Lerp(minPos, maxPos, percent);
			sizeDelta.y = Mathf.Lerp(minSize, maxSize, percent);
		}
		else
		{
			localPosition.x = Mathf.Lerp(minPos, maxPos, percent);
			sizeDelta.x = Mathf.Lerp(minSize, maxSize, percent);
		}
		rectTransform.localPosition = localPosition;
		rectTransform.sizeDelta = sizeDelta;
	}
}
