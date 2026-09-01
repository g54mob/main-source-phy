using UnityEngine;

public class CogMotorDamage : SimBehaviour
{
	private class CollisionContainer
	{
		public BasicInfo bInfo;

		public Collision col;

		public GameObject go;

		public CollisionContainer(BasicInfo b, Collision c, GameObject g)
		{
			bInfo = b;
			col = c;
			go = g;
		}
	}

	private int floorLayer = 29;

	private float timeMultiplication = 1f;

	public bool ignoreDamageFromZ;

	public float damageToNewAI = 800f;

	public float damageToBlock = 0.5f;

	public bool UseCollisionStay = true;

	public float jointDamageScale = 0.1f;

	public Vector3 rotationAxis;

	public float degrees = 90f;

	public bool hasEmitted;

	public int maxParticleEmittionsPerFrame = 4;

	private Transform particleTransform;

	private CogMotorControllerHinge cogController;

	private float speedDamageLimited;

	[SerializeField]
	protected ParticleSystem sparks;

	[SerializeField]
	protected int particles = 2;

	private ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);

	private ContactPoint contact;

	private Vector3 _rotationAxis;

	[HideInInspector]
	public float eulerX;

	[HideInInspector]
	public float drillDistance;

	private Vector3 worldRotationAxis;

	private Vector3 sparkDirection;

	private int sparkEmitted;

	protected override void Awake()
	{
		base.Awake();
		cogController = basicInfo as CogMotorControllerHinge;
		if (sparks != null)
		{
			particleTransform = sparks.transform;
			emitParams.applyShapeToPosition = true;
		}
		_rotationAxis = rotationAxis.normalized * ((!cogController.Flipped) ? 1 : (-1));
		if (base.isSimulating && !base.SimPhysics)
		{
			base.enabled = false;
		}
		else
		{
			speedDamageLimited = Mathf.Clamp01(cogController.speedSlider.Value);
		}
	}

	private void Update()
	{
		if (base.isSimulating && base.SimPhysics)
		{
			sparkEmitted = 0;
		}
	}

	private void OnCollisionStay(Collision collisionInfo)
	{
		if ((UseCollisionStay && !base.isSimulating) || !base.SimPhysics || collisionInfo == null || collisionInfo.gameObject == null)
		{
			return;
		}
		GameObject gameObject = collisionInfo.collider.gameObject;
		if (gameObject.layer == floorLayer)
		{
			return;
		}
		if (ignoreDamageFromZ)
		{
			Vector3 lhs = collisionInfo.contacts[0].point - base.transform.position;
			float num = Vector3.Dot(lhs, base.transform.forward);
			if (num > 0.7f)
			{
				return;
			}
		}
		BasicInfo componentInParent = gameObject.GetComponentInParent<BasicInfo>();
		if (!(componentInParent == null) && !componentInParent.noRigidbody)
		{
			OnCollision(componentInParent, collisionInfo.contacts[0], collisionInfo.collider);
		}
	}

	private void OnCollision(BasicInfo bInfo, ContactPoint con, Collider col)
	{
		if (Mathf.Abs(cogController.Velocity) < 0.1f || cogController.Rigidbody.velocity.sqrMagnitude < 100f)
		{
			return;
		}
		if (sparkEmitted <= maxParticleEmittionsPerFrame)
		{
			sparkEmitted++;
			if (cogController.Prefab.Type == BlockType.Drill)
			{
				EmitDrillSparks(con);
			}
			else
			{
				EmitSparks(con);
			}
		}
		BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
		if (!StatMaster.GodTools.UnbreakableMode && !object.ReferenceEquals(blockBehaviour, null))
		{
			if (blockBehaviour.gotChildBlocks)
			{
				BlockBehaviour childBlockFromCollider = blockBehaviour.GetChildBlockFromCollider(col);
				if (!object.ReferenceEquals(childBlockFromCollider, null))
				{
					blockBehaviour = childBlockFromCollider;
				}
			}
			if (UseCollisionStay)
			{
				timeMultiplication = Time.deltaTime;
			}
			if (blockBehaviour.Prefab.hasHealthBar)
			{
				blockBehaviour.BlockHealth.DamageBlock(speedDamageLimited * damageToBlock * timeMultiplication);
			}
			else if (ReduceBreakForceOnImpact.Used && blockBehaviour.Prefab.reduceBreakforce && blockBehaviour.BreakOnImpact != null)
			{
				blockBehaviour.BreakOnImpact.ReduceJointBreakForce(cogController.Velocity * timeMultiplication * jointDamageScale);
			}
			if (blockBehaviour.isParented && blockBehaviour.jointBreakForce <= 0f)
			{
				blockBehaviour.UnParentChildBlock(blockBehaviour);
			}
			return;
		}
		if (bInfo.hasAiScript)
		{
			KillingHandler killingHandler = bInfo.aiEntity.my.killingHandler;
			killingHandler.TakeDamage(damageToNewAI * Time.deltaTime, InjuryType.Sharp);
			return;
		}
		EnemyAISimple enemyAISimple = bInfo as EnemyAISimple;
		if (!object.ReferenceEquals(enemyAISimple, null))
		{
			enemyAISimple.TakeDamage(damageToNewAI * Time.deltaTime, InjuryType.Sharp);
			return;
		}
		ShipPartHitManager component = bInfo.GetComponent<ShipPartHitManager>();
		if (component != null)
		{
			component.ShipPartialDamage(Time.fixedDeltaTime);
		}
	}

	public void EmitSparks(ContactPoint contact)
	{
		if (sparks == null)
		{
			return;
		}
		emitParams.position = contact.point;
		worldRotationAxis = base.transform.rotation * _rotationAxis;
		sparkDirection = Quaternion.AngleAxis(degrees, worldRotationAxis) * Vector3.ProjectOnPlane(contact.normal, worldRotationAxis);
		particleTransform.rotation = Quaternion.LookRotation(sparkDirection, base.transform.forward);
		if (!hasEmitted)
		{
			Quaternion quaternion = Quaternion.Inverse(Quaternion.AngleAxis(base.transform.localRotation.eulerAngles.z, base.transform.forward));
			eulerX = Vector3.Angle(sparkDirection, quaternion * base.transform.up);
			if (Vector3.Dot(sparkDirection, quaternion * base.transform.right) > 0f)
			{
				eulerX *= -1f;
			}
			hasEmitted = true;
		}
		sparks.Emit(emitParams, particles);
	}

	public void EmitSparksClient(float angle)
	{
		if (!(sparks == null))
		{
			worldRotationAxis = base.transform.rotation * _rotationAxis;
			Quaternion quaternion = Quaternion.Inverse(Quaternion.AngleAxis(base.transform.localRotation.eulerAngles.z, base.transform.forward));
			particleTransform.rotation = Quaternion.LookRotation(Quaternion.AngleAxis(angle, base.transform.forward) * (quaternion * base.transform.up), base.transform.forward);
			sparks.Emit(particles);
		}
	}

	public void EmitDrillSparks(ContactPoint contact)
	{
		if (!(sparks == null))
		{
			emitParams.position = contact.point;
			if (!hasEmitted)
			{
				drillDistance = base.transform.InverseTransformPoint(contact.point).z / cogController.DefaultBounds.extents.z;
				hasEmitted = true;
			}
			sparks.Emit(emitParams, particles);
		}
	}

	public void EmitDrillSparksClient(float length)
	{
		if (!(sparks == null))
		{
			Vector3 localPosition = particleTransform.localPosition;
			Vector3 extents = cogController.DefaultBounds.extents;
			Vector3 position = base.transform.TransformPoint(localPosition + new Vector3(0f, 0f, extents.z * length));
			emitParams.position = position;
			sparks.Emit(emitParams, particles);
		}
	}
}
