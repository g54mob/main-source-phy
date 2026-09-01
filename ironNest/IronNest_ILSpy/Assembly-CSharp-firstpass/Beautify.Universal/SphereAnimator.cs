using Cpp2ILInjected;
using UnityEngine;

namespace Beautify.Universal;

public class SphereAnimator : MonoBehaviour
{
	private Rigidbody rb;

	private const float SPEED = 4f;

	private void Start()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Rigidbody rigidbody = default(Rigidbody);
		rb = rigidbody;
		Application.targetFrameRate = 60;
	}

	private unsafe void FixedUpdate()
	{
		//IL_009b: Expected O, but got Ref
		Transform transform = base.transform;
		Rigidbody rigidbody;
		if (!(0.5f > transform.position.z))
		{
			Transform transform2 = base.transform;
			if (!(transform2.position.z > 8f))
			{
				return;
			}
			rigidbody = rb;
		}
		else
		{
			rigidbody = rb;
		}
		Vector3 vector = default(Vector3);
		rigidbody.linearVelocity = (Vector3)(&vector);
	}
}
