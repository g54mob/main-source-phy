using System;

namespace Koenigz.PerfectCulling
{
	[Serializable]
	public sealed class PerfectCullingToggleException : Exception
	{
		public PerfectCullingToggleException()
		{
		}

		public PerfectCullingToggleException(string message)
		{
		}

		public PerfectCullingToggleException(string message, Exception inner)
		{
		}
	}
}
