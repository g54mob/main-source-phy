using UnityEngine;

public class SolidHingeJoints : MonoBehaviour
{
	public HingeJoint myJoint;

	public float myHingeAngle;

	public float angleToSubtract = 540f;

	public float angleOffset;

	public float speed = 100f;

	private void FixedUpdate()
	{
		FlipTargetPos();
		SetAngle();
	}

	private void SetAngle()
	{
		myHingeAngle = angleOffset + myJoint.angle;
		float num = ((Mathf.Abs(myJoint.angle) + 180f) % 360f - 180f) % 360f;
		if (myJoint.angle < 0f)
		{
			num *= -1f;
		}
		JointSpring spring = myJoint.spring;
		spring.targetPosition = num + Time.deltaTime * speed;
		myJoint.spring = spring;
	}

	private void FlipTargetPos()
	{
		if (myJoint.spring.targetPosition >= 179.99f)
		{
			JointSpring spring = myJoint.spring;
			spring.targetPosition = -179.98f;
			myJoint.spring = spring;
		}
		if (myJoint.spring.targetPosition <= -179.99f)
		{
			JointSpring spring2 = myJoint.spring;
			spring2.targetPosition = 179.98f;
			myJoint.spring = spring2;
		}
	}
}
