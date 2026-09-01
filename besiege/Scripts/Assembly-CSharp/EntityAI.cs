using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityAI : MonoBehaviour, ITarget
{
	[Serializable]
	public class FactionSystem
	{
		[Tooltip("This Units Faction")]
		public FactionsController.FactionEnum faction = FactionsController.FactionEnum.None;

		[HideInInspector]
		public string factionName;

		[HideInInspector]
		public MPTeam team;

		public FactionsController.FactionEnum primaryTargetFaction = FactionsController.FactionEnum.None;

		[HideInInspector]
		public string primaryTargetName;

		public FactionsController.AttackOnlyEnum AttackOnlyTypeOf = FactionsController.AttackOnlyEnum.Both;

		public FactionsController.DiscriminantEnum Discrimination = FactionsController.DiscriminantEnum.Indiscriminant;

		public void Setup(EntityAI inst)
		{
			if (!string.IsNullOrEmpty(factionName))
			{
				inst.faction.Name = factionName;
			}
			else
			{
				inst.faction.Name = faction.ToString();
			}
			inst.faction.AttackOnlyTypeOf = AttackOnlyTypeOf;
			inst.faction.Discrimination = Discrimination;
			if (faction == FactionsController.FactionEnum.None)
			{
				Debug.LogError(inst.gameObject.name + "Cant be of Faction None. Please choose or create another faction");
			}
			if (!string.IsNullOrEmpty(primaryTargetName))
			{
				inst.faction.Preference = primaryTargetName;
			}
			else
			{
				inst.faction.Preference = primaryTargetFaction.ToString();
			}
			if (inst.faction.Preference == inst.faction.Name)
			{
				inst.faction.Preference = null;
			}
		}
	}

	[Serializable]
	public enum FocusOn
	{
		Target = 0,
		Velocity = 1,
		ReverseVelocity = 2,
		TargetOpposite = 3,
		DirectControl = 4,
		Nothing = 5
	}

	[Serializable]
	public enum Action
	{
		ApproachTarget = 0,
		PursueTarget = 1,
		FactionCharge = 2,
		Flee = 3,
		Strafe = 4,
		WalkAround = 5,
		Stationary = 6,
		None = 7
	}

	public enum EntityState
	{
		Idle = 0,
		Pursuing = 1,
		FactionCharge = 2,
		Controlled = 3,
		Fleeing = 4,
		TacticalRetreat = 5,
		Strafing = 6,
		Close = 7,
		Stationary = 8,
		Fallen = 9,
		Grabbed = 10,
		Ungrounded = 11,
		Dead = 12,
		CantMove = 13,
		Attacking = 14,
		Suffocating = 15
	}

	[Serializable]
	public class Disposition
	{
		[Tooltip("Can this unit attack?")]
		public bool canAttack;

		public EntityState myState;

		public bool AutomaticTargetSystem = true;

		public bool useStateMachine = true;

		public bool OverwriteFleeOnFire;

		public bool AvoidFire = true;

		public float FireAvoidancetime = 0.75f;

		public bool SmartTargeting;

		public bool useBehaviours = true;

		public List<Behaviour> behaviours = new List<Behaviour>();

		[HideInInspector]
		public Behaviour[] behavioursArray;

		[HideInInspector]
		public Behaviour currentBehaviour;
	}

	[Serializable]
	public class Targeting
	{
		public EntityAI owner;

		public EntityAI AI;

		public BlockHealthBar BlockHealth;

		public BlockBehaviour Block;

		public Transform trans;

		public Rigidbody Rigidbody;

		public bool gotTarget;

		public bool isAI;

		public bool isBlock;

		public bool isArmored;

		public Vector3 deflectTargetRotation;

		public Targeting()
		{
		}

		public Targeting(EntityAI ai)
		{
			SetOwner(ai);
		}

		public Vector3 GetTargetPosition()
		{
			return (!isBlock) ? trans.position : Block.GetCenter();
		}

		public void NewTargetBlock(Transform target)
		{
			NewTargetBlock(target, null);
		}

		public void NewTargetBlock(Transform target, Rigidbody rb)
		{
			Null();
			if (target == null)
			{
				return;
			}
			BasicInfo component = target.GetComponent<BasicInfo>();
			if (object.ReferenceEquals(component, null) && rb != null)
			{
				component = rb.GetComponent<BasicInfo>();
			}
			if (object.ReferenceEquals(component, null))
			{
				Rigidbody = target.GetComponent<Rigidbody>();
				trans = ((!(Rigidbody != null)) ? target : Rigidbody.transform);
				return;
			}
			Rigidbody = ((!component.noRigidbody) ? component.Rigidbody : null);
			trans = ((!component.noRigidbody) ? Rigidbody.transform : target);
			gotTarget = true;
			if (component.noRigidbody)
			{
				return;
			}
			Block = component as BlockBehaviour;
			if (!object.ReferenceEquals(Block, null))
			{
				isBlock = true;
				if (Block.Prefab.hasHealthBar)
				{
					BlockHealth = Block.BlockHealth;
				}
				isArmored = Block.IsArmor;
			}
			else if (component.hasAiScript)
			{
				AI = component.aiEntity;
				if (!object.ReferenceEquals(AI, null))
				{
					isAI = true;
				}
			}
			else if (owner.disposition.AutomaticTargetSystem)
			{
				Null();
			}
		}

		public void Null()
		{
			AI = null;
			BlockHealth = null;
			Block = null;
			trans = null;
			Rigidbody = null;
			isArmored = false;
			gotTarget = false;
			isBlock = false;
			isAI = false;
		}

		public void SetOwner(EntityAI ai)
		{
			owner = ai;
		}
	}

	[Serializable]
	public class References
	{
		public Transform VisObject;

		public Collider Collider;

		public FireController fireController;

		public AttackScript attackScript;

		public KillingHandler killingHandler;

		[HideInInspector]
		public Machine ActiveMachine;

		[HideInInspector]
		public Rigidbody Rigidbody;

		[HideInInspector]
		public Transform Transform;

		[Obsolete("Deprecated. Use ReferenceMaster.physicsGoalInstance instead")]
		[HideInInspector]
		public Transform PhysicsGoal;

		[HideInInspector]
		public Vector3 worldUp;

		[HideInInspector]
		public Vector3 TransformUP;

		[HideInInspector]
		public Vector3 visRight;

		[HideInInspector]
		public Quaternion visRotation;

		[HideInInspector]
		public TimeSlider timeSlider;

		[HideInInspector]
		public BasicInfo basicInfo;

		[HideInInspector]
		public AIGenericEntity aiGenEntity;
	}

	[Serializable]
	public class Looking
	{
		public FocusOn Focus;

		public float Smoothing = 6f;

		public bool rotateRigidbody;

		public bool spinOnly;

		public bool strafeSideways = true;

		[HideInInspector]
		public Quaternion TargetRotation;
	}

	[Serializable]
	public class Movement
	{
		public bool Able = true;

		[HideInInspector]
		public bool moving;

		public bool keepInterpolation;

		public float Speed = 7f;

		public float VarianceAmount = -5f;

		public float randomWalkRate = 1f;

		public float randomWalkPeriod = 0.4f;

		public float distanceCloseState = 5f;

		public bool objectAvoidance = true;

		public float idleDampenTimer = 2f;

		[HideInInspector]
		public bool dampened;

		[HideInInspector]
		public float avoidanceOffset;

		[HideInInspector]
		public Quaternion avoidanceOffsetAngle;

		[HideInInspector]
		public float upRightAngle;

		[HideInInspector]
		public Vector3 targetVelocity;

		[HideInInspector]
		public float velocitySqr;

		[HideInInspector]
		public float origSpeed = 7f;

		[HideInInspector]
		public Vector3 TargetPos;

		[HideInInspector]
		public Vector3 DifferenceToTarget;

		[HideInInspector]
		public float DifferenceToTargetSqr;

		[HideInInspector]
		public Vector3 Direction;

		[HideInInspector]
		public Vector3 CurrentVelocity;

		[HideInInspector]
		public Vector3 PreviousVelocity;

		[HideInInspector]
		public Vector3 PreviousPosition;

		[HideInInspector]
		public bool returnToIdle;

		[HideInInspector]
		public Quaternion identityQuat = Quaternion.identity;

		[HideInInspector]
		public Vector3 factionchargeDir;

		[HideInInspector]
		public bool skipToCharge;

		[HideInInspector]
		public Vector3 randomWalkDir;

		[HideInInspector]
		public float randomWalkTimer;

		public bool canJump = true;

		public bool inJump;

		public float jumpHeight = 1.5f;

		public float jumpForcePerUnit = 5f;

		public float counterGravity = 4f;

		public float jumpDirectionMultiplier = 5f;

		[HideInInspector]
		public bool hitHighObject;

		[HideInInspector]
		public bool jumpedThisFrame;

		public bool walkUpSlopes = true;

		public float MaxAscent = 35f;

		public float slopeClimbingSpeed = 0.22f;

		public float straightSurfaceAngle = 0.9f;

		[HideInInspector]
		public Transform StraightSurface;

		[HideInInspector]
		public bool lineUp;

		[HideInInspector]
		public Vector3 lineUpPos;

		[HideInInspector]
		public bool AntiStuckRunning;

		public float VelocitySqr
		{
			get
			{
				velocitySqr = CurrentVelocity.sqrMagnitude;
				return velocitySqr;
			}
		}

		public void Initialize()
		{
			Speed += UnityEngine.Random.Range((0f - Speed) / 10f, Speed / 10f);
			origSpeed = Speed;
			MaxAscent = 1f - MaxAscent * 0.0111f;
			randomWalkRate = UnityEngine.Random.Range(0f, randomWalkRate);
			randomWalkTimer = UnityEngine.Random.Range(0f, randomWalkRate + randomWalkPeriod);
			avoidanceOffsetAngle = Quaternion.identity;
		}
	}

	[Serializable]
	public class Bob
	{
		public bool Able = true;

		[HideInInspector]
		public bool startValue;

		public float Amount = 0.2f;

		public float Rate = 0.23f;

		[HideInInspector]
		public float bobRateMultiphi;

		[HideInInspector]
		public float phi;

		[HideInInspector]
		public float amplitude;

		[HideInInspector]
		public float deSync;

		[HideInInspector]
		public float startY;

		[HideInInspector]
		public float diffToY;

		[HideInInspector]
		public float visPosX;

		[HideInInspector]
		public float visPosZ;

		[HideInInspector]
		public float smoothVel;

		[HideInInspector]
		public float lerpSpeed = 10f;

		[HideInInspector]
		public Vector3 previousBobPos;

		[HideInInspector]
		public float BobVel;

		[HideInInspector]
		public bool pause;

		public Bob()
		{
			startValue = Able;
		}
	}

	[Serializable]
	public class Retreating
	{
		public bool useMoral = true;

		public float MoralLimit = 100f;

		public float currentMoral = 100f;

		public float MentalFortutude;

		public float minDistDisappear = 3000f;

		public Fading fading = new Fading();

		public bool moralWasCalculated;

		[HideInInspector]
		public bool coward;

		[HideInInspector]
		public bool exeededMoralValue;

		[HideInInspector]
		public float MaxHealth;

		public float HealthAmountInfluence = 0.5f;

		public float LossOverTimeInfluence = 0.5f;

		public float InfanteryCountInfluence = 0.5f;

		public void Initialize()
		{
			MentalFortutude = UnityEngine.Random.Range(0, 20);
			HealthAmountInfluence *= MoralLimit;
			LossOverTimeInfluence *= MoralLimit;
			InfanteryCountInfluence *= MoralLimit;
		}
	}

	[Serializable]
	public class Fading
	{
		[HideInInspector]
		public float fadeProgress;

		[HideInInspector]
		public float currentFadeTime;

		[Tooltip("Time it takes for this Unit to fade away")]
		public float fadeTime = 2f;

		[Tooltip("Alpha Color to fade to")]
		public Color alphaColor = new Color(1f, 1f, 1f, 0f);

		[Tooltip("Renderes to be affected by Fading")]
		public Renderer[] renderers;

		[Tooltip("Shader that allows invisibility")]
		public Shader invisShader;

		private Color[] startColours;

		private MaterialPropertyBlock props;

		public void Fade(float progress)
		{
			if (renderers.Length == 0)
			{
				return;
			}
			for (int i = 0; i < renderers.Length; i++)
			{
				if (renderers[i] == null)
				{
					continue;
				}
				for (int j = 0; j < renderers[i].materials.Length; j++)
				{
					Material material = renderers[i].materials[j];
					if (!(material == null) && material.renderQueue != 3000)
					{
						material.shader = invisShader;
						material.renderQueue = 3000;
						material.SetTexture("_ShadowMask", ReferenceMaster.Instance.aiShaddowDissolve);
					}
				}
				props.SetColor("_Color", Color.Lerp(startColours[i], alphaColor, progress));
				renderers[i].SetPropertyBlock(props);
			}
		}

		public void Initialize()
		{
			props = new MaterialPropertyBlock();
			startColours = new Color[renderers.Length];
			for (int i = 0; i < startColours.Length; i++)
			{
				Material material = renderers[i].material;
				startColours[i] = material.color;
			}
		}
	}

	[Serializable]
	public class Behaviour
	{
		[Serializable]
		public class Parameters
		{
			public float Speed;

			public int RandomizeSign()
			{
				if (UnityEngine.Random.value <= 0.5f)
				{
					return 1;
				}
				return -1;
			}
		}

		public float Radius;

		[HideInInspector]
		public float RadiusSqr;

		public Action Action = Action.None;

		public Parameters parameters = new Parameters();

		[HideInInspector]
		public int id = -1;

		public bool attackState;

		public Behaviour()
		{
		}

		public Behaviour(float r, Action a, float s, bool attackS)
		{
			Radius = r;
			Action = a;
			parameters.Speed = s;
			attackState = attackS;
		}

		public void Initialize(int i)
		{
			if (Action == Action.Strafe)
			{
				parameters.Speed *= parameters.RandomizeSign();
			}
			id = i;
			RadiusSqr = Radius * Radius;
		}
	}

	[Serializable]
	public class SelfRighting
	{
		public delegate void StopGrabOnGrabber();

		public ParticleSystem[] ConfusedParticles;

		public bool enabled = true;

		public bool Fallen;

		public bool AllowedToFall = true;

		[HideInInspector]
		public bool Grabbed;

		public bool CanBreakGrab;

		public StopGrabOnGrabber StopBeingGrabbedBy;

		public float timeToBreakGrab = 4f;

		public float forceUsedWhileGrabbed = 40f;

		public float Torque = 30f;

		public int FallenMaxCount = 15;

		public float FallImpactThreshold = 7f;

		public float particleVelocityThreshold = 3000f;

		public float angularDrag = 10f;

		public float ResetDrag = 10f;

		public float SleepTime = 1.5f;

		[HideInInspector]
		public float Timer;

		[HideInInspector]
		public float RandomWait;

		[HideInInspector]
		public int FallenCount;

		[HideInInspector]
		public Quaternion StartRotation;

		public Vector3 StartRotationEuler;

		public bool LockedRotation = true;

		public bool useMeshBounds;

		[HideInInspector]
		public float selfRightLimit = 1f;
	}

	[HideInInspector]
	private class CollisionStruct
	{
		[HideInInspector]
		public Vector3 normal;

		[HideInInspector]
		public float angle;

		[HideInInspector]
		public Vector3 normalVector;

		[HideInInspector]
		public float direction;

		[HideInInspector]
		public float normalVectorLength;

		[HideInInspector]
		public Collider collider;

		[HideInInspector]
		public float height;
	}

	[Serializable]
	public class OnDeath
	{
		public float MaxLeapAmount = 1500f;

		[HideInInspector]
		public float LeapAmount = 1000f;

		public Vector3 LeapTorque = new Vector3(1000f, 800f, 800f);

		public Transform bloodQuad;

		public float floorYpos = 0.056f;

		public float extraDensity = 1f;

		public float extreAngularDrag;

		public GameObject[] objectsToDisableOnDeath;
	}

	protected delegate void EnterExit();

	public string nickname = string.Empty;

	public string fullName = string.Empty;

	public string deathDiscription = string.Empty;

	public bool DebugAI;

	public bool parentToPhysicsGoal;

	[NonSerialized]
	public Vector3 simStartPosition;

	public FactionSystem factionSystem = new FactionSystem();

	[Tooltip("Starting Health of this unit")]
	public float health = 600f;

	[Tooltip("Victory Value")]
	public int victoryValue = 1;

	[Tooltip("How submerged do we need to be before we drown")]
	public float suffocatingLimit = 0.3f;

	[Tooltip("LineUp Position. Lower = Front")]
	public float LineUpLayer = 1f;

	[Tooltip("Type of AI that defines things such as death triggers")]
	public AIType subAIType;

	[HideInInspector]
	public bool firstGroundTouch;

	[HideInInspector]
	public Faction faction;

	[HideInInspector]
	public float aiBaseHight;

	[HideInInspector]
	public float aiBaseWidth;

	[HideInInspector]
	public Vector3 aiBaseCenterOffset;

	[HideInInspector]
	public float chanceToCatchOnFire = 0.01f;

	protected bool wasGravDisabled;

	protected bool wasSimulating;

	protected Vector3 zero = Vector3.zero;

	protected bool UTBisRunning;

	[HideInInspector]
	public List<EntityAI> TargetedBy = new List<EntityAI>();

	protected int OldTargetedByCount;

	protected EntityAI OldTargetedBy;

	[HideInInspector]
	public EntityState aiControllerState;

	public bool ForwardAxisX;

	public bool isDead;

	public bool freezRigidbody = true;

	public bool grounded = true;

	public bool useJointAsGround;

	public Joint groundJoint;

	public bool useKinematicAsGround;

	public LayerMask groundLayerMask;

	public bool ignoreBreakCollision;

	protected List<Transform> listOfBlockCollisions = new List<Transform>();

	protected List<Transform> pinchedBetween = new List<Transform>();

	protected bool velocityCleared;

	protected float gcVelocityThreshold = 17.5f;

	protected bool waitForFirstRotation = true;

	protected bool wasInWater;

	protected EnterExit onEnterWater;

	protected EnterExit onExitWater;

	[HideInInspector]
	public LevelEntity levelEntity;

	public Disposition disposition = new Disposition();

	public Targeting TargetBlock = new Targeting();

	public References my = new References();

	public Looking looking = new Looking();

	public Movement movement = new Movement();

	public Bob bob = new Bob();

	public Retreating retreating = new Retreating();

	private float yPos;

	private bool prevUseBehaviour;

	private bool prevAutomaticTargetSystem;

	[HideInInspector]
	public float behavioursMaxDistance;

	public SelfRighting selfRighting = new SelfRighting();

	public float dotForCollisionTargetChange = -1f;

	private bool bol;

	private List<CollisionStruct> collisionList = new List<CollisionStruct>();

	public OnDeath onDeath = new OnDeath();

	public bool AllowedToModifyConstraints
	{
		get
		{
			return !IsKinematic && !HasJoint;
		}
	}

	public bool IsKinematic
	{
		get
		{
			return useKinematicAsGround && my.Rigidbody.isKinematic;
		}
	}

	public bool HasJoint
	{
		get
		{
			return useJointAsGround && groundJoint != null;
		}
	}

	public bool IsJointLocked
	{
		get
		{
			return HasJoint && !(groundJoint is HingeJoint);
		}
	}

	public virtual Vector3 HeadPosition
	{
		get
		{
			return my.Transform.position;
		}
	}

	public System.Action OnRemoved { get; set; }

	public float BehavioursMaxDistance
	{
		get
		{
			if (behavioursMaxDistance != 0f)
			{
				return behavioursMaxDistance;
			}
			behavioursMaxDistance = GetBehaviourMaxRad();
			return behavioursMaxDistance;
		}
	}

	public virtual Vector3 Center()
	{
		return my.Transform.position + my.Transform.up;
	}

	public void Remove()
	{
		if (OnRemoved != null)
		{
			OnRemoved();
		}
	}

	protected void Awake()
	{
		if (StatMaster.isMP)
		{
			levelEntity = GetComponent<LevelEntity>();
		}
		if (StatMaster.levelSimulating)
		{
			factionSystem.Setup(this);
		}
		TargetBlock.SetOwner(this);
		my.basicInfo = GetComponent<BasicInfo>();
		if (object.ReferenceEquals(my.basicInfo, null))
		{
			Debug.LogError(base.name + " AI is missing BasicInfo");
			base.enabled = false;
			return;
		}
		my.basicInfo.hasAiScript = true;
		my.basicInfo.aiEntity = this;
		my.timeSlider = TimeSlider.Instance;
		my.worldUp = Vector3.up;
		my.Transform = base.transform;
	}

	public virtual void Start()
	{
		if (!StatMaster.levelSimulating)
		{
			if (base.gameObject.CompareTag("ObjectiveObj"))
			{
				WinCondition.Instance.objectiveObjectCount += victoryValue - 1;
			}
			return;
		}
		if (ignoreBreakCollision)
		{
			ReferenceMaster.IgnoreBreakCollisions.Add(base.gameObject);
		}
		if (parentToPhysicsGoal)
		{
			simStartPosition = base.transform.position;
			base.transform.parent = ReferenceMaster.physicsGoalInstance;
		}
		onEnterWater = (EnterExit)Delegate.Combine(onEnterWater, new EnterExit(EnterWater));
		onExitWater = (EnterExit)Delegate.Combine(onExitWater, new EnterExit(ExitWater));
		movement.jumpHeight *= base.transform.localScale.y;
		bob.deSync = UnityEngine.Random.value * ((float)Math.PI / 2f);
		isDead = false;
		my.ActiveMachine = Machine.Active();
		if (!StatMaster.isMP || StatMaster.isHosting || StatMaster.isLocalSim)
		{
			if (FactionsController.setupComplete)
			{
				FactionsController.AddNewAIToFaction(this);
				faction.suddenLoss -= 1f;
				GetMoral();
			}
			my.Rigidbody = my.basicInfo.Rigidbody;
			if (!StatMaster.isMP)
			{
				my.Rigidbody.isKinematic = useKinematicAsGround;
				my.basicInfo.isKinematic = useKinematicAsGround;
				if (useKinematicAsGround)
				{
					my.Rigidbody.interpolation = RigidbodyInterpolation.None;
				}
			}
			my.Rigidbody.constraints = RigidbodyConstraints.None;
			movement.Initialize();
			retreating.Initialize();
			if (StatMaster.levelSimulating && StatMaster.GodTools.GravityDisabled)
			{
				ActivateZeroG();
			}
			else if (StatMaster.levelSimulating && wasGravDisabled && !StatMaster.GodTools.GravityDisabled)
			{
				DeActivateZeroG();
			}
		}
		if (object.ReferenceEquals(my.killingHandler, null))
		{
			my.killingHandler = GetComponent<KillingHandler>();
		}
		if (selfRighting.useMeshBounds)
		{
			CalculateHeight(my.VisObject.GetComponentInChildren<SkinnedMeshRenderer>());
		}
		else
		{
			CalculateHeight(my.Collider);
		}
		bob.startY = my.VisObject.localPosition.y;
		bob.visPosX = my.VisObject.localPosition.x;
		bob.visPosZ = my.VisObject.localPosition.z;
		bob.bobRateMultiphi = (float)Math.PI * 2f;
		bob.previousBobPos = my.Transform.position;
		if (!my.basicInfo.noRigidbody)
		{
			my.Rigidbody.constraints = RigidbodyConstraints.None;
		}
		if (freezRigidbody && !IsKinematic && !IsJointLocked)
		{
			if (!looking.rotateRigidbody)
			{
				if (!my.basicInfo.noRigidbody)
				{
					my.VisObject.rotation *= my.Rigidbody.rotation;
					my.Transform.rotation = movement.identityQuat;
					my.Rigidbody.constraints = (RigidbodyConstraints)80;
				}
			}
			else
			{
				my.Transform.rotation *= my.VisObject.localRotation;
				my.VisObject.localRotation = movement.identityQuat;
				if (!my.basicInfo.noRigidbody)
				{
					my.Rigidbody.constraints = (RigidbodyConstraints)80;
				}
			}
		}
		selfRighting.StartRotation = ((!looking.rotateRigidbody) ? my.VisObject.rotation : my.Transform.rotation);
		selfRighting.StartRotationEuler = selfRighting.StartRotation.eulerAngles;
		looking.TargetRotation = selfRighting.StartRotation;
		selfRighting.LockedRotation = selfRighting.enabled;
		if (!my.basicInfo.noRigidbody)
		{
			selfRighting.ResetDrag = my.Rigidbody.angularDrag;
		}
		retreating.fading.Initialize();
		retreating.MaxHealth = health;
		if (faction.MaxInfantry < 4f)
		{
			retreating.useMoral = false;
		}
		disposition.behavioursArray = disposition.behaviours.ToArray();
		for (int i = 0; i < disposition.behavioursArray.Length; i++)
		{
			disposition.behavioursArray[i].Initialize(i);
		}
		if (!my.attackScript || !my.attackScript.enabled)
		{
			disposition.canAttack = false;
		}
		if (!my.basicInfo.noRigidbody)
		{
			my.Rigidbody.centerOfMass = new Vector3(0f, aiBaseCenterOffset.y, 0f);
		}
		SetFocus();
		grounded = (useKinematicAsGround ? my.Rigidbody.isKinematic : ((!useJointAsGround) ? GroundedCheck() : ((bool)groundJoint && groundJoint.connectedBody != null)));
		if (!grounded)
		{
			FallOver(true);
		}
	}

	protected void OnSimulateStart()
	{
		StartCoroutines();
	}

	public virtual void Update()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (StatMaster.isMP && !my.aiGenEntity.PhysicsEnabled)
		{
			Strip();
			my.killingHandler.enabled = false;
			base.enabled = false;
			return;
		}
		if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			if (!isDead && !StatMaster.GodTools.GravityDisabled && bob.Able && !bob.pause)
			{
				AnimateBob();
			}
			return;
		}
		if (StatMaster.levelSimulating && !wasGravDisabled && StatMaster.GodTools.GravityDisabled)
		{
			ActivateZeroG();
		}
		else if (StatMaster.levelSimulating && wasGravDisabled && !StatMaster.GodTools.GravityDisabled)
		{
			DeActivateZeroG();
		}
		if (isDead)
		{
			if (UTBisRunning)
			{
				StopCoroutines();
			}
			if (TargetBlock.gotTarget)
			{
				ClearTargetsTargetedBy();
				TargetBlock.Null();
			}
			if (!movement.keepInterpolation && my.Rigidbody.interpolation == RigidbodyInterpolation.None)
			{
				my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
			return;
		}
		if (my.basicInfo.InWater && !wasInWater)
		{
			wasInWater = true;
			onEnterWater();
		}
		else if (!my.basicInfo.InWater && wasInWater)
		{
			wasInWater = false;
			onExitWater();
		}
		if (grounded && my.basicInfo._inWater && Mathf.Abs(yPos - base.transform.position.y) > 0.5f)
		{
			grounded = false;
		}
		if (!movement.keepInterpolation)
		{
			if ((my.timeSlider.delegateTimeScale < 0.3f && my.Rigidbody.interpolation == RigidbodyInterpolation.None) || disposition.currentBehaviour.parameters.Speed > 15f)
			{
				my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			}
			else if (!selfRighting.Grabbed && my.timeSlider.delegateTimeScale > 0.3f && my.Rigidbody.interpolation == RigidbodyInterpolation.Interpolate && disposition.currentBehaviour.parameters.Speed <= 15f)
			{
				my.Rigidbody.interpolation = RigidbodyInterpolation.None;
			}
		}
		if (StatMaster.GodTools.GravityDisabled || wasGravDisabled)
		{
			return;
		}
		if (TargetBlock.gotTarget)
		{
			Transform trans = TargetBlock.trans;
			if ((bool)trans && (!TargetBlock.isBlock || !TargetBlock.Block.IsDestroyed))
			{
				movement.TargetPos = TargetBlock.GetTargetPosition();
			}
			else
			{
				TargetBlock.Null();
			}
		}
		my.visRotation = ((!looking.rotateRigidbody) ? my.VisObject.rotation : my.Rigidbody.rotation);
		my.visRight = my.visRotation * Vector3.right;
		movement.PreviousPosition = HeadPosition;
		movement.DifferenceToTarget = movement.TargetPos - HeadPosition;
		movement.DifferenceToTargetSqr = new Vector2(movement.DifferenceToTarget.x, movement.DifferenceToTarget.z).sqrMagnitude - aiBaseWidth;
		movement.DifferenceToTarget += UnityEngine.Random.insideUnitSphere * movement.VarianceAmount;
		if (movement.avoidanceOffset != 0f)
		{
			movement.avoidanceOffsetAngle = Quaternion.AngleAxis(movement.avoidanceOffset, my.worldUp);
		}
		else if (movement.avoidanceOffsetAngle != movement.identityQuat)
		{
			movement.avoidanceOffsetAngle = movement.identityQuat;
		}
		movement.upRightAngle = Vector3.Dot(my.TransformUP, my.worldUp);
		movement.Direction = movement.avoidanceOffsetAngle * movement.DifferenceToTarget;
		if (TargetBlock.deflectTargetRotation != Vector3.zero)
		{
			movement.Direction = Quaternion.Euler(TargetBlock.deflectTargetRotation) * movement.Direction;
		}
		movement.Direction.Normalize();
		movement.hitHighObject = false;
		if (disposition.myState == EntityState.FactionCharge)
		{
			if (!movement.Able || !TargetBlock.gotTarget)
			{
				return;
			}
			Vector3 vector = (TargetBlock.isAI ? (TargetBlock.AI.faction.Center - faction.Center) : ((!TargetBlock.isBlock) ? (movement.TargetPos - faction.Center) : (FactionsController.GetMiddleOfClosestMachine(this) - faction.Center)));
			vector.Normalize();
			movement.factionchargeDir = movement.avoidanceOffsetAngle * vector;
			float num = Vector3.Dot(movement.factionchargeDir, movement.Direction);
			if (num > 0.75f)
			{
				movement.skipToCharge = false;
			}
			else
			{
				movement.skipToCharge = true;
			}
		}
		if (disposition.myState != EntityState.Strafing)
		{
			movement.dampened = false;
		}
		if (disposition.myState != EntityState.Idle && movement.returnToIdle)
		{
			movement.idleDampenTimer = 2f;
			movement.returnToIdle = false;
		}
		Animate();
		if (!movement.AntiStuckRunning && !movement.inJump && movement.avoidanceOffset != 0f && (disposition.myState == EntityState.Pursuing || disposition.myState == EntityState.Strafing || disposition.myState == EntityState.TacticalRetreat))
		{
			StartCoroutine(AntiStuck());
		}
		if (disposition.myState != EntityState.Grabbed)
		{
			CostumOnCollisionStay();
		}
		if (selfRighting.LockedRotation && movement.upRightAngle < 0.98f && AllowedToModifyConstraints)
		{
			if (looking.rotateRigidbody)
			{
				selfRighting.StartRotation.eulerAngles = new Vector3(selfRighting.StartRotationEuler.x, my.Transform.rotation.eulerAngles.y, selfRighting.StartRotationEuler.z);
			}
			my.Rigidbody.constraints = RigidbodyConstraints.None;
			my.Transform.rotation = ((!looking.rotateRigidbody) ? movement.identityQuat : selfRighting.StartRotation);
			if (freezRigidbody)
			{
				my.Rigidbody.constraints = (RigidbodyConstraints)80;
			}
		}
		if (TargetBlock.gotTarget && selfRighting.LockedRotation && looking.Focus != FocusOn.Nothing && looking.Focus != FocusOn.DirectControl && !looking.rotateRigidbody)
		{
			my.VisObject.rotation = Quaternion.Slerp(my.visRotation, looking.TargetRotation, Time.deltaTime * looking.Smoothing);
			if (looking.spinOnly)
			{
				my.VisObject.localEulerAngles = new Vector3(0f, my.VisObject.localEulerAngles.y, 0f);
			}
		}
		my.TransformUP = my.Rigidbody.rotation * my.worldUp;
		if (disposition.canAttack && !selfRighting.Fallen && (disposition.currentBehaviour.attackState || disposition.myState == EntityState.Close) && TargetBlock.gotTarget)
		{
			my.attackScript.Attack(TargetBlock, movement.DifferenceToTargetSqr);
		}
		if (disposition.currentBehaviour.parameters.Speed == 0f)
		{
			disposition.currentBehaviour.parameters.Speed = movement.Speed;
		}
		if (!selfRighting.Grabbed && !grounded && !movement.inJump && !selfRighting.Fallen && selfRighting.AllowedToFall)
		{
			FallOver(true);
		}
		if (retreating.useMoral && retreating.moralWasCalculated && retreating.currentMoral < retreating.MentalFortutude && !retreating.exeededMoralValue)
		{
			retreating.exeededMoralValue = true;
		}
		if (!wasSimulating)
		{
			wasSimulating = StatMaster.levelSimulating;
			OnSimulateStart();
		}
	}

	protected virtual void FixedUpdate()
	{
		if (!StatMaster.levelSimulating || StatMaster.GodTools.GravityDisabled || wasGravDisabled || isDead || (waitForFirstRotation && !selfRighting.Fallen) || (StatMaster.isClient && !StatMaster.isLocalSim))
		{
			return;
		}
		movement.PreviousVelocity = movement.CurrentVelocity;
		movement.CurrentVelocity = my.Rigidbody.velocity;
		grounded = GroundedCheck();
		movement.moving = grounded;
		switch (disposition.myState)
		{
		case EntityState.Idle:
			movement.moving = false;
			Idle();
			break;
		case EntityState.Controlled:
			movement.moving = false;
			break;
		case EntityState.Pursuing:
			if (disposition.currentBehaviour.Action == Action.ApproachTarget)
			{
				Approach();
			}
			else
			{
				Charge();
			}
			break;
		case EntityState.FactionCharge:
			FactionCharge();
			break;
		case EntityState.Strafing:
			CircleStrafe();
			break;
		case EntityState.Stationary:
			movement.moving = false;
			Stationary();
			break;
		case EntityState.Close:
			if (!movement.dampened && movement.Able)
			{
				movement.dampened = true;
				my.Rigidbody.AddForce(movement.CurrentVelocity * -0.8f, ForceMode.Acceleration);
			}
			Charge();
			break;
		case EntityState.TacticalRetreat:
			TacticalRetreat();
			break;
		case EntityState.Fleeing:
			Flee();
			break;
		case EntityState.Fallen:
			movement.moving = false;
			FallenCheck();
			break;
		case EntityState.Grabbed:
			movement.moving = false;
			GrabbedState();
			break;
		case EntityState.Ungrounded:
			movement.moving = false;
			if (!my.killingHandler.canSuffocate && my.basicInfo._inWater && !selfRighting.Grabbed)
			{
				WaterRight();
			}
			else
			{
				Ungrounded();
			}
			break;
		case EntityState.CantMove:
			movement.moving = false;
			break;
		case EntityState.Suffocating:
			movement.moving = false;
			WaterRight();
			break;
		case EntityState.Dead:
			movement.moving = false;
			break;
		}
		if (TargetBlock.gotTarget && selfRighting.LockedRotation && looking.Focus != FocusOn.Nothing && looking.Focus != FocusOn.DirectControl && looking.rotateRigidbody)
		{
			Quaternion rot = Quaternion.Slerp(my.Rigidbody.rotation, looking.TargetRotation, Time.deltaTime * looking.Smoothing);
			if ((my.Rigidbody.constraints & (RigidbodyConstraints)80) != RigidbodyConstraints.None && (my.Rigidbody.constraints & RigidbodyConstraints.FreezeRotationY) == 0)
			{
				rot = Quaternion.Euler(0f, rot.eulerAngles.y, 0f);
			}
			my.Rigidbody.MoveRotation(rot);
		}
	}

	protected virtual void Animate()
	{
		if (isDead || !StatMaster.levelSimulating || StatMaster.GodTools.GravityDisabled)
		{
			if (!bob.pause)
			{
				BobPlayPause();
			}
		}
		else if (!selfRighting.Grabbed && disposition.myState != EntityState.Ungrounded && disposition.myState != EntityState.Fallen && !selfRighting.Fallen && grounded)
		{
			if (TargetBlock.gotTarget && ((looking.Focus != FocusOn.Target && looking.Focus != FocusOn.TargetOpposite) || !(movement.DifferenceToTargetSqr > BehavioursMaxDistance)))
			{
				SetFocus();
			}
			if (bob.Able)
			{
				if ((bool)my.killingHandler.my.Poser && my.killingHandler.my.Poser.animateWhileMoving)
				{
					AnimateOnMovement();
				}
				else
				{
					AnimateBob();
				}
			}
			else if (!bob.pause)
			{
				BobPlayPause();
			}
		}
		else
		{
			if (!bob.pause)
			{
				BobPlayPause();
			}
			if (disposition.myState == EntityState.Ungrounded && !selfRighting.Grabbed && my.basicInfo._inWater && !my.killingHandler.canSuffocate)
			{
				AnimateBob(!isDead, 3f, 0.1f);
			}
		}
	}

	public void BobPlayPause()
	{
		if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.BobPlayPause);
		}
		bob.pause = !bob.pause;
	}

	protected void GetState()
	{
		if (!isDead && StatMaster.levelSimulating && !StatMaster.GodTools.GravityDisabled)
		{
			GetMoral();
			SelectState();
		}
	}

	public void StopDizzyParticles()
	{
		if (selfRighting.ConfusedParticles.Length != 0 && selfRighting.ConfusedParticles[0].isPlaying)
		{
			if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && levelEntity != null)
			{
				levelEntity.Event(NetworkEntity.EntityEvent.StopDizzyParticles);
			}
			for (int i = 0; i < selfRighting.ConfusedParticles.Length; i++)
			{
				selfRighting.ConfusedParticles[i].Stop();
				selfRighting.ConfusedParticles[i].Clear();
			}
		}
	}

	public void PlayDizzyParticles()
	{
		if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && levelEntity != null)
		{
			levelEntity.Event(NetworkEntity.EntityEvent.PlayDizzyParticles);
		}
		if (selfRighting.ConfusedParticles.Length != 0 && !selfRighting.ConfusedParticles[0].isPlaying)
		{
			for (int i = 0; i < selfRighting.ConfusedParticles.Length; i++)
			{
				selfRighting.ConfusedParticles[i].Play();
			}
		}
	}

	private void OnParticleCollision(GameObject other)
	{
		ParticleSystem component = other.GetComponent<ParticleSystem>();
		List<ParticleCollisionEvent> list = new List<ParticleCollisionEvent>();
		Vector3 vector = zero;
		int collisionEvents = component.GetCollisionEvents(base.gameObject, list);
		for (int i = 0; i < collisionEvents; i++)
		{
			vector += list[i].velocity;
		}
		float sqrMagnitude = vector.sqrMagnitude;
		if (sqrMagnitude > selfRighting.particleVelocityThreshold && disposition.myState != EntityState.Fallen && !selfRighting.Grabbed)
		{
			FallOver(false);
		}
	}

	public virtual void Idle()
	{
		if (!movement.Able || Mathf.Abs(movement.CurrentVelocity.y) > 0.5f)
		{
			return;
		}
		if (disposition.currentBehaviour.Action == Action.WalkAround)
		{
			if (movement.randomWalkTimer >= movement.randomWalkRate + movement.randomWalkPeriod)
			{
				movement.randomWalkTimer = 0f;
				Vector3 onUnitSphere = UnityEngine.Random.onUnitSphere;
				onUnitSphere.y = 0f;
				movement.randomWalkDir = movement.avoidanceOffsetAngle * onUnitSphere.normalized;
			}
			else
			{
				movement.randomWalkTimer += Time.deltaTime;
			}
			if (movement.randomWalkTimer < movement.randomWalkPeriod)
			{
				my.Rigidbody.AddForce(movement.randomWalkDir * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
		}
		else if (movement.idleDampenTimer > 0.066f && movement.VelocitySqr > 0.1f)
		{
			movement.idleDampenTimer -= Time.deltaTime;
			my.Rigidbody.AddForce(-movement.CurrentVelocity * 0.75f, ForceMode.Acceleration);
		}
		else if (!movement.returnToIdle)
		{
			movement.returnToIdle = true;
			my.Rigidbody.velocity = zero;
			my.Rigidbody.angularVelocity = zero;
			my.Rigidbody.Sleep();
		}
	}

	public void Charge()
	{
		if (movement.Able)
		{
			if (movement.lineUp)
			{
				Vector3 vector = movement.lineUpPos - movement.PreviousPosition;
				vector.Normalize();
				my.Rigidbody.AddForce(vector * movement.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
			else
			{
				my.Rigidbody.AddForce(movement.Direction * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
		}
	}

	public void Approach()
	{
		if (movement.Able)
		{
			if (movement.lineUp)
			{
				Vector3 vector = movement.lineUpPos - movement.PreviousPosition;
				vector.Normalize();
				my.Rigidbody.AddForce(vector * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
			else
			{
				my.Rigidbody.AddForce(movement.avoidanceOffsetAngle * movement.Direction * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
		}
	}

	public void FactionCharge()
	{
		if (movement.Able && TargetBlock.gotTarget)
		{
			if (!movement.skipToCharge)
			{
				my.Rigidbody.AddForce(movement.factionchargeDir * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
			else
			{
				Charge();
			}
		}
	}

	public virtual void CircleStrafe()
	{
		if (movement.Able)
		{
			if (looking.strafeSideways)
			{
				my.Rigidbody.AddForce(movement.avoidanceOffsetAngle * my.visRight * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
			else
			{
				my.Rigidbody.AddForce(Vector3.Cross(Vector3.up, movement.Direction) * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
			}
		}
	}

	public virtual void Stationary()
	{
	}

	public virtual void Flee()
	{
		if (!movement.Able)
		{
			return;
		}
		if (!TargetBlock.gotTarget && my.fireController.onFire && movement.randomWalkDir == zero)
		{
			movement.Direction = (movement.randomWalkDir = movement.avoidanceOffsetAngle * new Vector3(UnityEngine.Random.insideUnitSphere.x, 0f, UnityEngine.Random.insideUnitSphere.z));
			if (TargetBlock.deflectTargetRotation != Vector3.zero)
			{
				movement.Direction = Quaternion.Euler(TargetBlock.deflectTargetRotation) * movement.Direction;
			}
			movement.Direction.Normalize();
		}
		my.Rigidbody.AddForce(-movement.Direction * disposition.currentBehaviour.parameters.Speed - movement.CurrentVelocity, ForceMode.Acceleration);
	}

	public void TacticalRetreat()
	{
		if (movement.Able)
		{
			Vector3 vector = CalculateRetreatDir();
			my.Rigidbody.AddForce(-(movement.Speed * vector * 2f) - movement.CurrentVelocity, ForceMode.Acceleration);
		}
	}

	private Vector3 CalculateRetreatDir()
	{
		if (!movement.Able)
		{
			return zero;
		}
		Vector3 previousPosition = movement.PreviousPosition;
		Vector3 middlePosition = my.ActiveMachine.MiddlePosition;
		if (TargetBlock.isBlock)
		{
			return (middlePosition - previousPosition).normalized;
		}
		Vector3 vector;
		if (TargetBlock.isAI)
		{
			if (TargetBlock.AI.my.fireController.onFire)
			{
				return (TargetBlock.trans.position - previousPosition).normalized;
			}
			vector = TargetBlock.AI.faction.Center;
		}
		else if (!TargetBlock.gotTarget)
		{
			Faction closestFaction = FactionsController.GetClosestFaction(faction);
			if (closestFaction == null)
			{
				return (middlePosition - previousPosition).normalized;
			}
			vector = closestFaction.Center;
		}
		else
		{
			vector = movement.TargetPos;
		}
		Vector3 rhs = previousPosition - middlePosition;
		Vector3 normalized = (vector - middlePosition).normalized;
		float num = Vector3.Distance(middlePosition, vector);
		float num2 = Vector3.Dot(normalized, rhs);
		if (num2 <= 0f)
		{
			return (middlePosition - previousPosition).normalized;
		}
		if (num2 >= num)
		{
			return (vector - previousPosition).normalized;
		}
		Vector3 vector2 = normalized * num2;
		Vector3 vector3 = middlePosition + vector2;
		return (vector3 - previousPosition).normalized;
	}

	protected void AnimateBob()
	{
		float f = 0f;
		if (StatMaster.isClient && !StatMaster.isLocalSim)
		{
			if (float.IsNaN(f))
			{
				f = 0f;
			}
			Vector3 vector = (my.Transform.position - bob.previousBobPos) / Time.deltaTime;
			if (Mathf.Abs(vector.y) > 10f / Time.deltaTime)
			{
				return;
			}
			bob.BobVel = bob.BobVel * 0.8f + new Vector3(vector.x, 0f, vector.z).sqrMagnitude * 0.2f;
			if (float.IsNaN(bob.BobVel))
			{
				bob.BobVel = 0f;
			}
			f = bob.BobVel;
			bob.previousBobPos = my.Transform.position;
		}
		else
		{
			if (bob.pause)
			{
				BobPlayPause();
			}
			f = new Vector3(movement.CurrentVelocity.x, 0f, movement.CurrentVelocity.z).sqrMagnitude;
		}
		bool flag = f > 0.1f;
		float vel = 0f;
		if (flag)
		{
			vel = ((!(f * 10f > 5f)) ? (f * 10f) : 5f);
		}
		AnimateBob(flag, vel, 1f);
	}

	protected void AnimateBob(bool animate, float vel, float speed)
	{
		if (useJointAsGround && grounded)
		{
			animate = false;
		}
		if (animate)
		{
			bob.phi = (Time.time + bob.deSync) / bob.Rate * bob.bobRateMultiphi;
			bob.amplitude = (float)Math.Cos(bob.phi * speed) * 0.5f + 0.5f;
			bob.smoothVel = Mathf.Lerp(bob.smoothVel, vel, Time.deltaTime * bob.lerpSpeed);
			my.VisObject.localPosition = new Vector3(bob.visPosX, bob.startY + bob.amplitude * bob.Amount * bob.smoothVel, bob.visPosZ);
		}
		else if (!my.VisObject.localPosition.y.Equals(bob.startY))
		{
			my.VisObject.localPosition = Vector3.Lerp(my.VisObject.localPosition, new Vector3(bob.visPosX, bob.startY, bob.visPosZ), Time.deltaTime * bob.lerpSpeed);
		}
	}

	protected void AnimateOnMovement()
	{
		my.killingHandler.my.Poser.MoveAnim(disposition.myState);
	}

	protected void ActivateZeroG()
	{
		wasGravDisabled = true;
		selfRighting.LockedRotation = false;
		my.Rigidbody.constraints = RigidbodyConstraints.None;
		my.Rigidbody.AddForce(UnityEngine.Random.insideUnitSphere * 10f + new Vector3(0f, UnityEngine.Random.value * 200f, 0f), ForceMode.Acceleration);
		my.Rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * 10f);
		my.Rigidbody.drag = 0f;
	}

	protected void DeActivateZeroG()
	{
		wasGravDisabled = false;
		selfRighting.LockedRotation = false;
		my.Rigidbody.constraints = RigidbodyConstraints.None;
		disposition.myState = EntityState.Ungrounded;
		if (!useJointAsGround && !useKinematicAsGround)
		{
			grounded = false;
		}
	}

	protected IEnumerator AntiStuck()
	{
		if (!isDead && StatMaster.levelSimulating)
		{
			movement.AntiStuckRunning = true;
			yield return new WaitForSeconds(1f);
			if (!movement.inJump && (movement.PreviousPosition - movement.PreviousPosition).sqrMagnitude < 0.2f && movement.avoidanceOffset != 0f && (disposition.myState == EntityState.Pursuing || disposition.myState == EntityState.Strafing || disposition.myState == EntityState.TacticalRetreat) && movement.VelocitySqr < 15f && listOfBlockCollisions.Count >= 2)
			{
				Jump(1f);
			}
			movement.avoidanceOffset = 0f;
			movement.AntiStuckRunning = false;
		}
	}

	public void UpdateTargetBlock()
	{
		if (isDead || !StatMaster.levelSimulating || !disposition.AutomaticTargetSystem || disposition.myState == EntityState.Grabbed || disposition.myState == EntityState.Fallen || selfRighting.Fallen)
		{
			return;
		}
		if (!TargetBlock.gotTarget || disposition.myState == EntityState.Idle)
		{
			GetNewTarget();
			return;
		}
		if (TargetBlock.isAI)
		{
			if (TargetBlock.AI.isDead)
			{
				if (TargetedBy.Contains(TargetBlock.AI))
				{
					TargetedBy.Remove(TargetBlock.AI);
				}
				ClearTargetsTargetedBy();
				TargetBlock.Null();
				GetNewTarget();
				return;
			}
		}
		else if (!object.ReferenceEquals(TargetBlock.BlockHealth, null) && TargetBlock.BlockHealth.health <= 0f && TargetBlock.Block.ParentMachine.hasIntactBlocks)
		{
			GetNewTarget();
			return;
		}
		if (disposition.canAttack)
		{
			if (TargetBlock.isAI)
			{
				if (TargetBlock.AI.TargetedBy.Count > FactionsController.targetLimit && TargetBlock.AI.faction.Infantry.Count > 2)
				{
					GetNewTarget();
					return;
				}
				if (!TargetBlock.AI.disposition.canAttack && FactionsController.AvailableFactions.Count > 2)
				{
					GetNewTarget();
					return;
				}
			}
			if (TargetBlock.isBlock && object.ReferenceEquals(TargetBlock.BlockHealth, null) && !object.ReferenceEquals(my.attackScript, null) && TargetBlock.Block.ParentMachine.hasIntactBlocks)
			{
				GetNewTarget();
			}
			else if (TargetBlock.gotTarget && !TargetBlock.isBlock && !TargetBlock.isAI)
			{
				GetNewTarget();
			}
		}
		else if (TargetedBy.Count > 0 && (OldTargetedBy != TargetedBy.First() || TargetedBy.Count != OldTargetedByCount))
		{
			GetNewTarget();
			OldTargetedBy = TargetedBy.First();
			OldTargetedByCount = TargetedBy.Count;
		}
	}

	protected void GetNewTarget()
	{
		if (!FactionsController.setupComplete)
		{
			return;
		}
		Faction value;
		if (!FactionsController.Factions.TryGetValue(faction.Name, out value))
		{
			FactionsController.AddSingleFaction(this);
		}
		EntityAI entityAI = null;
		if (TargetBlock.isAI)
		{
			entityAI = TargetBlock.AI;
		}
		if (FactionsController.AvailableFactions.Count <= 1 && disposition.canAttack && faction.AttackOnlyTypeOf != FactionsController.AttackOnlyEnum.Ai)
		{
			int closestMachine = FactionsController.GetClosestMachine(this);
			if (closestMachine != -1)
			{
				BlockBehaviour blockBehaviour = ((!disposition.SmartTargeting) ? ReferenceMaster.GetRandomBlock((uint)closestMachine) : ReferenceMaster.GetRandomIntactBlock((uint)closestMachine));
				if (!object.ReferenceEquals(blockBehaviour, null) && !blockBehaviour.IsDestroyed)
				{
					TargetBlock.NewTargetBlock(blockBehaviour.transform, blockBehaviour.Rigidbody);
					if (!object.ReferenceEquals(blockBehaviour, null) && !object.ReferenceEquals(entityAI, null) && TargetBlock.AI != entityAI)
					{
						ClearTargetsTargetedBy(entityAI);
					}
				}
				return;
			}
		}
		TargetFinder();
		if (TargetBlock.isAI && !object.ReferenceEquals(entityAI, null) && TargetBlock.AI != entityAI)
		{
			ClearTargetsTargetedBy(entityAI);
		}
	}

	protected void TargetFinder()
	{
		if (faction.AttackOnlyTypeOf != FactionsController.AttackOnlyEnum.Machine)
		{
			if (!object.ReferenceEquals(faction.TargetFaction, null) && !object.ReferenceEquals(faction.TargetFaction.Infantry, null) && !faction.TargetFaction.Neutralized)
			{
				TargetBlock.NewTargetBlock(FactionsController.GetNewTargetFromFaction(this));
				return;
			}
			foreach (EntityAI item in TargetedBy)
			{
				if (item != null)
				{
					EntityState myState = item.disposition.myState;
					if (myState != EntityState.Idle && myState != EntityState.Fleeing && FactionsController.CheckDistance(item, this))
					{
						TargetBlock.NewTargetBlock(item.transform);
						return;
					}
				}
			}
		}
		TargetBlock.NewTargetBlock(FactionsController.GetNewDiscriminantTarget(this));
	}

	protected virtual void SetFocus()
	{
		FocusOn focusOn = looking.Focus;
		Vector3 targetPos = movement.TargetPos;
		Vector3 previousPosition = movement.PreviousPosition;
		Vector3 vector = zero;
		targetPos.y = previousPosition.y;
		if (my.fireController.onFire)
		{
			looking.Focus = FocusOn.Velocity;
		}
		if (disposition.myState == EntityState.Fleeing)
		{
			focusOn = FocusOn.TargetOpposite;
		}
		else if (disposition.myState == EntityState.Idle)
		{
			focusOn = FocusOn.Velocity;
		}
		switch (focusOn)
		{
		case FocusOn.Target:
			vector = targetPos - previousPosition;
			if (TargetBlock.deflectTargetRotation != Vector3.zero)
			{
				vector = Quaternion.Euler(TargetBlock.deflectTargetRotation) * vector;
			}
			break;
		case FocusOn.Velocity:
			if (movement.VelocitySqr > 0.1f)
			{
				vector = new Vector3(movement.CurrentVelocity.x, 0f, movement.CurrentVelocity.z);
			}
			break;
		case FocusOn.TargetOpposite:
			vector = previousPosition - targetPos;
			if (TargetBlock.deflectTargetRotation != Vector3.zero)
			{
				vector = Quaternion.Euler(TargetBlock.deflectTargetRotation) * vector;
			}
			break;
		case FocusOn.ReverseVelocity:
			if (movement.CurrentVelocity != zero)
			{
				vector = -new Vector3(movement.CurrentVelocity.x, 0f, movement.CurrentVelocity.z);
			}
			break;
		case FocusOn.Nothing:
			return;
		}
		if (Mathf.Abs(vector.x) + Mathf.Abs(vector.z) > 0.002f)
		{
			looking.TargetRotation = Quaternion.LookRotation(vector, my.worldUp);
		}
		waitForFirstRotation = false;
	}

	public void SelectState()
	{
		if (disposition.useStateMachine)
		{
			if (!isDead)
			{
				if (!my.fireController.onFire)
				{
					bool flag = false;
					switch (subAIType)
					{
					case AIType.Fish:
						flag = !my.basicInfo.InWater && my.basicInfo.submergedPercent < suffocatingLimit;
						break;
					case AIType.LandBased:
					case AIType.Bird:
						flag = my.basicInfo.InWater && my.basicInfo.submergedPercent > suffocatingLimit;
						break;
					}
					if (selfRighting.Grabbed)
					{
						disposition.myState = EntityState.Grabbed;
					}
					else if (my.killingHandler.canSuffocate && flag)
					{
						if (useKinematicAsGround)
						{
							if (my.basicInfo.submergedPercent > 0.9f)
							{
								SetDynamic();
							}
						}
						else if (useJointAsGround && my.basicInfo.submergedPercent > 0.9f && (bool)groundJoint)
						{
							UnityEngine.Object.Destroy(groundJoint);
							groundJoint = null;
						}
						disposition.myState = EntityState.Suffocating;
					}
					else if (selfRighting.Fallen)
					{
						if (grounded)
						{
							disposition.myState = EntityState.Fallen;
						}
						else
						{
							disposition.myState = EntityState.Ungrounded;
						}
					}
					else if (retreating.useMoral && retreating.exeededMoralValue && !retreating.coward)
					{
						StartCoroutine(Cowardly());
					}
					else if (!retreating.coward)
					{
						if (movement.DifferenceToTargetSqr <= movement.distanceCloseState && disposition.myState != EntityState.Fleeing && disposition.useBehaviours)
						{
							disposition.myState = EntityState.Close;
						}
						else if (!TargetBlock.gotTarget)
						{
							disposition.myState = EntityState.Idle;
						}
						else if (disposition.useBehaviours)
						{
							GetDisposition();
						}
						else if (!retreating.coward)
						{
							disposition.myState = aiControllerState;
						}
						else
						{
							disposition.myState = EntityState.Idle;
						}
					}
				}
				else if (selfRighting.Grabbed)
				{
					disposition.myState = EntityState.Grabbed;
				}
				else if (selfRighting.Fallen)
				{
					if (grounded)
					{
						disposition.myState = EntityState.Fallen;
					}
					else
					{
						disposition.myState = EntityState.Ungrounded;
					}
				}
				else if (!retreating.coward)
				{
					if (retreating.useMoral && retreating.exeededMoralValue)
					{
						StartCoroutine(Cowardly());
					}
					else if (disposition.OverwriteFleeOnFire)
					{
						disposition.myState = EntityState.Fleeing;
					}
					else
					{
						disposition.myState = EntityState.Pursuing;
					}
				}
				else
				{
					disposition.myState = EntityState.Fleeing;
				}
			}
			else
			{
				disposition.myState = EntityState.Dead;
			}
		}
		else if (selfRighting.Grabbed)
		{
			disposition.myState = EntityState.Grabbed;
		}
		else if (selfRighting.Fallen)
		{
			if (grounded)
			{
				disposition.myState = EntityState.Fallen;
			}
			else
			{
				disposition.myState = EntityState.Ungrounded;
			}
		}
		else
		{
			disposition.myState = EntityState.CantMove;
		}
	}

	public float GetBehaviourMaxRad()
	{
		float num = float.MinValue;
		for (int i = 0; i < disposition.behavioursArray.Length; i++)
		{
			if (disposition.behavioursArray[i].Radius > num)
			{
				num = disposition.behavioursArray[i].Radius;
			}
		}
		return Mathf.Clamp(num * num, float.MinValue, float.MaxValue);
	}

	public void SetDynamic()
	{
		base.transform.parent = ReferenceMaster.physicsGoalInstance;
		my.Rigidbody.isKinematic = false;
		my.basicInfo.isKinematic = false;
		useKinematicAsGround = false;
		my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		grounded = false;
	}

	private int CurrentDisposition()
	{
		float num = float.MaxValue;
		int result = -1;
		for (int i = 0; i < disposition.behavioursArray.Length; i++)
		{
			Behaviour behaviour = disposition.behavioursArray[i];
			float num2 = movement.DifferenceToTargetSqr - behaviour.RadiusSqr;
			if ((disposition.currentBehaviour.id == behaviour.id || !(num2 < 10f) || !(num2 > -10f)) && (num2 <= 0f || (disposition.currentBehaviour.id != behaviour.id && num2 < 10f)) && num >= behaviour.Radius)
			{
				num = behaviour.Radius;
				result = i;
			}
		}
		return result;
	}

	protected void GetDisposition()
	{
		int num = CurrentDisposition();
		if (num == -1)
		{
			disposition.myState = EntityState.Idle;
			return;
		}
		disposition.currentBehaviour = disposition.behavioursArray[num];
		switch (disposition.currentBehaviour.Action)
		{
		case Action.ApproachTarget:
			disposition.myState = EntityState.Pursuing;
			break;
		case Action.PursueTarget:
			disposition.myState = EntityState.Pursuing;
			break;
		case Action.FactionCharge:
			disposition.myState = EntityState.FactionCharge;
			break;
		case Action.Flee:
			disposition.myState = EntityState.Fleeing;
			break;
		case Action.Strafe:
			disposition.myState = EntityState.Strafing;
			break;
		case Action.Stationary:
			disposition.myState = EntityState.Stationary;
			break;
		case Action.WalkAround:
			disposition.myState = EntityState.Idle;
			break;
		case Action.None:
			disposition.myState = EntityState.Idle;
			break;
		}
	}

	public void GetMoral()
	{
		if (!FactionsController.setupComplete)
		{
			return;
		}
		float num = (1f - health / retreating.MaxHealth) * retreating.HealthAmountInfluence;
		float num2 = faction.LossOverTime * retreating.LossOverTimeInfluence;
		float num3 = 0f;
		float num4 = 0f;
		if (TargetBlock.isAI)
		{
			EntityAI aI = TargetBlock.AI;
			num3 = (faction.Loss - aI.faction.Loss) * retreating.InfanteryCountInfluence;
		}
		else
		{
			if (!TargetBlock.isBlock)
			{
				retreating.currentMoral = retreating.MoralLimit - num - num2;
				retreating.moralWasCalculated = true;
				return;
			}
			num4 = (faction.Loss - faction.machineLoss) * retreating.InfanteryCountInfluence;
		}
		retreating.currentMoral = retreating.MoralLimit - num3 - num - num4 - num2;
		if (my.fireController.onFire)
		{
			retreating.currentMoral -= retreating.MoralLimit * 0.5f;
		}
		retreating.moralWasCalculated = true;
	}

	private IEnumerator Cowardly()
	{
		UTBisRunning = false;
		disposition.useBehaviours = false;
		retreating.coward = true;
		if ((bool)my.attackScript)
		{
			my.attackScript.ClearHiddenProjectiles();
		}
		float dist = float.MinValue;
		while (dist < retreating.minDistDisappear)
		{
			if (disposition.myState != EntityState.Grabbed && disposition.myState != EntityState.Fallen && !selfRighting.Fallen)
			{
				disposition.myState = EntityState.Fleeing;
				if (TargetBlock.gotTarget)
				{
					dist = (TargetBlock.trans.position - HeadPosition).sqrMagnitude;
				}
			}
			if (isDead)
			{
				break;
			}
			yield return null;
		}
		CancelInvoke("UpdateTargetBlock");
		StartCoroutine(FadeAway());
	}

	protected void AiVictorious()
	{
		StopCoroutines();
		disposition.useBehaviours = false;
		bob.Able = true;
	}

	protected virtual void SelfRight()
	{
		selfRighting.Fallen = true;
		selfRighting.LockedRotation = false;
		my.Rigidbody.constraints = RigidbodyConstraints.None;
		if (grounded)
		{
			if (selfRighting.Timer < selfRighting.SleepTime + selfRighting.RandomWait)
			{
				selfRighting.Timer += Time.deltaTime;
				PlayDizzyParticles();
				return;
			}
			my.Rigidbody.angularDrag = selfRighting.angularDrag;
			my.Rigidbody.drag = selfRighting.angularDrag;
			Vector3 vector = (my.TransformUP - my.worldUp) * selfRighting.Torque * aiBaseHight;
			Vector3 vector2 = aiBaseCenterOffset + movement.PreviousPosition;
			my.Rigidbody.AddForceAtPosition(-vector, my.TransformUP * aiBaseHight + vector2, ForceMode.Acceleration);
			my.Rigidbody.AddForceAtPosition(vector, -my.TransformUP * aiBaseHight + vector2, ForceMode.Acceleration);
			float upRightAngle = movement.upRightAngle;
			float num = (1f - upRightAngle) * bob.diffToY;
			my.VisObject.localPosition = new Vector3(bob.visPosX, bob.startY + num, bob.visPosZ);
			if (upRightAngle < -0.88f)
			{
				my.Rigidbody.AddForceAtPosition(my.Transform.forward - my.worldUp, my.TransformUP, ForceMode.Acceleration);
			}
			if (!(upRightAngle > 0.95f) || !grounded)
			{
				return;
			}
			selfRighting.FallenCount = 0;
			velocityCleared = false;
			selfRighting.Timer = 0f;
			selfRighting.Fallen = false;
			disposition.myState = EntityState.Idle;
			my.VisObject.localPosition = new Vector3(bob.visPosX, bob.startY, bob.visPosZ);
			selfRighting.LockedRotation = true;
			if (freezRigidbody)
			{
				if (!looking.rotateRigidbody)
				{
					my.Rigidbody.rotation = movement.identityQuat;
					my.Rigidbody.constraints = (RigidbodyConstraints)80;
				}
				else
				{
					selfRighting.StartRotation.eulerAngles = new Vector3(selfRighting.StartRotation.eulerAngles.x, my.Rigidbody.rotation.eulerAngles.y, selfRighting.StartRotation.eulerAngles.z);
					my.Rigidbody.constraints = RigidbodyConstraints.None;
					my.Rigidbody.MoveRotation(selfRighting.StartRotation);
					my.Rigidbody.constraints = (RigidbodyConstraints)80;
				}
			}
			my.Rigidbody.angularDrag = selfRighting.ResetDrag;
			my.Rigidbody.drag = 0f;
			StopDizzyParticles();
		}
		else
		{
			StopDizzyParticles();
			if (my.Rigidbody.drag == selfRighting.angularDrag)
			{
				my.Rigidbody.drag = 0f;
			}
		}
	}

	protected virtual void WaterRight()
	{
		if (!selfRighting.Grabbed)
		{
			Vector3 vector = (my.TransformUP - my.worldUp) * selfRighting.Torque * aiBaseHight * 0.5f;
			Vector3 vector2 = aiBaseCenterOffset + movement.PreviousPosition;
			my.Rigidbody.AddForceAtPosition(-vector, my.TransformUP * aiBaseHight + vector2, ForceMode.Acceleration);
			my.Rigidbody.AddForceAtPosition(vector, -my.TransformUP * aiBaseHight + vector2, ForceMode.Acceleration);
			float upRightAngle = movement.upRightAngle;
			if (upRightAngle < -0.88f)
			{
				my.Rigidbody.AddForceAtPosition(my.Transform.forward - my.worldUp, my.TransformUP, ForceMode.Acceleration);
			}
		}
	}

	protected virtual void GrabbedState()
	{
		if (selfRighting.CanBreakGrab && selfRighting.StopBeingGrabbedBy != null)
		{
			my.Rigidbody.AddForce(selfRighting.forceUsedWhileGrabbed * UnityEngine.Random.onUnitSphere, ForceMode.Impulse);
		}
		else
		{
			if (pinchedBetween.Count < 2)
			{
				return;
			}
			for (int i = 0; i < pinchedBetween.Count; i++)
			{
				Transform transform = pinchedBetween[i];
				if (transform == null || !transform.gameObject.activeInHierarchy)
				{
					pinchedBetween.Clear();
					StopBeingGrabbed();
					break;
				}
			}
		}
	}

	public void ResetFall()
	{
		velocityCleared = false;
		selfRighting.Timer = 0f;
		selfRighting.Fallen = false;
		my.VisObject.localPosition = new Vector3(bob.visPosX, bob.startY, bob.visPosZ);
		selfRighting.LockedRotation = true;
	}

	public virtual void FallOver(bool clear)
	{
		if (!selfRighting.enabled || !selfRighting.AllowedToFall || !AllowedToModifyConstraints || isDead || !my.Rigidbody)
		{
			return;
		}
		if (movement.upRightAngle > 0.95f && grounded)
		{
			my.Rigidbody.AddForceAtPosition(my.Transform.forward - my.worldUp, my.TransformUP, ForceMode.Acceleration);
		}
		if (clear && !velocityCleared)
		{
			velocityCleared = true;
			selfRighting.RandomWait = UnityEngine.Random.Range(0f, 0.25f);
			if (grounded)
			{
				my.Rigidbody.velocity -= movement.PreviousVelocity;
				my.Rigidbody.angularVelocity = zero;
			}
			selfRighting.Timer = 0f;
			selfRighting.FallenCount++;
		}
		bob.diffToY = Mathf.Abs(my.VisObject.localPosition.y - bob.startY);
		selfRighting.LockedRotation = false;
		my.Rigidbody.constraints = RigidbodyConstraints.None;
		if (my.basicInfo._inWater)
		{
			disposition.myState = EntityState.Ungrounded;
		}
		else
		{
			selfRighting.Fallen = true;
		}
		if (selfRighting.FallenCount > selfRighting.FallenMaxCount)
		{
			DieNoJump();
		}
	}

	protected virtual void FallenCheck()
	{
		if (my.basicInfo._inWater && !selfRighting.Grabbed)
		{
			ResetFall();
			WaterRight();
		}
		else if (grounded && movement.CurrentVelocity.y < 0.1f && movement.CurrentVelocity.y > -0.1f)
		{
			if (my.Rigidbody.angularDrag != selfRighting.angularDrag)
			{
				my.Rigidbody.angularDrag = selfRighting.angularDrag;
			}
			if (movement.VelocitySqr < selfRighting.selfRightLimit)
			{
				SelfRight();
			}
		}
		else if (my.Rigidbody.angularDrag == selfRighting.angularDrag)
		{
			my.Rigidbody.angularDrag = selfRighting.ResetDrag;
		}
	}

	protected virtual void Ungrounded()
	{
		StopDizzyParticles();
	}

	public virtual bool GroundedCheck()
	{
		if (useKinematicAsGround && my.Rigidbody.isKinematic)
		{
			return true;
		}
		if (useJointAsGround && (bool)groundJoint && (bool)groundJoint.connectedBody)
		{
			return true;
		}
		if (!movement.inJump)
		{
			EntityState myState = disposition.myState;
			if (myState == EntityState.Grabbed || myState == EntityState.Suffocating)
			{
				return false;
			}
			if (movement.CurrentVelocity.y < (0f - gcVelocityThreshold) * 2f || movement.CurrentVelocity.y > gcVelocityThreshold)
			{
				return false;
			}
			if (!useJointAsGround)
			{
				if (movement.CurrentVelocity.y < 2f && movement.CurrentVelocity.y > -2f && grounded)
				{
					return true;
				}
			}
			else
			{
				useJointAsGround = false;
			}
			if (!grounded || !firstGroundTouch)
			{
				return CastRay();
			}
			return true;
		}
		return false;
	}

	protected bool CastRay()
	{
		float maxDistance = ((!movement.inJump && !selfRighting.Fallen) ? 1f : aiBaseHight);
		RaycastHit hitInfo = default(RaycastHit);
		if (Physics.Raycast(movement.PreviousPosition - my.Transform.rotation * (aiBaseCenterOffset + new Vector3(0f, aiBaseHight - 0.01f, 0f)), -my.worldUp, out hitInfo, maxDistance, groundLayerMask, QueryTriggerInteraction.Ignore))
		{
			return true;
		}
		return false;
	}

	protected void CheckPinch()
	{
		for (int num = listOfBlockCollisions.Count - 1; num >= 0; num--)
		{
			Transform transform = listOfBlockCollisions[num];
			if (transform == null || !transform.gameObject.activeInHierarchy)
			{
				listOfBlockCollisions.RemoveAt(num);
			}
		}
		if (listOfBlockCollisions.Count < 2)
		{
			return;
		}
		for (int i = 0; i < listOfBlockCollisions.Count; i++)
		{
			Transform transform2 = listOfBlockCollisions[i];
			for (int j = i; j < listOfBlockCollisions.Count; j++)
			{
				Vector3 lhs = movement.PreviousPosition - transform2.position;
				lhs.Normalize();
				Vector3 rhs = movement.PreviousPosition - listOfBlockCollisions[j].position;
				rhs.Normalize();
				float num2 = Vector3.Dot(lhs, rhs);
				if (num2 < -0.83f)
				{
					Grabbed();
					pinchedBetween.Add(transform2);
					pinchedBetween.Add(listOfBlockCollisions[j]);
					return;
				}
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!StatMaster.levelSimulating || isDead || !base.enabled || collision.contacts.Length == 0)
		{
			return;
		}
		OnCollisionReact(collision);
		float num = Vector3.Dot(collision.contacts[0].normal, my.worldUp);
		CollisionStruct collisionStruct = new CollisionStruct();
		collisionStruct.angle = num;
		collisionStruct.collider = collision.collider;
		if (num <= movement.MaxAscent)
		{
			float num2 = ((disposition.myState == EntityState.Fallen) ? 0.5f : 1f);
			if (!movement.inJump && !my.basicInfo.BeingVacuumed && !selfRighting.Grabbed && collision.relativeVelocity.sqrMagnitude > selfRighting.FallImpactThreshold * num2 * (selfRighting.FallImpactThreshold * num2))
			{
				FallOver(true);
			}
		}
		else if (num > movement.straightSurfaceAngle)
		{
			movement.StraightSurface = collision.transform;
		}
		Rigidbody rigidbody = collision.rigidbody;
		if (!object.ReferenceEquals(rigidbody, null))
		{
			Vector3 forward = my.Transform.forward;
			if (!looking.rotateRigidbody)
			{
				forward = my.VisObject.forward;
			}
			Vector3 rhs = rigidbody.worldCenterOfMass - movement.PreviousPosition;
			rhs.y = 0f;
			float num3 = Vector3.Dot(forward, rhs);
			if (num3 > dotForCollisionTargetChange)
			{
				Transform transform = rigidbody.transform;
				BlockBehaviour component = transform.GetComponent<BlockBehaviour>();
				if (!object.ReferenceEquals(component, null))
				{
					if (!disposition.SmartTargeting || (!object.ReferenceEquals(component.BlockHealth, null) && component.BlockHealth.health > 0f) || num3 > 0.9f)
					{
						TargetBlock.NewTargetBlock(transform, rigidbody);
					}
					if (!listOfBlockCollisions.Contains(collision.collider.transform))
					{
						listOfBlockCollisions.Add(collision.collider.transform);
					}
					CheckPinch();
				}
				else if (TargetBlock.isAI)
				{
					EntityAI aiFromTransform = FactionsController.GetAiFromTransform(transform);
					if (!object.ReferenceEquals(aiFromTransform, null) && aiFromTransform.faction == TargetBlock.AI.faction && aiFromTransform != TargetBlock.AI && !aiFromTransform.isDead)
					{
						TargetBlock.NewTargetBlock(collision.transform, rigidbody);
					}
				}
			}
		}
		if ((!movement.walkUpSlopes && !movement.canJump && !movement.objectAvoidance) || disposition.myState == EntityState.Grabbed)
		{
			return;
		}
		collisionStruct.normal = collision.contacts[0].normal;
		if (collisionStruct.angle >= movement.MaxAscent)
		{
			if (collisionStruct.angle < 0.99f && movement.walkUpSlopes && disposition.myState != EntityState.Fallen && !selfRighting.Fallen)
			{
				collisionStruct.normalVector = collisionStruct.normal;
				collisionStruct.normalVector.y = 0f;
				collisionStruct.normalVectorLength = collisionStruct.normalVector.sqrMagnitude;
			}
			collisionList.Add(collisionStruct);
			return;
		}
		if (grounded && disposition.myState != EntityState.Fallen && disposition.currentBehaviour.Action != Action.WalkAround && !movement.inJump && disposition.myState != EntityState.Close && !movement.jumpedThisFrame && movement.canJump && !movement.hitHighObject)
		{
			CapsuleCollider objA = collisionStruct.collider as CapsuleCollider;
			if (!object.ReferenceEquals(objA, null))
			{
				return;
			}
			SphereCollider objA2 = collisionStruct.collider as SphereCollider;
			if (!object.ReferenceEquals(objA2, null))
			{
				return;
			}
			collisionStruct.height = CalculateCollisionHight(collisionStruct.collider);
			if (collisionStruct.height < movement.jumpHeight && collisionStruct.height > 0.2f)
			{
				movement.jumpedThisFrame = true;
				StartCoroutine(Jump(collisionStruct.height, collisionStruct.normal * movement.jumpDirectionMultiplier));
				return;
			}
			if (collisionStruct.height > movement.jumpHeight)
			{
				movement.hitHighObject = true;
			}
		}
		collisionList.Add(collisionStruct);
	}

	private void OnCollisionExit(Collision collision)
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		Transform item = collision.collider.transform;
		Collider collider = collision.collider;
		for (int num = collisionList.Count - 1; num >= 0; num--)
		{
			if (collisionList[num].collider == collider)
			{
				collisionList.RemoveAt(num);
			}
		}
		if (listOfBlockCollisions.Contains(item))
		{
			if (pinchedBetween.Count >= 2 && pinchedBetween.Contains(item))
			{
				pinchedBetween.Clear();
				StopBeingGrabbed();
			}
			listOfBlockCollisions.Remove(item);
		}
		if ((bool)movement.StraightSurface && collision.transform == movement.StraightSurface.transform)
		{
			movement.StraightSurface = null;
		}
	}

	protected virtual void OnCollisionReact(Collision collision)
	{
	}

	protected void CostumOnCollisionStay()
	{
		if (my.basicInfo._inWater)
		{
			return;
		}
		for (int i = 0; i < collisionList.Count; i++)
		{
			CollisionStruct collisionStruct = collisionList[i];
			if (collisionStruct.angle >= movement.MaxAscent)
			{
				if (!firstGroundTouch)
				{
					firstGroundTouch = true;
				}
				if (!useJointAsGround && !useKinematicAsGround)
				{
					grounded = true;
					yPos = base.transform.position.y;
				}
				if (movement.inJump && !movement.jumpedThisFrame)
				{
					movement.inJump = false;
				}
				if (!movement.Able)
				{
					return;
				}
				if (collisionStruct.angle < 0.99f && movement.walkUpSlopes && disposition.myState != EntityState.Fallen && !selfRighting.Fallen)
				{
					if (movement.CurrentVelocity.y >= 0f)
					{
						my.Rigidbody.velocity -= collisionStruct.normalVector * movement.slopeClimbingSpeed * Time.deltaTime;
					}
					else
					{
						my.Rigidbody.velocity += collisionStruct.normalVector * movement.slopeClimbingSpeed * Time.deltaTime;
					}
				}
				if (!movement.objectAvoidance)
				{
					return;
				}
			}
			else if (grounded && disposition.myState != EntityState.Fallen && !movement.inJump && disposition.myState != EntityState.Close && (movement.jumpedThisFrame || !(movement.CurrentVelocity.y <= 0f) || !movement.canJump || movement.hitHighObject || !(collisionStruct.height < movement.jumpHeight) || !(collisionStruct.height > 0.2f)) && disposition.myState != EntityState.Controlled && movement.objectAvoidance && collisionStruct.collider != null)
			{
				movement.hitHighObject = true;
				if (Vector3.Dot(my.visRight, movement.PreviousPosition - collisionStruct.collider.transform.position) > 0f)
				{
					movement.avoidanceOffset = 45f;
				}
				else
				{
					movement.avoidanceOffset = -45f;
				}
			}
		}
		if (!bol && collisionList.Count > 1)
		{
			bol = true;
		}
	}

	protected void Jump(float height)
	{
		StartCoroutine(Jump(height, zero));
	}

	protected IEnumerator Jump(float height, Vector3 dir)
	{
		yield return new WaitForEndOfFrame();
		CheckForHighObject();
		if (!movement.hitHighObject)
		{
			movement.inJump = true;
			grounded = false;
			my.Rigidbody.velocity = zero;
			my.Rigidbody.AddForce(new Vector3(0f, (movement.jumpForcePerUnit - movement.counterGravity) * height + movement.counterGravity, 0f) * my.Rigidbody.mass, ForceMode.Impulse);
			StartCoroutine(LerpToBobStart(height / 2f));
		}
		movement.hitHighObject = false;
		yield return null;
		movement.jumpedThisFrame = false;
		while (movement.inJump && !selfRighting.Fallen)
		{
			my.Rigidbody.AddForce(new Vector3(0f - dir.x, (!(dir.y > 0f)) ? (0f - dir.y) : dir.y, 0f - dir.z) * Time.deltaTime * my.Rigidbody.mass);
			yield return null;
		}
	}

	private IEnumerator LerpToBobStart(float time)
	{
		float timeGone = 0f;
		float currentYPos = my.VisObject.localPosition.y;
		float timePercent = 0f;
		while (timeGone < time)
		{
			timeGone += Time.deltaTime;
			timePercent = timeGone / time;
			my.VisObject.localPosition = new Vector3(bob.visPosX, Mathf.Lerp(currentYPos, bob.startY, timePercent), bob.visPosZ);
			yield return null;
		}
	}

	protected float CalculateCollisionHight(Collider col)
	{
		float num = 0f;
		float num2 = movement.PreviousPosition.y - aiBaseHight;
		return col.bounds.max.y - num2;
	}

	protected void CheckForHighObject()
	{
		float num = Mathf.Abs(aiBaseHight - movement.jumpHeight);
		for (int i = 0; i < listOfBlockCollisions.Count; i++)
		{
			if (listOfBlockCollisions[i] == null)
			{
				listOfBlockCollisions.Clear();
				break;
			}
			float num2 = listOfBlockCollisions[i].position.y - movement.PreviousPosition.y;
			if (num2 > num)
			{
				movement.hitHighObject = true;
				break;
			}
		}
	}

	public void CalculateHeight(SkinnedMeshRenderer ren)
	{
		Quaternion rotation = ren.transform.rotation;
		float num = 0f;
		Vector3 vector = rotation * ren.bounds.extents;
		num = vector.y;
		aiBaseWidth = ((!(vector.x > vector.z)) ? vector.z : vector.x);
		aiBaseHight = num;
		aiBaseWidth *= aiBaseWidth;
		aiBaseCenterOffset = ren.bounds.center - base.transform.position;
	}

	public void CalculateHeight(Collider collider)
	{
		Quaternion rotation = collider.transform.rotation;
		Transform transform = collider.transform;
		float num = 0f;
		if (collider is BoxCollider)
		{
			BoxCollider boxCollider = collider as BoxCollider;
			Vector3 vector = rotation * boxCollider.bounds.extents;
			num = vector.y;
			aiBaseWidth = ((!(vector.x > vector.z)) ? vector.z : vector.x);
			aiBaseCenterOffset = boxCollider.bounds.center - my.Transform.position;
		}
		else if (collider is SphereCollider)
		{
			SphereCollider sphereCollider = collider as SphereCollider;
			num = (aiBaseWidth = sphereCollider.bounds.extents.x);
			aiBaseCenterOffset = sphereCollider.bounds.center - my.Transform.position;
		}
		else if (collider is MeshCollider)
		{
			MeshCollider meshCollider = collider as MeshCollider;
			Vector3 vector = rotation * meshCollider.bounds.extents;
			num = vector.y;
			aiBaseWidth = ((!(vector.x > vector.z)) ? vector.z : vector.x);
			aiBaseCenterOffset = meshCollider.bounds.center - my.Transform.position;
		}
		else if (collider is CapsuleCollider)
		{
			CapsuleCollider capsuleCollider = collider as CapsuleCollider;
			Vector3 vector2 = zero;
			switch (capsuleCollider.direction)
			{
			case 0:
			{
				float num2 = ((!(transform.localScale.y > transform.localScale.z)) ? transform.localScale.z : transform.localScale.y);
				vector2 = new Vector3(transform.localScale.x * (capsuleCollider.height / 2f), num2 * capsuleCollider.radius, num2 * capsuleCollider.radius);
				break;
			}
			case 1:
			{
				float num2 = ((!(transform.localScale.x > transform.localScale.z)) ? transform.localScale.z : transform.localScale.x);
				vector2 = new Vector3(num2 * capsuleCollider.radius, transform.localScale.y * (capsuleCollider.height / 2f), num2 * capsuleCollider.radius);
				break;
			}
			case 2:
			{
				float num2 = ((!(transform.localScale.y > transform.localScale.x)) ? transform.localScale.x : transform.localScale.y);
				vector2 = new Vector3(num2 * capsuleCollider.radius, num2 * capsuleCollider.radius, transform.localScale.z * (capsuleCollider.height / 2f));
				break;
			}
			}
			Vector3 vector = rotation * vector2;
			num = vector.y;
			aiBaseWidth = ((!(vector.x > vector.z)) ? vector.z : vector.x);
			aiBaseCenterOffset = capsuleCollider.bounds.center - my.Transform.position;
		}
		aiBaseHight = num;
		aiBaseWidth *= aiBaseWidth;
	}

	public virtual void Grabbed(MonoBehaviour grabber = null)
	{
		if (useKinematicAsGround)
		{
			SetDynamic();
		}
		else if (useJointAsGround && (bool)groundJoint)
		{
			UnityEngine.Object.Destroy(groundJoint);
			groundJoint = null;
		}
		selfRighting.Timer = 0f;
		selfRighting.RandomWait = 0f;
		selfRighting.LockedRotation = false;
		my.Rigidbody.constraints = RigidbodyConstraints.None;
		if (!movement.keepInterpolation)
		{
			my.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		}
		selfRighting.Grabbed = true;
		if (grabber is JoinOnTriggerBlock)
		{
			selfRighting.StopBeingGrabbedBy = (grabber as JoinOnTriggerBlock).BreakJoint;
		}
		else if (grabber is HarpoonTrigger)
		{
			selfRighting.StopBeingGrabbedBy = (grabber as HarpoonTrigger).Detach;
		}
		else if (grabber is VacuumController)
		{
			selfRighting.StopBeingGrabbedBy = (grabber as VacuumController).ScheduleJointBreak;
		}
		StartCoroutine(LerpToBobStart(0.5f));
		if (selfRighting.CanBreakGrab && selfRighting.StopBeingGrabbedBy != null)
		{
			StartCoroutine(BreakGrab(selfRighting.timeToBreakGrab));
		}
	}

	public virtual void StopBeingGrabbed()
	{
		FallOver(true);
		selfRighting.Grabbed = false;
		selfRighting.StopBeingGrabbedBy = null;
		if (!movement.keepInterpolation)
		{
			my.Rigidbody.interpolation = RigidbodyInterpolation.None;
		}
	}

	public virtual IEnumerator BreakGrab(float delay)
	{
		yield return new WaitForSeconds(delay);
		if (selfRighting.Grabbed && selfRighting.StopBeingGrabbedBy != null)
		{
			selfRighting.StopBeingGrabbedBy();
		}
	}

	public virtual void Die()
	{
		isDead = true;
		if (useJointAsGround)
		{
			SetDynamic();
		}
		BloodQuad();
		selfRighting.LockedRotation = false;
		faction.suddenLoss += 1f;
		my.basicInfo.density += onDeath.extraDensity;
		my.Rigidbody.angularDrag += onDeath.extreAngularDrag;
		if (groundJoint != null)
		{
			groundJoint.breakForce = 0f;
			groundJoint.breakTorque = 0f;
		}
		CheckAchievements();
		if (!StatMaster.isMP || my.aiGenEntity.SimPhysics)
		{
			if (StatMaster.isMP)
			{
				if (my.aiGenEntity.NetBlock != null)
				{
					my.aiGenEntity.NetBlock.Event(NetworkEntity.EntityEvent.Kill);
				}
				else
				{
					Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
				}
			}
			RemoveFromInfantryList();
			if ((bool)my.attackScript)
			{
				my.attackScript.ClearHiddenProjectiles();
			}
			my.Rigidbody.constraints = RigidbodyConstraints.None;
			my.Rigidbody.AddRelativeTorque(onDeath.LeapTorque * my.Rigidbody.mass);
			my.Rigidbody.AddForce(my.worldUp * onDeath.LeapAmount * UnityEngine.Random.Range(1f, 1.5f), ForceMode.Acceleration);
			if (!my.Collider)
			{
				my.Collider.material.dynamicFriction = 0.3f;
				my.Collider.material.staticFriction = 0.3f;
			}
		}
		my.VisObject.localPosition = new Vector3(bob.visPosX, bob.startY, bob.visPosZ);
		for (int i = 0; i < onDeath.objectsToDisableOnDeath.Length; i++)
		{
			onDeath.objectsToDisableOnDeath[i].SetActive(false);
		}
	}

	public void DieNoJump()
	{
		if (StatMaster.isMP && my.aiGenEntity != null && my.aiGenEntity.SimPhysics)
		{
			if (my.aiGenEntity.NetBlock != null)
			{
				my.aiGenEntity.NetBlock.Event(NetworkEntity.EntityEvent.Kill);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
		CheckAchievements();
		if ((bool)my.Rigidbody)
		{
			my.Rigidbody.angularDrag += onDeath.extreAngularDrag;
		}
		isDead = true;
		if ((bool)my.basicInfo)
		{
			my.basicInfo.density += onDeath.extraDensity;
		}
		BloodQuad();
		if (groundJoint != null)
		{
			groundJoint.breakForce = 0f;
			groundJoint.breakTorque = 0f;
		}
		selfRighting.LockedRotation = false;
		if ((bool)my.Rigidbody)
		{
			my.Rigidbody.constraints = RigidbodyConstraints.None;
		}
		faction.suddenLoss += 1f;
		RemoveFromInfantryList();
		if ((bool)my.attackScript && my.attackScript.ranged)
		{
			my.attackScript.ClearHiddenProjectiles();
		}
		if (!my.Collider)
		{
			my.Collider.material.dynamicFriction = 1f;
			my.Collider.material.staticFriction = 1f;
		}
		for (int i = 0; i < onDeath.objectsToDisableOnDeath.Length; i++)
		{
			onDeath.objectsToDisableOnDeath[i].SetActive(false);
		}
	}

	private void CheckAchievements()
	{
		if (!StatMaster.isMP)
		{
			AchievementHelper.Increment(8, 1);
			if (my.killingHandler.activeType == InjuryType.Fire && factionSystem.faction == FactionsController.FactionEnum.Animal && base.name.ToLower().Contains("chicken"))
			{
				AchievementHelper.Increment(16, 1);
			}
		}
	}

	public IEnumerator DieFromCowardice()
	{
		isDead = true;
		if (base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted += victoryValue;
		}
		if (!object.ReferenceEquals(my.killingHandler.OnDeath, null))
		{
			my.killingHandler.OnDeath(my.killingHandler);
		}
		my.VisObject.gameObject.SetActive(false);
		RemoveFromInfantryList();
		yield return null;
		base.gameObject.SetActive(false);
	}

	protected void RemoveFromInfantryList()
	{
		if (faction.Infantry.Contains(this))
		{
			faction.Infantry.Remove(this);
			ClearTargetsTargetedBy();
			Remove();
		}
	}

	public void BloodQuad()
	{
		if (!OptionsMaster.BesiegeConfig.BloodEnabled)
		{
			return;
		}
		if (my.basicInfo.InWater)
		{
			if (my.killingHandler.activeType == InjuryType.Suffocateing)
			{
				my.Rigidbody.AddTorque(-base.transform.right * 400f, ForceMode.Impulse);
			}
			ParticleSystem[] system;
			if (my.basicInfo.InWater && GlobalParticles.GetParticleSystem(8, out system))
			{
				for (int i = 0; i < system.Length; i++)
				{
					UnityEngine.Object.Instantiate(system[i], my.VisObject.position, Quaternion.LookRotation(Vector3.down), ReferenceMaster.physicsGoalInstance);
				}
			}
			if (WaterController.Exist && GlobalParticles.GetParticleSystem(9, out system))
			{
				for (int j = 0; j < system.Length; j++)
				{
					UnityEngine.Object.Instantiate(system[j], my.VisObject.position, my.VisObject.rotation, my.VisObject);
				}
			}
		}
		else if (onDeath.bloodQuad != null && base.transform.position.y < SingleInstanceFindOnly<AddPiece>.Instance.floorHeight + aiBaseHight * 2f)
		{
			Decal component = onDeath.bloodQuad.GetComponent<Decal>();
			Renderer component2 = onDeath.bloodQuad.GetComponent<Renderer>();
			component2.material.color = StatMaster.BloodColor;
			onDeath.bloodQuad.parent = ReferenceMaster.physicsGoalInstance;
			onDeath.bloodQuad.position = new Vector3(base.transform.position.x, (!component) ? onDeath.floorYpos : (base.transform.position.y - aiBaseHight + 0.1f), base.transform.position.z);
			onDeath.bloodQuad.forward = Vector3.up;
			onDeath.bloodQuad.localEulerAngles = new Vector3(90f, onDeath.bloodQuad.localEulerAngles.y, UnityEngine.Random.Range(0f, 360f));
			component2.enabled = true;
			if (component != null)
			{
				component.material = component2.material;
				component.enabled = true;
			}
		}
	}

	public void ClearTargetsTargetedBy()
	{
		if (TargetBlock.isAI && TargetBlock.AI.TargetedBy.Contains(this))
		{
			TargetBlock.AI.TargetedBy.Remove(this);
		}
	}

	public void ClearTargetsTargetedBy(EntityAI target)
	{
		if (target.TargetedBy.Contains(this))
		{
			target.TargetedBy.Remove(this);
		}
	}

	public IEnumerator FadeAway()
	{
		yield return StartCoroutine(FadeOut());
		StartCoroutine(DieFromCowardice());
	}

	protected IEnumerator FadeOut()
	{
		while (retreating.fading.fadeProgress < 1f)
		{
			retreating.fading.currentFadeTime += Time.deltaTime;
			if (disposition.myState != EntityState.Dead)
			{
				disposition.myState = EntityState.Fleeing;
			}
			retreating.fading.fadeProgress = Mathf.Clamp01(retreating.fading.currentFadeTime / retreating.fading.fadeTime);
			retreating.fading.Fade(retreating.fading.fadeProgress);
			if (StatMaster.isMP && StatMaster.isHosting && StatMaster.levelSimulating && levelEntity != null)
			{
				levelEntity.Event(NetworkEntity.EntityEvent.Fade, (byte)retreating.fading.fadeProgress);
			}
			yield return null;
		}
	}

	public void DisableAI()
	{
		StopCoroutines();
		looking.Focus = FocusOn.Nothing;
		movement.Able = false;
		movement.canJump = false;
		my.Rigidbody.velocity = zero;
		my.Rigidbody.angularVelocity = zero;
		disposition.canAttack = false;
	}

	protected void StartCoroutines()
	{
		InvokeRepeating("UpdateTargetBlock", 0f, 1f);
		UTBisRunning = true;
		InvokeRepeating("GetState", 0f, 0.25f);
	}

	protected void StopCoroutines()
	{
		CancelInvoke("UpdateTargetBlock");
		UTBisRunning = false;
		CancelInvoke("GetState");
	}

	private void OnBecameVisible()
	{
		if (!bob.Able && bob.startValue)
		{
			bob.Able = true;
		}
	}

	private void OnBecameInvisible()
	{
		if (bob.Able && bob.startValue)
		{
			bob.Able = false;
		}
	}

	private void OnDisable()
	{
		if (StatMaster.levelSimulating && !isDead)
		{
			RemoveFromInfantryList();
		}
	}

	public void Strip()
	{
		UnityEngine.Object.Destroy(my.Collider);
		UnityEngine.Object.Destroy(my.fireController.fireTagCode);
		UnityEngine.Object.Destroy(my.fireController);
		UnityEngine.Object.Destroy(my.attackScript);
		if (my.killingHandler != null)
		{
			if (my.killingHandler.my != null)
			{
				UnityEngine.Object.Destroy(my.killingHandler.my.Poser);
			}
			UnityEngine.Object.Destroy(my.killingHandler);
		}
		UnityEngine.Object.Destroy(this);
	}

	protected virtual void EnterWater()
	{
	}

	protected virtual void ExitWater()
	{
	}

	protected void OnDestroy()
	{
		RemoveFromInfantryList();
	}
}
