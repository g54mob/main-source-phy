using System;
using UnityEngine;

public class EnemyAISimple : BasicInfo, IExplosionEffect
{
	private static readonly float bobLerpSpeed = 10f;

	public VisibilityTracker visibilityTracker;

	public Transform visObject;

	public float health = 500f;

	[HideInInspector]
	public float maxHealth;

	public float projectileDeflection;

	public InjuryController injuryController;

	public BleedOnJointBreak bleedOnJointBreak;

	public Transform goal;

	public bool runAway;

	public float runAwaySpeed = -12f;

	public float runAwayBobAmount = 1f;

	public float bobRate = 1f;

	public bool canBob = true;

	public float runAwayLerpRotationSmooth = 12f;

	public float runAwayRadius = 100f;

	public float randAmount = -5f;

	public float runAwayOnFireChance = 100f;

	public float RandomRunChance;

	public Vector3 runVecNormalised;

	public Transform noGoZone;

	public float lookAtSmooth = 6f;

	public bool lookAtTarget;

	public bool lookAtVelocity;

	public bool lookForward;

	public bool isDead;

	public FireController fireController;

	public Collider myCollider;

	public float flyUpOnDieAmount = 1000f;

	public Vector3 torqueOnDieAmount = new Vector3(1000f, 800f, 800f);

	public Transform bloodQuad;

	public ParticleSystem BloodBurstHit;

	public bool slowDownWhenNear = true;

	public bool backOffWhenNear;

	public float nearDistance = 10f;

	public float circleSpeed = 8f;

	public float writheAmount = 100f;

	public bool walkRandomlyWhenIdle;

	public float randomWalkIdleAmount = 50f;

	public bool randomEveryFrame = true;

	public float randomWalkNewDirectionRate = 0.1f;

	private float randomWalkRate = 0.1f;

	public Transform targetBlock;

	public bool attackAi;

	[NonSerialized]
	public bool isRunningAway;

	[NonSerialized]
	public Vector3 runVec;

	private Rigidbody myRigidbody;

	private Vector3 randomIdleDirection;

	private bool gravEnabled;

	private bool wasGravDisabled;

	private bool isVisible = true;

	private float lastRandomDirection = -1f;

	private float lastTargetBlock;

	private float targetBlockInterval = 1f;

	private Transform myTransform;

	private bool setUp;

	public GibOnImpact gibCode;

	private Vector3 targetPos;

	private float phi;

	private float amplitude;

	private float startOffset;

	private Transform boodParent;

	private Renderer bloodQuadRenderer;

	private PhysicMaterial myPhysicMaterial;

	private Vector3 randomDirection;

	private Vector3 myVelocity;

	private Quaternion smoothRot;

	private float visObjectStarty;

	private float bobLerpVel;

	private float waiting;

	public Action GettingGibbed;

	public Action<EnemyAISimple> OnDeath;

	[SerializeField]
	[HideInInspector]
	private bool _cartBobing;

	public bool cartBobing
	{
		get
		{
			return _cartBobing;
		}
		set
		{
			_cartBobing = value;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (StatMaster.levelSimulating)
		{
			startOffset = UnityEngine.Random.value * ((float)Math.PI / 2f);
			visibilityTracker.onVisibilityChanged = OnToggleVisibility;
		}
		else if (visibilityTracker == null)
		{
			visibilityTracker = visObject.gameObject.AddComponent<VisibilityTracker>();
		}
	}

	protected override void Start()
	{
		base.Start();
		if (StatMaster.levelSimulating)
		{
			waiting = UnityEngine.Random.Range(0f, 2f);
			if (MeshRenderer == null)
			{
				MeshRenderer = visObject.GetComponentInChildren<MeshRenderer>();
			}
			if (gibCode == null)
			{
				gibCode = GetComponent<GibOnImpact>();
			}
			myRigidbody = Rigidbody;
			myRigidbody.isKinematic = false;
			myTransform = base.transform;
			setUp = true;
			if (injuryController == null)
			{
				injuryController = GetComponent<InjuryController>();
			}
			boodParent = ReferenceMaster.physicsGoalInstance;
			myPhysicMaterial = myCollider.material;
			if (bloodQuad != null)
			{
				bloodQuadRenderer = bloodQuad.GetComponent<Renderer>();
			}
			if (bleedOnJointBreak == null)
			{
				bleedOnJointBreak = GetComponent<BleedOnJointBreak>();
			}
			if (goal != null)
			{
				runAwayRadius = 1000000f;
			}
			runAwaySpeed += UnityEngine.Random.Range((0f - runAwaySpeed) / 10f, runAwaySpeed / 10f);
			randomDirection = UnityEngine.Random.insideUnitSphere;
			randomDirection.y = 0f;
			nearDistance -= nearDistance / UnityEngine.Random.Range(2f, 4f);
			circleSpeed *= RandomPosNeg();
			if (StatMaster.levelSimulating && !isDead)
			{
				CheckTargetBlock();
			}
			RandomRunChance = UnityEngine.Random.Range(0f, 99.9f);
			gravEnabled = !StatMaster.GodTools.GravityDisabled;
			lastTargetBlock = UnityEngine.Random.Range(0f, targetBlockInterval);
			if (StatMaster.levelSimulating && StatMaster.GodTools.GravityDisabled)
			{
				ZeroG();
			}
			visObjectStarty = visObject.localPosition.y;
			myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
			maxHealth = health;
		}
	}

	private void ZeroG()
	{
		wasGravDisabled = true;
		gravEnabled = !StatMaster.GodTools.GravityDisabled;
		myRigidbody.constraints = RigidbodyConstraints.None;
		myRigidbody.AddForce(UnityEngine.Random.insideUnitSphere * 10f + new Vector3(0f, UnityEngine.Random.value * 100f, 0f));
		myRigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 10f);
	}

