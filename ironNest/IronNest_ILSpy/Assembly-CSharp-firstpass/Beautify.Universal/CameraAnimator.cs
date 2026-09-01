using UnityEngine;

namespace Beautify.Universal;

public class CameraAnimator : MonoBehaviour
{
	private unsafe void Update()
	{
		//IL_0025: Expected O, but got Ref
		Transform transform = base.transform;
		float deltaTime = Time.deltaTime;
		object obj = default(object);
		transform.Rotate((Vector3)(&obj));
	}
}
