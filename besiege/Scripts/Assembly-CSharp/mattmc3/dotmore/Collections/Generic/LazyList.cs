using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace mattmc3.dotmore.Collections.Generic
{
	public class LazyList<T> : IEnumerable, ICollection<T>, IList<T>, IEnumerable<T>
	{
		private IQueryable<T> _query;

		private IList<T> _inner;

		protected IList<T> InnerList
		{
			get
			{
				if (_inner == null)
				{
					_inner = _query.ToList();
				}
				return _inner;
			}
		}

		public T this[int index]
		{
			get
			{
				return InnerList[index];
			}
			set
			{
				InnerList[index] = value;
			}
		}

		public int Count
		{
			get
			{
				return InnerList.Count;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return InnerList.IsReadOnly;
			}
		}

		public LazyList()
		{
			_inner = new List<T>();
		}

		public LazyList(IQueryable<T> query)
		{
			_query = query;
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return InnerList.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return InnerList.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			InnerList.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			InnerList.RemoveAt(index);
		}

		public void Add(T item)
		{
			_inner = _inner ?? new List<T>();
			InnerList.Add(item);
		}

		public void Add(object ob)
		{
			throw new NotImplementedException("This is for serialization");
		}

		public void Clear()
		{
			if (_inner != null)
			{
				InnerList.Clear();
			}
		}

		public bool Contains(T item)
		{
			return InnerList.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			InnerList.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return InnerList.Remove(item);
		}

		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)InnerList).GetEnumerator();
		}
	}
}
