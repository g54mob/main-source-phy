using System;

namespace ModIO
{
	public enum ModStatus
	{
		NotAccepted = 0,
		Accepted = 1,
		Deleted = 3,
		[Obsolete("No longer used. All mods previously Archived are now Accepted.")]
		Archived = 2
	}
}
