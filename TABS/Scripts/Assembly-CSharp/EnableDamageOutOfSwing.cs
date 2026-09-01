using UnityEngine;

public class EnableDamageOutOfSwing : MonoBehaviour
{
	private MeleeWeapon meleeWeapon;

	private void Start()
	{
		meleeWeapon = base.transform.root.GetComponentInChildren<MeleeWeapon>();
	}

	public void EnableDamage()
	{
		if (meleeWeapon == null)
		{
			meleeWeapon = base.transform.root.GetComponentInChildren<MeleeWeapon>();
		}
		if (meleeWeapon != null)
		{
			meleeWeapon.EnableDamageOutOfSwing();
		}
	}

	public void DisableDamage()
	{
		if (meleeWeapon == null)
		{
			meleeWeapon = base.transform.root.GetComponentInChildren<MeleeWeapon>();
		}
		if (meleeWeapon != null)
		{
			meleeWeapon.DisableDamageOutOfSwing();
		}
	}
}
