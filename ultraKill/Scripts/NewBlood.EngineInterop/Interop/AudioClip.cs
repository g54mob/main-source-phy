using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;

namespace Interop
{
	[NonCopyable]
	[IncompleteType]
	[SupportsInheritance]
	public struct AudioClip : AudioClip.Interface, IUpCastable<AudioClip>, SampleClip.Interface, IUpCastable<SampleClip>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
	{
		public interface Interface : IUpCastable<AudioClip>, SampleClip.Interface, IUpCastable<SampleClip>, NamedObject.Interface, IUpCastable<NamedObject>, EditorExtension.Interface, IUpCastable<EditorExtension>, Object.Interface, IUpCastable<Object>
		{
		}

		[BaseField]
		private SampleClip __SampleClip;

		ref AudioClip IUpCastable<AudioClip>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref SampleClip IUpCastable<SampleClip>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __SampleClip, 1));
		}

		ref NamedObject IUpCastable<NamedObject>.Cast()
		{
			return ref CastOperations.UpCast<SampleClip, NamedObject>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __SampleClip, 1)));
		}

		ref EditorExtension IUpCastable<EditorExtension>.Cast()
		{
			return ref CastOperations.UpCast<SampleClip, EditorExtension>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __SampleClip, 1)));
		}

		ref Object IUpCastable<Object>.Cast()
		{
			return ref CastOperations.UpCast<SampleClip, Object>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __SampleClip, 1)));
		}
	}
}
