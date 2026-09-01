using UnityEngine;

public class ConfigLimitBasedMotor : MonoBehaviour
{
	public ConfigurableJoint configJoint;

	public float speed = 100f;

	public float angleLimit;

	public Rigidbody myRigidbody;

	private void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.Joystick1Button1))
		{
			angleLimit += Time.deltaTime * speed;
			if (angleLimit > 180.5f)
			{
				angleLimit = -179f;
			}
			if (angleLimit < -180f)
			{
				angleLimit = 180f;
			}
			myRigidbody.WakeUp();
			SoftJointLimit highAngularXLimit = configJoint.highAngularXLimit;
			highAngularXLimit.limit = angleLimit;
			configJoint.highAngularXLimit = highAngularXLimit;
			SoftJointLimit lowAngularXLimit = configJoint.lowAngularXLimit;
			lowAngularXLimit.limit = angleLimit - 0.1f;
			configJoint.lowAngularXLimit = lowAngularXLimit;
		}
	}
}
