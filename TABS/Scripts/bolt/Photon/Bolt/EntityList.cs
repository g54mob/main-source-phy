using System.Collections;
using System.Collections.Generic;

namespace Photon.Bolt
{
	public class EntityList : IEnumerable<BoltEntity>, IEnumerable
	{
		private readonly List<Entity> _list;

		public int Count => _list.Count;

		internal EntityList(List<Entity> l)
		{
			_list = l;
		}

		public IEnumerator<BoltEntity> GetEnumerator()
		{
			foreach (Entity item in _list)
			{
				if (item != null && item.IsAttached && item.UnityObject != null)
				{
					yield return item.UnityObject;
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
