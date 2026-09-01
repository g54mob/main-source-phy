using Landfall.MonoBatch;
using Landfall.TABS;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using TFBGames;
using UnityEngine;

public class DataHandler : BatchedMonobehaviour
{
	public Vector3 lookDirectionIntent;

	public Vector3 currentVelocity;

	public Transform characterForwardObject;

	public DataHandler unitToInheritDirectionFrom;

	public Transform groundedMovementDirectionObject;

	public bool playingStaticAnim;

	public float extraGroundedTime;

	public bool isGrounded;

	public float sinceGrounded;

	public float distanceToGround;

	public float sinceSwitchedStep;

	public float currentLowSwitchCap = 0.1f;

	public float fallTime;

	public float immunityForSeconds;

	public float muscleControl = 1f;

	public float ragdollControl = 1f;

	public float health = 100f;

	public float maxHealth;

	public Vector3 upHillVector;

	public Vector3 groundNormal;

	public Vector3 centerOfMass;

	public Vector3 centerOfMassWithoutY;

	public Vector3 forwardGroundNormal;

	public Vector3 groundPosition;

	public Vector3 groundMapPosition;

	public float legAngle;

	public float torsoAngleMultiplier = 1f;

	public float lifeTime = 1f;

	public AnimationCurve torsoAngleCurve;

	public bool isRight = true;

	public bool takeFallDamage = true;

	public bool canFall = true;

	public float cantFallForSeconds;

	public int backwardsMultiplier = 1;

	public float angleToTarget;

	[HideInInspector]
	public RigidbodyHolder allRigs;

	public Team team;

	public float baseHeight = 1f;

	private bool dead;

	public bool groundedThisFrame;

	public Transform torso;

	public Rigidbody mainRig;

	public Transform head;

	private float largestDistanceThisFrame;

	public Transform legLeft;

	public Transform legRight;

	public Transform footLeft;

	public Transform footRight;

	public Transform leftHand;

	public Transform rightHand;

	public Transform leftArm;

	public Transform rightArm;

	public Rigidbody hip;

	public GeneralInput input;

	public float distanceToTarget;

	public bool inRange;

	public Rigidbody targetMainRig;

	public DataHandler targetData;

	[HideInInspector]
	public bool hasBeenRevived;

	[HideInInspector]
	public HealthHandler healthHandler;

	public WeaponHandler weaponHandler;

	public Unit unit;

	[Tooltip("Enable to avoid jittering for mount units in Project Mars.")]
	public bool setMainRigKinematic;

	private bool m_canSeeTarget;

	private GameStateManager m_gameStateManager;

	public Vector3 lastPos;

	internal float dontMoveFor;

	internal AnimationHandler anim;

	public bool Dead
	{
		get
		{
			return dead;
		}
		set
		{
			if (value && !dead)
			{
				GameModeService service = ServiceLocator.GetService<GameModeService>();
				if (service.CurrentGameMode == null)
				{
					Debug.LogError("Could not find CurrentGameMode!");
				}
				else
				{
					service.CurrentGameMode.OnUnitDied(unit);
				}
			}
			dead = value;
		}
	}

	public bool CanSeeTarget
	{
		get
		{
			return m_canSeeTarget;
		}
		set
		{
			m_canSeeTarget = value;
		}
	}

	internal void DontWalkFor(float time)
	{
		if (time > dontMoveFor)
		{
			dontMoveFor = time;
		}
	}

	private void Awake()
	{
		distanceToTarget = float.PositiveInfinity;
		input = GetComponent<GeneralInput>();
		unit = GetComponentInParent<Unit>();
		healthHandler = GetComponent<HealthHandler>();
		weaponHandler = GetComponent<WeaponHandler>();
		allRigs = GetComponent<RigidbodyHolder>();
		input.AddInputUpdateAction(UpdateInput);
		head = GetComponentInChildren<Head>().transform;
		torso = GetComponentInChildren<Torso>().transform;
		mainRig = torso.GetComponent<Rigidbody>();
		anim = GetComponent<AnimationHandler>();
		hip = GetComponentInChildren<Hip>().GetComponent<Rigidbody>();
		LegLeft componentInChildren = GetComponentInChildren<LegLeft>();
		if ((bool)componentInChildren)
		{
			legLeft = componentInChildren.transform;
		}
		LegRight componentInChildren2 = GetComponentInChildren<LegRight>();
		if ((bool)componentInChildren2)
		{
			legRight = componentInChildren2.transform;
		}
		KneeLeft componentInChildren3 = GetComponentInChildren<KneeLeft>();
		if ((bool)componentInChildren3)
		{
			footLeft = componentInChildren3.transform;
		}
		KneeRight componentInChildren4 = GetComponentInChildren<KneeRight>();
		if ((bool)componentInChildren4)
		{
			footRight = componentInChildren4.transform;
		}
		HandLeft componentInChildren5 = GetComponentInChildren<HandLeft>();
		if ((bool)componentInChildren5)
		{
			leftHand = componentInChildren5.transform;
		}
		HandRight componentInChildren6 = GetComponentInChildren<HandRight>();
		if ((bool)componentInChildren6)
		{
			rightHand = componentInChildren6.transform;
		}
		ArmLeft componentInChildren7 = GetComponentInChildren<ArmLeft>();
		if ((bool)componentInChildren7)
		{
			leftArm = componentInChildren7.transform;
		}
		ArmRight componentInChildren8 = GetComponentInChildren<ArmRight>();
		if ((bool)componentInChildren8)
		{
			rightArm = componentInChildren8.transform;
		}
		if (!characterForwardObject)
		{
			characterForwardObject = base.transform.root.GetComponentInChildren<DirectionObject>().transform;
		}
		groundedMovementDirectionObject = base.transform.root.GetComponentInChildren<GroundedDirectionObject>().transform;
		currentLowSwitchCap = 0.1f;
	}

