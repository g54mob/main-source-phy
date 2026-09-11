using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct Texture2D : Texture2D.Interface, IUpCastable<Texture2D>, Texture.Interface, IUpCastable<Texture>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<Texture2D>, Texture.Interface, IUpCastable<Texture>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private Texture __Texture;

		ref Texture2D IUpCastable<Texture2D>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref Texture IUpCastable<Texture>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Texture, 1));
		}

		ref NamedObject IUpCastable<NamedObject>.Cast()
		{
			return ref CastOperations.UpCast<Texture, NamedObject>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Texture, 1)));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<Texture, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Texture, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<Texture, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __Texture, 1)));
		}
	}
}
