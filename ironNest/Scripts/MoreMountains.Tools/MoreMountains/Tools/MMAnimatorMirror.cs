using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMAnimatorMirror : MonoBehaviour
	{
		public struct MMAnimatorMirrorBind
		{
			public int ParameterHash;

			public AnimatorControllerParameterType ParameterType;
		}

		[Header("Bindings")]
		public Animator SourceAnimator;

		public Animator TargetAnimator;

		protected AnimatorControllerParameter[] _sourceParameters;

		protected AnimatorControllerParameter[] _targetParameters;

		protected List<MMAnimatorMirrorBind> _updateParameters;

		protected virtual void Awake()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void Mirror()
		{
		}
	}
}
