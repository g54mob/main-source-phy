using System.Collections;
using UnityEngine;

namespace LevelCreator
{
	public class BalloonDart : MonoBehaviour
	{
		[SerializeField]
		private GameObject m_balloonPrefab;

		private GameObject m_balloon;

		private const float m_destroyHeight = 200f;

		private bool m_isConnected;

		private bool m_hasHit;

		private Rigidbody m_rigidbody;

		private float m_targetMass;

		private DMEditorComponent m_connectedTarget;

		private void Awake()
		{
		}

		private void Start()
		{
			m_rigidbody = GetComponent<Rigidbody>();
		}

		private void Update()
		{
			if (m_isConnected)
			{
				m_rigidbody.AddForce(Vector3.up * 10f * 9.82f * m_targetMass, ForceMode.Acceleration);
				m_balloon.transform.position = base.transform.position;
			}
			if (base.transform.position.y >= 200f)
			{
				if (m_connectedTarget != null)
				{
					Object.Destroy(m_connectedTarget.gameObject);
				}
				Object.Destroy(m_balloon);
				Object.Destroy(base.gameObject);
			}
			if ((m_rigidbody.IsSleeping() && m_connectedTarget == null) || (m_connectedTarget != null && m_connectedTarget.GetComponent<Rigidbody>() == null))
			{
				if (m_connectedTarget != null)
				{
					Object.Destroy(m_connectedTarget.gameObject);
				}
				Object.Destroy(m_balloon);
				Object.Destroy(base.gameObject);
			}
		}

		private void OnCollisionEnter(Collision other)
		{
			DMEditorComponent componentInParent = other.gameObject.GetComponentInParent<DMEditorComponent>();
			if (componentInParent != null && !m_hasHit)
			{
				m_hasHit = true;
				m_rigidbody.useGravity = false;
				m_rigidbody.velocity = Vector3.zero;
				base.transform.position = other.GetContact(0).point;
				if (!componentInParent.CanSimulatePhysics)
				{
					Object.Destroy(base.gameObject);
				}
				componentInParent.SimulatePhysics();
				StartCoroutine(ConnectBodies(componentInParent));
			}
		}

		private IEnumerator ConnectBodies(DMEditorComponent target)
		{
			yield return new WaitUntil(() => target.GetComponent<Rigidbody>());
			m_connectedTarget = target;
			FixedJoint fixedJoint = base.gameObject.AddComponent<FixedJoint>();
			if (target != null)
			{
				fixedJoint.connectedBody = target.GetComponent<Rigidbody>();
			}
			if (fixedJoint.connectedBody != null)
			{
				m_targetMass = fixedJoint.connectedBody.mass;
			}
			m_balloon = Object.Instantiate(m_balloonPrefab);
			m_balloon.GetComponentInChildren<MeshRenderer>().material.SetVector("_Color", Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f));
			m_isConnected = true;
		}
	}
}
