using System;
using BitCode.L10n;
using Steamworks;

namespace BitCode.Platform.Steamworks
{
	public class SteamLanguageProvider : IPlatformService, ISystemLanguageProvider
	{
		private readonly bool WvSqCESSFCdycBpaSHYINYQGdEwFA;

		event Action<IPlatformService, Exception> IPlatformService.InternalErrorOccurred
		{
			add
			{
			}
			remove
			{
			}
		}

		public SteamLanguageProvider(bool getUiLanguage = false)
		{
			while (true)
			{
				int num = 171074187;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x33D8B644)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						goto IL_0028;
					case 1u:
						return;
					}
					break;
					IL_0028:
					WvSqCESSFCdycBpaSHYINYQGdEwFA = getUiLanguage;
					num = (int)((num2 * 672148224) ^ 0xD2E958B);
				}
			}
		}

		public string GetLanguageCode()
		{
			if (!WvSqCESSFCdycBpaSHYINYQGdEwFA)
			{
				goto IL_000b;
			}
			string text = SteamUtils.GetSteamUILanguage();
			goto IL_05f7;
			IL_05f7:
			string text2 = text;
			int num = 2047133342;
			goto IL_0010;
			IL_000b:
			num = 381809428;
			goto IL_0010;
			IL_0010:
			uint num3 = default(uint);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x3C4B4957)) % 146)
				{
				case 137u:
					break;
				case 117u:
					goto IL_0270;
				case 70u:
					goto IL_0291;
				case 119u:
					return "vi";
				case 108u:
					return "pt-PT";
				case 99u:
					num = (int)(num2 * 876885221) ^ -117175436;
					continue;
				case 27u:
					goto IL_02df;
				case 87u:
					num = (int)(num2 * 494879203) ^ -559662658;
					continue;
				case 128u:
					goto IL_030d;
				case 12u:
				{
					int num40;
					int num41;
					if (num3 != 2805355685u)
					{
						num40 = -1576952341;
						num41 = num40;
					}
					else
					{
						num40 = -907683043;
						num41 = num40;
					}
					num = num40 ^ ((int)num2 * -654644136);
					continue;
				}
				case 88u:
					return "da";
				case 2u:
					num = (int)(num2 * 1011382415) ^ -1944593209;
					continue;
				case 1u:
					num = (int)(num2 * 1213959507) ^ -380207680;
					continue;
				case 28u:
					goto IL_0381;
				case 73u:
				{
					int num16;
					int num17;
					if (num3 != 2798875500u)
					{
						num16 = 516465285;
						num17 = num16;
					}
					else
					{
						num16 = 1092532615;
						num17 = num16;
					}
					num = num16 ^ ((int)num2 * -386912838);
					continue;
				}
				case 4u:
					return "it";
				case 111u:
					return "zh-Hans";
				case 78u:
					goto IL_03e6;
				case 53u:
					return "hu";
				case 45u:
					num = (int)((num2 * 1702176107) ^ 0x4BFA5010);
					continue;
				case 140u:
					num = ((int)num2 * -2041812519) ^ -1564005793;
					continue;
				case 115u:
					return "el";
				case 81u:
					goto IL_0446;
				case 16u:
					goto IL_0462;
				case 37u:
					goto IL_0483;
				case 121u:
					goto IL_04a4;
				case 11u:
					goto IL_04c5;
				case 79u:
					return "zh-Hant";
				case 55u:
					goto IL_04f6;
				case 38u:
					num = (int)(num2 * 1041737122) ^ -1194929735;
					continue;
				case 21u:
					num = ((int)num2 * -1450620592) ^ -1367199983;
					continue;
				case 39u:
				{
					int num30;
					int num31;
					if (num3 == 2471602315u)
					{
						num30 = 1140479179;
						num31 = num30;
					}
					else
					{
						num30 = 708575237;
						num31 = num30;
					}
					num = num30 ^ ((int)num2 * -827357622);
					continue;
				}
				case 63u:
					num = (int)(num2 * 1347711425) ^ -706079954;
					continue;
				case 130u:
					num = (int)(num2 * 1783694821) ^ -210905129;
					continue;
				case 74u:
					num = (int)(num2 * 519271068) ^ -1896013487;
					continue;
				case 54u:
					return "fi";
				case 77u:
				{
					int num20;
					int num21;
					if (num3 != 380651494)
					{
						num20 = -768321336;
						num21 = num20;
					}
					else
					{
						num20 = -1018636199;
						num21 = num20;
					}
					num = num20 ^ (int)(num2 * 1547223879);
					continue;
				}
				case 43u:
					num = ((int)num2 * -3311739) ^ 0x3A6FB5FC;
					continue;
				case 65u:
					return "pl";
				case 145u:
					goto IL_05eb;
				case 9u:
				{
					int num6;
					int num7;
					if (text2 == null)
					{
						num6 = 339257708;
						num7 = num6;
					}
					else
					{
						num6 = 979720236;
						num7 = num6;
					}
					num = num6 ^ ((int)num2 * -1727072187);
					continue;
				}
				case 64u:
					return "cs";
				case 122u:
					goto IL_0631;
				case 72u:
					return "nl";
				case 29u:
					num = (int)((num2 * 166615705) ^ 0x2AA2A76A);
					continue;
				case 127u:
					goto IL_066f;
				case 25u:
					return "ro";
				case 34u:
				{
					int num56;
					int num57;
					if (num3 != 1580935484)
					{
						num56 = 1133742098;
						num57 = num56;
					}
					else
					{
						num56 = 1900504217;
						num57 = num56;
					}
					num = num56 ^ (int)(num2 * 1815798013);
					continue;
				}
				case 61u:
				{
					int num52;
					int num53;
					if (num3 == 1262725376)
					{
						num52 = 1265644676;
						num53 = num52;
					}
					else
					{
						num52 = 1006221924;
						num53 = num52;
					}
					num = num52 ^ (int)(num2 * 391945126);
					continue;
				}
				case 112u:
					num = (int)((num2 * 278150532) ^ 0x7D603321);
					continue;
				case 101u:
					goto IL_06fa;
				case 134u:
					goto IL_0716;
				case 116u:
					num = (int)((num2 * 1828658872) ^ 0x31F9811);
					continue;
				case 96u:
					return "ko";
				case 46u:
				{
					int num44;
					int num45;
					if (num3 <= 2798875500u)
					{
						num44 = 859479736;
						num45 = num44;
					}
					else
					{
						num44 = 298118114;
						num45 = num44;
					}
					num = num44 ^ ((int)num2 * -1269459925);
					continue;
				}
				case 59u:
					return "no";
				case 131u:
					goto IL_0788;
				case 91u:
					num = ((int)num2 * -1250492551) ^ -1025297746;
					continue;
				case 90u:
				{
					int num36;
					int num37;
					if (num3 != 3405445907u)
					{
						num36 = -801181906;
						num37 = num36;
					}
					else
					{
						num36 = -1309384284;
						num37 = num36;
					}
					num = num36 ^ ((int)num2 * -183768502);
					continue;
				}
				case 15u:
					num = ((int)num2 * -1946013029) ^ -1288140382;
					continue;
				case 51u:
				{
					int num32;
					int num33;
					if (num3 == 3759690811u)
					{
						num32 = -963321568;
						num33 = num32;
					}
					else
					{
						num32 = -813634045;
						num33 = num32;
					}
					num = num32 ^ ((int)num2 * -1718844869);
					continue;
				}
				case 68u:
					goto IL_0815;
				case 123u:
				{
					int num24;
					int num25;
					if (num3 != 316123288)
					{
						num24 = -496221005;
						num25 = num24;
					}
					else
					{
						num24 = -1371250523;
						num25 = num24;
					}
					num = num24 ^ (int)(num2 * 645178440);
					continue;
				}
				case 60u:
					return "pt-BR";
				case 89u:
					num = ((int)num2 * -559629313) ^ 0xAC05D08;
					continue;
				case 138u:
					num = (int)(num2 * 775327092) ^ -1767161575;
					continue;
				case 104u:
					goto IL_088e;
				case 107u:
					goto IL_08aa;
				case 6u:
					goto IL_08cb;
				case 40u:
				{
					int num14;
					int num15;
					if (num3 == 3264533134u)
					{
						num14 = 636253323;
						num15 = num14;
					}
					else
					{
						num14 = 1613302455;
						num15 = num14;
					}
					num = num14 ^ ((int)num2 * -310196647);
					continue;
				}
				case 10u:
				{
					int num10;
					int num11;
					if (num3 == 319214730)
					{
						num10 = -55588538;
						num11 = num10;
					}
					else
					{
						num10 = -383915060;
						num11 = num10;
					}
					num = num10 ^ ((int)num2 * -391004615);
					continue;
				}
				case 49u:
				{
					int num62;
					int num63;
					if (num3 == 1901528810)
					{
						num62 = 1503580014;
						num63 = num62;
					}
					else
					{
						num62 = 1026541985;
						num63 = num62;
					}
					num = num62 ^ ((int)num2 * -1944940312);
					continue;
				}
				case 75u:
					return "th";
				case 97u:
					num = (int)((num2 * 1134311769) ^ 0x54EDF7C6);
					continue;
				case 100u:
					goto IL_0975;
				case 36u:
					goto IL_0996;
				case 26u:
					goto IL_09b7;
				case 110u:
					goto IL_09d8;
				case 14u:
					return "ar";
				case 0u:
					num = ((int)num2 * -320678636) ^ 0x3E2C5C11;
					continue;
				case 120u:
					goto IL_0a16;
				case 31u:
					num = (int)(num2 * 605272696) ^ -2065878599;
					continue;
				case 52u:
					goto IL_0a49;
				case 80u:
				{
					int num60;
					int num61;
					if (num3 <= 1901528810)
					{
						num60 = 1855943578;
						num61 = num60;
					}
					else
					{
						num60 = 617613391;
						num61 = num60;
					}
					num = num60 ^ ((int)num2 * -772942087);
					continue;
				}
				case 23u:
					return "es-419";
				case 103u:
				{
					int num58;
					int num59;
					if (num3 != 683056061)
					{
						num58 = -685984551;
						num59 = num58;
					}
					else
					{
						num58 = -312906129;
						num59 = num58;
					}
					num = num58 ^ (int)(num2 * 130035336);
					continue;
				}
				case 13u:
				{
					int num54;
					int num55;
					if (num3 == 3719199419u)
					{
						num54 = -673659252;
						num55 = num54;
					}
					else
					{
						num54 = -1997167386;
						num55 = num54;
					}
					num = num54 ^ ((int)num2 * -1084838962);
					continue;
				}
				case 125u:
					num = ((int)num2 * -1331842306) ^ 0x65E61527;
					continue;
				case 7u:
					return "ja";
				case 67u:
					goto IL_0b08;
				case 129u:
					return "es";
				case 83u:
					num = ((int)num2 * -540828068) ^ -1263134523;
					continue;
				case 132u:
					return "uk";
				case 32u:
				{
					int num50;
					int num51;
					if (num3 > 319214730)
					{
						num50 = -9091151;
						num51 = num50;
					}
					else
					{
						num50 = -1442803506;
						num51 = num50;
					}
					num = num50 ^ ((int)num2 * -891896235);
					continue;
				}
				case 62u:
					num = ((int)num2 * -1591711260) ^ -770698311;
					continue;
				case 57u:
				{
					int num48;
					int num49;
					if (num3 == 599131013)
					{
						num48 = -845857685;
						num49 = num48;
					}
					else
					{
						num48 = -2036494206;
						num49 = num48;
					}
					num = num48 ^ (int)(num2 * 460539377);
					continue;
				}
				case 93u:
				{
					int num46;
					int num47;
					if (num3 != 308944030)
					{
						num46 = -483696653;
						num47 = num46;
					}
					else
					{
						num46 = -650944767;
						num47 = num46;
					}
					num = num46 ^ ((int)num2 * -2015264087);
					continue;
				}
				case 118u:
					num = ((int)num2 * -544529177) ^ 0x5B8E5891;
					continue;
				case 22u:
				{
					int num42;
					int num43;
					if (num3 <= 3405445907u)
					{
						num42 = -1554960761;
						num43 = num42;
					}
					else
					{
						num42 = -1024483058;
						num43 = num42;
					}
					num = num42 ^ (int)(num2 * 1048528745);
					continue;
				}
				case 19u:
					goto IL_0c0f;
				case 124u:
					num = ((int)num2 * -472273593) ^ -930594383;
					continue;
				case 92u:
					num = (int)(num2 * 1749163364) ^ -691147695;
					continue;
				case 44u:
				{
					int num38;
					int num39;
					if (num3 == 497316822)
					{
						num38 = -632854069;
						num39 = num38;
					}
					else
					{
						num38 = -837384283;
						num39 = num38;
					}
					num = num38 ^ (int)(num2 * 1264693063);
					continue;
				}
				case 56u:
					num = ((int)num2 * -967787382) ^ -341101711;
					continue;
				case 114u:
				{
					int num34;
					int num35;
					if (num3 == 4263372803u)
					{
						num34 = -1910183341;
						num35 = num34;
					}
					else
					{
						num34 = -1720196573;
						num35 = num34;
					}
					num = num34 ^ ((int)num2 * -982374070);
					continue;
				}
				case 95u:
					num = (int)(num2 * 1698522165) ^ -481347784;
					continue;
				case 82u:
				{
					int num28;
					int num29;
					if (num3 != 3180870988u)
					{
						num28 = 1191846380;
						num29 = num28;
					}
					else
					{
						num28 = 653046521;
						num29 = num28;
					}
					num = num28 ^ (int)(num2 * 1526101864);
					continue;
				}
				case 58u:
				{
					int num26;
					int num27;
					if (num3 != 693158059)
					{
						num26 = -461523610;
						num27 = num26;
					}
					else
					{
						num26 = -916995286;
						num27 = num26;
					}
					num = num26 ^ ((int)num2 * -739809176);
					continue;
				}
				case 94u:
					return "en";
				case 76u:
					num = ((int)num2 * -738727069) ^ -676868981;
					continue;
				case 42u:
					num = ((int)num2 * -509930716) ^ -564644719;
					continue;
				case 8u:
					return "bg";
				case 126u:
					goto IL_0d4c;
				case 106u:
					num = ((int)num2 * -1353343038) ^ 0x6C2E416D;
					continue;
				case 98u:
					num = ((int)num2 * -1104540751) ^ -2008281361;
					continue;
				case 102u:
					goto IL_0d91;
				case 139u:
					return "fr";
				case 86u:
					goto IL_0dc2;
				case 135u:
					goto IL_0de3;
				case 133u:
					return "tr";
				case 71u:
					num = ((int)num2 * -1050402228) ^ -686421891;
					continue;
				case 50u:
					num = ((int)num2 * -659882516) ^ -1391535679;
					continue;
				case 143u:
				{
					int num22;
					int num23;
					if (num3 != 3229236340u)
					{
						num22 = -1295641885;
						num23 = num22;
					}
					else
					{
						num22 = -1239771997;
						num23 = num22;
					}
					num = num22 ^ (int)(num2 * 1220289914);
					continue;
				}
				case 141u:
					goto IL_0e5c;
				case 109u:
				{
					int num18;
					int num19;
					if (num3 <= 599131013)
					{
						num18 = 1547704054;
						num19 = num18;
					}
					else
					{
						num18 = 1017701274;
						num19 = num18;
					}
					num = num18 ^ ((int)num2 * -1294068791);
					continue;
				}
				case 3u:
					num = (int)((num2 * 1941354249) ^ 0x45B75D56);
					continue;
				case 85u:
					num = ((int)num2 * -1875511132) ^ -2119983043;
					continue;
				case 48u:
					goto IL_0ec5;
				case 47u:
					num = (int)((num2 * 1553341802) ^ 0x5B3B0F67);
					continue;
				case 20u:
					goto IL_0ef8;
				case 30u:
					num = ((int)num2 * -1412671745) ^ 0x2D5A93B;
					continue;
				case 69u:
					num = ((int)num2 * -627088021) ^ 0x65F73E80;
					continue;
				case 66u:
					goto IL_0f3d;
				case 5u:
					goto IL_0f5e;
				case 33u:
					return "sv";
				case 105u:
					num = ((int)num2 * -849282154) ^ -1338501201;
					continue;
				case 144u:
				{
					int num12;
					int num13;
					if (num3 != 2499415067u)
					{
						num12 = 52126146;
						num13 = num12;
					}
					else
					{
						num12 = 1930797409;
						num13 = num12;
					}
					num = num12 ^ ((int)num2 * -922012320);
					continue;
				}
				case 17u:
					goto IL_0fc0;
				case 41u:
				{
					int num8;
					int num9;
					if (num3 == 1544226106)
					{
						num8 = 1388693211;
						num9 = num8;
					}
					else
					{
						num8 = 1424324976;
						num9 = num8;
					}
					num = num8 ^ ((int)num2 * -1819396509);
					continue;
				}
				case 84u:
					num3 = DdYChCCNFlcqCGxqXSyxjtYmxFnRA.VnddVlepyRJSegDTusdnFINPwipbA(text2);
					num = ((int)num2 * -887074402) ^ -586157839;
					continue;
				case 113u:
					goto IL_101e;
				case 142u:
					return "de";
				case 24u:
					return "ru";
				case 18u:
				{
					int num4;
					int num5;
					if (num3 == 3739448251u)
					{
						num4 = -544457966;
						num5 = num4;
					}
					else
					{
						num4 = -1029536862;
						num5 = num4;
					}
					num = num4 ^ ((int)num2 * -125184311);
					continue;
				}
				case 35u:
					goto IL_1083;
				default:
					throw new NotImplementedException($"Steam API returned unexpected language \"{text2}\".");
				}
				break;
				IL_1083:
				int num64;
				if (text2 == "norwegian")
				{
					num = 899445742;
					num64 = num;
				}
				else
				{
					num = 552197963;
					num64 = num;
				}
				continue;
				IL_030d:
				int num65;
				if (num3 != 3210859552u)
				{
					num = 57030938;
					num65 = num;
				}
				else
				{
					num = 1546999116;
					num65 = num;
				}
				continue;
				IL_0a49:
				int num66;
				if (!(text2 == "english"))
				{
					num = 415437524;
					num66 = num;
				}
				else
				{
					num = 313980243;
					num66 = num;
				}
				continue;
				IL_101e:
				int num67;
				if (text2 == "russian")
				{
					num = 324895381;
					num67 = num;
				}
				else
				{
					num = 1416573254;
					num67 = num;
				}
				continue;
				IL_0815:
				int num68;
				if (!(text2 == "bulgarian"))
				{
					num = 1882407496;
					num68 = num;
				}
				else
				{
					num = 2077074075;
					num68 = num;
				}
				continue;
				IL_04f6:
				int num69;
				if (text2 == "hungarian")
				{
					num = 1526385088;
					num69 = num;
				}
				else
				{
					num = 1660694286;
					num69 = num;
				}
				continue;
				IL_0fc0:
				int num70;
				if (text2 == "japanese")
				{
					num = 1358764898;
					num70 = num;
				}
				else
				{
					num = 1897241089;
					num70 = num;
				}
				continue;
				IL_0a16:
				int num71;
				if (text2 == "greek")
				{
					num = 2125808798;
					num71 = num;
				}
				else
				{
					num = 1514103664;
					num71 = num;
				}
				continue;
				IL_0462:
				int num72;
				if (text2 == "italian")
				{
					num = 1690067485;
					num72 = num;
				}
				else
				{
					num = 2085151312;
					num72 = num;
				}
				continue;
				IL_0f5e:
				int num73;
				if (num3 == 1703858441)
				{
					num = 936470793;
					num73 = num;
				}
				else
				{
					num = 2106968604;
					num73 = num;
				}
				continue;
				IL_0788:
				int num74;
				if (!(text2 == "vietnamese"))
				{
					num = 2113689231;
					num74 = num;
				}
				else
				{
					num = 51499202;
					num74 = num;
				}
				continue;
				IL_09d8:
				int num75;
				if (num3 <= 1580935484)
				{
					num = 1789428980;
					num75 = num;
				}
				else
				{
					num = 1739260758;
					num75 = num;
				}
				continue;
				IL_0f3d:
				int num76;
				if (!(text2 == "polish"))
				{
					num = 1513612275;
					num76 = num;
				}
				else
				{
					num = 1560906480;
					num76 = num;
				}
				continue;
				IL_0291:
				int num77;
				if (num3 > 1262725376)
				{
					num = 1771192695;
					num77 = num;
				}
				else
				{
					num = 1194728702;
					num77 = num;
				}
				continue;
				IL_04c5:
				int num78;
				if (!(text2 == "latam"))
				{
					num = 1468450441;
					num78 = num;
				}
				else
				{
					num = 2051717414;
					num78 = num;
				}
				continue;
				IL_0ef8:
				int num79;
				if (!(text2 == "thai"))
				{
					num = 2101870711;
					num79 = num;
				}
				else
				{
					num = 1642203324;
					num79 = num;
				}
				continue;
				IL_09b7:
				int num80;
				if (text2 == "schinese")
				{
					num = 21582330;
					num80 = num;
				}
				else
				{
					num = 649299139;
					num80 = num;
				}
				continue;
				IL_0716:
				int num81;
				if (num3 == 505713757)
				{
					num = 2005136374;
					num81 = num;
				}
				else
				{
					num = 1026926186;
					num81 = num;
				}
				continue;
				IL_0ec5:
				int num82;
				if (!(text2 == "danish"))
				{
					num = 243585081;
					num82 = num;
				}
				else
				{
					num = 1481586931;
					num82 = num;
				}
				continue;
				IL_0381:
				int num83;
				if (!(text2 == "portuguese"))
				{
					num = 821763254;
					num83 = num;
				}
				else
				{
					num = 1844782785;
					num83 = num;
				}
				continue;
				IL_0996:
				int num84;
				if (text2 == "finnish")
				{
					num = 1590106691;
					num84 = num;
				}
				else
				{
					num = 1335418850;
					num84 = num;
				}
				continue;
				IL_0e5c:
				int num85;
				if (text2 == "romanian")
				{
					num = 838188264;
					num85 = num;
				}
				else
				{
					num = 968546100;
					num85 = num;
				}
				continue;
				IL_0446:
				int num86;
				if (num3 == 3426057626u)
				{
					num = 104583062;
					num86 = num;
				}
				else
				{
					num = 1429575526;
					num86 = num;
				}
				continue;
				IL_06fa:
				int num87;
				if (num3 <= 3719199419u)
				{
					num = 957850793;
					num87 = num;
				}
				else
				{
					num = 1441376323;
					num87 = num;
				}
				continue;
				IL_0de3:
				int num88;
				if (text2 == "french")
				{
					num = 1035095062;
					num88 = num;
				}
				else
				{
					num = 183855224;
					num88 = num;
				}
				continue;
				IL_0975:
				int num89;
				if (!(text2 == "czech"))
				{
					num = 993245510;
					num89 = num;
				}
				else
				{
					num = 711698905;
					num89 = num;
				}
				continue;
				IL_04a4:
				int num90;
				if (!(text2 == "koreana"))
				{
					num = 1492703118;
					num90 = num;
				}
				else
				{
					num = 1263992569;
					num90 = num;
				}
				continue;
				IL_0dc2:
				int num91;
				if (!(text2 == "arabic"))
				{
					num = 1704362394;
					num91 = num;
				}
				else
				{
					num = 572826471;
					num91 = num;
				}
				continue;
				IL_0270:
				int num92;
				if (!(text2 == "turkish"))
				{
					num = 671240555;
					num92 = num;
				}
				else
				{
					num = 860796848;
					num92 = num;
				}
				continue;
				IL_08cb:
				int num93;
				if (num3 <= 497316822)
				{
					num = 1535643792;
					num93 = num;
				}
				else
				{
					num = 125039621;
					num93 = num;
				}
				continue;
				IL_0d91:
				int num94;
				if (!(text2 == "ukrainian"))
				{
					num = 2107277081;
					num94 = num;
				}
				else
				{
					num = 753974029;
					num94 = num;
				}
				continue;
				IL_066f:
				int num95;
				if (!(text2 == "spanish"))
				{
					num = 458781305;
					num95 = num;
				}
				else
				{
					num = 1748880894;
					num95 = num;
				}
				continue;
				IL_02df:
				int num96;
				if (num3 <= 3180870988u)
				{
					num = 2044197449;
					num96 = num;
				}
				else
				{
					num = 1760063561;
					num96 = num;
				}
				continue;
				IL_0d4c:
				int num97;
				if (text2 == "tchinese")
				{
					num = 1919885702;
					num97 = num;
				}
				else
				{
					num = 187040537;
					num97 = num;
				}
				continue;
				IL_08aa:
				int num98;
				if (!(text2 == "swedish"))
				{
					num = 595467085;
					num98 = num;
				}
				else
				{
					num = 1027848754;
					num98 = num;
				}
				continue;
				IL_0483:
				int num99;
				if (!(text2 == "german"))
				{
					num = 1581603281;
					num99 = num;
				}
				else
				{
					num = 322631679;
					num99 = num;
				}
				continue;
				IL_0c0f:
				int num100;
				if (!(text2 == "dutch"))
				{
					num = 1525456864;
					num100 = num;
				}
				else
				{
					num = 2142586931;
					num100 = num;
				}
				continue;
				IL_0631:
				int num101;
				if (num3 != 4151292721u)
				{
					num = 1651169039;
					num101 = num;
				}
				else
				{
					num = 387061300;
					num101 = num;
				}
				continue;
				IL_088e:
				int num102;
				if (num3 > 3759690811u)
				{
					num = 531150267;
					num102 = num;
				}
				else
				{
					num = 365084465;
					num102 = num;
				}
				continue;
				IL_0b08:
				int num103;
				if (text2 == "brazilian")
				{
					num = 1764835307;
					num103 = num;
				}
				else
				{
					num = 341711600;
					num103 = num;
				}
				continue;
				IL_03e6:
				int num104;
				if (num3 > 3229236340u)
				{
					num = 1780428684;
					num104 = num;
				}
				else
				{
					num = 1696303161;
					num104 = num;
				}
			}
			goto IL_000b;
			IL_05eb:
			text = SteamApps.GetCurrentGameLanguage();
			goto IL_05f7;
		}
	}
}
