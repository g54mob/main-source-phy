namespace Interop
{
	public struct ImmediatePtr
	{
		public unsafe void* m_Ptr;
	}
	public struct ImmediatePtr<T> where T : unmanaged
	{
		public unsafe T* m_Ptr;
	}
}
