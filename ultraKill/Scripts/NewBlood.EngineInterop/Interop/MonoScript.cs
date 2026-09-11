using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct MonoScript : MonoScript.Interface, IUpCastable<MonoScript>, TextAsset.Interface, IUpCastable<TextAsset>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<MonoScript>, TextAsset.Interface, IUpCastable<TextAsset>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private TextAsset __TextAsset;

		ref MonoScript IUpCastable<MonoScript>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TextAsset IUpCastable<TextAsset>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextAsset, 1));
		}

		ref NamedObject IUpCastable<NamedObject>.Cast()
		{
			return ref CastOperations.UpCast<TextAsset, NamedObject>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextAsset, 1)));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<TextAsset, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextAsset, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<TextAsset, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextAsset, 1)));
		}
	}
}
