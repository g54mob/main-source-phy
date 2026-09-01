using System;
using UnityEngine;

public class HarpoonTrigger : MonoBehaviour
{
	public bool hasController;

	public HarpoonController controller;

	public Rigidbody rb;

	public Transform visual;

	public bool attached;

	public bool attachedBody;

	public float attachTime;

	public Collider harpoonCollider;

	public Collider harpoonTrigger;

	public ConfigurableJoint harpoonJointBase;

	public Transform MPpool;

	public float minVelocityToAttach = 400f;

	public float artificialDrag = 2f;

	[HideInInspector]
	public ConfigurableJoint harpoonJointcurrent;

	public Action OnAttach;

	[HideInInspector]
	public Rigidbody attachedTo;

	private BasicInfo attachedToBasicInfo;

	[HideInInspector]
	public BreakOnForce breakOnForce;

	[HideInInspector]
	public StructuralPhysTile structuralTile;

	private bool hasBreakOnForce;

	private float breakOnForceNeeded;

	private Collider attachedCollider;

	[HideInInspector]
	public bool detaching;

	public RandomSoundController randomSoundController;

	public ParticleSystem[] particleOnCollide;

	[HideInInspector]
	public bool stopSelfPropulsion;

	private EnemyAISimple AISimple;

	private Vector3 originalScale;

	private float reattachTime;

	private void Awake()
	{
		hasController = controller != null;
		if (hasController && controller.isSimulating && !StatMaster.isMP)
		{
			harpoonJointBase.transform.parent = ReferenceMaster.physicsGoalInstance;
		}
	}

	private void Start()
	{
		originalScale = base.transform.localScale;
	}

	private void Update()
	{
		if (!hasController || !controller.isSimulating || !controller.SimPhysics || !attached)
		{
			return;
		}
		if (attachedToBasicInfo != null && (attachedToBasicInfo.isDestroyed || !attachedToBasicInfo.enabled || !attachedToBasicInfo.gameObject.activeInHierarchy))
		{
			controller.Detach();
			return;
		}
		if (attachedCollider == null || !attachedCollider.gameObject.activeInHierarchy)
		{
			controller.Detach();
		}
		if (harpoonJointcurrent == null)
		{
			controller.Detach();
		}
	}

	private void FixedUpdate()
	{
		if (!attached && !stopSelfPropulsion && hasController && controller.SimPhysics)
		{
			rb.AddForceAtPosition(-rb.velocity.normalized * artificialDrag, controller.endRopePoint.position);
		}
	}

	public void ResetVis()
	{
		visual.localPosition = Vector3.zero;
		visual.localRotation = Quaternion.identity;
		base.transform.localScale = originalScale;
		detaching = false;
		if (hasController)
		{
			controller.autoWind = false;
		}
	}

