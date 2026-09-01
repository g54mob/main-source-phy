using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will simply play the specified ParticleSystem (from your scene) when played.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Particles/Particles Play")]
	public class MMF_Particles : MMF_Feedback
	{
		public enum Modes
		{
			Play = 0,
			Stop = 1,
			Pause = 2,
			Emit = 3
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Bound Particles", true, 41, true, false)]
		[Tooltip("whether to Play, Stop or Pause the target particle system when that feedback is played")]
		public Modes Mode;

		[Tooltip("in Emit mode, the amount of particles per emit")]
		[MMFEnumCondition("Mode", new int[] { 3 })]
		public int EmitCount;

		[Tooltip("the particle system to play with this feedback")]
		public ParticleSystem BoundParticleSystem;

		[Tooltip("a list of (optional) particle systems")]
		public List<ParticleSystem> RandomParticleSystems;

		[Tooltip("if this is true, the particles will be moved to the position passed in parameters")]
		public bool MoveToPosition;

		[Tooltip("if this is true, the particle system's object will be set active on play")]
		public bool ActivateOnPlay;

		[Tooltip("if this is true, the particle system will be stopped on initialization")]
		public bool StopSystemOnInit;

		[Tooltip("if this is true, the particle system will be stopped on reset")]
		public bool StopSystemOnReset;

		[Tooltip("if this is true, the particle system will be stopped on feedback stop")]
		public bool StopSystemOnStopFeedback;

		[Tooltip("the duration for the player to consider. This won't impact your particle system, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual particle system, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

		[MMFInspectorGroup("Simulation Speed", true, 43, false, false)]
		[Tooltip("whether or not to force a specific simulation speed on the target particle system(s)")]
		public bool ForceSimulationSpeed;

		[Tooltip("The min and max values at which to randomize the simulation speed, if ForceSimulationSpeed is true. A new value will be randomized every time this feedback plays")]
		[MMFCondition("ForceSimulationSpeed", true)]
		public Vector2 ForcedSimulationSpeed;

		protected ParticleSystem.EmitParams _emitParams;

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

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomReset()
		{
		}

		protected virtual void PlayParticles(Vector3 position)
		{
		}

		protected virtual void HandleParticleSystemAction(ParticleSystem targetParticleSystem)
		{
		}

		protected virtual void StopParticles()
		{
		}
	}
}
