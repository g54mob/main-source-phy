using UnityEngine;

namespace Lofelt.NiceVibrations;

public static class DeviceCapabilities
{
	private static readonly RuntimePlatform _003Cplatform_003Ek__BackingField;

	private static readonly int _003CplatformVersion_003Ek__BackingField;

	private static bool _meetsAdvancedRequirements;

	private static readonly bool _003CisVersionSupported_003Ek__BackingField;

	private static bool _hasAmplitudeControl;

	private static bool _hasFrequencyControl;

	private static bool _hasAmplitudeModulation;

	private static bool _hasFrequencyModulation;

	private static bool _hasEmphasis;

	private static bool _canEmulateEmphasis;

	private static bool _canLoop;

	public static RuntimePlatform platform => _003Cplatform_003Ek__BackingField;

	public static int platformVersion => _003CplatformVersion_003Ek__BackingField;

	public static bool meetsAdvancedRequirements => _meetsAdvancedRequirements;

	public static bool isVersionSupported => _003CisVersionSupported_003Ek__BackingField;

	public static bool hasAmplitudeControl => _hasAmplitudeControl;

	public static bool hasFrequencyControl => _hasFrequencyControl;

	public static bool hasAmplitudeModulation => _hasAmplitudeModulation;

	public static bool hasFrequencyModulation => _hasFrequencyModulation;

	public static bool hasEmphasis => _hasEmphasis;

	public static bool canEmulateEmphasis => _canEmulateEmphasis;

	public static bool canLoop => _canLoop;

	static DeviceCapabilities()
	{
		RuntimePlatform runtimePlatform = Application.platform;
		_003Cplatform_003Ek__BackingField = runtimePlatform;
		_003CplatformVersion_003Ek__BackingField = 0;
		_003CisVersionSupported_003Ek__BackingField = false;
	}

	public static void Init()
	{
		_meetsAdvancedRequirements = true;
	}
}
