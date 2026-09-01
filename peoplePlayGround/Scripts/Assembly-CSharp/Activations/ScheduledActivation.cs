using System;

namespace Activations
{
	[Serializable]
	public struct ScheduledActivation
	{
		public float TimeRemaining;

		public readonly ActivationEvent Activation;

		public ScheduledActivation(float timeRemaining, in ActivationEvent activation)
		{
			TimeRemaining = timeRemaining;
			Activation = activation;
		}

		public readonly bool HasExpired()
		{
			return TimeRemaining < float.Epsilon;
		}

		public static bool HasExpiredPredicate(ScheduledActivation s)
		{
			return s.HasExpired();
		}
	}
}
