using System.Collections.Generic;

namespace BitCode.Debug.TokenResolvers
{
	internal class StringParamsTokenResolver : TokenResolverBase<string[]>, ITokenResolver, IStringParamsResolver
	{
		public override bool TryResolve(IReadOnlyList<string> tokens, ref int lastConsumedTokenIndex, out object resolvedToken)
		{
			int num = lastConsumedTokenIndex + 1;
			int num2 = tokens.Count - num;
			int num5 = default(int);
			string[] array = default(string[]);
			while (true)
			{
				int num3 = -904662890;
				while (true)
				{
					uint num4;
					switch ((num4 = (uint)(num3 ^ -764986607)) % 9)
					{
					case 8u:
						break;
					case 3u:
						num5++;
						num3 = (int)((num4 * 613379322) ^ 0x2BDB02D0);
						continue;
					case 5u:
						num3 = (int)((num4 * 1969214239) ^ 0x536F22B6);
						continue;
					case 0u:
						num5 = num;
						num3 = ((int)num4 * -1822307476) ^ -857912963;
						continue;
					case 1u:
						lastConsumedTokenIndex += num2;
						array = new string[num2];
						num3 = (int)((num4 * 133566810) ^ 0x6B6AD892);
						continue;
					case 6u:
						resolvedToken = array;
						num3 = ((int)num4 * -753644273) ^ 0x53B601B5;
						continue;
					case 4u:
					{
						int num6;
						if (num5 >= num + num2)
						{
							num3 = -93413174;
							num6 = num3;
						}
						else
						{
							num3 = -1036413932;
							num6 = num3;
						}
						continue;
					}
					case 2u:
						array[num5 - num] = tokens[num5];
						num3 = -1958810330;
						continue;
					default:
						return true;
					}
					break;
				}
			}
		}
	}
}
