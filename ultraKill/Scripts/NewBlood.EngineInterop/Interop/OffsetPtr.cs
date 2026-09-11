namespace Interop
{
	public struct OffsetPtr<T> where T : unmanaged
	{
		public nuint m_Offset;

		public readonly bool IsNull()
		{
			return m_Offset == 0;
		}

		public unsafe readonly T* Get()
		{
			if (m_Offset == 0)
			{
				return null;
			}
			fixed (OffsetPtr<T>* ptr = &this)
			{
				return (T*)((byte*)ptr + m_Offset);
			}
		}
	}
}
