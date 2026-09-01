using Landfall.TABS;
using UnityEngine;

public class AddEffectToSelf : MonoBehaviour
{
	public UnitEffectBase EffectPrefab;

	public void Go()
	{
		Unit component = base.transform.root.GetComponent<Unit>();
		if (!(component != null))
		{
			return;
		}
		DataHandler targetData = component.data.targetData;
		if ((!targetData || !targetData.Dead) && (bool)targetData)
		{
			UnitEffectBase unitEffectBase = UnitEffectBase.AddEffectToTarget(component.transform.gameObject, EffectPrefab);
			if (unitEffectBase == null)
			{
				unitEffectBase = Object.Instantiate(EffectPrefab, component.transform.root);
				unitEffectBase.transform.SetPositionAndRotation(component.transform.position, Quaternion.LookRotation(component.data.mainRig.position));
				TeamHolder.AddTeamHolder(unitEffectBase.gameObject, component, null);
				unitEffectBase.DoEffect();
			}
			else
			{
				unitEffectBase.transform.rotation = Quaternion.LookRotation(component.data.mainRig.position);
				unitEffectBase.Ping();
			}
		}
	}
}
