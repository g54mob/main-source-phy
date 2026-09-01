using System.Collections.Generic;

namespace Poly.Physics.Viewers.Game
{
	public class LoopedList<T> : List<T>
	{
		public int maxSize;

		private int lastAddedIndex = -1;

		public LoopedList(int maxSize = 16)
			: base(maxSize)
		{
			this.maxSize = maxSize;
		}

		public new void Add(T elem)
		{
			if (base.Count == maxSize)
			{
				lastAddedIndex = (lastAddedIndex + 1) % maxSize;
				base[lastAddedIndex] = elem;
			}
			else
			{
				lastAddedIndex = base.Count;
				base.Add(elem);
			}
		}
	}
}
