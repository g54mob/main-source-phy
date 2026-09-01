using UnityEngine;

namespace Lofelt.NiceVibrations;

public class HapticClipsDemoRotator : MonoBehaviour
{
	public Vector3 RotationSpeed;

	protected unsafe void Update()
	{
		//IL_002b: Expected O, but got Ref
		Transform transform = base.transform;
		float deltaTime = Time.deltaTime;
		Vector3 vector = default(Vector3);
		transform.Rotate((Vector3)(&vector), Space.Self);
	}

	public HapticClipsDemoRotator()
	{
		Vector3 rotationSpeed = default(Vector3);
		RotationSpeed = rotationSpeed;
		_ = 100f;
		base._002Ector();
	}
}
