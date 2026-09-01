using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class ShortTokenResolver : TokenResolver<short>
	{
		protected override short Resolve(string token)
		{
			return short.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
