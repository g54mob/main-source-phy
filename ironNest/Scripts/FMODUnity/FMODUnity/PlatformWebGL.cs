namespace FMODUnity
{
	public class PlatformWebGL : Platform
	{
		internal override string DisplayName => null;

		static PlatformWebGL()
		{
		}

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override string GetPluginPath(string pluginName)
		{
			return null;
		}
	}
}
