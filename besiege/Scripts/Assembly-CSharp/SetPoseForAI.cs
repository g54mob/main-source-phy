using System;
using System.Collections;
using UnityEngine;

[AddComponentMenu("AI/SetPoseForAI")]
public class SetPoseForAI : MonoBehaviour
{
	[Header("General References")]
	public MeshFilter meshFilter;

	public Mesh[] StandingPoses;

	public Mesh[] FleeingPoses;

	public Mesh[] CowardPoses;

	public Mesh[] ChargingPoses;

	public Mesh[] AttackingPoses;

	public Mesh[] SuffocatingPoses;

	public Mesh[] DeathPoses;

	[Header("Attack animation Parameters")]
	public int PoseToAttack;

	public bool fixAttackCycle;

	public float TotalAttackAnimationTime = 0.3f;

	public Mesh[] MoveAttackingPoses;

	public ParticleSystem[] attackParticles;

	public int particleStopOffset;

	public bool useStandingForPursue = true;

	[HideInInspector]
	public bool StopScript;

	private EntityAI aiCode;

	private bool justAttacked;

	private EntityAI.EntityState previousState;

	private LevelEntity levelEntity;

	private bool simPhys;

	[Header("Animator Settings")]
	public Animator animator;

	public float velocityToAnimationScale = 1f;

	public float animationSpeedClamp = 10f;

	public float idleSpeed;

	[SerializeField]
	protected float animationOffset;

	private int attackMesh;

	private float currentTime;

	private Mesh[] currentArray = new Mesh[0];

	private int currentState;

	private bool isDrowning;

	private bool isSwimming;

	[HideInInspector]
	public bool updatePoses = true;

	[Header("Animate Movement")]
	[Tooltip("Will progress through mesh arrays")]
	public bool animateWhileMoving;

	public float movementAnimationTime = 2f;

	private float movementTimer;

	[Header("Used for cyceling through meshs")]
	public bool cycleStates;

	public float[] cycleDuration = new float[1] { 0.5f };

	private int lastParticle = -1;

	private int animationPose;

	protected void Awake()
	{
		if (!StatMaster.levelSimulating)
		{
			if (WaterController.Exist && base.transform.position.y < WaterController.waterTransformHeight)
			{
				DisplayDrowning();
			}
			else
			{
				SetPose(StandingPoses);
			}
			animationOffset = UnityEngine.Random.Range(0f, 1f);
		}
		aiCode = GetComponent<EntityAI>();
		levelEntity = aiCode.levelEntity;
		simPhys = !StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim;
	}

	protected void OnEnable()
	{
		if (animator != null)
		{
			animator.SetFloat("AnimationOffset", animationOffset);
		}
	}

	protected void Update()
	{
		if ((!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim) && !StopScript && StatMaster.levelSimulating && !StatMaster.GodTools.GravityDisabled && !aiCode.isDead)
		{
			if (previousState != aiCode.disposition.myState || justAttacked)
			{
				justAttacked = false;
				previousState = aiCode.disposition.myState;
				ChangeMesh(aiCode.disposition.myState);
			}
			if (updatePoses)
			{
				UpdatePose();
			}
			if (animator != null)
			{
				float num = 0f;
				num = ((!aiCode.my.basicInfo._inWater) ? 10f : ((!aiCode.selfRighting.Grabbed) ? Mathf.Clamp(Mathf.Max(velocityToAnimationScale * aiCode.movement.velocitySqr, idleSpeed), 0f - animationSpeedClamp, animationSpeedClamp) : ((!aiCode.selfRighting.CanBreakGrab) ? 0f : 10f)));
				animator.SetFloat("Speed", num);
			}
		}
	}

