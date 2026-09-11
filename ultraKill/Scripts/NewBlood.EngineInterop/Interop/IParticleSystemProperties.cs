using System.Runtime.InteropServices;

namespace Interop
{
	[SupportsInheritance]
	public struct IParticleSystemProperties : IParticleSystemProperties.Interface, IUpCastable<IParticleSystemProperties>
	{
		public interface Interface : IUpCastable<IParticleSystemProperties>
		{
		}

		public unsafe void** __vftable;

		ref IParticleSystemProperties IUpCastable<IParticleSystemProperties>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}
	}
}
