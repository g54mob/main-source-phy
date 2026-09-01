using System;
using UnityEngine;

namespace BitCode.UI
{
	public static class RadialMenuHelpers
	{
		public static int Wrap(int value, int min, int max)
		{
			if (max < min)
			{
				goto IL_0007;
			}
			goto IL_00dc;
			IL_0007:
			int num = 413547089;
			goto IL_000c;
			IL_000c:
			int num3 = default(int);
			int num6 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1ECE287A)) % 11)
				{
				case 0u:
					break;
				case 4u:
					goto IL_004d;
				case 9u:
				{
					int num7;
					int num8;
					if (num3 < 0)
					{
						num7 = -1634897734;
						num8 = num7;
					}
					else
					{
						num7 = -887803223;
						num8 = num7;
					}
					num = num7 ^ ((int)num2 * -775758805);
					continue;
				}
				case 5u:
					num3 += num6;
					num = ((int)num2 * -1814611579) ^ -1296200435;
					continue;
				case 7u:
					throw new ArgumentException("Max must be greater than min");
				case 8u:
					num6 = max - min + 1;
					num3 = (value - min) % num6;
					num = 1032522161;
					continue;
				case 1u:
					return value;
				case 10u:
					goto IL_00dc;
				case 6u:
					return min;
				case 2u:
				{
					int num4;
					int num5;
					if (value <= max)
					{
						num4 = -1596091530;
						num5 = num4;
					}
					else
					{
						num4 = -1661272725;
						num5 = num4;
					}
					num = num4 ^ ((int)num2 * -388998415);
					continue;
				}
				default:
					return num3 + min;
				}
				break;
				IL_004d:
				int num9;
				if (value >= min)
				{
					num = 288007600;
					num9 = num;
				}
				else
				{
					num = 148406849;
					num9 = num;
				}
			}
			goto IL_0007;
			IL_00dc:
			int num10;
			if (max == min)
			{
				num = 1235045762;
				num10 = num;
			}
			else
			{
				num = 1387377043;
				num10 = num;
			}
			goto IL_000c;
		}

		public static int WrappedDistance(int a, int b, int min, int max)
		{
			if (max < min)
			{
				goto IL_0007;
			}
			goto IL_0090;
			IL_0007:
			int num = 1774455014;
			goto IL_000c;
			IL_000c:
			int num3 = default(int);
			int num4 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x67FEE0E5)) % 7)
				{
				case 2u:
					break;
				case 1u:
					num3 += num4;
					num = ((int)num2 * -845976377) ^ 0x71772E7A;
					continue;
				case 5u:
					goto IL_004f;
				case 6u:
					return 0;
				case 3u:
					goto IL_0090;
				case 4u:
					throw new ArgumentException("Max must be greater than min");
				default:
					return Math.Min(num3, num4 - num3);
				}
				break;
				IL_004f:
				num4 = max - min + 1;
				num3 = Wrap(a, min, max) - Wrap(b, min, max);
				int num5;
				if (num3 >= 0)
				{
					num = 1266759475;
					num5 = num;
				}
				else
				{
					num = 1363114378;
					num5 = num;
				}
			}
			goto IL_0007;
			IL_0090:
			int num6;
			if (max != min)
			{
				num = 1189558868;
				num6 = num;
			}
			else
			{
				num = 1690322706;
				num6 = num;
			}
			goto IL_000c;
		}

		public static int SignedWrappedDistance(int a, int b, int min, int max)
		{
			int num = Mathf.Abs(a - b);
			int num5 = default(int);
			int num4 = default(int);
			while (true)
			{
				int num2 = 718593337;
				while (true)
				{
					uint num3;
					int num6;
					int num7;
					switch ((num3 = (uint)(num2 ^ 0x5695482D)) % 8)
					{
					case 6u:
						break;
					case 3u:
						num6 = -1;
						goto IL_0046;
					case 0u:
						if (max + 1 - b + (a - min) >= num)
						{
							num2 = 1526935888;
							continue;
						}
						num7 = -1;
						goto IL_009e;
					case 7u:
					{
						int num8;
						int num9;
						if (a > b)
						{
							num8 = -405540117;
							num9 = num8;
						}
						else
						{
							num8 = -315825687;
							num9 = num8;
						}
						num2 = num8 ^ ((int)num3 * -837354932);
						continue;
					}
					case 4u:
						num5 = WrappedDistance(a, b, min, max);
						num2 = (int)((num3 * 604460755) ^ 0x3ABD3C8E);
						continue;
					case 5u:
						num7 = 1;
						goto IL_009e;
					case 2u:
						if (max + 1 - a + (b - min) < num)
						{
							num6 = 1;
							goto IL_0046;
						}
						num2 = (int)(num3 * 413177961) ^ -1210343724;
						continue;
					default:
						{
							return num4 * num5;
						}
						IL_0046:
						num4 = num6;
						num2 = 1819549436;
						continue;
						IL_009e:
						num4 = num7;
						num2 = 1819549436;
						continue;
					}
					break;
				}
			}
		}

		public static Vector2 VectorFromAngle(float angle)
		{
			float f = angle * ((float)Math.PI / 180f);
			return new Vector2(Mathf.Sin(f), Mathf.Cos(f));
		}
	}
}
