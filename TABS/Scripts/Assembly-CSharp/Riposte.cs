using UnityEngine;

public class Riposte : MonoBehaviour
{
	public float secondsTilAttack = 0.5f;

	public void DoRiposte()
	{
		Weapon weapon = null;
		WeaponHandler componentInChildren = base.transform.root.GetComponentInChildren<WeaponHandler>();
		if ((bool)componentInChildren)
		{
			if ((bool)componentInChildren.rightWeapon && (bool)componentInChildren.rightWeapon.rigidbody)
			{
				weapon = componentInChildren.rightWeapon;
			}
			else if ((bool)componentInChildren.leftWeapon && (bool)componentInChildren.leftWeapon.rigidbody)
			{
				weapon = componentInChildren.leftWeapon;
			}
		}
		if ((bool)weapon)
		{
			weapon.internalCounter = weapon.internalCooldown - secondsTilAttack;
		}
	}
}
