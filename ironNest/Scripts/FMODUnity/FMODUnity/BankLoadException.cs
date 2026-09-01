using System;
using FMOD;

namespace FMODUnity
{
	public class BankLoadException : Exception
	{
		public string Path;

		public RESULT Result;

		public BankLoadException(string path, RESULT result)
		{
		}

		public BankLoadException(string path, string error)
		{
		}
	}
}
