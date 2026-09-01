using UnityEngine;

public class SetComponentEnabledStatusOnClick : ClickBehaviour
{
	public bool setTo;

	public MonoBehaviour monoBehaviourToSet;

	public override void OnClicked()
	{
		monoBehaviourToSet.enabled = setTo;
	}
}
