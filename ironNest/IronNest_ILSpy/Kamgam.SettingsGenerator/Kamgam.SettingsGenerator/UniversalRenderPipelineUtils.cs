using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Kamgam.SettingsGenerator;

public static class UniversalRenderPipelineUtils
{
	private static FieldInfo MainLightCastShadows_FieldInfo;

	private static FieldInfo AdditionalLightCastShadows_FieldInfo;

	private static FieldInfo MainLightShadowmapResolution_FieldInfo;

	private static FieldInfo AdditionalLightShadowmapResolution_FieldInfo;

	private static FieldInfo Cascade2Split_FieldInfo;

	private static FieldInfo Cascade4Split_FieldInfo;

	private static FieldInfo SoftShadowsEnabled_FieldInfo;

	private static FieldInfo RenderDataList_FieldInfo;

	static UniversalRenderPipelineUtils()
	{
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(UniversalRenderPipelineAsset));
		FieldInfo field = typeFromHandle.GetField("m_MainLightShadowsSupported", (BindingFlags)36);
		MainLightCastShadows_FieldInfo = field;
		FieldInfo field2 = typeFromHandle.GetField("m_AdditionalLightShadowsSupported", (BindingFlags)36);
		AdditionalLightCastShadows_FieldInfo = field2;
		FieldInfo field3 = typeFromHandle.GetField("m_MainLightShadowmapResolution", (BindingFlags)36);
		MainLightShadowmapResolution_FieldInfo = field3;
		FieldInfo field4 = typeFromHandle.GetField("m_AdditionalLightsShadowmapResolution", (BindingFlags)36);
		AdditionalLightShadowmapResolution_FieldInfo = field4;
		FieldInfo field5 = typeFromHandle.GetField("m_Cascade2Split", (BindingFlags)36);
		Cascade2Split_FieldInfo = field5;
		FieldInfo field6 = typeFromHandle.GetField("m_Cascade4Split", (BindingFlags)36);
		Cascade4Split_FieldInfo = field6;
		FieldInfo field7 = typeFromHandle.GetField("m_SoftShadowsSupported", (BindingFlags)36);
		SoftShadowsEnabled_FieldInfo = field7;
		FieldInfo field8 = typeFromHandle.GetField("m_RendererDataList", (BindingFlags)36);
		RenderDataList_FieldInfo = field8;
	}

	public unsafe static void SetMainLightCastShadows(bool value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_0127: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				object obj3 = default(object);
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v22+FFFFFFF8+v154 @ rax_v21*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_0157;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_0157;
		IL_0157:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			MainLightCastShadows_FieldInfo.SetValue(obj, value2);
		}
	}

	public unsafe static void SetAdditionalLightCastShadows(bool value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_0127: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				object obj3 = default(object);
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v22+FFFFFFF8+v154 @ rax_v21*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_0157;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_0157;
		IL_0157:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			AdditionalLightCastShadows_FieldInfo.SetValue(obj, value2);
		}
	}

	public unsafe static void SetMainLightShadowResolution(int value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_0127: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				object obj3 = default(object);
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v22+FFFFFFF8+v154 @ rax_v21*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_0157;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_0157;
		IL_0157:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			MainLightShadowmapResolution_FieldInfo.SetValue(obj, value2);
		}
	}

	public static int GetMainLightShadowResolution(UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_023f: Expected I, but got O
		//IL_0244: Expected I, but got O
		//IL_008b: Expected I, but got O
		//IL_009b: Expected O, but got I
		//IL_00c7: Expected I, but got O
		//IL_017b: Expected I, but got O
		//IL_00ed: Expected O, but got I
		//IL_011a: Expected I, but got O
		//IL_0191: Expected I, but got O
		//IL_01d7: Expected I4, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
		nint num2;
		nint num4;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			universalRenderPipelineAsset = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rdx_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r8_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rdx_v10 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				num4 = (nint)typeof(UniversalRenderPipelineAsset);
				universalRenderPipelineAsset = (UniversalRenderPipelineAsset)currentRenderPipeline;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r8_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v27+FFFFFFF8+v152 @ rax_v26*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					universalRenderPipelineAsset = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						universalRenderPipelineAsset = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_01e8;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				UniversalRenderPipelineAsset universalRenderPipelineAsset2 = universalRenderPipelineAsset;
				goto IL_0252;
			}
		}
		goto IL_01e8;
		IL_01e8:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj3 = default(object);
		if (obj3 == null)
		{
			return 0;
		}
		bool flag6 = (object)MainLightShadowmapResolution_FieldInfo == null;
		num4 = unchecked((nint)null);
		num2 = unchecked((nint)null);
		if (!flag6)
		{
			UniversalRenderPipelineAsset value = (UniversalRenderPipelineAsset)MainLightShadowmapResolution_FieldInfo.GetValue(universalRenderPipelineAsset);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
			num2 = 0;
			bool flag7 = (object)value == null;
			num4 = (nint)universalRenderPipelineAsset;
			if (!flag7)
			{
				nint num5 = (nint)value;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rdx_v9 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+40]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v216 @ r8_v2 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+40]");
				bool flag8 = num6 != 0;
				UniversalRenderPipelineAsset universalRenderPipelineAsset2 = value;
				if (!flag8)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_object_unbox\"");
					object obj4 = default(object);
					return (int)obj4;
				}
				goto IL_0252;
			}
		}
		throw new NullReferenceException();
		IL_0252:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		int result = default(int);
		return result;
	}

	public unsafe static void SetAdditionalLightShadowResolution(int value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_0127: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				object obj3 = default(object);
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v22+FFFFFFF8+v154 @ rax_v21*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_0157;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_0157;
		IL_0157:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			AdditionalLightShadowmapResolution_FieldInfo.SetValue(obj, value2);
		}
	}

	public unsafe static void SetCascade2Split(float value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_0127: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				object obj3 = default(object);
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v22+FFFFFFF8+v154 @ rax_v21*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_0157;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_0157;
		IL_0157:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			Cascade2Split_FieldInfo.SetValue(obj, value2);
		}
	}

	public unsafe static void SetCascade4Split(Vector3 value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_012f: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		object obj3 = default(object);
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v81 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v23+FFFFFFF8+v156 @ rax_v22*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					float num7 = default(float);
					float num6 = num7;
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_015f;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_015f;
		IL_015f:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			float num6 = value.x;
			object value2 = (Vector3)obj3;
			Cascade4Split_FieldInfo.SetValue(obj, value2);
		}
	}

	public unsafe static void SetSoftShadowsEnabled(bool value, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_00a1: Expected I, but got O
		//IL_00b1: Expected O, but got I
		//IL_00dc: Expected I, but got O
		//IL_00fa: Expected O, but got I
		//IL_0127: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset obj = asset;
		if (!flag2)
		{
			RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
			nint num = (nint)typeof(UniversalRenderPipelineAsset);
			bool flag3 = (object)currentRenderPipeline != null;
			obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (flag3)
			{
				nint num2 = (nint)currentRenderPipeline;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v77 @ rdx_v8 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
				bool flag4 = num3 < 0;
				object obj3 = default(object);
				nint num4 = (nint)(&obj3);
				nint num5 = unchecked((nint)null);
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ r8_v7 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rax_v22+FFFFFFF8+v154 @ rax_v21*8]");
					bool flag5 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
					num4 = (nint)typeof(UniversalRenderPipelineAsset);
					num5 = num2;
					obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
					if (!flag5)
					{
						obj = (UniversalRenderPipelineAsset)currentRenderPipeline;
						goto IL_0157;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		goto IL_0157;
		IL_0157:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj5 = default(object);
		if (obj5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object value2 = default(object);
			SoftShadowsEnabled_FieldInfo.SetValue(obj, value2);
		}
	}

	public static ScriptableRendererData[] GetRendererDataList(UniversalRenderPipelineAsset asset = null)
	{
		//IL_0050: Expected I, but got O
		//IL_0087: Expected I, but got O
		//IL_0097: Expected O, but got I
		//IL_00d3: Expected O, but got I
		//IL_0140: Expected I, but got O
		bool flag = asset == null;
		bool flag2 = !flag;
		UniversalRenderPipelineAsset universalRenderPipelineAsset = asset;
		if (flag2)
		{
			goto IL_0108;
		}
		RenderPipelineAsset currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
		nint num = (nint)typeof(UniversalRenderPipelineAsset);
		UniversalRenderPipelineAsset universalRenderPipelineAsset2;
		if ((object)currentRenderPipeline == null)
		{
			universalRenderPipelineAsset2 = null;
			universalRenderPipelineAsset = null;
			goto IL_0112;
		}
		nint num2 = (nint)currentRenderPipeline;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rdx_v13 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v9 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+130]");
		nint num3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rdx_v13 (Il2CppClass<UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset>)+130]");
		if (num3 >= 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v221 @ r8_v9 (Il2CppClass<UnityEngine.Rendering.RenderPipelineAsset>)+C8]");
			object obj2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ rax_v32+FFFFFFF8+v164 @ rax_v29*8]");
			bool flag3 = 0 != (nint)typeof(UniversalRenderPipelineAsset);
			universalRenderPipelineAsset = (UniversalRenderPipelineAsset)currentRenderPipeline;
			if (!flag3)
			{
				goto IL_0108;
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		UniversalRenderPipelineAsset universalRenderPipelineAsset3 = (UniversalRenderPipelineAsset)currentRenderPipeline;
		goto IL_01f4;
		IL_0108:
		universalRenderPipelineAsset2 = null;
		goto IL_0112;
		IL_0112:
		ScriptableRendererData[] result;
		if (!(universalRenderPipelineAsset != null))
		{
			result = (ScriptableRendererData[])(object)universalRenderPipelineAsset2;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D12FD0");
			object obj3 = default(object);
			if (obj3 != null)
			{
				result = (ScriptableRendererData[])(object)universalRenderPipelineAsset2;
			}
			else
			{
				FieldInfo renderDataList_FieldInfo = RenderDataList_FieldInfo;
				if ((object)RenderDataList_FieldInfo == null)
				{
					goto IL_0203;
				}
				nint num4 = (nint)renderDataList_FieldInfo;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v375 @ r8_v6 (Il2CppClass<System.Reflection.FieldInfo>)+2D0]");
				num2 = 0;
				UniversalRenderPipelineAsset value = (UniversalRenderPipelineAsset)RenderDataList_FieldInfo.GetValue(universalRenderPipelineAsset);
				bool flag4 = (object)value == null;
				result = (ScriptableRendererData[])(object)universalRenderPipelineAsset2;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					ScriptableRendererData[] array = default(ScriptableRendererData[]);
					bool flag5 = array == null;
					result = array;
					universalRenderPipelineAsset3 = value;
					if (flag5)
					{
						goto IL_01f4;
					}
				}
			}
		}
		return result;
		IL_0203:
		return (ScriptableRendererData[])(object)new NullReferenceException();
		IL_01f4:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		goto IL_0203;
	}

	public static T GetRendererFeature<T>(UniversalRenderPipelineAsset asset = null)
	{
		//IL_0084: Expected O, but got I4
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ rdx (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		ScriptableRendererData[] rendererDataList = GetRendererDataList(asset);
		if (rendererDataList != null && rendererDataList.Length != 0)
		{
			object obj = 0;
			List<ScriptableRendererFeature>.Enumerator enumerator = default(List<ScriptableRendererFeature>.Enumerator);
			object obj2 = default(object);
			T result = default(T);
			while ((nint)obj < rendererDataList.Length)
			{
				ScriptableRendererData scriptableRendererData = rendererDataList[obj];
				if ((object)rendererDataList[obj] != null && scriptableRendererData.m_RendererFeatures != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						if (obj2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
							return result;
						}
					}
					enumerator.Dispose();
					obj++;
					continue;
				}
				return (T)new NullReferenceException();
			}
		}
		return (T)null;
	}

	public static ScriptableRendererFeature GetRendererFeature(string typeName, UniversalRenderPipelineAsset asset = null)
	{
		//IL_0057: Expected O, but got I4
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Expected O, but got Unknown
		//IL_013d: Expected I, but got O
		ScriptableRendererData[] rendererDataList = GetRendererDataList(asset);
		if (rendererDataList != null && rendererDataList.Length != 0)
		{
			object obj = 0;
			List<ScriptableRendererFeature>.Enumerator enumerator = default(List<ScriptableRendererFeature>.Enumerator);
			UnityEngine.Object obj2 = default(UnityEngine.Object);
			UnityEngine.Object obj3 = default(UnityEngine.Object);
			string text = default(string);
			while ((nint)obj < rendererDataList.Length)
			{
				ScriptableRendererData scriptableRendererData = rendererDataList[obj];
				if ((object)rendererDataList[obj] != null && scriptableRendererData.m_RendererFeatures != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
					while (enumerator.MoveNext())
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
						if (obj2 != null)
						{
							if ((object)obj2 == null)
							{
								throw new NullReferenceException();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
							if ((object)obj3 == null)
							{
								throw new NullReferenceException();
							}
							nint num = (nint)obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v479 @ rdx_v15 (Il2CppClass<UnityEngine.Object>)+1B8] (should have been resolved before IL gen)");
							if (text == null)
							{
								throw new NullReferenceException();
							}
							if (text.Contains(typeName))
							{
								enumerator.Dispose();
								return (ScriptableRendererFeature)obj2;
							}
						}
					}
					enumerator.Dispose();
					obj++;
					continue;
				}
				return (ScriptableRendererFeature)(object)new NullReferenceException();
			}
		}
		return null;
	}

	public unsafe static T GetRendererFeatureChild<T>(ScriptableRendererFeature feature, string fieldName, string subFieldName = null)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001d: Expected O, but got I
		//IL_0096: Expected O, but got I
		//IL_00b4: Expected O, but got I
		//IL_03e0: Expected O, but got I
		//IL_03ad: Expected O, but got Ref
		//IL_01c9: Expected O, but got I
		//IL_0201: Expected O, but got I
		//IL_0377: Expected O, but got I
		//IL_034b: Expected O, but got I
		//IL_02c8: Expected O, but got I
		//IL_02ff: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
		object obj4 = 0;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2+FC]");
			object obj7 = (nint)0 + (nint)15;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rcx_v2+FC]");
			if ((nint)obj7 <= 0)
			{
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		}
		bool flag = feature != null;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			goto IL_038c;
		}
		if ((object)feature != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			object obj8 = default(object);
			if (obj8 != null)
			{
				object obj9 = obj8;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v340 @ r9_v4+6C0]");
				num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v340 @ r9_v4+6B8] (should have been resolved before IL gen)");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
				object obj10 = default(object);
				if (obj10 != null)
				{
					object obj11 = default(object);
					if (obj11 == null)
					{
						goto IL_03b2;
					}
					object obj12 = obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r8_v11+2C8]");
					num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v418 @ r8_v11+2D0]");
					object obj13 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v418 @ r8_v11+2C8] (should have been resolved before IL gen)");
					object obj14 = default(object);
					if (obj14 != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+40]");
						if (string.IsNullOrEmpty((string)0))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
							object obj15 = 0;
							object obj16 = obj15;
							object obj17 = obj14;
							goto IL_0314;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
						object obj18 = default(object);
						if (obj18 == null)
						{
							goto IL_03b2;
						}
						object obj19 = obj18;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v457 @ r9_v8+6C0]");
						num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v457 @ r9_v8+6B8] (should have been resolved before IL gen)");
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
						object obj20 = default(object);
						if (obj20 != null)
						{
							object obj21 = default(object);
							if (obj21 == null)
							{
								goto IL_03b2;
							}
							object obj22 = obj21;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ r8_v17+2C8]");
							num = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ r8_v17+2D0]");
							obj13 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v469 @ r8_v17+2C8] (should have been resolved before IL gen)");
							object obj23 = default(object);
							if (obj23 != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
								object obj24 = 0;
								object obj16 = obj24;
								object obj17 = obj23;
								goto IL_0314;
							}
						}
					}
				}
				goto IL_038c;
			}
		}
		goto IL_03b2;
		IL_0314:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
		object obj25 = default(object);
		if (obj25 == null)
		{
			goto IL_038c;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
		object obj26 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A67B0");
		object obj28 = default(object);
		object obj27 = obj28;
		goto IL_0418;
		IL_038c:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		obj27 = (object)(&obj2);
		goto IL_0418;
		IL_03b2:
		return (T)new NullReferenceException();
		IL_0418:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		T result = default(T);
		return result;
	}

	public unsafe static void SetRendererFeatureChild<T>(T value, ScriptableRendererFeature feature, string fieldName, string subFieldName = null)
	{
		//IL_0008: Expected O, but got Ref
		//IL_001d: Expected O, but got I
		//IL_0096: Expected O, but got I
		//IL_00b4: Expected O, but got I
		//IL_0116: Expected O, but got I
		//IL_0291: Expected O, but got I
		//IL_02b6: Expected O, but got I
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Expected O, but got Unknown
		//IL_034c: Expected O, but got Ref
		//IL_032f: Expected O, but got I
		//IL_01e8: Expected O, but got I
		//IL_020d: Expected O, but got I
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+50]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
		if ((nint)0 == 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
		object obj4 = 0;
		object obj5 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		object obj6 = (nint)0 + (nint)15;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v2+FC]");
		if ((nint)obj6 > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803378F0");
		}
		if (!(feature != null))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
		object obj8 = default(object);
		object obj7 = obj8;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v325 @ r9_v3+6C0]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v325 @ r9_v3+6B8] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
		object obj10 = default(object);
		if (obj10 == null)
		{
			return;
		}
		FieldInfo fieldInfo = default(FieldInfo);
		bool flag;
		bool flag2;
		ScriptableRendererFeature obj17;
		FieldInfo fieldInfo2;
		if (!string.IsNullOrEmpty(subFieldName))
		{
			ScriptableRendererFeature value2 = (ScriptableRendererFeature)fieldInfo.GetValue(feature);
			if ((object)value2 == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_vm_object_is_inst\"");
			object obj12 = default(object);
			object obj11 = obj12;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v386 @ r9_v7+6B8] (should have been resolved before IL gen)");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180D16FE0");
			object obj13 = default(object);
			if (obj13 == null)
			{
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
			object obj14 = 0;
			obj9 = obj14;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r9_v5+28]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r9_v5+28]");
			object obj15 = num ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r9_v5+28]");
			object obj16 = 0 & obj15;
			flag = (nint)obj16 < 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ r9_v5+28]");
			flag2 = (nint)0 < (nint)0;
			obj17 = value2;
			FieldInfo fieldInfo3 = default(FieldInfo);
			fieldInfo2 = fieldInfo3;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
			object obj18 = 0;
			object obj19 = obj18;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ rcx_v16+28]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ rcx_v16+28]");
			object obj20 = num2 ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ rcx_v16+28]");
			object obj21 = 0 & obj20;
			flag = (nint)obj21 < 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ rcx_v16+28]");
			flag2 = (nint)0 < (nint)0;
			obj17 = feature;
			fieldInfo2 = fieldInfo;
		}
		T val = (T)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
		if (flag2 != flag)
		{
			val = value;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036B9D0");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v17 @ rbx_v1+38]");
		object obj22 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
		object value3 = default(object);
		fieldInfo2.SetValue(obj17, value3);
	}

	public static bool IsRendererFeatureActive<T>(UniversalRenderPipelineAsset asset = null, bool defaultValue = false)
	{
		//IL_009d: Expected I4, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		UnityEngine.Object rendererFeature = (UnityEngine.Object)GetRendererFeature<T>(asset);
		if (rendererFeature != null)
		{
			if ((object)rendererFeature != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v76 @ rax_v3 (UnityEngine.Object)+18]");
				return false;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return defaultValue;
	}

	public static bool IsRendererFeatureActive(string typeName, UniversalRenderPipelineAsset asset = null, bool defaultValue = false)
	{
		//IL_0071: Expected I4, but got O
		ScriptableRendererFeature rendererFeature = GetRendererFeature(typeName, asset);
		if (rendererFeature != null)
		{
			if ((object)rendererFeature != null)
			{
				return rendererFeature.m_Active;
			}
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}
		return defaultValue;
	}

	public static void SetRendererFeatureActive<T>(bool active, UniversalRenderPipelineAsset asset = null)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [methodInfo @ r8 (Il2CppMethodInfo)+38]");
		if ((nint)0 == 0)
		{
		}
		UnityEngine.Object rendererFeature = (UnityEngine.Object)GetRendererFeature<T>(asset);
		if (rendererFeature != null)
		{
		}
	}

	public static void SetRendererFeatureActive(string typeName, bool active, UniversalRenderPipelineAsset asset = null)
	{
		ScriptableRendererFeature rendererFeature = GetRendererFeature(typeName, asset);
		if (rendererFeature != null)
		{
			rendererFeature.m_Active = active;
		}
	}
}
