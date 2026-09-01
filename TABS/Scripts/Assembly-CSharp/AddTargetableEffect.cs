using Landfall.TABS;
using UnityEngine;

public class AddTargetableEffect : TargetableEffect
{
	public UnitEffectBase EffectPrefab;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	private void Start()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform.root, ref unit, ref rootTeamHolder);
	}

	public override void DoEffect(Transform startPoint, Transform target)
	{
		DataHandler componentInChildren = target.transform.root.GetComponentInChildren<DataHandler>();
		if ((!componentInChildren || !componentInChildren.Dead) && (bool)componentInChildren)
		{
			UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(target.gameObject, EffectPrefab);
			if (unitEffectBase == null)
			{
				UnitEffectBase unitEffectBase2 = Object.Instantiate(EffectPrefab, target.transform.root);
				unitEffectBase2.transform.position = target.transform.root.position;
				unitEffectBase2.damageMultiplier = damageMultiplier;
				unitEffectBase2.DoEffect();
				TeamHolder.AddTeamHolder(unitEffectBase2.gameObject, unit, rootTeamHolder);
			}
			else
			{
				unitEffectBase.Ping();
			}
		}
	}

	public override void DoEffect(Vector3 startPoint, Vector3 endPoint, Rigidbody targetRig = null)
	{
	}
}
