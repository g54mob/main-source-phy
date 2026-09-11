using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct RemapPPtrTransfer : RemapPPtrTransfer.Interface, IUpCastable<RemapPPtrTransfer>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<RemapPPtrTransfer>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TransferBase __TransferBase;

		ref RemapPPtrTransfer IUpCastable<RemapPPtrTransfer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TransferBase, 1));
		}
	}
}
