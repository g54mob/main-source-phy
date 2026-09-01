using UnityEngine;

public class CameraFollower : MonoBehaviour
{
	private Transform _Target;

	private unsafe void FixedUpdate()
	{
		//IL_0031: Expected O, but got Ref
		//IL_0061: Expected O, but got Ref
		Transform transform = base.transform;
		Vector3 position = _Target.position;
		float num = default(float);
		transform.position = (Vector3)(&num);
		Vector3 eulerAngles = _Target.eulerAngles;
		Transform transform2 = base.transform;
		transform2.eulerAngles = (Vector3)(&num);
	}
}
