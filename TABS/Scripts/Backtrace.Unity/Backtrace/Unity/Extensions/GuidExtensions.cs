using System;

namespace Backtrace.Unity.Extensions
{
	public static class GuidExtensions
	{
		public static Guid FromLong(long source)
		{
			byte[] array = new byte[16];
			Array.Copy(BitConverter.GetBytes(source), array, 8);
			return new Guid(array);
		}
	}
}
