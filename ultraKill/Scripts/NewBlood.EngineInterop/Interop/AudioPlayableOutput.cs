using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct AudioPlayableOutput : AudioPlayableOutput.Interface, IUpCastable<AudioPlayableOutput>, PlayableOutput.Interface, IUpCastable<PlayableOutput>, ListElement.Interface, IUpCastable<ListElement>
	{
		public interface Interface : IUpCastable<AudioPlayableOutput>, PlayableOutput.Interface, IUpCastable<PlayableOutput>, ListElement.Interface, IUpCastable<ListElement>
		{
		}

		[BaseField]
		private PlayableOutput __PlayableOutput;

		ref AudioPlayableOutput IUpCastable<AudioPlayableOutput>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref PlayableOutput IUpCastable<PlayableOutput>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __PlayableOutput, 1));
		}

		ref ListElement IUpCastable<ListElement>.Cast()
		{
			return ref CastOperations.UpCast<PlayableOutput, ListElement>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __PlayableOutput, 1)));
		}
	}
}
