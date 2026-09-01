using System;

namespace BitCode.Maths
{
	public static class RandomNumberGeneratorExtensions
	{
		public static int Next(this IRandomNumberGenerator random, int minValue, int maxValue)
		{
			if (random == null)
			{
				goto IL_0003;
			}
			goto IL_006c;
			IL_0003:
			int num = -1516669729;
			goto IL_0008;
			IL_0008:
			uint num2;
			switch ((num2 = (uint)(num ^ -1183289574)) % 5)
			{
			case 0u:
				break;
			case 2u:
				throw new ArgumentNullException("random");
			case 1u:
				throw new ArgumentOutOfRangeException("minValue", minValue, "minValue cannot be greater than maxValue.");
			case 4u:
				goto IL_006c;
			default:
				return eXwrrMzIJXjkeyTxuRUoJUWTolL(random, minValue, maxValue);
			}
			goto IL_0003;
			IL_006c:
			int num3;
			if (minValue <= maxValue)
			{
				num = -1528208660;
				num3 = num;
			}
			else
			{
				num = -628960782;
				num3 = num;
			}
			goto IL_0008;
		}

		public static int Next(this IRandomNumberGenerator random, int maxValue)
		{
			if (random == null)
			{
				goto IL_0003;
			}
			goto IL_0052;
			IL_0003:
			int num = -1041799221;
			goto IL_0008;
			IL_0008:
			uint num2;
			switch ((num2 = (uint)(num ^ -2037804404)) % 5)
			{
			case 3u:
				break;
			case 0u:
				throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue cannot be less than 0.");
			case 1u:
				goto IL_0052;
			case 2u:
				throw new ArgumentNullException("random");
			default:
				return eXwrrMzIJXjkeyTxuRUoJUWTolL(random, 0, maxValue);
			}
			goto IL_0003;
			IL_0052:
			int num3;
			if (maxValue < 0)
			{
				num = -1679424360;
				num3 = num;
			}
			else
			{
				num = -1389693258;
				num3 = num;
			}
			goto IL_0008;
		}

		public static int Next(this IRandomNumberGenerator random)
		{
			if (random == null)
			{
				while (true)
				{
					uint num;
					switch ((num = 1557484259u) % 3)
					{
					case 0u:
						continue;
					case 2u:
						throw new ArgumentNullException("random");
					}
					break;
				}
			}
			return eXwrrMzIJXjkeyTxuRUoJUWTolL(random, 0, int.MaxValue);
		}

		public static double Next(this IRandomNumberGenerator random, double minValue, double maxValue)
		{
			if (random == null)
			{
				goto IL_0003;
			}
			goto IL_0047;
			IL_0003:
			int num = 1115574422;
			goto IL_0008;
			IL_0008:
			uint num2;
			switch ((num2 = (uint)(num ^ 0xAC336F5)) % 5)
			{
			case 0u:
				break;
			case 2u:
				throw new ArgumentNullException("random");
			case 4u:
				goto IL_0047;
			case 1u:
				throw new ArgumentOutOfRangeException("minValue", minValue, "minValue cannot be greater than maxValue.");
			default:
			{
				double num3 = random.NextDouble();
				double num4 = maxValue - minValue;
				return num3 * num4 + minValue;
			}
			}
			goto IL_0003;
			IL_0047:
			int num5;
			if (minValue <= maxValue)
			{
				num = 1336357367;
				num5 = num;
			}
			else
			{
				num = 739045058;
				num5 = num;
			}
			goto IL_0008;
		}

		public static double Next(this IRandomNumberGenerator random, double maxValue)
		{
			if (random == null)
			{
				goto IL_0003;
			}
			goto IL_0052;
			IL_0003:
			int num = -1930805302;
			goto IL_0008;
			IL_0008:
			uint num2;
			switch ((num2 = (uint)(num ^ -120608374)) % 5)
			{
			case 3u:
				break;
			case 0u:
				throw new ArgumentOutOfRangeException("maxValue", maxValue, "maxValue cannot be less than 0.");
			case 1u:
				goto IL_0052;
			case 2u:
				throw new ArgumentNullException("random");
			default:
				return random.NextDouble() * maxValue;
			}
			goto IL_0003;
			IL_0052:
			int num3;
			if (maxValue >= 0.0)
			{
				num = -804273285;
				num3 = num;
			}
			else
			{
				num = -812749018;
				num3 = num;
			}
			goto IL_0008;
		}

		private static int eXwrrMzIJXjkeyTxuRUoJUWTolL(IRandomNumberGenerator P_0, int P_1, int P_2)
		{
			double num = P_0.NextDouble();
			int num2 = P_2 - P_1;
			return (int)(num * (double)num2) + P_1;
		}
	}
}
