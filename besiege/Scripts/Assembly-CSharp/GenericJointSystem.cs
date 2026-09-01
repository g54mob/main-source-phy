using UnityEngine;

[RequireComponent(typeof(JointSearchSystem))]
public class GenericJointSystem : MonoBehaviour
{
	public ConfigurableJoint jointToCopy;

	private JointSearchSystem jointSearchSystem;

	[SerializeField]
	private BreakOnForce breakScript;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			return;
		}
		jointSearchSystem = GetComponent<JointSearchSystem>();
		if (breakScript == null)
		{
			breakScript = GetComponent<BreakOnForce>();
		}
		for (int i = 0; i < jointSearchSystem.connectedBodies.Count; i++)
		{
			if (jointSearchSystem.connectedBodies[i] != null)
			{
				ConfigurableJoint configurableJoint = base.transform.gameObject.AddComponent<ConfigurableJoint>();
				CopyJointSettings(configurableJoint, i);
				configurableJoint.connectedBody = jointSearchSystem.connectedBodies[i];
				if (!(breakScript != null))
				{
				}
			}
		}
	}

	private void CopyJointSettings(ConfigurableJoint joint, int index)
	{
		joint.breakForce = jointToCopy.breakForce;
		joint.breakTorque = jointToCopy.breakTorque;
		joint.xMotion = jointToCopy.xMotion;
		joint.yMotion = jointToCopy.yMotion;
		joint.zMotion = jointToCopy.zMotion;
		joint.angularXMotion = jointToCopy.angularXMotion;
		joint.angularYMotion = jointToCopy.angularYMotion;
		joint.angularZMotion = jointToCopy.angularZMotion;
		joint.axis = jointToCopy.axis;
		joint.anchor = jointSearchSystem.Joints[index].transform.localPosition;
	}
}
