using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu("More Mountains/Feedbacks/Shakers/Various/MMWiggle")]
	public class MMWiggle : MonoBehaviour
	{
		public enum UpdateModes
		{
			Update = 0,
			FixedUpdate = 1,
			LateUpdate = 2
		}

		[Tooltip("the selected update mode")]
		public UpdateModes UpdateMode;

		[Tooltip("whether or not position wiggle is active")]
		public bool PositionActive;

		[Tooltip("whether or not rotation wiggle is active")]
		public bool RotationActive;

		[Tooltip("whether or not scale wiggle is active")]
		public bool ScaleActive;

		[Tooltip("all public info related to position wiggling")]
		public WiggleProperties PositionWiggleProperties;

		[Tooltip("all public info related to rotation wiggling")]
		public WiggleProperties RotationWiggleProperties;

		[Tooltip("all public info related to scale wiggling")]
		public WiggleProperties ScaleWiggleProperties;

		[Tooltip("a debug duration used in conjunction with the debug buttons")]
		public float DebugWiggleDuration;

		protected InternalWiggleProperties _positionInternalProperties;

		protected InternalWiggleProperties _rotationInternalProperties;

		protected InternalWiggleProperties _scaleInternalProperties;

		public virtual void WigglePosition(float duration)
		{
		}

		public virtual void WiggleRotation(float duration)
		{
		}

		public virtual void WiggleScale(float duration)
		{
		}

		protected virtual void WiggleValue(ref WiggleProperties property, ref InternalWiggleProperties internalProperties, float duration)
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void InitializeRandomValues(ref WiggleProperties properties, ref InternalWiggleProperties internalProperties)
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void FixedUpdate()
		{
		}

		protected virtual void ProcessUpdate()
		{
		}

		protected virtual bool UpdateValue(bool valueActive, WiggleProperties properties, ref InternalWiggleProperties internalProperties)
		{
			return false;
		}

		protected float ApplyFalloff(WiggleProperties properties)
		{
			return 0f;
		}

		protected virtual Vector3 AnimateNoiseValue(ref InternalWiggleProperties internalProperties, WiggleProperties properties)
		{
			return default(Vector3);
		}

		protected virtual Vector3 AnimateCurveValue(ref InternalWiggleProperties internalProperties, WiggleProperties properties)
		{
			return default(Vector3);
		}

		protected virtual void EvaluateCurve(AnimationCurve curve, float percent, Vector3 remapMin, Vector3 remapMax, ref Vector3 returnValue, WiggleProperties properties)
		{
		}

		protected virtual bool MoveVector3TowardsTarget(ref Vector3 movedValue, WiggleProperties properties, ref Vector3 startValue, Vector3 initialValue, ref Vector3 destinationValue, ref float timeSinceLastPause, ref float timeSinceLastValueChange, ref Vector3 randomAmplitude, ref float randomFrequency, ref float pauseDuration, float frequency)
		{
			return false;
		}

		protected virtual Vector3 DetermineNewValue(WiggleProperties properties, Vector3 newValue, Vector3 initialValue, ref Vector3 startValue, ref Vector3 randomAmplitude, ref float randomFrequency, ref float pauseDuration, bool firstPlay = false)
		{
			return default(Vector3);
		}

		protected virtual float RandomizeFloat(ref float randomizedFloat, float floatMin, float floatMax)
		{
			return 0f;
		}

		protected virtual Vector3 RandomizeVector3(ref Vector3 randomizedVector, Vector3 vectorMin, Vector3 vectorMax)
		{
			return default(Vector3);
		}

		public virtual void RestoreInitialValues()
		{
		}
	}
}
