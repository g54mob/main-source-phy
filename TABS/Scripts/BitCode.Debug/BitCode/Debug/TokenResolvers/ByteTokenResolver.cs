using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class ByteTokenResolver : TokenResolver<byte>
	{
		protected override byte Resolve(string token)
		{
			return byte.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
