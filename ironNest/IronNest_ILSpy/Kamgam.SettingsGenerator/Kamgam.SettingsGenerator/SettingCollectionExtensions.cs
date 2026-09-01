using System;
using System.Collections.Generic;
using Cpp2ILInjected;

namespace Kamgam.SettingsGenerator;

public static class SettingCollectionExtensions
{
	public unsafe static IList<ISetting> PullFromConnection(IList<ISetting> settings)
	{
		//IL_0017: Expected O, but got Ref
		//IL_0072: Expected I, but got O
		//IL_0105: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_00b3: Expected O, but got I4
		//IL_0112: Expected I, but got O
		//IL_01cd: Expected O, but got I
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01a5: Expected O, but got I4
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_014a: Expected O, but got I
		//IL_0153: Expected O, but got I4
		//IL_0210: Expected O, but got I
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		IList<ISetting> list = default(IList<ISetting>);
		object obj = (object)(&list);
		IList<ISetting> list2 = null;
		object obj2 = default(object);
		object obj12 = default(object);
		object obj14 = default(object);
		IList<ISetting> list3 = default(IList<ISetting>);
		while (true)
		{
			object obj10;
			object obj3;
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = list == null;
					list2 = null;
					if (!flag)
					{
						nint num = (nint)list;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r10_v5 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_00ea;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r10_v5 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ r8_v9+v288 @ rax_v31*8]");
							if (0 == (nint)typeof(IEnumerator<ISetting>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r10_v5 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_00ea;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ r8_v9+8+v344 @ rcx_v28*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_0325;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				break;
			}
			throw new NullReferenceException();
			IL_018a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			object obj11 = obj12;
			object obj13 = 30;
			goto IL_034c;
			IL_00ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj14;
			obj3 = 0;
			goto IL_0325;
			IL_034c:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v442 @ rdx_v13] (should have been resolved before IL gen)");
			continue;
			IL_0325:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v349 @ rdx_v10] (should have been resolved before IL gen)");
			if (list3 != null)
			{
				nint num2 = (nint)list3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
					obj13 = 0;
					object obj15 = 0;
					while (true)
					{
						object obj16 = obj15 + obj15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v10+v379 @ rax_v26*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						obj15++;
						object obj17 = obj15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
						if ((nint)obj17 < 0)
						{
							continue;
						}
						goto IL_018a;
					}
					object obj18 = obj15 + obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v10+8+v435 @ rcx_v20*8]");
					object obj19 = (nint)0 + (nint)30;
					object obj20 = obj19 << 4;
					object obj21 = obj20 + 312;
					obj11 = obj21 + num2;
					goto IL_034c;
				}
				goto IL_018a;
			}
			throw new NullReferenceException();
		}
		IList<ISetting> result = default(IList<ISetting>);
		return result;
	}

	public unsafe static IList<ISetting> PushToConnection(IList<ISetting> settings)
	{
		//IL_0017: Expected O, but got Ref
		//IL_0072: Expected I, but got O
		//IL_0105: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_00b3: Expected O, but got I4
		//IL_0112: Expected I, but got O
		//IL_01cd: Expected O, but got I
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Expected O, but got Unknown
		//IL_01a5: Expected O, but got I4
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_014a: Expected O, but got I
		//IL_0153: Expected O, but got I4
		//IL_0210: Expected O, but got I
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Expected O, but got Unknown
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		IList<ISetting> list = default(IList<ISetting>);
		object obj = (object)(&list);
		IList<ISetting> list2 = null;
		object obj2 = default(object);
		object obj12 = default(object);
		object obj14 = default(object);
		IList<ISetting> list3 = default(IList<ISetting>);
		while (true)
		{
			object obj10;
			object obj3;
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				if (obj2 != null)
				{
					bool flag = list == null;
					list2 = null;
					if (!flag)
					{
						nint num = (nint)list;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r10_v5 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_00ea;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r10_v5 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
						obj3 = 0;
						object obj4 = 0;
						while (true)
						{
							object obj5 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ r8_v9+v288 @ rax_v31*8]");
							if (0 == (nint)typeof(IEnumerator<ISetting>))
							{
								break;
							}
							obj4++;
							object obj6 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ r10_v5 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
							if ((nint)obj6 < 0)
							{
								continue;
							}
							goto IL_00ea;
						}
						object obj7 = obj4 + obj4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v78 @ r8_v9+8+v344 @ rcx_v28*8]");
						object obj8 = (nint)0 << 4;
						object obj9 = obj8 + 312;
						obj10 = obj9 + num;
						goto IL_0325;
					}
					throw new NullReferenceException();
				}
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				break;
			}
			throw new NullReferenceException();
			IL_018a:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			object obj11 = obj12;
			object obj13 = 30;
			goto IL_034c;
			IL_00ea:
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
			obj10 = obj14;
			obj3 = 0;
			goto IL_0325;
			IL_034c:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v442 @ rdx_v13] (should have been resolved before IL gen)");
			continue;
			IL_0325:
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v349 @ rdx_v10] (should have been resolved before IL gen)");
			if (list3 != null)
			{
				nint num2 = (nint)list3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
				if ((nint)0 < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+B0]");
					obj13 = 0;
					object obj15 = 0;
					while (true)
					{
						object obj16 = obj15 + obj15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v10+v379 @ rax_v26*8]");
						if (0 == (nint)typeof(ISetting))
						{
							break;
						}
						obj15++;
						object obj17 = obj15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v109 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<Kamgam.SettingsGenerator.ISetting>>)+12E]");
						if ((nint)obj17 < 0)
						{
							continue;
						}
						goto IL_018a;
					}
					object obj18 = obj15 + obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v10+8+v435 @ rcx_v20*8]");
					object obj19 = (nint)0 + (nint)30;
					object obj20 = obj19 << 4;
					object obj21 = obj20 + 312;
					obj11 = obj21 + num2;
					goto IL_034c;
				}
				goto IL_018a;
			}
			throw new NullReferenceException();
		}
		IList<ISetting> result = default(IList<ISetting>);
		return result;
	}

	public unsafe static IList<ISetting> RefreshRegisteredResolvers(IList<ISetting> settings, Settings settingsObj)
	{
		//IL_0017: Expected O, but got Ref
		//IL_0072: Expected I, but got O
		//IL_0105: Expected O, but got I4
		//IL_00aa: Expected O, but got I
		//IL_00b3: Expected O, but got I4
		//IL_0112: Expected I, but got O
		//IL_01df: Expected O, but got I
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Expected O, but got Unknown
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_01a5: Expected O, but got I4
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Expected O, but got Unknown
		//IL_014a: Expected O, but got I
		//IL_0153: Expected O, but got I4
		//IL_0222: Expected O, but got I
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Expected O, but got Unknown
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Expected O, but got Unknown
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		if (settings != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			Settings settings2 = default(Settings);
			object obj = (object)(&settings2);
			Settings settings3 = null;
			object obj2 = default(object);
			object obj12 = default(object);
			object obj14 = default(object);
			string id = default(string);
			Settings settings4 = default(Settings);
			while (true)
			{
				object obj10;
				object obj3;
				if ((object)settings2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj2 != null)
					{
						bool flag = (object)settings2 == null;
						settings3 = null;
						if (!flag)
						{
							nint num = (nint)settings2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r10_v5 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
							if ((nint)0 >= (nint)0)
							{
								goto IL_00ea;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r10_v5 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+B0]");
							obj3 = 0;
							object obj4 = 0;
							while (true)
							{
								object obj5 = obj4 + obj4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v348 @ r8_v9+v286 @ rax_v34*8]");
								if (0 == (nint)typeof(IEnumerator<ISetting>))
								{
									break;
								}
								obj4++;
								object obj6 = obj4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v246 @ r10_v5 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
								if ((nint)obj6 < 0)
								{
									continue;
								}
								goto IL_00ea;
							}
							object obj7 = obj4 + obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v348 @ r8_v9+8+v342 @ rcx_v30*8]");
							object obj8 = (nint)0 << 4;
							object obj9 = obj8 + 312;
							obj10 = obj9 + num;
							goto IL_035e;
						}
						throw new NullReferenceException();
					}
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
					break;
				}
				throw new NullReferenceException();
				IL_018a:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				object obj11 = obj12;
				object obj13 = 4;
				goto IL_0385;
				IL_00ea:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				obj10 = obj14;
				obj3 = 0;
				goto IL_035e;
				IL_0385:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v460 @ rdx_v14] (should have been resolved before IL gen)");
				settingsObj.RefreshRegisteredResolvers(id);
				continue;
				IL_035e:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v347 @ rdx_v10] (should have been resolved before IL gen)");
				if ((object)settings4 != null)
				{
					nint num2 = (nint)settings4;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r10_v7 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
					if ((nint)0 < (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r10_v7 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+B0]");
						obj13 = 0;
						object obj15 = 0;
						while (true)
						{
							object obj16 = obj15 + obj15;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r8_v11+v397 @ rax_v29*8]");
							if (0 == (nint)typeof(ISetting))
							{
								break;
							}
							obj15++;
							object obj17 = obj15;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v115 @ r10_v7 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
							if ((nint)obj17 < 0)
							{
								continue;
							}
							goto IL_018a;
						}
						object obj18 = obj15 + obj15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v390 @ r8_v11+8+v453 @ rcx_v22*8]");
						object obj19 = (nint)0 + (nint)4;
						object obj20 = obj19 << 4;
						object obj21 = obj20 + 312;
						obj11 = obj21 + num2;
						goto IL_0385;
					}
					goto IL_018a;
				}
				throw new NullReferenceException();
			}
			return settings;
		}
		return null;
	}

	public unsafe static IList<ISetting> RefreshRegisteredResolvers(IList<ISetting> settings, SettingsProvider provider)
	{
		//IL_0065: Expected O, but got Ref
		//IL_0153: Expected O, but got I4
		//IL_00f8: Expected O, but got I
		//IL_0101: Expected O, but got I4
		//IL_0160: Expected I, but got O
		//IL_022d: Expected O, but got I
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Expected O, but got Unknown
		//IL_01f3: Expected O, but got I4
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0198: Expected O, but got I
		//IL_01a1: Expected O, but got I4
		//IL_0270: Expected O, but got I
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Expected O, but got Unknown
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Expected O, but got Unknown
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Expected O, but got Unknown
		if (settings != null)
		{
			if ((object)provider != null)
			{
				if (provider._settings != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj2 = default(object);
					object obj = (object)(&obj2);
					Settings settings2 = null;
					object obj3 = default(object);
					object obj14 = default(object);
					object obj16 = default(object);
					Settings settings4 = default(Settings);
					string id = default(string);
					while (true)
					{
						object obj12;
						object obj5;
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							if (obj3 == null)
							{
								break;
							}
							bool flag = obj2 == null;
							settings2 = null;
							if (!flag)
							{
								object obj4 = obj2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v6+12E]");
								if ((nint)0 >= (nint)0)
								{
									goto IL_0138;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v6+B0]");
								obj5 = 0;
								object obj6 = 0;
								while (true)
								{
									object obj7 = obj6 + obj6;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ r8_v11+v379 @ rax_v40*8]");
									if (0 == (nint)typeof(IEnumerator<ISetting>))
									{
										break;
									}
									obj6++;
									object obj8 = obj6;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r10_v6+12E]");
									if ((nint)obj8 < 0)
									{
										continue;
									}
									goto IL_0138;
								}
								object obj9 = obj6 + obj6;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v441 @ r8_v11+8+v435 @ rcx_v35*8]");
								object obj10 = (nint)0 << 4;
								object obj11 = obj10 + 312;
								obj12 = obj11 + obj4;
								goto IL_03bd;
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
						IL_01d8:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						object obj13 = obj14;
						object obj15 = 4;
						goto IL_03f1;
						IL_0138:
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
						obj12 = obj16;
						obj5 = 0;
						goto IL_03bd;
						IL_03bd:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v440 @ rdx_v12] (should have been resolved before IL gen)");
						Settings settings3 = provider.Settings;
						if ((object)settings4 != null)
						{
							nint num = (nint)settings4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ r10_v8 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
							if ((nint)0 < (nint)0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ r10_v8 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+B0]");
								obj15 = 0;
								object obj17 = 0;
								while (true)
								{
									object obj18 = obj17 + obj17;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r8_v13+v493 @ rax_v35*8]");
									if (0 == (nint)typeof(ISetting))
									{
										break;
									}
									obj17++;
									object obj19 = obj17;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v251 @ r10_v8 (Il2CppClass<Kamgam.SettingsGenerator.Settings>)+12E]");
									if ((nint)obj19 < 0)
									{
										continue;
									}
									goto IL_01d8;
								}
								object obj20 = obj17 + obj17;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r8_v13+8+v549 @ rcx_v27*8]");
								object obj21 = (nint)0 + (nint)4;
								object obj22 = obj21 << 4;
								object obj23 = obj22 + 312;
								obj13 = obj23 + num;
								goto IL_03f1;
							}
							goto IL_01d8;
						}
						throw new NullReferenceException();
						IL_03f1:
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v556 @ rdx_v17] (should have been resolved before IL gen)");
						settings3.RefreshRegisteredResolvers(id);
					}
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					}
				}
				return settings;
			}
			return (IList<ISetting>)new NullReferenceException();
		}
		return null;
	}
}
