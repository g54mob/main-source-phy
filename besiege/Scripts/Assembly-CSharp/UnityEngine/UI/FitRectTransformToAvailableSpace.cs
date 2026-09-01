using System;

namespace UnityEngine.UI
{
	public class FitRectTransformToAvailableSpace : MonoBehaviour
	{
		public enum Axis
		{
			None = 0,
			Width = 1,
			Height = 2
		}

		public Axis dimension;

		public float size;

		[NonSerialized]
		private RectTransform m_Rect;

		private RectTransform rectTransform
		{
			get
			{
				if (m_Rect == null)
				{
					m_Rect = GetComponent<RectTransform>();
				}
				return m_Rect;
			}
		}

		protected void Start()
		{
			switch (dimension)
			{
			case Axis.Width:
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
				break;
			case Axis.Height:
				rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
				break;
			}
		}
	}
}
