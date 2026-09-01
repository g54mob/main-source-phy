using System;
using System.Collections.Generic;

namespace mattmc3.dotmore.Collections.Generic
{
	internal class KeyValueComparisonHelper<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
	{
		private IComparer<TKey> _keyComparer;

		private IComparer<TValue> _valueComparer;

		private Comparison<TKey> _keyComparisonMethod;

		private Comparison<TValue> _valueComparisonMethod;

		private KeyValueComparisonHelper(IComparer<TKey> keyComparer, IComparer<TValue> valueComparer)
		{
			_keyComparer = keyComparer;
			_valueComparer = valueComparer;
		}

		private KeyValueComparisonHelper(Comparison<TKey> keyComparisonMethod, Comparison<TValue> valueComparisonMethod)
		{
			_keyComparisonMethod = keyComparisonMethod;
			_valueComparisonMethod = valueComparisonMethod;
		}

		public static int DefaultKeyComparison(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
		{
			IComparable<TKey> comparable = (IComparable<TKey>)(object)x.Key;
			return comparable.CompareTo(y.Key);
		}

		public static int DefaultValueComparison(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
		{
			IComparable<TValue> comparable = (IComparable<TValue>)(object)x.Value;
			return comparable.CompareTo(y.Value);
		}

		public static KeyValueComparisonHelper<TKey, TValue> GetInstanceOfKeyComparer(IComparer<TKey> keyComparer)
		{
			return new KeyValueComparisonHelper<TKey, TValue>(keyComparer, null);
		}

		public static KeyValueComparisonHelper<TKey, TValue> GetInstanceOfValueComparer(IComparer<TValue> valueComparer)
		{
			return new KeyValueComparisonHelper<TKey, TValue>(null, valueComparer);
		}

		public static KeyValueComparisonHelper<TKey, TValue> GetInstanceOfDelegatedKeyComparer(Comparison<TKey> keyComparison)
		{
			return new KeyValueComparisonHelper<TKey, TValue>(keyComparison, null);
		}

		public static KeyValueComparisonHelper<TKey, TValue> GetInstanceOfDelegatedValueComparer(Comparison<TValue> valueComparison)
		{
			return new KeyValueComparisonHelper<TKey, TValue>(null, valueComparison);
		}

		public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
		{
			if (_keyComparer != null)
			{
				return _keyComparer.Compare(x.Key, y.Key);
			}
			return _valueComparer.Compare(x.Value, y.Value);
		}

		public int DelegatedCompare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
		{
			if (_keyComparisonMethod != null)
			{
				return _keyComparisonMethod(x.Key, y.Key);
			}
			return _valueComparisonMethod(x.Value, y.Value);
		}
	}
}
