using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct IManagedObjectHost : IManagedObjectHost.Interface, IUpCastable<IManagedObjectHost>
	{
		public interface Interface : IUpCastable<IManagedObjectHost>
		{
		}

		public unsafe void** __vftable;

		ref IManagedObjectHost IUpCastable<IManagedObjectHost>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
