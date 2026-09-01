using System;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSpringColor : MMSpringDefinition<Color>
	{
		public MMSpringFloat ColorSpring;

		public MMSpringFloat SpringR;

		public MMSpringFloat SpringG;

		public MMSpringFloat SpringB;

		public MMSpringFloat SpringA;

		protected Color _returnCurrentValue;

		protected Color _returnTargetValue;

		protected Color _returnVelocity;

		public override Color CurrentValue
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public override Color TargetValue
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public override Color Velocity
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		public virtual void SetDamping(float newDamping)
		{
		}

		public virtual void SetFrequency(float newFrequency)
		{
		}

		public override void UpdateSpringValue(float deltaTime)
		{
		}

		public override void MoveToInstant(Color newValue)
		{
		}

		public override void Stop()
		{
		}

		public override void SetInitialValue(Color newInitialValue)
		{
		}

		public override void RestoreInitialValue()
		{
		}

		public override void SetCurrentValueAsInitialValue()
		{
		}

		public override void MoveTo(Color newValue)
		{
		}

		public override void MoveToAdditive(Color newValue)
		{
		}

		public override void MoveToSubtractive(Color newValue)
		{
		}

		public override void MoveToRandom(Color min, Color max)
		{
		}

		public override void Bump(Color bumpAmount)
		{
		}

		public override void BumpRandom(Color min, Color max)
		{
		}

		public override void Finish()
		{
		}
	}
}
