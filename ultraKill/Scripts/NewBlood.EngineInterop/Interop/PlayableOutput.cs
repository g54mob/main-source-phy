using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct PlayableOutput : PlayableOutput.Interface, IUpCastable<PlayableOutput>, ListElement.Interface, IUpCastable<ListElement>
	{
		public interface Interface : IUpCastable<PlayableOutput>, ListElement.Interface, IUpCastable<ListElement>
		{
		}

		[BaseField]
		private ListElement __ListElement;

		ref PlayableOutput IUpCastable<PlayableOutput>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref ListElement IUpCastable<ListElement>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ListElement, 1));
		}
	}
}
