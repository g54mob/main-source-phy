using MoreMountains.Tools;
using UnityEngine.Events;

namespace MoreMountains.Feedbacks
{
	[MMRequiresConstantRepaintOnlyWhenPlaying]
	public abstract class MMSpringComponentBase : MMMonoBehaviour
	{
		public enum TimeScaleModes
		{
			Unscaled = 0,
			Scaled = 1
		}

		[MMInspectorGroup("Events", true, 16, true)]
		public UnityEvent OnEquilibriumReached;

		protected float _velocityLowThreshold;

		public virtual bool LowVelocity => false;

		public virtual void SetVelocityLowThreshold(float threshold)
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void Activate()
		{
		}

		protected virtual void SelfDisable()
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void Finish()
		{
		}

		public virtual void RestoreInitialValue()
		{
		}

		public virtual void ResetInitialValue()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void GrabCurrentValue()
		{
		}

		protected virtual void UpdateSpringValue()
		{
		}

		protected virtual void TestMoveTo()
		{
		}

		protected virtual void TestMoveToAdditive()
		{
		}

		protected virtual void TestMoveToSubtractive()
		{
		}

		protected virtual void TestMoveToRandom()
		{
		}

		protected virtual void TestMoveToInstant()
		{
		}

		protected virtual void TestBump()
		{
		}

		protected virtual void TestBumpRandom()
		{
		}
	}
}
