using System;
using JetBrains.Annotations;

namespace BitCode
{
	public class ServiceNotInitializedException : Exception
	{
		[StringFormatMethod("formatArgs")]
		public ServiceNotInitializedException(string message, params object[] formatArgs)
			: base(string.Format(message, formatArgs))
		{
		}
	}
}
