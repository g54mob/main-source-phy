using System;

namespace BitCode
{
	public interface IPlatformService
	{
		event Action<IPlatformService, Exception> InternalErrorOccurred;
	}
}
