using System.Collections.Generic;
using UnityEngine;

public class MyJointController : MonoBehaviour
{
	public Joint mainJoint;

	public List<ConfigurableJoint> objectsAttachedToMe = new List<ConfigurableJoint>();
}
