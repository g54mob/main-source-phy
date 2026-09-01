using System.Collections.Generic;

namespace Kamgam.SettingsGenerator
{
	public class ExposedList<T> where T : class
	{
		public const int k_DefaultCapacity = 10;

		private const int k_MaxAutoIncrease = 1000;

		private int _capacity;

		public T[] Values;

		public int Capacity
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public ExposedList()
		{
		}

		public ExposedList(int capacity)
		{
		}

		public ExposedList(IList<T> list)
		{
		}

		protected void resizeTo(int newCapacity)
		{
		}

		protected void autoIncreaseCapacity()
		{
		}

		public void Clear()
		{
		}

		public void Add(T value)
		{
		}

		public void Add(IList<T> values)
		{
		}

		public void Remove(T value)
		{
		}

		protected bool Contains(T value)
		{
			return false;
		}
	}
}
