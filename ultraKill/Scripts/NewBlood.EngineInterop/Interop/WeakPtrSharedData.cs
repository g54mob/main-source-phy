using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct WeakPtrSharedData : WeakPtrSharedData.Interface, IUpCastable<WeakPtrSharedData>, ThreadSharedObject<WeakPtrSharedData>.Interface, IUpCastable<ThreadSharedObject<WeakPtrSharedData>>, SharedObject<WeakPtrSharedData>.Interface, IUpCastable<SharedObject<WeakPtrSharedData>>
	{
		public interface Interface : IUpCastable<WeakPtrSharedData>, ThreadSharedObject<WeakPtrSharedData>.Interface, IUpCastable<ThreadSharedObject<WeakPtrSharedData>>, SharedObject<WeakPtrSharedData>.Interface, IUpCastable<SharedObject<WeakPtrSharedData>>
		{
		}

		[BaseField]
		private ThreadSharedObject<WeakPtrSharedData> __ThreadSharedObject;

		ref WeakPtrSharedData IUpCastable<WeakPtrSharedData>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref ThreadSharedObject<WeakPtrSharedData> IUpCastable<ThreadSharedObject<WeakPtrSharedData>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ThreadSharedObject, 1));
		}

		ref SharedObject<WeakPtrSharedData> IUpCastable<SharedObject<WeakPtrSharedData>>.Cast()
		{
			return ref CastOperations.UpCast<ThreadSharedObject<WeakPtrSharedData>, SharedObject<WeakPtrSharedData>>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ThreadSharedObject, 1)));
		}
	}
}
