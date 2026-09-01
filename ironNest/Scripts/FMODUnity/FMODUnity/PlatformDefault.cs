namespace FMODUnity
{
	public class PlatformDefault : Platform
	{
		public const string ConstIdentifier = "default";

		internal override string DisplayName => null;

		internal override bool IsIntrinsic => false;

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override void InitializeProperties()
		{
		}

		internal override void EnsurePropertiesAreValid()
		{
		}
	}
}
