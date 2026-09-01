using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you pilot a Global PostProcessing Volume AutoBlend URP component. A GPPVAB component is placed on a PostProcessing Volume, and will let you control and blend its weight over time on demand.")]
	[FeedbackPath("PostProcess/Global PP Volume Auto Blend URP")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.URP", null)]
	public class MMF_GlobalPPVolumeAutoBlend_URP : MMF_Feedback
	{
		public enum Modes
		{
			Default = 0,
			Override = 1
		}

		public enum Actions
		{
			Blend = 0,
			BlendBack = 1
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("PostProcess Volume Blend", true, 22, true, false)]
		public MMGlobalPostProcessingVolumeAutoBlend_URP TargetAutoBlend;

		public Modes Mode;

		[MMFEnumCondition("Mode", new int[] { 0 })]
		public Actions BlendAction;

		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float BlendDuration;

		[MMFEnumCondition("Mode", new int[] { 1 })]
		public AnimationCurve BlendCurve;

		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float InitialWeight;

		[MMFEnumCondition("Mode", new int[] { 1 })]
		public float FinalWeight;

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

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
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
