using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct JSONRead : JSONRead.Interface, IUpCastable<JSONRead>, TextDOMTransferReadBase<JSONRead>.Interface, IUpCastable<TextDOMTransferReadBase<JSONRead>>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<JSONRead>, TextDOMTransferReadBase<JSONRead>.Interface, IUpCastable<TextDOMTransferReadBase<JSONRead>>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TextDOMTransferReadBase<JSONRead> __TextDOMTransferReadBase;

		ref JSONRead IUpCastable<JSONRead>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TextDOMTransferReadBase<JSONRead> IUpCastable<TextDOMTransferReadBase<JSONRead>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferReadBase, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref CastOperations.UpCast<TextDOMTransferReadBase<JSONRead>, TransferBase>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferReadBase, 1)));
		}
	}
}
