using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GLTFast
{
	internal class FlatArray<T>
	{
		private readonly T[] m_Array;

		private readonly int[] m_Indices;

		public int Length => m_Array.Length;

		public T this[int key] => m_Array[key];

		public FlatArray(int[] indices)
		{
			m_Indices = indices;
			int num = indices[^1];
			m_Array = new T[num];
		}

		public int GetLength(int primaryIndex)
		{
			int num = m_Indices[primaryIndex];
			return m_Indices[primaryIndex + 1] - num;
		}

		public T GetValue(int primaryIndex, int secondaryIndex)
		{
			int index = GetIndex(primaryIndex, secondaryIndex);
			return m_Array[index];
		}

		public void SetValue(int primaryIndex, int secondaryIndex, T value)
		{
			int index = GetIndex(primaryIndex, secondaryIndex);
			m_Array[index] = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private int GetIndex(int primaryIndex, int secondaryIndex)
		{
			int num = m_Indices[primaryIndex];
			_ = m_Indices[primaryIndex + 1];
			return num + secondaryIndex;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void GetIndexRange(int primaryIndex, out int start, out int end)
		{
			start = m_Indices[primaryIndex];
			end = m_Indices[primaryIndex + 1];
		}

		public IEnumerable<T> Values(int primaryIndex)
		{
			GetIndexRange(primaryIndex, out var start, out var end);
			for (int i = start; i < end; i++)
			{
				yield return m_Array[i];
			}
		}
	}
}
