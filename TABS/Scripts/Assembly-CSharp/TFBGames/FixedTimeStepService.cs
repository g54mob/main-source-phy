using UnityEngine;
using UnityEngine.Serialization;

namespace TFBGames
{
	public class FixedTimeStepService : ServicePrefab
	{
		public enum FixedTimeStep
		{
			SixtyUpdates = 0,
			ThirtyUpdates = 1
		}

		public const float SixtyUpdatesPerSec = 1f / 60f;

		public const float ThirtyUpdatesPerSec = 1f / 30f;

		[SerializeField]
		[FormerlySerializedAs("smallForceAdjustmentCoefficient")]
		private float smallForceAdjustmentDivisor = 3.9760838f;

		[SerializeField]
		[FormerlySerializedAs("largeForceAdjustmentCoefficient")]
		private float largeForceAdjustmentDivisor = 1.9880419f;

		[SerializeField]
		[FormerlySerializedAs("torqueAdjustmentCoefficient")]
		private float torqueAdjustmentDivisor = 2f;

		private static float largeForceCoefficient = 1f;

		private static float smallForceCoefficient = 1f;

		private static float torqueCoefficient = 1f;

		private float previousFixedDeltaTime;

		public static float LargeForceCoefficient => largeForceCoefficient;

		public static float SmallForceCoefficient => smallForceCoefficient;

		public static float TorqueCoefficient => torqueCoefficient;

		public FixedTimeStep CurrentFixedTimeStep
		{
			get
			{
				if (Time.fixedDeltaTime < 0.025f)
				{
					return FixedTimeStep.SixtyUpdates;
				}
				return FixedTimeStep.ThirtyUpdates;
			}
		}

		public override void OnAwake()
		{
			UpdateForceCoefficients();
		}

		public override void OnUpdate()
		{
			if (!Mathf.Approximately(previousFixedDeltaTime, Time.fixedDeltaTime))
			{
				UpdateForceCoefficients();
			}
		}

		private void UpdateForceCoefficients()
		{
			switch (CurrentFixedTimeStep)
			{
			case FixedTimeStep.SixtyUpdates:
				SetCoefficientsFor60FixedUpdatesPerSecond();
				break;
			case FixedTimeStep.ThirtyUpdates:
				SetCoefficientsFor30FixedUpdatesPerSecond();
				break;
			default:
				Debug.LogError($"This FixedTimeStep ({CurrentFixedTimeStep}) has not been handled. This should not happen." + "Defaulting to 60 fixed updates per second.");
				Time.maximumDeltaTime = 1f / 60f;
				break;
			}
			previousFixedDeltaTime = Time.fixedDeltaTime;
		}

		private void SetCoefficientsFor30FixedUpdatesPerSecond()
		{
			largeForceCoefficient = 1f / smallForceAdjustmentDivisor;
			smallForceCoefficient = 1f / largeForceAdjustmentDivisor;
			torqueCoefficient = 1f / torqueAdjustmentDivisor;
		}

		private void SetCoefficientsFor60FixedUpdatesPerSecond()
		{
			largeForceCoefficient = 1f;
			smallForceCoefficient = 1f;
			torqueCoefficient = 1f;
		}

		private void SetFixedTimeStepTo30UpdatesPerSecond()
		{
			Time.fixedDeltaTime = 1f / 30f;
		}

		private void SetFixedTimeStepTo60UpdatesPerSecond()
		{
			Time.fixedDeltaTime = 1f / 60f;
		}
	}
}