	public void ChangeMesh(EntityAI.EntityState state)
	{
		if (StatMaster.isMP && StatMaster.isHosting && simPhys && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.ChangeMesh, (byte)state);
		}
		isDrowning = false;
		isSwimming = false;
		switch (state)
		{
		case EntityAI.EntityState.Idle:
			SetPose(StandingPoses);
			StopParticles();
			break;
		case EntityAI.EntityState.Fallen:
		case EntityAI.EntityState.Grabbed:
			SetPose(FleeingPoses);
			StopParticles();
			break;
		case EntityAI.EntityState.Fleeing:
			if (aiCode.retreating.coward && CowardPoses.Length > 0)
			{
				SetPose(CowardPoses);
			}
			else
			{
				SetPose(FleeingPoses);
			}
			StopParticles();
			break;
		case EntityAI.EntityState.Pursuing:
			if (useStandingForPursue && aiCode.disposition.currentBehaviour.Action != EntityAI.Action.ApproachTarget)
			{
				SetPose(StandingPoses);
			}
			else
			{
				SetPose(ChargingPoses);
			}
			StopParticles();
			break;
		case EntityAI.EntityState.FactionCharge:
		case EntityAI.EntityState.Close:
			SetPose(ChargingPoses);
			StopParticles();
			break;
		case EntityAI.EntityState.Suffocating:
			isDrowning = true;
			DisplayDrowning();
			StopParticles();
			break;
		case EntityAI.EntityState.Dead:
			SetPose(DeathPoses);
			StopParticles();
			if (animator != null)
			{
				animator.enabled = false;
			}
			break;
		case EntityAI.EntityState.Stationary:
		case EntityAI.EntityState.Attacking:
			SetAttackPose(attackMesh);
			break;
		case EntityAI.EntityState.Ungrounded:
			if (!aiCode.my.killingHandler.canSuffocate && (aiCode.my.basicInfo._inWater || (WaterController.Exist && !(base.transform.position.y > WaterController.waterTransformHeight))))
			{
				isSwimming = true;
				DisplayDrowning();
				StopParticles();
			}
			break;
		case EntityAI.EntityState.Controlled:
		case EntityAI.EntityState.TacticalRetreat:
		case EntityAI.EntityState.Strafing:
		case EntityAI.EntityState.CantMove:
			break;
		}
	}

	private void DisplayDrowning()
	{
		if (SuffocatingPoses.Length > 0)
		{
			SetPose(SuffocatingPoses);
		}
		else
		{
			SetPose(FleeingPoses);
		}
	}

	private void UpdatePose()
	{
		if ((cycleStates || isDrowning || isSwimming) && currentArray.Length > 1)
		{
			CyclePose(currentArray);
		}
	}

	private void SetPose(Mesh[] arr)
	{
		if ((float)arr.Length > 0f)
		{
			if (cycleStates || isDrowning || isSwimming)
			{
				SetPoseCycle(arr, 0);
			}
			else
			{
				meshFilter.sharedMesh = arr[UnityEngine.Random.Range(0, arr.Length)];
			}
		}
	}

	private void CyclePose(Mesh[] arr)
	{
		if (currentTime < float.Epsilon)
		{
			if (currentState == arr.Length)
			{
				currentState = 0;
			}
			SetPoseCycle(arr, currentState);
		}
		else
		{
			currentTime -= Time.deltaTime;
		}
	}

	private void SetPoseCycle(Mesh[] arr, int state)
	{
		currentState = state;
		meshFilter.sharedMesh = arr[currentState];
		currentState++;
		if (isDrowning)
		{
			currentTime = 0.1f;
		}
		else if (isSwimming)
		{
			currentTime = UnityEngine.Random.Range(0.4f, 0.6f);
		}
		else
		{
			currentTime = cycleDuration[currentState % arr.Length];
		}
		currentArray = arr;
	}

	private void SetAttackPose(int i)
	{
		if (!aiCode.disposition.canAttack && StandingPoses.Length > 0)
		{
			SetPose(StandingPoses);
			return;
		}
		if (animator != null)
		{
			animator.SetTrigger("Attack");
			return;
		}
		Mesh[] attackPoses = GetAttackPoses();
		attackMesh = i;
		if (attackPoses.Length != 0)
		{
			meshFilter.sharedMesh = attackPoses[attackMesh];
		}
		StartParticle(attackMesh);
		attackMesh++;
		if (attackMesh >= attackPoses.Length)
		{
			attackMesh = 0;
		}
	}

	public void AttackPose()
	{
		if (AttackingPoses.Length != 0)
		{
			StopScript = true;
			SetAttackPose(0);
			StopScript = false;
			justAttacked = true;
		}
	}

	public IEnumerator AttackAnim(EntityAI.Targeting target, float dist, Action<EntityAI.Targeting, float> attackAction)
	{
		if (AttackingPoses.Length == 0)
		{
			yield break;
		}
		StopScript = true;
		int i = 0;
		SetAttackPose(0);
		if (fixAttackCycle)
		{
			attackMesh = 0;
		}
		for (; i < AttackingPoses.Length; i++)
		{
			if (!StatMaster.levelSimulating)
			{
				break;
			}
			if (aiCode.isDead)
			{
				KillPose();
				break;
			}
			if (aiCode.selfRighting.Grabbed)
			{
				if (StandingPoses.Length > 0)
				{
					meshFilter.sharedMesh = StandingPoses[UnityEngine.Random.Range(0, StandingPoses.Length)];
				}
				break;
			}
			ChangeMesh(EntityAI.EntityState.Attacking);
			if (target != null && PoseToAttack == i)
			{
				attackAction(target, dist);
			}
			float duration = TotalAttackAnimationTime / (float)AttackingPoses.Length;
			yield return new WaitForSeconds(duration);
		}
		attackMesh = 0;
		StopScript = false;
		justAttacked = true;
	}

	private void StartParticle(int i)
	{
		if (attackParticles.Length > 0 && i != lastParticle)
		{
			int num = lastParticle + particleStopOffset;
			Mesh[] attackPoses = GetAttackPoses();
			if (num < 0)
			{
				num = attackPoses.Length - 1;
			}
			else if (num >= attackPoses.Length)
			{
				num = 0;
			}
			if (num >= 0 && num < attackParticles.Length)
			{
				attackParticles[num].Stop();
			}
			lastParticle = i;
			if (i == 0)
			{
				StartCoroutine(ChargeParticle(i, 2f));
			}
			else if (i < attackParticles.Length)
			{
				attackParticles[i].Play();
			}
		}
	}

	private Mesh[] GetAttackPoses()
	{
		Mesh[] result = AttackingPoses;
		if (aiCode.movement.moving && MoveAttackingPoses.Length != 0)
		{
			result = MoveAttackingPoses;
		}
		return result;
	}

	private void StopParticles()
	{
		lastParticle = -1;
		for (int i = 0; i < attackParticles.Length; i++)
		{
			attackParticles[i].Stop();
		}
	}

	private IEnumerator ChargeParticle(int i, float duration)
	{
		ParticleSystem p = attackParticles[i];
		ParticleSystem.EmissionModule em = p.emission;
		float rate = em.rate.constant;
		yield return new WaitForSeconds(0.15f);
		em.rate = 0f;
		p.Play();
		for (float t = 0f; t < duration; t += Time.deltaTime)
		{
			float pct = t / duration;
			em.rate = pct * pct * rate;
			yield return null;
		}
		em.rate = rate;
	}

	public void KillPose()
	{
		if ((bool)aiCode)
		{
			if (StopScript)
			{
				StopScript = false;
				StopAllCoroutines();
			}
			ChangeMesh(EntityAI.EntityState.Dead);
			if ((bool)aiCode.my.basicInfo && !aiCode.my.basicInfo.noRigidbody)
			{
				aiCode.my.Rigidbody.angularDrag = 10f;
			}
			aiCode.StopDizzyParticles();
			base.enabled = false;
		}
	}

	public void MoveAnim(EntityAI.EntityState state)
	{
		Mesh[] array = null;
		switch (state)
		{
		case EntityAI.EntityState.Idle:
			if (StandingPoses.Length > 0 && aiCode.grounded)
			{
				array = StandingPoses;
			}
			else if (FleeingPoses.Length > 0)
			{
				array = FleeingPoses;
			}
			break;
		case EntityAI.EntityState.Fleeing:
			if (aiCode.retreating.coward && CowardPoses.Length > 0)
			{
				array = CowardPoses;
			}
			else if (FleeingPoses.Length > 0)
			{
				array = FleeingPoses;
			}
			break;
		case EntityAI.EntityState.Pursuing:
			if (ChargingPoses.Length > 0)
			{
				array = ChargingPoses;
			}
			break;
		case EntityAI.EntityState.FactionCharge:
			if (ChargingPoses.Length > 0)
			{
				array = ChargingPoses;
			}
			break;
		case EntityAI.EntityState.Close:
			if (ChargingPoses.Length > 0)
			{
				array = ChargingPoses;
			}
			break;
		case EntityAI.EntityState.Dead:
			array = DeathPoses;
			break;
		case EntityAI.EntityState.Stationary:
			if (StandingPoses.Length > 0 && aiCode.grounded)
			{
				array = StandingPoses;
			}
			else if (FleeingPoses.Length > 0)
			{
				array = FleeingPoses;
			}
			break;
		}
		movementTimer += Time.deltaTime;
		if (!(movementTimer >= movementAnimationTime))
		{
			return;
		}
		movementTimer -= movementAnimationTime;
		if (array != null && array.Length != 0)
		{
			if (animationPose >= array.Length)
			{
				animationPose = 0;
			}
			meshFilter.sharedMesh = array[animationPose];
			animationPose++;
		}
	}
}
