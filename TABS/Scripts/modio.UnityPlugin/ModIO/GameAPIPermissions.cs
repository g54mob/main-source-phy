using System;

namespace ModIO
{
	[Flags]
	public enum GameAPIPermissions
	{
		RestrictAll = 0,
		AllowPublicAccess = 1,
		AllowDirectDownloads = 2
	}
}
