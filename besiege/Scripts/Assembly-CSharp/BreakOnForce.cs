using System;
using UnityEngine;

[AddComponentMenu("Destruction/Break On Force")]
public class BreakOnForce : BreakBase, IExplosionEffect
{
	public Transform BreakInto;

	public Transform BreakParent;

	[HideInInspector]
	public Transform BrokenInstance;

	[Header("Options")]
	public bool usePhysicsGoalAsParent = true;

	public bool CanDie = true;

	public bool shouldSleepOnStart = true;

	public bool breakOnJointSnap;

	public bool jointProgress;

	public bool useLossyScale;

	public bool alwaysScaleParent;

	public bool scaleSubpositions;

	[Header("Values")]
	public float ForceToBreak = 2f;

	public float breakPower = 200f;

	public float breakForceRadius = 6f;

	public Vector3 offsetRotation = Vector3.zero;

	public Vector3 offsetPosition = Vector3.zero;

	public float damageBlocks;

	public bool blockVelocityDmg;

	public bool breakEasierWithSharp;

	[Header("Explosivity")]
	public bool handleOnExplode = true;

	public ExplosiveProperty explosiveProperty = ExplosiveProperty.ForceBreaking;

	[Header("References")]
	public Rigidbody myBody;

	public Renderer visCopyMaterialFrom;

	public CollisionHook colHook;

	[Header("Cross References")]
	public BasicInfo[] supportedBy;

	public Rigidbody[] objsImSupporting;

	protected BasicInfo[] supportedInfos = new BasicInfo[0];

	public GameObject[] objsToDestroy;

	public BreakOnForce[] nestedBreaks;

	public GameObject breakCallback;

	protected CustomLevel level;

	[HideInInspector]
	public bool isBroken;

	protected bool initDone;

	protected float forceToBreakSqr;

	[Header("Victory progression")]
	[SerializeField]
	protected int victoryValue = 1;

	public Action victoryTriggered;

	public bool addVictoryValue;

	private bool brokenJoint;

	public override Vector3 Center()
	{
		if (base.SimPhysics)
		{
			return myBody.worldCenterOfMass;
		}
		return base.Center();
	}

	protected override void Awake()
	{
		base.Awake();
		if (BreakParent == null)
		{
			usePhysicsGoalAsParent = true;
		}
	}

	protected override void Start()
	{
		base.Start();
		Init();
		if (base.SimPhysics && (bool)myBody && shouldSleepOnStart)
		{
			myBody.Sleep();
		}
	}

	protected void Update()
	{
		if (HasBasicInfo && basicInfo.isSimulating && !CheckIfSupported() && !basicInfo.noRigidbody)
		{
			basicInfo.IgnoredByWater = false;
			basicInfo.isKinematic = false;
			basicInfo.Rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
			basicInfo.Rigidbody.isKinematic = false;
			basicInfo.Rigidbody.WakeUp();
			basicInfo.Rigidbody.AddTorque(10f, 10f, 10f);
		}
	}

