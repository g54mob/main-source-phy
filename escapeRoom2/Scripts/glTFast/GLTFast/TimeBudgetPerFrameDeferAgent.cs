using System.Threading.Tasks;
using UnityEngine;

namespace GLTFast
{
	[DefaultExecutionOrder(-10)]
	public class TimeBudgetPerFrameDeferAgent : MonoBehaviour, IDeferAgent
	{
		[SerializeField]
		[Range(0.01f, 5f)]
		[Tooltip("Per-frame time budget as fraction of the targeted frame time. Keep it well below 0.5, so there's enough time for other game logic and rendering. A value of 1.0 can lead to dropping a full frame. Even higher values can stall for multiple frames.")]
		private float frameBudget = 0.5f;

		private float m_LastTime;

		private float m_TimeBudget = 1f / 60f;

		public void SetFrameBudget(float newFrameBudget = 0.5f)
		{
			frameBudget = newFrameBudget;
			UpdateTimeBudget();
		}

		private void UpdateTimeBudget()
		{
			float num = Application.targetFrameRate;
			if (num < 0f)
			{
				num = 30f;
			}
			m_TimeBudget = frameBudget / num;
			ResetLastTime();
		}

		private void Awake()
		{
			UpdateTimeBudget();
		}

		private void Update()
		{
			ResetLastTime();
		}

		private void ResetLastTime()
		{
			m_LastTime = Time.realtimeSinceStartup;
		}

		public bool ShouldDefer()
		{
			return !FitsInCurrentFrame(0f);
		}

		public bool ShouldDefer(float duration)
		{
			return !FitsInCurrentFrame(duration);
		}

		private bool FitsInCurrentFrame(float duration)
		{
			return duration <= m_TimeBudget - (Time.realtimeSinceStartup - m_LastTime);
		}

		public async Task BreakPoint()
		{
			if (ShouldDefer())
			{
				await Task.Yield();
			}
		}

		public async Task BreakPoint(float duration)
		{
			if (ShouldDefer(duration))
			{
				await Task.Yield();
			}
		}
	}
}
