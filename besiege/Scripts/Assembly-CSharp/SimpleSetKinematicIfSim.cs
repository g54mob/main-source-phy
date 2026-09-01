using UnityEngine;

[AddComponentMenu("Physics/SimpleSetKinematicIfSim")]
public class SimpleSetKinematicIfSim : SimBehaviour
{
	protected override void Start()
	{
		base.Start();
		if (base.isSimulating && (base.SimPhysics || !StatMaster.isMP))
		{
			if (HasBasicInfo && !basicInfo.noRigidbody)
			{
				basicInfo.Rigidbody.isKinematic = false;
			}
			else
			{
				GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}
}
