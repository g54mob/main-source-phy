using System;
using System.Globalization;

namespace BitCode.Extensions
{
	public static class NumberExtensions
	{
		internal const int BDKRpkJbMmuxdbpltYoNdozjxZmF = 0;

		internal const int DefaultDecimals = 2;

		private const string PECDmwFSvyckGDZxOKsBVEZreuesA = "B";

		private const string jXSfTzRxXNOYhBjiKNZTYCoPFDjz = "KiB";

		private const string QTgtzdEpuesJEiZCahUKBMlcLdso = "MiB";

		private const string VqoiMrKYKBCblVWbOHUesSrMEgMIA = "GiB";

		private const string xdrTbSOFUmwVjihnXRUgdbqiksBk = "TiB";

		private static readonly NumberFormatInfo prKyIjsLcoZUPajAjcPrHOCBeUIF = new NumberFormatInfo
		{
			NumberDecimalDigits = 0
		};

		private static readonly NumberFormatInfo POIbwPglmQoiIuIYvtNuvBBnFlfPA = new NumberFormatInfo
		{
			NumberDecimalDigits = 2
		};

		private const double xgCodIMcVheNVXKUCNARagctTYPD = 0.0009765625;

		private const ulong pMiEcxnoYyEUJdRmdhIzBiBlYVipA = 8192uL;

		private const double tODIDZCPVfgOZLzuztXynhQsAqQk = 9.5367431640625E-07;

		private const ulong HHYNospPDIdUiBqaqinTJzyZzAVDA = 8388608uL;

		private const double aKmfCieZLemYetPBCCbGCGmlkQWk = 9.313225746154785E-10;

		private const ulong KExaFXUnmAQeqwWaowKldvdnABBT = 8589934592uL;

		private const double AYAdXQBpRVkTbdaOeGjVHmaXFwkAA = 9.094947017729282E-13;

		private const ulong ezOcaceNFgjZRmfeSqCdRlHlcHUQ = 8796093022208uL;

