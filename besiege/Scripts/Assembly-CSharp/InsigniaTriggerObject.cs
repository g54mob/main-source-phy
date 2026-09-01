using UnityEngine;

public class InsigniaTriggerObject : SimBehaviour
{
	public InsigniaTrigger trigger;

	public Rigidbody body;

	public Collider triggerObj;

	private bool simPhys;

	protected override void Start()
	{
		base.Start();
		simPhys = base.SimPhysics;
		if (base.isSimulating && !simPhys)
		{
			Clear();
		}
	}

	protected void OnEnable()
	{
	}

	public void Toggle(bool toggle)
	{
		if (simPhys && triggerObj != null)
		{
			triggerObj.enabled = toggle;
		}
	}

	public void Clear()
	{
		if (body != null)
		{
			Object.Destroy(body);
		}
		Object.Destroy(triggerObj);
	}

	public void OnTriggerEnter(Collider other)
	{
		if (base.isSimulating && base.SimPhysics && !other.isTrigger && !StatMaster.IgnoreLevelTriggerResults)
		{
			if (trigger != null)
			{
				trigger.TriggerEnter(other);
			}
			else
			{
				Debug.LogError("OnTriggerEnter: Trying to transmit TriggerEnter to trigger that doesn't exist!");
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (base.isSimulating && base.SimPhysics && !other.isTrigger && !StatMaster.IgnoreLevelTriggerResults)
		{
			if (trigger != null)
			{
				trigger.TriggerExit(other);
			}
			else
			{
				Debug.LogError("OnTriggerExit: Trying to transmit TriggerExit to trigger that doesn't exist!");
			}
		}
	}
}
