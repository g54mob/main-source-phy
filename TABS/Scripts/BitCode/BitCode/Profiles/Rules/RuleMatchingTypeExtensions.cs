using System;

namespace BitCode.Profiles.Rules
{
	public static class RuleMatchingTypeExtensions
	{
		public static bool Matches(this RuleMatchingType matchingType, sbyte value, sbyte comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = 90568016;
				goto IL_0027;
			case RuleMatchingType.IsNot:
				goto IL_0082;
			case RuleMatchingType.Is:
				goto IL_0091;
			case RuleMatchingType.HasAnyFlag:
				goto IL_00c8;
			case RuleMatchingType.HasNoFlags:
				goto IL_00e4;
			case RuleMatchingType.HasAllFlags:
				goto IL_00f9;
			case RuleMatchingType.AtLeast:
				goto IL_010e;
			case RuleMatchingType.AtMost:
				break;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x6408B033)) % 12)
					{
					case 0u:
						break;
					case 6u:
						return (value & comparedValue) == 0;
					case 2u:
						goto IL_0082;
					case 5u:
						goto IL_0091;
					case 1u:
						return (value & comparedValue) == comparedValue;
					case 11u:
						num = (int)((num2 * 1043481397) ^ 0x52D13EEB);
						continue;
					case 8u:
						goto IL_00c8;
					case 10u:
						goto IL_00e4;
					case 3u:
						goto IL_00f9;
					case 9u:
						goto IL_010e;
					case 4u:
						goto end_IL_0001;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_010e:
				return value >= comparedValue;
				IL_00f9:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = 1922848974;
				goto IL_0027;
				IL_00e4:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = 1565157753;
				goto IL_0027;
				IL_00c8:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) > 0;
				IL_0091:
				return value == comparedValue;
				IL_0082:
				return value != comparedValue;
				end_IL_0001:
				break;
			}
			return value <= comparedValue;
		}

		public static bool Matches(this RuleMatchingType matchingType, short value, short comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = 2111299398;
				goto IL_0027;
			case RuleMatchingType.Is:
				goto IL_0091;
			case RuleMatchingType.HasAnyFlag:
				goto IL_00b6;
			case RuleMatchingType.AtLeast:
				goto IL_00cb;
			case RuleMatchingType.IsNot:
				goto IL_00dd;
			case RuleMatchingType.AtMost:
				goto IL_00ef;
			case RuleMatchingType.HasNoFlags:
				break;
			case RuleMatchingType.HasAllFlags:
				goto IL_011d;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x39274CCF)) % 12)
					{
					case 0u:
						break;
					case 1u:
						num = ((int)num2 * -360518827) ^ -1203385923;
						continue;
					case 5u:
						return (value & comparedValue) == comparedValue;
					case 10u:
						goto IL_0091;
					case 8u:
						return (value & comparedValue) > 0;
					case 11u:
						goto IL_00b6;
					case 2u:
						goto IL_00cb;
					case 4u:
						goto IL_00dd;
					case 6u:
						goto IL_00ef;
					case 3u:
						goto end_IL_0001;
					case 9u:
						goto IL_011d;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_011d:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = 1205424886;
				goto IL_0027;
				IL_00ef:
				return value <= comparedValue;
				IL_00dd:
				return value != comparedValue;
				IL_00cb:
				return value >= comparedValue;
				IL_00b6:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = 1156903223;
				goto IL_0027;
				IL_0091:
				return value == comparedValue;
				end_IL_0001:
				break;
			}
			CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
			return (value & comparedValue) == 0;
		}

		public static bool Matches(this RuleMatchingType matchingType, int value, int comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = -665743372;
				goto IL_0027;
			case RuleMatchingType.IsNot:
				goto IL_006c;
			case RuleMatchingType.HasAllFlags:
				goto IL_0091;
			case RuleMatchingType.HasNoFlags:
				goto IL_00ad;
			case RuleMatchingType.AtMost:
				goto IL_00d4;
			case RuleMatchingType.AtLeast:
				goto IL_00e6;
			case RuleMatchingType.Is:
				break;
			case RuleMatchingType.HasAnyFlag:
				goto IL_0120;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -409038999)) % 12)
					{
					case 6u:
						break;
					case 4u:
						goto IL_006c;
					case 8u:
						return (value & comparedValue) > 0;
					case 2u:
						goto IL_0091;
					case 5u:
						goto IL_00ad;
					case 1u:
						num = ((int)num2 * -401189465) ^ 0x2003D06D;
						continue;
					case 0u:
						goto IL_00d4;
					case 3u:
						goto IL_00e6;
					case 9u:
						return (value & comparedValue) == 0;
					case 10u:
						goto end_IL_0001;
					case 11u:
						goto IL_0120;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_0120:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -1815306955;
				goto IL_0027;
				IL_00e6:
				return value >= comparedValue;
				IL_00d4:
				return value <= comparedValue;
				IL_00ad:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -646329132;
				goto IL_0027;
				IL_0091:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) == comparedValue;
				IL_006c:
				return value != comparedValue;
				end_IL_0001:
				break;
			}
			return value == comparedValue;
		}

		public static bool Matches(this RuleMatchingType matchingType, long value, long comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = 1034703226;
				goto IL_0027;
			case RuleMatchingType.HasAllFlags:
				goto IL_007b;
			case RuleMatchingType.AtMost:
				goto IL_008d;
			case RuleMatchingType.HasAnyFlag:
				goto IL_009c;
			case RuleMatchingType.IsNot:
				goto IL_00ec;
			case RuleMatchingType.Is:
				goto IL_00fe;
			case RuleMatchingType.AtLeast:
				break;
			case RuleMatchingType.HasNoFlags:
				goto IL_011f;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x2F0303E8)) % 12)
					{
					case 7u:
						break;
					case 10u:
						num = ((int)num2 * -1387928477) ^ 0x1495FFC9;
						continue;
					case 9u:
						goto IL_007b;
					case 2u:
						goto IL_008d;
					case 5u:
						goto IL_009c;
					case 1u:
						return (value & comparedValue) == 0;
					case 6u:
						return (value & comparedValue) == comparedValue;
					case 8u:
						goto IL_00ec;
					case 0u:
						goto IL_00fe;
					case 3u:
						goto end_IL_0001;
					case 4u:
						goto IL_011f;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_011f:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = 557469461;
				goto IL_0027;
				IL_00fe:
				return value == comparedValue;
				IL_00ec:
				return value != comparedValue;
				IL_009c:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) > 0;
				IL_008d:
				return value <= comparedValue;
				IL_007b:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = 1710552854;
				goto IL_0027;
				end_IL_0001:
				break;
			}
			return value >= comparedValue;
		}

		public static bool Matches(this RuleMatchingType matchingType, byte value, byte comparedValue)
		{
			switch (matchingType)
			{
			default:
				while (true)
				{
					int num = -1664042039;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -1820062957)) % 10)
						{
						case 0u:
							break;
						case 9u:
							goto end_IL_0022;
						case 6u:
							goto IL_007d;
						case 8u:
							num = (int)((num2 * 770074790) ^ 0x5EDEEC2);
							continue;
						case 5u:
							goto IL_009b;
						case 1u:
							goto IL_00b7;
						case 3u:
							goto IL_00c6;
						case 4u:
							goto IL_00e2;
						case 2u:
							goto end_IL_0001;
						default:
							throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
						}
						break;
					}
					continue;
					end_IL_0022:
					break;
				}
				goto case RuleMatchingType.HasAllFlags;
			case RuleMatchingType.HasAllFlags:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) == comparedValue;
			case RuleMatchingType.AtLeast:
				goto IL_007d;
			case RuleMatchingType.HasAnyFlag:
				goto IL_009b;
			case RuleMatchingType.Is:
				goto IL_00b7;
			case RuleMatchingType.HasNoFlags:
				goto IL_00c6;
			case RuleMatchingType.IsNot:
				goto IL_00e2;
			case RuleMatchingType.AtMost:
				break;
				IL_00e2:
				return value != comparedValue;
				IL_00c6:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) == 0;
				IL_00b7:
				return value == comparedValue;
				IL_009b:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) > 0;
				IL_007d:
				return value >= comparedValue;
				end_IL_0001:
				break;
			}
			return value <= comparedValue;
		}

		public static bool Matches(this RuleMatchingType matchingType, ushort value, ushort comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = -785882359;
				goto IL_0027;
			case RuleMatchingType.HasNoFlags:
				goto IL_0082;
			case RuleMatchingType.AtLeast:
				goto IL_00ad;
			case RuleMatchingType.HasAnyFlag:
				goto IL_00bf;
			case RuleMatchingType.AtMost:
				goto IL_00d4;
			case RuleMatchingType.IsNot:
				goto IL_00ff;
			case RuleMatchingType.HasAllFlags:
				goto IL_0111;
			case RuleMatchingType.Is:
				break;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1414351545)) % 12)
					{
					case 0u:
						break;
					case 9u:
						return (value & comparedValue) > 0;
					case 3u:
						goto IL_0082;
					case 10u:
						num = ((int)num2 * -1108581693) ^ 0x165745B6;
						continue;
					case 5u:
						goto IL_00ad;
					case 6u:
						goto IL_00bf;
					case 11u:
						goto IL_00d4;
					case 2u:
						return (value & comparedValue) == comparedValue;
					case 4u:
						goto IL_00ff;
					case 1u:
						goto IL_0111;
					case 8u:
						goto end_IL_0001;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_0111:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -1403521695;
				goto IL_0027;
				IL_00ff:
				return value != comparedValue;
				IL_00d4:
				return value <= comparedValue;
				IL_00bf:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -705529066;
				goto IL_0027;
				IL_00ad:
				return value >= comparedValue;
				IL_0082:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) == 0;
				end_IL_0001:
				break;
			}
			return value == comparedValue;
		}

		public static bool Matches(this RuleMatchingType matchingType, uint value, uint comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = -573850290;
				goto IL_0027;
			case RuleMatchingType.HasAllFlags:
				goto IL_0068;
			case RuleMatchingType.HasAnyFlag:
				goto IL_0081;
			case RuleMatchingType.AtMost:
				goto IL_009a;
			case RuleMatchingType.HasNoFlags:
				goto IL_00ac;
			case RuleMatchingType.Is:
				goto IL_00c1;
			case RuleMatchingType.AtLeast:
				goto IL_00d0;
			case RuleMatchingType.IsNot:
				break;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -57259584)) % 11)
					{
					case 9u:
						break;
					case 7u:
						goto IL_0068;
					case 1u:
						goto IL_0081;
					case 2u:
						goto IL_009a;
					case 0u:
						goto IL_00ac;
					case 4u:
						goto IL_00c1;
					case 10u:
						goto IL_00d0;
					case 5u:
						return (value & comparedValue) == 0;
					case 3u:
						goto end_IL_0001;
					case 6u:
						num = (int)((num2 * 1318506625) ^ 0x28C06915);
						continue;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_00d0:
				return value >= comparedValue;
				IL_00c1:
				return value == comparedValue;
				IL_00ac:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -326212999;
				goto IL_0027;
				IL_009a:
				return value <= comparedValue;
				IL_0081:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) != 0;
				IL_0068:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				return (value & comparedValue) == comparedValue;
				end_IL_0001:
				break;
			}
			return value != comparedValue;
		}

		public static bool Matches(this RuleMatchingType matchingType, ulong value, ulong comparedValue)
		{
			int num;
			switch (matchingType)
			{
			default:
				num = -1180970343;
				goto IL_0027;
			case RuleMatchingType.AtLeast:
				goto IL_009d;
			case RuleMatchingType.IsNot:
				goto IL_00af;
			case RuleMatchingType.HasAnyFlag:
				goto IL_00d3;
			case RuleMatchingType.AtMost:
				goto IL_00e8;
			case RuleMatchingType.HasNoFlags:
				goto IL_0114;
			case RuleMatchingType.Is:
				break;
			case RuleMatchingType.HasAllFlags:
				goto IL_0138;
				IL_0027:
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -2089396639)) % 13)
					{
					case 11u:
						break;
					case 5u:
						return (value & comparedValue) != 0;
					case 4u:
						return (value & comparedValue) == comparedValue;
					case 9u:
						goto IL_009d;
					case 6u:
						goto IL_00af;
					case 8u:
						num = ((int)num2 * -120376940) ^ -1376903652;
						continue;
					case 0u:
						goto IL_00d3;
					case 12u:
						goto IL_00e8;
					case 7u:
						return (value & comparedValue) == 0;
					case 10u:
						goto IL_0114;
					case 2u:
						goto end_IL_0001;
					case 1u:
						goto IL_0138;
					default:
						throw new ArgumentOutOfRangeException("matchingType", "An unknown or unsupported matching type was provided.");
					}
					break;
				}
				goto default;
				IL_0138:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -787388055;
				goto IL_0027;
				IL_0114:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -1341129783;
				goto IL_0027;
				IL_00e8:
				return value <= comparedValue;
				IL_00d3:
				CTCSfFesczEKjKBDKjgiUnEhTHHGA(comparedValue, "comparedValue");
				num = -2007821803;
				goto IL_0027;
				IL_00af:
				return value != comparedValue;
				IL_009d:
				return value >= comparedValue;
				end_IL_0001:
				break;
			}
			return value == comparedValue;
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(sbyte P_0, string P_1)
		{
			if (P_0 != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1061632160u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 1u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(short P_0, string P_1)
		{
			if (P_0 != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1357243759u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 2u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(int P_0, string P_1)
		{
			if (P_0 != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 166112291u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 1u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(long P_0, string P_1)
		{
			if (P_0 != 0L)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 904270235u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 1u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(byte P_0, string P_1)
		{
			if (P_0 != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1267902844u) % 3)
				{
				case 2u:
					break;
				default:
					return;
				case 1u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 0u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(ushort P_0, string P_1)
		{
			if (P_0 != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 63033437u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 1u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(uint P_0, string P_1)
		{
			if (P_0 != 0)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1828415990u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 1u:
					return;
				}
			}
		}

		private static void CTCSfFesczEKjKBDKjgiUnEhTHHGA(ulong P_0, string P_1)
		{
			if (P_0 != 0L)
			{
				return;
			}
			while (true)
			{
				uint num;
				switch ((num = 1479734060u) % 3)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					throw new ArgumentException("Compare value must not be zero with 'Has' checks.", P_1);
				case 1u:
					return;
				}
			}
		}
	}
}
