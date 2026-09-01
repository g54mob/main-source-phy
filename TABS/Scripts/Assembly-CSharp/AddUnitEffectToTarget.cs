using Landfall.TABS;
using UnityEngine;

public class AddUnitEffectToTarget : MonoBehaviour
{
	public delegate void AddedEffectEventHandler(Unit attacker, DataHandler targetData);

	public UnitEffectBase EffectPrefab;

	private Unit ownUnit;

	private TeamHolder rootTeamHolder;

	public event AddedEffectEventHandler AddedEffect;

	private void Start()
	{
		TeamHolder.GetTeamRelevantComponents(base.transform.root, ref ownUnit, ref rootTeamHolder);
	}

	public void Go()
	{
		Unit component = base.transform.root.GetComponent<Unit>();
		if ((bool)component)
		{
			AddEffect(component, component.data.targetData);
		}
	}

	public void AddEffect(Unit attacker, DataHandler targetData)
	{
		if (!(attacker == null) && (!targetData || !targetData.Dead) && (bool)targetData)
		{
			EffectPrefab.GetType();
			UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(targetData.unit.transform.gameObject, EffectPrefab);
			if (unitEffectBase == null)
			{
				UnitEffectBase unitEffectBase2 = Object.Instantiate(EffectPrefab, targetData.unit.transform.root);
				unitEffectBase2.transform.position = targetData.unit.transform.position;
				unitEffectBase2.transform.rotation = Quaternion.LookRotation(targetData.mainRig.position - attacker.data.mainRig.position);
				unitEffectBase = unitEffectBase2;
				TeamHolder.AddTeamHolder(unitEffectBase2.gameObject, attacker, rootTeamHolder);
				unitEffectBase = unitEffectBase2;
				unitEffectBase.DoEffect();
			}
			else
			{
				unitEffectBase.transform.rotation = Quaternion.LookRotation(targetData.mainRig.position - attacker.data.mainRig.position);
				unitEffectBase.Ping();
			}
			this.AddedEffect?.Invoke(attacker, targetData);
		}
	}
}
