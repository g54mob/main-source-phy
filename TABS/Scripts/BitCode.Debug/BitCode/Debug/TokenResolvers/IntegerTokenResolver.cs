using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class IntegerTokenResolver : TokenResolver<int>
	{
		protected override int Resolve(string token)
		{
			return int.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
