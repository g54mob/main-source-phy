using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct AsyncOperation : AsyncOperation.Interface, IUpCastable<AsyncOperation>, ThreadSharedObject<AsyncOperation>.Interface, IUpCastable<ThreadSharedObject<AsyncOperation>>, SharedObject<AsyncOperation>.Interface, IUpCastable<SharedObject<AsyncOperation>>
	{
		public interface Interface : IUpCastable<AsyncOperation>, ThreadSharedObject<AsyncOperation>.Interface, IUpCastable<ThreadSharedObject<AsyncOperation>>, SharedObject<AsyncOperation>.Interface, IUpCastable<SharedObject<AsyncOperation>>
		{
		}

		public unsafe void** __vftable;

		[BaseField]
		private ThreadSharedObject<AsyncOperation> __ThreadSharedObject;

		public unsafe delegate* unmanaged[Stdcall]<Object*, void*, CallObjectState> m_CoroutineDone;

		public unsafe delegate* unmanaged[Stdcall]<void*, CallObjectState> m_CoroutineCleanup;

		public unsafe void* m_CoroutineData;

		public PPtr<Object> m_CoroutineBehaviour;

		public ScriptingGCHandle m_MonoReference;

		ref AsyncOperation IUpCastable<AsyncOperation>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref ThreadSharedObject<AsyncOperation> IUpCastable<ThreadSharedObject<AsyncOperation>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ThreadSharedObject, 1));
		}

		ref SharedObject<AsyncOperation> IUpCastable<SharedObject<AsyncOperation>>.Cast()
		{
			return ref CastOperations.UpCast<ThreadSharedObject<AsyncOperation>, SharedObject<AsyncOperation>>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ThreadSharedObject, 1)));
		}
	}
}
