using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[Serializable]
	public abstract class MMReorderableArray<T> : ICloneable, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
	{
		[SerializeField]
		private List<T> array;

		public T this[int index]
		{
			get
			{
				return default(T);
			}
			set
			{
			}
		}

		public int Length => 0;

		public bool IsReadOnly => false;

		public int Count => 0;

		public MMReorderableArray()
		{
		}

		public MMReorderableArray(int length)
		{
		}

		public object Clone()
		{
			return null;
		}

		public void CopyFrom(IEnumerable<T> value)
		{
		}

		public bool Contains(T value)
		{
			return false;
		}

		public int IndexOf(T value)
		{
			return 0;
		}

		public void Insert(int index, T item)
		{
		}

		public void RemoveAt(int index)
		{
		}

		public void Add(T item)
		{
		}

		public void Clear()
		{
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
		}

		public bool Remove(T item)
		{
			return false;
		}

		public T[] ToArray()
		{
			return null;
		}

		public IEnumerator<T> GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}
}
