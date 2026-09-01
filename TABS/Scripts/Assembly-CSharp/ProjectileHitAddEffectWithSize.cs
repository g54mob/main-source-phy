using Landfall.TABS;
using UnityEngine;

public class ProjectileHitAddEffectWithSize : ProjectileHitEffect
{
	public UnitEffectBase EffectPrefab;

	public bool ignoreArmor;

	public bool onlyOnce;

	public bool addToDead;

	private Unit unit;

	private TeamHolder rootTeamHolder;

	private void Start()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform.root, ref unit, ref rootTeamHolder);
	}

	public override bool DoEffect(HitData hit)
	{
		DataHandler componentInChildren = hit.transform.root.GetComponentInChildren<DataHandler>();
		if (!componentInChildren)
		{
			return false;
		}
		if (componentInChildren.Dead && !addToDead)
		{
			return false;
		}
		if (!ignoreArmor && (bool)hit.transform.GetComponent<Armor>())
		{
			return false;
		}
		bool flag = componentInChildren.mainRig.GetComponentInChildren<Collider>().bounds.size.magnitude + componentInChildren.unit.thickness > 2f;
		UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(hit.transform.gameObject, EffectPrefab);
		if (unitEffectBase == null)
		{
			unitEffectBase = Object.Instantiate(EffectPrefab, hit.transform.root);
			unitEffectBase.transform.position = hit.transform.root.position;
			unitEffectBase.DoEffect();
			if (flag)
			{
				ParticleSystem componentInChildren2 = unitEffectBase.GetComponentInChildren<ParticleSystem>();
				if ((bool)componentInChildren2)
				{
					componentInChildren2.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 8f;
					componentInChildren2.transform.SetParent(base.transform);
					componentInChildren2.transform.localPosition = Vector3.zero;
				}
			}
			TeamHolder.AddTeamHolder(unitEffectBase.gameObject, unit, rootTeamHolder);
		}
		else
		{
			if (onlyOnce)
			{
				return false;
			}
			unitEffectBase.Ping();
			if (flag)
			{
				ParticleSystem particleSystem = Object.Instantiate(EffectPrefab.GetComponentInChildren<ParticleSystem>());
				particleSystem.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 8f;
				particleSystem.transform.SetParent(base.transform);
				particleSystem.transform.localPosition = Vector3.zero;
			}
		}
		return false;
	}
}
