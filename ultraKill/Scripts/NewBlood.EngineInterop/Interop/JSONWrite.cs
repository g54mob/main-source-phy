using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct JSONWrite : JSONWrite.Interface, IUpCastable<JSONWrite>, TextDOMTransferWriteBase<JSONWrite>.Interface, IUpCastable<TextDOMTransferWriteBase<JSONWrite>>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<JSONWrite>, TextDOMTransferWriteBase<JSONWrite>.Interface, IUpCastable<TextDOMTransferWriteBase<JSONWrite>>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TextDOMTransferWriteBase<JSONWrite> __TextDOMTransferWriteBase;

		ref JSONWrite IUpCastable<JSONWrite>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TextDOMTransferWriteBase<JSONWrite> IUpCastable<TextDOMTransferWriteBase<JSONWrite>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferWriteBase, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref CastOperations.UpCast<TextDOMTransferWriteBase<JSONWrite>, TransferBase>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferWriteBase, 1)));
		}
	}
}
