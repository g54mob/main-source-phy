using UnityEngine;

namespace TFBG
{
	public class CountdownTimerService : ServicePrefab, IDisruptionServiceSubscriber
	{
		public delegate void CounterEnded();

		private bool canCountDown;

		private float timerDuration;

		private bool countingDown;

		private float elapsedTime;

		private float timeLeft;

		public float TimeLeft => timeLeft;

		public bool IsCountingDown => countingDown;

		public event CounterEnded OnCounterEnded;

		private void Start()
		{
			timeLeft = 0f;
			canCountDown = true;
		}

		private void Update()
		{
			if (countingDown)
			{
				elapsedTime += Time.unscaledDeltaTime;
				timeLeft = timerDuration - elapsedTime;
				if (elapsedTime >= timerDuration)
				{
					EndTimer();
				}
			}
		}

		public void EndTimer()
		{
			timeLeft = 0f;
			elapsedTime = 0f;
			canCountDown = true;
			countingDown = false;
			this.OnCounterEnded?.Invoke();
		}

		public void BeginCountDown(float seconds)
		{
			if (canCountDown)
			{
				timerDuration = seconds;
				elapsedTime = 0f;
				countingDown = true;
				canCountDown = false;
			}
		}

		public void Subscribe()
		{
		}

		public void Unsubscribe()
		{
			if (countingDown)
			{
				countingDown = false;
			}
		}
	}
}
