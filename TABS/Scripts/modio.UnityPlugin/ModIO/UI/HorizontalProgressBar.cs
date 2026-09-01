using UnityEngine;

namespace ModIO.UI
{
	public class HorizontalProgressBar : MonoBehaviour
	{
		[Header("UI Components")]
		[Tooltip("The element to be resized with respect to its parent transform.")]
		public RectTransform barTransform;

		[Header("Display Data")]
		[Range(0f, 1f)]
		[SerializeField]
		private float m_percentComplete;

		public float percentComplete
		{
			get
			{
				return m_percentComplete;
			}
			set
			{
				if (value < 0f)
				{
					value = 0f;
				}
				else if (value > 1f)
				{
					value = 1f;
				}
				m_percentComplete = value;
				UpdateBarSize();
			}
		}

		private RectTransform barParent => barTransform.parent as RectTransform;

		private void OnEnable()
		{
			SetBarTransformValues();
			UpdateBarSize();
		}

		private void SetBarTransformValues()
		{
			Vector2 anchorMin = barTransform.anchorMin;
			barTransform.anchorMin = new Vector2(anchorMin.x, 0f);
			barTransform.anchorMax = new Vector2(anchorMin.x, 1f);
			barTransform.pivot = new Vector2(anchorMin.x, 0.5f);
			barTransform.offsetMin = Vector2.zero;
			barTransform.offsetMax = Vector2.zero;
		}

		private void UpdateBarSize()
		{
			float x = m_percentComplete * barParent.rect.width;
			barTransform.sizeDelta = new Vector2(x, 0f);
		}
	}
}
