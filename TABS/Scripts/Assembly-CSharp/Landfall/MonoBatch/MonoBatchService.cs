using System;
using System.Collections.Generic;
using UnityEngine;

namespace Landfall.MonoBatch
{
	[CreateAssetMenu(fileName = "MonoBatchService", menuName = "Services/MonoBatch", order = 0)]
	public class MonoBatchService : ServiceAsset
	{
		private struct TypeMethods
		{
			public bool HasUpdate;

			public bool HasFixedUpdate;
		}

		private Dictionary<Type, TypeMethods> m_methodMapping = new Dictionary<Type, TypeMethods>();

		private BatchedMonobehaviour[] m_batchedUpdates = new BatchedMonobehaviour[1000];

		private BatchedMonobehaviour[] m_batchedFixedUpdates = new BatchedMonobehaviour[1000];

		private int m_updateCount;

		private int m_fixedUpdateCount;

		public void AddBatchedBehaviour(BatchedMonobehaviour behaviour)
		{
			Type type = behaviour.GetType();
			if (!HasMethodMapping(type))
			{
				AddMethodMapping(type);
			}
			TypeMethods methodMapping = GetMethodMapping(type);
			if (methodMapping.HasUpdate)
			{
				m_updateCount++;
				if (m_updateCount >= m_batchedUpdates.Length)
				{
					Array.Resize(ref m_batchedUpdates, m_batchedUpdates.Length + 100);
				}
				m_batchedUpdates[m_updateCount - 1] = behaviour;
			}
			if (methodMapping.HasFixedUpdate)
			{
				m_fixedUpdateCount++;
				if (m_fixedUpdateCount >= m_batchedFixedUpdates.Length)
				{
					Array.Resize(ref m_batchedFixedUpdates, m_batchedFixedUpdates.Length + 100);
				}
				m_batchedFixedUpdates[m_fixedUpdateCount - 1] = behaviour;
			}
		}

		private void AddMethodMapping(Type type)
		{
			bool hasUpdate = type.GetMethod("BatchedUpdate").DeclaringType == type;
			bool hasFixedUpdate = type.GetMethod("BatchedFixedUpdate").DeclaringType == type;
			m_methodMapping.Add(type, new TypeMethods
			{
				HasUpdate = hasUpdate,
				HasFixedUpdate = hasFixedUpdate
			});
		}

		private void RemoveMethodMapping(Type type)
		{
			m_methodMapping.Remove(type);
		}

		private TypeMethods GetMethodMapping(Type type)
		{
			return m_methodMapping[type];
		}

		private bool HasMethodMapping(Type type)
		{
			if (m_methodMapping.ContainsKey(type))
			{
				return true;
			}
			return false;
		}

		public void RemoveBatchedBehaviour(BatchedMonobehaviour behaviour)
		{
			Type type = behaviour.GetType();
			TypeMethods methodMapping = GetMethodMapping(type);
			if (methodMapping.HasUpdate)
			{
				RemoveElement(ref m_batchedUpdates, ref m_updateCount, behaviour);
			}
			if (methodMapping.HasFixedUpdate)
			{
				RemoveElement(ref m_batchedFixedUpdates, ref m_fixedUpdateCount, behaviour);
			}
		}

		private void RemoveElement(ref BatchedMonobehaviour[] behaviours, ref int elementCount, BatchedMonobehaviour behaviour)
		{
			for (int i = 0; i < elementCount; i++)
			{
				if (behaviours[i] == behaviour)
				{
					behaviours[i] = behaviours[elementCount - 1];
					behaviours[elementCount - 1] = null;
					elementCount--;
					break;
				}
			}
		}

		public override void OnUpdate()
		{
			int updateCount = m_updateCount;
			for (int i = 0; i < updateCount; i++)
			{
				m_batchedUpdates[i].BatchedUpdate();
			}
		}

		public override void OnFixedUpdate()
		{
			int fixedUpdateCount = m_fixedUpdateCount;
			for (int i = 0; i < fixedUpdateCount; i++)
			{
				m_batchedFixedUpdates[i].BatchedFixedUpdate();
			}
		}
	}
}
