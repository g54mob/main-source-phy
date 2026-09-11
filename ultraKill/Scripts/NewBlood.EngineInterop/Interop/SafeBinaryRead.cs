using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct SafeBinaryRead : SafeBinaryRead.Interface, IUpCastable<SafeBinaryRead>, TransferBase.Interface, IUpCastable<TransferBase>
	{
		public interface Interface : IUpCastable<SafeBinaryRead>, TransferBase.Interface, IUpCastable<TransferBase>
		{
		}

		[BaseField]
		private TransferBase __TransferBase;

		ref SafeBinaryRead IUpCastable<SafeBinaryRead>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref TransferBase IUpCastable<TransferBase>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __TransferBase, 1));
		}
	}
}
