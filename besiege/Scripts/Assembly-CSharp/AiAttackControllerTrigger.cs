using System.Collections;
using UnityEngine;

public class AiAttackControllerTrigger : MonoBehaviour
{
	public float attackRate = 1f;

	public ParticleSystem hitParticle;

	public RandomSoundController hitSound;

	public EnemyAISimple EnemyAiCode;

	public Vector3 hitVelToAdd;

	public float hitPower = 10000f;

	public float blockDamageAmount = 1f;

	public LerpRotation animationController;

	public Transform rayPos;

	public RandomSoundController sfx;

	public MeshFilter meshFilter;

	public Mesh raisedPos;

	public Mesh attackPos;

	public Rigidbody myBody;

	public Collider myCollider;

	public ParticleSystem swingParticle;

	public ParticleSystem flashParticle;

	public ParticleSystem chunkParticle;

	public ParticleSystem ringParticle;

	public ParticleSystem cogParticle;

	public SineRotate SineRotCode;

	public float lastTarget;

	public float targetInterval = 3f;

	public float lastAttack;

	private bool isAnimating;

	private RaycastHit hit;

	private void Start()
	{
		myBody = GetComponent<Rigidbody>();
		myCollider = GetComponent<Collider>();
		lastAttack -= Random.Range(0f, 0.5f);
	}

	private void Update()
	{
		if (StatMaster.levelSimulating && !EnemyAiCode.isDead)
		{
			lastTarget += Time.deltaTime;
			if (lastTarget > targetInterval)
			{
				EnemyAiCode.GetNewTarget();
				lastTarget = 0f;
			}
			lastAttack += Time.deltaTime;
			if (lastAttack > attackRate)
			{
				CastRay();
				lastAttack = 0f - Random.Range(0f, 0.5f);
			}
		}
	}

	private IEnumerator ColliderPulse()
	{
		if (!isAnimating)
		{
			myBody.WakeUp();
			if (myCollider != null)
			{
				myCollider.enabled = true;
			}
			yield return new WaitForFixedUpdate();
			if (myCollider != null)
			{
				myCollider.enabled = false;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.isTrigger && (bool)other.attachedRigidbody)
		{
			BlockHealthBar component = other.attachedRigidbody.GetComponent<BlockHealthBar>();
			if (component != null)
			{
				StartCoroutine(AttackEffect(other.attachedRigidbody));
			}
		}
	}

	private void CastRay()
	{
		if (Physics.SphereCast(rayPos.position, 0.5f, rayPos.forward, out hit, 5f))
		{
			Rigidbody attachedRigidbody = hit.collider.attachedRigidbody;
			if ((bool)attachedRigidbody && ((bool)attachedRigidbody.GetComponent<BlockHealthBar>() || (bool)attachedRigidbody.GetComponent<AiAttackMeTag>()))
			{
				StartCoroutine(AttackEffect(attachedRigidbody));
			}
		}
	}

	private IEnumerator AttackAnim(Rigidbody objToAttack)
	{
		if (!isAnimating)
		{
			isAnimating = true;
			animationController.StopRotation();
			yield return null;
			animationController.Anim1();
			yield return new WaitForSeconds(animationController.lerpSpeeds[1]);
			animationController.Anim2();
			yield return new WaitForSeconds(animationController.lerpSpeeds[2]);
			yield return new WaitForSeconds(0.25f);
			animationController.ReturnToStartAnim();
			yield return new WaitForSeconds(animationController.lerpSpeeds[0]);
			isAnimating = false;
		}
	}

	private IEnumerator AttackEffect(Rigidbody objToAttack)
	{
		if (!isAnimating)
		{
			isAnimating = true;
			meshFilter.mesh = attackPos;
			objToAttack.AddForce(base.transform.forward * hitPower);
			BlockHealthBar blockHealth = objToAttack.GetComponent<BlockHealthBar>();
			BleedOnJointBreak bleedOnBreak = objToAttack.GetComponent<BleedOnJointBreak>();
			if (blockHealth != null)
			{
				blockHealth.DamageBlock(blockDamageAmount);
			}
			else if (bleedOnBreak != null)
			{
				bleedOnBreak.KillMe(true);
			}
			hitParticle.Play();
			sfx.Play();
			swingParticle.Play();
			flashParticle.Play();
			chunkParticle.Play();
			yield return new WaitForSeconds(0.3f);
			meshFilter.mesh = raisedPos;
			isAnimating = false;
		}
	}
}
