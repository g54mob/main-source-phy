using System.Collections.Generic;

namespace Shapes
{
	public class ExpandoList<T>
	{
		public List<T> list;

		public T this[int i]
		{
			get
			{
				return default(T);
			}
			set
			{
			}
		}

		public void SetCountToAtLeast(int minCount)
		{
		}

		public void Add(T item)
		{
		}

		public void Clear()
		{
		}

		public void ClearAndSetMinCapacity(int minCapacity)
		{
		}
	}
}
