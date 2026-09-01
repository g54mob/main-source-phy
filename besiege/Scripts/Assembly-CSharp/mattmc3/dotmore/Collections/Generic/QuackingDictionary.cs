using System;
using System.Collections;
using System.Collections.Generic;

namespace mattmc3.dotmore.Collections.Generic
{
	public class QuackingDictionary<TKey, TValue> : IEnumerable, IDictionary, ICollection, ICollection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
	{
		private IDictionary<TKey, TValue> _dict;

		ICollection<TKey> IDictionary<TKey, TValue>.Keys
		{
			get
			{
				return Keys;
			}
		}

		ICollection<TValue> IDictionary<TKey, TValue>.Values
		{
			get
			{
				return Values;
			}
		}

		TValue IDictionary<TKey, TValue>.this[TKey key]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		int ICollection<KeyValuePair<TKey, TValue>>.Count
		{
			get
			{
				return Count;
			}
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool IDictionary.IsFixedSize
		{
			get
			{
				return GetIDictionary().IsFixedSize;
			}
		}

		bool IDictionary.IsReadOnly
		{
			get
			{
				return GetIDictionary().IsReadOnly;
			}
		}

		ICollection IDictionary.Keys
		{
			get
			{
				return GetIDictionary().Keys;
			}
		}

		ICollection IDictionary.Values
		{
			get
			{
				return GetIDictionary().Values;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				IDictionary iDictionary = GetIDictionary();
				if (iDictionary.Contains(key))
				{
					return iDictionary[key];
				}
				return null;
			}
			set
			{
				IDictionary iDictionary = GetIDictionary();
				if (iDictionary.Contains(key))
				{
					iDictionary[key] = value;
				}
				else
				{
					iDictionary.Add(key, value);
				}
			}
		}

		int ICollection.Count
		{
			get
			{
				return Count;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return GetIDictionary().IsSynchronized;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				return GetIDictionary().SyncRoot;
			}
		}

		public int Count
		{
			get
			{
				return _dict.Count;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				TValue value;
				if (_dict.TryGetValue(key, out value))
				{
					return value;
				}
				return default(TValue);
			}
			set
			{
				if (_dict.ContainsKey(key))
				{
					_dict[key] = value;
				}
				else
				{
					_dict.Add(key, value);
				}
			}
		}

		public ICollection<TKey> Keys
		{
			get
			{
				return _dict.Keys;
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				return _dict.Values;
			}
		}

		public QuackingDictionary()
		{
			_dict = new Dictionary<TKey, TValue>();
		}

		public QuackingDictionary(IEqualityComparer<TKey> comparer)
		{
			_dict = new Dictionary<TKey, TValue>(comparer);
		}

		public QuackingDictionary(IDictionary<TKey, TValue> storageDictionary)
		{
			if (storageDictionary == null)
			{
				throw new ArgumentNullException("storageDictionary", "The dictionary to use as storage cannot be null.");
			}
			_dict = storageDictionary;
		}

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			this[key] = value;
		}

		bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
		{
			return ContainsKey(key);
		}

		bool IDictionary<TKey, TValue>.Remove(TKey key)
		{
			return Remove(key);
		}

		bool IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
		{
			return TryGetValue(key, out value);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
		{
			this[item.Key] = item.Value;
		}

		void ICollection<KeyValuePair<TKey, TValue>>.Clear()
		{
			Clear();
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
		{
			return Contains(item.Key, item.Value);
		}

		void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			_dict.CopyTo(array, arrayIndex);
		}

		bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
		{
			return _dict.Remove(item);
		}

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void IDictionary.Add(object key, object value)
		{
			IDictionary iDictionary = GetIDictionary();
			if (iDictionary.Contains(key))
			{
				iDictionary[key] = value;
			}
			else
			{
				iDictionary.Add(key, value);
			}
		}

		void IDictionary.Clear()
		{
			Clear();
		}

		bool IDictionary.Contains(object key)
		{
			return GetIDictionary().Contains(key);
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return GetIDictionary().GetEnumerator();
		}

		void IDictionary.Remove(object key)
		{
			if (_dict.ContainsKey((TKey)key))
			{
				_dict.Remove((TKey)key);
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			GetIDictionary().CopyTo(array, index);
		}

		public void Clear()
		{
			_dict.Clear();
		}

		public bool Contains(TKey key, TValue value)
		{
			return _dict.Contains(new KeyValuePair<TKey, TValue>(key, value));
		}

		public bool ContainsKey(TKey key)
		{
			return _dict.ContainsKey(key);
		}

		public bool Remove(TKey key)
		{
			return _dict.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dict.TryGetValue(key, out value);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _dict.GetEnumerator();
		}

		private IDictionary GetIDictionary()
		{
			return (IDictionary)_dict;
		}
	}
}
