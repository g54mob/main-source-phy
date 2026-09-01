namespace Kamgam.SettingsGenerator;

public class VolumetricsEnabledConnection : Connection<bool>
{
	public override bool Get()
	{
		Logger.LogWarning("Volumetrics no supported in URP.");
		return true;
	}

	public override void Set(bool enable)
	{
		Logger.LogWarning("Volumetrics no supported in URP.");
	}
}
