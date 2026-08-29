using System;
using System.Collections.Generic;
using UnityEngine;

public class ContestedResource<TEnum, TValue> where TEnum : Enum
{
	private readonly List<TEnum> enumTypes = new List<TEnum>(8);

	private readonly List<int> enumValues = new List<int>(8);

	private readonly List<TValue> values = new List<TValue>(8);

	private readonly List<int> timings = new List<int>(8);

	public void set(TEnum type, TValue newValue, bool oneFrame = false)
	{
		for (int i = 0; i < enumTypes.Count; i++)
		{
			if (EqualityComparer<TEnum>.Default.Equals(enumTypes[i], type))
			{
				values[i] = newValue;
				timings[i] = (oneFrame ? Time.frameCount : (-1));
				return;
			}
		}
		enumTypes.Add(type);
		enumValues.Add((int)(object)type);
		values.Add(newValue);
		timings.Add(oneFrame ? Time.frameCount : (-1));
	}

	public bool unset(TEnum type)
	{
		for (int i = 0; i < enumTypes.Count; i++)
		{
			if (EqualityComparer<TEnum>.Default.Equals(enumTypes[i], type))
			{
				enumTypes.RemoveAt(i);
				enumValues.RemoveAt(i);
				values.RemoveAt(i);
				timings.RemoveAt(i);
				return true;
			}
		}
		return false;
	}

	public TValue value()
	{
		int num = calculateBestIndex();
		if (num < 0)
		{
			return default(TValue);
		}
		return values[num];
	}

	public TEnum valueType()
	{
		int num = calculateBestIndex();
		if (num < 0)
		{
			return default(TEnum);
		}
		return enumTypes[num];
	}

	private int calculateBestIndex()
	{
		if (enumTypes.Count == 0)
		{
			return -1;
		}
		int num = 0;
		for (int i = 0; i < enumTypes.Count; i++)
		{
			if (enumValues[i] > enumValues[num] && (timings[i] < 0 || timings[i] == Time.frameCount))
			{
				num = i;
			}
		}
		return num;
	}
}
