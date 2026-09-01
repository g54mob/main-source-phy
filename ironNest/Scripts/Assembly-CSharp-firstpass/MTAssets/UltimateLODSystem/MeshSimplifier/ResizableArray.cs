using System.Runtime.CompilerServices;

namespace MTAssets.UltimateLODSystem.MeshSimplifier
{
	internal sealed class ResizableArray<T>
	{
		private T[] items;

		private int length;

		private static T[] emptyArr;

		public int Length
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0;
			}
		}

		public T[] Data
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return null;
			}
		}

		public T this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return default(T);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
			}
		}

		public ResizableArray(int capacity)
		{
		}

		public ResizableArray(int capacity, int length)
		{
		}

		public ResizableArray(T[] initialArray)
		{
		}

		private void IncreaseCapacity(int capacity)
		{
		}

		public void Clear()
		{
		}

		public void Resize(int length, bool trimExess = false, bool clearMemory = false)
		{
		}

		public void TrimExcess()
		{
		}

		public void Add(T item)
		{
		}

		public T[] ToArray()
		{
			return null;
		}
	}
}
