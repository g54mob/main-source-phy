using System;

namespace BitCode.Purchasing
{
	public class PurchasingException : Exception
	{
		public PurchasingException()
		{
		}

		public PurchasingException(string message)
			: base(message)
		{
		}
	}
}
