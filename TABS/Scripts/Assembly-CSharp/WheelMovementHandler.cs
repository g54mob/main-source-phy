using System.Collections.Generic;
using UnityEngine;

public class WheelMovementHandler : MonoBehaviour
{
	public float force;

	public float torque;

	public float multiplier = 1f;

	public Rigidbody[] leftWheels;

	public Rigidbody[] rightWheels;

	private GeneralInput input;

	private DataHandler data;

	private Rigidbody rig;

	private void Start()
	{
		input = GetComponent<GeneralInput>();
		data = GetComponent<DataHandler>();
		rig = data.torso.GetComponent<Rigidbody>();
		Rigidbody[] componentsInChildren = GetComponentsInChildren<Rigidbody>();
		List<Rigidbody> list = new List<Rigidbody>();
		List<Rigidbody> list2 = new List<Rigidbody>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].transform.localPosition.x < 0f)
			{
				list.Add(componentsInChildren[i]);
			}
			else
			{
				list2.Add(componentsInChildren[i]);
			}
		}
		leftWheels = list.ToArray();
		rightWheels = list2.ToArray();
	}

	private void Update()
	{
		if (data.Dead)
		{
			Object.Destroy(this);
			return;
		}
		float num = 0f;
		float num2 = 0f;
		if (input.inputDirection.z > 0f)
		{
			num += 1f;
			num2 += 1f;
		}
		float x = data.torso.InverseTransformPoint(data.torso.position + data.characterForwardObject.forward * 5f).x;
		if ((double)x < -0.2)
		{
			num2 += 1f;
			num -= 1f;
		}
		if ((double)x > 0.2)
		{
			num2 -= 1f;
			num += 1f;
		}
		if (input.inputDirection.z < 0f)
		{
			num += 1f;
			num2 += 1f;
			num *= -1f;
			num2 *= -1f;
		}
		rig.AddForce(input.inputDirection.z * force * multiplier * Time.deltaTime * rig.transform.forward, ForceMode.Acceleration);
		Vector3 vector = num * Time.deltaTime * torque * rig.transform.right;
		for (int i = 0; i < leftWheels.Length; i++)
		{
			if ((bool)leftWheels[i])
			{
				leftWheels[i].AddTorque(vector, ForceMode.Acceleration);
			}
		}
		Vector3 vector2 = num2 * Time.deltaTime * torque * rig.transform.right;
		for (int j = 0; j < rightWheels.Length; j++)
		{
			if ((bool)rightWheels[j])
			{
				rightWheels[j].AddTorque(vector2, ForceMode.Acceleration);
			}
		}
	}
}
