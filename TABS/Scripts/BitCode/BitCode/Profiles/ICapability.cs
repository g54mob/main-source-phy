using System;

namespace BitCode.Profiles
{
	public interface ICapability<TCapabilityLevel> where TCapabilityLevel : Enum
	{
		TCapabilityLevel Level { get; set; }
	}
}
