using System;
using UnityEngine;

public class FruitOfLife : SimBehaviour, IExplosionEffect
{
	public SkinnedMeshRenderer[] fruitRenderers;

	public GameObject[] childrenToEnable;

	public GameObject splatterTexture;

	private bool hitMovingEnvironment;

	public Transform BreakInto;

	public float ForceToBreak = 2f;

	public bool CanDie = true;

	public Transform BreakParent;

	public bool usePhysicsGoalAsParent = true;

	public Transform BrokenInstance;

	public Vector3 offsetRotation = Vector3.zero;

	public CollisionHook colHook;

	public Rigidbody myBody;

	private Vector3 startRotation;

	private Vector3 hitPoint;

	public Rigidbody[] objsImSupporting;

	public GameObject[] objsToDestroy;

	public float breakPower = 200f;

	public float breakForceRadius = 6f;

	public Renderer visCopyMaterialFrom;

	public bool handleOnExplode = true;

	public ExplosiveProperty explosiveProperty = ExplosiveProperty.ForceBreaking;

	protected CustomLevel level;

	protected bool isBroken;

	protected bool initDone;

	protected float forceToBreakSqr;

	protected override void Awake()
	{
		base.Awake();
		if (BreakParent == null)
		{
			usePhysicsGoalAsParent = true;
		}
		if ((bool)colHook)
		{
			colHook.CollisionHappend += OnCollisionEnter;
			colHook.ExplosionHappend += OnExplode;
		}
	}

	protected override void Start()
	{
		startRotation = base.transform.parent.rotation.eulerAngles;
		base.Start();
		Init();
		if (base.SimPhysics && (bool)myBody)
		{
			myBody.Sleep();
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

	protected virtual void OnCollisionEnter(Collision collision)
	{
		hitPoint = collision.contacts[0].point;
		if (collision.gameObject.tag == "MovingEnvironment")
		{
			hitMovingEnvironment = true;
		}
		if (base.enabled && base.SimPhysics && base.isSimulating && CanDie && initDone && collision.relativeVelocity.sqrMagnitude > forceToBreakSqr)
		{
			if (collision.contacts.Length > 0)
			{
				BreakExplosion(breakPower, collision.contacts[0].point, breakForceRadius, 0f);
			}
			else
			{
				BreakExplosion(breakPower, collision.collider.transform.position, breakForceRadius, 0f);
			}
		}
	}

	protected virtual void SetParent(Transform breakObj)
	{
		breakObj.parent = ((!usePhysicsGoalAsParent) ? BreakParent : ReferenceMaster.physicsGoalInstance);
		if ((bool)breakObj.GetComponent<Rigidbody>())
		{
			breakObj.localScale = base.transform.localScale;
			return;
		}
		Vector3 localScale = ((!colHook || !(myBody != null)) ? base.transform : myBody.transform).localScale;
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
		Debug.Log("Boop");
		isBroken = true;
		CanDie = false;
		SkinnedMeshRenderer[] array = fruitRenderers;
		foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
		{
			skinnedMeshRenderer.enabled = false;
		}
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().useGravity = false;
		base.transform.rotation = Quaternion.Euler(startRotation);
		GameObject[] array2 = childrenToEnable;
		foreach (GameObject gameObject in array2)
		{
			if (hitMovingEnvironment && gameObject.name == splatterTexture.name)
			{
				return null;
			}
			gameObject.transform.position = hitPoint + new Vector3(0f, 0.1f, 0f);
			gameObject.SetActive(true);
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
		for (int i = 0; i < objsToDestroy.Length; i++)
		{
			GameObject gameObject = objsToDestroy[i];
			if (gameObject != null)
			{
				UnityEngine.Object.Destroy(gameObject);
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

	protected void DropSupports()
	{
		for (int i = 0; i < objsImSupporting.Length; i++)
		{
			Rigidbody rigidbody = objsImSupporting[i];
			if (rigidbody != null)
			{
				rigidbody.isKinematic = false;
				rigidbody.WakeUp();
				rigidbody.AddTorque(10f, 10f, 10f);
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
			WinCondition.currentObjsCompleted++;
		}
	}
}
