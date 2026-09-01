using Landfall.TABS;
using UnityEngine;

public class SetAttackSpeed : MonoBehaviour
{
	private WeaponHandler weaponHandler;

	private Unit unit;

	public float attackSpeedMultiplier;

	private float originalAttackSpeed;

	private void Start()
	{
		weaponHandler = base.transform.root.GetComponentInChildren<WeaponHandler>();
		unit = base.transform.root.GetComponent<Unit>();
		originalAttackSpeed = unit.attackSpeedMultiplier;
	}

	public void GiveAttackSpeed()
	{
		if (!unit)
		{
			unit = base.transform.root.GetComponent<Unit>();
		}
		unit.AddAttackSpeed(attackSpeedMultiplier);
	}

	public void ResetAttackSpeed()
	{
		unit.AddAttackSpeed(0f - attackSpeedMultiplier);
	}
}
