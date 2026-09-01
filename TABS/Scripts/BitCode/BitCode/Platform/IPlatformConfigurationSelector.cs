using System.Collections.Generic;

namespace BitCode.Platform
{
	public interface IPlatformConfigurationSelector
	{
		IEnumerable<IPlatformConfiguration> GetActiveConfiguration();
	}
}
