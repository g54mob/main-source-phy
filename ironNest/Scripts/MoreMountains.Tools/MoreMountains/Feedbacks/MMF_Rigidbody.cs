using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you apply forces and torques (relative or not) to a Rigidbody.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("GameObject/Rigidbody")]
	public class MMF_Rigidbody : MMF_Feedback
	{
		public enum Modes
		{
			AddForce = 0,
			AddRelativeForce = 1,
			AddTorque = 2,
			AddRelativeTorque = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Rigidbody", true, 61, true, false)]
		[Tooltip("the rigidbody to target on play")]
		public Rigidbody TargetRigidbody;

		[Tooltip("a list of extra rigidbodies to target on play")]
		public List<Rigidbody> ExtraTargetRigidbodies;

		[Tooltip("the selected mode for this feedback")]
		public Modes Mode;

		[Tooltip("the min force or torque to apply")]
		public Vector3 MinForce;

		[Tooltip("the max force or torque to apply")]
		public Vector3 MaxForce;

		[Tooltip("the force mode to apply")]
		public ForceMode AppliedForceMode;

		[Tooltip("if this is true, the velocity of the rigidbody will be reset before applying the new force")]
		public bool ResetVelocityOnPlay;

		[Tooltip("if this is true, the magnitude of the min/max force will be applied in the target transform's forward direction")]
		public bool ForwardForce;

		protected Vector3 _force;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ApplyForce(Rigidbody rb)
		{
		}
	}
}
