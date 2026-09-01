namespace FMODUnity
{
	public class PlatformMobileLow : Platform
	{
		internal override string DisplayName => null;

		internal override float Priority => 0f;

		internal override bool MatchesCurrentEnvironment => false;

		static PlatformMobileLow()
		{
		}

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}
	}
}
