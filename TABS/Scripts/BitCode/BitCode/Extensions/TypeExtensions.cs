using System;

namespace BitCode.Extensions
{
	public static class TypeExtensions
	{
		public static Type FindGenericAncestor(this Type type, Type openGenericType)
		{
			if (openGenericType.IsGenericTypeDefinition)
			{
				Type type3 = default(Type);
				Type[] interfaces = default(Type[]);
				int num7 = default(int);
				Type type2 = default(Type);
				while (true)
				{
					int num = -1210747979;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1371393076)) % 20)
						{
						case 17u:
							break;
						case 8u:
							goto IL_0076;
						case 14u:
							type3 = interfaces[num7];
							num = -1564297239;
							continue;
						case 15u:
							goto end_IL_000b;
						case 1u:
						{
							int num5;
							int num6;
							if (openGenericType.IsGenericType)
							{
								num5 = 627934551;
								num6 = num5;
							}
							else
							{
								num5 = 810706096;
								num6 = num5;
							}
							num = num5 ^ ((int)num2 * -1655114861);
							continue;
						}
						case 18u:
							num7++;
							num = -2046139877;
							continue;
						case 4u:
							return type3;
						case 7u:
							num = ((int)num2 * -859947649) ^ -316824917;
							continue;
						case 13u:
						{
							int num8;
							int num9;
							if (type2.GetGenericTypeDefinition() == openGenericType)
							{
								num8 = 1021635082;
								num9 = num8;
							}
							else
							{
								num8 = 1380598923;
								num9 = num8;
							}
							num = num8 ^ (int)(num2 * 1937849736);
							continue;
						}
						case 10u:
							goto IL_013d;
						case 9u:
						{
							int num10;
							int num11;
							if (type3.IsGenericType)
							{
								num10 = 872743903;
								num11 = num10;
							}
							else
							{
								num10 = 1848384002;
								num11 = num10;
							}
							num = num10 ^ (int)(num2 * 1060979832);
							continue;
						}
						case 5u:
							num = ((int)num2 * -935087338) ^ 0x75A131D;
							continue;
						case 0u:
							type2 = type;
							num = -745260641;
							continue;
						case 2u:
							num7 = 0;
							num = ((int)num2 * -185559507) ^ -765015905;
							continue;
						case 11u:
							interfaces = type2.GetInterfaces();
							num = -309394898;
							continue;
						case 3u:
							goto IL_01c4;
						case 19u:
						{
							int num3;
							int num4;
							if (type3.GetGenericTypeDefinition() == openGenericType)
							{
								num3 = -1544877915;
								num4 = num3;
							}
							else
							{
								num3 = -682650009;
								num4 = num3;
							}
							num = num3 ^ (int)(num2 * 1079414423);
							continue;
						}
						case 6u:
							return type2;
						case 12u:
							type2 = type2.BaseType;
							num = ((int)num2 * -35150917) ^ -948954958;
							continue;
						default:
							return null;
						}
						break;
						IL_01c4:
						int num12;
						if (num7 < interfaces.Length)
						{
							num = -1727993770;
							num12 = num;
						}
						else
						{
							num = -28930928;
							num12 = num;
						}
						continue;
						IL_013d:
						int num13;
						if (type2 != null)
						{
							num = -703468296;
							num13 = num;
						}
						else
						{
							num = -1122305556;
							num13 = num;
						}
						continue;
						IL_0076:
						int num14;
						if (!type2.IsGenericType)
						{
							num = -88409309;
							num14 = num;
						}
						else
						{
							num = -766870967;
							num14 = num;
						}
					}
					continue;
					end_IL_000b:
					break;
				}
			}
			throw new ArgumentException("Provided type must be an open generic type", "openGenericType");
		}

		public static bool HasGenericAncestor(this Type type, Type openGenericType)
		{
			return type.FindGenericAncestor(openGenericType) != null;
		}
	}
}
