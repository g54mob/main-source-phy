using System;
using System.Runtime.CompilerServices;
using BitCode.Extensions;
using BitCode.Performance;
using JetBrains.Annotations;
using UnityEngine;

namespace BitCode.Debug.UI
{
	public static class DisplayablePerformanceCounter
	{
		private sealed class idgktDISShpcbFEBJueJFZAREJLe
		{
			public bool woSbilPJLQzHizvrmAJbMwpxaLC;

			public string aXvcnDaxkaaBziUBwlQscxeoRzzTA;

			public string mbjNeFkSdQzRbmrdjIJaVhGBVnZq;

			public ThresholdColoriser kPgkgemDbIhwVGUaBlaPeGWDKDdAb;

			internal string QQGVCtrujGBsyMkCaBfzIsEMyRbfA(IPerformanceCounter<double, double> P_0)
			{
				if (P_0.Count == 0)
				{
					goto IL_000b;
				}
				goto IL_00ca;
				IL_000b:
				int num = -1861774529;
				goto IL_0010;
				IL_0010:
				double current = default(double);
				double num5 = default(double);
				double average = default(double);
				double min = default(double);
				double max = default(double);
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -302660265)) % 9)
					{
					case 0u:
						break;
					case 2u:
					{
						double num6 = 1000.0 / P_0.Average;
						return $"{aXvcnDaxkaaBziUBwlQscxeoRzzTA}{current:f2} ms ({num5:f2}/s), avg {average:f2} ms ({num6:f2}/s), {min:f2}-{max:f2} ms{mbjNeFkSdQzRbmrdjIJaVhGBVnZq}";
					}
					case 5u:
						goto IL_00ca;
					case 8u:
						num5 = 1000.0 / P_0.Current;
						num = (int)(num2 * 1704718373) ^ -992767884;
						continue;
					case 4u:
						min = P_0.Min;
						num = (int)((num2 * 931889404) ^ 0x69608524);
						continue;
					case 3u:
						average = P_0.Average;
						num = ((int)num2 * -503551060) ^ -1519686868;
						continue;
					case 6u:
					{
						max = P_0.Max;
						int num3;
						int num4;
						if (woSbilPJLQzHizvrmAJbMwpxaLC)
						{
							num3 = -1686498668;
							num4 = num3;
						}
						else
						{
							num3 = -1776495684;
							num4 = num3;
						}
						num = num3 ^ ((int)num2 * -2019457603);
						continue;
					}
					case 7u:
						return "???";
					default:
						return $"{aXvcnDaxkaaBziUBwlQscxeoRzzTA}{current:f2} ms, avg {average:f2} ms, {min:f2}-{max:f2} ms{mbjNeFkSdQzRbmrdjIJaVhGBVnZq}";
					}
					break;
				}
				goto IL_000b;
				IL_00ca:
				current = P_0.Current;
				num = -373907697;
				goto IL_0010;
			}

			internal Color NQGKRZeTkbKSRhJBkgYmVlFqPpPK(IPerformanceCounter<double, double> P_0)
			{
				return kPgkgemDbIhwVGUaBlaPeGWDKDdAb?.GetColour(P_0.Average) ?? Color.white;
			}
		}

		private sealed class lACCIJUmQLfYuFIHtwCLnDEvtzPO
		{
			public bool woSbilPJLQzHizvrmAJbMwpxaLC;

			public string aXvcnDaxkaaBziUBwlQscxeoRzzTA;

			public string mbjNeFkSdQzRbmrdjIJaVhGBVnZq;

			public ThresholdColoriser kPgkgemDbIhwVGUaBlaPeGWDKDdAb;

			internal string QQGVCtrujGBsyMkCaBfzIsEMyRbfA(IPerformanceCounter<float, float> P_0)
			{
				if (P_0.Count == 0)
				{
					goto IL_000b;
				}
				goto IL_00c7;
				IL_000b:
				int num = -699359002;
				goto IL_0010;
				IL_0010:
				float num4 = default(float);
				float average = default(float);
				float min = default(float);
				float max = default(float);
				float num3 = default(float);
				float current = default(float);
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1918917689)) % 9)
					{
					case 2u:
						break;
					case 1u:
						return "???";
					case 0u:
						num4 = 1000f / P_0.Average;
						num = (int)((num2 * 209996908) ^ 0x76B4A338);
						continue;
					case 5u:
						average = P_0.Average;
						min = P_0.Min;
						max = P_0.Max;
						num = ((int)num2 * -1870129218) ^ -1357999107;
						continue;
					case 7u:
						num3 = 1000f / P_0.Current;
						num = ((int)num2 * -185839371) ^ 0x6EC1904D;
						continue;
					case 3u:
						goto IL_00c7;
					case 4u:
					{
						int num5;
						int num6;
						if (!woSbilPJLQzHizvrmAJbMwpxaLC)
						{
							num5 = -1736708032;
							num6 = num5;
						}
						else
						{
							num5 = -1025656837;
							num6 = num5;
						}
						num = num5 ^ ((int)num2 * -1476227878);
						continue;
					}
					case 6u:
						return $"{aXvcnDaxkaaBziUBwlQscxeoRzzTA}{current:f2} ms ({num3:f2}/s), avg {average:f2} ms ({num4:f2}/s), {min:f2}-{max:f2} ms{mbjNeFkSdQzRbmrdjIJaVhGBVnZq}";
					default:
						return $"{aXvcnDaxkaaBziUBwlQscxeoRzzTA}{current:f2} ms, avg {average:f2} ms, {min:f2}-{max:f2} ms{mbjNeFkSdQzRbmrdjIJaVhGBVnZq}";
					}
					break;
				}
				goto IL_000b;
				IL_00c7:
				current = P_0.Current;
				num = -1313417578;
				goto IL_0010;
			}

			internal Color NQGKRZeTkbKSRhJBkgYmVlFqPpPK(IPerformanceCounter<float, float> P_0)
			{
				return kPgkgemDbIhwVGUaBlaPeGWDKDdAb?.GetColour(P_0.Average) ?? Color.white;
			}
		}

		private sealed class uVKzDRtmmZmciCWtvLtdvQBHLkET
		{
			public string aXvcnDaxkaaBziUBwlQscxeoRzzTA;

			public string mbjNeFkSdQzRbmrdjIJaVhGBVnZq;

			public ThresholdColoriser kPgkgemDbIhwVGUaBlaPeGWDKDdAb;

			internal string NAHUQMQfGjlLAMVOTSTDhnuZjFrK(IPerformanceCounter<long, double> P_0)
			{
				if (P_0.Count == 0)
				{
					goto IL_0008;
				}
				goto IL_0063;
				IL_0008:
				int num = 321946677;
				goto IL_000d;
				IL_000d:
				string text3 = default(string);
				string text = default(string);
				string text2 = default(string);
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x44E7BB1B)) % 5)
					{
					case 3u:
						break;
					case 1u:
						return "???";
					case 0u:
						text3 = P_0.Max.MemoryQuantityToString();
						num = ((int)num2 * -1264978231) ^ 0x234ADF2F;
						continue;
					case 2u:
						goto IL_0063;
					default:
						return aXvcnDaxkaaBziUBwlQscxeoRzzTA + text + ", min " + text2 + ", max " + text3 + mbjNeFkSdQzRbmrdjIJaVhGBVnZq;
					}
					break;
				}
				goto IL_0008;
				IL_0063:
				text = P_0.Current.MemoryQuantityToString();
				text2 = P_0.Min.MemoryQuantityToString();
				num = 972667053;
				goto IL_000d;
			}

			internal Color VjAdjCKNUqwdZClForBfoNnErBYi(IPerformanceCounter<long, double> P_0)
			{
				return kPgkgemDbIhwVGUaBlaPeGWDKDdAb?.GetColour(P_0.Average) ?? Color.white;
			}
		}

		private const string bFiENPDWiDuDtTFZHFEPDknnoARU = "???";

		public static DisplayablePerformanceCounter<double, double> ForMillisecondMetric(IPerformanceCounter<double, double> millisecondCounter, [CanBeNull] ThresholdColoriser coloriser = null, string prefix = null, string suffix = null, bool includePerSecond = true)
		{
			idgktDISShpcbFEBJueJFZAREJLe idgktDISShpcbFEBJueJFZAREJLe2 = new idgktDISShpcbFEBJueJFZAREJLe();
			idgktDISShpcbFEBJueJFZAREJLe2.woSbilPJLQzHizvrmAJbMwpxaLC = includePerSecond;
			idgktDISShpcbFEBJueJFZAREJLe2.aXvcnDaxkaaBziUBwlQscxeoRzzTA = prefix;
			while (true)
			{
				int num = -1567426985;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1838767999)) % 3)
					{
					case 2u:
						break;
					case 1u:
						goto IL_0037;
					default:
						idgktDISShpcbFEBJueJFZAREJLe2.kPgkgemDbIhwVGUaBlaPeGWDKDdAb = coloriser;
						return new DisplayablePerformanceCounter<double, double>(millisecondCounter, idgktDISShpcbFEBJueJFZAREJLe2.QQGVCtrujGBsyMkCaBfzIsEMyRbfA, idgktDISShpcbFEBJueJFZAREJLe2.NQGKRZeTkbKSRhJBkgYmVlFqPpPK);
					}
					break;
					IL_0037:
					idgktDISShpcbFEBJueJFZAREJLe2.mbjNeFkSdQzRbmrdjIJaVhGBVnZq = suffix;
					num = ((int)num2 * -932267600) ^ 0x756A84C3;
				}
			}
		}

		public static DisplayablePerformanceCounter<float, float> ForMillisecondMetric(IPerformanceCounter<float, float> millisecondCounter, [CanBeNull] ThresholdColoriser coloriser = null, string prefix = null, string suffix = null, bool includePerSecond = true)
		{
			lACCIJUmQLfYuFIHtwCLnDEvtzPO lACCIJUmQLfYuFIHtwCLnDEvtzPO2 = new lACCIJUmQLfYuFIHtwCLnDEvtzPO();
			while (true)
			{
				int num = -760547224;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -2080702587)) % 4)
					{
					case 0u:
						break;
					case 1u:
						lACCIJUmQLfYuFIHtwCLnDEvtzPO2.woSbilPJLQzHizvrmAJbMwpxaLC = includePerSecond;
						num = ((int)num2 * -745159906) ^ -1749128464;
						continue;
					case 3u:
						lACCIJUmQLfYuFIHtwCLnDEvtzPO2.aXvcnDaxkaaBziUBwlQscxeoRzzTA = prefix;
						num = (int)(num2 * 1454720512) ^ -332633233;
						continue;
					default:
						lACCIJUmQLfYuFIHtwCLnDEvtzPO2.mbjNeFkSdQzRbmrdjIJaVhGBVnZq = suffix;
						lACCIJUmQLfYuFIHtwCLnDEvtzPO2.kPgkgemDbIhwVGUaBlaPeGWDKDdAb = coloriser;
						return new DisplayablePerformanceCounter<float, float>(millisecondCounter, lACCIJUmQLfYuFIHtwCLnDEvtzPO2.QQGVCtrujGBsyMkCaBfzIsEMyRbfA, lACCIJUmQLfYuFIHtwCLnDEvtzPO2.NQGKRZeTkbKSRhJBkgYmVlFqPpPK);
					}
					break;
				}
			}
		}

		public static DisplayablePerformanceCounter<long, double> ForByteMetric(IPerformanceCounter<long, double> byteMetric, [CanBeNull] ThresholdColoriser coloriser = null, string prefix = null, string suffix = null)
		{
			uVKzDRtmmZmciCWtvLtdvQBHLkET uVKzDRtmmZmciCWtvLtdvQBHLkET2 = new uVKzDRtmmZmciCWtvLtdvQBHLkET();
			while (true)
			{
				int num = -2054861605;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -199003412)) % 4)
					{
					case 0u:
						break;
					case 3u:
						uVKzDRtmmZmciCWtvLtdvQBHLkET2.aXvcnDaxkaaBziUBwlQscxeoRzzTA = prefix;
						num = (int)(num2 * 2070888685) ^ -1548781094;
						continue;
					case 1u:
						uVKzDRtmmZmciCWtvLtdvQBHLkET2.mbjNeFkSdQzRbmrdjIJaVhGBVnZq = suffix;
						num = (int)(num2 * 1266661758) ^ -1353642420;
						continue;
					default:
						uVKzDRtmmZmciCWtvLtdvQBHLkET2.kPgkgemDbIhwVGUaBlaPeGWDKDdAb = coloriser;
						return new DisplayablePerformanceCounter<long, double>(byteMetric, uVKzDRtmmZmciCWtvLtdvQBHLkET2.NAHUQMQfGjlLAMVOTSTDhnuZjFrK, uVKzDRtmmZmciCWtvLtdvQBHLkET2.VjAdjCKNUqwdZClForBfoNnErBYi);
					}
					break;
				}
			}
		}
	}
	public class DisplayablePerformanceCounter<T, TAverage> : IDisplayableMetric
	{
		[CompilerGenerated]
		private readonly IPerformanceCounter<T, TAverage> OKsCRjnohBZopiCflNJpasaNpvXI;

		private readonly Func<IPerformanceCounter<T, TAverage>, string> aSAtLaTvmkqcYniQCDZQneweehoG;

		private readonly Func<IPerformanceCounter<T, TAverage>, Color> kPgkgemDbIhwVGUaBlaPeGWDKDdAb;

		public IPerformanceCounter<T, TAverage> PerformanceCounter
		{
			[CompilerGenerated]
			get
			{
				return OKsCRjnohBZopiCflNJpasaNpvXI;
			}
		}

		public Color DisplayColor => kPgkgemDbIhwVGUaBlaPeGWDKDdAb(PerformanceCounter);

		public DisplayablePerformanceCounter(IPerformanceCounter<T, TAverage> performanceCounter, Func<IPerformanceCounter<T, TAverage>, string> formatter, Func<IPerformanceCounter<T, TAverage>, Color> coloriser)
		{
			while (true)
			{
				int num = -856507013;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1706117720)) % 3)
					{
					case 2u:
						break;
					case 1u:
						goto IL_0028;
					default:
						kPgkgemDbIhwVGUaBlaPeGWDKDdAb = coloriser;
						return;
					}
					break;
					IL_0028:
					OKsCRjnohBZopiCflNJpasaNpvXI = performanceCounter;
					aSAtLaTvmkqcYniQCDZQneweehoG = formatter;
					num = ((int)num2 * -1497793003) ^ 0x64A3D4EB;
				}
			}
		}

		public override string ToString()
		{
			return aSAtLaTvmkqcYniQCDZQneweehoG(PerformanceCounter);
		}
	}
}
