using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct Mesh : Mesh.Interface, IUpCastable<Mesh>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Mesh>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private NamedObject __NamedObject;

		ref Mesh IUpCastable<Mesh>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref NamedObject IUpCastable<NamedObject>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<NamedObject, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<NamedObject, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __NamedObject, 1)));
		}
	}
}