	public bool OnExplode(GameObject hit, float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		return OnExplode(power, upPower, torquePower, explosionPos, radius, mask, inWater);
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.enabled || !handleOnExplode || !base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & ReferenceMaster.EnumToInt((int)explosiveProperty)) != 0)
		{
			if (inWater)
			{
				float sqrMagnitude = (explosionPos - base.transform.position).sqrMagnitude;
				if (sqrMagnitude > 100f)
				{
					return false;
				}
			}
			BreakExplosion(power, explosionPos, radius, upPower);
			return true;
		}
		return false;
	}

	protected virtual void OnEnable()
	{
		if (base.SimPhysics)
		{
			if (myBody != null && shouldSleepOnStart)
			{
				myBody.Sleep();
			}
			else
			{
				myBody = GetComponent<Rigidbody>();
				if (myBody != null && shouldSleepOnStart)
				{
					myBody.Sleep();
				}
			}
		}
		if ((bool)colHook)
		{
			colHook.CollisionHappend += OnCollisionEnter;
			colHook.ExplosionHappend += OnExplode;
		}
	}

	protected virtual void OnDisable()
	{
		if ((bool)colHook)
		{
			colHook.CollisionHappend -= OnCollisionEnter;
			colHook.ExplosionHappend -= OnExplode;
		}
	}

	protected void Init()
	{
		if (initDone)
		{
			return;
		}
		level = CustomLevel.Instance;
		forceToBreakSqr = ForceToBreak * ForceToBreak;
		if (base.SimPhysics && myBody == null)
		{
			myBody = GetComponent<Rigidbody>();
			if (myBody == null)
			{
				Debug.LogError(base.transform.name + " has no Rigidbody on BOF script");
				base.enabled = false;
				return;
			}
		}
		if (base.isSimulating)
		{
			supportedInfos = new BasicInfo[objsImSupporting.Length];
			int num = 0;
			for (int i = 0; i < objsImSupporting.Length; i++)
			{
				if (objsImSupporting[i] != null)
				{
					BasicInfo component = objsImSupporting[i].GetComponent<BasicInfo>();
					if ((bool)component)
					{
						supportedInfos[num] = component;
						num++;
					}
				}
			}
			if (num == 0)
			{
				supportedInfos = new BasicInfo[0];
			}
			else if (num < objsImSupporting.Length)
			{
				Array.Copy(supportedInfos, supportedInfos, num);
			}
		}
		isBroken = false;
		initDone = true;
	}

	protected virtual void OnCollisionEnter(Collision collision)
	{
		if (!base.enabled || !base.SimPhysics || !base.isSimulating || !CanDie || !initDone || ReferenceMaster.IgnoreBreakCollisions.Contains(collision.gameObject))
		{
			return;
		}
		float num = collision.relativeVelocity.sqrMagnitude;
		if (breakEasierWithSharp && collision.collider.gameObject.layer == 26)
		{
			num *= 2f;
		}
		if (!(num > forceToBreakSqr))
		{
			return;
		}
		Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;
		bool flag = attachedRigidbody != null;
		if (damageBlocks > 0f && flag)
		{
			BasicInfo component = attachedRigidbody.GetComponent<BasicInfo>();
			if ((bool)component)
			{
				BasicInfo.BasicInfoType infoType = component.infoType;
				if (infoType == BasicInfo.BasicInfoType.Block)
				{
					BlockBehaviour blockBehaviour = component as BlockBehaviour;
					if (blockBehaviour.Prefab.hasHealthBar)
					{
						if (blockBehaviour.BlockHealth.health > 0f)
						{
							blockBehaviour.BlockHealth.DamageBlock((!blockVelocityDmg) ? damageBlocks : (damageBlocks * num * 0.01f));
						}
						else
						{
							foreach (Joint item in blockBehaviour.jointsToMe)
							{
								if ((bool)item)
								{
									float breakForce = (item.breakTorque = 0f);
									item.breakForce = breakForce;
								}
							}
						}
					}
				}
			}
		}
		if (collision.contacts.Length > 0)
		{
			Vector3 point = collision.contacts[0].point;
			if (flag)
			{
				point += -attachedRigidbody.velocity * Time.fixedDeltaTime * 2f;
			}
			BreakExplosion(breakPower, point, breakForceRadius, 0f);
			DebugExtension.DebugWireSphere(point, Color.yellow, 1f, 2f);
		}
		else if (flag)
		{
			Vector3 worldCenterOfMass = attachedRigidbody.worldCenterOfMass;
			BreakExplosion(breakPower, worldCenterOfMass, breakForceRadius, 0f);
			DebugExtension.DebugWireSphere(worldCenterOfMass, Color.red, 1f, 2f);
		}
		else
		{
			Break();
		}
	}

	private void OnJointBreak(float force)
	{
		if (!brokenJoint)
		{
			if (jointProgress)
			{
				AddToPercentageBar();
			}
			brokenJoint = true;
		}
		if (!isBroken && breakOnJointSnap)
		{
			Break();
		}
	}

	protected virtual void SetParent(Transform breakObj)
	{
		breakObj.parent = ((!usePhysicsGoalAsParent) ? BreakParent : ReferenceMaster.physicsGoalInstance);
		bool flag = usePhysicsGoalAsParent && StatMaster.isMP && useLossyScale;
		if (alwaysScaleParent || (bool)breakObj.GetComponent<Rigidbody>())
		{
			Vector3 localScale = ((!flag) ? base.transform.localScale : base.transform.lossyScale);
			breakObj.localScale = localScale;
			return;
		}
		Transform transform = ((!colHook || !(myBody != null)) ? base.transform : myBody.transform);
		Vector3 vector = ((!flag) ? transform.localScale : transform.lossyScale);
		for (int i = 0; i < breakObj.childCount; i++)
		{
			Transform child = breakObj.GetChild(i);
			child.localScale = vector;
			if (scaleSubpositions)
			{
				child.localPosition = Vector3.Scale(child.localPosition, vector);
			}
		}
	}

	protected virtual Quaternion GetBreakRotation()
	{
		return base.transform.rotation * Quaternion.Euler(offsetRotation);
	}

	public virtual Transform BreakObj()
	{
		if (!CanDie || !base.enabled)
		{
			return null;
		}
		Init();
		isBroken = true;
		CanDie = false;
		if (HasBasicInfo)
		{
			basicInfo.isDestroyed = true;
		}
		if (BreakInto == null)
		{
			Debug.LogWarning("BreakInto is null (" + Machine.GetObjectPath(base.gameObject) + ")!");
			return null;
		}
		BrokenInstance = UnityEngine.Object.Instantiate(BreakInto, base.transform.position + base.transform.TransformVector(offsetPosition), GetBreakRotation()) as Transform;
		if (BrokenInstance == null)
		{
			OnBreak();
			return null;
		}
		ParticleSystem[] componentsInChildren = BrokenInstance.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (StatMaster.isMP && NetworkBlock.applyingState)
			{
				UnityEngine.Object.Destroy(componentsInChildren[i].gameObject);
			}
			else if (componentsInChildren[i].playOnAwake)
			{
				componentsInChildren[i].Stop();
				componentsInChildren[i].Clear();
				componentsInChildren[i].randomSeed = (uint)UnityEngine.Random.Range(0, 9999999);
				componentsInChildren[i].Play();
			}
		}
		DropSupports();
		SetParent(BrokenInstance);
		if (visCopyMaterialFrom != null)
		{
			CopyMaterial component = BrokenInstance.GetComponent<CopyMaterial>();
			if (component != null)
			{
				component.CopyMat(visCopyMaterialFrom);
			}
		}
		if (!jointProgress || !brokenJoint)
		{
			AddToPercentageBar();
			brokenJoint = true;
		}
		DestroyObjects();
		if (colHook != null)
		{
			colHook.gameObject.SetActive(false);
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		SendBreakEvent();
		OnBreak();
		if ((bool)breakCallback)
		{
			breakCallback.SendMessage("Break", SendMessageOptions.DontRequireReceiver);
		}
		return BrokenInstance;
	}

	protected virtual void SendBreakEvent()
	{
		if (!base.SimPhysics)
		{
			return;
		}
		if (base.HasParentMachine)
		{
			ServerMachine serverMachine = base.ParentMachine as ServerMachine;
			BlockBehaviour componentInParent = base.gameObject.GetComponentInParent<BlockBehaviour>();
			if (componentInParent != null)
			{
				serverMachine.ApplyDamage(componentInParent, MachineDamageType.Break);
			}
		}
		if (StatMaster.isMP)
		{
			NetworkBlock netBlock = base.NetBlock;
			if (netBlock != null)
			{
				netBlock.Event(NetworkEntity.EntityEvent.Break);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	protected virtual void DestroyObjects()
	{
		for (int i = 0; i < nestedBreaks.Length; i++)
		{
			BreakOnForce breakOnForce = nestedBreaks[i];
			if (breakOnForce != null)
			{
				breakOnForce.Break();
			}
		}
		for (int j = 0; j < objsToDestroy.Length; j++)
		{
			GameObject gameObject = objsToDestroy[j];
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
	}

	public virtual void Drill(float force)
	{
		if (force > ForceToBreak)
		{
			Break();
		}
	}

	public virtual void Break()
	{
		if (!CanDie || isBroken || !base.enabled)
		{
			return;
		}
		Transform transform = BreakObj();
		if (!base.SimPhysics || !(transform != null))
		{
			return;
		}
		if (myBody != null)
		{
			InheritForce component = transform.GetComponent<InheritForce>();
			if ((bool)component)
			{
				component.forceToAdd = myBody.velocity;
				component.torqueToAdd = myBody.angularVelocity;
				component.AddForce();
			}
		}
		else
		{
			Debug.LogError("Couldn't inherit force in BoF, body is null!");
		}
	}

	public virtual void BreakExplosion(float powery, Vector3 position, float radiusy, float upAmount)
	{
		if (!CanDie || isBroken || !base.enabled)
		{
			return;
		}
		Transform transform = BreakObj();
		if (base.SimPhysics && transform != null)
		{
			InheritExplosion component = transform.GetComponent<InheritExplosion>();
			if ((bool)component)
			{
				component.InheritForce(powery, position, radiusy, upAmount);
			}
		}
	}

	protected bool CheckIfSupported()
	{
		if (supportedBy.Length == 0)
		{
			return true;
		}
		for (int i = 0; i < supportedBy.Length; i++)
		{
			BasicInfo basicInfo = supportedBy[i];
			if (basicInfo != null && !basicInfo.isDestroyed && !basicInfo.noRigidbody && basicInfo.gameObject.activeInHierarchy)
			{
				return true;
			}
		}
		return false;
	}

	protected void DropSupports()
	{
		for (int i = 0; i < supportedInfos.Length; i++)
		{
			BasicInfo basicInfo = supportedInfos[i];
			if (!basicInfo)
			{
				continue;
			}
			basicInfo.enabled = true;
			bool flag = !basicInfo.noRigidbody && basicInfo.Rigidbody != null;
			if (basicInfo.isDestroyed)
			{
				continue;
			}
			Rigidbody rigidbody = basicInfo.Rigidbody;
			Transform transform = ((!flag) ? basicInfo.transform : rigidbody.transform);
			if (basicInfo.hasAiScript && flag)
			{
				basicInfo.aiEntity.SetDynamic();
				continue;
			}
			if (transform.parent == base.transform || StatMaster.isMP)
			{
				transform.parent = ReferenceMaster.physicsGoalInstance;
				basicInfo.IgnoredByWater = false;
				if (flag)
				{
					rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
				}
			}
			basicInfo.isKinematic = false;
			if (flag)
			{
				rigidbody.isKinematic = false;
				rigidbody.WakeUp();
				rigidbody.AddTorque(10f, 10f, 10f);
			}
		}
	}

	public void ExternalBreak()
	{
		if (myBody != null && !myBody.isKinematic && myBody.velocity.sqrMagnitude > breakPower)
		{
			Break();
		}
		else
		{
			BreakExplosion(breakPower, -base.transform.up, breakForceRadius, 0f);
		}
	}

	protected void AddToPercentageBar()
	{
		if (!StatMaster.isMP)
		{
			if (victoryTriggered != null)
			{
				victoryTriggered();
			}
			else if (base.gameObject.CompareTag("ObjectiveObj") || addVictoryValue)
			{
				WinCondition.currentObjsCompleted += victoryValue;
			}
		}
	}

	public override void OnBreak()
	{
		base.OnBreak();
		if (isBroken && OnBreakTrigger != null)
		{
			OnBreakTrigger(this);
		}
	}
}
