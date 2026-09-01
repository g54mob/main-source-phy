using System;
using System.Collections.Generic;
using BitCode.Maths;

namespace BitCode.Extensions
{
	public static class ListExtensions
	{
		private static readonly IRandomNumberGenerator ohkqGRybqAfuKgOfxitVBEbUARBxA = new DotNetRandomNumberGenerator();

		public static T WeightedSelection<T>(this IList<T> elements, float weightSum, Func<T, float> getElementWeight, IRandomNumberGenerator randomiser = null)
		{
			int index = elements.WeightedSelectionIndex(weightSum, getElementWeight, randomiser ?? ohkqGRybqAfuKgOfxitVBEbUARBxA);
			return elements[index];
		}

		public static T WeightedSelection<T>(this IList<T> elements, int weightSum, Func<T, int> getElementWeight, IRandomNumberGenerator randomiser = null)
		{
			int index = elements.WeightedSelectionIndex(weightSum, getElementWeight, randomiser ?? ohkqGRybqAfuKgOfxitVBEbUARBxA);
			return elements[index];
		}

		public static int WeightedSelectionIndex<T>(this IList<T> elements, int weightSum, Func<T, int> getElementWeight, IRandomNumberGenerator randomiser = null)
		{
			if (weightSum <= 0)
			{
				goto IL_0007;
			}
			goto IL_012b;
			IL_0007:
			int num = -189224367;
			goto IL_000c;
			IL_000c:
			int num3 = default(int);
			int count = default(int);
			int num4 = default(int);
			int num5 = default(int);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1104186259)) % 15)
				{
				case 6u:
					break;
				case 10u:
				{
					num3++;
					int num8;
					int num9;
					if (num3 < count)
					{
						num8 = -606277603;
						num9 = num8;
					}
					else
					{
						num8 = -748079200;
						num9 = num8;
					}
					num = num8 ^ (int)(num2 * 1151572926);
					continue;
				}
				case 4u:
					throw new ArgumentException("Weighted selection exceeded indexable range. Is your weightSum correct?", "weightSum");
				case 12u:
					throw new ArgumentException("WeightSum should be a positive value", "weightSum");
				case 13u:
				{
					int num6;
					int num7;
					if (count == 0)
					{
						num6 = -772083215;
						num7 = num6;
					}
					else
					{
						num6 = -1603729624;
						num7 = num6;
					}
					num = num6 ^ ((int)num2 * -565692038);
					continue;
				}
				case 14u:
					count = elements.Count;
					num = ((int)num2 * -1704737013) ^ 0xE3FAE99;
					continue;
				case 2u:
					num4 = getElementWeight(elements[num3]);
					num = -2139027861;
					continue;
				case 11u:
					num = (int)((num2 * 968272705) ^ 0x10FDE63D);
					continue;
				case 1u:
					goto IL_012b;
				case 3u:
					goto IL_0141;
				case 7u:
					num3 = 0;
					num5 = randomiser.Next(weightSum);
					num = (int)(num2 * 1473602097) ^ -1244328510;
					continue;
				case 9u:
					throw new InvalidOperationException("Cannot perform selection on an empty collection");
				case 8u:
					num5 -= num4;
					num = -1069607007;
					continue;
				case 5u:
					num4 = getElementWeight(elements[num3]);
					num = -968551493;
					continue;
				default:
					return num3;
				}
				break;
				IL_0141:
				int num10;
				if (num5 >= num4)
				{
					num = -2070741782;
					num10 = num;
				}
				else
				{
					num = -242927807;
					num10 = num;
				}
			}
			goto IL_0007;
			IL_012b:
			randomiser = randomiser ?? ohkqGRybqAfuKgOfxitVBEbUARBxA;
			num = -1015522711;
			goto IL_000c;
		}

		public static int WeightedSelectionIndex<T>(this IList<T> elements, float weightSum, Func<T, float> getElementWeight, IRandomNumberGenerator randomiser = null)
		{
			if (weightSum <= 0f)
			{
				goto IL_000b;
			}
			goto IL_00b5;
			IL_000b:
			int num = 969189324;
			goto IL_0010;
			IL_0010:
			int num3 = default(int);
			int count = default(int);
			double num6 = default(double);
			double num9 = default(double);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x1B993B70)) % 15)
				{
				case 11u:
					break;
				case 3u:
				{
					int num7;
					int num8;
					if (num3 >= count)
					{
						num7 = 731582530;
						num8 = num7;
					}
					else
					{
						num7 = 1771436199;
						num8 = num7;
					}
					num = num7 ^ ((int)num2 * -1741247136);
					continue;
				}
				case 13u:
					num6 = randomiser.Next(weightSum);
					num = ((int)num2 * -23323968) ^ -2139602105;
					continue;
				case 5u:
					num9 = getElementWeight(elements[num3]);
					num = 1718532823;
					continue;
				case 14u:
					goto IL_00b5;
				case 4u:
					num9 = getElementWeight(elements[num3]);
					num = 1718532823;
					continue;
				case 6u:
					throw new ArgumentException("WeightSum should be a positive value", "weightSum");
				case 0u:
					num6 -= num9;
					num = 1511601841;
					continue;
				case 12u:
					throw new ArgumentException("Weighted selection exceeded indexable range. Is your weightSum correct?", "weightSum");
				case 9u:
					num3++;
					num = (int)((num2 * 472968549) ^ 0x777A0B00);
					continue;
				case 8u:
					goto IL_014f;
				case 1u:
					num3 = 0;
					num = (int)(num2 * 396010275) ^ -2043406561;
					continue;
				case 7u:
					throw new InvalidOperationException("Cannot perform selection on an empty collection");
				case 2u:
				{
					count = elements.Count;
					int num4;
					int num5;
					if (count != 0)
					{
						num4 = -1536579366;
						num5 = num4;
					}
					else
					{
						num4 = -1522558245;
						num5 = num4;
					}
					num = num4 ^ (int)(num2 * 295516299);
					continue;
				}
				default:
					return num3;
				}
				break;
				IL_014f:
				int num10;
				if (!(num6 >= num9))
				{
					num = 1398223395;
					num10 = num;
				}
				else
				{
					num = 347235485;
					num10 = num;
				}
			}
			goto IL_000b;
			IL_00b5:
			randomiser = randomiser ?? ohkqGRybqAfuKgOfxitVBEbUARBxA;
			num = 564325984;
			goto IL_0010;
		}

		public static T[] Shuffle<T>(this IList<T> original, IRandomNumberGenerator randomiser = null)
		{
			randomiser = randomiser ?? ohkqGRybqAfuKgOfxitVBEbUARBxA;
			T[] array = default(T[]);
			int num4 = default(int);
			int num3 = default(int);
			int count = default(int);
			while (true)
			{
				int num = -1439668043;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1001488191)) % 9)
					{
					case 0u:
						break;
					case 7u:
						array[num4] = array[num3];
						num = ((int)num2 * -777459641) ^ -365564243;
						continue;
					case 3u:
						num4++;
						num = (int)(num2 * 916573387) ^ -1499589509;
						continue;
					case 2u:
						count = original.Count;
						array = new T[count];
						num4 = 0;
						num = ((int)num2 * -1165034416) ^ 0x51038D42;
						continue;
					case 6u:
					{
						int num6;
						int num7;
						if (num3 != num4)
						{
							num6 = -552847494;
							num7 = num6;
						}
						else
						{
							num6 = -609770384;
							num7 = num6;
						}
						num = num6 ^ (int)(num2 * 1746946189);
						continue;
					}
					case 8u:
						array[num3] = original[num4];
						num = -62674294;
						continue;
					case 1u:
					{
						int num5;
						if (num4 >= count)
						{
							num = -790892905;
							num5 = num;
						}
						else
						{
							num = -1032179761;
							num5 = num;
						}
						continue;
					}
					case 4u:
						num3 = randomiser.Next(num4 + 1);
						num = -656815711;
						continue;
					default:
						return array;
					}
					break;
				}
			}
		}
	}
}
