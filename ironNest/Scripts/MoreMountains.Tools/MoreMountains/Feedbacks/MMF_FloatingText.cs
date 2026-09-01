using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will request the spawn of a floating text, usually to signify damage, but not necessarily. This requires that a MMFloatingTextSpawner be correctly setup in the scene, otherwise nothing will happen. To do so, create a new empty object, add a MMFloatingTextSpawner to it. Drag (at least) one MMFloatingText prefab into its PooledSimpleMMFloatingText slot. You'll find such prefabs already made in the MMTools/Tools/MMFloatingText/Prefabs folder, but feel free to create your own. Using that feedback will always spawn the same text. While this may be what you want, if you're using the Corgi Engine or TopDown Engine, you'll find dedicated versions directly hooked to the Health component, letting you display damage taken.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.MMTools", null)]
	[FeedbackPath("UI/Floating Text")]
	public class MMF_FloatingText : MMF_Feedback
	{
		public enum PositionModes
		{
			TargetTransform = 0,
			FeedbackPosition = 1,
			PlayPosition = 2
		}

		public enum RoundingMethods
		{
			NoRounding = 0,
			Round = 1,
			Ceil = 2,
			Floor = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Floating Text", true, 64, false, false)]
		[Tooltip("the Intensity to spawn this text with, will act as a lifetime/movement/scale multiplier based on the spawner's settings")]
		public float Intensity;

		[Tooltip("the value to display when spawning this text")]
		public string Value;

		[Tooltip("if this is true, the intensity passed to this feedback will be the value displayed")]
		public bool UseIntensityAsValue;

		[Tooltip("the rounding methods to apply to the output value (when using intensity as the output value, string values won't get rounded)")]
		[MMFInspectorGroup("Rounding", true, 68, false, false)]
		public RoundingMethods RoundingMethod;

		[MMFInspectorGroup("Color", true, 65, false, false)]
		[Tooltip("whether or not to force a color on the new text, if not, the default colors of the spawner will be used")]
		public bool ForceColor;

		[Tooltip("the gradient to apply over the lifetime of the text")]
		[GradientUsage(true)]
		public Gradient AnimateColorGradient;

		[MMFInspectorGroup("Lifetime", true, 66, false, false)]
		[Tooltip("whether or not to force a lifetime on the new text, if not, the default colors of the spawner will be used")]
		public bool ForceLifetime;

		[Tooltip("the forced lifetime for the spawned text")]
		[MMFCondition("ForceLifetime", true)]
		public float Lifetime;

		[MMFInspectorGroup("Position", true, 67, false, false)]
		[Tooltip("where to spawn the new text (at the position of the feedback, or on a specified Transform)")]
		public PositionModes PositionMode;

		[Tooltip("in transform mode, the Transform on which to spawn the new floating text")]
		[MMFEnumCondition("PositionMode", new int[] { 0 })]
		public Transform TargetTransform;

		[Tooltip("the direction to apply to the new floating text (leave it to 0 to let the Spawner decide based on its settings)")]
		public Vector3 Direction;

		protected Vector3 _playPosition;

		protected string _value;

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

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual float ApplyRounding(float value)
		{
			return 0f;
		}
	}
}
