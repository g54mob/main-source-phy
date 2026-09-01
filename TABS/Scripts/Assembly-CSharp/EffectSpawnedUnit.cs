using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class EffectSpawnedUnit : MonoBehaviour
{
	public float force;

	public bool randomForce;

	public bool forceDependingOnSize;

	public bool forceOnWarmachines;

	private List<Rigidbody> rigs = new List<Rigidbody>();

	private RiderHolder riders;

	private Unit unitinfo;

	private Unit riderinfo;

	public void Spawn(GameObject unit)
	{
		riders = unit.transform.GetComponent<RiderHolder>();
		unitinfo = unit.transform.GetComponent<Unit>();
		Rigidbody[] allRigs = unit.GetComponentInChildren<RigidbodyHolder>().AllRigs;
		if (randomForce)
		{
			force *= Random.Range(0.8f, 1.2f);
		}
		if (forceDependingOnSize)
		{
			force *= Mathf.Clamp(unit.transform.localScale.x, 0.5f, 3f);
		}
		for (int i = 0; i < allRigs.Length; i++)
		{
			rigs.Add(allRigs[i]);
		}
		foreach (GameObject rider in riders.riders)
		{
			riderinfo = rider.transform.GetComponent<Unit>();
			if (riderinfo.unitType != Unit.UnitType.Warmachine && riderinfo.RigType != Stitcher.TransformCatalog.RigType.None)
			{
				Rigidbody[] allRigs2 = rider.transform.GetComponentInChildren<RigidbodyHolder>().AllRigs;
				for (int j = 0; j < allRigs2.Length; j++)
				{
					rigs.Add(allRigs2[j]);
				}
			}
		}
		WeaponHandler componentInChildren = unit.GetComponentInChildren<WeaponHandler>();
		if ((bool)componentInChildren && (bool)componentInChildren.rightWeapon && (bool)componentInChildren.rightWeapon.rigidbody)
		{
			rigs.Add(componentInChildren.rightWeapon.rigidbody);
		}
		if ((bool)componentInChildren && (bool)componentInChildren.leftWeapon && (bool)componentInChildren.leftWeapon.rigidbody)
		{
			rigs.Add(componentInChildren.leftWeapon.rigidbody);
		}
		if (force != 0f)
		{
			if (unitinfo.unitType == Unit.UnitType.Warmachine || unitinfo.RigType == Stitcher.TransformCatalog.RigType.None)
			{
				return;
			}
			for (int k = 0; k < rigs.Count; k++)
			{
				if ((bool)rigs[k])
				{
					rigs[k].AddForce(base.transform.up * force, ForceMode.VelocityChange);
				}
			}
		}
		rigs.Clear();
	}
}
