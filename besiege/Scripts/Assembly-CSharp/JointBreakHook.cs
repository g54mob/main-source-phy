using System;
using UnityEngine;

public class JointBreakHook : MonoBehaviour
{
	public Rigidbody body;

	public Action<Rigidbody> JointBroke;

	public void OnJointBreak(float breakForce)
	{
		if (JointBroke != null)
		{
			JointBroke(body);
		}
	}
}
