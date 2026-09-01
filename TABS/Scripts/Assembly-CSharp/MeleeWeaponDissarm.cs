using Landfall.TABS;
using UnityEngine;

public class MeleeWeaponDissarm : CollisionWeaponEffect
{
	private Weapon weapon;

	private void Start()
	{
		weapon = GetComponent<Weapon>();
	}

	public override void DoEffect(Transform hitTransform, Collision collision)
	{
		HoldingHandler componentInChildren = hitTransform.transform.root.GetComponentInChildren<HoldingHandler>();
		if (componentInChildren == null)
		{
			return;
		}
		Unit componentInParent = componentInChildren.GetComponentInParent<Unit>();
		if ((bool)componentInParent && (bool)weapon && (bool)weapon.connectedData && componentInParent.Team != weapon.connectedData.team)
		{
			DataHandler componentInParent2 = collision.gameObject.GetComponentInParent<DataHandler>();
			if ((!componentInParent2 || !(Random.value < componentInParent2.maxHealth * 0.001f)) && (bool)componentInChildren)
			{
				componentInChildren.LetGoOfAll();
				Unit componentInParent3 = componentInChildren.GetComponentInParent<Unit>();
				componentInParent3.m_PreferedDistance = 1.3f;
				componentInParent3.m_AttackDistance = 1.5f;
			}
		}
	}
}
