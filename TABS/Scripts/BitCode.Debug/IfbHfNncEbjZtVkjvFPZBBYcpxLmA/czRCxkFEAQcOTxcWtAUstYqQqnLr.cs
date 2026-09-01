using System.Collections.Generic;
using BitCode.Debug;
using JetBrains.Annotations;

namespace IfbHfNncEbjZtVkjvFPZBBYcpxLmA
{
	internal static class czRCxkFEAQcOTxcWtAUstYqQqnLr
	{
		private static readonly Dictionary<char, char> QoNdPzKdFbDhqGkdcOcXaFelnUNO = new Dictionary<char, char>
		{
			{ ')', '(' },
			{ '}', '{' },
			{ ']', '[' }
		};

		internal static List<string> LeFUloyjiVvyikIvaNNflmcQgPpk([NotNull] string P_0, bool P_1 = false)
		{
			List<string> list = new List<string>();
			int num = 0;
			int num2 = 0;
			Stack<char> stack = new Stack<char>();
			bool flag = false;
			bool flag2 = default(bool);
			string text = default(string);
			int num14 = default(int);
			char c = default(char);
			string text2 = default(string);
			while (true)
			{
				int num3 = -136501692;
				while (true)
				{
					uint num4;
					int num5;
					int num12;
					int num52;
					switch ((num4 = (uint)(num3 ^ -729912500)) % 71)
					{
					case 30u:
						break;
					case 9u:
					{
						int num55;
						int num56;
						if (flag2)
						{
							num55 = 205261067;
							num56 = num55;
						}
						else
						{
							num55 = 1984817243;
							num56 = num55;
						}
						num3 = num55 ^ ((int)num4 * -100006203);
						continue;
					}
					case 39u:
						list.Add(P_0.Substring(num, num2 - num));
						num3 = ((int)num4 * -842835875) ^ 0x7716CBEB;
						continue;
					case 62u:
						text = P_0.Substring(num, num14 + 1 - num);
						num3 = -58568293;
						continue;
					case 18u:
					{
						stack.Pop();
						int num51;
						if (stack.Count == 0)
						{
							num3 = -1408636929;
							num51 = num3;
						}
						else
						{
							num3 = -1674581841;
							num51 = num3;
						}
						continue;
					}
					case 53u:
						num14 = num2;
						num3 = -1674581841;
						continue;
					case 13u:
					{
						int num32;
						int num33;
						if (c != '[')
						{
							num32 = 617224982;
							num33 = num32;
						}
						else
						{
							num32 = 1576626505;
							num33 = num32;
						}
						num3 = num32 ^ (int)(num4 * 1544981893);
						continue;
					}
					case 8u:
					{
						int num20;
						int num21;
						if (c != ']')
						{
							num20 = -83053248;
							num21 = num20;
						}
						else
						{
							num20 = -1119156183;
							num21 = num20;
						}
						num3 = num20 ^ (int)(num4 * 63041282);
						continue;
					}
					case 17u:
						flag2 = true;
						num3 = ((int)num4 * -962853229) ^ -2061647226;
						continue;
					case 59u:
					{
						int num42;
						if (!flag)
						{
							num3 = -2122148188;
							num42 = num3;
						}
						else
						{
							num3 = -1674581841;
							num42 = num3;
						}
						continue;
					}
					case 65u:
						num3 = (int)(num4 * 1418780696) ^ -2112376794;
						continue;
					case 20u:
					{
						int num34;
						int num35;
						if (stack.Count != 0)
						{
							num34 = 1742140877;
							num35 = num34;
						}
						else
						{
							num34 = 655436203;
							num35 = num34;
						}
						num3 = num34 ^ (int)(num4 * 253826319);
						continue;
					}
					case 70u:
					{
						int num23;
						if (flag2)
						{
							num3 = -1610383444;
							num23 = num3;
						}
						else
						{
							num3 = -53139220;
							num23 = num3;
						}
						continue;
					}
					case 46u:
						list.Add(text);
						num3 = -548847855;
						continue;
					case 40u:
					{
						int num48;
						int num49;
						if (stack.Count == 0)
						{
							num48 = 101723448;
							num49 = num48;
						}
						else
						{
							num48 = 831999011;
							num49 = num48;
						}
						num3 = num48 ^ ((int)num4 * -289673747);
						continue;
					}
					case 58u:
					{
						int num43;
						int num44;
						if (stack.Count == 0)
						{
							num43 = -628008775;
							num44 = num43;
						}
						else
						{
							num43 = -741531894;
							num44 = num43;
						}
						num3 = num43 ^ ((int)num4 * -1207771714);
						continue;
					}
					case 64u:
						list.Add(string.Empty);
						num3 = ((int)num4 * -1243584249) ^ 0x58246601;
						continue;
					case 7u:
					{
						int num28;
						int num29;
						if (stack.Peek() == c)
						{
							num28 = -727606907;
							num29 = num28;
						}
						else
						{
							num28 = -1053976270;
							num29 = num28;
						}
						num3 = num28 ^ ((int)num4 * -1623563481);
						continue;
					}
					case 15u:
					{
						int num10;
						int num11;
						if ((uint)c <= 44u)
						{
							num10 = -768551572;
							num11 = num10;
						}
						else
						{
							num10 = -230136495;
							num11 = num10;
						}
						num3 = num10 ^ (int)(num4 * 1012325976);
						continue;
					}
					case 16u:
						flag2 = false;
						num3 = ((int)num4 * -1206866076) ^ -787518256;
						continue;
					case 60u:
					{
						int num53;
						int num54;
						if (stack.Peek() == QoNdPzKdFbDhqGkdcOcXaFelnUNO[c])
						{
							num53 = -631229496;
							num54 = num53;
						}
						else
						{
							num53 = -1124121485;
							num54 = num53;
						}
						num3 = num53 ^ (int)(num4 * 110159591);
						continue;
					}
					case 25u:
					{
						int num46;
						int num47;
						if (c != '}')
						{
							num46 = 1768863939;
							num47 = num46;
						}
						else
						{
							num46 = 724274646;
							num47 = num46;
						}
						num3 = num46 ^ (int)(num4 * 545288235);
						continue;
					}
					case 14u:
					{
						int num39;
						int num40;
						if (P_1)
						{
							num39 = -537131454;
							num40 = num39;
						}
						else
						{
							num39 = -1422262721;
							num40 = num39;
						}
						num3 = num39 ^ (int)(num4 * 2125899338);
						continue;
					}
					case 42u:
						list.Add(text2);
						num3 = -46514100;
						continue;
					case 47u:
						c = P_0[num2];
						num3 = -674592526;
						continue;
					case 33u:
						num3 = ((int)num4 * -370249402) ^ -2032671277;
						continue;
					case 3u:
					{
						int num26;
						int num27;
						if (flag2)
						{
							num26 = -1581270172;
							num27 = num26;
						}
						else
						{
							num26 = -1400751481;
							num27 = num26;
						}
						num3 = num26 ^ (int)(num4 * 1069397953);
						continue;
					}
					case 44u:
					{
						int num22;
						if ((uint)c > 93u)
						{
							num3 = -2020713999;
							num22 = num3;
						}
						else
						{
							num3 = -1885134697;
							num22 = num3;
						}
						continue;
					}
					case 32u:
					{
						int num16;
						int num17;
						if (c != ' ')
						{
							num16 = 1325096928;
							num17 = num16;
						}
						else
						{
							num16 = 1565941337;
							num17 = num16;
						}
						num3 = num16 ^ ((int)num4 * -1993325793);
						continue;
					}
					case 36u:
						flag = true;
						num3 = ((int)num4 * -1917711707) ^ 0x4CB1E219;
						continue;
					case 29u:
						flag2 = false;
						num3 = -953698347;
						continue;
					case 50u:
						goto IL_04a2;
					case 28u:
					{
						int num6;
						if (stack.Count != 0)
						{
							num3 = -310223662;
							num6 = num3;
						}
						else
						{
							num3 = -1896589798;
							num6 = num3;
						}
						continue;
					}
					case 21u:
						num3 = ((int)num4 * -1785548591) ^ 0x4680BBF;
						continue;
					case 35u:
						flag2 = false;
						num3 = -1674581841;
						continue;
					case 69u:
						flag = false;
						stack.Pop();
						text2 = P_0.Substring(num, num2 - num);
						num3 = ((int)num4 * -200351426) ^ 0x4D320898;
						continue;
					case 52u:
						flag2 = true;
						num3 = ((int)num4 * -1460187486) ^ -1523069713;
						continue;
					case 0u:
						goto IL_0535;
					case 45u:
					{
						int num50;
						if (flag2)
						{
							num3 = -863501736;
							num50 = num3;
						}
						else
						{
							num3 = -396789705;
							num50 = num3;
						}
						continue;
					}
					case 56u:
					{
						int num45;
						if (!flag)
						{
							num3 = -1674581841;
							num45 = num3;
						}
						else
						{
							num3 = -42205672;
							num45 = num3;
						}
						continue;
					}
					case 12u:
						stack.Push(c);
						num3 = ((int)num4 * -1377163980) ^ -752139145;
						continue;
					case 19u:
					{
						int num41;
						if (stack.Count == 0)
						{
							num3 = -75972584;
							num41 = num3;
						}
						else
						{
							num3 = -487734134;
							num41 = num3;
						}
						continue;
					}
					case 6u:
						throw new TokenizationException("Mismatched brackets in expression \"" + P_0 + "\"");
					case 34u:
					{
						int num37;
						int num38;
						if (P_1)
						{
							num37 = 1402806553;
							num38 = num37;
						}
						else
						{
							num37 = 416069005;
							num38 = num37;
						}
						num3 = num37 ^ (int)(num4 * 135727065);
						continue;
					}
					case 66u:
					{
						int num36;
						if (num2 >= P_0.Length)
						{
							num3 = -699812644;
							num36 = num3;
						}
						else
						{
							num3 = -1581817040;
							num36 = num3;
						}
						continue;
					}
					case 4u:
					{
						int num30;
						int num31;
						if (c == '\t')
						{
							num30 = -1244393215;
							num31 = num30;
						}
						else
						{
							num30 = -1248913388;
							num31 = num30;
						}
						num3 = num30 ^ ((int)num4 * -77043082);
						continue;
					}
					case 68u:
						throw new TokenizationException("Unclosed brackets or quotes in expression \"" + P_0 + "\"");
					case 11u:
						num14 = 0;
						num3 = (int)((num4 * 337169464) ^ 0x3C4B4220);
						continue;
					case 48u:
						num3 = (int)((num4 * 490971871) ^ 0x55C513E8);
						continue;
					case 22u:
						num14 = num2;
						num3 = -681683691;
						continue;
					case 67u:
						num = num2;
						num3 = (int)((num4 * 1537723354) ^ 0x502445E2);
						continue;
					case 49u:
						list.Add(P_0.Substring(num, num2 - num));
						flag2 = false;
						num3 = (int)((num4 * 226681103) ^ 0x777F4FCA);
						continue;
					case 41u:
					{
						int num24;
						int num25;
						if (stack.Count != 0)
						{
							num24 = 1880914469;
							num25 = num24;
						}
						else
						{
							num24 = 1686428412;
							num25 = num24;
						}
						num3 = num24 ^ (int)(num4 * 266511181);
						continue;
					}
					case 37u:
						num3 = (int)((num4 * 1716612566) ^ 0x74C4A2EA);
						continue;
					case 27u:
						num3 = (int)((num4 * 485857417) ^ 0x611F533A);
						continue;
					case 61u:
					{
						int num18;
						int num19;
						if ((uint)c <= 32u)
						{
							num18 = 1484330648;
							num19 = num18;
						}
						else
						{
							num18 = 1772354197;
							num19 = num18;
						}
						num3 = num18 ^ (int)(num4 * 773165599);
						continue;
					}
					case 23u:
					{
						int num15;
						if (c == '"')
						{
							num3 = -583629755;
							num15 = num3;
						}
						else
						{
							num3 = -1033222761;
							num15 = num3;
						}
						continue;
					}
					case 51u:
						num3 = (int)(num4 * 1484743104) ^ -1852148234;
						continue;
					case 31u:
						stack.Push(c);
						num3 = (int)(num4 * 479221187) ^ -214359458;
						continue;
					case 5u:
						flag2 = true;
						num = num2;
						num3 = ((int)num4 * -1225772246) ^ -1422416584;
						continue;
					case 55u:
						num2++;
						num3 = -1827603712;
						continue;
					case 24u:
						num14 = num2;
						num3 = (int)((num4 * 1382577074) ^ 0x4A3A59FF);
						continue;
					case 63u:
					{
						int num13;
						if (c != '{')
						{
							num3 = -656337789;
							num13 = num3;
						}
						else
						{
							num3 = -2006225266;
							num13 = num3;
						}
						continue;
					}
					case 10u:
						list.AddRange(LeFUloyjiVvyikIvaNNflmcQgPpk(text2));
						num3 = ((int)num4 * -361387806) ^ 0x5F1E6208;
						continue;
					case 26u:
					{
						int num8;
						int num9;
						if (!P_1)
						{
							num8 = 849973847;
							num9 = num8;
						}
						else
						{
							num8 = 1922581135;
							num9 = num8;
						}
						num3 = num8 ^ (int)(num4 * 1164925894);
						continue;
					}
					case 2u:
						list.AddRange(LeFUloyjiVvyikIvaNNflmcQgPpk(text));
						num3 = ((int)num4 * -98541917) ^ -1958899857;
						continue;
					case 38u:
						num = num2;
						num3 = ((int)num4 * -741709118) ^ 0x3113C6B3;
						continue;
					case 43u:
					{
						int num7;
						if (flag)
						{
							num3 = -1674581841;
							num7 = num3;
						}
						else
						{
							num3 = -1904630206;
							num7 = num3;
						}
						continue;
					}
					case 54u:
						switch (c)
						{
						case ')':
							break;
						case ',':
							goto IL_04a2;
						case '*':
						case '+':
							goto IL_0535;
						default:
							goto IL_0881;
						case '(':
							goto IL_0894;
						}
						goto case 59u;
					case 57u:
						goto IL_0894;
					default:
						{
							return list;
						}
						IL_0894:
						if (!P_1)
						{
							num3 = -570289498;
							num5 = num3;
						}
						else
						{
							num3 = -361833818;
							num5 = num3;
						}
						continue;
						IL_0881:
						num3 = (int)((num4 * 172305838) ^ 0x66F180B3);
						continue;
						IL_04a2:
						if (flag2)
						{
							num3 = -573482731;
							num12 = num3;
						}
						else
						{
							num3 = -1022839302;
							num12 = num3;
						}
						continue;
						IL_0535:
						num14 = num2;
						if (flag2)
						{
							num3 = -1674581841;
							num52 = num3;
						}
						else
						{
							num3 = -289315040;
							num52 = num3;
						}
						continue;
					}
					break;
				}
			}
		}
	}
}
