using System;

namespace NAudio.Utils
{
	public class InvalidDataException : Exception
	{
		public InvalidDataException()
			: base("")
		{
		}

		public InvalidDataException(string message)
			: base(message)
		{
		}

		public InvalidDataException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
