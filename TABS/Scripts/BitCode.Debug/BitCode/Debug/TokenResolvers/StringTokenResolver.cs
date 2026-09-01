namespace BitCode.Debug.TokenResolvers
{
	internal class StringTokenResolver : TokenResolver<string>
	{
		protected override string Resolve(string token)
		{
			return token;
		}
	}
}
