using System.Collections.Generic;
using UnityEngine;

namespace Plugins.Runtime.Animators
{
	public class AnimatorRandomBehaviour : StateMachineBehaviour, ISerializationCallbackReceiver
	{
		[SerializeField]
		private RuntimeAnimatorController animatorController;

		[SerializeField]
		private float crossfadeTime;

		[SerializeField]
		private List<int> statesNames;

		private bool isCrossFading;

		private int isCrossFadingFromThis;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		[ContextMenu("Collect States")]
		private void CollectStates()
		{
		}

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
		}
	}
}
