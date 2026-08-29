using UnityEngine;
using UnityEngine.UI;

public class FlowLayoutGroup : LayoutGroup
{
	public enum Corner
	{
		UpperLeft = 0,
		UpperRight = 1,
		LowerLeft = 2,
		LowerRight = 3
	}

	public enum Constraint
	{
		Flexible = 0,
		FixedColumnCount = 1,
		FixedRowCount = 2
	}

	protected Vector2 m_CellSize = new Vector2(100f, 100f);

	[SerializeField]
	protected Vector2 m_Spacing = Vector2.zero;

	[SerializeField]
	protected bool m_Horizontal = true;

	public Vector2 cellSize
	{
		get
		{
			return m_CellSize;
		}
		set
		{
			SetProperty(ref m_CellSize, value);
		}
	}

	public Vector2 spacing
	{
		get
		{
			return m_Spacing;
		}
		set
		{
			SetProperty(ref m_Spacing, value);
		}
	}

	public bool horizontal
	{
		get
		{
			return m_Horizontal;
		}
		set
		{
			SetProperty(ref m_Horizontal, value);
		}
	}

	protected FlowLayoutGroup()
	{
	}

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();
		int num = Mathf.CeilToInt(Mathf.Sqrt(base.rectChildren.Count));
		SetLayoutInputForAxis((float)base.padding.horizontal + cellSize.x, (float)base.padding.horizontal + (cellSize.x + spacing.x) * (float)num - spacing.x, -1f, 0);
	}

	public override void CalculateLayoutInputVertical()
	{
		float num = (float)base.padding.vertical + cellSize.y;
		SetLayoutInputForAxis(num, num, -1f, 1);
	}

	public override void SetLayoutHorizontal()
	{
		SetCellsAlongAxis();
	}

	public override void SetLayoutVertical()
	{
		SetCellsAlongAxis();
	}

	private void SetCellsAlongAxis()
	{
		float num = base.rectTransform.rect.size.x - (float)base.padding.horizontal;
		float num2 = base.rectTransform.rect.size.y - (float)base.padding.vertical;
		float num3 = 0f;
		float num4 = 0f;
		float num5 = 0f;
		int num6 = ((cellSize.x + spacing.x <= 0f) ? int.MaxValue : Mathf.Max(1, Mathf.FloorToInt((num + spacing.x + 0.001f) / (cellSize.x + spacing.x))));
		int value = ((cellSize.y + spacing.y <= 0f) ? int.MaxValue : Mathf.Max(1, Mathf.FloorToInt((num2 + spacing.y + 0.001f) / (cellSize.y + spacing.y))));
		int num7 = Mathf.Clamp(num6, 1, base.rectChildren.Count);
		int num8 = Mathf.Clamp(value, 1, Mathf.CeilToInt((float)base.rectChildren.Count / (float)num6));
		Vector2 vector = new Vector2((float)num7 * cellSize.x + (float)(num7 - 1) * spacing.x, (float)num8 * cellSize.y + (float)(num8 - 1) * spacing.y);
		Vector2 vector2 = new Vector2(GetStartOffset(0, vector.x), GetStartOffset(1, vector.y));
		for (int i = 0; i < base.rectChildren.Count; i++)
		{
			SetChildAlongAxis(base.rectChildren[i], 0, vector2.x + num3, base.rectChildren[i].rect.size.x);
			SetChildAlongAxis(base.rectChildren[i], 1, vector2.y + num4, base.rectChildren[i].rect.size.y);
			if (horizontal)
			{
				num3 += base.rectChildren[i].rect.width + spacing[0];
				if (base.rectChildren[i].rect.height > num5)
				{
					num5 = base.rectChildren[i].rect.height;
				}
				if (i < base.rectChildren.Count - 1 && num3 + base.rectChildren[i + 1].rect.width + spacing[0] > num)
				{
					num3 = 0f;
					num4 += num5 + spacing[1];
					num5 = 0f;
				}
			}
			else
			{
				num4 += base.rectChildren[i].rect.height + spacing[1];
				if (base.rectChildren[i].rect.width > num5)
				{
					num5 = base.rectChildren[i].rect.width;
				}
				if (i < base.rectChildren.Count - 1 && num4 + base.rectChildren[i + 1].rect.height + spacing[1] > num2)
				{
					num4 = 0f;
					num3 += num5 + spacing[0];
					num5 = 0f;
				}
			}
		}
		base.rectTransform.sizeDelta = new Vector2(base.rectTransform.sizeDelta.x, num4 + num5 + spacing[1] * 2f);
	}
}