		public static string MemoryQuantityToString(this ulong bytes, int decimals = 2)
		{
			string arg = "B";
			double num = 1.0;
			while (true)
			{
				int num2 = 1343769374;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x6FEEB77)) % 16)
					{
					case 12u:
						break;
					case 9u:
					{
						int num7;
						int num8;
						if (bytes >= 8796093022208L)
						{
							num7 = 1816159753;
							num8 = num7;
						}
						else
						{
							num7 = 935629013;
							num8 = num7;
						}
						num2 = num7 ^ ((int)num3 * -658886117);
						continue;
					}
					case 0u:
						arg = "KiB";
						num2 = ((int)num3 * -1624731506) ^ 0x46BB3194;
						continue;
					case 5u:
						decimals = 0;
						num2 = 1731488371;
						continue;
					case 11u:
						num = 9.094947017729282E-13;
						num2 = (int)((num3 * 801931446) ^ 0x6E3FD857);
						continue;
					case 2u:
						num2 = ((int)num3 * -1634867100) ^ 0x7EC92D7B;
						continue;
					case 6u:
						arg = "MiB";
						num = 9.5367431640625E-07;
						num2 = (int)(num3 * 1931846885) ^ -330568025;
						continue;
					case 3u:
						num = 0.0009765625;
						num2 = (int)((num3 * 1865033529) ^ 0x4681718);
						continue;
					case 7u:
						num = 9.313225746154785E-10;
						num2 = (int)((num3 * 1263452952) ^ 0x418B5E5B);
						continue;
					case 13u:
						arg = "TiB";
						num2 = ((int)num3 * -217499771) ^ 0x5AB322CD;
						continue;
					case 10u:
						arg = "GiB";
						num2 = ((int)num3 * -944838681) ^ -473605818;
						continue;
					case 15u:
					{
						int num6;
						if (bytes < 8192)
						{
							num2 = 2056994338;
							num6 = num2;
						}
						else
						{
							num2 = 738443399;
							num6 = num2;
						}
						continue;
					}
					case 8u:
					{
						int num5;
						if (bytes < 8388608)
						{
							num2 = 497471672;
							num5 = num2;
						}
						else
						{
							num2 = 824508497;
							num5 = num2;
						}
						continue;
					}
					case 14u:
						num2 = ((int)num3 * -527916469) ^ 0x22B25A09;
						continue;
					case 1u:
					{
						int num4;
						if (bytes < 8589934592L)
						{
							num2 = 237260207;
							num4 = num2;
						}
						else
						{
							num2 = 1987600957;
							num4 = num2;
						}
						continue;
					}
					default:
						return string.Format(VOVgmAsUQTNEVSBcVAYeEyqYUqFWA(decimals), "{0:F} {1}", (double)bytes * num, arg);
					}
					break;
				}
			}
		}

		public static string MemoryQuantityToString(this long bytes, int decimals = 2)
		{
			if (bytes < 0)
			{
				goto IL_0008;
			}
			goto IL_00f6;
			IL_0008:
			int num = -1369090766;
			goto IL_000d;
			IL_000d:
			string arg = default(string);
			double num3 = default(double);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -1491342225)) % 18)
				{
				case 3u:
					break;
				case 2u:
					arg = "MiB";
					num3 = 9.5367431640625E-07;
					num = ((int)num2 * -2118284599) ^ 0x1CE98299;
					continue;
				case 7u:
					goto IL_0089;
				case 11u:
					arg = "KiB";
					num3 = 0.0009765625;
					num = (int)((num2 * 178990224) ^ 0xE602DB);
					continue;
				case 0u:
					num = ((int)num2 * -1245531197) ^ -1097205947;
					continue;
				case 10u:
					num3 = 9.313225746154785E-10;
					num = (int)(num2 * 806728718) ^ -217819044;
					continue;
				case 4u:
					goto IL_00f6;
				case 12u:
					arg = "TiB";
					num = ((int)num2 * -1795838155) ^ 0x54F1E319;
					continue;
				case 14u:
					num = (int)(num2 * 717034007) ^ -190953695;
					continue;
				case 16u:
					decimals = 0;
					num = -1655150563;
					continue;
				case 1u:
					num = (int)((num2 * 257105817) ^ 0x3880D2DA);
					continue;
				case 5u:
					throw new ArgumentOutOfRangeException("bytes", bytes, "Bytes should not be negative.");
				case 15u:
					goto IL_0197;
				case 17u:
					goto IL_01b7;
				case 6u:
					num3 = 9.094947017729282E-13;
					num = (int)(num2 * 763459634) ^ -1873757880;
					continue;
				case 9u:
					num = (int)(num2 * 218346119) ^ -523154696;
					continue;
				case 13u:
					arg = "GiB";
					num = ((int)num2 * -2105531063) ^ 0x6A87029A;
					continue;
				default:
					return string.Format(VOVgmAsUQTNEVSBcVAYeEyqYUqFWA(decimals), "{0:F} {1}", (double)bytes * num3, arg);
				}
				break;
				IL_01b7:
				int num4;
				if (bytes < 8192)
				{
					num = -681528545;
					num4 = num;
				}
				else
				{
					num = -1311856882;
					num4 = num;
				}
				continue;
				IL_0197:
				int num5;
				if (bytes >= 8589934592L)
				{
					num = -82454504;
					num5 = num;
				}
				else
				{
					num = -1836622386;
					num5 = num;
				}
				continue;
				IL_0089:
				int num6;
				if (bytes < 8388608)
				{
					num = -718751912;
					num6 = num;
				}
				else
				{
					num = -543089119;
					num6 = num;
				}
			}
			goto IL_0008;
			IL_00f6:
			arg = "B";
			num3 = 1.0;
			int num7;
			if (bytes < 8796093022208L)
			{
				num = -1551375502;
				num7 = num;
			}
			else
			{
				num = -1014022061;
				num7 = num;
			}
			goto IL_000d;
		}

		public static string MemoryQuantityToString(this uint bytes, int decimals = 2)
		{
			string arg = "B";
			double num3 = default(double);
			while (true)
			{
				int num = 942378906;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0xCF07B35)) % 11)
					{
					case 0u:
						break;
					case 4u:
						arg = "KiB";
						num = (int)((num2 * 615144743) ^ 0x72123F22);
						continue;
					case 7u:
					{
						int num5;
						int num6;
						if ((ulong)bytes >= 8388608uL)
						{
							num5 = -1459394446;
							num6 = num5;
						}
						else
						{
							num5 = -656220936;
							num6 = num5;
						}
						num = num5 ^ ((int)num2 * -706561149);
						continue;
					}
					case 9u:
						decimals = 0;
						num = 818465651;
						continue;
					case 6u:
					{
						int num4;
						if ((ulong)bytes < 8192uL)
						{
							num = 490476223;
							num4 = num;
						}
						else
						{
							num = 1086464013;
							num4 = num;
						}
						continue;
					}
					case 10u:
						num3 = 0.0009765625;
						num = ((int)num2 * -1679070697) ^ -2053505367;
						continue;
					case 3u:
						num3 = 1.0;
						num = ((int)num2 * -1119464168) ^ 0x2696EF91;
						continue;
					case 8u:
						num = ((int)num2 * -812607403) ^ 0x110837CA;
						continue;
					case 1u:
						arg = "MiB";
						num3 = 9.5367431640625E-07;
						num = (int)(num2 * 718880604) ^ -1649785559;
						continue;
					case 2u:
						num = (int)(num2 * 461504874) ^ -1816322781;
						continue;
					default:
						return string.Format(VOVgmAsUQTNEVSBcVAYeEyqYUqFWA(decimals), "{0:F} {1}", (double)bytes * num3, arg);
					}
					break;
				}
			}
		}

		public static string MemoryQuantityToString(this int bytes, int decimals = 2)
		{
			if (bytes < 0)
			{
				goto IL_0004;
			}
			goto IL_0061;
			IL_0004:
			int num = 2062885183;
			goto IL_0009;
			IL_0009:
			string arg = default(string);
			double num3 = default(double);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x20552BCB)) % 13)
				{
				case 12u:
					break;
				case 8u:
					num = (int)(num2 * 1049457714) ^ -788578169;
					continue;
				case 11u:
					goto IL_0061;
				case 5u:
					goto IL_006e;
				case 9u:
					num = ((int)num2 * -1163756756) ^ 0x5A7F3ED5;
					continue;
				case 0u:
					decimals = 0;
					num = 613664729;
					continue;
				case 2u:
					arg = "KiB";
					num = ((int)num2 * -1839942175) ^ -973235797;
					continue;
				case 7u:
					throw new ArgumentOutOfRangeException("bytes", bytes, "Bytes should not be negative.");
				case 6u:
				{
					int num4;
					int num5;
					if (bytes >= 8388608)
					{
						num4 = 1932040680;
						num5 = num4;
					}
					else
					{
						num4 = 846675613;
						num5 = num4;
					}
					num = num4 ^ ((int)num2 * -725135920);
					continue;
				}
				case 3u:
					num3 = 0.0009765625;
					num = (int)((num2 * 392917658) ^ 0x30A079DE);
					continue;
				case 4u:
					arg = "MiB";
					num3 = 9.5367431640625E-07;
					num = (int)((num2 * 926496194) ^ 0x3F17E464);
					continue;
				case 1u:
					num3 = 1.0;
					num = ((int)num2 * -799240244) ^ -77114027;
					continue;
				default:
					return string.Format(VOVgmAsUQTNEVSBcVAYeEyqYUqFWA(decimals), "{0:F} {1}", (double)bytes * num3, arg);
				}
				break;
				IL_006e:
				int num6;
				if (bytes < 8192)
				{
					num = 1308492164;
					num6 = num;
				}
				else
				{
					num = 1370414374;
					num6 = num;
				}
			}
			goto IL_0004;
			IL_0061:
			arg = "B";
			num = 1635610629;
			goto IL_0009;
		}

		private static NumberFormatInfo VOVgmAsUQTNEVSBcVAYeEyqYUqFWA(int P_0)
		{
			if (P_0 != 0)
			{
				while (true)
				{
					int num = -538961123;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -167759542)) % 6)
						{
						case 3u:
							break;
						case 5u:
							return POIbwPglmQoiIuIYvtNuvBBnFlfPA;
						case 4u:
							num = (int)(num2 * 1369893346) ^ -1542280380;
							continue;
						case 1u:
						{
							int num3;
							int num4;
							if (P_0 == 2)
							{
								num3 = -1063006898;
								num4 = num3;
							}
							else
							{
								num3 = -968499631;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -2048936785);
							continue;
						}
						case 0u:
							goto end_IL_0003;
						default:
							return new NumberFormatInfo
							{
								NumberDecimalDigits = P_0
							};
						}
						break;
					}
					continue;
					end_IL_0003:
					break;
				}
			}
			return prKyIjsLcoZUPajAjcPrHOCBeUIF;
		}
	}
}
