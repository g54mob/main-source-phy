using System.Collections;
using UnityEngine;

public class ExplosionSpawnObject : ExplosionEffect
{
	public GameObject objectToSpawnAsChild;

	public float maxDelay;

	public override void DoEffect(GameObject target)
	{
		StartCoroutine(DelaySpawn(target));
	}

	public IEnumerator DelaySpawn(GameObject target)
	{
		yield return new WaitForSeconds(Random.Range(0f, maxDelay));
		TargetableEffect[] componentsInChildren = Object.Instantiate(objectToSpawnAsChild, base.transform.position, base.transform.rotation, target.transform).GetComponentsInChildren<TargetableEffect>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].DoEffect(base.transform, target.transform);
		}
	}
}
