using System;

namespace BitCode.Profiles
{
	public interface IProfileSelectionState
	{
		ICapability<TCapabilityLevel> GetCapability<TCapabilityLevel>() where TCapabilityLevel : Enum;
	}
}
