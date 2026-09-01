using Poly.Base;
using UnityEngine;

namespace Poly.Physics.Test
{
	public class TestVelocityDamping : PolyBehaviour
	{
		public float springConstant = 9.81f;

		public float dampingConstant = 9.81f;

		private Vector3 initalPosition;

		public bool showVelocity = true;

		private void Awake()
		{
			initalPosition = base.transform.position;
		}

		private void FixedUpdate()
		{
			UnityEngine.Rigidbody component = GetComponent<UnityEngine.Rigidbody>();
			Vector3 vector = -component.velocity * dampingConstant + (initalPosition - base.transform.position) * springConstant;
			component.velocity += vector * Time.fixedDeltaTime / component.mass;
		}
	}
}
