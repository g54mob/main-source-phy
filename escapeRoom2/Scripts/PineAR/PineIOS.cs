public class PineIOS
{
	public static NSProcessInfoThermalState thermalState()
	{
		return NSProcessInfoThermalState.NSProcessInfoThermalStateNominal;
	}

	public static bool isLowPowerModeEnabled()
	{
		return false;
	}
}
