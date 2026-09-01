using Landfall.TABS;
using Landfall.TABS.AI;
using Landfall.TABS.AI.Components.Tags;
using UnityEngine;

public class ChangeTargetingType : MonoBehaviour
{
	public float targetFriendlyFor;

	public bool stopAttacks = true;

	public float stopAttacksFor;

	public bool setStopToEffectTime = true;

	private UnitAPI api;

	public float teamDamage;

	private float originalteamdamage;

	private CollisionWeapon rightDamage;

	private CollisionWeapon leftDamage;

	private bool rightTeamDamageIsSet;

	private bool leftTeamDamageIsSet;

	private void Update()
	{
		if (targetFriendlyFor > 0f)
		{
			targetFriendlyFor -= Time.deltaTime;
		}
		else if (api.CurrentTargetingType != api.DefaultTargetingType)
		{
			SetDefault();
		}
	}

	public void SetTargetFriendlyFor(float f)
	{
		if (!api)
		{
			api = base.transform.root.GetComponent<UnitAPI>();
		}
		targetFriendlyFor = f;
		WeaponHandler weaponHandler = api.GetComponent<Unit>().data.weaponHandler;
		if (stopAttacks)
		{
			if (setStopToEffectTime)
			{
				stopAttacksFor = f;
			}
			if (weaponHandler != null)
			{
				weaponHandler.StopAttacksFor(stopAttacksFor);
			}
			else
			{
				MultipleWeaponHandler componentInChildren = api.GetComponentInChildren<MultipleWeaponHandler>();
				if ((bool)componentInChildren)
				{
					componentInChildren.StopAttacksFor(stopAttacksFor);
				}
			}
		}
		if (teamDamage != 0f)
		{
			if ((bool)weaponHandler.rightWeapon)
			{
				rightDamage = weaponHandler.rightWeapon.GetComponent<CollisionWeapon>();
				if ((bool)rightDamage)
				{
					originalteamdamage = rightDamage.teamDamage;
					rightDamage.teamDamage = teamDamage;
					rightTeamDamageIsSet = true;
				}
			}
			if ((bool)weaponHandler.leftWeapon)
			{
				leftDamage = weaponHandler.leftWeapon.GetComponent<CollisionWeapon>();
				if ((bool)leftDamage)
				{
					originalteamdamage = leftDamage.teamDamage;
					leftDamage.teamDamage = teamDamage;
					leftTeamDamageIsSet = true;
				}
			}
		}
		SetTargetFriendly();
	}

	public void SetTargetFriendly()
	{
		if (!api)
		{
			api = base.transform.root.GetComponent<UnitAPI>();
		}
		api.SetTargetingType(new FindNearestFriendTargeting
		{
			PrioritizeMount = 0
		});
	}

	public void SetDefault()
	{
		if (!api)
		{
			api = base.transform.root.GetComponent<UnitAPI>();
		}
		api.SetTargetingType(api.DefaultTargetingType);
		if (rightTeamDamageIsSet)
		{
			rightDamage.teamDamage = originalteamdamage;
			rightTeamDamageIsSet = false;
		}
		if (leftTeamDamageIsSet)
		{
			leftDamage.teamDamage = originalteamdamage;
			leftTeamDamageIsSet = false;
		}
	}
}
