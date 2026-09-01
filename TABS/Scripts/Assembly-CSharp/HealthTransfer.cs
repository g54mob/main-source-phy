using System.Collections;
using Landfall.TABS;
using UnityEngine;

public class HealthTransfer : MonoBehaviour
{
	public void TransferHealth(Unit unit, float healthDiff)
	{
		StartCoroutine(TransferHP(unit, healthDiff));
	}

	private IEnumerator TransferHP(Unit unit, float healthDiff)
	{
		yield return new WaitForSeconds(0.05f);
		unit.data.lifeTime = 0.4f;
		unit.data.healthHandler.TakeDamage(unit.data.maxHealth - unit.data.health * healthDiff, base.transform.forward, null);
	}
}
