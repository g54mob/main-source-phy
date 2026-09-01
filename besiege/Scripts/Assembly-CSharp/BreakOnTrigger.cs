using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Destruction/Break On Trigger")]
public class BreakOnTrigger : SimBehaviour, IExplosionEffect
{
	public Transform BreakInto;

	public float ForceToBreak = 2f;

	public int numberOfTimes = 1;

	public bool CanDie = true;

	public Transform BreakParent;

	public bool usePhysicsGoalAsParent = true;

	public Transform BrokenInstance;

	public Vector3 offsetRotation = Vector3.zero;

	public Rigidbody myBody;

	public List<Joint> jointToBreak = new List<Joint>();

	public bool breakIfJointIsExternallyBroken = true;

	public float breakPower = 200f;

	public float breakForceRadius = 6f;

	public Renderer visCopyMaterialFrom;

	public bool handleOnExplode = true;

	public ExplosiveProperty explosiveProperty = ExplosiveProperty.ForceBreaking;

	public float impactDragDuration;

	protected CustomLevel level;

	protected bool isBroken;

	protected bool initDone;

	protected float forceToBreakSqr;

	[Header("Victory progression")]
	[SerializeField]
	protected int victoryValue = 1;

	public static HashSet<BasicInfo> slowedTargets = new HashSet<BasicInfo>();

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
		if (base.SimPhysics && (bool)myBody)
		{
			myBody.Sleep();
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.enabled || !handleOnExplode || !base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & ReferenceMaster.EnumToInt((int)explosiveProperty)) != 0)
		{
			BreakExplosion(power, explosionPos, radius, upPower);
			return true;
		}
		return false;
	}

	protected virtual void OnEnable()
	{
		if (!base.SimPhysics)
		{
			return;
		}
		if (myBody != null)
		{
			myBody.Sleep();
			return;
		}
		myBody = GetComponent<Rigidbody>();
		if (myBody != null)
		{
			myBody.Sleep();
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
		isBroken = false;
		initDone = true;
	}

	protected virtual void OnTriggerEnter(Collider collider)
	{
		if (!base.enabled || !base.SimPhysics || !base.isSimulating || !CanDie || !initDone)
		{
			return;
		}
		Rigidbody attachedRigidbody = collider.attachedRigidbody;
		bool flag = attachedRigidbody != null;
		if ((flag && attachedRigidbody.CompareTag("IgnoreBreakCollision")) || collider.gameObject.layer == 2)
		{
			return;
		}
		float num = ((!flag) ? myBody.velocity.sqrMagnitude : attachedRigidbody.velocity.sqrMagnitude);
		if (!(num > forceToBreakSqr))
		{
			return;
		}
		numberOfTimes--;
		if (numberOfTimes != 0)
		{
			return;
		}
		BreakExplosion(breakPower, collider.transform.position, breakForceRadius, 0f);
		if (impactDragDuration > 0f)
		{
			BasicInfo component = attachedRigidbody.GetComponent<BasicInfo>();
			if ((bool)component)
			{
				ReferenceMaster.Instance.StartCoroutine(SlowTarget(component, impactDragDuration));
			}
		}
	}

	private IEnumerator SlowTarget(BasicInfo info, float duration)
	{
		if (!slowedTargets.Contains(info))
		{
			slowedTargets.Add(info);
			Rigidbody r = info.Rigidbody;
			float org = info.waterDragMulti;
			float drag = r.drag - info.WaterDrag;
			r.drag = 50f / r.mass + info.WaterDrag;
			info.waterDragMulti = 10f;
			yield return new WaitForSeconds(duration);
			if ((bool)info)
			{
				r.drag = drag + info.WaterDrag;
				info.waterDragMulti = org;
				slowedTargets.Remove(info);
			}
		}
	}

	private void OnDestroy()
	{
		slowedTargets.Clear();
	}

	protected void Update()
	{
		if (!breakIfJointIsExternallyBroken)
		{
			return;
		}
		for (int i = 0; i < jointToBreak.Count; i++)
		{
			if (jointToBreak[i] == null)
			{
				ExternalBreak();
				break;
			}
		}
	}

	protected virtual void SetParent(Transform breakObj)
	{
		breakObj.parent = ((!usePhysicsGoalAsParent) ? BreakParent : ReferenceMaster.physicsGoalInstance);
		if ((bool)breakObj.GetComponent<Rigidbody>())
		{
			breakObj.localScale = ((!usePhysicsGoalAsParent) ? base.transform.localScale : base.transform.lossyScale);
			return;
		}
		Vector3 localScale = ((!(myBody != null)) ? base.transform : myBody.transform).localScale;
		for (int i = 0; i < breakObj.childCount; i++)
		{
			breakObj.GetChild(i).localScale = localScale;
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
		if (BreakInto == null)
		{
			Debug.LogWarning("BreakInto is null (" + Machine.GetObjectPath(base.gameObject) + ")!");
			return null;
		}
		BrokenInstance = UnityEngine.Object.Instantiate(BreakInto, base.transform.position, GetBreakRotation()) as Transform;
		if (BrokenInstance == null)
		{
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
		SetParent(BrokenInstance);
		if (visCopyMaterialFrom != null)
		{
			CopyMaterial component = BrokenInstance.GetComponent<CopyMaterial>();
			if (component != null)
			{
				component.CopyMat(visCopyMaterialFrom);
			}
		}
		AddToPercentageBar();
		BreakJointConnections();
		base.gameObject.SetActive(false);
		SendBreakEvent();
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

	protected virtual void BreakJointConnections()
	{
		for (int i = 0; i < jointToBreak.Count; i++)
		{
			if (jointToBreak[i] != null)
			{
				UnityEngine.Object.Destroy(jointToBreak[i]);
			}
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

	public void ExternalBreak()
	{
		BreakExplosion(breakPower, -base.transform.up, breakForceRadius, 0f);
	}

	protected void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted += victoryValue;
		}
	}
}
