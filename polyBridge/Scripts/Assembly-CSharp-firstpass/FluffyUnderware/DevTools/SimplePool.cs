using System.Collections.Generic;

namespace FluffyUnderware.DevTools
{
	internal class SimplePool<T> where T : new()
	{
		private readonly List<T> freeItemsBackfield;

		public SimplePool(int preCreatedElementsCount)
		{
			freeItemsBackfield = new List<T>();
			for (int i = 0; i < preCreatedElementsCount; i++)
			{
				freeItemsBackfield.Add(new T());
			}
		}

		public T GetItem()
		{
			T result;
			if (freeItemsBackfield.Count == 0)
			{
				result = new T();
			}
			else
			{
				int index = freeItemsBackfield.Count - 1;
				result = freeItemsBackfield[index];
				freeItemsBackfield.RemoveAt(index);
			}
			return result;
		}

		public void ReleaseItem(T item)
		{
			freeItemsBackfield.Add(item);
		}
	}
}
