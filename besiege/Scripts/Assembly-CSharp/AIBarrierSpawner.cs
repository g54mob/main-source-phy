using System.Collections;
using UnityEngine;

public class AIBarrierSpawner : MonoBehaviour
{
	public enum SpawnMethod
	{
		Instant = 0,
		FloatUp = 1
	}

	public BarrierExplosionEnabler barrierEnabler;

	public SpawnMethod method = SpawnMethod.FloatUp;

	public Vector3 positionOffset = Vector3.zero;

	public float lerpSpeed = 0.5f;

	public GameObject instanceToSpawn;

	public int maxNumberOfInstances;

	public float timerMin;

	public float timerMax;

	public bool isBeingSpawned;

	public Transform spawnLocation;

	public Transform AiSpawnParent;

	private void Update()
	{
		if (!StatMaster.levelSimulating || !(barrierEnabler != null))
		{
			return;
		}
		for (int i = 0; i < barrierEnabler.Targets.Count; i++)
		{
			if (barrierEnabler.Targets[i].isDead)
			{
				barrierEnabler.Targets.Remove(barrierEnabler.Targets[i]);
				barrierEnabler.targetEliminated++;
			}
		}
		if (!isBeingSpawned && barrierEnabler.Targets.Count < maxNumberOfInstances)
		{
			StartCoroutine(Spawn());
		}
	}

	private IEnumerator Spawn()
	{
		float lerpValue = 0f;
		isBeingSpawned = true;
		yield return new WaitForSeconds(Random.Range(timerMin, timerMax));
		GameObject instance = Object.Instantiate(instanceToSpawn, spawnLocation.position + positionOffset, Quaternion.identity, AiSpawnParent.transform) as GameObject;
		AIGenericEntity entity = instance.GetComponent<AIGenericEntity>();
		EntityAI ai = entity.aiEntity;
		barrierEnabler.Targets.Add(ai);
		SpawnMethod spawnMethod = method;
		if (spawnMethod != SpawnMethod.Instant && spawnMethod == SpawnMethod.FloatUp)
		{
			entity.Rigidbody.isKinematic = true;
			float oldMinimalVelocity = ai.my.killingHandler.damageAmount.minimalVelocity;
			ai.my.killingHandler.damageAmount.minimalVelocity = 20f;
			float oldHealth = ai.health;
			ai.health = 1f;
			while (lerpValue != 1f)
			{
				if (ai.isDead)
				{
					barrierEnabler.Targets.Remove(ai);
					barrierEnabler.targetEliminated++;
					yield break;
				}
				if (!entity.Rigidbody.isKinematic)
				{
					entity.Rigidbody.isKinematic = true;
				}
				lerpValue += Time.deltaTime * lerpSpeed;
				lerpValue = ((!(lerpValue > 1f)) ? lerpValue : 1f);
				entity.Rigidbody.MovePosition(Vector3.Lerp(instance.transform.position, spawnLocation.position, lerpValue));
				yield return null;
			}
			ai.my.killingHandler.damageAmount.minimalVelocity = oldMinimalVelocity;
			ai.health = oldHealth;
			entity.Rigidbody.isKinematic = false;
		}
		EntityAI.Disposition disposition = ai.disposition;
		EntityAI.Disposition disposition2 = ai.disposition;
		EntityAI.Disposition disposition3 = ai.disposition;
		bool useStateMachine = true;
		disposition3.useBehaviours = true;
		disposition.AutomaticTargetSystem = (disposition2.useStateMachine = useStateMachine);
		isBeingSpawned = false;
	}

	private void OnTriggerEnter(Collider other)
	{
	}
}
