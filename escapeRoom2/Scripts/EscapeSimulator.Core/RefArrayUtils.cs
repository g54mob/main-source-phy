using System;

internal static class RefArrayUtils
{
	internal static T[] ToArray<T>(int length, Func<int, T> extractor)
	{
		T[] array = new T[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = extractor(i);
		}
		return array;
	}
}
