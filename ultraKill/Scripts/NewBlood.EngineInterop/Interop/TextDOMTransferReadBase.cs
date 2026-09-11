using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct TextDOMTransferReadBase<T> : TextDOMTransferReadBase<T>.Interface, IUpCastable<TextDOMTransferReadBase<T>>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<TextDOMTransferReadBase<T>>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TransferBase __TransferBase;

		ref TextDOMTransferReadBase<T> IUpCastable<TextDOMTransferReadBase<T>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TransferBase, 1));
		}
	}
}
