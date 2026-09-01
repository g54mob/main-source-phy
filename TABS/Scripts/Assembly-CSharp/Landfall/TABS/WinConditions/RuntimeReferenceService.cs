using System;
using System.Collections.Generic;

namespace Landfall.TABS.WinConditions
{
	public class RuntimeReferenceService : ServicePrefab
	{
		public delegate void OnReleasedReferenceDelegate(RuntimeReference reference);

		protected struct ReferencePair
		{
			public RuntimeReference Reference;

			public object ReferenceTarget;
		}

		private Dictionary<Guid, ReferencePair> m_references = new Dictionary<Guid, ReferencePair>();

		private List<RuntimeReference> m_referenceRequests = new List<RuntimeReference>();

		public OnReleasedReferenceDelegate OnReleasedReferenceCallback;

		public ReferenceType<T> CreateReference<T>(T referenceTarget)
		{
			Guid key = Guid.NewGuid();
			ReferenceType<T> referenceType = new ReferenceType<T>(key.ToString());
			if (m_references.ContainsKey(key))
			{
				m_references[key] = new ReferencePair
				{
					Reference = referenceType,
					ReferenceTarget = referenceTarget
				};
			}
			else
			{
				m_references.Add(key, new ReferencePair
				{
					Reference = referenceType,
					ReferenceTarget = referenceTarget
				});
			}
			return referenceType;
		}

		public ReferenceType<T> CreateReferenceFromCache<T>(RuntimeReference cachedReference, T referenceTarget)
		{
			Guid guid = cachedReference.Guid;
			ReferenceType<T> referenceType = new ReferenceType<T>(guid.ToString());
			if (m_references.ContainsKey(guid))
			{
				m_references[guid] = new ReferencePair
				{
					Reference = referenceType,
					ReferenceTarget = referenceTarget
				};
			}
			else
			{
				m_references.Add(guid, new ReferencePair
				{
					Reference = referenceType,
					ReferenceTarget = referenceTarget
				});
			}
			return referenceType;
		}

		public void CreateRequest<T>(ReferenceRequest<T> request)
		{
			m_referenceRequests.Add(request);
		}

		public void ReleaseRequest<T>(ReferenceRequest<T> request)
		{
			m_referenceRequests.Remove(request);
			OnReleasedReferenceCallback?.Invoke(request);
		}

		public ReferenceType<T> GetReference<T>(Guid guid)
		{
			if (!m_references.ContainsKey(guid))
			{
				return null;
			}
			return (ReferenceType<T>)m_references[guid].ReferenceTarget;
		}

		public T GetReferenceTarget<T>(RuntimeReference reference)
		{
			if (reference == null)
			{
				return default(T);
			}
			if (!m_references.ContainsKey(reference.Guid))
			{
				return default(T);
			}
			return (T)m_references[reference.Guid].ReferenceTarget;
		}

		public void ReleaseAllReferences()
		{
			m_references.Clear();
			m_referenceRequests.Clear();
		}

		public List<RuntimeReference> GetAllReferencesRequests()
		{
			return m_referenceRequests;
		}
	}
}
