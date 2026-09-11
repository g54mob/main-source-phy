using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct RuntimeBaseAllocator : RuntimeBaseAllocator.Interface, IUpCastable<RuntimeBaseAllocator>
	{
		public struct Vtbl<T> where T : unmanaged, Interface
		{
			public unsafe delegate* unmanaged[Thiscall]<T*, nuint, nuint, void*> Allocate;

			public unsafe delegate* unmanaged[Thiscall]<T*, void*> Deallocate;
		}

		public interface Interface : IUpCastable<RuntimeBaseAllocator>
		{
		}

		public unsafe void** __vftable;

		ref RuntimeBaseAllocator IUpCastable<RuntimeBaseAllocator>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
