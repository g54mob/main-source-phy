using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using UnityEngine;

public class CorrectionDistanceTierConfig : MonoBehaviour
{
	public enum Mode
	{
		Bracketed,
		Exact,
		Ranged
	}

	[Serializable]
	public struct Bracket
	{
		public float maxDistance;

		public string label;
	}

	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public float low;

		public float high;

		internal unsafe string _003CFormatRange_003Eb__0(Match m)
		{
			//IL_0276: Expected Ref, but got F4
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A38B]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			string text;
			string text2;
			if (m != null)
			{
				GroupCollection groups = m.Groups;
				if (groups != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
					Capture capture = default(Capture);
					if (capture != null)
					{
						string value = capture.Value;
						if (value != null)
						{
							text = value.ToLowerInvariant();
							GroupCollection groups2 = m.Groups;
							if (groups2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
								Group obj = default(Group);
								if (obj != null)
								{
									if (!obj.Success)
									{
										text2 = "0.0";
										goto IL_023e;
									}
									GroupCollection groups3 = m.Groups;
									if (groups3 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
										Capture capture2 = default(Capture);
										if (capture2 != null)
										{
											string value2 = capture2.Value;
											text2 = value2;
											goto IL_023e;
										}
									}
								}
							}
						}
					}
				}
			}
			return (string)(object)new NullReferenceException();
			IL_023e:
			float num;
			if (text == "low")
			{
				num = (float)this + 16f;
			}
			else
			{
				if (!(text == "high"))
				{
					return m.Value;
				}
				num = (float)this + 20f;
			}
			return ((float*)num)->ToString(text2);
		}
	}

	public Mode mode;

	public string exactFormat;

	public float unitScale;

	public List<Bracket> brackets;

	public float rangeStep;

	public bool clampRangeLowToZero;

	public string rangeFormat;

	private static readonly Regex RangeTokenRegex;

	private void OnEnable()
	{
		ImpactCorrectionTierController.ScheduleGlobalReevaluate();
	}

	private void OnDisable()
	{
		ImpactCorrectionTierController.ScheduleGlobalReevaluate();
	}

	public unsafe string FormatDistance(float rawDistance)
	{
		//IL_001d: Expected O, but got I4
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Expected O, but got Unknown
		//IL_0254: Expected O, but got I4
		//IL_025d: Expected O, but got I4
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Expected O, but got Unknown
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_00cd: Expected F4, but got I4
		//IL_036b: Expected O, but got I
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_029d: Invalid comparison between O and F4
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Expected F4, but got Unknown
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Expected O, but got Unknown
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Expected O, but got Unknown
		//IL_011b: Expected F4, but got I4
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Expected O, but got Unknown
		//IL_04e5: Expected F4, but got Ref
		//IL_04e5: Expected F4, but got Ref
		//IL_04f8: Expected F4, but got O
		//IL_0129: Invalid comparison between I4 and F4
		//IL_0149: Expected F4, but got I4
		float num = rawDistance;
		bool flag = mode == Mode.Bracketed;
		float num2;
		if (!flag)
		{
			object obj = mode - 1;
			if (flag)
			{
				object obj2 = rawDistance & -2147483649L;
				if ((nint)obj2 <= 2139095040)
				{
					object obj3 = rawDistance & -2147483649L;
					if ((nint)obj3 != 2139095040)
					{
						float value = unitScale * rawDistance;
						return SafeFormatExact(exactFormat, value);
					}
				}
				return SafeFormatExact(exactFormat, 0f);
			}
			if ((nint)obj == 1)
			{
				object obj4 = rawDistance & -2147483649L;
				if ((nint)obj4 <= 2139095040)
				{
					object obj5 = rawDistance & -2147483649L;
					if ((nint)obj5 != 2139095040)
					{
						num2 = unitScale * rawDistance;
						goto IL_049d;
					}
				}
				num2 = 0f;
				goto IL_049d;
			}
		}
		object obj6 = default(object);
		object obj7 = default(object);
		if (brackets != null)
		{
			List<Bracket> list = brackets;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rax_v5 (System.Collections.Generic.List`1<CorrectionDistanceTierConfig+Bracket>)+18]");
			if ((nint)0 != 0)
			{
				List<Bracket> list2 = brackets;
				obj6 = 0;
				obj7 = 0;
				goto IL_0476;
			}
		}
		float num3 = default(float);
		return num3.ToString("0.0");
		IL_049d:
		float num4 = rangeStep;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num5 = num4 & 0;
		if (1E-06f > num5)
		{
			num5 = 1f;
		}
		object obj8 = num2 & -2147483649L;
		if ((nint)obj8 <= 2139095040)
		{
			object obj9 = num2 & -2147483649L;
			if ((nint)obj9 != 2139095040)
			{
				goto IL_03ff;
			}
		}
		num2 = 0f;
		goto IL_03ff;
		IL_03ff:
		float num6 = num2 / num5;
		float num7 = MathF.Floor(num6);
		float num8 = num7 * num5;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F1C0");
		float num9 = num6 * num5;
		if (~(clampRangeLowToZero ? 1u : 0u) == 0 && 0f > num8)
		{
			num8 = 0f;
		}
		if (!(num8 > num9))
		{
			object obj10 = default(object);
			object obj11 = default(object);
			float high = default(float);
			return FormatRange(low: (float)((nint)(&obj10), (nint)(&obj11)), template: rangeFormat, high: high);
		}
		goto IL_0476;
		IL_0476:
		(float, float) tuple = default((float, float));
		string result = default(string);
		while (true)
		{
			object obj12 = obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ rcx_v7 (System.Collections.Generic.List`1<CorrectionDistanceTierConfig+Bracket>)+18]");
			if ((nint)obj12 < 0)
			{
				if (brackets == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				List<Bracket> list2 = brackets;
				if (System.Runtime.CompilerServices.Unsafe.As<(float, float), UIntPtr>(ref tuple) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num))
				{
					obj6++;
					if (brackets == null)
					{
						break;
					}
					obj7 = obj6;
					num = rawDistance;
					continue;
				}
				if (brackets == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				return result;
			}
			List<Bracket> list3 = brackets;
			if (brackets == null)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v462 @ rdx_v5 (System.Collections.Generic.List`1<CorrectionDistanceTierConfig+Bracket>)+18]");
			object obj13 = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			return result;
		}
		return (string)(object)new NullReferenceException();
	}

	private static float SafeScale(float value, float scale)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0078: Expected F4, but got I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Expected O, but got Unknown
		object obj = value & -2147483649L;
		if ((nint)obj <= 2139095040)
		{
			object obj2 = value & -2147483649L;
			if ((nint)obj2 != 2139095040)
			{
				return value * scale;
			}
		}
		return 0f;
	}

	private unsafe static (float, float) QuantizeRange(float scaledValue, float step, bool clampLowToZero)
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected F4, but got Unknown
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Expected O, but got Unknown
		//IL_0056: Expected F4, but got I4
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_018a: Expected F4, but got Ref
		//IL_018a: Expected F4, but got Ref
		//IL_0064: Invalid comparison between I4 and F4
		//IL_0076: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
		float num = step & 0;
		if (1E-06f > num)
		{
			num = 1f;
		}
		object obj = scaledValue & -2147483649L;
		float num2;
		if ((nint)obj <= 2139095040)
		{
			object obj2 = scaledValue & -2147483649L;
			bool flag = (nint)obj2 != 2139095040;
			num2 = scaledValue;
			if (flag)
			{
				goto IL_00c6;
			}
		}
		num2 = 0f;
		goto IL_00c6;
		IL_00c6:
		float num3 = num2 / num;
		float num4 = MathF.Floor(num3);
		float num5 = num4 * num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033F1C0");
		float num6 = num3 * num;
		if (!clampLowToZero)
		{
			goto IL_0123;
		}
		bool flag2 = !(0f < num5);
		float num7 = 0f;
		if (!flag2)
		{
			num7 = num5;
		}
		goto IL_013d;
		IL_013d:
		num5 = num7;
		goto IL_0123;
		IL_0123:
		if (!(num5 < num6))
		{
			float num8 = default(float);
			object obj3 = default(object);
			return ((nint)(&num8), (nint)(&obj3));
		}
		goto IL_013d;
	}

	private static string SafeFormatExact(string format, float value)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A387]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!string.IsNullOrEmpty(format))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			return string.Format(format, arg);
		}
		float num = default(float);
		return num.ToString("0.0");
	}

	private unsafe static string FormatRange(string template, float low, float high)
	{
		_003C_003Ec__DisplayClass16_0 CS_0024_003C_003E8__locals4 = new _003C_003Ec__DisplayClass16_0();
		CS_0024_003C_003E8__locals4.low = low;
		CS_0024_003C_003E8__locals4.high = high;
		if (string.IsNullOrEmpty(template))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			return $"{arg:0.0}–{arg2:0.0}";
		}
		MatchEvaluator evaluator = delegate(Match m)
		{
			//IL_0276: Expected Ref, but got F4
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A38B]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			string text;
			string text2;
			if (m != null)
			{
				GroupCollection groups = m.Groups;
				if (groups != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
					Capture capture = default(Capture);
					if (capture != null)
					{
						string value = capture.Value;
						if (value != null)
						{
							text = value.ToLowerInvariant();
							GroupCollection groups2 = m.Groups;
							if (groups2 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
								Group obj = default(Group);
								if (obj != null)
								{
									if (!obj.Success)
									{
										text2 = "0.0";
										goto IL_023e;
									}
									GroupCollection groups3 = m.Groups;
									if (groups3 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
										Capture capture2 = default(Capture);
										if (capture2 != null)
										{
											string value2 = capture2.Value;
											text2 = value2;
											goto IL_023e;
										}
									}
								}
							}
						}
					}
				}
			}
			return (string)(object)new NullReferenceException();
			IL_023e:
			float num;
			if (text == "low")
			{
				num = (float)CS_0024_003C_003E8__locals4 + 16f;
			}
			else
			{
				if (!(text == "high"))
				{
					return m.Value;
				}
				num = (float)CS_0024_003C_003E8__locals4 + 20f;
			}
			return ((float*)num)->ToString(text2);
		};
		if (RangeTokenRegex != null)
		{
			return RangeTokenRegex.Replace(template, evaluator);
		}
		return (string)(object)new NullReferenceException();
	}

	public unsafe CorrectionDistanceTierConfig()
	{
		//IL_0012: Expected O, but got Ref
		//IL_0024: Expected O, but got Ref
		exactFormat = "{0:0.0} m";
		unitScale = 1f;
		object obj = default(object);
		object obj2 = default(object);
		brackets = new List<Bracket>
		{
			(Bracket)(&obj),
			(Bracket)(&obj2)
		};
		rangeStep = 10f;
		clampRangeLowToZero = true;
		rangeFormat = "{low:0}-{high:0} m";
		base._002Ector();
	}

	static CorrectionDistanceTierConfig()
	{
		Regex rangeTokenRegex = new Regex("\\{(low|high)(:([^}]+))?\\}", (RegexOptions)9);
		RangeTokenRegex = rangeTokenRegex;
	}
}
