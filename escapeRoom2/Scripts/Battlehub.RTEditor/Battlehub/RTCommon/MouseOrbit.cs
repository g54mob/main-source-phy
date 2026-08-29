using UnityEngine;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(-60)]
	public class MouseOrbit : MonoBehaviour
	{
		protected Camera m_camera;

		public Transform Target;

		public Transform SecondaryTarget;

		public float Distance = 5f;

		public float XSpeed = 5f;

		public float YSpeed = 5f;

		public float DistanceMin = 0.5f;

		public float DistanceMax = 5000f;

		public bool CanOrbit;

		public bool CanZoom;

		public bool ChangeOrthographicSizeOnly;

		private void Awake()
		{
			m_camera = GetComponent<Camera>();
		}

		private void Start()
		{
			if (Target != null && m_camera != null)
			{
				Distance = (Target.transform.position - m_camera.transform.position).magnitude;
			}
		}

		public virtual void Zoom(float deltaZ)
		{
			if (!CanZoom)
			{
				deltaZ = 0f;
			}
			if (m_camera.orthographic)
			{
				m_camera.orthographicSize -= deltaZ * m_camera.orthographicSize;
				if (m_camera.orthographicSize < 0.01f)
				{
					m_camera.orthographicSize = 0.01f;
				}
				if (ChangeOrthographicSizeOnly)
				{
					return;
				}
			}
			Distance = Mathf.Clamp(Distance - deltaZ * Mathf.Max(1f, Distance), DistanceMin, DistanceMax);
			Vector3 vector = new Vector3(0f, 0f, 0f - Distance);
			Vector3 position = base.transform.rotation * vector + Target.position;
			base.transform.position = position;
		}

		public virtual void Orbit(float deltaX, float deltaY, float deltaZ)
		{
			if (!CanOrbit)
			{
				deltaX = 0f;
				deltaY = 0f;
			}
			if (!(m_camera == null) && (deltaX != 0f || deltaY != 0f || deltaZ != 0f))
			{
				deltaX *= XSpeed;
				deltaY *= YSpeed;
				Quaternion rotation = Quaternion.Inverse(Quaternion.Euler(deltaY, 0f, 0f) * Quaternion.Inverse(base.transform.rotation) * Quaternion.Euler(0f, 0f - deltaX, 0f));
				base.transform.rotation = rotation;
				Zoom(deltaZ);
			}
		}
	}
}
