using System;
using System.IO;
using System.Linq;
using System.Text;
using Cpp2ILInjected;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

internal static class IOUtils
{
	internal unsafe static string MakeSafeRelativePath(string path)
	{
		//IL_0083: Expected O, but got I4
		//IL_00e7: Expected O, but got I4
		//IL_00f9: Expected O, but got I4
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Expected O, but got Unknown
		//IL_0181: Expected O, but got I4
		//IL_019b: Expected O, but got I4
		//IL_01bc: Expected O, but got I4
		//IL_021e: Expected O, but got I4
		//IL_0373: Expected I, but got O
		//IL_0383: Expected O, but got I
		//IL_03ac: Expected O, but got I4
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Expected O, but got Unknown
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		//IL_02e9: Expected O, but got I4
		if (!string.IsNullOrEmpty(path))
		{
			if (path != null)
			{
				string text = path.Replace('\\', '/');
				if (text != null)
				{
					string text2 = text.Trim('/');
					bool flag = Path.IsPathRooted(text2);
					object obj = 0;
					StringSplitOptions stringSplitOptions = StringSplitOptions.None;
					char[] array = null;
					if (flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentException ex = new ArgumentException("The path cannot be rooted.", "path");
						ex._002Ector("The path cannot be rooted.", "path");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex;
					}
					char[] array2 = new char[1];
					if (array2 != null)
					{
						bool flag2 = array2.Length <= 0;
						obj = 0;
						stringSplitOptions = StringSplitOptions.None;
						array = (char[])1;
						if (flag2)
						{
							goto IL_040c;
						}
						array2[0] = '/';
						if (text2 != null)
						{
							string[] array3 = text2.Split(array2, StringSplitOptions.RemoveEmptyEntries);
							if (array3 != null)
							{
								object obj2 = array3 + 32;
								object obj3 = 0;
								stringSplitOptions = StringSplitOptions.RemoveEmptyEntries;
								array = array2;
								object obj4 = 0;
								char c2 = default(char);
								while (true)
								{
									if ((nint)obj4 < array3.Length)
									{
										bool flag3 = (nint)obj3 >= array3.Length;
										obj = 0;
										if (flag3)
										{
											break;
										}
										string text3 = (string)obj2;
										char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
										if (obj2 != null)
										{
											StringBuilder stringBuilder = new StringBuilder(text3._stringLength);
											object obj5 = 0;
											stringSplitOptions = StringSplitOptions.None;
											int num = 0;
											while (num < text3._stringLength)
											{
												char c = ((string)obj2).get_Chars(num);
												if (Enumerable.Contains(invalidFileNameChars, (char)(ushort)(&c2)))
												{
													bool flag4 = obj5 != null;
													stringSplitOptions = StringSplitOptions.None;
													if (flag4)
													{
														goto IL_0333;
													}
													if (stringBuilder != null)
													{
														StringBuilder stringBuilder2 = stringBuilder.Append('_');
														num++;
														c2 = c;
														obj5 = 1;
														stringSplitOptions = StringSplitOptions.None;
														continue;
													}
												}
												else if (stringBuilder != null)
												{
													StringBuilder stringBuilder3 = stringBuilder.Append(c);
													stringSplitOptions = StringSplitOptions.None;
													goto IL_0333;
												}
												goto IL_0465;
												IL_0333:
												num++;
												c2 = c;
											}
											if (stringBuilder != null)
											{
												nint num2 = (nint)stringBuilder;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v543 @ rdx_v16 (Il2CppClass<System.Text.StringBuilder>)+170]");
												array = (char[])0;
												char[] array4 = (char[])stringBuilder.ToString();
												bool flag5 = (nint)obj3 >= array3.Length;
												obj = 0;
												if (flag5)
												{
													break;
												}
												obj2 = array4;
												obj3++;
												obj2 += 8;
												array = array4;
												obj4 = obj3;
												continue;
											}
										}
										goto IL_0465;
									}
									return string.Join("/", array3);
								}
								goto IL_040c;
							}
						}
					}
				}
			}
			goto IL_0465;
		}
		return null;
		IL_0465:
		return (string)(object)new NullReferenceException();
		IL_040c:
		throw new IndexOutOfRangeException();
	}

	internal unsafe static string MakeSafeFileName(string name)
	{
		//IL_004c: Expected O, but got I4
		//IL_0051: Expected I, but got O
		//IL_0195: Expected I, but got O
		//IL_01a5: Expected O, but got I
		//IL_01b5: Expected O, but got I
		//IL_0150: Expected I, but got O
		//IL_0113: Expected O, but got I4
		//IL_0118: Expected I, but got O
		char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
		if (name != null)
		{
			StringBuilder stringBuilder = new StringBuilder(name._stringLength);
			object obj = 0;
			nint num = unchecked((nint)null);
			int num2 = 0;
			char c2 = default(char);
			while (true)
			{
				if (num2 < name._stringLength)
				{
					char c = name.get_Chars(num2);
					if (Enumerable.Contains(invalidFileNameChars, (char)(ushort)(&c2)))
					{
						bool flag = obj != null;
						num = 0;
						if (!flag)
						{
							if (stringBuilder == null)
							{
								break;
							}
							StringBuilder stringBuilder2 = stringBuilder.Append('_');
							num2++;
							c2 = c;
							obj = 1;
							num = unchecked((nint)null);
							continue;
						}
					}
					else
					{
						if (stringBuilder == null)
						{
							break;
						}
						StringBuilder stringBuilder3 = stringBuilder.Append(c);
						num = unchecked((nint)null);
					}
					num2++;
					c2 = c;
					continue;
				}
				if (stringBuilder != null)
				{
					nint num3 = (nint)stringBuilder;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rdx_v4 (Il2CppClass<System.Text.StringBuilder>)+168]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rdx_v4 (Il2CppClass<System.Text.StringBuilder>)+170]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v161 @ rax_v10 (should have been resolved before IL gen)");
				}
				break;
			}
		}
		return (string)(object)new NullReferenceException();
	}
}