	public void Detach()
	{
		reattachTime = Time.unscaledTime + 1f;
		detaching = true;
		if (harpoonJointcurrent != null)
		{
			UnityEngine.Object.Destroy(harpoonJointcurrent);
		}
		attached = false;
		visual.SetParent(base.transform, true);
		visual.localScale = Vector3.one;
		if ((bool)attachedToBasicInfo)
		{
			if (attachedToBasicInfo.hasAiScript && hasController)
			{
				KillingHandler killingHandler = attachedToBasicInfo.aiEntity.my.killingHandler;
				killingHandler.GettingGibbed = (Action)Delegate.Remove(killingHandler.GettingGibbed, new Action(controller.Detach));
			}
			attachedToBasicInfo.SetGrabbed(false, this);
		}
		if (AISimple != null && hasController)
		{
			EnemyAISimple aISimple = AISimple;
			aISimple.GettingGibbed = (Action)Delegate.Remove(aISimple.GettingGibbed, new Action(controller.Detach));
			AISimple = null;
		}
		attachedToBasicInfo = null;
		if (attachedCollider != null)
		{
			Physics.IgnoreCollision(harpoonCollider, attachedCollider, false);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger || !hasController || !controller.SimPhysics)
		{
			return;
		}
		if (other.gameObject.CompareTag("ArmourTag"))
		{
			controller.Detach();
			return;
		}
		Rigidbody rigidbody = other.attachedRigidbody;
		if (attached || !(rigidbody != controller.Rigidbody) || controller.shouldContract)
		{
			return;
		}
		bool flag = rigidbody == null;
		if (!flag && rigidbody.isKinematic)
		{
			rigidbody = other.attachedRigidbody.transform.parent.GetComponentInParent<Rigidbody>() ?? rigidbody;
			if (rigidbody == null)
			{
				return;
			}
		}
		if (minVelocityToAttach > rb.velocity.sqrMagnitude)
		{
			Debug.Log("Attachment denied due to lacking Velocity: " + rb.velocity.sqrMagnitude);
		}
		else
		{
			if (attachedTo == rigidbody && Time.unscaledTime < reattachTime)
			{
				return;
			}
			float sqrMagnitude = rb.velocity.sqrMagnitude;
			if (sqrMagnitude < 10f)
			{
				return;
			}
			attachedCollider = other;
			Collider[] array = (flag ? other.GetComponentsInChildren<Collider>() : rigidbody.GetComponentsInChildren<Collider>());
			for (int i = 0; i < array.Length; i++)
			{
				Physics.IgnoreCollision(harpoonCollider, array[i], true);
			}
			attached = true;
			attachTime = Time.fixedTime;
			attachedTo = rigidbody;
			harpoonJointcurrent = base.gameObject.AddComponent<ConfigurableJoint>();
			harpoonJointcurrent.anchor = harpoonJointBase.anchor;
			harpoonJointcurrent.axis = harpoonJointBase.axis;
			harpoonJointcurrent.enablePreprocessing = harpoonJointBase.enablePreprocessing;
			harpoonJointcurrent.enableCollision = harpoonJointBase.enableCollision;
			harpoonJointcurrent.breakForce = controller.jointStrength;
			harpoonJointcurrent.breakTorque = controller.jointStrength;
			harpoonJointcurrent.highAngularXLimit = harpoonJointBase.highAngularXLimit;
			harpoonJointcurrent.lowAngularXLimit = harpoonJointBase.lowAngularXLimit;
			harpoonJointcurrent.angularYLimit = harpoonJointBase.angularYLimit;
			harpoonJointcurrent.angularZLimit = harpoonJointBase.angularZLimit;
			harpoonJointcurrent.angularXLimitSpring = harpoonJointBase.angularXLimitSpring;
			harpoonJointcurrent.angularYZLimitSpring = harpoonJointBase.angularYZLimitSpring;
			harpoonJointcurrent.xMotion = harpoonJointBase.xMotion;
			harpoonJointcurrent.yMotion = harpoonJointBase.yMotion;
			harpoonJointcurrent.zMotion = harpoonJointBase.zMotion;
			harpoonJointcurrent.angularXMotion = harpoonJointBase.angularXMotion;
			harpoonJointcurrent.angularYMotion = harpoonJointBase.angularYMotion;
			harpoonJointcurrent.angularZMotion = harpoonJointBase.angularZMotion;
			harpoonJointcurrent.connectedBody = attachedTo;
			attachedBody = attachedTo != null;
			if (attachedBody)
			{
				if (controller.negateNaturalForce)
				{
					rb.velocity = attachedTo.velocity;
				}
				breakOnForce = attachedTo.GetComponent<BreakOnForce>();
				if (breakOnForce != null)
				{
					breakOnForceNeeded = breakOnForce.ForceToBreak;
				}
				else
				{
					structuralTile = attachedTo.GetComponent<StructuralPhysTile>();
					if (structuralTile != null)
					{
						breakOnForceNeeded = structuralTile.destroyThreshold;
					}
				}
				hasBreakOnForce = breakOnForce != null || structuralTile != null;
				attachedToBasicInfo = attachedTo.GetComponent<BasicInfo>();
				if (attachedToBasicInfo != null)
				{
					attachedToBasicInfo.SetGrabbed(true, this);
					if (attachedToBasicInfo.hasAiScript)
					{
						KillingHandler killingHandler = attachedToBasicInfo.aiEntity.my.killingHandler;
						killingHandler.GettingGibbed = (Action)Delegate.Combine(killingHandler.GettingGibbed, new Action(controller.Detach));
						float damage = Mathf.Sqrt(sqrMagnitude) * (1f + Mathf.Clamp(attachedToBasicInfo.aiEntity.my.killingHandler.maxHealth * 0.002f, 0f, 3f));
						attachedToBasicInfo.aiEntity.my.killingHandler.TakeDamage(damage, InjuryType.Sharp);
					}
				}
				AISimple = attachedTo.GetComponent<EnemyAISimple>();
				if (AISimple != null)
				{
					EnemyAISimple aISimple = AISimple;
					aISimple.GettingGibbed = (Action)Delegate.Combine(aISimple.GettingGibbed, new Action(controller.Detach));
					float damage2 = Mathf.Clamp(AISimple.maxHealth / 2f, 0f, 500f);
					AISimple.TakeDamage(damage2, InjuryType.Sharp);
					if (AISimple != null)
					{
						visual.SetParent(AISimple.visObject, true);
					}
				}
			}
			if (randomSoundController != null)
			{
				randomSoundController.Play();
			}
			if (particleOnCollide != null)
			{
				particleOnCollide[UnityEngine.Random.Range(0, particleOnCollide.Length - 1)].Play();
			}
			OnAttach();
		}
	}

	public void CheckForBreak(Vector3 force)
	{
		if (hasBreakOnForce && hasController && force.magnitude * controller.breakPullPower > breakOnForceNeeded && (!StatMaster.isMP || attachedToBasicInfo.transform.IsChildOf(ReferenceMaster.physicsGoalInstance)))
		{
			if (breakOnForce != null)
			{
				breakOnForce.BreakExplosion(200f, base.transform.position, 6f, 0f);
			}
			else if (structuralTile != null)
			{
				structuralTile.DestroyTile(force);
			}
		}
	}
}
