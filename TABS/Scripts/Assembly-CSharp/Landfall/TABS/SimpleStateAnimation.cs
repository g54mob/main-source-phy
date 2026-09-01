using System;
using UnityEngine;

namespace Landfall.TABS
{
	public class SimpleStateAnimation : MonoBehaviour
	{
		public enum State
		{
			State01 = 0,
			State02 = 1
		}

		public bool setToStateAtStart = true;

		public bool m_AnimatePosition = true;

		public Vector3 m_State01LocalPosistion;

		public Vector3 m_State02LocalPosistion;

		public bool m_AnimateScale;

		public Vector3 m_State01LocalScale = Vector3.zero;

		public Vector3 m_State02LocalScale = Vector3.one;

		[Tooltip("By default, only the length of the curve is used to define the duration. To evaluate based on the height of the curve, enable Follow Curve Multiplier below.")]
		public AnimationCurve m_LerpCurve;

		[Tooltip("Set to true to scale the animation by evaluating the curve.")]
		public bool m_FollowCurveMultiplier;

		public State m_state;

		private float m_time;

		private float m_curveLength;

		private bool m_Ongoing;

		private const float TimeMultiplier = 15f;

		public event Action<State> Completed;

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

		private void Start()
		{
			m_curveLength = m_LerpCurve.keys[m_LerpCurve.keys.Length - 1].time;
			m_Ongoing = false;
			if (!setToStateAtStart)
			{
				return;
			}
			if (m_state == State.State01)
			{
				if (m_AnimatePosition)
				{
					base.transform.localPosition = m_State01LocalPosistion;
				}
				if (m_AnimateScale)
				{
					base.transform.localScale = m_State01LocalScale;
				}
				m_time = 0f;
			}
			else
			{
				if (m_AnimatePosition)
				{
					base.transform.localPosition = m_State02LocalPosistion;
				}
				if (m_AnimateScale)
				{
					base.transform.localScale = m_State02LocalScale;
				}
				m_time = m_curveLength;
			}
		}

		public void SetState(State newState)
		{
			m_state = newState;
			m_Ongoing = true;
		}

		private void Complete()
		{
			if (m_Ongoing)
			{
				this.Completed?.Invoke(m_state);
			}
			m_Ongoing = false;
		}

		private void Update()
		{
			if (m_state == State.State01)
			{
				if (m_time > 0f)
				{
					m_time -= Time.unscaledDeltaTime;
				}
				else
				{
					m_time = 0f;
					Complete();
					m_Ongoing = false;
				}
			}
			else if (m_time <= m_curveLength)
			{
				m_time += Time.unscaledDeltaTime;
			}
			else
			{
				m_time = m_curveLength;
				Complete();
				m_Ongoing = false;
			}
			if (!(Mathf.Abs(m_curveLength) <= Mathf.Epsilon))
			{
				float t = m_time / m_curveLength;
				float num = (m_FollowCurveMultiplier ? m_LerpCurve.Evaluate(m_time) : 1f);
				if (m_AnimatePosition)
				{
					Vector3 b = Vector3.Lerp(m_State01LocalPosistion, m_State02LocalPosistion, t) * num;
					base.transform.localPosition = Vector3.Lerp(base.transform.localPosition, b, 15f * Time.unscaledDeltaTime);
				}
				if (m_AnimateScale)
				{
					Vector3 localScale = Vector3.Lerp(m_State01LocalScale, m_State02LocalScale, t) * num;
					base.transform.localScale = localScale;
				}
			}
		}
	}
}
