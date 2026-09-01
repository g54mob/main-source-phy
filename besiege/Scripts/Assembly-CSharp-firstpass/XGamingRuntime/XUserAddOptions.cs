using System;

namespace XGamingRuntime
{
	[Flags]
	public enum XUserAddOptions : uint
	{
		None = 0u,
		AddDefaultUserSilently = 1u,
		AllowGuests = 2u,
		AddDefaultUserAllowingUI = 4u
	}
}
