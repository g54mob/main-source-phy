using UnityEngine;

public class AddMeleeCopy : MonoBehaviour
{
	public bool onStart = true;

	private WeaponHandler weaponHandler;

	public float triggerChance = 0.5f;

	public GameObject poof;

	private void Start()
	{
		weaponHandler = base.transform.root.GetComponentInChildren<WeaponHandler>();
		if (onStart)
		{
			AddToUnit();
		}
	}

	public void AddToUnit()
	{
		if (weaponHandler != null)
		{
			if (weaponHandler.rightWeapon != null)
			{
				AddCopyAbilityToWeapon(weaponHandler.rightWeapon);
			}
			if (weaponHandler.leftWeapon != null)
			{
				AddCopyAbilityToWeapon(weaponHandler.leftWeapon);
			}
		}
	}

	private void AddCopyAbilityToWeapon(Weapon weapon)
	{
		MeleeWeaponCopySelf meleeWeaponCopySelf = null;
		MeleeWeaponCopySelf component = weapon.gameObject.GetComponent<MeleeWeaponCopySelf>();
		if (component != null)
		{
			Debug.LogError(weapon.name + " already has a MeleeWeaponCopySelf attached. Don't attach another.");
			meleeWeaponCopySelf = component;
		}
		else
		{
			meleeWeaponCopySelf = weapon.gameObject.AddComponent<MeleeWeaponCopySelf>();
		}
		meleeWeaponCopySelf.triggerChance = triggerChance;
		meleeWeaponCopySelf.poof = poof;
		if (weapon.isRange)
		{
			meleeWeaponCopySelf.isRanged = true;
			meleeWeaponCopySelf.useParentTransform = true;
			weapon.GetComponent<RangeWeapon>().willCopy = true;
		}
	}
}
