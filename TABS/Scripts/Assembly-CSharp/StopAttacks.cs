using Landfall.TABS;
using Landfall.TABS.AI;
using UnityEngine;

public class StopAttacks : MonoBehaviour
{
	public bool onStart;

	public float stopAttacksFor;

	private UnitAPI api;

	private void Start()
	{
		if (onStart)
		{
			StopAttacksFor(stopAttacksFor);
		}
	}

	public void StopAttacksFor(float stopAttacksFor)
	{
		if (!api)
		{
			api = base.transform.root.GetComponent<UnitAPI>();
		}
		WeaponHandler weaponHandler = api.GetComponent<Unit>().data.weaponHandler;
		if (weaponHandler != null)
		{
			weaponHandler.StopAttacksFor(stopAttacksFor);
			return;
		}
		MultipleWeaponHandler componentInChildren = api.GetComponentInChildren<MultipleWeaponHandler>();
		if ((bool)componentInChildren)
		{
			componentInChildren.StopAttacksFor(stopAttacksFor);
		}
	}
}
