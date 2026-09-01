using System;

namespace Ceras
{
	[Flags]
	public enum DelegateSerializationFlags
	{
		Off = 0,
		AllowStatic = 1,
		AllowInstance = 2
	}
}
