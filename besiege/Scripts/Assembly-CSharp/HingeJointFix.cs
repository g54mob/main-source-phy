using UnityEngine;

public class HingeJointFix : MonoBehaviour
{
	public HingeJoint hingeJointy;

	private void FixedUpdate()
	{
		JointMotor motor = hingeJointy.motor;
		motor.targetVelocity = 50f;
		hingeJointy.motor = motor;
		hingeJointy.axis = new Vector3((!Input.GetKey(KeyCode.Joystick1Button1)) ? 1f : (-1f), hingeJointy.axis.y, hingeJointy.axis.z);
	}
}