	protected override void Start()
	{
		base.Start();
		m_gameStateManager = ServiceLocator.GetService<GameStateManager>();
		maxHealth = health;
	}

	private void UpdateInput()
	{
		if (input.inputDirection != Vector3.zero)
		{
			groundedMovementDirectionObject.rotation = Quaternion.LookRotation(characterForwardObject.TransformDirection(input.inputDirection));
			forwardGroundNormal = Vector3.Cross(groundedMovementDirectionObject.right, groundNormal);
			groundedMovementDirectionObject.rotation = Quaternion.LookRotation(forwardGroundNormal);
		}
		else
		{
			groundedMovementDirectionObject.rotation = characterForwardObject.rotation;
		}
		if (!isGrounded)
		{
			groundedMovementDirectionObject.transform.rotation = characterForwardObject.rotation;
		}
		if ((bool)unitToInheritDirectionFrom && unitToInheritDirectionFrom.dead)
		{
			unitToInheritDirectionFrom = null;
			characterForwardObject = base.transform.root.GetComponentInChildren<DirectionObject>().transform;
			input.AllowRotationChange = true;
		}
	}

	public override void BatchedUpdate()
	{
		if (Dead)
		{
			muscleControl = 0f;
			ragdollControl = 0f;
			return;
		}
		if (dontMoveFor > 0f)
		{
			dontMoveFor -= Time.deltaTime;
		}
		if (cantFallForSeconds > 0f)
		{
			cantFallForSeconds -= Time.deltaTime;
		}
		if (fallTime > 0f)
		{
			ragdollControl = 0f;
		}
		else
		{
			ragdollControl = Mathf.Clamp(ragdollControl + Time.deltaTime, 0f, 1f);
		}
		muscleControl = ragdollControl;
		if (playingStaticAnim)
		{
			muscleControl = 0f;
			fallTime = -1f;
		}
		immunityForSeconds -= Time.deltaTime;
		if (isGrounded)
		{
			fallTime -= Time.deltaTime;
		}
		if (m_gameStateManager.GameState == GameState.BattleState)
		{
			lifeTime += Time.deltaTime;
			currentVelocity = Vector3.Lerp(currentVelocity, mainRig.velocity, Time.deltaTime * muscleControl);
		}
		else
		{
			currentVelocity = mainRig.transform.forward * 3f;
		}
		if (input.inputDirection != Vector3.zero || m_gameStateManager.GameState != GameState.BattleState)
		{
			lastPos = mainRig.position;
		}
		else if (lastPos != Vector3.zero)
		{
			Vector3 vector = lastPos - mainRig.position;
			if (vector.sqrMagnitude > 4f)
			{
				lastPos = mainRig.position;
			}
			else
			{
				mainRig.AddForce(FixedTimeStepService.LargeForceCoefficient * 3000f * Time.deltaTime * vector, ForceMode.Acceleration);
			}
		}
	}

	public override void BatchedFixedUpdate()
	{
		if (Dead || !hip)
		{
			return;
		}
		centerOfMass = (torso.position + head.position + hip.position) / 3f;
		centerOfMassWithoutY = centerOfMass;
		centerOfMassWithoutY.y = 0f;
		torsoAngleMultiplier = torsoAngleCurve.Evaluate(Vector3.Angle(torso.transform.up, Vector3.up));
		if ((bool)legLeft && (bool)legRight)
		{
			legAngle = Vector3.Angle(legLeft.up, legRight.up);
		}
		if (!groundedThisFrame)
		{
			if (sinceGrounded >= extraGroundedTime)
			{
				isGrounded = false;
			}
			sinceGrounded += Time.fixedDeltaTime;
		}
		largestDistanceThisFrame = 0f;
		groundedThisFrame = false;
	}

	public void TouchGround(Vector3 collisionPoint, Vector3 normal, Rigidbody rig = null)
	{
		if (sinceGrounded > 1f && takeFallDamage && (bool)mainRig && mainRig.velocity.y < -5f && (bool)healthHandler)
		{
			healthHandler.TakeDamage(sinceGrounded * 100f, Vector3.zero, null);
		}
		sinceGrounded = 0f;
		isGrounded = true;
		groundedThisFrame = true;
		float num = Mathf.Abs(head.position.y - collisionPoint.y);
		if (num > largestDistanceThisFrame)
		{
			distanceToGround = num;
			largestDistanceThisFrame = num;
			upHillVector = Vector3.Cross(Vector3.Cross(normal, Vector3.up), normal);
			groundNormal = normal;
			groundPosition = collisionPoint;
			if (!rig)
			{
				groundMapPosition = collisionPoint;
			}
			forwardGroundNormal = Vector3.Cross(groundedMovementDirectionObject.right, groundNormal);
			groundedMovementDirectionObject.rotation = Quaternion.LookRotation(forwardGroundNormal);
		}
	}
}
