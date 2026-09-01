using Landfall.TABS.GameState;
using UnityEngine;

public class MapProp : GameStateListener
{
	private GameObject myClone;

	public override void OnEnterBattleState()
	{
		if ((bool)myClone)
		{
			Rigidbody component = myClone.GetComponent<Rigidbody>();
			if ((bool)component)
			{
				component.isKinematic = false;
			}
		}
	}

	public override void OnEnterPlacementState()
	{
		base.gameObject.SetActive(value: false);
		Rigidbody component = GetComponent<Rigidbody>();
		if ((bool)component)
		{
			component.isKinematic = true;
		}
		if ((bool)myClone)
		{
			Object.Destroy(myClone);
		}
		myClone = Object.Instantiate(base.gameObject, base.transform.position, base.transform.rotation, base.transform.parent);
		myClone.SetActive(value: true);
		Object.Destroy(myClone.GetComponent<MapProp>());
	}
}
