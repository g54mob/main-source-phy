using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct YAMLWrite : YAMLWrite.Interface, IUpCastable<YAMLWrite>, TextDOMTransferWriteBase<YAMLWrite>.Interface, IUpCastable<TextDOMTransferWriteBase<YAMLWrite>>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<YAMLWrite>, TextDOMTransferWriteBase<YAMLWrite>.Interface, IUpCastable<TextDOMTransferWriteBase<YAMLWrite>>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TextDOMTransferWriteBase<YAMLWrite> __TextDOMTransferWriteBase;

		ref YAMLWrite IUpCastable<YAMLWrite>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TextDOMTransferWriteBase<YAMLWrite> IUpCastable<TextDOMTransferWriteBase<YAMLWrite>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferWriteBase, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref CastOperations.UpCast<TextDOMTransferWriteBase<YAMLWrite>, TransferBase>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferWriteBase, 1)));
		}
	}
}
