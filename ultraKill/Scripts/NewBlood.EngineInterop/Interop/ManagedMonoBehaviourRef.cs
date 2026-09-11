using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct ManagedMonoBehaviourRef : ManagedMonoBehaviourRef.Interface, IUpCastable<ManagedMonoBehaviourRef>, ObjectStoredSerializableManagedRef.Interface, IUpCastable<ObjectStoredSerializableManagedRef>, SerializableManagedRef.Interface, IUpCastable<SerializableManagedRef>
	{
		public interface Interface : IUpCastable<ManagedMonoBehaviourRef>, ObjectStoredSerializableManagedRef.Interface, IUpCastable<ObjectStoredSerializableManagedRef>, SerializableManagedRef.Interface, IUpCastable<SerializableManagedRef>
		{
		}

		[BaseField]
		private ObjectStoredSerializableManagedRef __ObjectStoredSerializableManagedRef;

		[NativeTypeName("ScriptingMethodPtr const *")]
		public unsafe ScriptingMethodPtr* m_CachedMethods;

		ref ManagedMonoBehaviourRef IUpCastable<ManagedMonoBehaviourRef>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref ObjectStoredSerializableManagedRef IUpCastable<ObjectStoredSerializableManagedRef>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ObjectStoredSerializableManagedRef, 1));
		}

		ref SerializableManagedRef IUpCastable<SerializableManagedRef>.Cast()
		{
			return ref CastOperations.UpCast<ObjectStoredSerializableManagedRef, SerializableManagedRef>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ObjectStoredSerializableManagedRef, 1)));
		}
	}
}
