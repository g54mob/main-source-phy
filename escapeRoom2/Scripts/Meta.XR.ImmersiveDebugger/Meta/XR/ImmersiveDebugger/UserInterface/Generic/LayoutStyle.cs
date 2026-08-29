using UnityEngine;

namespace Meta.XR.ImmersiveDebugger.UserInterface.Generic
{
	public class LayoutStyle : Style
	{
		public enum Layout
		{
			Fixed = 0,
			Fill = 1,
			FillHorizontal = 2,
			FillVertical = 3
		}

		public enum Direction
		{
			Left = 0,
			Right = 1,
			Down = 2,
			Up = 3
		}

		public Direction flexDirection;

		public Layout layout;

		public TextAnchor anchor;

		public TextAnchor pivot;

		public Vector2 size;

		public Vector2 margin;

		public bool useBottomRightMargin;

		public Vector2 bottomRightMargin;

		public float spacing;

		public bool masks;

		public bool adaptHeight;

		public bool autoFitChildren;

		public bool isOverlayCanvas;

		public float LeftMargin => margin.x;

		public float TopMargin => margin.y;

		public float RightMargin
		{
			get
			{
				if (!useBottomRightMargin)
				{
					return margin.x;
				}
				return bottomRightMargin.x;
			}
		}

		public float BottomMargin
		{
			get
			{
				if (!useBottomRightMargin)
				{
					return margin.y;
				}
				return bottomRightMargin.y;
			}
		}

		public Vector2 TopLeftMargin => margin;

		public Vector2 BottomRightMargin
		{
			get
			{
				if (!useBottomRightMargin)
				{
					return margin;
				}
				return bottomRightMargin;
			}
		}

		internal bool SetHeight(float height)
		{
			if (!_instantiated || size.y == height)
			{
				return false;
			}
			size.y = height;
			return true;
		}

		internal bool SetWidth(float width)
		{
			if (!_instantiated || size.x == width)
			{
				return false;
			}
			size.x = width;
			return true;
		}
	}
}
