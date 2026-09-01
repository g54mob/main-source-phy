using UnityEngine;
using UnityEngine.Events;

namespace MoreMountains.Tools
{
	public class MMPeriodicExecution : MonoBehaviour
	{
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 RandomIntervalDuration;

		public UnityEvent OnRandomInterval;

		protected float _lastUpdateAt;

		protected float _currentInterval;

		protected virtual void Start()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void DetermineNewInterval()
		{
		}
	}
}
