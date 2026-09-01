using System;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSpringFloat : MMSpringDefinition<float>
	{
		[Tooltip("the dumping ratio determines how fast the spring will evolve after a disturbance. At a low value, it'll oscillate for a long time, while closer to 1 it'll stop oscillating quickly")]
		[Range(0.01f, 1f)]
		public float Damping;

		[Tooltip("the frequency determines how fast the spring will oscillate when disturbed, low frequency means less oscillations per second, high frequency means more oscillations per second")]
		public float Frequency;

		public MMSpringClampSettings ClampSettings;

		public MMSpringDebug SpringDebug;

		[MMHidden]
		public bool UnifiedSpring;

		[MMHidden]
		public float CurrentValueDisplay;

		[MMHidden]
		public float TargetValueDisplay;

		[MMHidden]
		public float VelocityDisplay;

		protected float _actualCurrentValue;

		protected float _returnCurrentValue;

		protected float _targetValue;

		protected float _velocity;

		[MMInspectorGroup("Debug", true, 19, true)]
		[Tooltip("the current value of this spring")]
		public override float CurrentValue
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Tooltip("the value towards which this spring is trending, and that it'll reach once it stops oscillating")]
		public override float TargetValue
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[Tooltip("the current velocity of the spring")]
		public override float Velocity
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public float InitialValue { get; protected set; }

		public override void UpdateSpringValue(float deltaTime)
		{
		}

		protected virtual void HandleClampMode()
		{
		}

		protected virtual void UpdateSpringDebug()
		{
		}

		public override void MoveToInstant(float newValue)
		{
		}

		public override void Stop()
		{
		}

		public override void SetInitialValue(float newInitialValue)
		{
		}

		public override void RestoreInitialValue()
		{
		}

		public override void SetCurrentValueAsInitialValue()
		{
		}

		public override void MoveTo(float newValue)
		{
		}

		public override void MoveToAdditive(float newValue)
		{
		}

		public override void MoveToSubtractive(float newValue)
		{
		}

		public override void MoveToRandom(float min, float max)
		{
		}

		public override void Bump(float bumpAmount)
		{
		}

		public override void BumpRandom(float min, float max)
		{
		}

		public override void Finish()
		{
		}
	}
}
