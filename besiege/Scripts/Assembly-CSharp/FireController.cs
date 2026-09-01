using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class FireController : SimBehaviour
{
	public enum OverlapType
	{
		Default = 0,
		Sphere = 1,
		Box = 2
	}

	public const float IGNITE_CHANCE = 0.1f;

	[Header("State")]
	public bool onFire;

	[Header("Parameters")]
	public float burnIntensity;

	public float pctDmgOnBurn = 0.998f;

	public float igniteDelay = 0.15f;

	[FormerlySerializedAs("destroyTimer")]
	public float earlyBurnDuration = 2f;

	[FormerlySerializedAs("onFireDuration")]
	public float lateBurnDuration = 2f;

	public float randomAmount = 4f;

	public float douseDelay;

	public float submergeLimit = 0.2f;

	public float colliderRepeatRate = 0.5f;

	[Header("Overlap")]
	public OverlapType overlapType;

	public float overlapRadius;

	public Vector3 overlapCenter;

	public Vector3 overlapSize;

	public Transform additionalRope;

	private bool hasRope;

	[Header("Options")]
	public bool dontSetSelfOnFire;

	public bool displayBurnOnBlock = true;

	public Vector3 torqueToAddOnBurn = Vector3.zero;

	public float fallOverDelay = 0.8f;

	[Header("References")]
	public bool hasFireTag;

	public FireTag fireTagCode;

	public ParticleSystem fireParticles;

	public ParticleSystem[] additionalFireParticles;

	public Transform[] sendKillMessage;

	public Transform[] sendDousedMessage;

	[Obsolete("aiKillCode is obsolete.")]
	[HideInInspector]
	public BleedOnJointBreak aiKillCode;

	[Obsolete("aiKillCode is obsolete.")]
	[HideInInspector]
	public GibOnImpact aiKillCode2;

	[Obsolete("parentRigidbody is obsolete. Now infers the Rigidbody from its BasicInfo.")]
	[HideInInspector]
	public Rigidbody parentRigidbody;

	[Obsolete("myBody is obsolete. No longer uses a Rigidbody to detect objects.")]
	public Rigidbody myBody;

	[Obsolete("myCollider is obsolete. Please overlapSize/Center/Radius instead", false)]
	public Collider myCollider;

	[HideInInspector]
	public bool hasSpread;

	[HideInInspector]
	public float fireProgress;

	[HideInInspector]
	public float fullFireDuration;

	[HideInInspector]
	public float currentFireDuration;

	[HideInInspector]
	public bool setBreakForce = true;

	[NonSerialized]
	public bool hasFullFireDuration;

	private int fireLayerMask;

	private Renderer[] renderers;

	private Vector3 explosionPos;

	private float burningDuration;

	private float flameTimer;

	private float currentTime;

	private BlockVisualController bvc;

	private StructuralPhysTile structuralTile;

	private ParticleSystem.ExternalForcesModule externalForcesModule;

	private bool hasFireParticles;

	private Vector3 lossyScale = Vector3.one;

	private BlockHealthBar healthBar;

	private bool hasHealthBar;

	private bool isAwake;

	private Collider[] hitColliders = new Collider[0];

	public bool randomIgniteChance = true;

	private Color burntColor = new Color(0.2f, 0.2f, 0.2f, 1f);

	private Color[] startColours;

	private Color startRimColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	[HideInInspector]
	public bool hasStartingAtributes;

	protected Coroutine catchFire;

	protected bool catchingFire;

	public static float baseDelay = 0.25f;

	public Joint[] joints;

	private Vector2[] multiJointStartBreakForces;

	private Vector2[] multiJointMinBreakForces;

	private bool disableFire
	{
		get
		{
			return StatMaster.Rules.DisableFire;
		}
	}

	private IEnumerator ColliderPulse()
	{
		if (myBody != null)
		{
			myBody.WakeUp();
		}
		if (!object.ReferenceEquals(myCollider, null))
		{
			myCollider.enabled = true;
		}
		yield return new WaitForFixedUpdate();
		if (!object.ReferenceEquals(myCollider, null))
		{
			myCollider.enabled = false;
		}
	}

	protected override void Awake()
	{
		isAwake = true;
		base.Awake();
		hasRope = additionalRope != null;
		if (HasBasicInfo)
		{
			if (basicInfo.infoType == BasicInfo.BasicInfoType.Block)
			{
				healthBar = (basicInfo as BlockBehaviour).BlockHealth;
			}
			else
			{
				healthBar = basicInfo.GetComponent<BlockHealthBar>();
			}
		}
		else
		{
			healthBar = base.gameObject.GetComponentInParent<BlockHealthBar>();
		}
		hasHealthBar = healthBar != null;
		if (overlapType == OverlapType.Default)
		{
			if (myCollider == null)
			{
				myCollider = GetComponent<Collider>();
				myBody = GetComponent<Rigidbody>();
			}
			if (myCollider != null)
			{
				myCollider.enabled = false;
			}
		}
		if (HasBasicInfo)
		{
			basicInfo.UpdateSimState(false);
		}
		if (!base.isSimulating)
		{
			base.enabled = false;
			return;
		}
		lossyScale = new Vector3(Mathf.Abs(base.transform.lossyScale.x), Mathf.Abs(base.transform.lossyScale.y), Mathf.Abs(base.transform.lossyScale.z));
		if (fireParticles != null)
		{
			hasFireParticles = true;
			externalForcesModule = fireParticles.externalForces;
			externalForcesModule.multiplier = 10f;
		}
		hasFireTag = fireTagCode != null;
		bool flag = HasBasicInfo;
		if (HasBasicInfo)
		{
			if (basicInfo.infoType != BasicInfo.BasicInfoType.Block)
			{
				structuralTile = GetComponent<StructuralPhysTile>();
				if (basicInfo is SkinnedInfo)
				{
					renderers = new Renderer[1] { (basicInfo as SkinnedInfo).render };
				}
				switch (basicInfo.infoType)
				{
				case BasicInfo.BasicInfoType.Entity:
					renderers = (basicInfo as GenericEntity).visualController.renderers;
					break;
				case BasicInfo.BasicInfoType.Projectile:
					renderers = new MeshRenderer[1] { basicInfo.MeshRenderer };
					break;
				default:
					flag = false;
					break;
				}
			}
			else
			{
				bvc = (basicInfo as BlockBehaviour).VisualController;
				renderers = bvc.renderers;
			}
		}
		fireLayerMask = -1224290303;
		if (!flag)
		{
			Transform transform = ((overlapType != OverlapType.Default) ? base.transform : base.transform.parent);
			MeshRenderer[] components = transform.GetComponents<MeshRenderer>();
			MeshRenderer[] componentsInChildren = transform.GetComponentsInChildren<MeshRenderer>();
			renderers = components.Concat(componentsInChildren).ToArray();
		}
	}

	protected void OnDrawGizmosSelected()
	{
		FireTrigger componentInChildren = GetComponentInChildren<FireTrigger>();
		if (!(componentInChildren != null) || !(componentInChildren.fireController == this))
		{
			Vector3 vector = base.transform.TransformPoint(overlapCenter);
			Vector3 vector2 = base.transform.lossyScale;
			Gizmos.color = new Color(1f, 0.7f, 0.3f, 1f);
			switch (overlapType)
			{
			case OverlapType.Sphere:
				Gizmos.DrawWireSphere(vector, Mathf.Min(Mathf.Abs(vector2.x), Mathf.Abs(vector2.y), Mathf.Abs(vector2.z)) * overlapRadius);
				break;
			case OverlapType.Box:
			{
				Matrix4x4 matrix = Matrix4x4.TRS(vector, base.transform.rotation, Vector3.one);
				Gizmos.matrix = matrix;
				Gizmos.DrawWireCube(Vector3.zero, new Vector3(vector2.x * overlapSize.x, vector2.y * overlapSize.y, vector2.z * overlapSize.z));
				Gizmos.matrix = Matrix4x4.identity;
				break;
			}
			}
		}
	}

	public void SetFireDuration(float randomTime)
	{
		earlyBurnDuration += randomTime;
		fullFireDuration = earlyBurnDuration + lateBurnDuration;
		hasFullFireDuration = true;
	}

	private void FixedUpdate()
	{
		if (!base.isSimulating)
		{
			return;
		}
		currentTime += Time.deltaTime;
		if (currentTime >= colliderRepeatRate)
		{
			if (this.overlapType == OverlapType.Default)
			{
				StartCoroutine(ColliderPulse());
			}
			else
			{
				Vector3 vector = base.transform.TransformPoint(overlapCenter);
				OverlapType overlapType = this.overlapType;
				if (overlapType == OverlapType.Box)
				{
					hitColliders = Physics.OverlapBox(vector, new Vector3(lossyScale.x * overlapSize.x, lossyScale.y * overlapSize.y, lossyScale.z * overlapSize.z) / 2f, base.transform.rotation, fireLayerMask);
				}
				else
				{
					hitColliders = Physics.OverlapSphere(vector, Mathf.Min(lossyScale.x, lossyScale.y, lossyScale.z) * overlapRadius, fireLayerMask);
				}
				if (randomIgniteChance)
				{
					AttemptIgnite(hitColliders);
				}
				else if (onFire && !disableFire)
				{
					for (int i = 0; i < hitColliders.Length; i++)
					{
						Ignite(hitColliders[i]);
					}
				}
				if (hasRope)
				{
					Vector3 vector2 = additionalRope.up * additionalRope.lossyScale.y;
					hitColliders = Physics.OverlapCapsule(additionalRope.position + vector2, additionalRope.position - vector2, 0.5f, fireLayerMask, QueryTriggerInteraction.Ignore);
				}
			}
			currentTime = 0f;
		}
		else if (randomIgniteChance && this.overlapType != OverlapType.Default)
		{
			AttemptIgnite(hitColliders);
		}
	}

	private void AttemptIgnite(Collider[] hitColliders)
	{
		if (onFire && !disableFire && hitColliders.Length != 0)
		{
			int num = UnityEngine.Random.Range(0, (int)((float)hitColliders.Length / 0.1f));
			if (num < hitColliders.Length && (bool)hitColliders[num])
			{
				Ignite(hitColliders[num]);
			}
		}
	}

	private void Update()
	{
		if (base.isSimulating && hasFireTag && fireTagCode.HasBasicInfo && !fireTagCode.basicInfo.isDestroyed)
		{
			if (hasFireParticles && fireTagCode.basicInfo.inWind && !externalForcesModule.enabled)
			{
				externalForcesModule.enabled = true;
				externalForcesModule.multiplier = 10f;
			}
			else if (hasFireParticles && !fireTagCode.basicInfo.inWind && externalForcesModule.enabled)
			{
				externalForcesModule.enabled = false;
			}
		}
	}

	private void GetStartingAttributes()
	{
		if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block)
		{
			return;
		}
		if (renderers != null && renderers.Length > 0 && renderers[0] != null && renderers[0].material.HasProperty("_RimColor"))
		{
			startRimColor = renderers[0].material.GetColor("_RimColor");
		}
		List<Color> list = new List<Color>();
		List<Renderer> list2 = new List<Renderer>();
		if (renderers == null)
		{
			Debug.LogError("Renderers are null for object " + Machine.GetObjectPath(base.gameObject) + "!");
		}
		else
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				if ((bool)renderers[i] && !renderers[i].gameObject.CompareTag("FireControllerIgnore") && renderers[i].material.HasProperty("_Color") && (renderers[i].material.HasProperty("_EmissCol") || renderers[i].material.HasProperty("_Emission")))
				{
					list2.Add(renderers[i]);
					list.Add(renderers[i].material.color);
				}
			}
		}
		renderers = list2.ToArray();
		startColours = list.ToArray();
		hasStartingAtributes = true;
		if (joints.Length == 0)
		{
			joints = base.gameObject.GetComponents<Joint>();
		}
		multiJointStartBreakForces = new Vector2[joints.Length];
		multiJointMinBreakForces = new Vector2[joints.Length];
		for (int j = 0; j < joints.Length; j++)
		{
			if (!(joints[j] == null))
			{
				multiJointStartBreakForces[j] = new Vector2(joints[j].breakForce, joints[j].breakTorque);
				multiJointMinBreakForces[j] = new Vector2(UnityEngine.Random.Range(0.1f, 1f), UnityEngine.Random.Range(0.1f, 1f));
			}
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if (onFire && !disableFire)
		{
			Ignite(other);
		}
	}

	public void Ignite(Collider other)
	{
		FireTag componentInParent = other.GetComponentInParent<FireTag>();
		if (!object.ReferenceEquals(componentInParent, null) && (!dontSetSelfOnFire || componentInParent != fireTagCode))
		{
			hasSpread = true;
			componentInParent.Ignite(other, burnIntensity);
		}
	}

	public void CatchFire(float intensity)
	{
		if (currentFireDuration >= fullFireDuration || !base.isSimulating || disableFire || base.gameObject.isStatic)
		{
			return;
		}
		if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block)
		{
			if (!catchingFire || onFire)
			{
				intensity = Mathf.Max(burnIntensity, intensity);
				float num = UnityEngine.Random.Range(burningDuration, fullFireDuration) * (1f - intensity) + fullFireDuration * intensity;
				if (num > burningDuration)
				{
					burningDuration = num;
				}
			}
		}
		else
		{
			burningDuration = fullFireDuration;
		}
		if (!hasFireTag || !fireTagCode.burning)
		{
			catchingFire = true;
			catchFire = StartCoroutine(CatchFireIE());
		}
	}

	public void StartStatic()
	{
		PlayParticles();
		if (!onFire)
		{
			base.enabled = base.SimPhysics && !disableFire;
		}
		onFire = true;
	}

	public void StopStatic()
	{
		StopParticles();
		onFire = false;
		base.enabled = false;
	}

	public void PlayParticles()
	{
		if (HasBasicInfo && basicInfo.InWater && basicInfo.submergedPercent >= submergeLimit)
		{
			return;
		}
		if (!isAwake)
		{
			Debug.LogError("Trying to play fire particles before awake on " + base.name);
		}
		if (!hasFireParticles)
		{
			Debug.LogError("Missing fire particles on " + base.name);
		}
		else
		{
			if (!fireParticles.isPlaying)
			{
				fireParticles.randomSeed = (uint)UnityEngine.Random.Range(0f, 9999999f);
			}
			fireParticles.Play();
		}
		for (int i = 0; i < additionalFireParticles.Length; i++)
		{
			if (additionalFireParticles[i] != null)
			{
				additionalFireParticles[i].Play();
			}
		}
	}

	public void StopParticles()
	{
		if (fireParticles != null && fireParticles.isPlaying)
		{
			fireParticles.Stop();
		}
		for (int i = 0; i < additionalFireParticles.Length; i++)
		{
			ParticleSystem particleSystem = additionalFireParticles[i];
			if (particleSystem != null && particleSystem.isPlaying)
			{
				particleSystem.Stop();
			}
		}
	}

	public IEnumerator CatchFireIE()
	{
		for (float t = 0f; t < igniteDelay + baseDelay; t += Time.deltaTime)
		{
			yield return null;
		}
		GetStartingAttributes();
		if (!onFire)
		{
			base.enabled = base.SimPhysics && !disableFire;
			onFire = true;
		}
		PlayParticles();
		bool hasKilled = false;
		int healthIndex = 0;
		while (flameTimer < burningDuration)
		{
			if (hasFireTag && fireTagCode.canBeDoused)
			{
				bool submerged = false;
				if (!HasBasicInfo || StatMaster.GodTools.GravityDisabled)
				{
					submerged = WaterController.Exist && WaterController.IsUnderwater(base.transform.position);
				}
				else if (basicInfo.InWater && basicInfo.submergedPercent >= submergeLimit)
				{
					submerged = true;
				}
				if (submerged)
				{
					if (douseDelay == 0f)
					{
						DouseFire();
					}
					else
					{
						yield return StartCoroutine(DouseFire(douseDelay));
					}
					break;
				}
			}
			if (!hasKilled && flameTimer > earlyBurnDuration)
			{
				SendKillMessage();
				hasKilled = true;
			}
			flameTimer += Time.deltaTime;
			currentFireDuration += Time.deltaTime;
			fireProgress = currentFireDuration / fullFireDuration;
			SetProgressAttributes();
			if (StatMaster.isMP && hasHealthBar && base.SimPhysics)
			{
				bool decreaseHealth = false;
				if (healthIndex != 1 && fireProgress >= 0f)
				{
					decreaseHealth = true;
					healthIndex = 1;
				}
				else if (healthIndex != 2 && fireProgress >= 0.25f)
				{
					decreaseHealth = true;
					healthIndex = 2;
				}
				else if (healthIndex != 3 && fireProgress >= 0.5f)
				{
					decreaseHealth = true;
					healthIndex = 3;
				}
				else if (healthIndex != 4 && fireProgress >= 0.75f)
				{
					decreaseHealth = true;
					healthIndex = 4;
				}
				if (decreaseHealth)
				{
					healthBar.DamageBlock(1f, false);
				}
			}
			yield return null;
		}
		if (StatMaster.isMP && base.HasParentMachine && base.SimPhysics)
		{
			ServerMachine serverMachine = base.ParentMachine as ServerMachine;
			serverMachine.ApplyDamage(base.gameObject.GetComponentInParent<BlockBehaviour>(), MachineDamageType.Ignite);
		}
		if (hasFireTag)
		{
			fireTagCode.burning = false;
		}
		if (base.SimPhysics)
		{
			base.enabled = false;
		}
		onFire = false;
		StopParticles();
		if (base.SimPhysics && fireProgress >= 0.95f)
		{
			SendFireOverMessages();
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(FallOver());
			}
		}
		catchingFire = false;
	}

	private void SetProgressAttributes()
	{
		if (setBreakForce)
		{
			if (base.HasParentMachine)
			{
				if (hasHealthBar && !base.ParentMachine.UnbreakableMode)
				{
					healthBar.SetJointHealth(1f - fireProgress * pctDmgOnBurn);
				}
			}
			else
			{
				for (int i = 0; i < joints.Length; i++)
				{
					if (!(joints[i] == null))
					{
						joints[i].breakForce = Mathf.Lerp(multiJointStartBreakForces[i].x, multiJointMinBreakForces[i].x, fireProgress);
						joints[i].breakTorque = Mathf.Lerp(multiJointStartBreakForces[i].y, multiJointMinBreakForces[i].y, fireProgress);
					}
				}
			}
		}
		if (!object.ReferenceEquals(structuralTile, null))
		{
			structuralTile.BurnJoints(fireProgress);
		}
		if (!object.ReferenceEquals(bvc, null))
		{
			if (displayBurnOnBlock)
			{
				bvc.SetBurnedLevel(fireProgress);
			}
		}
		else
		{
			SetBurnedLevel(fireProgress);
		}
	}

	public void SetBurnedLevel(float pct)
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		materialPropertyBlock.SetColor("_EmissCol", pct * Color.white);
		materialPropertyBlock.SetColor("_RimColor", (1f - pct) * startRimColor);
		for (int i = 0; i < renderers.Length; i++)
		{
			if (!object.ReferenceEquals(renderers[i], null))
			{
				materialPropertyBlock.SetColor("_Color", Color.Lerp(startColours[i], burntColor, pct));
				renderers[i].SetPropertyBlock(materialPropertyBlock);
			}
		}
	}

	public void DouseFire()
	{
		if (StatMaster.isMP)
		{
			if (base.SimPhysics)
			{
				if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block)
				{
					ServerMachine serverMachine = basicInfo.ParentMachine as ServerMachine;
					serverMachine.ApplyDamage(basicInfo as BlockBehaviour, MachineDamageType.Douse);
				}
				if (!StatMaster.IsLevelEditorOnly && base.NetBlock != null)
				{
					base.NetBlock.Event(NetworkEntity.EntityEvent.Douse);
				}
			}
		}
		else if (HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block)
		{
			AchievementHelper.Increment(7, 1);
		}
		burningDuration = 0f;
		SendDousedMessage();
	}

	public IEnumerator DouseFire(float delay)
	{
		if (base.isSimulating && onFire)
		{
			yield return new WaitForSeconds(delay);
			DouseFire();
		}
	}

	public void ImmediateStop()
	{
		if (catchingFire)
		{
			StopCoroutine(catchFire);
			catchingFire = false;
		}
		StopParticles();
	}

	private void SendFireOverMessages()
	{
		Transform transform = ((!HasBasicInfo) ? base.transform : basicInfo.transform);
		BleedOnJointBreak componentInChildren = transform.GetComponentInChildren<BleedOnJointBreak>();
		if (componentInChildren != null)
		{
			componentInChildren.Killed(false);
		}
		GibOnImpact componentInChildren2 = transform.GetComponentInChildren<GibOnImpact>();
		if (componentInChildren2 != null)
		{
			componentInChildren2.Gib();
		}
		BreakOnForce componentInChildren3 = transform.GetComponentInChildren<BreakOnForce>();
		if (componentInChildren3 != null)
		{
			componentInChildren3.Break();
		}
		PhysNodeTile componentInChildren4 = transform.GetComponentInChildren<PhysNodeTile>();
		if (componentInChildren4 != null)
		{
			componentInChildren4.ExplodeFromFire();
		}
	}

	private IEnumerator FallOver()
	{
		if (base.SimPhysics)
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, fallOverDelay));
			if (HasBasicInfo && !basicInfo.noRigidbody)
			{
				Rigidbody parentRigidbody = basicInfo.Rigidbody;
				parentRigidbody.WakeUp();
				parentRigidbody.AddTorque(torqueToAddOnBurn);
			}
		}
	}

	private void Dissolve()
	{
	}

	private void SendKillMessage()
	{
		if (!base.SimPhysics)
		{
			return;
		}
		for (int i = 0; i < sendKillMessage.Length; i++)
		{
			Transform transform = sendKillMessage[i];
			if (transform != null && transform.gameObject != null)
			{
				transform.gameObject.SendMessage("FireKill", SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private void SendDousedMessage()
	{
		if (base.SimPhysics)
		{
			for (int i = 0; i < sendDousedMessage.Length; i++)
			{
				sendDousedMessage[i].gameObject.SendMessage("Doused", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
