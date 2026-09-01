using UnityEngine;
using UnityEngine.UI;

public class ContentSizeFitterEx : ContentSizeFitter
{
	public Vector2 sizeMin = new Vector2(0f, 0f);

	public Vector2 sizeMax = new Vector2(1920f, 1080f);

	public override void SetLayoutHorizontal()
	{
		base.SetLayoutHorizontal();
		RectTransform obj = base.transform as RectTransform;
		Vector2 sizeDelta = obj.sizeDelta;
		sizeDelta.x = Mathf.Clamp(sizeDelta.x, sizeMin.x, sizeMax.x);
		obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, sizeDelta.x);
	}

	public override void SetLayoutVertical()
	{
		base.SetLayoutVertical();
		RectTransform obj = base.transform as RectTransform;
		Vector2 sizeDelta = obj.sizeDelta;
		sizeDelta.y = Mathf.Clamp(sizeDelta.y, sizeMin.y, sizeMax.y);
		obj.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, sizeDelta.y);
	}
}
