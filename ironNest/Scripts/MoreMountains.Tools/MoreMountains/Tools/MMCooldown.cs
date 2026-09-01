using System;

namespace MoreMountains.Tools
{
	[Serializable]
	public class MMCooldown
	{
		public enum CooldownStates
		{
			Idle = 0,
			Consuming = 1,
			Stopped = 2,
			Refilling = 3
		}

		public delegate void OnStateChangeDelegate(CooldownStates newState);

		public bool Unlimited;

		public float ConsumptionDuration;

		public float PauseOnEmptyDuration;

		public float RefillDuration;

		public bool CanInterruptRefill;

		[MMReadOnly]
		public CooldownStates CooldownState;

		[MMReadOnly]
		public float CurrentDurationLeft;

		public OnStateChangeDelegate OnStateChange;

		protected float _emptyReachedTimestamp;

		public float Progress => 0f;

		public virtual void Initialization()
		{
		}

		public virtual void Start()
		{
		}

		public virtual bool Ready()
		{
			return false;
		}

		public virtual void Stop()
		{
		}

		public virtual void Update()
		{
		}

		protected virtual void ChangeState(CooldownStates newState)
		{
		}
	}
}
