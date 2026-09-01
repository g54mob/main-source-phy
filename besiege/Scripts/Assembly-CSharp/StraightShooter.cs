using System.Collections;
using UnityEngine;

public class StraightShooter : MonoBehaviour
{
	public GameObject projectile;

	public float delay;

	public int amountToSpawn = 3;

	public float delayBetweenSpawns = 0.2f;

	public bool SmartTargeting;

	public bool seperateTarget;

	public float spread = 0.5f;

	public Collider colliderToIgnore;

	private BlockBehaviour target;

	private bool hasTarget;

	private bool shooting;

	private float time;

	private Transform physGoal;

	private Vector3 targetPosition;

	private Vector3 machinePos;

	private Vector3 distance;

	private float effectiveDistance;

	public float Range;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			physGoal = ((!StatMaster.isMP) ? GameObject.Find("PHYSICS GOAL").transform : ReferenceMaster.physicsGoalInstance);
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (time < delay)
		{
			time += Time.deltaTime;
		}
		else if (!shooting)
		{
			if (!seperateTarget)
			{
				GetNewTarget();
			}
			machinePos = Machine.Active().MachineCenterPos;
			distance = machinePos - base.transform.position;
			effectiveDistance = distance.sqrMagnitude;
			if (effectiveDistance <= Range)
			{
				StartCoroutine(Shoot((amountToSpawn != 1) ? delayBetweenSpawns : 0f));
			}
		}
	}

	private IEnumerator Shoot(float waitTime)
	{
		shooting = true;
		for (int i = 0; i < amountToSpawn; i++)
		{
			if (seperateTarget)
			{
				GetNewTarget();
				targetPosition = target.transform.position;
			}
			else if (hasTarget)
			{
				targetPosition = target.transform.position + Random.insideUnitSphere * spread;
			}
			if (!hasTarget && seperateTarget)
			{
				i--;
				continue;
			}
			if (!hasTarget)
			{
				yield break;
			}
			GameObject projectileGO = (GameObject)Object.Instantiate(projectile, base.transform.position, Quaternion.FromToRotation(Vector3.forward, targetPosition - base.transform.position), GetPhysGoal());
			Collider col = projectileGO.GetComponent<Collider>();
			if (col != null)
			{
				Physics.IgnoreCollision(colliderToIgnore, col, true);
			}
			yield return new WaitForSeconds(waitTime);
		}
		time = 0f;
		shooting = false;
	}

	private Transform GetPhysGoal()
	{
		return (!StatMaster.isMP) ? physGoal : ReferenceMaster.physicsGoalInstance;
	}

	protected void GetNewTarget()
	{
		int closestMachine = FactionsController.GetClosestMachine(base.transform.position);
		if (closestMachine != -1)
		{
			BlockBehaviour blockBehaviour = ((!SmartTargeting) ? ReferenceMaster.GetRandomBlock((uint)closestMachine) : ReferenceMaster.GetRandomIntactBlock((uint)closestMachine));
			if (!object.ReferenceEquals(blockBehaviour, null) && !blockBehaviour.IsDestroyed)
			{
				target = blockBehaviour;
				hasTarget = true;
			}
		}
	}
}
