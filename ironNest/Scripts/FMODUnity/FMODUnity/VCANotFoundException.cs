using System;

namespace FMODUnity
{
	public class VCANotFoundException : Exception
	{
		public string Path;

		public VCANotFoundException(string path)
		{
		}
	}
}
