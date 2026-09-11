using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct ListNode<T> : ListNode<T>.Interface, IUpCastable<ListNode<T>>, ListElement.Interface, IUpCastable<ListElement> where T : unmanaged
	{
		public interface Interface : IUpCastable<ListNode<T>>, ListElement.Interface, IUpCastable<ListElement>
		{
		}

		[BaseField]
		private ListElement __ListElement;

		public unsafe T* m_Data;

		ref ListNode<T> IUpCastable<ListNode<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref ListElement IUpCastable<ListElement>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __ListElement, 1));
		}
	}
}
