using System;

namespace BitCode.Dlc
{
	public class DlcException : Exception
	{
		public DlcException()
		{
		}

		public DlcException(string message)
			: base(message)
		{
		}
	}
}
