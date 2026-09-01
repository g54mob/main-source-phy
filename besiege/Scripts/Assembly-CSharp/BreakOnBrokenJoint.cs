using UnityEngine;

[AddComponentMenu("Destruction/Break On Broken Joint")]
public class BreakOnBrokenJoint : SimBehaviour
{
	public BreakOnForce breakingScript;

	public Joint joint;

	protected void LateUpdate()
	{
		if (base.isSimulating)
		{
			if (StatMaster.isMP && StatMaster.isClient && !StatMaster.isLocalSim)
			{
				Object.Destroy(this);
			}
			if (joint == null || joint.connectedBody == null || joint.breakForce == 0f || !joint.connectedBody.gameObject.activeSelf)
			{
				Break();
			}
		}
	}

	protected void OnJointBreak(float breakForce)
	{
		if (base.enabled)
		{
			Break();
		}
	}

	protected void Break()
	{
		if (!StatMaster.isMP || !StatMaster.isClient || StatMaster.isLocalSim)
		{
			breakingScript.ExternalBreak();
			if (joint != null)
			{
				Object.Destroy(joint);
			}
		}
	}
}
