using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct ListElement : ListElement.Interface, IUpCastable<ListElement>
	{
		public interface Interface : IUpCastable<ListElement>
		{
		}

		public unsafe ListElement* m_Prev;

		public unsafe ListElement* m_Next;

		ref ListElement IUpCastable<ListElement>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
