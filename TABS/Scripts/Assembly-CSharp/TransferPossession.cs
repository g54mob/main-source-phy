using System;
using Landfall.TABS;
using UnityEngine;

public class TransferPossession : MonoBehaviour
{
	private void Start()
	{
		UnitSpawner component = GetComponent<UnitSpawner>();
		if ((bool)component)
		{
			component.spawnUnitAction = (Action<GameObject>)Delegate.Combine(component.spawnUnitAction, new Action<GameObject>(Transfer));
		}
	}

	public void Transfer(GameObject unitRoot)
	{
		CameraAbilityPossess componentInParent = MainCam.instance.GetComponentInParent<CameraAbilityPossess>();
		Unit component = unitRoot.GetComponent<Unit>();
		Unit component2 = base.transform.root.GetComponent<Unit>();
		if ((bool)componentInParent && componentInParent.currentUnit == component2)
		{
			componentInParent.EnterUnit(component);
		}
	}
}
