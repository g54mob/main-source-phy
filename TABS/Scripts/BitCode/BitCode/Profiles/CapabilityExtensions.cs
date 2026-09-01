using System;
using BitCode.Profiles.Rules;

namespace BitCode.Profiles
{
	public static class CapabilityExtensions
	{
		public static bool Matches<TCapabilityLevel>(this ICapability<TCapabilityLevel> capability, RuleMatchingType matchingType, TCapabilityLevel testedLevel) where TCapabilityLevel : Enum
		{
			TypeCode typeCode = testedLevel.GetTypeCode();
			while (true)
			{
				int num = -318854738;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1643210257)) % 12)
					{
					case 2u:
						break;
					case 6u:
						return matchingType.Matches(Convert.ToUInt16(capability.Level), Convert.ToUInt16(testedLevel));
					case 7u:
						goto IL_0081;
					case 4u:
						goto IL_00ad;
					case 3u:
						goto IL_00d9;
					case 0u:
						num = (int)(num2 * 1938354421) ^ -2115172130;
						continue;
					case 5u:
						switch (typeCode)
						{
						case TypeCode.UInt16:
							break;
						case TypeCode.Int16:
							goto IL_0081;
						case TypeCode.UInt64:
							goto IL_00ad;
						case TypeCode.SByte:
							goto IL_00d9;
						default:
							goto IL_0169;
						case TypeCode.UInt32:
							goto IL_017b;
						case TypeCode.Int64:
							goto IL_01a7;
						case TypeCode.Int32:
							goto IL_01d3;
						case TypeCode.Byte:
							goto IL_01ff;
						case TypeCode.Empty:
						case TypeCode.Object:
						case TypeCode.DBNull:
						case TypeCode.Boolean:
						case TypeCode.Char:
						case TypeCode.Single:
						case TypeCode.Double:
						case TypeCode.Decimal:
						case TypeCode.DateTime:
						case (TypeCode)17:
						case TypeCode.String:
							goto IL_022b;
						}
						goto case 6u;
					case 11u:
						goto IL_017b;
					case 8u:
						goto IL_01a7;
					case 10u:
						goto IL_01d3;
					case 9u:
						goto IL_01ff;
					default:
						goto IL_022b;
						IL_022b:
						throw new NotImplementedException($"Matching behaviour for enums of underlying type {typeCode} is not implemented.");
						IL_01ff:
						return matchingType.Matches(Convert.ToByte(capability.Level), Convert.ToByte(testedLevel));
						IL_01d3:
						return matchingType.Matches(Convert.ToInt32(capability.Level), Convert.ToInt32(testedLevel));
						IL_01a7:
						return matchingType.Matches(Convert.ToInt64(capability.Level), Convert.ToInt64(testedLevel));
						IL_017b:
						return matchingType.Matches(Convert.ToUInt32(capability.Level), Convert.ToUInt32(testedLevel));
						IL_0169:
						num = (int)((num2 * 448885960) ^ 0x63194F7B);
						continue;
						IL_00d9:
						return matchingType.Matches(Convert.ToSByte(capability.Level), Convert.ToSByte(testedLevel));
						IL_00ad:
						return matchingType.Matches(Convert.ToUInt64(capability.Level), Convert.ToUInt64(testedLevel));
						IL_0081:
						return matchingType.Matches(Convert.ToInt16(capability.Level), Convert.ToInt16(testedLevel));
					}
					break;
				}
			}
		}
	}
}
