using System;
using UnityEngine;

public abstract class UnitEffectBase : MonoBehaviour
{
	public int effectID;

	public float damageMultiplier = 1f;

	public bool ShouldSkipDeadTests { get; set; }

	public abstract void DoEffect();

	public abstract void Ping();

	public static UnitEffectBase AddEffectToTarget(GameObject target, UnitEffectBase EffectPrefab)
	{
		Type type = EffectPrefab.GetType();
		Component[] componentsInChildren = target.transform.root.GetComponentsInChildren(type);
		UnitEffectBase result = null;
		if (componentsInChildren != null)
		{
			UnitEffectBase[] array = new UnitEffectBase[componentsInChildren.Length];
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				array[i] = componentsInChildren[i] as UnitEffectBase;
			}
			UnitEffectBase component = EffectPrefab.GetComponent<UnitEffectBase>();
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].effectID == component.effectID)
				{
					result = array[j];
					break;
				}
			}
		}
		return result;
	}
}
