using TFBGames;
using UnityEngine;

public class AddTorque : MonoBehaviour
{
	public Vector3 torque;

	public Vector3 worldTorque;

	private Rigidbody[] rig;

	private void Start()
	{
		rig = GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < rig.Length; i++)
		{
			rig[i].AddTorque(FixedTimeStepService.TorqueCoefficient * base.transform.TransformDirection(torque), ForceMode.VelocityChange);
			rig[i].AddTorque(FixedTimeStepService.TorqueCoefficient * worldTorque, ForceMode.VelocityChange);
		}
	}
}
