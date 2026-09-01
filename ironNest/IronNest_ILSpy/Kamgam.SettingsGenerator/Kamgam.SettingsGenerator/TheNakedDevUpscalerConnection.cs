using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using TND.Upscaling.Framework;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class TheNakedDevUpscalerConnection : ConnectionWithOptions<string>
{
	protected List<string> _labels;

	protected List<UpscalerQuality> _labelQualities;

	public bool CheckForCameraMarker = true;

	protected bool _explicitOffValueExists;

	public TNDUpscaler GetUtils()
	{
		Camera currentRenderingCamera = RenderUtils.GetCurrentRenderingCamera(CheckForCameraMarker);
		bool flag = currentRenderingCamera != null;
		bool flag2 = !flag;
		UnityEngine.Object obj = null;
		if (!flag2)
		{
			if ((object)currentRenderingCamera == null)
			{
				goto IL_013f;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			obj = obj2;
		}
		if (obj == null)
		{
			Camera current = Camera.current;
			if (current != null)
			{
				if ((object)current == null)
				{
					goto IL_013f;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				UnityEngine.Object obj3 = default(UnityEngine.Object);
				obj = obj3;
			}
			if (obj == null)
			{
				Logger.LogWarning("TheNakedDevUpscalerConnection: Could not find the TNDUpscaler component on the current camera. Please make sure you have it added (ignoring all upscaler settings for now).");
			}
		}
		return (TNDUpscaler)obj;
		IL_013f:
		return (TNDUpscaler)(object)new NullReferenceException();
	}

	public unsafe override List<string> GetOptionLabels()
	{
		//IL_00b3: Expected O, but got Ref
		//IL_00bb: Expected O, but got Ref
		//IL_0845: Expected O, but got Ref
		//IL_084d: Expected O, but got Ref
		//IL_0131: Expected I, but got O
		//IL_01bc: Expected O, but got I4
		//IL_0307: Expected I, but got O
		//IL_0169: Expected O, but got I
		//IL_078c: Expected O, but got I4
		//IL_033f: Expected O, but got I
		//IL_01d1: Expected I, but got O
		//IL_01e1: Expected O, but got I
		//IL_0278: Expected O, but got I4
		//IL_028e: Expected O, but got I
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Expected O, but got Unknown
		//IL_085f: Expected O, but got I4
		//IL_05bf: Expected O, but got I4
		//IL_05d5: Expected O, but got I
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Expected O, but got Unknown
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Expected O, but got Unknown
		//IL_0232: Expected O, but got I4
		//IL_03c0: Expected I, but got O
		//IL_044b: Expected O, but got I4
		//IL_03f8: Expected O, but got I
		//IL_0896: Expected O, but got I4
		//IL_0602: Expected O, but got I4
		//IL_0618: Expected O, but got I
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0634: Expected O, but got Unknown
		//IL_063c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Expected O, but got Unknown
		//IL_0540: Expected I, but got O
		//IL_0548: Expected I, but got O
		if (_labels == null)
		{
			List<string> labels = new List<string>();
			_labels = labels;
			List<UpscalerQuality> labelQualities = new List<UpscalerQuality>();
			_labelQualities = labelQualities;
			Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UpscalerQuality));
			Array values = Enum.GetValues(typeFromHandle);
			_explicitOffValueExists = false;
			bool flag = values == null;
			Type type = typeFromHandle;
			Type type2 = typeFromHandle;
			if (flag)
			{
				goto IL_06e3;
			}
			IEnumerator enumerator = values.GetEnumerator();
			Type type3 = default(Type);
			object obj = (object)(&type3);
			bool flag2 = default(bool);
			object obj2 = (object)(&flag2);
			type = typeFromHandle;
			Array array = values;
			object obj3 = default(object);
			object obj10 = default(object);
			Type type5 = default(Type);
			while (true)
			{
				bool flag3 = (object)type3 == null;
				Type type4 = type3;
				object obj9;
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					if (obj3 == null)
					{
						break;
					}
					bool flag4 = (object)type3 == null;
					type4 = type3;
					type = type3;
					array = null;
					if (!flag4)
					{
						nint num = (nint)type3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r10_v18 (Il2CppClass<System.Type>)+12E]");
						if ((nint)0 >= (nint)0)
						{
							goto IL_01a9;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r10_v18 (Il2CppClass<System.Type>)+B0]");
						type4 = (Type)0;
						bool flag5 = false;
						while (true)
						{
							object obj4 = (flag5 ? 1 : 0) + (flag5 ? 1 : 0);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v446 @ r8_v11 (System.Type)+v644 @ rax_v89*8]");
							if (0 == (nint)typeof(IEnumerator))
							{
								break;
							}
							flag5 = (byte)((flag5 ? 1u : 0u) + 1u) != 0;
							bool num2 = flag5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v323 @ r10_v18 (Il2CppClass<System.Type>)+12E]");
							if ((nint)(num2 ? 1 : 0) < (nint)0)
							{
								continue;
							}
							goto IL_01a9;
						}
						object obj5 = (flag5 ? 1 : 0) + (flag5 ? 1 : 0);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v446 @ r8_v11 (System.Type)+8+v794 @ rcx_v76*8]");
						object obj6 = (nint)0 + (nint)1;
						object obj7 = obj6 << 4;
						object obj8 = obj7 + 312;
						obj9 = obj8 + num;
						goto IL_07f9;
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
				IL_01a9:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				type4 = (Type)1;
				obj9 = obj10;
				goto IL_07f9;
				IL_07f9:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v801 @ rdx_v60] (should have been resolved before IL gen)");
				bool flag6 = (object)type5 == null;
				type = type3;
				type2 = type3;
				if (!flag6)
				{
					nint num3 = (nint)type5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v858 @ rdx_v62 (Il2CppClass<System.Type>)+168]");
					type4 = (Type)0;
					string text = type5.ToString();
					bool flag7 = text == null;
					type = type3;
					type2 = type5;
					if (!flag7)
					{
						string text2 = text.ToLower();
						type = (Type)_explicitOffValueExists;
						bool flag8 = text2 == "off";
						bool explicitOffValueExists = !flag7;
						_explicitOffValueExists = explicitOffValueExists;
						array = (Array)(object)text2;
						continue;
					}
					throw new NullReferenceException();
				}
				array = (Array)(object)type2;
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj11 = default(object);
			obj2 = obj11;
			if (obj11 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			}
			IEnumerator enumerator2 = values.GetEnumerator();
			object obj12 = (object)(&type3);
			object obj13 = (object)(&flag2);
			object obj19 = default(object);
			object obj22 = default(object);
			object obj23 = default(object);
			Type type6 = default(Type);
			object obj29 = default(object);
			while (true)
			{
				Type type4;
				object obj18;
				if ((object)type3 != null)
				{
					nint num4 = (nint)type3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r10_v16 (Il2CppClass<System.Type>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_037f;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r10_v16 (Il2CppClass<System.Type>)+B0]");
					type4 = (Type)0;
					bool flag9 = false;
					while (true)
					{
						object obj14 = (flag9 ? 1 : 0) + (flag9 ? 1 : 0);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v446 @ r8_v11 (System.Type)+v973 @ rax_v75*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						flag9 = (byte)((flag9 ? 1u : 0u) + 1u) != 0;
						bool num5 = flag9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ r10_v16 (Il2CppClass<System.Type>)+12E]");
						if ((nint)(num5 ? 1 : 0) < (nint)0)
						{
							continue;
						}
						goto IL_037f;
					}
					object obj15 = (flag9 ? 1 : 0) + (flag9 ? 1 : 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v446 @ r8_v11 (System.Type)+8+v1030 @ rcx_v63*8]");
					object obj16 = (nint)0 << 4;
					object obj17 = obj16 + 312;
					obj18 = obj17 + num4;
					goto IL_08e8;
				}
				throw new NullReferenceException();
				IL_037f:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				type4 = null;
				obj18 = obj19;
				goto IL_08e8;
				IL_0438:
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
				object obj20 = 1;
				object obj21 = obj22;
				goto IL_090f;
				IL_08e8:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1035 @ rdx_v29] (should have been resolved before IL gen)");
				if (obj23 == null)
				{
					break;
				}
				if ((object)type3 != null)
				{
					nint num6 = (nint)type3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v528 @ r10_v17 (Il2CppClass<System.Type>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_0438;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v528 @ r10_v17 (Il2CppClass<System.Type>)+B0]");
					obj20 = 0;
					bool flag10 = false;
					while (true)
					{
						object obj24 = (flag10 ? 1 : 0) + (flag10 ? 1 : 0);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v818 @ r8_v22+v1088 @ rax_v70*8]");
						if (0 == (nint)typeof(IEnumerator))
						{
							break;
						}
						flag10 = (byte)((flag10 ? 1u : 0u) + 1u) != 0;
						bool num7 = flag10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v528 @ r10_v17 (Il2CppClass<System.Type>)+12E]");
						if ((nint)(num7 ? 1 : 0) < (nint)0)
						{
							continue;
						}
						goto IL_0438;
					}
					object obj25 = (flag10 ? 1 : 0) + (flag10 ? 1 : 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v818 @ r8_v22+8+v1148 @ rcx_v55*8]");
					object obj26 = (nint)0 + (nint)1;
					object obj27 = obj26 << 4;
					object obj28 = obj27 + 312;
					obj21 = obj28 + num6;
					goto IL_090f;
				}
				throw new NullReferenceException();
				IL_090f:
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1155 @ rdx_v36] (should have been resolved before IL gen)");
				if ((object)type6 != null)
				{
					string text3 = type6.ToString();
					if (_explicitOffValueExists)
					{
						bool flag11 = text3 == null;
						type = type6;
						array = (Array)(object)type6;
						if (flag11)
						{
							throw new NullReferenceException();
						}
						string text4 = text3.ToLower();
						if (text4 == "custom")
						{
							continue;
						}
					}
					if (_labels != null)
					{
						_labels.Add(text3);
						if (_labelQualities != null)
						{
							nint num8 = (nint)typeof(UpscalerQuality);
							nint num9 = (nint)type6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v610 @ rdx_v42 (Il2CppClass<System.Type>)+40]");
							nint num10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v615 @ rcx_v46 (Il2CppClass<TND.Upscaling.Framework.UpscalerQuality>)+40]");
							bool flag12 = num10 != 0;
							type = type6;
							array = (Array)(object)typeof(UpscalerQuality);
							if (!flag12)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
								_labelQualities.Add((UpscalerQuality)(int)(&obj29));
								continue;
							}
							((List<string>)(object)type).Add((string)(object)array);
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			Type type7 = default(Type);
			obj13 = type7;
			if ((object)type7 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				Type type4 = type7;
			}
			if (!_explicitOffValueExists)
			{
				bool flag13 = _labels == null;
				type = type3;
				type2 = (Type)(object)_labels;
				if (flag13)
				{
					goto IL_06e3;
				}
				_labels.set_Item(0, "Off");
			}
		}
		return _labels;
		IL_06e3:
		throw new NullReferenceException();
	}

	public override void SetOptionLabels(List<string> optionLabels)
	{
		if (optionLabels != null)
		{
			_labels = optionLabels;
		}
		else
		{
			Debug.LogError("Invalid new labels. Need to be four.");
		}
	}

	public override void RefreshOptionLabels()
	{
		//IL_000c: Expected I, but got O
		//IL_001c: Expected O, but got I
		//IL_002c: Expected O, but got I
		_labels = null;
		nint num = (nint)this;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.TheNakedDevUpscalerConnection>)+2C8]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rdx_v2 (Il2CppClass<Kamgam.SettingsGenerator.TheNakedDevUpscalerConnection>)+2D0]");
		object obj2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v10 @ rax_v2 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	public unsafe override int Get()
	{
		//IL_0076: Expected I4, but got O
		List<string> optionLabels = GetOptionLabels();
		TNDUpscaler utils = GetUtils();
		if (!(utils != null))
		{
			return 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18067E580");
		object obj = default(object);
		if (_labelQualities != null)
		{
			return _labelQualities.IndexOf((UpscalerQuality)(int)(&obj));
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public unsafe static T GetFieldValue<T>(object obj, string fieldName)
	{
		//IL_0008: Expected O, but got Ref
		//IL_007c: Expected O, but got I
		//IL_009a: Expected O, but got I
		//IL_026f: Expected O, but got I
		//IL_00c6: Expected O, but got I8
		//IL_02b3: Expected O, but got I
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Expected O, but got Unknown
		//IL_0301: Expected O, but got I
		//IL_022c: Expected O, but got Ref
		//IL_023c: Expected O, but got I
		//IL_016d: Expected O, but got I
		//IL_01a9: Expected O, but got I
		//IL_01e1: Expected O, but got I
		//IL_01f1: Expected O, but got I
		object obj3 = default(object);
		object obj2 = (object)(&obj3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
		object obj4 = 0;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
		object obj9;
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			object obj8 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj8 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			_ = ref obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			obj9 = (nint)0 + (nint)15;
			object obj10 = obj9;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
			if ((nint)obj10 > 0)
			{
				goto IL_0325;
			}
		}
		obj9 = 1152921504606846960L;
		goto IL_0325;
		IL_0241:
		return (T)new NullReferenceException();
		IL_0364:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
		IL_0325:
		object obj11 = obj9 & -16;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		if (obj != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			object obj12 = default(object);
			if (obj12 != null)
			{
				object obj13 = obj12;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v230 @ r9_v2+6B8] (should have been resolved before IL gen)");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
				object obj14 = default(object);
				object obj20;
				object obj21;
				if (obj14 == null)
				{
					object obj15 = default(object);
					if (obj15 == null)
					{
						goto IL_0241;
					}
					object obj16 = obj15;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v307 @ r8_v12+2C8] (should have been resolved before IL gen)");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj18 = default(object);
					if (obj18 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ r9+38]");
						object obj19 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v14 @ rbp_v1+58]");
						obj20 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
						obj21 = 0;
						goto IL_0364;
					}
				}
				else
				{
					Logger.LogError("TheNakedDev UpscalerController.qualityMode was not found. Maybe the internal API changed. Please contact TheNakedDev for support.");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
				obj20 = (object)(&obj3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rcx_v2+FC]");
				obj21 = 0;
				goto IL_0364;
			}
		}
		goto IL_0241;
	}

	public override void Set(int index)
	{
		//IL_00a6: Expected I, but got O
		//IL_00b6: Expected O, but got I
		//IL_00c6: Expected O, but got I
		UpscalerQuality quality = default(UpscalerQuality);
		while (true)
		{
			List<string> optionLabels = GetOptionLabels();
			TNDUpscaler utils = GetUtils();
			if (utils != null)
			{
				if (!_explicitOffValueExists)
				{
					bool flag = index == 0;
					bool enabled = !flag;
					utils.enabled = enabled;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				utils.SetQuality(quality);
				nint num = 0;
			}
			nint num2 = (nint)this;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v3 (Il2CppClass<Kamgam.SettingsGenerator.TheNakedDevUpscalerConnection>)+258]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ r8_v3 (Il2CppClass<Kamgam.SettingsGenerator.TheNakedDevUpscalerConnection>)+260]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v119 @ rax_v8 (should have been resolved before IL gen)");
		}
	}
}
