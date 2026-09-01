using System;
using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	[AddComponentMenu("Surface Waves/Dimensions")]
	public class Dimensions : MonoBehaviour
	{
		[SerializeField]
		private Vector3 m_localExtends = new Vector3(256f, 64f, 256f);

		[SerializeField]
		protected int m_resolutionX = 512;

		[SerializeField]
		private int m_resolutionZ = 512;

		private BoxCollider m_influenceTrigger;

		private Transform m_cachedTransformReference;

		public int resolutionX
		{
			get
			{
				return m_resolutionX;
			}
			set
			{
				if (value > 0)
				{
					m_resolutionX = value;
					UpdateTriggerDimensions();
					return;
				}
				m_resolutionX = 1;
				throw new ArgumentOutOfRangeException(resolutionTooLowMessage);
			}
		}

		public int resolutionZ
		{
			get
			{
				return m_resolutionZ;
			}
			set
			{
				if (value > 0)
				{
					m_resolutionZ = value;
					UpdateTriggerDimensions();
					return;
				}
				m_resolutionZ = 1;
				throw new ArgumentOutOfRangeException(resolutionTooLowMessage);
			}
		}

		private string resolutionTooLowMessage
		{
			get
			{
				return "Resolution must be higher than 0";
			}
		}

		public Vector3 localExtends
		{
			get
			{
				return m_localExtends;
			}
			set
			{
				if (value != m_localExtends)
				{
					m_localExtends = value;
					UpdateTriggerDimensions();
				}
			}
		}

		protected Transform cachedTransformReference
		{
			get
			{
				if (m_cachedTransformReference == null)
				{
					CacheTransformReference();
				}
				return m_cachedTransformReference;
			}
		}

		public Vector3 localSize
		{
			get
			{
				return 2f * localExtends;
			}
			set
			{
				localExtends = 0.5f * value;
			}
		}

		public Vector3 localFirstCorner
		{
			get
			{
				return new Vector3(0f - localExtends.x, 0f, 0f - localExtends.z);
			}
		}

		public Vector3 firstCorner
		{
			get
			{
				return cachedTransformReference.TransformPoint(localFirstCorner);
			}
		}

		public Vector3 localCenter
		{
			get
			{
				return new Vector3(0f, localExtends.y, 0f);
			}
		}

		public Vector3 center
		{
			get
			{
				return cachedTransformReference.TransformPoint(localCenter);
			}
		}

		protected void OnEnable()
		{
			CacheTransformReference();
			FindTriggerOnGameObject();
		}

		protected void OnValidate()
		{
			localExtends = m_localExtends;
			resolutionX = m_resolutionX;
			resolutionZ = m_resolutionZ;
		}

		protected void FindTriggerOnGameObject()
		{
			m_influenceTrigger = GetComponent<BoxCollider>();
			if (m_influenceTrigger != null)
			{
				m_influenceTrigger.isTrigger = true;
				return;
			}
			throw new MissingComponentException("Box collider trigger required");
		}

		protected void UpdateTriggerDimensions()
		{
			if (m_influenceTrigger == null)
			{
				FindTriggerOnGameObject();
			}
			m_influenceTrigger.size = localSize;
			m_influenceTrigger.center = new Vector3(0f, localExtends.y, 0f);
		}

		protected void CacheTransformReference()
		{
			m_cachedTransformReference = base.transform;
		}
	}
}
