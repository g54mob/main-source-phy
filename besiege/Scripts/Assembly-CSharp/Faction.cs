using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Faction
{
	public string Name;

	[HideInInspector]
	public bool Neutralized;

	public List<EntityAI> Infantry = new List<EntityAI>();

	public Faction TargetFaction;

	[HideInInspector]
	public string Preference;

	public FactionsController.AttackOnlyEnum AttackOnlyTypeOf = FactionsController.AttackOnlyEnum.Both;

	public FactionsController.DiscriminantEnum Discrimination = FactionsController.DiscriminantEnum.Indiscriminant;

	public Vector3 Center;

	public float MaxInfantry;

	[HideInInspector]
	public float suddenLoss;

	public float LossOverTime;

	public float Loss;

	public float machineLoss;

	private Vector3 zero = Vector3.zero;

	public Faction(string name)
	{
		Name = name;
	}

	public Faction(string name, string pref, FactionsController.DiscriminantEnum discri, FactionsController.AttackOnlyEnum aoto)
	{
		Name = name;
		Preference = pref;
		AttackOnlyTypeOf = aoto;
		Discrimination = discri;
	}

	public void UpdateLoss()
	{
		if (!Neutralized && Infantry.Count != 0)
		{
			LossOverTime = suddenLoss / MaxInfantry;
			Loss = 1f - (float)Infantry.Count / MaxInfantry;
		}
	}

	public void UpdateCenter()
	{
		if (Neutralized || Infantry.Count == 0)
		{
			return;
		}
		Vector3 center = zero;
		int num = ((Infantry.Count <= 24) ? Infantry.Count : 25);
		for (int i = 0; i < Infantry.Count; i += Infantry.Count / num)
		{
			EntityAI entityAI = Infantry[i];
			if (!object.ReferenceEquals(entityAI, null))
			{
				center += entityAI.movement.PreviousPosition;
			}
		}
		center /= (float)Infantry.Count;
		Center = center;
	}
}
