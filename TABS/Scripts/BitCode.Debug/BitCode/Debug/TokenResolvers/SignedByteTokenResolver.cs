using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class SignedByteTokenResolver : TokenResolver<sbyte>
	{
		protected override sbyte Resolve(string token)
		{
			return sbyte.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
