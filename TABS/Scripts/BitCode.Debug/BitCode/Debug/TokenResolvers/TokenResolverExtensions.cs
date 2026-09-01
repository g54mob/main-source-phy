using System;

namespace BitCode.Debug.TokenResolvers
{
	public static class TokenResolverExtensions
	{
		public static object ResolveSingleToken(this ITokenResolver tokenResolver, Type destinationType, string token)
		{
			int lastConsumedTokenIndex = -1;
			string[] tokens = default(string[]);
			object resolvedToken = default(object);
			while (true)
			{
				int num = 447324338;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x3EAA2546)) % 5)
					{
					case 0u:
						break;
					case 4u:
						throw new TokenResolutionException(token, destinationType, $"Couldn't resolve token {token} with resolver {tokenResolver}.");
					case 2u:
					{
						int num3;
						int num4;
						if (tokenResolver.TryResolve(tokens, ref lastConsumedTokenIndex, out resolvedToken))
						{
							num3 = -660292855;
							num4 = num3;
						}
						else
						{
							num3 = -882635381;
							num4 = num3;
						}
						num = num3 ^ (int)(num2 * 1998257620);
						continue;
					}
					case 1u:
						tokens = new string[1] { token };
						num = (int)(num2 * 399891713) ^ -1862533049;
						continue;
					default:
						return resolvedToken;
					}
					break;
				}
			}
		}
	}
}
