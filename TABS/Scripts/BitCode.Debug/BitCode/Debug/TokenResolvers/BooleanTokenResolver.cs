using System;

namespace BitCode.Debug.TokenResolvers
{
	internal class BooleanTokenResolver : TokenResolver<bool>
	{
		protected override bool Resolve(string token)
		{
			if (!bool.TryParse(token, out var result))
			{
				long result2 = default(long);
				while (true)
				{
					int num = 262115795;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ 0x7BA3AB07)) % 5)
						{
						case 4u:
							break;
						case 3u:
							result = result2 != 0;
							num = 1378945937;
							continue;
						case 0u:
							throw new FormatException("Couldn't interpret input " + token + " as a boolean value.");
						case 1u:
						{
							int num3;
							int num4;
							if (!long.TryParse(token, out result2))
							{
								num3 = 1523857308;
								num4 = num3;
							}
							else
							{
								num3 = 238693528;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -281270820);
							continue;
						}
						default:
							goto end_IL_000a;
						}
						break;
					}
					continue;
					end_IL_000a:
					break;
				}
			}
			return result;
		}
	}
}
