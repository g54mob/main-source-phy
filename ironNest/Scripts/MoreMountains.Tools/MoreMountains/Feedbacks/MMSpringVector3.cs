using System;
using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[Serializable]
	public class MMSpringVector3 : MMSpringDefinition<Vector3>
	{
		public bool SeparateAxis;

		public MMSpringFloat UnifiedSpring;

		public MMSpringFloat SpringX;

		public MMSpringFloat SpringY;

		public MMSpringFloat SpringZ;

		protected Vector3 _returnCurrentValue;

		protected Vector3 _returnTargetValue;

		protected Vector3 _returnVelocity;

		public override Vector3 CurrentValue
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public override Vector3 TargetValue
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public override Vector3 Velocity
		{
			get
			{
				return default(Vector3);
			}
			set
			{
			}
		}

		public virtual void SetDamping(Vector3 newDamping)
		{
		}

		public virtual void SetFrequency(Vector3 newFrequency)
		{
		}

		public override void UpdateSpringValue(float deltaTime)
		{
		}

		public override void MoveToInstant(Vector3 newValue)
		{
		}

		public override void Stop()
		{
		}

		public override void SetInitialValue(Vector3 newInitialValue)
		{
		}

		public override void RestoreInitialValue()
		{
		}

		public override void SetCurrentValueAsInitialValue()
		{
		}

		public override void MoveTo(Vector3 newValue)
		{
		}

		public override void MoveToAdditive(Vector3 newValue)
		{
		}

		public override void MoveToSubtractive(Vector3 newValue)
		{
		}

		public override void MoveToRandom(Vector3 min, Vector3 max)
		{
		}

		public override void Bump(Vector3 bumpAmount)
		{
		}

		public override void BumpRandom(Vector3 min, Vector3 max)
		{
		}

		public override void Finish()
		{
		}
	}
}
