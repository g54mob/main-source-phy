using System;
using System.Collections.Generic;
using UnityEngine;

public class RingBuffer<T>
{
	public T[] buffer;

	public int nextFree;

	public int rotationCount;

	public RingBuffer(int length)
	{
		buffer = new T[length];
		nextFree = 0;
		rotationCount = 0;
	}

	public void add(T item)
	{
		buffer[nextFree] = item;
		nextFree = (nextFree + 1) % buffer.Length;
		if (nextFree == 0)
		{
			rotationCount++;
		}
	}

	public void reset()
	{
		for (int i = 0; i < buffer.Length; i++)
		{
			buffer[i] = default(T);
		}
		nextFree = 0;
		rotationCount = 0;
	}

	public int itemCount()
	{
		return Math.Min(rotationCount * buffer.Length + nextFree, buffer.Length);
	}

	public List<T> getItemsInReverseOrder()
	{
		List<T> list = new List<T>();
		int num = ((nextFree == 0) ? (buffer.Length - 1) : (nextFree - 1));
		for (int i = 0; i < itemCount(); i++)
		{
			list.Add(buffer[num]);
			num = ((num == 0) ? (buffer.Length - 1) : (num - 1));
		}
		return list;
	}

	public T getOldestItem()
	{
		if (rotationCount == 0 && nextFree == 0)
		{
			return default(T);
		}
		int num = (nextFree + 1) % buffer.Length;
		return buffer[num];
	}
}
public static class RingBuffer
{
	private static readonly Comparison<Vector3> MAGNITUDE_COMPARER = (Vector3 v1, Vector3 v2) => v2.magnitude.CompareTo(v1.magnitude);

	private static readonly List<Vector3> REUSABLE_VECTOR_LIST = new List<Vector3>();

	public static Vector3 getAverageOfHighestN(this RingBuffer<Vector3> buffer, int highestN = 3)
	{
		Vector3 zero = Vector3.zero;
		Vector3[] buffer2 = buffer.buffer;
		int num = ((buffer.rotationCount == 0) ? buffer.nextFree : buffer2.Length);
		if (num == 0)
		{
			return zero;
		}
		int num2 = ((buffer.rotationCount != 0) ? buffer.nextFree : 0);
		List<Vector3> rEUSABLE_VECTOR_LIST = REUSABLE_VECTOR_LIST;
		rEUSABLE_VECTOR_LIST.Clear();
		for (int i = 0; i < num; i++)
		{
			int num3 = (num2 + i) % buffer2.Length;
			rEUSABLE_VECTOR_LIST.Add(buffer2[num3]);
		}
		rEUSABLE_VECTOR_LIST.Sort(MAGNITUDE_COMPARER);
		num = Mathf.Min(highestN, num);
		for (int j = 0; j < num; j++)
		{
			zero += rEUSABLE_VECTOR_LIST[j];
		}
		return zero / num;
	}

	public static Vector3 getAverage(this RingBuffer<Vector3> buffer, AnimationCurve weightAnimationCurve = null)
	{
		Vector3 zero = Vector3.zero;
		Vector3[] buffer2 = buffer.buffer;
		int num = ((buffer.rotationCount == 0) ? buffer.nextFree : buffer2.Length);
		if (num == 0)
		{
			return zero;
		}
		int num2 = ((buffer.rotationCount != 0) ? buffer.nextFree : 0);
		for (int i = 0; i < num; i++)
		{
			int num3 = (num2 + i) % buffer2.Length;
			float num4 = ((weightAnimationCurve == null) ? 1f : UnityUtils.mapOnCurve(0f, num, weightAnimationCurve, i));
			zero += buffer2[num3] * num4;
		}
		return zero / num;
	}

	public static float getAverage(this RingBuffer<float> buffer, AnimationCurve weightAnimationCurve)
	{
		float num = 0f;
		float[] buffer2 = buffer.buffer;
		int num2 = ((buffer.rotationCount == 0) ? buffer.nextFree : buffer2.Length);
		if (num2 == 0)
		{
			return num;
		}
		int num3 = ((buffer.rotationCount != 0) ? buffer.nextFree : 0);
		for (int i = 0; i < num2; i++)
		{
			int num4 = (num3 + i) % buffer2.Length;
			float num5 = UnityUtils.mapOnCurve(0f, num2, weightAnimationCurve, i);
			num += buffer2[num4] * num5;
		}
		return num / (float)num2;
	}
}
