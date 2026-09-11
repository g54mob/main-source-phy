using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct ThreadSharedObject<T> : ThreadSharedObject<T>.Interface, IUpCastable<ThreadSharedObject<T>>, SharedObject<T>.Interface, IUpCastable<SharedObject<T>>
	{
		public interface Interface : IUpCastable<ThreadSharedObject<T>>, SharedObject<T>.Interface, IUpCastable<SharedObject<T>>
		{
		}

		[BaseField]
		private SharedObject<T> __SharedObject;

		ref ThreadSharedObject<T> IUpCastable<ThreadSharedObject<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref SharedObject<T> IUpCastable<SharedObject<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __SharedObject, 1));
		}
	}
}
