using System;

namespace ModIO
{
	public enum ModTeamMemberAccessLevel
	{
		Moderator = 1,
		Manager = 4,
		Administrator = 8,
		[Obsolete("Replaced by ModTeamMemberAccessLevel.Manager")]
		Statistics = 4
	}
}
