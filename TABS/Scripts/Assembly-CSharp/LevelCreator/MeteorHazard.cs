using System.Collections;
using UnityEngine;

namespace LevelCreator
{
	public class MeteorHazard : LandscapeHazard
	{
		private Brush m_subtractBrush;

		[Space]
		[SerializeField]
		private float m_startDelay = 1f;

		[SerializeField]
		private float m_destroyDelay = 1f;

		private bool m_hasHit;

		private Brush GenerateSubtractBrush()
		{
			return VolumeBrushes.CreateSphereBrush(Random.Range(8, 12), Random.Range(0.1f, 0.2f), 0f);
		}

		private void Start()
		{
			m_subtractBrush = GenerateSubtractBrush();
			StartCoroutine(StartHazard());
			Object.Destroy(base.gameObject, m_destroyDelay);
		}

		private IEnumerator StartHazard()
		{
			yield return new WaitForSeconds(m_startDelay);
			ExecuteHazard();
		}

		private void OnTriggerEnter(Collider other)
		{
			ExecuteHazard();
		}

		private void ExecuteHazard()
		{
			if (m_hasHit)
			{
				return;
			}
			m_hasHit = true;
			Vector3 position = base.transform.position;
			if (position == Vector3.zero)
			{
				return;
			}
			if (DMEditor.Instance != null)
			{
				DMEditor.Instance.VolumeRootObject.SubtractVolume(position, m_subtractBrush, Volume.fullLerpIntensity);
				DMEditor.Instance.MarkObjectsForSnapping(DMEditor.Instance.VolumeRootObject.GetBounds(base.transform.position, m_subtractBrush));
				if (TakeSnapshot)
				{
					DMEditor.Instance.ScheduleTakeLevelSnapshot();
				}
			}
			else
			{
				Collider[] array = Physics.OverlapSphere(base.transform.position, 8f);
				for (int i = 0; i < array.Length; i++)
				{
					Rigidbody attachedRigidbody = array[i].attachedRigidbody;
					if (attachedRigidbody != null)
					{
						Vector3 normalized = (attachedRigidbody.position - base.transform.position).normalized;
						float b = Vector3.Distance(attachedRigidbody.position, base.transform.position);
						Vector3 force = normalized * (40000f * (1f / Mathf.Max(1f, b)));
						attachedRigidbody.AddForce(force);
					}
				}
			}
			Shake();
			Object.Destroy(base.gameObject);
		}

		private new void OnDestroy()
		{
			Shake();
			base.OnDestroy();
		}
	}
}
