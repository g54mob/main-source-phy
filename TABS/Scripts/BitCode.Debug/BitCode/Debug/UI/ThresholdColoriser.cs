using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BitCode.Debug.UI
{
	public class ThresholdColoriser
	{
		[Serializable]
		private sealed class zhzuCEXyYSJokcxwneoOptcywCwm
		{
			public static readonly zhzuCEXyYSJokcxwneoOptcywCwm _003C_003E9 = new zhzuCEXyYSJokcxwneoOptcywCwm();

			public static Comparison<(double value, Color color)> _003C_003E9__8_0;

			internal int WrgtEjsqurTTcoIzIcydfHonlaWfb((double value, Color color) P_0, (double value, Color color) P_1)
			{
				return P_0.value.CompareTo(P_1.value);
			}
		}

		private readonly Color jFalMEeKOMrJkSMCMCWLghMpOsXh;

		private readonly (double value, Color color)[] yPcKkbTJCuCgUjRRhzMWEoDUGckYA;

		[CompilerGenerated]
		private static readonly ThresholdColoriser jkAChfKXzeLlQTqxFtfoiiToZEnCA = new ThresholdColoriser(Color.white, new(double, Color)[4]
		{
			(16.6, Color.green),
			(18.0, Color.cyan),
			(20.0, Color.yellow),
			(22.0, Color.red)
		});

		[CompilerGenerated]
		private static readonly ThresholdColoriser RtIirlCiqSivTjGGlKZNnerJqjXV;

		public static ThresholdColoriser SixtyFpsMsColoriser
		{
			[CompilerGenerated]
			get
			{
				return jkAChfKXzeLlQTqxFtfoiiToZEnCA;
			}
		}

		public static ThresholdColoriser ThirtyFpsMsColoriser
		{
			[CompilerGenerated]
			get
			{
				return RtIirlCiqSivTjGGlKZNnerJqjXV;
			}
		}

		public ThresholdColoriser(Color defaultColour, IEnumerable<(double value, Color color)> thresholds)
		{
			jFalMEeKOMrJkSMCMCWLghMpOsXh = defaultColour;
			List<(double, Color)> list = thresholds.ToList();
			list.Sort(zhzuCEXyYSJokcxwneoOptcywCwm._003C_003E9.WrgtEjsqurTTcoIzIcydfHonlaWfb);
			yPcKkbTJCuCgUjRRhzMWEoDUGckYA = list.ToArray();
		}

		public Color GetColour(double value)
		{
			if (double.IsNaN(value))
			{
				goto IL_000b;
			}
			goto IL_00aa;
			IL_000b:
			int num = -1332566608;
			goto IL_0010;
			IL_0010:
			int num3 = default(int);
			Color result = default(Color);
			(double, Color)[] array = default((double, Color)[]);
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -922123022)) % 8)
				{
				case 5u:
					break;
				case 3u:
					goto IL_0045;
				case 7u:
					num3++;
					num = -1188779623;
					continue;
				case 2u:
					return jFalMEeKOMrJkSMCMCWLghMpOsXh;
				case 1u:
					goto IL_007e;
				case 6u:
					goto IL_00aa;
				case 4u:
					return result;
				default:
					return jFalMEeKOMrJkSMCMCWLghMpOsXh;
				}
				break;
				IL_007e:
				double num4;
				(num4, result) = array[num3];
				int num5;
				if (num4 < value)
				{
					num = -1614828867;
					num5 = num;
				}
				else
				{
					num = -589882426;
					num5 = num;
				}
				continue;
				IL_0045:
				int num6;
				if (num3 < array.Length)
				{
					num = -810956901;
					num6 = num;
				}
				else
				{
					num = -16960846;
					num6 = num;
				}
			}
			goto IL_000b;
			IL_00aa:
			array = yPcKkbTJCuCgUjRRhzMWEoDUGckYA;
			num3 = 0;
			num = -1188779623;
			goto IL_0010;
		}

		static ThresholdColoriser()
		{
			while (true)
			{
				int num = 1151742459;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5BC11185)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 1u:
						goto IL_00a2;
					case 2u:
						return;
					}
					break;
					IL_00a2:
					RtIirlCiqSivTjGGlKZNnerJqjXV = new ThresholdColoriser(Color.white, new(double, Color)[4]
					{
						(33.3, Color.green),
						(35.0, Color.cyan),
						(40.0, Color.yellow),
						(50.0, Color.red)
					});
					num = (int)(num2 * 1187585809) ^ -989535901;
				}
			}
		}
	}
}
