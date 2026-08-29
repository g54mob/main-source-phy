using System;
using System.Collections.Generic;

public class NonAllocSortBuffer<T>
{
	public readonly T[] elements;

	private readonly List<T> sortingList;

	private readonly Comparison<T> sortingComparison;

	public T this[int index]
	{
		get
		{
			return elements[index];
		}
		set
		{
			elements[index] = value;
		}
	}

	public NonAllocSortBuffer(int size, Comparison<T> sortingStrategy)
	{
		elements = new T[size];
		sortingList = new List<T>(size);
		sortingComparison = sortingStrategy;
	}

	public void sort(int count)
	{
		if (count > elements.Length)
		{
			count = elements.Length;
		}
		sortingList.Clear();
		for (int i = 0; i < count; i++)
		{
			sortingList.Add(elements[i]);
		}
		sortingList.Sort(sortingComparison);
		for (int j = 0; j < count; j++)
		{
			elements[j] = sortingList[j];
		}
	}
}
