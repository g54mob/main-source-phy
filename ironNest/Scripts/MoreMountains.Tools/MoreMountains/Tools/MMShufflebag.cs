using System.Collections.Generic;

namespace MoreMountains.Tools
{
	public class MMShufflebag<T>
	{
		protected List<T> _contents;

		protected T _currentItem;

		protected int _currentIndex;

		public virtual int Capacity => 0;

		public virtual int Size => 0;

		public MMShufflebag(int initialCapacity)
		{
		}

		public virtual void Add(T item, int quantity)
		{
		}

		public T Pick()
		{
			return default(T);
		}
	}
}
