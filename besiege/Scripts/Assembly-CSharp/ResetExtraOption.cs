using UnityEngine;

public class ResetExtraOption : ClickBehaviour
{
	public MonoBehaviour monoBehaviour;

	public override void OnClicked()
	{
		ICanBeReset canBeReset = monoBehaviour as ICanBeReset;
		if (canBeReset == null)
		{
			Debug.Log(monoBehaviour.ToString() + " does not implement ICanBeReset");
		}
		else
		{
			canBeReset.Reset();
		}
	}
}
