using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class FloatTokenResolver : TokenResolver<float>
	{
		protected override float Resolve(string token)
		{
			return float.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
