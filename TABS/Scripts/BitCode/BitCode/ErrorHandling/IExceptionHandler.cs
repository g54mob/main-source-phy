using System;
using System.Threading.Tasks;

namespace BitCode.ErrorHandling
{
	public interface IExceptionHandler
	{
		bool CanHandleException(Exception exception);

		Task<ExceptionResolution> TryHandleExceptionAsync(Exception exception);
	}
}
