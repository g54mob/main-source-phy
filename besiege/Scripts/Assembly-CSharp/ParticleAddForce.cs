using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ParticleAddForce : SimBehaviour
{
	public BlockBehaviour block;

	public ParticleSystem part;

	public float particleForce = 100f;

	public bool StartDelay = true;

	public bool canDouse = true;

	public bool useRelativeForce;

	public bool addForceToOwnBlock;

	protected List<ParticleCollisionEvent> collisionEvents;

	protected Rigidbody rb;

	protected BasicInfo bInfo;

	protected bool hasBody;

	protected Rigidbody myRigidbody;

	private int numCollisionEvents;

	private int safeLength;

	private Vector3 pos;

	private Vector3 force;

	private Machine machine;

	private FireTag waterTagCode;

	private bool hasMachine;

	private float delayStartTime = 0.1f;

	public bool interactWithWater = true;

	private float particleInterval = 0.1f;

	private float particleTime;

	private WaterLod waterLod;

	private ParticleSystem.TriggerModule trigger;

	private Vector3 delta = Vector3.zero;

	protected ParticleSystem.EmitParams foamEmitter = default(ParticleSystem.EmitParams);

	protected List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>(3);

	protected override void Start()
	{
		base.Start();
		if (!base.isSimulating)
		{
			return;
		}
		particleTime = Random.Range(0f, particleInterval);
		if (interactWithWater && WaterController.Exist)
		{
			waterLod = WaterController.WaterLOD;
			interactWithWater = !object.ReferenceEquals(waterLod, null) && (bool)waterLod.collisionPlane;
			if (interactWithWater && OptionsMaster.BesiegeConfig.WaterCannonRippling)
			{
				trigger = part.trigger;
				trigger.SetCollider(0, waterLod.collisionPlane);
				trigger.inside = ((!RipplePostProcessing.Active) ? ParticleSystemOverlapAction.Kill : ParticleSystemOverlapAction.Callback);
				trigger.enabled = OptionsMaster.BesiegeConfig.WaterCannonRippling;
			}
		}
		StartDelay = true;
		if (!object.ReferenceEquals(block, null))
		{
			machine = block.ParentMachine;
		}
		else
		{
			machine = GetComponentInParent<Machine>();
		}
		hasMachine = !object.ReferenceEquals(machine, null);
		if (object.ReferenceEquals(part, null))
		{
			part = GetComponent<ParticleSystem>();
		}
		if (!hasMachine || machine.SimPhysics)
		{
			collisionEvents = new List<ParticleCollisionEvent>(16);
			if (HasBasicInfo && !basicInfo.noRigidbody)
			{
				myRigidbody = basicInfo.Rigidbody;
				hasBody = true;
			}
			else
			{
				myRigidbody = base.transform.GetComponentInParent<Rigidbody>();
				hasBody = myRigidbody != null;
			}
			delayStartTime = Time.fixedTime + 0.1f;
		}
	}

	protected void OnParticleTrigger()
	{
		if (!(Time.time > particleTime) || !RipplePostProcessing.Active || !OptionsMaster.BesiegeConfig.WaterCannonRippling)
		{
			return;
		}
		int triggerParticles = part.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
		if (triggerParticles > 0)
		{
			Vector3 vector = ((!hasBody) ? delta : myRigidbody.velocity);
			vector.y = 0f;
			float num = vector.sqrMagnitude * 40f;
			for (int i = 0; i < triggerParticles; i++)
			{
				ParticleSystem.Particle value = enter[i];
				float y = value.velocity.y;
				y = y * y * 0.1f + num;
				EmitRipple(value.position, y);
				value.lifetime = 0f;
				enter[i] = value;
			}
			part.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, enter);
		}
		particleTime = Time.time + particleInterval;
	}

	public void EmitRipple(Vector3 emitPosition, float velocity)
	{
		if (velocity > 10f)
		{
			foamEmitter.startSize = Random.Range(15f, 25f);
			foamEmitter.startColor = new Color32(160, 80, byte.MaxValue, (byte)(int)Mathf.Clamp(velocity * velocity * 0.05f, 0f, 80f));
			emitPosition += Random.insideUnitSphere;
			emitPosition.y = WaterController.waterTransformHeight + Random.Range(0f, 0.5f);
			foamEmitter.position = emitPosition;
			GlobalParticles.EmitParticle(15, foamEmitter, 1);
			GlobalParticles.EmitParticleAmount(0, emitPosition, 1);
		}
	}

	protected void OnParticleCollision(GameObject other)
	{
		if (hasMachine)
		{
			if (!machine.SimPhysics || !machine.isSimulating)
			{
				return;
			}
		}
		else if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (Time.fixedTime < delayStartTime)
		{
			return;
		}
		bInfo = other.GetComponent<BasicInfo>();
		if (bInfo == null || bInfo.noRigidbody)
		{
			return;
		}
		rb = bInfo.Rigidbody;
		if (canDouse)
		{
			bool flag = false;
			switch (bInfo.infoType)
			{
			case BasicInfo.BasicInfoType.Block:
			{
				BlockBehaviour blockBehaviour = bInfo as BlockBehaviour;
				flag = blockBehaviour.Prefab.canBurn;
				waterTagCode = blockBehaviour.fireTag;
				if (flag && blockBehaviour.SurfaceType)
				{
					flag = waterTagCode != null;
				}
				break;
			}
			case BasicInfo.BasicInfoType.Entity:
			{
				LevelEntity entity = (bInfo as GenericEntity).entity;
				flag = entity.hasFireController;
				waterTagCode = entity.fireTag;
				break;
			}
			case BasicInfo.BasicInfoType.Projectile:
				if (bInfo is ProjectileInfo)
				{
					ProjectileInfo projectileInfo = bInfo as ProjectileInfo;
					if (projectileInfo.hasProjectile)
					{
						waterTagCode = projectileInfo.projectile.firecontrol.fireTagCode;
						flag = waterTagCode != null;
						break;
					}
				}
				waterTagCode = rb.GetComponent<FireTag>();
				flag = waterTagCode != null;
				break;
			default:
				waterTagCode = rb.GetComponent<FireTag>();
				flag = waterTagCode != null;
				break;
			}
			if (flag)
			{
				waterTagCode.WaterHit();
			}
		}
		if (rb.isKinematic || (!addForceToOwnBlock && hasBody && rb == myRigidbody))
		{
			return;
		}
		safeLength = part.GetSafeCollisionEventSize();
		if (collisionEvents.Count < safeLength)
		{
			collisionEvents = new List<ParticleCollisionEvent>(safeLength);
		}
		numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
		float num = particleForce * particleForce * (1f - bInfo.submergedPercent * 0.95f);
		if (useRelativeForce)
		{
			Vector3 velocity = rb.velocity;
			for (int i = 0; i < numCollisionEvents; i++)
			{
				pos = collisionEvents[i].intersection;
				force = collisionEvents[i].velocity - velocity;
				rb.AddForceAtPosition(force * num, pos);
			}
		}
		else
		{
			for (int j = 0; j < numCollisionEvents; j++)
			{
				pos = collisionEvents[j].intersection;
				force = collisionEvents[j].velocity;
				rb.AddForceAtPosition(force * num, pos);
			}
		}
	}
}
