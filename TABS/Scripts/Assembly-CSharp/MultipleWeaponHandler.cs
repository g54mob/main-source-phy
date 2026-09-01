using Landfall.TABS;
using UnityEngine;

public class MultipleWeaponHandler : MonoBehaviour
{
	public GameObject[] leftWeapons;

	public GameObject[] rightWeapons;

	[Space(20f)]
	public float screenShakeCap = 100f;

	public float screenShakeMultiplier = 1f;

	public float massMultiplier = 1f;

	public float minMass;

	private Unit unit;

	private bool inited;

	private void Init()
	{
		if (!inited)
		{
			inited = true;
			unit = GetComponentInParent<Unit>();
		}
	}

	public void SetWeapon(GameObject weapon, HoldingHandler.HandType handType, bool isUnitEditor)
	{
		Init();
		GameObject[] array = ((handType == HoldingHandler.HandType.Left) ? leftWeapons : rightWeapons);
		for (int i = 0; i < array.Length; i++)
		{
			SetWeaponInterface component = array[i].GetComponent<SetWeaponInterface>();
			if (component != null)
			{
				GameObject gameObject = component.SetWeapon(weapon, handType);
				TryRunTimeBakeWeapon(gameObject, isUnitEditor);
				ConfigureWeapon(gameObject);
			}
		}
	}

	private void ConfigureWeapon(GameObject w)
	{
		if (!w)
		{
			return;
		}
		Weapon component = w.GetComponent<Weapon>();
		if ((bool)component)
		{
			if (unit.m_AttackDistance < component.maxRange + unit.thickness)
			{
				unit.m_AttackDistance = component.maxRange + unit.thickness;
			}
			w.AddComponent<SetInterpolation>();
			if ((bool)component.rigidbody)
			{
				component.rigidbody.mass *= massMultiplier;
				component.rigidbody.mass = Mathf.Clamp(component.rigidbody.mass, minMass, float.MaxValue);
			}
			CollisionWeapon component2 = component.GetComponent<CollisionWeapon>();
			if ((bool)component2)
			{
				component2.screenShakeMultiplier *= screenShakeMultiplier;
				component2.screenShakeMultiplier = Mathf.Clamp(component2.screenShakeMultiplier, 0f, screenShakeCap);
			}
			if (component.m_weaponTargetType == Weapon.WeaponTargetType.Fridemies)
			{
				unit.targetYourFriends = true;
			}
		}
	}

	public void StopAttacksFor(float seconds)
	{
		for (int i = 0; i < rightWeapons.Length; i++)
		{
			if ((bool)rightWeapons[i])
			{
				Weapon component = rightWeapons[i].GetComponent<Weapon>();
				if (component != null)
				{
					component.internalCounter = Mathf.Clamp(component.internalCounter, -1000f, component.internalCooldown - seconds);
				}
			}
		}
		for (int j = 0; j < leftWeapons.Length; j++)
		{
			if ((bool)leftWeapons[j])
			{
				Weapon component2 = leftWeapons[j].GetComponent<Weapon>();
				if (component2 != null)
				{
					component2.internalCounter = Mathf.Clamp(component2.internalCounter, -1000f, component2.internalCooldown - seconds);
				}
			}
		}
	}

	private void TryRunTimeBakeWeapon(GameObject weapon, bool isUnitEditor)
	{
	}
}
