using System.Runtime.InteropServices;

namespace Interop
{
	[IncompleteType]
	[SupportsInheritance]
	public struct ParticleSystemReadOnlyState : ParticleSystemReadOnlyState.Interface, IUpCastable<ParticleSystemReadOnlyState>, IParticleSystemProperties.Interface, IUpCastable<IParticleSystemProperties>
	{
		public interface Interface : IUpCastable<ParticleSystemReadOnlyState>, IParticleSystemProperties.Interface, IUpCastable<IParticleSystemProperties>
		{
		}

		[BaseField]
		private IParticleSystemProperties __IParticleSystemProperties;

		ref ParticleSystemReadOnlyState IUpCastable<ParticleSystemReadOnlyState>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref this, 1));
		}

		ref IParticleSystemProperties IUpCastable<IParticleSystemProperties>.Cast()
		{
			return ref MemoryMarshal.GetReference(MemoryMarshal.CreateSpan(ref __IParticleSystemProperties, 1));
		}
	}
}
