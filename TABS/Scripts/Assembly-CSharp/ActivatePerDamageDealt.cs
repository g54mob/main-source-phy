using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class ActivatePerDamageDealt : MonoBehaviour
{
	private Unit unit;

	private List<ObjectToActivate> objectsToActivate;

	private int activateIndex;

	private float damageTracker;

	public float requiredDamage = 100f;

	private void Start()
	{
		unit = base.transform.GetComponentInParent<SetParent>().parentBefore.transform.root.GetComponent<Unit>();
		objectsToActivate = new List<ObjectToActivate>();
		objectsToActivate.AddRange(base.transform.GetComponentsInChildren<ObjectToActivate>());
	}

	private void Update()
	{
		damageTracker = unit.damageDealt - requiredDamage * (float)(activateIndex + 1);
		if (damageTracker > requiredDamage && activateIndex < objectsToActivate.Count)
		{
			objectsToActivate[activateIndex].FirstActivateEvent();
			activateIndex++;
		}
	}
}
