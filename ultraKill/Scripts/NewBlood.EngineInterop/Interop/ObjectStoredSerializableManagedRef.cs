using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct ObjectStoredSerializableManagedRef : ObjectStoredSerializableManagedRef.Interface, IUpCastable<ObjectStoredSerializableManagedRef>, SerializableManagedRef.Interface, IUpCastable<SerializableManagedRef>
	{
		public interface Interface : IUpCastable<ObjectStoredSerializableManagedRef>, SerializableManagedRef.Interface, IUpCastable<SerializableManagedRef>
		{
		}

		[BaseField]
		private SerializableManagedRef __SerializableManagedRef;

		ref ObjectStoredSerializableManagedRef IUpCastable<ObjectStoredSerializableManagedRef>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref SerializableManagedRef IUpCastable<SerializableManagedRef>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __SerializableManagedRef, 1));
		}
	}
}
