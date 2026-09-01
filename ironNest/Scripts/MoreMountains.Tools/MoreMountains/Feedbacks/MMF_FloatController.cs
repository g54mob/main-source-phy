using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback lets you trigger a one time play on a target FloatController.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("GameObject/FloatController")]
	public class MMF_FloatController : MMF_Feedback
	{
		public enum Modes
		{
			OneTime = 0,
			ToDestination = 1
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Float Controller", true, 36, true, false)]
		[Tooltip("the mode this controller is in")]
		public Modes Mode;

		[Tooltip("the float controller to trigger a one time play on")]
		public FloatController TargetFloatController;

		[Tooltip("a list of extra and optional float controllers to trigger a one time play on")]
		public List<FloatController> ExtraTargetFloatControllers;

		[Tooltip("whether this should revert to original at the end")]
		public bool RevertToInitialValueAfterEnd;

		[Tooltip("the duration of the One Time shake")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float OneTimeDuration;

		[Tooltip("the amplitude of the One Time shake (this will be multiplied by the curve's height)")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float OneTimeAmplitude;

		[Tooltip("the low value to remap the normalized curve value to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float OneTimeRemapMin;

		[Tooltip("the high value to remap the normalized curve value to")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public float OneTimeRemapMax;

		[Tooltip("the curve to apply to the one time shake")]
		[MMFEnumCondition("Mode", new int[] { 0 })]
		public AnimationCurve OneTimeCurve;

		[Tooltip("the value to move this float controller to")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float ToDestinationValue;

		[Tooltip("the duration over which to move the value")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float ToDestinationDuration;

		[Tooltip("the curve over which to move the value in ToDestination mode")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public AnimationCurve ToDestinationCurve;

		protected float _oneTimeDurationStorage;

		protected float _oneTimeAmplitudeStorage;

		protected float _oneTimeRemapMinStorage;

		protected float _oneTimeRemapMaxStorage;

		protected AnimationCurve _oneTimeCurveStorage;

		protected float _toDestinationValueStorage;

		protected float _toDestinationDurationStorage;

		protected AnimationCurve _toDestinationCurveStorage;

		protected bool _revertToInitialValueAfterEndStorage;

		public override bool HasRandomness => false;

		public override bool CanForceInitialValue => false;

		public override bool ForceInitialValueDelayed => false;

		public override bool HasAutomatedTargetAcquisition => false;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void HandleFloatController(FloatController target, float intensityMultiplier)
		{
		}

		protected override void CustomReset()
		{
		}

		protected virtual void ResetFloatController(FloatController controller)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
