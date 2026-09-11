using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct SharedObject<T> : SharedObject<T>.Interface, IUpCastable<SharedObject<T>>
	{
		public interface Interface : IUpCastable<SharedObject<T>>
		{
		}

		public MemLabelId m_MemLabel;

		public int m_RefCount;

		ref SharedObject<T> IUpCastable<SharedObject<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
