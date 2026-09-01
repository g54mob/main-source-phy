using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Animation/MMAnimationModifier")]
	public class MMAnimationModifier : StateMachineBehaviour
	{
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 StartPosition;

		[MMVector(new string[] { "Min", "Max" })]
		public Vector2 AnimationSpeed;

		protected bool _enteredState;

		protected float _initialSpeed;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}
	}
}
