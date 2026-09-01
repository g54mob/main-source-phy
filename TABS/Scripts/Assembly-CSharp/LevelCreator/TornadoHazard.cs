using UnityEngine;

namespace LevelCreator
{
	public class TornadoHazard : LandscapeHazard
	{
		[SerializeField]
		private float m_radius = 10f;

		[SerializeField]
		private float m_speed = 6f;

		[SerializeField]
		private float m_turnRate = 0.1f;

		private Vector3 m_direction = Vector3.zero;

		private void Start()
		{
			Object.Destroy(base.gameObject, 2.5f);
			m_direction = Random.insideUnitSphere;
		}

		private void Update()
		{
			if (DMEditor.Instance == null)
			{
				Collider[] array = Physics.OverlapSphere(base.transform.position, 6f);
				for (int i = 0; i < array.Length; i++)
				{
					Rigidbody attachedRigidbody = array[i].attachedRigidbody;
					if (attachedRigidbody != null)
					{
						Vector3 normalized = (base.transform.position - attachedRigidbody.position).normalized;
						Vector3.Distance(attachedRigidbody.position, base.transform.position);
						Vector3 force = normalized * 400f;
						attachedRigidbody.AddForce(force);
					}
				}
			}
			m_direction += new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * m_turnRate;
			m_direction.Normalize();
			base.transform.position += m_direction * m_speed * Time.deltaTime;
			Vector3 bestPosition = base.transform.position;
			if (!Utility.FindBestGroundPosition(base.transform.position, Vector3.up * 10f, out bestPosition))
			{
				Object.Destroy(base.gameObject);
			}
			base.transform.position = bestPosition;
		}
	}
}
