using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Transform/Scale Shake")]
	[FeedbackHelp("This feedback lets you emit a ScaleShake event. This will be caught by MMScaleShakers (on the specified channel). Scale shakers, as the name suggests, are used to shake the scale of a transform, along a direction, with optional noise and other fine control options.")]
	public class MMF_ScaleShake : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Optional Target", true, 33, false, false)]
		[Tooltip("a specific (and optional) shaker to target, regardless of its channel")]
		public MMScaleShaker TargetShaker;

		[MMFInspectorGroup("Scale Shake", true, 28, false, false)]
		[Tooltip("the duration of the shake, in seconds")]
		public float Duration;

		[Tooltip("whether or not to reset shaker values after shake")]
		public bool ResetShakerValuesAfterShake;

		[Tooltip("whether or not to reset the target's values after shake")]
		public bool ResetTargetValuesAfterShake;

		[MMFInspectorGroup("Shake Settings", true, 42, false, false)]
		[Tooltip("the speed at which the transform should shake")]
		public float ShakeSpeed;

		[Tooltip("the maximum distance from its initial scale the transform will move to during the shake")]
		public float ShakeRange;

		[MMFInspectorGroup("Direction", true, 43, false, false)]
		[Tooltip("the direction along which to shake the transform's scale")]
		public Vector3 ShakeMainDirection;

		[Tooltip("if this is true, instead of using ShakeMainDirection as the direction of the shake, a random vector3 will be generated, randomized between ShakeMainDirection and ShakeAltDirection")]
		public bool RandomizeDirection;

		[Tooltip("when in RandomizeDirection mode, a vector against which to randomize the main direction")]
		[MMFCondition("RandomizeDirection", true)]
		public Vector3 ShakeAltDirection;

		[Tooltip("if this is true, a new direction will be randomized every time a shake happens")]
		public bool RandomizeDirectionOnPlay;

		[MMFInspectorGroup("Directional Noise", true, 47, false, false)]
		[Tooltip("whether or not to add noise to the main direction")]
		public bool AddDirectionalNoise;

		[Tooltip("when adding directional noise, noise strength will be randomized between this value and DirectionalNoiseStrengthMax")]
		[MMFCondition("AddDirectionalNoise", true)]
		public Vector3 DirectionalNoiseStrengthMin;

		[Tooltip("when adding directional noise, noise strength will be randomized between this value and DirectionalNoiseStrengthMin")]
		[MMFCondition("AddDirectionalNoise", true)]
		public Vector3 DirectionalNoiseStrengthMax;

		[MMFInspectorGroup("Randomness", true, 44, false, false)]
		[Tooltip("a unique seed you can use to get different outcomes when shaking more than one transform at once")]
		public Vector3 RandomnessSeed;

		[Tooltip("whether or not to generate a unique seed automatically on every shake")]
		public bool RandomizeSeedOnShake;

		[MMFInspectorGroup("One Time", true, 45, false, false)]
		[Tooltip("whether or not to use attenuation, which will impact the amplitude of the shake, along the defined curve")]
		public bool UseAttenuation;

		[Tooltip("the animation curve used to define attenuation, impacting the amplitude of the shake")]
		[MMFCondition("UseAttenuation", true)]
		public AnimationCurve AttenuationCurve;

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

		public override bool HasChannel => false;

		public override bool HasRandomness => false;

		public override bool HasRange => false;

		public override bool HasAutomatedTargetAcquisition => false;

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
