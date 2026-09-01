using UnityEngine;

public class BreakIcicleOnIgnite : Drillable, IFireEffect
{
	public GameObject brokenPrefab;

	public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
	{
		Object.Instantiate(brokenPrefab, base.transform.position, base.transform.rotation, ReferenceMaster.physicsGoalInstance);
		base.gameObject.SetActive(false);
		return true;
	}
}
