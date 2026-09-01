using System;
using FMOD;

namespace FMODUnity
{
	public class SystemNotInitializedException : Exception
	{
		public RESULT Result;

		public string Location;

		public SystemNotInitializedException(RESULT result, string location)
		{
		}

		public SystemNotInitializedException(Exception inner)
		{
		}
	}
}
