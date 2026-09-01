using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class UnsignedLongTokenResolver : TokenResolver<ulong>
	{
		protected override ulong Resolve(string token)
		{
			return ulong.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
