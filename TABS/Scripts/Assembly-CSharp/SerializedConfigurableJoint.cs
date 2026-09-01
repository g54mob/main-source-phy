using System;
using UnityEngine;

[Serializable]
public class SerializedConfigurableJoint
{
	public Rigidbody connectedBody;

	public Vector3 anchor;

	public Vector3 axis;

	public ConfigurableJointMotion xMotion;

	public ConfigurableJointMotion yMotion;

	public ConfigurableJointMotion zMotion;

	[Space(15f)]
	public ConfigurableJointMotion angularXMotion;

	public ConfigurableJointMotion angularYMotion;

	public ConfigurableJointMotion angularZMotion;

	[Range(-170f, 170f)]
	public float minXRotation;

	[Range(-170f, 170f)]
	public float maxXRotation;

	[Space(15f)]
	[Range(-170f, 170f)]
	public float yRotation;

	[Space(15f)]
	[Range(-170f, 170f)]
	public float zRotation;

	public ConfigurableJoint CreateJoint(Rigidbody ownRig, Rigidbody bodyToConnectTo = null)
	{
		ConfigurableJoint configurableJoint = ownRig.gameObject.AddComponent<ConfigurableJoint>();
		configurableJoint.anchor = anchor;
		configurableJoint.axis = axis;
		if ((bool)bodyToConnectTo)
		{
			configurableJoint.connectedBody = bodyToConnectTo;
		}
		else
		{
			configurableJoint.connectedBody = connectedBody;
		}
		SoftJointLimit highAngularXLimit = configurableJoint.highAngularXLimit;
		highAngularXLimit.limit = maxXRotation;
		configurableJoint.highAngularXLimit = highAngularXLimit;
		highAngularXLimit = configurableJoint.lowAngularXLimit;
		highAngularXLimit.limit = minXRotation;
		configurableJoint.lowAngularXLimit = highAngularXLimit;
		highAngularXLimit = configurableJoint.angularYLimit;
		highAngularXLimit.limit = yRotation;
		configurableJoint.angularYLimit = highAngularXLimit;
		highAngularXLimit = configurableJoint.angularZLimit;
		highAngularXLimit.limit = zRotation;
		configurableJoint.angularZLimit = highAngularXLimit;
		configurableJoint.xMotion = xMotion;
		configurableJoint.yMotion = yMotion;
		configurableJoint.zMotion = zMotion;
		configurableJoint.angularXMotion = angularXMotion;
		configurableJoint.angularYMotion = angularYMotion;
		configurableJoint.angularZMotion = angularZMotion;
		configurableJoint.enablePreprocessing = false;
		configurableJoint.projectionMode = JointProjectionMode.PositionAndRotation;
		return configurableJoint;
	}
}
