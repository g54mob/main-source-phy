namespace FMODUnity
{
	public class PlatformMobileHigh : PlatformMobileLow
	{
		internal override string DisplayName => null;

		internal override float Priority => 0f;

		internal override bool MatchesCurrentEnvironment => false;

		static PlatformMobileHigh()
		{
		}
	}
}
