using UnityEngine;

[AddComponentMenu("Physics/SetInertiaTensor")]
public class SetInertiaTensor : MonoBehaviour
{
	public BasicInfo basicInfo;

	public Vector3 inertia;

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			basicInfo.Rigidbody.inertiaTensor = inertia;
		}
	}
}