	private void CheckTargetBlock()
	{
		Machine machine = Machine.Active();
		if (attackAi)
		{
			if (targetBlock == null)
			{
				GetNewTargetAi();
			}
		}
		else
		{
			if (!(machine != null) || !machine.isReady)
			{
				return;
			}
			BlockHealthBar blockHealthBar = null;
			if (targetBlock != null)
			{
				blockHealthBar = targetBlock.GetComponent<BlockHealthBar>();
			}
			if (targetBlock == null || (blockHealthBar != null && blockHealthBar.health <= 0f))
			{
				BlockBehaviour blockBehaviour = null;
				int num = 0;
				do
				{
					blockBehaviour = machine.GetRandomBlock();
					num++;
				}
				while (blockBehaviour is GenericDraggedBlock && num < 10);
				if (blockBehaviour != null)
				{
					targetBlock = blockBehaviour.transform;
				}
			}
		}
	}

	public void GetNewTarget()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (attackAi)
		{
			GetNewTargetAi();
			return;
		}
		Machine machine = Machine.Active();
		if (machine != null)
		{
			targetBlock = machine.GetRandomBlock().transform;
		}
	}

	private void GetNewTargetAi()
	{
		if (StatMaster.levelSimulating)
		{
			AiAttackMeTag[] componentsInChildren = ReferenceMaster.physicsGoalInstance.GetComponentsInChildren<AiAttackMeTag>();
			targetBlock = componentsInChildren[UnityEngine.Random.Range(0, componentsInChildren.Length)].GetComponent<Transform>();
		}
	}

	private void Update()
	{
		if (!StatMaster.levelSimulating || !setUp || isDead || StatMaster.GodTools.GravityDisabled || wasGravDisabled)
		{
			return;
		}
		if (!randomEveryFrame)
		{
			if (waiting > 0f)
			{
				randomIdleDirection = Vector3.zero;
				waiting -= Time.deltaTime;
			}
			else if (lastRandomDirection < 0f)
			{
				RandomDirection();
				randomWalkRate = UnityEngine.Random.Range(randomWalkNewDirectionRate * 0.75f, randomWalkNewDirectionRate * 1.25f);
				lastRandomDirection = 0f;
			}
			else if (lastRandomDirection >= randomWalkRate)
			{
				lastRandomDirection = -1f;
				waiting = UnityEngine.Random.Range(0.5f, 2f);
			}
			else
			{
				lastRandomDirection += Time.deltaTime;
			}
		}
		lastTargetBlock += Time.deltaTime;
		if (lastTargetBlock > targetBlockInterval)
		{
			CheckTargetBlock();
			lastTargetBlock = 0f;
		}
		if (goal != null)
		{
			targetPos = goal.position;
		}
		else if (targetBlock != null)
		{
			targetPos = targetBlock.position;
		}
		else
		{
			targetPos = myTransform.position + myTransform.forward;
		}
		float sqrMagnitude = myVelocity.sqrMagnitude;
		if (canBob && isVisible && sqrMagnitude > 0f)
		{
			Bob(sqrMagnitude);
		}
		if ((!isVisible && sqrMagnitude == 0f) || targetBlock == null)
		{
			return;
		}
		if (lookForward)
		{
			if (isRunningAway)
			{
				LookAtTarget();
			}
		}
		else
		{
			LookAtTarget();
		}
	}

	private void Bob(float currentVelocity)
	{
		phi = (Time.time + startOffset) / bobRate * (float)Math.PI * 2f;
		bobLerpVel = Mathf.Lerp(bobLerpVel, currentVelocity, Time.deltaTime * bobLerpSpeed);
		if (cartBobing)
		{
			amplitude = Mathf.Cos(phi) * 2f;
			if (!float.IsNaN(amplitude) && !float.IsNaN(runAwayBobAmount))
			{
				visObject.localEulerAngles = new Vector3(visObject.localEulerAngles.x, visObject.localEulerAngles.y, 0f + amplitude * runAwayBobAmount * Mathf.Clamp(bobLerpVel * 10f, 0f, 5f));
			}
		}
		else
		{
			amplitude = Mathf.Cos(phi) * 0.5f + 0.5f;
			if (!float.IsNaN(amplitude) && !float.IsNaN(runAwayBobAmount))
			{
				visObject.localPosition = new Vector3(visObject.localPosition.x, visObjectStarty + amplitude * runAwayBobAmount * Mathf.Clamp(bobLerpVel * 10f, 0f, 5f), visObject.localPosition.z);
			}
		}
	}

	private void LookAtTarget()
	{
		if (!fireController.onFire)
		{
			if (lookAtTarget)
			{
				if (!walkRandomlyWhenIdle)
				{
					smoothRot = Quaternion.LookRotation(new Vector3(targetPos.x, visObject.position.y, targetPos.z) - visObject.position);
				}
				else if (myVelocity.sqrMagnitude > 0.001f)
				{
					smoothRot = Quaternion.LookRotation(new Vector3(myVelocity.x, 0f, myVelocity.z), Vector3.up);
				}
			}
			else if (!walkRandomlyWhenIdle)
			{
				smoothRot = Quaternion.LookRotation(visObject.position - new Vector3(targetPos.x, visObject.position.y, targetPos.z));
			}
			else if (myVelocity.sqrMagnitude > 0.001f)
			{
				smoothRot = Quaternion.LookRotation(new Vector3(myVelocity.x, 0f, myVelocity.z), Vector3.up);
			}
		}
		else if (myVelocity.sqrMagnitude > 0.001f)
		{
			smoothRot = Quaternion.LookRotation(new Vector3(myVelocity.x, 0f, myVelocity.z), Vector3.up);
		}
		if (lookAtTarget || lookAtVelocity)
		{
			visObject.rotation = Quaternion.Slerp(visObject.rotation, smoothRot, Time.deltaTime * lookAtSmooth);
		}
	}

	protected void FixedUpdate()
	{
		if (!StatMaster.levelSimulating || !setUp)
		{
			return;
		}
		if (!StatMaster.GodTools.GravityDisabled && !wasGravDisabled)
		{
			myVelocity = myRigidbody.velocity;
			myVelocity.y = 0f;
			if (isDead)
			{
				myRigidbody.AddRelativeTorque(Vector3.up * writheAmount * UnityEngine.Random.value);
			}
			else if (runAway)
			{
				runVec = targetPos - myTransform.position;
				runVec += UnityEngine.Random.insideUnitSphere * randAmount;
				runVec.y = 0f;
				float sqrMagnitude = runVec.sqrMagnitude;
				runVecNormalised = runVec.normalized;
				bool flag = sqrMagnitude < runAwayRadius;
				bool onFire = fireController.onFire;
				isRunningAway = (bool)targetBlock && (flag || onFire);
				if (!onFire || (onFire && runAwayOnFireChance < RandomRunChance))
				{
					if (sqrMagnitude < runAwayRadius && (bool)targetBlock)
					{
						if (sqrMagnitude > nearDistance)
						{
							myRigidbody.AddForce(runVecNormalised * runAwaySpeed - myVelocity);
						}
						else
						{
							CircleStrafe();
							if (backOffWhenNear)
							{
								myRigidbody.AddForce(-runVecNormalised * runAwaySpeed - myVelocity);
							}
						}
					}
					else if (walkRandomlyWhenIdle)
					{
						Vector3 vector = ((!randomEveryFrame) ? randomIdleDirection : new Vector3(UnityEngine.Random.insideUnitSphere.x, 0f, UnityEngine.Random.insideUnitSphere.z));
						myRigidbody.AddForce(vector * randomWalkIdleAmount);
					}
				}
				else if (runAwayOnFireChance > RandomRunChance)
				{
					myRigidbody.AddForce(-runVecNormalised * runAwaySpeed * ((runAwaySpeed >= 0f) ? 1 : (-1)) * 1.25f - myVelocity);
					isRunningAway = true;
				}
			}
		}
		if (gravEnabled == StatMaster.GodTools.GravityDisabled && StatMaster.GodTools.GravityDisabled)
		{
			ZeroG();
		}
	}

	private void RandomDirection()
	{
		float num = 6f;
		Vector3 insideUnitSphere = UnityEngine.Random.insideUnitSphere;
		randomIdleDirection = (base.transform.forward + new Vector3(insideUnitSphere.x * num, 0f, insideUnitSphere.z * num)).normalized / 11f;
	}

	private void CircleStrafe()
	{
		myRigidbody.AddForce(Vector3.Cross(runVecNormalised, Vector3.up) * circleSpeed - myVelocity);
	}

	private int RandomPosNeg()
	{
		return Mathf.RoundToInt((!(UnityEngine.Random.value <= 0.5f)) ? (-1f) : 1f);
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!isSimulating || !SimPhysics)
		{
			return false;
		}
		if ((mask & 4) != 0)
		{
			Die();
			return true;
		}
		return false;
	}

	public void Die()
	{
		BloodQuad();
		if (!isDead)
		{
			isDead = true;
			if (OnDeath != null)
			{
				OnDeath(this);
			}
			if (myRigidbody != null)
			{
				myRigidbody.constraints = RigidbodyConstraints.None;
				myRigidbody.AddRelativeTorque(torqueOnDieAmount * myRigidbody.mass);
				myRigidbody.AddForce(Vector3.up * flyUpOnDieAmount * UnityEngine.Random.Range(1f, 1.5f) * myRigidbody.mass);
				myRigidbody.drag = 0.2f;
			}
			else
			{
				Debug.LogWarning("Rigidbody null while calling EnemyAISimple::Die!");
			}
			if (myPhysicMaterial != null)
			{
				myPhysicMaterial.dynamicFriction = 0.3f;
				myPhysicMaterial.staticFriction = 0.3f;
			}
			else
			{
				Debug.LogWarning("PhysicsMaterial null while calling EnemyAISimple::Die!");
			}
		}
	}

	public void DieNoJump()
	{
		BloodQuad();
		if (!isDead)
		{
			isDead = true;
			if (myRigidbody != null)
			{
				myRigidbody.constraints = RigidbodyConstraints.None;
			}
			else
			{
				Debug.LogWarning("Rigidbody null while calling EnemyAISimple::DieNoJump!");
			}
			if (myPhysicMaterial != null)
			{
				myPhysicMaterial.dynamicFriction = 1f;
				myPhysicMaterial.staticFriction = 1f;
			}
			else
			{
				Debug.LogWarning("PhysicsMaterial null while calling EnemyAISimple::DieNoJump!");
			}
		}
	}

	private void BloodQuad()
	{
		if (OptionsMaster.BesiegeConfig.BloodEnabled && !(bloodQuad == null))
		{
			bloodQuadRenderer.material.color = StatMaster.BloodColor;
			bloodQuadRenderer.enabled = true;
			bloodQuad.parent = boodParent;
			bloodQuad.position = new Vector3(base.transform.position.x, SingleInstanceFindOnly<AddPiece>.Instance.floorHeight + 0.05f, base.transform.position.z);
			bloodQuad.forward = -Vector3.up;
			bloodQuad.localEulerAngles = new Vector3(bloodQuad.localEulerAngles.x, bloodQuad.localEulerAngles.y, UnityEngine.Random.Range(0f, 360f));
		}
	}

	public void TakeDamage(float damage, InjuryType injuryType)
	{
		if (isDead)
		{
			return;
		}
		health -= damage;
		if (health <= 0f)
		{
			if (!object.ReferenceEquals(bleedOnJointBreak, null))
			{
				injuryController.activeType = injuryType;
				bleedOnJointBreak.Killed(true, injuryType);
			}
			else if (gibCode != null)
			{
				if (GettingGibbed != null)
				{
					GettingGibbed();
				}
				gibCode.Gib();
			}
			else
			{
				Die();
			}
		}
		else if (!NetworkBlock.applyingState && OptionsMaster.BesiegeConfig.BloodEnabled && BloodBurstHit != null && !BloodBurstHit.isPlaying)
		{
			BloodBurstHit.GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", StatMaster.BloodColor);
			BloodBurstHit.Play();
		}
	}

	private void OnToggleVisibility(bool toggle)
	{
		isVisible = toggle;
	}
}
