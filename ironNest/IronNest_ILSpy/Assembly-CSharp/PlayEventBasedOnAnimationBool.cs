using UnityEngine;
using UnityEngine.Events;

public class PlayEventBasedOnAnimationBool : MonoBehaviour
{
	public AnimatorBoolToggler animatorToCheck;

	public UnityEvent eventIfTrue;

	public UnityEvent eventIfFalse;

	public void Trigger()
	{
		if (animatorToCheck != null)
		{
			(animatorToCheck.GetBool() ? eventIfTrue : eventIfFalse)?.Invoke();
		}
	}
}
