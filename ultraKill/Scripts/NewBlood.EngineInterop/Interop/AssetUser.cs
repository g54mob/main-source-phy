using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct AssetUser : AssetUser.Interface, IUpCastable<AssetUser>
	{
		public struct Vtbl<T> where T : unmanaged, Interface
		{
			public unsafe delegate* unmanaged[Thiscall]<T*, void> OnAssetDeleted;

			public unsafe delegate* unmanaged[Thiscall]<T*, void> OnAssetBoundsChanged;
		}

		public interface Interface : IUpCastable<AssetUser>
		{
		}

		public unsafe void** __vftable;

		ref AssetUser IUpCastable<AssetUser>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
