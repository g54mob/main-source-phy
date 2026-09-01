using System;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSpringVector2 : MMSpringDefinition<Vector2>
	{
		public bool SeparateAxis;

		public MMSpringFloat UnifiedSpring;

		public MMSpringFloat SpringX;

		public MMSpringFloat SpringY;

		protected Vector2 _returnCurrentValue;

		protected Vector2 _returnTargetValue;

		protected Vector2 _returnVelocity;

		public override Vector2 CurrentValue
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public override Vector2 TargetValue
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public override Vector2 Velocity
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		public virtual void SetDamping(Vector2 newDamping)
		{
		}

		public virtual void SetFrequency(Vector2 newFrequency)
		{
		}

		public override void UpdateSpringValue(float deltaTime)
		{
		}

		public override void MoveToInstant(Vector2 newValue)
		{
		}

		public override void Stop()
		{
		}

		public override void SetInitialValue(Vector2 newInitialValue)
		{
		}

		public override void RestoreInitialValue()
		{
		}

		public override void SetCurrentValueAsInitialValue()
		{
		}

		public override void MoveTo(Vector2 newValue)
		{
		}

		public override void MoveToAdditive(Vector2 newValue)
		{
		}

		public override void MoveToSubtractive(Vector2 newValue)
		{
		}

		public override void MoveToRandom(Vector2 min, Vector2 max)
		{
		}

		public override void Bump(Vector2 bumpAmount)
		{
		}

		public override void BumpRandom(Vector2 min, Vector2 max)
		{
		}

		public override void Finish()
		{
		}
	}
}
