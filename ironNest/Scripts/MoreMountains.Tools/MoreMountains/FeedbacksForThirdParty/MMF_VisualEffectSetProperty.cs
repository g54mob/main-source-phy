using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you set a property on a target VisualEffect")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.VisualEffectGraph", null)]
	public class MMF_VisualEffectSetProperty : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Visual Effect Property", true, 41, false, false)]
		[Tooltip("the duration for the player to consider. This won't impact your visual effect, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual particle system, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

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

		protected override void CustomPlayFeedback(Vector3 position, float attenuation = 1f)
		{
		}
	}
}
