using System.Runtime.InteropServices;

namespace Interop
{
	public struct WeakPtr<T> where T : unmanaged
	{
		[SupportsInheritance]
		public struct SharedData : SharedData.Interface, IUpCastable<SharedData>, WeakPtrSharedData.Interface, IUpCastable<WeakPtrSharedData>, ThreadSharedObject<WeakPtrSharedData>.Interface, IUpCastable<ThreadSharedObject<WeakPtrSharedData>>, SharedObject<WeakPtrSharedData>.Interface, IUpCastable<SharedObject<WeakPtrSharedData>>
		{
			public interface Interface : IUpCastable<SharedData>, WeakPtrSharedData.Interface, IUpCastable<WeakPtrSharedData>, ThreadSharedObject<WeakPtrSharedData>.Interface, IUpCastable<ThreadSharedObject<WeakPtrSharedData>>, SharedObject<WeakPtrSharedData>.Interface, IUpCastable<SharedObject<WeakPtrSharedData>>
			{
			}

			[BaseField]
			private WeakPtrSharedData __WeakPtrSharedData;

			public unsafe T* m_Ptr;

			ref SharedData IUpCastable<SharedData>.Cast()
			{
				return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
			}

			ref WeakPtrSharedData IUpCastable<WeakPtrSharedData>.Cast()
			{
				return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __WeakPtrSharedData, 1));
			}

			ref ThreadSharedObject<WeakPtrSharedData> IUpCastable<ThreadSharedObject<WeakPtrSharedData>>.Cast()
			{
				return ref CastOperations.UpCast<WeakPtrSharedData, ThreadSharedObject<WeakPtrSharedData>>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __WeakPtrSharedData, 1)));
			}

			ref SharedObject<WeakPtrSharedData> IUpCastable<SharedObject<WeakPtrSharedData>>.Cast()
			{
				return ref CastOperations.UpCast<WeakPtrSharedData, SharedObject<WeakPtrSharedData>>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __WeakPtrSharedData, 1)));
			}
		}

		public unsafe SharedData* m_SharedData;
	}
}
