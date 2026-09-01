using System.Collections;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
	public Transform myTransform;

	public Transform bowTransform;

	public Transform projectile;

	public Transform projectileSpawnPos;

	public float ShootHite = 4f;

	public Transform arrow;

	public float ShootTimer = 6f;

	public float randomShootTime = 0.5f;

	public float randomAimAmount = 1f;

	public float upAmountScaler = 0.1f;

	public ProjectilePhysArc projectilePhys;

	public float shootAngle = 60f;

	public float newShootForce = 10f;

	public Transform arrowSkinnedParent;

	public GameObject[] arrowArray;

	public float predictionScaler = 1f;

	public EnemyAISimple aiCode;

	public AudioSource arrowLoosingSound;

	public float maxRange = 100f;

	private float StartShootTimer = 6f;

	private Vector3 EndPos;

	private Vector3 MindAmePos;

	private float startTime;

	private void Start()
	{
		StartShootTimer = ShootTimer;
	}

	private void FixedUpdate()
	{
		if (StatMaster.levelSimulating && !aiCode.isDead && !StatMaster.GodTools.GravityDisabled)
		{
			Vector3 middlePosition = Machine.Active().MiddlePosition;
			Vector3 vector = middlePosition - base.transform.position;
			vector = new Vector3(vector.x, 0f, vector.z);
			aiCode.Rigidbody.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
		}
	}

	private void LateUpdate()
	{
		if (StatMaster.levelSimulating && !aiCode.isDead && !StatMaster.GodTools.GravityDisabled)
		{
			if (ShootTimer > 0f)
			{
				ShootTimer -= Time.deltaTime;
				return;
			}
			StartCoroutine(Shoot());
			ShootTimer = StartShootTimer;
		}
	}

	private bool GetProjectile(out Transform outProj)
	{
		arrowArray = GameObject.FindGameObjectsWithTag("ArrowRigStatic");
		if (arrowArray.Length > 0)
		{
			outProj = arrowArray[Random.Range(0, arrowArray.Length)].transform;
		}
		else
		{
			outProj = Object.Instantiate(projectile);
		}
		return true;
	}

	private IEnumerator Shoot()
	{
		yield return new WaitForSeconds(Random.Range(0f, randomShootTime));
		Vector3 randomPos = Random.insideUnitSphere * randomAimAmount;
		randomPos.y = 0f;
		Machine activeMachine = Machine.Active();
		if (activeMachine == null)
		{
			yield break;
		}
		BlockBehaviour randomBlock = activeMachine.GetRandomBlock();
		randomPos += ((!randomBlock) ? activeMachine.MiddlePosition : randomBlock.transform.position);
		if (!((projectileSpawnPos.position - randomPos).sqrMagnitude < 10000f))
		{
			yield break;
		}
		Vector3 smoothMiddle = activeMachine.SmoothFollowPosition;
		arrowLoosingSound.Play();
		Vector3 deltaPos = smoothMiddle;
		yield return null;
		deltaPos -= smoothMiddle;
		randomPos += deltaPos * predictionScaler;
		if (GetProjectile(out arrow))
		{
			arrow.parent = ReferenceMaster.physicsGoalInstance;
			arrow.rotation = projectileSpawnPos.rotation;
			arrow.position = projectileSpawnPos.position;
			GameObject arrowGO = arrow.gameObject;
			if (!arrowGO.activeSelf)
			{
				arrowGO.SetActive(true);
			}
			ArrowController arrowController = arrow.GetComponent<ArrowController>();
			if (arrowController != null)
			{
				arrowController.ResetRigidbody();
			}
			else
			{
				Debug.LogError("ArcherAI: Arrow controller is null!");
			}
			Collider arrowCollider = arrow.GetComponent<Collider>();
			Collider ownCollider = GetComponent<Collider>();
			if (ownCollider != null && arrowCollider != null)
			{
				Physics.IgnoreCollision(arrowCollider, ownCollider);
			}
			else
			{
				Debug.LogError(string.Concat("ArcherAI: ownCollider=", ownCollider, " arrowCollider=", arrowCollider));
			}
			Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
			if (arrowBody != null)
			{
				arrowBody.AddForce(projectilePhys.BallisticVel(randomPos, shootAngle) * newShootForce);
			}
			else
			{
				Debug.LogError("ArcherAI: arrowBody is null!");
			}
		}
	}
}
