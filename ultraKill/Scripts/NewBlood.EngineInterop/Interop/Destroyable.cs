using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct Destroyable : Destroyable.Interface, IUpCastable<Destroyable>
	{
		public struct Vtbl<T> where T : unmanaged, Interface
		{
			public unsafe delegate* unmanaged[Thiscall]<T*, void> __dtor;
		}

		public interface Interface : IUpCastable<Destroyable>
		{
		}

		public unsafe void** __vftable;

		ref Destroyable IUpCastable<Destroyable>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
