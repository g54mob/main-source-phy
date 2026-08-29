using UnityEngine;

namespace Battlehub.Utils
{
	public struct TransformChangesTracker
	{
		private Vector3 m_prevPosition;

		private Quaternion m_prevRotation;

		private Vector3 m_prevScale;

		private Transform m_transform;

		public bool HasChanged
		{
			get
			{
				if (m_transform == null)
				{
					return false;
				}
				if (!(m_prevPosition != m_transform.position) && !(m_prevRotation != m_transform.rotation))
				{
					return m_prevScale != m_transform.lossyScale;
				}
				return true;
			}
		}

		public TransformChangesTracker(Transform transform)
		{
			m_transform = transform;
			m_prevPosition = m_transform.position;
			m_prevRotation = m_transform.rotation;
			m_prevScale = m_transform.lossyScale;
		}

		public void Reset()
		{
			m_prevPosition = m_transform.position;
			m_prevRotation = m_transform.rotation;
			m_prevScale = m_transform.lossyScale;
		}
	}
}
