using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Plugins.Runtime.Animators;

public class AnimatorRandomBehaviour : StateMachineBehaviour, ISerializationCallbackReceiver
{
	private RuntimeAnimatorController animatorController;

	private float crossfadeTime = 0.1f;

	private List<int> statesNames;

	private bool isCrossFading;

	private int isCrossFadingFromThis;

	public unsafe override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!isCrossFading)
		{
			List<int> list = statesNames;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v51 @ r14_v2 (System.Collections.Generic.List`1<System.Int32>)+18]");
			int num = Random.Range(0, 0);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			int shortNameHash = ((AnimatorStateInfo*)stateInfo)->shortNameHash;
			int num2 = default(int);
			if (num2 != shortNameHash)
			{
				animator.CrossFade(num2, crossfadeTime, layerIndex);
				int fullPathHash = ((AnimatorStateInfo*)stateInfo)->fullPathHash;
				isCrossFadingFromThis = fullPathHash;
			}
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//IL_0045: Expected I4, but got I8
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18091B3E0");
		object obj = default(object);
		if (isCrossFadingFromThis == (nint)obj)
		{
			isCrossFading = false;
			isCrossFadingFromThis = -1;
		}
	}

	private void CollectStates()
	{
	}

	public void OnBeforeSerialize()
	{
	}

	public void OnAfterDeserialize()
	{
	}

	public AnimatorRandomBehaviour()
	{
		List<int> list = new List<int>();
		statesNames = list;
		base._002Ector();
	}
}
