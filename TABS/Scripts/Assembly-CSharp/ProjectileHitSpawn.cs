using UnityEngine;

public class ProjectileHitSpawn : ProjectileHitEffect
{
	public ObjectToSpawn objectToSpawn;

	private void Start()
	{
	}

	public override bool DoEffect(HitData hit)
	{
		Quaternion rotation = Quaternion.identity;
		if (objectToSpawn.spawnRotation == ObjectToSpawn.SpawnRotation.forward)
		{
			rotation = Quaternion.LookRotation(base.transform.forward);
		}
		if (objectToSpawn.spawnRotation == ObjectToSpawn.SpawnRotation.normal)
		{
			rotation = Quaternion.LookRotation(hit.normal);
		}
		Object.Instantiate(objectToSpawn.objectToSpawn, base.transform.position, rotation);
		return false;
	}
}
