using UnityEngine;
using UnityEngine.UI;

public class DMRankedLayout : LayoutGroup
{
	public Vector2 spacing;

	public float xOffset;

	public float yOffset;

	public float scaleDecrease = 0.3f;

	public Vector2 cellSize;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();
		m_Tracker.Clear();
		if (base.rectChildren.Count > 0)
		{
			SetChild(base.rectChildren[0], 0f, 0f, 1f);
		}
		int num = 1;
		int num2 = 1;
		for (int i = 1; i < base.rectChildren.Count; i++)
		{
			RectTransform child = base.rectChildren[i];
			float num3 = 1f - (float)num * scaleDecrease;
			float xPos = (spacing.x * (float)num + num3 * xOffset) * (float)num2;
			num2 *= -1;
			float yPos = spacing.y * (float)num + num3 * yOffset;
			SetChild(child, xPos, yPos, num3);
			num += Mathf.Max(0, num2);
		}
	}

	private void SetChild(RectTransform child, float xPos, float yPos, float scale)
	{
		child.anchorMax = new Vector2(0.5f, 0.5f);
		child.anchorMin = new Vector2(0.5f, 0.5f);
		child.localPosition = new Vector3(xPos, yPos, 0f);
		child.sizeDelta = cellSize * scale;
	}

	public override void CalculateLayoutInputVertical()
	{
	}

	public override void SetLayoutHorizontal()
	{
	}

	public override void SetLayoutVertical()
	{
	}
}
