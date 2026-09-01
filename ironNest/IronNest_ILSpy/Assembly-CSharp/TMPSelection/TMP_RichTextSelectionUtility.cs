using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Cpp2ILInjected;

namespace TMPSelection;

public static class TMP_RichTextSelectionUtility
{
	public enum StyleFlags
	{
		None = 0,
		Bold = 1,
		Italic = 2,
		Underline = 4,
		SmallCaps = 8
	}

	private struct Mapping
	{
		public string Raw;

		public int PlainLength;

		public int[] PlainToRaw;

		public StyleFlags[] PlainStyles;
	}

	private enum TagKind
	{
		Open,
		Close
	}

	private static readonly (string, int, StyleFlags)[] s_OpenTags;

	private static readonly (string, int, StyleFlags)[] s_CloseTags;

	public static string ExtractRichSubstringByPlainRange(string raw, int plainStart, int plainEndInclusive, bool trimResult)
	{
		//IL_02c4: Expected O, but got I
		//IL_02d4: Expected O, but got I
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Expected I4, but got Unknown
		string text3;
		if (raw != null)
		{
			Mapping mapping = BuildMapping(raw);
			int[] plainToRaw = mapping.PlainToRaw;
			if (mapping.PlainLength > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
				if ((nint)mapping.Raw > 0)
				{
					bool flag = plainStart > plainEndInclusive;
					int num = plainEndInclusive;
					if (!flag)
					{
						num = plainStart;
					}
					bool flag2 = plainStart > plainEndInclusive;
					int num2 = plainStart;
					if (!flag2)
					{
						num2 = plainEndInclusive;
					}
					if (num2 >= 0 && num < (nint)mapping.Raw)
					{
						bool flag3 = num < 0;
						int num3 = 0;
						if (!flag3)
						{
							num3 = num;
						}
						int num4 = mapping.Raw - 1;
						if (num2 < (nint)mapping.Raw)
						{
							num4 = num2;
						}
						if (num3 <= num4)
						{
							if (mapping.PlainToRaw != null)
							{
								int val = plainToRaw[num4] + 1;
								int num5 = Math.Min(raw._stringLength, val);
								if (plainToRaw[num3] < 0 || plainToRaw[num3] >= raw._stringLength || num5 < plainToRaw[num3])
								{
									goto IL_02b4;
								}
								int length = num5 - plainToRaw[num3];
								string text = raw.Substring(plainToRaw[num3], length);
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
								if (mapping.PlainToRaw != null)
								{
									string text2 = BuildOpenTags((StyleFlags)plainToRaw[num3]);
									string s = text2 + text;
									string s2 = BalanceTags(s);
									text3 = StripEmptyTagPairs(s2);
									if (!trimResult)
									{
										goto IL_0396;
									}
									if (text3 != null)
									{
										return text3.Trim();
									}
								}
							}
							return (string)(object)new NullReferenceException();
						}
					}
				}
			}
		}
		goto IL_02b4;
		IL_02b4:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v206 @ rax_v4+B8]");
		object obj2 = 0;
		text3 = (string)obj2;
		goto IL_0396;
		IL_0396:
		return text3;
	}

	public unsafe static bool RemoveRichTextByPlainRange(ref string raw, int plainStart, int plainEndInclusive, bool trimOuterNewlines, bool preserveVisualSeparation)
	{
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Expected I4, but got Unknown
		//IL_0b98: Expected I4, but got O
		//IL_0564: Expected O, but got I
		//IL_0574: Expected O, but got I
		//IL_05e7: Expected O, but got I
		//IL_05f7: Expected O, but got I
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Expected O, but got Unknown
		//IL_033b: Expected O, but got I
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Expected I4, but got Unknown
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7f: Expected O, but got Unknown
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Expected I4, but got Unknown
		//IL_09a3: Expected O, but got I
		//IL_09b3: Expected O, but got I
		//IL_0aa4: Expected O, but got I4
		//IL_0aad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ab2: Expected I4, but got Unknown
		//IL_0a73: Expected O, but got I4
		int[] plainToRaw;
		int num3;
		int num5;
		int rawPos;
		int num9;
		ref string reference;
		if (raw != null)
		{
			Mapping mapping = BuildMapping(raw);
			plainToRaw = mapping.PlainToRaw;
			if (mapping.PlainLength > 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
				if ((nint)mapping.Raw > 0)
				{
					bool flag = plainStart > plainEndInclusive;
					int num = plainEndInclusive;
					if (!flag)
					{
						num = plainStart;
					}
					bool flag2 = plainStart > plainEndInclusive;
					int num2 = plainStart;
					if (!flag2)
					{
						num2 = plainEndInclusive;
					}
					if (num2 >= 0 && num < (nint)mapping.Raw)
					{
						bool flag3 = num < 0;
						num3 = 0;
						if (!flag3)
						{
							num3 = num;
						}
						int num4 = mapping.Raw - 1;
						if (num2 < (nint)mapping.Raw)
						{
							num4 = num2;
						}
						if (num3 <= num4)
						{
							if (mapping.PlainToRaw != null)
							{
								num5 = plainToRaw[num3];
								string text = raw;
								if (raw != null)
								{
									int val = plainToRaw[num4] + 1;
									rawPos = Math.Min(text._stringLength, val);
									if (plainToRaw[num3] < 0)
									{
										goto IL_0ad4;
									}
									string text2 = raw;
									if (raw != null)
									{
										if (plainToRaw[num3] > text2._stringLength)
										{
											goto IL_0ad4;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
										if (mapping.PlainToRaw != null)
										{
											int num6;
											if (num3 > 0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ xmm2_v4 (System.Int32[])+1C+v1134 @ rbx_v28 (System.Int32)*4]");
												num6 = 0;
												if (num3 > 2)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ xmm2_v4 (System.Int32[])+14+v1134 @ rbx_v28 (System.Int32)*4]");
													num6 = 0;
												}
											}
											else
											{
												num6 = 0;
											}
											int num7 = ~num6;
											string text3 = raw;
											int num8 = num7 & plainToRaw[num3];
											bool flag4 = num8 == 0;
											num9 = 0;
											reference = ref raw;
											if (!flag4)
											{
												bool flag5 = num5 <= 0;
												num9 = 0;
												reference = ref raw;
												int num10 = num8;
												int num11 = num5;
												int i = 0;
												if (!flag5)
												{
													while (true)
													{
														IL_0e1c:
														if (num11 != 0)
														{
															object obj = s_OpenTags + 32;
															bool flag6 = s_OpenTags == null;
															(string, int, StyleFlags)[] array = s_OpenTags;
															if (flag6)
															{
																break;
															}
															for (; i < array.Length; i++, obj += 16)
															{
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ r13_v12+8]");
																object obj2 = (nint)0 >> 32;
																if ((num8 & obj2) == 0)
																{
																	continue;
																}
																int num12 = num11;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ r13_v12+8]");
																if ((nint)num12 < (nint)0)
																{
																	continue;
																}
																int num13 = num11;
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ r13_v12+8]");
																int num14 = (int)((nint)num13 - (nint)0);
																if (num14 < 0 || num11 > text3._stringLength)
																{
																	goto IL_04d3;
																}
																Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ r13_v12+8]");
																bool flag7 = (nint)0 <= (nint)0;
																num3 = 0;
																if (!flag7)
																{
																	num3 = 0;
																	while (true)
																	{
																		int index = num3 + num14;
																		char c = text3.get_Chars(index);
																		char c2 = char.ToLowerInvariant(c);
																		if (obj == null)
																		{
																			break;
																		}
																		char c3 = ((string)obj).get_Chars(num3);
																		char c4 = char.ToLowerInvariant(c3);
																		bool flag8 = c2 != c4;
																		val = 0;
																		if (flag8)
																		{
																			goto IL_04d3;
																		}
																		num3++;
																		int num15 = num3;
																		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v561 @ r13_v12+8]");
																		if ((nint)num15 < (nint)0)
																		{
																			continue;
																		}
																		goto IL_04e9;
																	}
																	goto end_IL_0e1c;
																}
																goto IL_0c84;
																IL_0c84:
																object obj3 = ~obj2;
																num8 = num10 & obj3;
																num10 = num8;
																num11 = num14;
																i = 0;
																goto IL_0e1c;
																IL_04d3:
																num8 = num10;
																array = s_OpenTags;
																continue;
																IL_04e9:
																val = 0;
																goto IL_0c84;
															}
															i = 0;
														}
														num5 = num11;
														num9 = i;
														reference = ref raw;
														goto IL_0bf3;
														continue;
														end_IL_0e1c:
														break;
													}
													goto IL_0b8a;
												}
											}
											goto IL_0bf3;
										}
									}
								}
							}
							goto IL_0b8a;
						}
					}
				}
			}
		}
		goto IL_0ad4;
		IL_0d13:
		string text4;
		if (!string.Equals(text4, reference, StringComparison.Ordinal))
		{
			reference = ref *(string*)text4;
			return true;
		}
		goto IL_0ad4;
		IL_0bf3:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ xmm2_v4 (System.Int32[])+24+v128 @ rsi_v6 (System.Int32)*4]");
		int stylesMask = (int)((nint)0 | (nint)plainToRaw[num3]);
		int num16 = ConsumeTrailingCloseTags(reference, rawPos, (StyleFlags)stylesMask);
		string text6;
		if (num16 > num5)
		{
			if (reference == null)
			{
				goto IL_0b8a;
			}
			int length = num16 - num5;
			string text5 = reference.Substring(num5, length);
			text6 = text5;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
			object obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1047 @ rax_v79+B8]");
			object obj5 = 0;
			text6 = (string)obj5;
		}
		string text7;
		string text8;
		string text9;
		if (reference != null)
		{
			text7 = reference.Substring(0, num5);
			if (reference != null)
			{
				text8 = reference.Substring(num16);
				object obj6 = default(object);
				bool flag9 = obj6 == null;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1156 @ rcx_v22+B8]");
				object obj8 = 0;
				text9 = (string)obj8;
				if (!flag9)
				{
					if (!string.IsNullOrEmpty(text6))
					{
						bool flag10 = text6 == null;
						int num17 = num9;
						int num18 = num9;
						if (flag10)
						{
							goto IL_0b8a;
						}
						while (num17 < text6._stringLength)
						{
							char c5 = text6.get_Chars(num18);
							if (c5 != '\n')
							{
								char c6 = text6.get_Chars(num18);
								if (c6 != '\r')
								{
									num18++;
									num17 = num18;
									continue;
								}
							}
							goto IL_0cf9;
						}
					}
					if (!string.IsNullOrEmpty(text7))
					{
						if (text7 == null)
						{
							goto IL_0b8a;
						}
						int index2 = text7._stringLength - 1;
						char c7 = text7.get_Chars(index2);
						if (c7 == '\n' || c7 == '\r')
						{
							goto IL_0cf9;
						}
					}
					if (string.IsNullOrEmpty(text8))
					{
						goto IL_082b;
					}
					if (text8 == null)
					{
						goto IL_0b8a;
					}
					char c8 = text8.get_Chars(0);
					if (c8 != '\n')
					{
						char c9 = text8.get_Chars(0);
						if (c9 != '\r')
						{
							goto IL_082b;
						}
					}
				}
				goto IL_0cf9;
			}
		}
		goto IL_0b8a;
		IL_0b8a:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0993:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1523 @ rax_v54+B8]");
		object obj10 = 0;
		text4 = (string)obj10;
		goto IL_0d13;
		IL_0ad4:
		return false;
		IL_0cf9:
		string s = text7 + text9 + text8;
		string s2 = BalanceTags(s);
		string text10 = StripEmptyTagPairs(s2);
		bool flag11 = !trimOuterNewlines;
		text4 = text10;
		if (!flag11)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A5E1]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			bool flag12 = string.IsNullOrEmpty(text10);
			text4 = text10;
			if (!flag12)
			{
				if (text10 != null)
				{
					string text11 = text10.Replace("\r\n", "\n");
					if (text11 != null)
					{
						string text12 = text11.Replace('\r', '\n');
						bool flag13 = (nint)text12 < 0;
						if (text12 != null)
						{
							int num19 = text12._stringLength - 1;
							if (!flag13)
							{
								while (true)
								{
									char c10 = text12.get_Chars(num9);
									if (c10 != '\n')
									{
										break;
									}
									num9++;
									if (num9 <= num19)
									{
										continue;
									}
									goto IL_0993;
								}
								if (num19 >= num9)
								{
									while (true)
									{
										char c11 = text12.get_Chars(num19);
										if (c11 != '\n')
										{
											break;
										}
										num19--;
										if (num19 >= num9)
										{
											continue;
										}
										goto IL_0993;
									}
									if (num9 <= num19)
									{
										if (num9 == 0)
										{
											object obj11 = text12._stringLength - 1;
											bool flag14 = num19 == (nint)obj11;
											text4 = text12;
											if (flag14)
											{
												goto IL_0d13;
											}
										}
										object obj12 = num19 - num9;
										int length2 = obj12 + 1;
										string text13 = text12.Substring(num9, length2);
										text4 = text13;
										goto IL_0d13;
									}
								}
							}
							goto IL_0993;
						}
					}
				}
				goto IL_0b8a;
			}
		}
		goto IL_0d13;
		IL_082b:
		text9 = "\n";
		goto IL_0cf9;
	}

	public static string SanitizeRichText(string s)
	{
		if (!string.IsNullOrEmpty(s))
		{
			string s2 = BalanceTags(s);
			return StripEmptyTagPairs(s2);
		}
		return s;
	}

	private static int ConsumeLeadingOpenTagsBackward(string raw, int rawPos, StyleFlags stylesMask)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0077: Expected O, but got I4
		//IL_02b2: Expected I4, but got O
		//IL_00c5: Expected O, but got I
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Expected O, but got Unknown
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Expected O, but got Unknown
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected I4, but got Unknown
		StyleFlags styleFlags = default(StyleFlags);
		int num = default(int);
		if (styleFlags != StyleFlags.None && num > 0)
		{
			int num2 = num;
			StyleFlags styleFlags2 = styleFlags;
			StyleFlags styleFlags3 = styleFlags;
			while (true)
			{
				IL_0354:
				if (num2 != 0)
				{
					object obj = s_OpenTags + 32;
					bool flag = s_OpenTags == null;
					object obj2 = obj;
					object obj3 = 0;
					(string, int, StyleFlags)[] array = s_OpenTags;
					if (flag)
					{
						break;
					}
					for (; (nint)obj3 < array.Length; obj3++, obj += 16, obj2 = obj)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rax_v16+8]");
						object obj4 = (nint)0 >> 32;
						object obj5 = styleFlags2 & obj4;
						if (obj5 == null)
						{
							continue;
						}
						int num3 = num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rax_v16+8]");
						if ((nint)num3 < (nint)0)
						{
							continue;
						}
						int num4 = num2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rax_v16+8]");
						int num5 = (int)((nint)num4 - (nint)0);
						if (num5 >= 0)
						{
							if (raw == null)
							{
								goto end_IL_0354;
							}
							if (num2 <= raw._stringLength)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rax_v16+8]");
								if ((nint)0 > (nint)0)
								{
									int num6 = 0;
									while (true)
									{
										int index = num6 + num5;
										char c = raw.get_Chars(index);
										char c2 = char.ToLowerInvariant(c);
										if (obj == null)
										{
											break;
										}
										char c3 = ((string)obj).get_Chars(num6);
										char c4 = char.ToLowerInvariant(c3);
										if (c2 == c4)
										{
											num6++;
											int num7 = num6;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rax_v16+8]");
											if ((nint)num7 < (nint)0)
											{
												continue;
											}
											goto IL_023f;
										}
										goto IL_0256;
									}
									goto end_IL_0354;
								}
								goto IL_0306;
							}
						}
						goto IL_02f8;
						IL_0306:
						object obj6 = ~obj4;
						styleFlags2 = (StyleFlags)(styleFlags3 & obj6);
						num2 = num5;
						styleFlags3 = styleFlags2;
						goto IL_0354;
						IL_02f8:
						array = s_OpenTags;
						continue;
						IL_023f:
						styleFlags = StyleFlags.None;
						num = 0;
						goto IL_0306;
						IL_0256:
						styleFlags = StyleFlags.None;
						num = 0;
						obj = obj2;
						styleFlags2 = styleFlags3;
						goto IL_02f8;
					}
				}
				return num2;
				continue;
				end_IL_0354:
				break;
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return num;
	}

	private static int ConsumeTrailingCloseTags(string raw, int rawPos, StyleFlags stylesMask)
	{
		//IL_02bc: Expected I4, but got O
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Expected O, but got Unknown
		//IL_009a: Expected O, but got I4
		//IL_00e9: Expected O, but got I
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Expected O, but got Unknown
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_0128: Expected O, but got I
		//IL_0181: Expected O, but got I
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Expected I4, but got Unknown
		if (stylesMask != StyleFlags.None)
		{
			bool flag = raw == null;
			int num = rawPos;
			StyleFlags styleFlags = stylesMask;
			StyleFlags styleFlags2 = stylesMask;
			if (!flag)
			{
				while (true)
				{
					object obj3;
					if (num < raw._stringLength)
					{
						char c = raw.get_Chars(num);
						if (c == '<')
						{
							object obj = s_CloseTags + 32;
							bool flag2 = s_CloseTags == null;
							object obj2 = 0;
							(string, int, StyleFlags)[] array = s_CloseTags;
							if (flag2)
							{
								break;
							}
							for (; (nint)obj2 < array.Length; obj2++, obj += 16)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r13_v5+8]");
								obj3 = (nint)0 >> 32;
								object obj4 = styleFlags & obj3;
								if (obj4 == null)
								{
									continue;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r13_v5+8]");
								object obj5 = (nint)0 + (nint)num;
								if ((nint)obj5 <= raw._stringLength && num >= 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r13_v5+8]");
									object obj6 = (nint)0 + (nint)num;
									if ((nint)obj6 <= raw._stringLength)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r13_v5+8]");
										if ((nint)0 > (nint)0)
										{
											int num2 = 0;
											while (true)
											{
												int index = num2 + num;
												char c2 = raw.get_Chars(index);
												char c3 = char.ToLowerInvariant(c2);
												if (obj == null)
												{
													break;
												}
												char c4 = ((string)obj).get_Chars(num2);
												char c5 = char.ToLowerInvariant(c4);
												if (c3 == c5)
												{
													num2++;
													int num3 = num2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r13_v5+8]");
													if ((nint)num3 >= (nint)0)
													{
														goto IL_02eb;
													}
													continue;
												}
												goto IL_0277;
											}
											goto end_IL_0346;
										}
										goto IL_02eb;
									}
								}
								goto IL_02dd;
								IL_0277:
								styleFlags = styleFlags2;
								goto IL_02dd;
								IL_02dd:
								array = s_CloseTags;
							}
						}
					}
					return num;
					IL_02eb:
					int num4 = num;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ r13_v5+8]");
					num = (int)((nint)num4 + (nint)0);
					object obj7 = ~obj3;
					styleFlags = (StyleFlags)(styleFlags2 & obj7);
					styleFlags2 = styleFlags;
					continue;
					end_IL_0346:
					break;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (int)ex;
		}
		return rawPos;
	}

	private unsafe static string BalanceTags(string s)
	{
		//IL_0073: Expected O, but got I4
		//IL_0695: Expected O, but got I
		//IL_00d2: Expected O, but got I4
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Expected O, but got Unknown
		//IL_014a: Expected O, but got I4
		//IL_0784: Expected O, but got I4
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0791: Expected I4, but got Unknown
		//IL_05d0: Expected O, but got I4
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Expected I4, but got Unknown
		//IL_0221: Expected O, but got I4
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Expected I4, but got Unknown
		//IL_033f: Expected O, but got I
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Expected I4, but got Unknown
		//IL_051a: Expected I4, but got O
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				int stringLength = s._stringLength;
				int capacity = default(int);
				StringBuilder stringBuilder = new StringBuilder(capacity);
				capacity = s._stringLength + 32;
				List<StyleFlags> list = new List<StyleFlags>(8);
				List<StyleFlags>.Enumerator enumerator = (List<StyleFlags>.Enumerator)0;
				int num = 0;
				string text = s;
				object obj5 = default(object);
				int capacity2 = default(int);
				StyleFlags flags = default(StyleFlags);
				object obj7 = default(object);
				int num7 = default(int);
				StyleFlags flags2 = default(StyleFlags);
				StyleFlags flags3 = default(StyleFlags);
				List<StyleFlags>.Enumerator enumerator2 = default(List<StyleFlags>.Enumerator);
				while (true)
				{
					char c;
					int num3;
					int num4;
					List<StyleFlags> list2;
					bool flag;
					int num5;
					if (num < text._stringLength)
					{
						c = text.get_Chars(num);
						if (c == '<')
						{
							object obj = text._stringLength - num;
							if ((nint)obj >= 3)
							{
								if ((nint)obj < 4)
								{
									flag = false;
								}
								else
								{
									int index = num + 1;
									char c2 = s.get_Chars(index);
									object obj2 = c2 - 47;
									bool flag2 = obj2 == null;
									flag = flag2;
								}
								object obj3 = (flag ? 1 : 0) + 1;
								int num2 = obj3 + num;
								if (num2 < s._stringLength)
								{
									char c3 = s.get_Chars(num2);
									char c4 = char.ToLowerInvariant(c3);
									if (c4 == 'b')
									{
										stringLength = 1;
									}
									else if (c4 == 'i')
									{
										stringLength = 2;
									}
									else
									{
										if (c4 != 'u')
										{
											goto IL_0599;
										}
										stringLength = 4;
									}
									num3 = (flag ? 1 : 0) + 3;
									bool flag3 = (nint)obj < num3;
									text = s;
									if (!flag3)
									{
										object obj4 = num3 - 1;
										int index2 = obj4 + num;
										char c5 = s.get_Chars(index2);
										bool flag4 = c5 != '>';
										text = s;
										if (!flag4)
										{
											if (list == null)
											{
												break;
											}
											bool flag5 = (flag ? 1 : 0) < (false ? 1 : 0);
											if (flag)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10 (System.Collections.Generic.List`1<TMPSelection.TMP_RichTextSelectionUtility+StyleFlags>)+18]");
												num4 = (int)(-1);
												if (flag5)
												{
													goto IL_0303;
												}
												while (true)
												{
													Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
													if ((nint)obj5 == stringLength)
													{
														break;
													}
													num4--;
													bool flag6 = (nint)obj5 >= stringLength;
													num5 = 0;
													if (flag6)
													{
														continue;
													}
													goto IL_0303;
												}
												list2 = new List<StyleFlags>(capacity2);
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10 (System.Collections.Generic.List`1<TMPSelection.TMP_RichTextSelectionUtility+StyleFlags>)+18]");
												object obj6 = -num4;
												capacity2 = obj6 - 1;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10 (System.Collections.Generic.List`1<TMPSelection.TMP_RichTextSelectionUtility+StyleFlags>)+18]");
												int num6 = (int)(-1);
												if (num6 > num4)
												{
													while (true)
													{
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														string value = BuildCloseTags(flags);
														if (stringBuilder == null)
														{
															break;
														}
														StringBuilder stringBuilder2 = stringBuilder.Append(value);
														Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
														if (list2 == null)
														{
															break;
														}
														list2.Insert(0, (StyleFlags)(int)(&obj7));
														list.RemoveAt(num6);
														num6--;
														if (num6 > num4)
														{
															continue;
														}
														goto IL_043d;
													}
													break;
												}
												goto IL_043d;
											}
											list.Add((StyleFlags)(int)(&num7));
											if (stringBuilder == null)
											{
												break;
											}
											StringBuilder stringBuilder3 = stringBuilder.Append(s, num, num3);
											num5 = num3;
											text = s;
											goto IL_0587;
										}
									}
									goto IL_07e7;
								}
							}
							goto IL_0599;
						}
						goto IL_0612;
					}
					bool flag7 = (nint)list < 0;
					if (list == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ rax_v10 (System.Collections.Generic.List`1<TMPSelection.TMP_RichTextSelectionUtility+StyleFlags>)+18]");
					object obj8 = -1;
					if (flag7)
					{
						if (stringBuilder == null)
						{
							break;
						}
						goto IL_072e;
					}
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						string value2 = BuildCloseTags(flags2);
						if (stringBuilder == null)
						{
							break;
						}
						StringBuilder stringBuilder4 = stringBuilder.Append(value2);
						obj8--;
						if ((nint)stringBuilder >= 0)
						{
							continue;
						}
						goto IL_072e;
					}
					break;
					IL_0303:
					num += num3;
					text = s;
					continue;
					IL_0612:
					if (stringBuilder == null)
					{
						break;
					}
					StringBuilder stringBuilder5 = stringBuilder.Append(c);
					num++;
					continue;
					IL_0587:
					num += num3;
					continue;
					IL_072e:
					return stringBuilder.ToString();
					IL_0599:
					text = s;
					goto IL_07e7;
					IL_043d:
					if (stringBuilder == null)
					{
						break;
					}
					StringBuilder stringBuilder6 = stringBuilder.Append(s, num, num3);
					list.RemoveAt(num4);
					if (list2 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						string value3 = BuildOpenTags(flags3);
						StringBuilder stringBuilder7 = stringBuilder.Append(value3);
						list.Add((StyleFlags)(int)(&obj7));
					}
					enumerator.Dispose();
					enumerator = enumerator2;
					num5 = num3;
					flag = (byte)(int)list2 != 0;
					stringLength = (int)(&enumerator);
					text = s;
					goto IL_0587;
					IL_07e7:
					int num8 = text.IndexOf('>', num);
					bool flag8 = num8 < 0;
					num5 = 0;
					stringLength = num8;
					if (!flag8)
					{
						if (stringBuilder == null)
						{
							break;
						}
						object obj9 = num8 - num;
						num5 = obj9 + 1;
						StringBuilder stringBuilder8 = stringBuilder.Append(text, num, num5);
						num = num8 + 1;
						stringLength = num8;
						continue;
					}
					goto IL_0612;
				}
			}
			return (string)(object)new NullReferenceException();
		}
		return s;
	}

	private static string StripEmptyTagPairs(string s)
	{
		//IL_003c: Expected O, but got I4
		//IL_0059: Expected O, but got I
		//IL_0069: Expected O, but got I
		//IL_00ac: Expected O, but got I
		//IL_00bc: Expected O, but got I
		//IL_00ff: Expected O, but got I
		//IL_010f: Expected O, but got I
		//IL_0152: Expected O, but got I
		//IL_0162: Expected O, but got I
		//IL_01a5: Expected O, but got I
		//IL_01b5: Expected O, but got I
		//IL_01f8: Expected O, but got I
		//IL_0208: Expected O, but got I
		//IL_024b: Expected O, but got I
		//IL_025b: Expected O, but got I
		//IL_029e: Expected O, but got I
		//IL_02ae: Expected O, but got I
		//IL_02f1: Expected O, but got I
		//IL_0301: Expected O, but got I
		//IL_0344: Expected O, but got I
		//IL_0354: Expected O, but got I
		//IL_0397: Expected O, but got I
		//IL_03a7: Expected O, but got I
		//IL_03ea: Expected O, but got I
		//IL_03fa: Expected O, but got I
		//IL_043d: Expected O, but got I
		//IL_044d: Expected O, but got I
		//IL_0490: Expected O, but got I
		//IL_04a0: Expected O, but got I
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A5DB]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!string.IsNullOrEmpty(s))
		{
			object obj = 0;
			string text = s;
			while (text != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ rax_v7+B8]");
				object newValue = 0;
				string text2 = text.Replace("<b></b>", (string)newValue);
				if (text2 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v361 @ rcx_v6+B8]");
				object newValue2 = 0;
				string text3 = text2.Replace("<i></i>", (string)newValue2);
				if (text3 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v367 @ rcx_v8+B8]");
				object newValue3 = 0;
				string text4 = text3.Replace("<u></u>", (string)newValue3);
				if (text4 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ rcx_v10+B8]");
				object newValue4 = 0;
				string text5 = text4.Replace("<B></B>", (string)newValue4);
				if (text5 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rcx_v12+B8]");
				object newValue5 = 0;
				string text6 = text5.Replace("<I></I>", (string)newValue5);
				if (text6 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj7 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rcx_v14+B8]");
				object newValue6 = 0;
				string text7 = text6.Replace("<U></U>", (string)newValue6);
				if (text7 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v383 @ rcx_v16+B8]");
				object newValue7 = 0;
				string text8 = text7.Replace("<smallcaps></smallcaps>", (string)newValue7);
				if (text8 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v385 @ rcx_v18+B8]");
				object newValue8 = 0;
				string text9 = text8.Replace("<b>\n</b>", (string)newValue8);
				if (text9 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ rcx_v20+B8]");
				object newValue9 = 0;
				string text10 = text9.Replace("<i>\n</i>", (string)newValue9);
				if (text10 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v389 @ rcx_v22+B8]");
				object newValue10 = 0;
				string text11 = text10.Replace("<u>\n</u>", (string)newValue10);
				if (text11 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj12 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v391 @ rcx_v24+B8]");
				object newValue11 = 0;
				string text12 = text11.Replace("<B>\n</B>", (string)newValue11);
				if (text12 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj13 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v393 @ rcx_v26+B8]");
				object newValue12 = 0;
				string text13 = text12.Replace("<I>\n</I>", (string)newValue12);
				if (text13 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj14 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ rcx_v28+B8]");
				object newValue13 = 0;
				string text14 = text13.Replace("<U>\n</U>", (string)newValue13);
				if (text14 == null)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
				object obj15 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rcx_v30+B8]");
				object newValue14 = 0;
				string text15 = text14.Replace("<smallcaps>\n</smallcaps>", (string)newValue14);
				if (!string.Equals(text15, text, StringComparison.Ordinal))
				{
					obj++;
					bool flag = (nint)obj < 16;
					text = text15;
					if (flag)
					{
						continue;
					}
				}
				return text15;
			}
			return (string)(object)new NullReferenceException();
		}
		return s;
	}

	private unsafe static Mapping BuildMapping(string raw)
	{
		//IL_0490: Expected native int or pointer, but got O
		//IL_049a: Expected native int or pointer, but got O
		//IL_00ad: Expected O, but got I4
		//IL_03e2: Expected native int or pointer, but got O
		//IL_03ec: Expected native int or pointer, but got O
		//IL_03f6: Expected native int or pointer, but got O
		//IL_0403: Expected native int or pointer, but got O
		//IL_0435: Expected native int or pointer, but got O
		//IL_044f: Expected native int or pointer, but got O
		//IL_046e: Expected native int or pointer, but got O
		//IL_011c: Expected O, but got I4
		//IL_03c4: Expected I4, but got O
		//IL_0194: Expected O, but got I4
		//IL_0506: Expected O, but got I4
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Expected I4, but got Unknown
		//IL_01fc: Expected O, but got I4
		//IL_022a: Expected O, but got I4
		//IL_0545: Expected O, but got I4
		//IL_0258: Expected O, but got I4
		//IL_026b: Expected O, but got I4
		//IL_0278: Expected I4, but got O
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Expected I4, but got Unknown
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected I4, but got Unknown
		Mapping mapping = default(Mapping);
		System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->Raw, null);
		System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->PlainToRaw, null);
		if (raw != null)
		{
			int capacity = Math.Max(16, raw._stringLength);
			List<int> list = new List<int>(capacity);
			int val = raw._stringLength + 1;
			int capacity2 = Math.Max(16, val);
			List<StyleFlags> list2 = new List<StyleFlags>(capacity2);
			if (list2 != null)
			{
				int num = default(int);
				list2.Add((StyleFlags)(int)(&num));
				num = 0;
				object obj = 0;
				List<StyleFlags> list3 = list2;
				List<int> list4 = list;
				int num2 = 0;
				int num3 = 0;
				while (true)
				{
					if (num3 < raw._stringLength)
					{
						char c = raw.get_Chars(num2);
						if (c != '<')
						{
							goto IL_0372;
						}
						object obj2 = raw._stringLength - num2;
						if ((nint)obj2 >= 3)
						{
							bool flag;
							if ((nint)obj2 < 4)
							{
								flag = false;
							}
							else
							{
								int index = num2 + 1;
								char c2 = raw.get_Chars(index);
								object obj3 = c2 - 47;
								bool flag2 = obj3 == null;
								flag = flag2;
							}
							object obj4 = (flag ? 1 : 0) + 1;
							int num4 = obj4 + num2;
							if (num4 < raw._stringLength)
							{
								char c3 = raw.get_Chars(num4);
								char c4 = char.ToLowerInvariant(c3);
								object obj5;
								if (c4 == 'b')
								{
									obj5 = 1;
								}
								else if (c4 == 'i')
								{
									obj5 = 2;
								}
								else
								{
									if (c4 != 'u')
									{
										goto IL_0339;
									}
									obj5 = 4;
								}
								object obj6 = (flag ? 1 : 0) + 3;
								if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
								{
									object obj7 = num2 - 1;
									int index2 = (int)(obj7 + obj6);
									char c5 = raw.get_Chars(index2);
									if (c5 == '>')
									{
										if (flag)
										{
											object obj8 = ~obj5;
											obj &= obj8;
											num2 += obj6;
											list3 = list2;
											list4 = list;
											num3 = num2;
										}
										else
										{
											obj |= obj5;
											num2 += obj6;
											list3 = list2;
											list4 = list;
											num3 = num2;
										}
										continue;
									}
								}
								goto IL_0339;
							}
						}
						goto IL_04bc;
					}
					((Mapping*)(nint)mapping)->PlainLength = 0;
					System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->PlainToRaw, null);
					System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->PlainStyles, null);
					System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->Raw, raw);
					if (list4 == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v346 @ rdi_v7 (System.Collections.Generic.List`1<System.Int32>)+18]");
					((Mapping*)(nint)mapping)->PlainLength = 0;
					int[] plainToRaw = list4.ToArray();
					System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->PlainToRaw, plainToRaw);
					StyleFlags[] plainStyles = list3.ToArray();
					System.Runtime.CompilerServices.Unsafe.Write(&((Mapping*)(nint)mapping)->PlainStyles, plainStyles);
					return mapping;
					IL_0339:
					list4 = list;
					goto IL_04bc;
					IL_0372:
					if (list4 == null)
					{
						break;
					}
					list4.Add((int)(&num));
					num2++;
					list3.Add((StyleFlags)(int)(&num));
					num = (int)obj;
					list4 = list;
					num3 = num2;
					continue;
					IL_04bc:
					int num5 = raw.IndexOf('>', num2);
					bool flag3 = num5 < 0;
					list3 = list2;
					if (!flag3)
					{
						num2 = num5 + 1;
						list3 = list2;
						list4 = list;
						num3 = num2;
						continue;
					}
					goto IL_0372;
				}
			}
		}
		return (Mapping)new NullReferenceException();
	}

	private unsafe static bool TryParseSupportedTag(string raw, int startIndex, out TagKind kind, out StyleFlags style, out int length)
	{
		//IL_001b: Expected O, but got I4
		//IL_0211: Expected I4, but got O
		//IL_004a: Expected O, but got I4
		//IL_021f: Expected O, but got I4
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected I4, but got Unknown
		//IL_00ba: Expected O, but got I4
		//IL_0122: Expected O, but got I4
		//IL_0150: Expected O, but got I4
		//IL_025e: Expected O, but got I4
		//IL_017e: Expected O, but got I4
		//IL_0191: Expected O, but got I4
		//IL_019e: Expected I4, but got O
		ref TagKind reference = ref *(TagKind*)null;
		ref StyleFlags reference2 = ref *(StyleFlags*)null;
		object obj = 0;
		if (raw != null)
		{
			object obj2 = raw._stringLength - startIndex;
			if ((nint)obj2 >= 3)
			{
				bool flag = (nint)obj2 < 4;
				bool flag2 = false;
				if (!flag)
				{
					int index = startIndex + 1;
					char c = raw.get_Chars(index);
					object obj3 = c - 47;
					bool flag3 = obj3 == null;
					flag2 = flag3;
				}
				object obj4 = startIndex + 1;
				int num = (int)(obj4 + flag2);
				if (num < raw._stringLength)
				{
					char c2 = raw.get_Chars(num);
					char c3 = char.ToLowerInvariant(c2);
					object obj5;
					if (c3 == 'b')
					{
						obj5 = 1;
					}
					else if (c3 == 'i')
					{
						obj5 = 2;
					}
					else
					{
						if (c3 != 'u')
						{
							goto IL_01f5;
						}
						obj5 = 4;
					}
					object obj6 = (flag2 ? 1 : 0) + 3;
					if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6))
					{
						object obj7 = startIndex - 1;
						int index2 = (int)(obj7 + obj6);
						char c4 = raw.get_Chars(index2);
						if (c4 == '>')
						{
							reference = ref *(flag2 ? ((TagKind*)1) : ((TagKind*)null));
							reference2 = ref *(StyleFlags*)obj5;
							obj = obj6;
							return true;
						}
					}
				}
			}
			goto IL_01f5;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_01f5:
		return false;
	}

	private static string BuildOpenTags(StyleFlags flags)
	{
		//IL_0212: Expected O, but got I
		//IL_0222: Expected O, but got I
		//IL_0027: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		//IL_0098: Expected O, but got I4
		//IL_0100: Expected O, but got I4
		//IL_0176: Expected O, but got I4
		//IL_0085: Expected O, but got I4
		//IL_00ed: Expected O, but got I4
		//IL_01d8: Expected I, but got O
		//IL_01e8: Expected O, but got I
		//IL_01f8: Expected O, but got I
		//IL_015a: Expected O, but got I4
		//IL_0163: Expected O, but got I4
		//IL_01cb: Expected O, but got I4
		if (flags == StyleFlags.None)
		{
			goto IL_0202;
		}
		StringBuilder stringBuilder = new StringBuilder(16);
		object obj = flags & StyleFlags.Bold;
		bool flag = obj == null;
		object obj2 = 0;
		if (!flag)
		{
			if (stringBuilder == null)
			{
				goto IL_0227;
			}
			StringBuilder stringBuilder2 = stringBuilder.Append("<b>");
			obj2 = 0;
		}
		object obj3 = flags & StyleFlags.Italic;
		if (obj3 != null)
		{
			if (stringBuilder == null)
			{
				goto IL_0227;
			}
			StringBuilder stringBuilder3 = stringBuilder.Append("<i>");
			obj2 = 0;
		}
		object obj4 = flags & StyleFlags.Underline;
		object obj5;
		if (obj4 != null)
		{
			if (stringBuilder != null)
			{
				StringBuilder stringBuilder4 = stringBuilder.Append("<u>");
				obj5 = flags & StyleFlags.SmallCaps;
				obj2 = 0;
				goto IL_0193;
			}
		}
		else
		{
			obj5 = flags & StyleFlags.SmallCaps;
			if (stringBuilder != null)
			{
				goto IL_0193;
			}
		}
		goto IL_0227;
		IL_0202:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v2+B8]");
		return (string)0;
		IL_0227:
		return (string)(object)new NullReferenceException();
		IL_0193:
		if (obj5 != null)
		{
			StringBuilder stringBuilder5 = stringBuilder.Append("<smallcaps>");
			obj2 = 0;
		}
		nint num = (nint)stringBuilder;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rdx_v7 (Il2CppClass<System.Text.StringBuilder>)+168]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rdx_v7 (Il2CppClass<System.Text.StringBuilder>)+170]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v66 @ rax_v13 (should have been resolved before IL gen)");
		goto IL_0202;
	}

	private static string BuildCloseTags(StyleFlags flags)
	{
		//IL_0212: Expected O, but got I
		//IL_0222: Expected O, but got I
		//IL_0027: Expected O, but got I4
		//IL_003f: Expected O, but got I4
		//IL_0098: Expected O, but got I4
		//IL_0100: Expected O, but got I4
		//IL_0176: Expected O, but got I4
		//IL_0085: Expected O, but got I4
		//IL_00ed: Expected O, but got I4
		//IL_01d8: Expected I, but got O
		//IL_01e8: Expected O, but got I
		//IL_01f8: Expected O, but got I
		//IL_015a: Expected O, but got I4
		//IL_0163: Expected O, but got I4
		//IL_01cb: Expected O, but got I4
		if (flags == StyleFlags.None)
		{
			goto IL_0202;
		}
		StringBuilder stringBuilder = new StringBuilder(16);
		object obj = flags & StyleFlags.SmallCaps;
		bool flag = obj == null;
		object obj2 = 0;
		if (!flag)
		{
			if (stringBuilder == null)
			{
				goto IL_0227;
			}
			StringBuilder stringBuilder2 = stringBuilder.Append("</smallcaps>");
			obj2 = 0;
		}
		object obj3 = flags & StyleFlags.Underline;
		if (obj3 != null)
		{
			if (stringBuilder == null)
			{
				goto IL_0227;
			}
			StringBuilder stringBuilder3 = stringBuilder.Append("</u>");
			obj2 = 0;
		}
		object obj4 = flags & StyleFlags.Italic;
		object obj5;
		if (obj4 != null)
		{
			if (stringBuilder != null)
			{
				StringBuilder stringBuilder4 = stringBuilder.Append("</i>");
				obj5 = flags & StyleFlags.Bold;
				obj2 = 0;
				goto IL_0193;
			}
		}
		else
		{
			obj5 = flags & StyleFlags.Bold;
			if (stringBuilder != null)
			{
				goto IL_0193;
			}
		}
		goto IL_0227;
		IL_0202:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
		object obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v45 @ rax_v2+B8]");
		return (string)0;
		IL_0227:
		return (string)(object)new NullReferenceException();
		IL_0193:
		if (obj5 != null)
		{
			StringBuilder stringBuilder5 = stringBuilder.Append("</b>");
			obj2 = 0;
		}
		nint num = (nint)stringBuilder;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rdx_v7 (Il2CppClass<System.Text.StringBuilder>)+168]");
		object obj7 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v238 @ rdx_v7 (Il2CppClass<System.Text.StringBuilder>)+170]");
		object obj8 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v66 @ rax_v13 (should have been resolved before IL gen)");
		goto IL_0202;
	}

	private static bool StringMatchAt(string source, int startIndex, string pattern, int patternLen)
	{
		//IL_0174: Expected I4, but got O
		//IL_0047: Expected O, but got I4
		if (startIndex >= 0)
		{
			if (source != null)
			{
				object obj = startIndex + patternLen;
				if ((nint)obj > source._stringLength)
				{
					goto IL_0158;
				}
				bool flag = patternLen <= 0;
				int num = 0;
				if (flag)
				{
					goto IL_014a;
				}
				while (true)
				{
					int index = num + startIndex;
					char c = source.get_Chars(index);
					char c2 = char.ToLowerInvariant(c);
					if (pattern == null)
					{
						break;
					}
					char c3 = pattern.get_Chars(num);
					char c4 = char.ToLowerInvariant(c3);
					if (c2 == c4)
					{
						num++;
						if (num < patternLen)
						{
							continue;
						}
						goto IL_014a;
					}
					goto IL_0158;
				}
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		goto IL_0158;
		IL_014a:
		return true;
		IL_0158:
		return false;
	}

	private unsafe static bool NormalizePlainRange(int plainLen, ref int plainStart, ref int plainEndInclusive)
	{
		//IL_013a: Expected O, but got I4
		//IL_0151: Expected O, but got I4
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Expected O, but got Unknown
		//IL_00cd: Expected O, but got I4
		if (plainLen > 0)
		{
			if (plainStart > plainEndInclusive)
			{
				ref int reference = ref *(int*)plainEndInclusive;
				ref int reference2 = ref *(int*)plainStart;
			}
			if (plainEndInclusive >= 0 && plainStart < plainLen)
			{
				if (plainStart < 0)
				{
					ref int reference = ref *(int*)null;
				}
				if (plainEndInclusive >= plainLen)
				{
					object obj = plainLen - 1;
					ref int reference2 = ref *(int*)obj;
				}
				object obj2 = plainStart - plainEndInclusive;
				object obj3 = plainStart ^ plainEndInclusive;
				object obj4 = plainStart ^ obj2;
				object obj5 = obj3 & obj4;
				bool flag = (nint)obj5 < 0;
				bool flag2 = (nint)obj2 < 0;
				bool flag3 = obj2 == null;
				bool flag4 = flag2 != flag;
				return flag4 | flag3;
			}
		}
		return false;
	}

	private static bool ContainsAnyNewline(string s)
	{
		//IL_00ff: Expected I4, but got O
		if (!string.IsNullOrEmpty(s))
		{
			bool flag = s == null;
			int num = 0;
			int num2 = 0;
			if (flag)
			{
				NullReferenceException ex = new NullReferenceException();
				return (byte)(int)ex != 0;
			}
			while (num2 < s._stringLength)
			{
				char c = s.get_Chars(num);
				if (c != '\n')
				{
					char c2 = s.get_Chars(num);
					if (c2 != '\r')
					{
						num++;
						num2 = num;
						continue;
					}
				}
				return true;
			}
		}
		return false;
	}

	private static bool ContainsAnyClosingTags(string s)
	{
		//IL_00db: Expected I4, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A5DF]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				if (!s.Contains("</u>") && !s.Contains("</i>"))
				{
					bool flag = s.Contains("</b>");
					bool flag2 = !flag;
					return !flag2;
				}
				return true;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private static bool ContainsMatchingOpeningTags(string s, StyleFlags flag)
	{
		//IL_0078: Expected I4, but got O
		if (!string.IsNullOrEmpty(s))
		{
			string value = BuildOpenTags(flag);
			if (s != null)
			{
				bool flag2 = s.Contains(value);
				bool flag3 = !flag2;
				return !flag3;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private static bool EndsWithNewline(string s)
	{
		//IL_00b8: Expected I4, but got O
		//IL_0090: Expected O, but got I4
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				int index = s._stringLength - 1;
				char c = s.get_Chars(index);
				if (c == '\n')
				{
					return true;
				}
				object obj = c - 13;
				return obj == null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private static bool StartsWithNewline(string s)
	{
		//IL_00b8: Expected I4, but got O
		//IL_0090: Expected O, but got I4
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				char c = s.get_Chars(0);
				if (c == '\n')
				{
					return true;
				}
				char c2 = s.get_Chars(0);
				object obj = c2 - 13;
				return obj == null;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return false;
	}

	private static string TrimNewlinesNormalize(string s)
	{
		//IL_012e: Expected O, but got I
		//IL_013e: Expected O, but got I
		//IL_0211: Expected O, but got I4
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected I4, but got Unknown
		//IL_01e0: Expected O, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A5E1]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!string.IsNullOrEmpty(s))
		{
			if (s != null)
			{
				string text = s.Replace("\r\n", "\n");
				if (text != null)
				{
					string text2 = text.Replace('\r', '\n');
					bool flag = (nint)text2 < 0;
					if (text2 != null)
					{
						int num = text2._stringLength - 1;
						int num2 = 0;
						if (!flag)
						{
							do
							{
								char c = text2.get_Chars(num2);
								if (c == '\n')
								{
									num2++;
									continue;
								}
								if (num < num2)
								{
									break;
								}
								do
								{
									char c2 = text2.get_Chars(num);
									if (c2 == '\n')
									{
										num--;
										continue;
									}
									if (num2 > num)
									{
										break;
									}
									if (num2 == 0)
									{
										object obj = text2._stringLength - 1;
										if (num == (nint)obj)
										{
											return text2;
										}
									}
									object obj2 = num - num2;
									int length = obj2 + 1;
									return text2.Substring(num2, length);
								}
								while (num >= num2);
								break;
							}
							while (num2 <= num);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502D0]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rax_v18+B8]");
						return (string)0;
					}
				}
			}
			return (string)(object)new NullReferenceException();
		}
		return s;
	}

	unsafe static TMP_RichTextSelectionUtility()
	{
		//IL_0008: Expected O, but got Ref
		//IL_02c9: Expected O, but got Ref
		//IL_02d7: Expected O, but got Ref
		//IL_02e5: Expected O, but got Ref
		//IL_0028: Expected O, but got Ref
		//IL_0036: Expected O, but got Ref
		//IL_0044: Expected O, but got Ref
		//IL_0080: Expected O, but got Ref
		//IL_008e: Expected O, but got Ref
		//IL_009c: Expected O, but got Ref
		//IL_00d8: Expected O, but got Ref
		//IL_00e6: Expected O, but got Ref
		//IL_00f4: Expected O, but got Ref
		//IL_014c: Expected O, but got Ref
		//IL_015a: Expected O, but got Ref
		//IL_0168: Expected O, but got Ref
		//IL_01a4: Expected O, but got Ref
		//IL_01b2: Expected O, but got Ref
		//IL_01c0: Expected O, but got Ref
		//IL_01fc: Expected O, but got Ref
		//IL_020a: Expected O, but got Ref
		//IL_0218: Expected O, but got Ref
		//IL_0254: Expected O, but got Ref
		//IL_0262: Expected O, but got Ref
		//IL_0270: Expected O, but got Ref
		object obj2 = default(object);
		object obj = (object)(&obj2);
		(string, int, StyleFlags)[] array = new(string, int, StyleFlags)[4];
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
		_ = 8;
		_ = 11;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-29]");
		_ = 0;
		object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
		_ = 1;
		_ = 0;
		_ = 3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-19]");
		_ = 0;
		object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
		_ = 2;
		_ = 0;
		_ = 3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1-9]");
		_ = 0;
		object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj13 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 7));
		_ = 4;
		_ = 0;
		_ = 3;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+7]");
		_ = 0;
		s_OpenTags = array;
		(string, int, StyleFlags)[] array2 = new(string, int, StyleFlags)[4];
		object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj17 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 23));
		_ = 8;
		_ = 12;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+17]");
		_ = 0;
		object obj18 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj19 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj20 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 39));
		_ = 1;
		_ = 0;
		_ = 4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+27]");
		_ = 0;
		object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj23 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 55));
		_ = 2;
		_ = 0;
		_ = 4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+37]");
		_ = 0;
		object obj24 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
		object obj25 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 119));
		object obj26 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 71));
		_ = 4;
		_ = 0;
		_ = 4;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807E5B70");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v4 @ rbp_v1+47]");
		_ = 0;
		s_CloseTags = array2;
	}
}
