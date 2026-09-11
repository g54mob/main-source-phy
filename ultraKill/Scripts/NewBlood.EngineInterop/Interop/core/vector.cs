namespace Interop.core
{
	public struct vector<T> where T : unmanaged
	{
		private const ulong k_reference_bit = 1uL;

		private const int k_capacity_shift = 1;

		public unsafe T* m_ptr;

		public allocator<T> m_allocator;

		public nuint m_size;

		public nuint m_capacity;

		public unsafe readonly ref T this[nuint index] => ref m_ptr[index];

		public unsafe readonly ref T this[nint index] => ref m_ptr[index];

		public unsafe readonly T* data()
		{
			return m_ptr;
		}

		public readonly nuint size()
		{
			return m_size;
		}

		public readonly nuint capacity()
		{
			return (m_capacity & (nuint)(~(nint)1)) >> 1;
		}
	}
	public struct vector<T, Allocator> where T : unmanaged where Allocator : unmanaged
	{
		private const ulong k_reference_bit = 1uL;

		private const int k_capacity_shift = 1;

		public unsafe T* m_ptr;

		public Allocator m_allocator;

		public nuint m_size;

		public nuint m_capacity;

		public unsafe readonly ref T this[nuint index] => ref m_ptr[index];

		public unsafe readonly ref T this[nint index] => ref m_ptr[index];

		public unsafe readonly T* data()
		{
			return m_ptr;
		}

		public readonly nuint size()
		{
			return m_size;
		}

		public readonly nuint capacity()
		{
			return (m_capacity & (nuint)(~(nint)1)) >> 1;
		}
	}
}
