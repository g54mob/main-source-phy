using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you apply forces and torques (relative or not) to a Rigidbody.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("GameObject/Rigidbody2D")]
	public class MMF_Rigidbody2D : MMF_Feedback
	{
		public enum Modes
		{
			AddForce = 0,
			AddRelativeForce = 1,
			AddTorque = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Rigidbody2D", true, 32, true, false)]
		[Tooltip("the rigidbody to target on play")]
		public Rigidbody2D TargetRigidbody2D;

		[Tooltip("an extra list of rigidbodies to target on play")]
		public List<Rigidbody2D> ExtraTargetRigidbodies2D;

		[Tooltip("the selected mode for this feedback")]
		public Modes Mode;

		[Tooltip("the min force or torque to apply")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public Vector2 MinForce;

		[Tooltip("the max force or torque to apply")]
		[MMFEnumCondition("Mode", new int[] { 0, 1 })]
		public Vector2 MaxForce;

		[Tooltip("the min torque to apply to this rigidbody on play")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public float MinTorque;

		[Tooltip("the max torque to apply to this rigidbody on play")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public float MaxTorque;

		[Tooltip("the force mode to apply")]
		public ForceMode2D AppliedForceMode;

		[Tooltip("if this is true, the velocity of the rigidbody will be reset before applying the new force")]
		public bool ResetVelocityOnPlay;

		protected Vector2 _force;

		protected float _torque;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ApplyForce(Rigidbody2D rb, float feedbacksIntensity)
		{
		}
	}
}
