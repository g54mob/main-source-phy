using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop.mecanim.memory
{
	[NonCopyable]
	[SupportsInheritance]
	public struct ChainedAllocator : ChainedAllocator.Interface, IUpCastable<ChainedAllocator>, RuntimeBaseAllocator.Interface, IUpCastable<RuntimeBaseAllocator>
	{
		public struct MemoryBlock
		{
			public unsafe MemoryBlock* next;

			public unsafe byte* headPtr;

			public nuint blockSize;
		}

		public struct Vtbl<T> where T : unmanaged, Interface
		{
			public RuntimeBaseAllocator.Vtbl<T> __base;

			public unsafe delegate* unmanaged[Thiscall]<T*, void> Reset;

			[NativeTypeName("bool (uint64_t const &, uint64_t const &)")]
			public unsafe delegate* unmanaged[Thiscall]<T*, nuint*, nuint*, byte> AllocateNewBlock;
		}

		public interface Interface : IUpCastable<ChainedAllocator>, RuntimeBaseAllocator.Interface, IUpCastable<RuntimeBaseAllocator>
		{
		}

		[BaseField]
		private RuntimeBaseAllocator __RuntimeBaseAllocator;

		public unsafe MemoryBlock* first;

		public unsafe MemoryBlock* current;

		public unsafe byte* heapPtr;

		public nuint blockSize;

		public MemLabelId label;

		ref ChainedAllocator IUpCastable<ChainedAllocator>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref RuntimeBaseAllocator IUpCastable<RuntimeBaseAllocator>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __RuntimeBaseAllocator, 1));
		}
	}
}
