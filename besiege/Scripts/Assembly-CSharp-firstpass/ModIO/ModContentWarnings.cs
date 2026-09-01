using System;

namespace ModIO
{
	[Flags]
	public enum ModContentWarnings
	{
		None = 0,
		Alcohol = 1,
		Drugs = 2,
		Violence = 4,
		Explicit = 8
	}
}
