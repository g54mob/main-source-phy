using System;

namespace Ceras
{
	[Flags]
	public enum TargetMember
	{
		None = 0,
		PublicFields = 1,
		PrivateFields = 2,
		PublicProperties = 4,
		PrivateProperties = 8,
		AllPublic = 5,
		AllPrivate = 0xA,
		AllFields = 3,
		AllProperties = 0xC,
		All = 0xF
	}
}
