using System;
using UnityEngine;

public class BreakOnForceBoulder : SimBehaviour, IExplosionEffect
{
	public Transform BreakInto;

	public Transform BrokenInstance;

	public float ForceToBreak = 2f;

	public bool CanDie = true;

	public Transform BreakParent;

	public float forcePower = 200f;

	public float forceRadius = 6f;

	public Renderer vis;

	public bool sleepOnEnable = true;

	public bool rotateSpawnedObject = true;

	public bool usingBreakableSkin = true;

	protected Rigidbody myBody;

	private BlockVisualController bvc;

	protected override void Start()
	{
		base.Start();
		bvc = base.gameObject.GetComponent<BlockVisualController>();
		if ((bool)bvc && bvc.selectedSkin != null)
		{
			usingBreakableSkin = bvc.selectedSkin == bvc.Prefab.DefaultSkin;
		}
		if (base.SimPhysics)
		{
			myBody = GetComponent<Rigidbody>();
			if (sleepOnEnable)
			{
				myBody.Sleep();
			}
		}
	}

	private void OnEnable()
	{
		if (base.SimPhysics && sleepOnEnable)
		{
			myBody = GetComponent<Rigidbody>();
			myBody.Sleep();
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (CanDie && base.isSimulating && collision.relativeVelocity.magnitude > ForceToBreak)
		{
			Break();
		}
	}

	public bool OnExplode(float power, float upPower, float torquePower, Vector3 explosionPos, float radius, int mask, bool inWater)
	{
		if (!base.isSimulating || !base.SimPhysics)
		{
			return false;
		}
		if ((mask & 0x80) != 0)
		{
			BreakExplosion(power, explosionPos, radius, upPower);
			return true;
		}
		return false;
	}

	protected void SetParent(Transform breakObj)
	{
		if (!StatMaster.isMP)
		{
			breakObj.parent = ReferenceMaster.physicsGoalInstance;
		}
		else
		{
			breakObj.parent = base.transform.parent;
		}
	}

	protected Quaternion GetBreakRotation()
	{
		Quaternion result = base.transform.rotation;
		if (!rotateSpawnedObject)
		{
			result = Quaternion.LookRotation(Vector3.forward, Vector3.up);
		}
		return result;
	}

	public Transform Break()
	{
		if (!CanDie || !usingBreakableSkin || !base.enabled)
		{
			return null;
		}
		CanDie = false;
		if (BreakInto == null)
		{
			Debug.LogWarning("BreakInto is null!");
			return null;
		}
		BrokenInstance = (Transform)UnityEngine.Object.Instantiate(BreakInto, vis.transform.position, vis.transform.rotation);
		if (BrokenInstance == null)
		{
			return null;
		}
		SetParent(BrokenInstance);
		AddChildrenForces(BrokenInstance);
		AddToPercentageBar();
		SendBreakEvent();
		base.gameObject.SetActive(false);
		return BrokenInstance;
	}

	protected void SendBreakEvent()
	{
		if (StatMaster.isMP && base.SimPhysics)
		{
			if (base.NetBlock != null)
			{
				base.NetBlock.Event(NetworkEntity.EntityEvent.Break);
			}
			else
			{
				Debug.LogError("Missing NetworkBlock on '" + Machine.GetObjectPath(base.gameObject) + "'? " + Environment.StackTrace, base.gameObject);
			}
		}
	}

	protected Transform BreakExplosion(float powery, Vector3 position, float radiusy, float upAmount)
	{
		Transform transform = Break();
		if (transform == null)
		{
			return null;
		}
		AddExplosionForces(transform, powery, position, radiusy, upAmount);
		return transform;
	}

	protected void AddChildrenForces(Transform obj)
	{
		if (!base.SimPhysics || myBody == null)
		{
			return;
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			Transform child = obj.GetChild(i);
			if (child.name == "Frag")
			{
				Rigidbody component = child.GetComponent<Rigidbody>();
				component.AddForce(myBody.velocity * 0.5f, ForceMode.VelocityChange);
				component.AddTorque(myBody.angularVelocity * 0.5f, ForceMode.VelocityChange);
			}
		}
	}

	protected void AddExplosionForces(Transform obj, float powery, Vector3 position, float radiusy, float upAmount)
	{
		if (!base.SimPhysics)
		{
			return;
		}
		for (int i = 0; i < obj.childCount; i++)
		{
			Transform child = obj.GetChild(i);
			if (child.name == "Frag")
			{
				Rigidbody component = child.GetComponent<Rigidbody>();
				component.AddExplosionForce(powery * 0.5f, position, radiusy, upAmount * 2f);
			}
		}
	}

	private void AddToPercentageBar()
	{
		if (!StatMaster.isMP && base.gameObject.CompareTag("ObjectiveObj"))
		{
			WinCondition.currentObjsCompleted++;
		}
	}
}
