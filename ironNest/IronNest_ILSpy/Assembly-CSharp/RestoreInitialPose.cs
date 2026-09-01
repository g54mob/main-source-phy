using UnityEngine;

public class RestoreInitialPose : MonoBehaviour
{
	private Vector3 _initialPosition;

	private Quaternion _initialRotation;

	private unsafe void Awake()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected Ref, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected Ref, but got Unknown
		Transform transform = base.transform;
		transform.GetPositionAndRotation(out *(Vector3*)(this + 32), out *(Quaternion*)(this + 44));
	}

	public unsafe void Restore()
	{
		//IL_0020: Expected O, but got Ref
		//IL_0020: Expected O, but got Ref
		Transform transform = base.transform;
		object obj = default(object);
		object obj2 = default(object);
		transform.SetPositionAndRotation((Vector3)(&obj), (Quaternion)(&obj2));
	}
}
