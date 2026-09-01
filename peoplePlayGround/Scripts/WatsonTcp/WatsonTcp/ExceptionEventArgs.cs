using System;

namespace WatsonTcp
{
	public class ExceptionEventArgs
	{
		public Exception Exception { get; }

		public string Json { get; }

		internal ExceptionEventArgs(Exception e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			Exception = e;
			Json = SerializationHelper.SerializeJson(e, pretty: true);
		}
	}
}
