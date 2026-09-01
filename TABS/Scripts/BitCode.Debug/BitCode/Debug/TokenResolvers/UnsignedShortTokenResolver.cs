using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class UnsignedShortTokenResolver : TokenResolver<ushort>
	{
		protected override ushort Resolve(string token)
		{
			return ushort.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
