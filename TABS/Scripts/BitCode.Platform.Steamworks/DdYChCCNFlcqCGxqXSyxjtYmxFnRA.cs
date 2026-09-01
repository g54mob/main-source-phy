internal sealed class DdYChCCNFlcqCGxqXSyxjtYmxFnRA
{
	internal static uint VnddVlepyRJSegDTusdnFINPwipbA(string P_0)
	{
		uint num4 = default(uint);
		if (P_0 != null)
		{
			int num3 = default(int);
			while (true)
			{
				int num = 209820269;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x77A50A29)) % 8)
					{
					case 0u:
						break;
					case 3u:
						num4 = (P_0[num3] ^ num4) * 16777619;
						num = 253288339;
						continue;
					case 1u:
						num3 = 0;
						num = ((int)num2 * -843002468) ^ -127984;
						continue;
					case 5u:
						num = (int)((num2 * 550410974) ^ 0x65C7E019);
						continue;
					case 4u:
						num4 = 2166136261u;
						num = (int)(num2 * 159547824) ^ -431318336;
						continue;
					case 2u:
						num3++;
						num = ((int)num2 * -1820738679) ^ 0x6ECD5845;
						continue;
					case 6u:
						goto IL_009e;
					default:
						goto end_IL_0006;
					}
					break;
					IL_009e:
					int num5;
					if (num3 >= P_0.Length)
					{
						num = 1026791246;
						num5 = num;
					}
					else
					{
						num = 1488069418;
						num5 = num;
					}
				}
				continue;
				end_IL_0006:
				break;
			}
		}
		return num4;
	}
}
