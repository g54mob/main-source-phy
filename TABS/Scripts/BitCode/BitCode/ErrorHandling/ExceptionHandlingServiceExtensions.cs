using System;
using BitCode.Threading;

namespace BitCode.ErrorHandling
{
	public static class ExceptionHandlingServiceExtensions
	{
		[Obsolete("Prefer using task-based asynchronous patterns.")]
		public static void TryHandleExceptionAsync<TException>(this ExceptionHandlingService service, TException exception, Action<ExceptionResolution, Exception> onCompleted) where TException : Exception
		{
			service.TryHandleExceptionAsync(exception).ContinueWithAsync(onCompleted);
		}
	}
}
