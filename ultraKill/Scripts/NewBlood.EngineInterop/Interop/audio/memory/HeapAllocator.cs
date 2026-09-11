using System.Runtime.InteropServices;

namespace Interop.audio.memory
{
	[SupportsInheritance]
	public struct HeapAllocator : HeapAllocator.Interface, IUpCastable<HeapAllocator>, RuntimeBaseAllocator.Interface, IUpCastable<RuntimeBaseAllocator>
	{
		public interface Interface : IUpCastable<HeapAllocator>, RuntimeBaseAllocator.Interface, IUpCastable<RuntimeBaseAllocator>
		{
		}

		[BaseField]
		private RuntimeBaseAllocator __RuntimeBaseAllocator;

		public MemLabelId m_Label;

		ref HeapAllocator IUpCastable<HeapAllocator>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref RuntimeBaseAllocator IUpCastable<RuntimeBaseAllocator>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __RuntimeBaseAllocator, 1));
		}
	}
}
