using System;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class RepeatingSimulationFlow : SimulationFlow
	{
		public bool fixedFrameRate = true;

		[SerializeField]
		private float m_targetFrameRate = 60f;

		[SerializeField]
		private float m_maxUpdateTime = 0.1f;

		[HideInInspector]
		[SerializeField]
		private float m_timeStepSize = 0.016f;

		private float m_nextUpdateTime;

		private float m_firstUpdateTimeOfRun;

		private int m_maximumLoopCount = 480;

		private int m_loopCount;

		public float maxUpdateTime
		{
			get
			{
				return m_maxUpdateTime;
			}
			set
			{
				if (value > 0.1f)
				{
					m_maxUpdateTime = 0.1f;
				}
				else if (value <= 0f)
				{
					m_maxUpdateTime = 0.008f;
				}
				else
				{
					m_maxUpdateTime = value;
				}
				SetFirstFixedFrameData();
			}
		}

		public float targetFrameRate
		{
			get
			{
				return m_targetFrameRate;
			}
			set
			{
				if (value <= 0f)
				{
					m_targetFrameRate = 1f / m_maxUpdateTime;
				}
				else
				{
					m_targetFrameRate = value;
				}
				m_timeStepSize = 1f / m_targetFrameRate;
				CalculateNextStepTime();
			}
		}

		protected void OnValidate()
		{
			targetFrameRate = m_targetFrameRate;
			maxUpdateTime = m_maxUpdateTime;
		}

		protected void OnEnable()
		{
			CalculateNextStepTime();
		}

		public override void RunStep()
		{
			if (!base.enabled)
			{
				return;
			}
			if (fixedFrameRate)
			{
				m_firstUpdateTimeOfRun = m_nextUpdateTime;
				m_loopCount = 0;
				while (m_nextUpdateTime <= Time.time)
				{
					base.RunStep();
					CalculateNextStepTime();
					if (spentTooMuchTime())
					{
						SetFirstFixedFrameData();
						break;
					}
					m_loopCount++;
					if (m_loopCount > m_maximumLoopCount)
					{
						base.enabled = false;
						throw new Exception("Infinite loop prevention. Number of loops exceeds " + m_maximumLoopCount + " which probably can't be handled within one frame.");
					}
				}
			}
			else
			{
				base.RunStep();
			}
		}

		protected void CalculateNextStepTime()
		{
			m_nextUpdateTime += m_timeStepSize;
		}

		protected void SetFirstFixedFrameData()
		{
			m_nextUpdateTime = Time.time + m_timeStepSize;
		}

		protected bool isProbablyFirstUpdate()
		{
			return m_nextUpdateTime == 0f;
		}

		protected bool spentTooMuchTime()
		{
			return m_nextUpdateTime - m_firstUpdateTimeOfRun > m_maxUpdateTime;
		}
	}
}
