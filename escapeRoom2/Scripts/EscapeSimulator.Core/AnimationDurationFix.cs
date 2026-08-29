using UnityEngine;

public class AnimationDurationFix : StateMachineBehaviour
{
	public int ResetEveryNthCycles = 100;

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!(stateInfo.normalizedTime < (float)ResetEveryNthCycles))
		{
			animator.Play(stateInfo.fullPathHash, layerIndex, 0f);
		}
	}
}
