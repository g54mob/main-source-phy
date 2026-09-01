using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will instantiate the specified ParticleSystem at the specified position on Start or on Play, optionally nesting them.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Particles/Particles Instantiation")]
	public class MMF_ParticlesInstantiation : MMF_Feedback
	{
		public enum PositionModes
		{
			FeedbackPosition = 0,
			Transform = 1,
			WorldPosition = 2,
			Script = 3
		}

		public enum Modes
		{
			Cached = 0,
			OnDemand = 1,
			Pool = 2
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Particles Instantiation", true, 37, true, false)]
		[Tooltip("whether the particle system should be cached or created on demand the first time")]
		public Modes Mode;

		[Tooltip("the initial and planned size of this object pool")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public int ObjectPoolSize;

		[Tooltip("whether or not to create a new pool even if one already exists for that same prefab")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public bool MutualizePools;

		[Tooltip("if specified, the instantiated object (or the pool of objects) will be parented to this transform ")]
		[MMFEnumCondition("Mode", new int[] { 2 })]
		public Transform ParentTransform;

		[Tooltip("if this is false, a brand new particle system will be created every time")]
		[MMFEnumCondition("Mode", new int[] { 1 })]
		public bool CachedRecycle;

		[Tooltip("the particle system to spawn")]
		public ParticleSystem ParticlesPrefab;

		[Tooltip("the possible random particle systems")]
		public List<ParticleSystem> RandomParticlePrefabs;

		[Tooltip("if this is true, the particle system game object will be activated on Play, useful if you've somehow disabled it in a past Play")]
		public bool ForceSetActiveOnPlay;

		[Tooltip("if this is true, the particle system will be stopped every time the feedback is reset - usually before play")]
		public bool StopOnReset;

		[Tooltip("the duration for the player to consider. This won't impact your particle system, but is a way to communicate to the MMF Player the duration of this feedback. Usually you'll want it to match your actual particle system, and setting it can be useful to have this feedback work with holding pauses.")]
		public float DeclaredDuration;

		[MMFInspectorGroup("Position", true, 29, false, false)]
		[Tooltip("the selected position mode")]
		public PositionModes PositionMode;

		[Tooltip("the position at which to spawn this particle system")]
		[MMFEnumCondition("PositionMode", new int[] { 1 })]
		public Transform InstantiateParticlesPosition;

		[Tooltip("the world position to move to when in WorldPosition mode")]
		[MMFEnumCondition("PositionMode", new int[] { 2 })]
		public Vector3 TargetWorldPosition;

		[Tooltip("an offset to apply to the instantiation position")]
		public Vector3 Offset;

		[Tooltip("whether or not the particle system should be nested in hierarchy or floating on its own")]
		[MMFEnumCondition("PositionMode", new int[] { 1, 0 })]
		public bool NestParticles;

		[Tooltip("whether or not to also apply rotation")]
		public bool ApplyRotation;

		[Tooltip("whether or not to also apply scale")]
		public bool ApplyScale;

		[MMFInspectorGroup("Simulation Speed", true, 43, false, false)]
		[Tooltip("whether or not to force a specific simulation speed on the target particle system(s)")]
		public bool ForceSimulationSpeed;

		[Tooltip("The min and max values at which to randomize the simulation speed, if ForceSimulationSpeed is true. A new value will be randomized every time this feedback plays")]
		[MMFCondition("ForceSimulationSpeed", true)]
		public Vector2 ForcedSimulationSpeed;

		protected ParticleSystem _instantiatedParticleSystem;

		protected List<ParticleSystem> _instantiatedRandomParticleSystems;

		protected MMMiniObjectPooler _objectPooler;

		protected GameObject _newGameObject;

		protected bool _poolCreatedOrFound;

		protected Vector3 _scriptPosition;

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

		protected virtual bool ShouldCache => false;

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected virtual void CreatePools(MMF_Player owner)
		{
		}

		protected virtual void CacheParticleSystem()
		{
		}

		protected virtual void InstantiateParticleSystem()
		{
		}

		protected virtual void PositionParticleSystem(ParticleSystem system)
		{
		}

		protected virtual Quaternion GetRotation(Transform target)
		{
			return default(Quaternion);
		}

		protected virtual Vector3 GetScale(Transform target)
		{
			return default(Vector3);
		}

		protected virtual Vector3 GetPosition(Vector3 position)
		{
			return default(Vector3);
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void PlayTargetParticleSystem(ParticleSystem targetParticleSystem)
		{
		}

		protected virtual void GrabCachedParticleSystem()
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomReset()
		{
		}
	}
}
