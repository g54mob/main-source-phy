using System;

namespace XGamingRuntime
{
	[Flags]
	public enum XUserGetTokenAndSignatureOptions : uint
	{
		None = 0u,
		ForceRefresh = 1u,
		AllUsers = 2u
	}
}
