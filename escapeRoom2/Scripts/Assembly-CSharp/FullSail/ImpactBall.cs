using UnityEngine;

namespace FullSail
{
	public class ImpactBall : MonoBehaviour
	{
		private Rigidbody rb;

		private int[] types = new int[5] { 0, 3, 6, 7, 5 };

		private void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			if ((bool)rb && Physics.Raycast(new Ray(base.transform.position, rb.linearVelocity.normalized), out var hitInfo, rb.linearVelocity.magnitude * Time.fixedDeltaTime))
			{
				Sail component = hitInfo.collider.GetComponent<Sail>();
				if ((bool)component)
				{
					float num = Random.Range(0.05f, 0.2f);
					int type = types[Random.Range(0, 4)];
					Vector3 vector = hitInfo.collider.transform.InverseTransformPoint(hitInfo.point);
					vector /= 10f;
					vector.x = 1f - (vector.x + 0.5f);
					vector.y = 1f + vector.y;
					component.AddImpact(vector.x, vector.y, num, type, remove: true);
					component.AddRipple(vector.x, vector.y, num * 10f);
				}
			}
		}
	}
}
