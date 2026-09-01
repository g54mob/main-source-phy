using MoreMountains.Feedbacks;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackPath("Camera/Cinemachine Impulse")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.Cinemachine", null)]
	[FeedbackHelp("This feedback lets you trigger a Cinemachine Impulse event. You'll need a Cinemachine Impulse Listener on your camera for this to work.")]
	public class MMF_CinemachineImpulse : MMF_Feedback
	{
		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Cinemachine Impulse", true, 28, false, false)]
		[Tooltip("the impulse definition to broadcast")]
		public CinemachineImpulseDefinition m_ImpulseDefinition;

		[Tooltip("the velocity to apply to the impulse shake")]
		public Vector3 Velocity;

		[Tooltip("whether or not to clear impulses (stopping camera shakes) when the Stop method is called on that feedback")]
		public bool ClearImpulseOnStop;

		[Header("Gizmos")]
		[Tooltip("whether or not to draw gizmos to showcase the various distance properties of this feedback, when applicable. Dissipation distance in blue, impact radius in yellow.")]
		public bool DrawGizmos;

		public override bool HasRandomness => false;

		public override float FeedbackDuration => 0f;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		public override void OnAddFeedback()
		{
		}

		public override void OnDrawGizmosSelectedHandler()
		{
		}

		public override void AutomaticShakerSetup()
		{
		}
	}
}
