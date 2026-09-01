using System.Collections.Generic;

namespace MoreMountains.Tools
{
	public class MMCircularList<T> : List<T>
	{
		private int _currentIndex;

		public int CurrentIndex
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public virtual T Current => default(T);

		public virtual int PreviousIndex => 0;

		public virtual int NextIndex => 0;

		protected virtual int GetCurrentIndex()
		{
			return 0;
		}

		public virtual void IncrementCurrentIndex()
		{
		}

		public virtual void DecrementCurrentIndex()
		{
		}
	}
}
