using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
	public float attackRange = 5f;

	public float attackRate = 1f;

	public float rayLength = 1f;

	public ParticleSystem hitParticle;

	public RandomSoundController hitSound;

	public EnemyAISimple EnemyAiCode;

	public float impactForceMultiplier = 10f;

	public float blockDamageAmount = 1f;

	public Transform forwardObj;

	private Rigidbody myBody;

	private RaycastHit hit;

	private void Start()
	{
		InvokeRepeating("Attack", Random.Range(0f, attackRate), attackRate);
	}

	private void Attack()
	{
		if (StatMaster.levelSimulating && EnemyAiCode.runVec.sqrMagnitude < attackRange)
		{
			ShootRay();
		}
	}

	private void ShootRay()
	{
		hitParticle.Play();
		if (!Physics.Raycast(forwardObj.position, forwardObj.forward, out hit, attackRange))
		{
			return;
		}
		Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
		if (!(attachedRigidbody == null))
		{
			if (myBody == null)
			{
				myBody = GetComponent<Rigidbody>();
			}
			attachedRigidbody.AddForce(myBody.velocity * impactForceMultiplier);
			BlockHealthBar component = attachedRigidbody.GetComponent<BlockHealthBar>();
			if (component != null)
			{
				component.DamageBlock(blockDamageAmount);
			}
		}
	}
}
