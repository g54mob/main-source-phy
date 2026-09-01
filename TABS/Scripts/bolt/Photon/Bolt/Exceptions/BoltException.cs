using System;

namespace Photon.Bolt.Exceptions
{
	public class BoltException : Exception
	{
		public object ExtraInfo { get; set; }

		public override string Message
		{
			get
			{
				if (ExtraInfo != null)
				{
					return string.Format(base.Message, ExtraInfo.ToString());
				}
				return base.Message;
			}
		}

		public BoltException(string message, params object[] args)
			: base(string.Format(message, args))
		{
		}
	}
}
