using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct StreamedBinaryWrite : StreamedBinaryWrite.Interface, IUpCastable<StreamedBinaryWrite>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<StreamedBinaryWrite>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TransferBase __TransferBase;

		ref StreamedBinaryWrite IUpCastable<StreamedBinaryWrite>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TransferBase, 1));
		}
	}
}
