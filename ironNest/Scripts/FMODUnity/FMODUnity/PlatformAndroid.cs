namespace FMODUnity
{
	public class PlatformAndroid : Platform
	{
		internal override string DisplayName => null;

		static PlatformAndroid()
		{
		}

		internal override void DeclareRuntimePlatforms(Settings settings)
		{
		}

		internal override string GetBankFolder()
		{
			return null;
		}

		internal static string StaticGetBankFolder()
		{
			return null;
		}

		internal override string GetPluginPath(string pluginName)
		{
			return null;
		}

		internal static string StaticGetPluginPath(string pluginName)
		{
			return null;
		}
	}
}
