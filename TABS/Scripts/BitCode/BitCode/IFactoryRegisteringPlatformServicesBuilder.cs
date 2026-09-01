using System;
using JetBrains.Annotations;

namespace BitCode
{
	public interface IFactoryRegisteringPlatformServicesBuilder : IPlatformServicesBuilder
	{
		void RegisterFactory<TPlatformService>([NotNull] Func<TPlatformService> factory) where TPlatformService : class, IPlatformService;

		void RegisterServicesDecorator(Action<IPlatformServices> decorator);

		void RegisterServiceDecorator<TPlatformService>(Action<TPlatformService> decorator, string dependerName) where TPlatformService : class, IPlatformService;
	}
}
