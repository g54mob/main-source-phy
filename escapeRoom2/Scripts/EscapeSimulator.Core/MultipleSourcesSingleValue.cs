using System;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSourcesSingleValue<T, TEnum> where TEnum : Enum
{
	public class SourceData
	{
		public T value;

		public TEnum source;
	}

	public List<SourceData> sources = new List<SourceData>();

	public void addSource(T value, TEnum source)
	{
		if (sources.Find(delegate(SourceData x)
		{
			ref TEnum source2 = ref x.source;
			object obj = source;
			return source2.Equals(obj);
		}) != null)
		{
			Debug.Log($"Source {source} already exists");
			return;
		}
		sources.Add(new SourceData
		{
			value = value,
			source = source
		});
	}

	public void removeSource(TEnum source)
	{
		sources.RemoveAll(delegate(SourceData x)
		{
			ref TEnum source2 = ref x.source;
			object obj = source;
			return source2.Equals(obj);
		});
	}

	public T getValue(MultipleSourcesGetStrategy strategy)
	{
		switch (strategy)
		{
		case MultipleSourcesGetStrategy.HasAny:
			if (sources.Count > 0)
			{
				return sources[0].value;
			}
			break;
		case MultipleSourcesGetStrategy.Max:
		{
			if (sources.Count <= 0)
			{
				break;
			}
			T value2 = sources[0].value;
			{
				foreach (SourceData source in sources)
				{
					if (Comparer<T>.Default.Compare(source.value, value2) > 0)
					{
						value2 = source.value;
					}
				}
				return value2;
			}
		}
		case MultipleSourcesGetStrategy.Min:
		{
			if (sources.Count <= 0)
			{
				break;
			}
			T value = sources[0].value;
			{
				foreach (SourceData source2 in sources)
				{
					if (Comparer<T>.Default.Compare(source2.value, value) < 0)
					{
						value = source2.value;
					}
				}
				return value;
			}
		}
		}
		return default(T);
	}
}
