using UnityEngine;

public class NailRayStart : MonoBehaviour
{
	public Transform myParent;

	public ConfigurableJoint myJoint;

	public void AddJointy(Rigidbody target)
	{
		if (!(myJoint == null))
		{
			ConfigurableJoint configurableJoint = base.gameObject.AddComponent<ConfigurableJoint>();
			configurableJoint.anchor = myJoint.anchor;
			configurableJoint.axis = myJoint.axis;
			configurableJoint.secondaryAxis = myJoint.secondaryAxis;
			configurableJoint.angularXMotion = myJoint.angularXMotion;
			configurableJoint.angularYMotion = myJoint.angularYMotion;
			configurableJoint.angularZMotion = myJoint.angularZMotion;
			configurableJoint.xMotion = myJoint.xMotion;
			configurableJoint.yMotion = myJoint.yMotion;
			configurableJoint.zMotion = myJoint.zMotion;
			configurableJoint.projectionMode = myJoint.projectionMode;
			configurableJoint.projectionDistance = myJoint.projectionDistance;
			configurableJoint.projectionAngle = myJoint.projectionAngle;
			configurableJoint.breakForce = myJoint.breakForce;
			configurableJoint.breakTorque = myJoint.breakTorque;
			configurableJoint.connectedBody = target;
		}
	}
}
