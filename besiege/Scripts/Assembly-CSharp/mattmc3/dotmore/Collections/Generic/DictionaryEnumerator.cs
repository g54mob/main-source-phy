using System;
using System.Collections;
using System.Collections.Generic;

namespace mattmc3.dotmore.Collections.Generic
{
	public class DictionaryEnumerator<TKey, TValue> : IDisposable, IEnumerator, IDictionaryEnumerator
	{
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _impl;

		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<TKey, TValue> current = _impl.Current;
				return new DictionaryEntry(current.Key, current.Value);
			}
		}

		public object Key
		{
			get
			{
				return _impl.Current.Key;
			}
		}

		public object Value
		{
			get
			{
				return _impl.Current.Value;
			}
		}

		public object Current
		{
			get
			{
				return Entry;
			}
		}

		public DictionaryEnumerator(IDictionary<TKey, TValue> value)
		{
			_impl = value.GetEnumerator();
		}

		public void Dispose()
		{
			_impl.Dispose();
		}

		public void Reset()
		{
			_impl.Reset();
		}

		public bool MoveNext()
		{
			return _impl.MoveNext();
		}
	}
}
