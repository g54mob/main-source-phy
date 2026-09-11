namespace Interop
{
	public struct PPtr
	{
		public int m_InstanceID;
	}
	public struct PPtr<T> where T : Object.Interface
	{
		public int m_InstanceID;
	}
}
