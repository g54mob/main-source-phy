using System;
using System.Text.RegularExpressions;
using Cpp2ILInjected;
using UnityEngine;

public static class GridCodeConverter
{
	private static readonly Regex codeRegex;

	public static readonly float[] digitThresholds;

	public static string LocalToCode(Vector2 localPos, float cellWidth, float cellHeight, bool yIncreasesUp, int unusedRowDecimals)
	{
		//IL_0410: Invalid comparison between I4 and F4
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		//IL_0028: Invalid comparison between I4 and F4
		//IL_00f6: Expected I4, but got F8
		//IL_014f: Expected I, but got O
		//IL_01bc: Expected I, but got O
		//IL_01cc: Expected O, but got I
		//IL_0241: Expected I, but got O
		//IL_0251: Expected O, but got I
		//IL_02c6: Expected I, but got O
		//IL_02d6: Expected O, but got I
		object obj2 = default(object);
		object obj = obj2;
		if (!yIncreasesUp)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
			obj = obj2 ^ 0;
		}
		if (0f < cellWidth && 0f < cellHeight)
		{
			float num = (float)localPos / cellWidth;
			double num2 = Math.Floor(num);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm9\"");
			double num3 = Math.Floor(0.0);
			double num4 = num2 * (double)cellWidth;
			double num5 = (double)localPos - num4;
			double num6 = num5 / (double)cellWidth;
			int num7 = MapFracToDigit((float)num6);
			double num8 = num3 * (double)cellHeight;
			double num9 = (double)obj - num8;
			double num10 = num9 / (double)cellHeight;
			int num11 = MapFracToDigit((float)num10);
			string text = IndexToLetters((int)num2);
			object[] array = new object[4];
			if (array != null)
			{
				if (text != null)
				{
					nint num12 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj3 = default(object);
					if (obj3 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj4 = default(object);
						throw obj4;
					}
				}
				array[0] = text;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj5 = default(object);
				if (obj5 != null)
				{
					nint num13 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v494 @ rdx_v30 (Il2CppClass<System.Object[]>)+40]");
					object obj6 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj7 = default(object);
					bool flag = obj7 == null;
					object obj8 = obj5;
					if (flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj9 = default(object);
						throw obj9;
					}
				}
				array[1] = obj5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj10 = default(object);
				if (obj10 != null)
				{
					nint num14 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v586 @ rdx_v28 (Il2CppClass<System.Object[]>)+40]");
					object obj11 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj12 = default(object);
					bool flag2 = obj12 == null;
					object obj13 = obj10;
					if (flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj14 = default(object);
						throw obj14;
					}
				}
				array[2] = obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				object obj15 = default(object);
				if (obj15 != null)
				{
					nint num15 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v634 @ rdx_v26 (Il2CppClass<System.Object[]>)+40]");
					object obj16 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj17 = default(object);
					bool flag3 = obj17 == null;
					object obj18 = obj15;
					if (flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						object obj19 = default(object);
						throw obj19;
					}
				}
				array[3] = obj15;
				return string.Format("{0}{1} {2}:{3}", array);
			}
			return (string)(object)new NullReferenceException();
		}
		return "ERR 0:0";
	}

	public static string LocalToCodeRegion(Vector2 localPos, float cellWidth, float cellHeight, bool yIncreasesUp, int unusedRowDecimals)
	{
		//IL_00c0: Invalid comparison between I4 and F4
		//IL_000e: Invalid comparison between I4 and F4
		//IL_0063: Expected I4, but got F8
		float num = default(float);
		if (yIncreasesUp)
		{
			if (!(0f < cellWidth) || !(0f < cellHeight))
			{
				return "ERR 0:0";
			}
			num = (float)localPos / cellWidth;
		}
		double num2 = Math.Floor(num);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
		double num3 = Math.Floor(0.0);
		string arg = IndexToLetters((int)num2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object arg2 = default(object);
		return $"{arg}{arg2}";
	}

	public static Vector2 CodeToLocal(string code, float cellWidth, float cellHeight, bool yIncreasesUp)
	{
		//IL_0199: Expected O, but got I4
		//IL_022f: Expected O, but got I4
		//IL_02c5: Expected O, but got I4
		//IL_03b3: Expected O, but got I4
		//IL_03c1: Expected O, but got I4
		//IL_03ce: Expected I4, but got O
		Vector2 result = default(Vector2);
		if (!string.IsNullOrWhiteSpace(code))
		{
			if (code != null)
			{
				string input = code.Trim();
				if (codeRegex != null)
				{
					Match match = codeRegex.Match(input);
					if (match != null)
					{
						if (!match.Success)
						{
							return result;
						}
						GroupCollection groups = match.Groups;
						if (groups != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
							Capture capture = default(Capture);
							if (capture != null)
							{
								string value = capture.Value;
								if (value != null)
								{
									string text = value.ToUpperInvariant();
									GroupCollection groups2 = match.Groups;
									if (groups2 != null)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
										Capture capture2 = default(Capture);
										if (capture2 != null)
										{
											string value2 = capture2.Value;
											bool flag = int.TryParse(value2, out var result2);
											bool flag2 = !flag;
											object obj = 0;
											if (flag2)
											{
												goto IL_041a;
											}
											GroupCollection groups3 = match.Groups;
											if (groups3 != null)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
												Capture capture3 = default(Capture);
												if (capture3 != null)
												{
													string value3 = capture3.Value;
													bool flag3 = int.TryParse(value3, out var result3);
													bool flag4 = !flag3;
													obj = 0;
													if (flag4)
													{
														goto IL_041a;
													}
													GroupCollection groups4 = match.Groups;
													if (groups4 != null)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1814A0330");
														Capture capture4 = default(Capture);
														if (capture4 != null)
														{
															string value4 = capture4.Value;
															bool flag5 = int.TryParse(value4, out var result4);
															bool flag6 = !flag5;
															obj = 0;
															if (!flag6)
															{
																if ((result2 <= 1 && result3 < 0) || result3 > 9)
																{
																}
																if (result4 < 0 || result4 > 9)
																{
																}
																bool flag7 = text == null;
																int num = 0;
																int num2 = 0;
																int num3 = 0;
																if (flag7)
																{
																	goto IL_0433;
																}
																while (num2 < text._stringLength)
																{
																	char c = text.get_Chars(num3);
																	object obj2 = num * 26;
																	object obj3 = c + -64;
																	num = (int)(obj2 + obj3);
																	num3++;
																	num2 = num3;
																}
																Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm2,dword ptr [rsp+70h]\"");
																Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,eax\"");
																Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm3,eax\"");
																Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsi2ss xmm0,dword ptr [rsp+24h]\"");
																if (yIncreasesUp)
																{
																	return result;
																}
															}
															goto IL_041a;
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			goto IL_0433;
		}
		return result;
		IL_041a:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180371660");
		Vector2 result5 = default(Vector2);
		return result5;
		IL_0433:
		return (Vector2)new NullReferenceException();
	}

	private static int MapFracToDigit(float frac)
	{
		//IL_00e9: Invalid comparison between I4 and F4
		//IL_0044: Expected F4, but got I4
		//IL_0106: Expected O, but got I4
		//IL_0131: Expected I4, but got O
		//IL_00a0: Invalid comparison between I and F4
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Expected O, but got Unknown
		float num;
		if (!(0f > frac))
		{
			bool flag = !(frac > 1f);
			num = frac;
			if (!flag)
			{
				num = 1f;
			}
		}
		else
		{
			num = 0f;
		}
		object obj = 32;
		int num2 = 0;
		while (true)
		{
			float[] array = digitThresholds;
			if (num2 >= array.Length)
			{
				return 9;
			}
			float[] array2 = digitThresholds;
			if (num2 >= array2.Length)
			{
				break;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdi_v2+v78 @ rdx_v5 (System.Single[])]");
			if (!(0f > num))
			{
				num2++;
				obj += 4;
				continue;
			}
			return num2;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (int)ex;
	}

	private static string IndexToLetters(int index)
	{
		//IL_0170: Expected O, but got I4
		//IL_0193: Expected O, but got I
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Expected O, but got Unknown
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A11C]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = index < 0;
		int num = 0;
		if (!flag)
		{
			num = index;
		}
		object obj = num + 1;
		bool flag2 = (nint)obj <= 0;
		string result = "";
		IntPtr intPtr = default(IntPtr);
		string text = (string)(nint)intPtr;
		string text2 = "";
		if (!flag2)
		{
			string text4 = default(string);
			bool flag3;
			do
			{
				object obj2 = obj - 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
				object obj3 = (object)text >> 3;
				object obj4 = obj3 >> 31;
				object obj5 = obj3 + obj4;
				object obj6 = obj5 * 26;
				object obj7 = obj2 - obj6;
				object obj8 = obj7 + 65;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D4CC80");
				string text3 = text4 + text2;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ebx\"");
				object obj9 = (object)text2 >> 3;
				object obj10 = obj9 >> 31;
				obj = obj9 + obj10;
				flag3 = (nint)obj > 0;
				result = text3;
				text = text2;
				text2 = text3;
			}
			while (flag3);
		}
		return result;
	}

	private static int LettersToIndex(string letters)
	{
		//IL_00ae: Expected I4, but got O
		//IL_0057: Expected O, but got I4
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Expected O, but got Unknown
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected I4, but got Unknown
		bool flag = letters == null;
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		if (flag)
		{
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		while (num < letters._stringLength)
		{
			char c = letters.get_Chars(num2);
			object obj = num3 * 26;
			object obj2 = obj + -64;
			num3 = (int)(obj2 + c);
			num2++;
			num = num2;
		}
		return num3 - 1;
	}

	static GridCodeConverter()
	{
		Regex regex = new Regex("^([A-Za-z]+)(\\d+)\\s+([0-9]):([0-9])$", RegexOptions.Compiled);
		codeRegex = regex;
		digitThresholds = new float[10] { 0.1f, 0.2f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f, 0.9f, 1.01f };
	}
}
