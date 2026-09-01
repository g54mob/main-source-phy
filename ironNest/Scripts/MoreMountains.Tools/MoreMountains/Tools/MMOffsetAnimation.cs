using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Animator))]
	[AddComponentMenu("More Mountains/Tools/Animation/MMOffsetAnimation")]
	public class MMOffsetAnimation : MonoBehaviour
	{
		public float MinimumRandomRange;

		public float MaximumRandomRange;

		public int AnimationLayerID;

		public bool OffsetOnStart;

		public bool OffsetOnEnable;

		public bool DisableAfterOffset;

		protected Animator _animator;

		protected AnimatorStateInfo _stateInfo;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void OnEnable()
		{
		}

		public virtual void OffsetCurrentAnimation()
		{
		}
	}
}
