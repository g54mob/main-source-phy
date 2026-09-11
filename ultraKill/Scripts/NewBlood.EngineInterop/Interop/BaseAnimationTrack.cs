using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[SupportsInheritance]
	public struct BaseAnimationTrack : BaseAnimationTrack.Interface, IUpCastable<BaseAnimationTrack>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<BaseAnimationTrack>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private NamedObject __NamedObject;

		ref BaseAnimationTrack IUpCastable<BaseAnimationTrack>.Cast()
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
