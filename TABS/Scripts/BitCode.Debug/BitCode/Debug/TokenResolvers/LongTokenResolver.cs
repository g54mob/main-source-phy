using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class LongTokenResolver : TokenResolver<long>
	{
		protected override long Resolve(string token)
		{
			return long.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
