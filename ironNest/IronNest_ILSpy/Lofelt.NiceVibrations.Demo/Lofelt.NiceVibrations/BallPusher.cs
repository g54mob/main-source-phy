using System;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Lofelt.NiceVibrations;

public class BallPusher : MonoBehaviour
{
	public float Force = 5f;

	public BallDemoBall TargetBall;

	protected Vector2 _direction;

	protected virtual void OnTriggerEnter2D(Collider2D collider)
	{
		//IL_00fa: Invalid comparison between O and F4
		GameObject gameObject = collider.gameObject;
		GameObject gameObject2 = TargetBall.gameObject;
		if (gameObject == gameObject2)
		{
			Transform transform = collider.transform;
			Vector3 position = transform.position;
			Transform transform2 = base.transform;
			Vector3 position2 = transform2.position;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
			Vector2 vector = default(Vector2);
			Vector3 direction = ((System.Runtime.CompilerServices.Unsafe.As<Vector2, UIntPtr>(ref vector) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f)) ? Vector3.zeroVector : ((Vector3)vector));
			_direction = direction;
			_ = 1065353216;
			Rigidbody2D attachedRigidbody = collider.attachedRigidbody;
			attachedRigidbody.linearVelocity = vector;
			Rigidbody2D attachedRigidbody2 = collider.attachedRigidbody;
			attachedRigidbody2.AddForce(vector);
			TargetBall.HitPusher();
		}
	}
}
