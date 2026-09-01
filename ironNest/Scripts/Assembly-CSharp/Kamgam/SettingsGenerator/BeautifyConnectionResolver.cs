using Beautify.Universal;

namespace Kamgam.SettingsGenerator
{
	public class BeautifyConnectionResolver
	{
		private Beautify.Universal.Beautify _cached;

		private readonly bool _resolveEveryAccess;

		private readonly bool _logWarnings;

		public BeautifyConnectionResolver(bool resolveEveryAccess, bool logWarnings)
		{
		}

		public void Invalidate()
		{
		}

		public Beautify.Universal.Beautify Resolve()
		{
			return null;
		}
	}
}
