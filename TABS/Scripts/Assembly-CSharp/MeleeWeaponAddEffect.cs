using System;
using Landfall.TABS;
using UnityEngine;

public class MeleeWeaponAddEffect : CollisionWeaponEffect
{
	public UnitEffectBase EffectPrefab;

	public bool ignoreTeamMates;

	public override void DoEffect(Transform hitTransform, Collision hit)
	{
		if (ignoreTeamMates)
		{
			Unit component = base.transform.root.GetComponent<Unit>();
			Unit component2 = hitTransform.root.GetComponent<Unit>();
			if ((bool)component && (bool)component2 && component.Team == component2.Team)
			{
				return;
			}
		}
		Type type = EffectPrefab.GetType();
		UnitEffectBase unitEffectBase = (UnitEffectBase)hitTransform.root.GetComponentInChildren(type);
		if (unitEffectBase == null)
		{
			GameObject obj = UnityEngine.Object.Instantiate(EffectPrefab.gameObject, hitTransform.root);
			obj.transform.position = hitTransform.root.position;
			obj.GetComponent<UnitEffectBase>().DoEffect();
		}
		else
		{
			unitEffectBase.Ping();
		}
	}
}
