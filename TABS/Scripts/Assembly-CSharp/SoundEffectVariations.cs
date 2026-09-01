using System;
using Landfall.TABS;
using UnityEngine;

[Serializable]
public class SoundEffectVariations
{
	public enum MaterialType
	{
		Default = 0,
		Meat = 1,
		Ground = 2,
		Metal = 3,
		Wood = 4,
		WarMachine = 5,
		Sand = 6,
		Grass = 7,
		Rock = 8,
		Pavement = 9,
		Snow = 10,
		Ice = 11,
		Dirt = 12
	}

	public MaterialType materialType;

	public AudioClip[] clips;

	public static MaterialType GetMaterialTypeFromCollision(Collision collision)
	{
		MaterialType result = MaterialType.Default;
		switch (collision.collider.tag)
		{
		case "Wood":
			result = MaterialType.Wood;
			break;
		case "Sand":
			result = MaterialType.Sand;
			break;
		case "Grass":
			result = MaterialType.Grass;
			break;
		case "Rock":
			result = MaterialType.Rock;
			break;
		case "Pavement":
			result = MaterialType.Pavement;
			break;
		case "Snow":
			result = MaterialType.Snow;
			break;
		case "Ice":
			result = MaterialType.Ice;
			break;
		case "Dirt":
			result = MaterialType.Dirt;
			break;
		case "Metal":
			result = MaterialType.Metal;
			break;
		}
		return result;
	}

	public static MaterialType GetMaterialType(GameObject hitObject, Rigidbody hitRig)
	{
		if ((bool)hitRig)
		{
			Weapon component = hitRig.GetComponent<Weapon>();
			if ((bool)component)
			{
				if (component.weaponMaterialType == Weapon.WeaponMaterialType.Meat)
				{
					return MaterialType.Meat;
				}
				if (component.weaponMaterialType == Weapon.WeaponMaterialType.Metal)
				{
					return MaterialType.Metal;
				}
				if (component.weaponMaterialType == Weapon.WeaponMaterialType.Wood)
				{
					return MaterialType.Wood;
				}
			}
			else
			{
				Unit component2 = hitRig.transform.root.GetComponent<Unit>();
				if ((bool)component2)
				{
					if (component2.unitType == Unit.UnitType.Meat)
					{
						return MaterialType.Meat;
					}
					if (component2.unitType == Unit.UnitType.Warmachine)
					{
						return MaterialType.WarMachine;
					}
				}
			}
			return MaterialType.Default;
		}
		return MaterialType.Ground;
	}
}
