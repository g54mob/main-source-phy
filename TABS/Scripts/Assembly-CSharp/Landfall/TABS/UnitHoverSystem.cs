using UnityEngine;

namespace Landfall.TABS
{
	public class UnitHoverSystem : InstancedHandler<UnitHoverSystem>
	{
		public RectTransform m_UnitHover;

		public Camera m_Camera;

		private Unit m_currentUnit;

		private float m_startedHover;

		private RectTransform m_canvasRect;

		private void Start()
		{
			m_canvasRect = base.transform.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
		}

		public static void OnHoverUpdate(Unit unit)
		{
			InstancedHandler<UnitHoverSystem>.Instance.OnHoverUpdateInternal(unit);
		}

		public void OnHoverUpdateInternal(Unit unit)
		{
			if (m_currentUnit != unit)
			{
				m_startedHover = Time.time;
			}
			m_currentUnit = unit;
		}

		private void Update()
		{
			if ((bool)m_currentUnit && Time.time > m_startedHover + 0.5f)
			{
				Vector2 vector = m_Camera.WorldToViewportPoint(m_currentUnit.data.head.transform.position + Vector3.right * 0.2f + Vector3.up * 0.4f);
				Vector2 anchoredPosition = new Vector2(vector.x * m_canvasRect.sizeDelta.x - m_canvasRect.sizeDelta.x * 0.5f, vector.y * m_canvasRect.sizeDelta.y - m_canvasRect.sizeDelta.y * 0.5f);
				m_UnitHover.anchoredPosition = anchoredPosition;
			}
		}
	}
}
