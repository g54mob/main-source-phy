using System;
using UnityEngine;

namespace Landfall.TABS
{
	public class UIMovementAnimation : MonoBehaviour
	{
		public enum State
		{
			State01 = 1,
			State02 = 2
		}

		private RectTransform m_rectTransform;

		public Vector2 Pos01;

		public Vector2 Pos02;

		public AnimationCurve m_LerpCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		public State m_state = State.State01;

		private float m_time;

		private float m_curveLength;

		private bool m_ongoing;

		public bool m_CompleteDone;

		public RectTransform RectTransform => base.transform as RectTransform;

		public event Action OnCompleteState01;

		public event Action OnCompleteState02;

		public void SetPos01()
		{
			Pos01 = RectTransform.anchoredPosition;
		}

		public void SetPos02()
		{
			Pos02 = RectTransform.anchoredPosition;
		}

		public void ToggleState()
		{
			if (m_state == State.State01)
			{
				SetState(State.State02);
			}
			else
			{
				SetState(State.State01);
			}
		}

		public void SetState(int state)
		{
			SetState((State)state);
		}

		public void SetState(State newState)
		{
			m_state = newState;
		}

		private void Awake()
		{
			m_curveLength = m_LerpCurve.keys[m_LerpCurve.keys.Length - 1].time;
			if (m_state == State.State01)
			{
				RectTransform.anchoredPosition = Pos01;
				m_time = 0f;
			}
			else
			{
				RectTransform.anchoredPosition = Pos02;
				m_time = m_curveLength;
			}
		}

		private void Update()
		{
			if (m_state == State.State01)
			{
				if (m_time > 0f)
				{
					m_time -= Time.unscaledDeltaTime;
					m_CompleteDone = false;
				}
				else
				{
					m_time = 0f;
					if (!m_CompleteDone)
					{
						this.OnCompleteState01?.Invoke();
					}
					m_CompleteDone = true;
				}
			}
			else if (m_time < m_curveLength)
			{
				m_time += Time.unscaledDeltaTime;
				m_CompleteDone = false;
			}
			else
			{
				m_time = m_curveLength;
				if (!m_CompleteDone)
				{
					this.OnCompleteState02?.Invoke();
				}
				m_CompleteDone = true;
			}
			Vector2 b = Vector2.Lerp(Pos01, Pos02, m_time / m_curveLength);
			RectTransform.anchoredPosition = Vector2.Lerp(RectTransform.anchoredPosition, b, 15f * Time.unscaledDeltaTime);
		}

		public void ClearOnCompleteState02()
		{
			this.OnCompleteState02 = null;
		}
	}
}
