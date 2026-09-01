using System;
using System.Globalization;

namespace BitCode.L10n
{
	public class DotNetSystemLanguageProvider : IPlatformService, ISystemLanguageProvider
	{
		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public string GetLanguageCode()
		{
			return CultureInfo.CurrentCulture.Name;
		}
	}
}
