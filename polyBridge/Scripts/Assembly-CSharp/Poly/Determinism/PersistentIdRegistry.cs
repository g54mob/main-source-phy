using System.Collections.Generic;
using Poly.Base;
using UnityEngine;

namespace Poly.Determinism
{
	public class PersistentIdRegistry<TComponent> : Singleton<PersistentIdRegistry<TComponent>> where TComponent : Component
	{
		private int nextId = -1;

		private Dictionary<int, TComponent> allIds;

		public int VerifyOrGetNewId(TComponent obj, int objectId)
		{
			if (obj.gameObject.scene.path == null)
			{
				objectId = -1;
			}
			else
			{
				if (allIds == null)
				{
					allIds = new Dictionary<int, TComponent>();
					allIds.Add(-1, null);
					nextId = 0;
				}
				if (objectId >= 0 && !allIds.ContainsKey(objectId))
				{
					allIds.Add(objectId, obj);
					nextId = Mathf.Max(nextId, objectId + 1);
				}
				else if (allIds[objectId] != obj)
				{
					objectId = nextId++;
					allIds.Add(objectId, obj);
				}
			}
			return objectId;
		}

		public void Clear()
		{
			if (allIds == null)
			{
				allIds = new Dictionary<int, TComponent>();
			}
			else
			{
				allIds.Clear();
			}
			allIds.Add(-1, null);
			nextId = 0;
		}
	}
}
