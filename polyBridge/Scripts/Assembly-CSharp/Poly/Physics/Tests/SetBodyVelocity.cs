using UnityEngine;

namespace Poly.Physics.Tests
{
	public class SetBodyVelocity : MonoBehaviour
	{
		public Vec2 velocity;

		public float angularVelocity;

		private Rigidbody body;

		private void Awake()
		{
			body = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			if ((bool)body && body.isAddedToWorld)
			{
				body.linearVelocity = velocity;
				body.angularVelocityDeg = angularVelocity;
				body = null;
				Object.Destroy(this);
			}
		}
	}
}
