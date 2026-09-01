using System;

namespace BitCode.ErrorHandling
{
	public static class IExceptionHandlerExtensions
	{
		public static NotSupportedException BuildNotSupportedException(this IExceptionHandler exceptionHandler, Exception exception)
		{
			return new NotSupportedException($"Exception of type {exception.GetType().Name} cannot be handled by this {exceptionHandler.GetType().Name}.");
		}
	}
}
