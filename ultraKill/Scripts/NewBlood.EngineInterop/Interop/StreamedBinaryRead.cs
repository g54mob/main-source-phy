using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct StreamedBinaryRead : StreamedBinaryRead.Interface, IUpCastable<StreamedBinaryRead>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<StreamedBinaryRead>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TransferBase __TransferBase;

		ref StreamedBinaryRead IUpCastable<StreamedBinaryRead>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TransferBase, 1));
		}
	}
}
