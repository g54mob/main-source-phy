using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Animation/MMRagdoller")]
	public class MMRagdoller : MonoBehaviour
	{
		public enum RagdollStates
		{
			Animated = 0,
			Ragdolling = 1,
			Blending = 2
		}

		[Header("Ragdoll")]
		public RagdollStates CurrentState;

		public float RagdollToMecanimBlendDuration;

		[Header("Rigidbodies")]
		public Rigidbody MainRigidbody;

		public bool ForceSleep;

		public bool AllowBlending;

		protected float _mecanimToGetUpTransitionTime;

		protected float _ragdollingEndTimestamp;

		protected Vector3 _ragdolledHipPosition;

		protected Vector3 _ragdolledHeadPosition;

		protected Vector3 _ragdolledFeetPosition;

		protected List<RagdollBodyPart> _bodyparts;

		protected Animator _animator;

		protected List<Component> _rigidbodiesTempList;

		protected Component[] _rigidbodies;

		protected HashSet<int> _animatorParameters;

		protected const string _getUpFromBackAnimationParameterName = "GetUpFromBack";

		protected int _getUpFromBackAnimationParameter;

		protected const string _getUpFromBellyAnimationParameterName = "GetUpFromBelly";

		protected int _getUpFromBellyAnimationParameter;

		protected bool _initialized;

		public bool Ragdolling
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		protected virtual void Start()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void RegisterAnimatorParameters()
		{
		}

		protected virtual void SetIsKinematic(bool isKinematic)
		{
		}

		public virtual void ForceRigidbodiesToSleep()
		{
		}

		protected virtual void LateUpdate()
		{
		}

		protected virtual void HandleBlending()
		{
		}

		public Vector3 GetPosition()
		{
			return default(Vector3);
		}

		protected Vector3 GetRootPosition()
		{
			return default(Vector3);
		}
	}
}
