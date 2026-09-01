using System.Globalization;

namespace BitCode.Debug.TokenResolvers
{
	internal class DoubleTokenResolver : TokenResolver<double>
	{
		protected override double Resolve(string token)
		{
			return double.Parse(token, CultureInfo.InvariantCulture);
		}
	}
}
