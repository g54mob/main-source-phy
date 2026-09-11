using System.Runtime.InteropServices;
using Interop.core.impl;

namespace Interop.core
{
	[SupportsInheritance]
	public struct allocator<T> : allocator<T>.Interface, IUpCastable<allocator<T>>, allocator_shared<T>.Interface, IUpCastable<allocator_shared<T>>
	{
		public interface Interface : IUpCastable<allocator<T>>, allocator_shared<T>.Interface, IUpCastable<allocator_shared<T>>
		{
		}

		[BaseField]
		private allocator_shared<T> __allocator_shared;

		ref allocator<T> IUpCastable<allocator<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref allocator_shared<T> IUpCastable<allocator_shared<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __allocator_shared, 1));
		}
	}
}
