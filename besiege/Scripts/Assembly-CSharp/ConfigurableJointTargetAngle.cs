using UnityEngine;

public class ConfigurableJointTargetAngle : MonoBehaviour
{
	public Vector3 targetAngle;

	private ConfigurableJoint myJoint;

	private Quaternion startRotation;

	private void Start()
	{
		startRotation = base.transform.localRotation;
		myJoint = GetComponent<ConfigurableJoint>();
	}

	private void Update()
	{
		myJoint.SetTargetRotationLocal(Quaternion.Euler(targetAngle), startRotation);
	}
}
