using Landfall.TABS;
using UnityEngine;

public class ExplosionAddEffect : ExplosionEffect
{
	public UnitEffectBase EffectPrefab;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	private void Start()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform.root, ref unit, ref rootTeamHolder);
	}

	public override void DoEffect(GameObject target)
	{
		UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(target, EffectPrefab);
		if (unitEffectBase == null)
		{
			unitEffectBase = Object.Instantiate(EffectPrefab, target.transform.root);
			unitEffectBase.transform.SetPositionAndRotation(target.transform.root.position, Quaternion.LookRotation(target.transform.root.position - base.transform.position));
			TeamHolder.AddTeamHolder(unitEffectBase.gameObject, unit, rootTeamHolder);
			unitEffectBase.DoEffect();
		}
		else
		{
			unitEffectBase.Ping();
		}
		TargetableEffect[] componentsInChildren = unitEffectBase.gameObject.GetComponentsInChildren<TargetableEffect>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].DoEffect(base.transform, target.transform);
		}
	}
}
