using System.Runtime.InteropServices;

namespace Interop.core.impl
{
	[SupportsInheritance]
	public struct allocator_shared<T> : allocator_shared<T>.Interface, IUpCastable<allocator_shared<T>>
	{
		public interface Interface : IUpCastable<allocator_shared<T>>
		{
		}

		public MemLabelId m_label;

		ref allocator_shared<T> IUpCastable<allocator_shared<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
