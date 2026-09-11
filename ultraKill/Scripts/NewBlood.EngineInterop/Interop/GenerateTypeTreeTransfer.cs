using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct GenerateTypeTreeTransfer : GenerateTypeTreeTransfer.Interface, IUpCastable<GenerateTypeTreeTransfer>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<GenerateTypeTreeTransfer>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TransferBase __TransferBase;

		ref GenerateTypeTreeTransfer IUpCastable<GenerateTypeTreeTransfer>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TransferBase, 1));
		}
	}
}
