using System;
using System.Runtime.CompilerServices;

namespace Poly.Base
{
	public class FastList<T>
	{
		public T[] array;

		private int size;

		private int capacity;

		public int Count
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return size;
			}
		}

		public int Capacity
		{
			get
			{
				return capacity;
			}
			set
			{
				SetCapacity(value);
			}
		}

		public ref T this[int idx]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref array[idx];
			}
		}

		public FastList(short capacity = 16)
		{
			array = null;
			size = 0;
			this.capacity = capacity;
			SetCapacity(capacity);
		}

		private void SetCapacity(int newCapacity)
		{
			if (array != null && size > 0)
			{
				Array.Resize(ref array, newCapacity);
				capacity = newCapacity;
			}
			else
			{
				array = new T[newCapacity];
				capacity = newCapacity;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void _SetCapacityMore(int newCapacity)
		{
			Array.Resize(ref array, newCapacity);
			capacity = newCapacity;
		}

		public void SetSize(int newSize, int additionalCapacityIfReallocating = -1)
		{
			Reserve(newSize, additionalCapacityIfReallocating);
			size = newSize;
		}

		public ref T ExpandOne()
		{
			SetSize(size + 1);
			return ref array[size - 1];
		}

		public void Reserve(int minCapacity, int additionalCapacityIfReallocating = -1)
		{
			if (capacity < minCapacity)
			{
				if (additionalCapacityIfReallocating < 0)
				{
					additionalCapacityIfReallocating = 32 - minCapacity % 16;
				}
				SetCapacity(minCapacity + additionalCapacityIfReallocating);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void _ReserveMore(int minCapacity, int additionalCapacityIfReallocating)
		{
			if (capacity < minCapacity)
			{
				_SetCapacityMore(minCapacity + additionalCapacityIfReallocating);
			}
		}

		public void Clear()
		{
			SetSize(0);
		}

		public void Add(in T element)
		{
			if (size == capacity)
			{
				int num = System.Math.Max(16, 2 * capacity);
				SetCapacity(num);
			}
			array[size++] = element;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Add_Unchecked(in T element)
		{
			array[size++] = element;
		}

		public bool Remove_Slow(in T element)
		{
			bool result = false;
			for (int i = 0; i < size; i++)
			{
				ref readonly T reference = ref array[i];
				object obj = element;
				if (reference.Equals(obj))
				{
					RemoveAt_Slow(i);
					result = true;
					break;
				}
			}
			return result;
		}

		public void RemoveAt_Slow(int index)
		{
			size--;
			for (int i = index; i < size; i++)
			{
				array[i] = array[i + 1];
			}
		}

		public T RemoveAtAndSwap(int removeIndex)
		{
			int num = Count - 1;
			T result = (array[removeIndex] = array[num]);
			RemoveAt_Slow(num);
			return result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref T _RemoveAtAndSwap_Faster_Unchecked(int removeIndex)
		{
			int num = Count - 1;
			array[removeIndex] = array[num];
			size--;
			return ref array[removeIndex];
		}
	}
}
