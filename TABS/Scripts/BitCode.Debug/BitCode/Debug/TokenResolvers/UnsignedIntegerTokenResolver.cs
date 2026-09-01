using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class UnsignedIntegerTokenResolver : TokenResolver<uint>
	{
		protected override uint Resolve(string token)
		{
			return uint.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
