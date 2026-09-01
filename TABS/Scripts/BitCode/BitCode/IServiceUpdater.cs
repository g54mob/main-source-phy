using System;

namespace BitCode
{
	public interface IServiceUpdater
	{
		event Action<Exception, IUpdateableService> InternalErrorOccurred;

		void RegisterService(IUpdateableService service);

		void DeregisterService(IUpdateableService service);
	}
}
