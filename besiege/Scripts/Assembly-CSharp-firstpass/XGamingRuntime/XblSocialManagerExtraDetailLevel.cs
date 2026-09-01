using System;

namespace XGamingRuntime
{
	[Flags]
	public enum XblSocialManagerExtraDetailLevel : uint
	{
		NoExtraDetail = 0u,
		TitleHistoryLevel = 1u,
		PreferredColorLevel = 2u,
		All = 3u
	}
}
