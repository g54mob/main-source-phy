using System;

namespace BitCode.Users
{
	[Flags]
	public enum SignInPromptOptions
	{
		Default = 0,
		AllowGuests = 1,
		Silent = 2
	}
}
