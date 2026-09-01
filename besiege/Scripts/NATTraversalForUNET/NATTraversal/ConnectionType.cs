using System;

namespace NATTraversal
{
	[Flags]
	public enum ConnectionType
	{
		DIRECT = 1,
		PUNCHTHROUGH = 2,
		RELAY = 4,
		ANY = 7
	}
}
