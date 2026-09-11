using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct YAMLRead : YAMLRead.Interface, IUpCastable<YAMLRead>, TextDOMTransferReadBase<YAMLRead>.Interface, IUpCastable<TextDOMTransferReadBase<YAMLRead>>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<YAMLRead>, TextDOMTransferReadBase<YAMLRead>.Interface, IUpCastable<TextDOMTransferReadBase<YAMLRead>>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TextDOMTransferReadBase<YAMLRead> __TextDOMTransferReadBase;

		ref YAMLRead IUpCastable<YAMLRead>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TextDOMTransferReadBase<YAMLRead> IUpCastable<TextDOMTransferReadBase<YAMLRead>>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferReadBase, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref CastOperations.UpCast<TextDOMTransferReadBase<YAMLRead>, TransferBase>(ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TextDOMTransferReadBase, 1)));
		}
	}
}
