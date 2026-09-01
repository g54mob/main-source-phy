using UnityEngine;

namespace Landfall.TABS
{
	public class ThreeStateUIAnimation : MonoBehaviour
	{
		public enum State
		{
			State01 = 0,
			State02 = 1
		}

		private RectTransform m_rectTransform;

		public Vector2 Pos01;

		public Vector2 Pos02;

		public Vector2 Pos03;

		public AnimationCurve m_LerpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		public State m_state;

		private float m_time;

		private float m_curveLength;

		private bool m_ongoing;

		public RectTransform RectTransform => base.transform as RectTransform;

		public void SetPos01()
		{
			Pos01 = RectTransform.anchoredPosition;
		}

		public void SetPos02()
		{
			Pos02 = RectTransform.anchoredPosition;
		}

		public void SetPos03()
		{
			Pos03 = RectTransform.anchoredPosition;
		}

		public void SetState(State newState)
		{
			m_state = newState;
		}

		private void Update()
		{
			if (m_state == State.State01)
			{
				if (m_time > 0f)
				{
					m_time -= Time.deltaTime;
				}
				else
				{
					m_time = 0f;
				}
			}
			else if (m_time < m_curveLength)
			{
				m_time += Time.deltaTime;
			}
			else
			{
				m_time = m_curveLength;
			}
			Vector2 b = Vector2.Lerp(Pos01, Pos02, m_time / m_curveLength);
			RectTransform.anchoredPosition = Vector2.Lerp(RectTransform.anchoredPosition, b, 15f * Time.deltaTime);
		}
	}
}
