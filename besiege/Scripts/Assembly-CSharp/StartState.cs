using UnityEngine;

public class StartState : MonoBehaviour
{
	public Vector3 startPos;

	public Quaternion startRotation;

	public Rigidbody myRigidbody;

	public ConfigurableJoint startJoint;

	public Rigidbody startJointConnectedBody;

	private Vector3 jAnchor;

	private Vector3 jAxis;

	private Vector3 jSecondaryAxis;

	private ConfigurableJointMotion jAngularXMotion;

	private ConfigurableJointMotion jAngularYMotion;

	private ConfigurableJointMotion jAngularZMotion;

	private ConfigurableJointMotion jXMotion;

	private ConfigurableJointMotion jYMotion;

	private ConfigurableJointMotion jZMotion;

	private JointProjectionMode jProjectionMode;

	private float jProjectionDistance;

	private float jProjectionAngle;

	private float jBreakForce;

	private float jBreakTorque;

	private Rigidbody jConnectedBody;
}
