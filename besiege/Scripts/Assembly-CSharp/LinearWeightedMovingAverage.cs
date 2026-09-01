using System;
using System.Collections.Generic;

[Serializable]
public class LinearWeightedMovingAverage
{
	private int capacity;

	private double average;

	private Queue<double> values;

	public double Average
	{
		get
		{
			return average;
		}
	}

	public LinearWeightedMovingAverage()
	{
		values = new Queue<double>(5);
		capacity = 5;
	}

	public LinearWeightedMovingAverage(int size)
	{
		values = new Queue<double>(size);
		capacity = size;
	}

	public void Add(double value)
	{
		if (values.Count == capacity)
		{
			values.Dequeue();
		}
		values.Enqueue(value);
		CalculateAverage();
	}

	public void Clear()
	{
		values.Clear();
	}

	private void CalculateAverage()
	{
		double num = 0.0;
		int num2 = 0;
		int num3 = 1;
		double[] array = values.ToArray();
		for (int i = 0; i < values.Count; i++)
		{
			num += array[i] * (double)num3;
			num2 += num3;
			num3++;
		}
		average = num / (double)num2;
	}

	public static LinearWeightedMovingAverage operator +(LinearWeightedMovingAverage c1, double c2)
	{
		c1.Add(c2);
		return c1;
	}
}
