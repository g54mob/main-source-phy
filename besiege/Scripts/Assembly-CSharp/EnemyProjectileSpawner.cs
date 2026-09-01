using System.Collections;
using UnityEngine;

public class EnemyProjectileSpawner : MonoBehaviour
{
	public GameObject projectilePrefab;

	[SerializeField]
	private Transform projectileSpawnPos;

	[SerializeField]
	private float force;

	[SerializeField]
	private float shootingDelay;

	[SerializeField]
	private float rotationSpeed;

	public bool isAlive = true;

	private Rigidbody thisRB;

	private Quaternion rot;

	private Coroutine shootingCor;

	private Vector3 direction;

	private BlockBehaviour target;

	private void Start()
	{
		thisRB = GetComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		thisRB.isKinematic = false;
		isAlive = false;
		WinCondition.currentObjsCompleted++;
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && isAlive)
		{
			if (target == null)
			{
				GetTarget();
			}
			if (shootingCor == null)
			{
				shootingCor = StartCoroutine(Shoot());
			}
		}
	}

	private void FixedUpdate()
	{
		if (target != null && isAlive)
		{
			rot = Quaternion.LookRotation(-(target.transform.position - base.transform.position), Vector3.up);
			rot = Quaternion.Slerp(target.transform.rotation, rot, rotationSpeed * Time.fixedDeltaTime);
			thisRB.MoveRotation(rot);
		}
	}

	private IEnumerator Shoot()
	{
		yield return new WaitForSecondsRealtime(shootingDelay);
		direction = (target.transform.position - base.transform.position).normalized;
		GameObject projectile = Object.Instantiate(projectilePrefab, projectileSpawnPos.position, Quaternion.identity) as GameObject;
		Rigidbody rb = projectile.GetComponent<Rigidbody>();
		rb.AddForce(direction * force, ForceMode.Impulse);
		shootingCor = null;
		GetTarget();
	}

	private bool GetTarget()
	{
		int closestMachine = FactionsController.GetClosestMachine(base.transform.position);
		if (closestMachine != -1)
		{
			BlockBehaviour randomIntactBlock = ReferenceMaster.GetRandomIntactBlock((uint)closestMachine);
			if (object.ReferenceEquals(randomIntactBlock, null) || randomIntactBlock.IsDestroyed)
			{
				return false;
			}
			target = randomIntactBlock;
			return true;
		}
		return false;
	}
}
