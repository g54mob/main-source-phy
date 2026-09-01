using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal struct IMDrawer : IDisposable
{
	public enum DrawType
	{
		Shape,
		Custom,
		TextAssetClone,
		TextPooledAuto,
		TextPooledPersistent
	}

	internal static MetaMpb metaMpbPrevious;

	private static Dictionary<Material, string[]> matKeywords;

	private MetaMpb metaMpb;

	private ShapeDrawState drawState;

	private Matrix4x4 mtx;

	private bool allowInstancing;

	private static string[] GetMaterialKeywords(Material m)
	{
		string[] array = default(string[]);
		if (matKeywords != null)
		{
			if (matKeywords.TryGetValue(m, out var _))
			{
				goto IL_006d;
			}
			if ((object)m != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D8A9B0");
				if (matKeywords != null)
				{
					matKeywords.set_Item(m, array);
					goto IL_006d;
				}
			}
		}
		return (string[])(object)new NullReferenceException();
		IL_006d:
		return array;
	}

	public unsafe IMDrawer(MetaMpb metaMpb, Material sourceMat, Mesh sourceMesh, int submesh = 0, DrawType drawType = DrawType.Shape, bool allowInstancing = true, int textAutoDisposeId = -1)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0020: Expected I, but got O
		//IL_00f0: Expected O, but got Ref
		//IL_02bc: Expected I, but got O
		//IL_033f: Expected I, but got O
		//IL_066b: Expected O, but got I
		//IL_0695: Expected O, but got I
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Expected O, but got Unknown
		//IL_0192: Expected O, but got Ref
		//IL_038d: Expected O, but got Ref
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Expected O, but got Unknown
		//IL_0288: Expected O, but got I
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Expected O, but got Unknown
		//IL_088b: Expected O, but got I
		//IL_08b8: Expected O, but got I
		//IL_08c3: Expected O, but got Ref
		//IL_071d: Expected I, but got O
		//IL_0a2f: Expected O, but got I
		//IL_0a3f: Expected O, but got I
		//IL_0a4f: Expected O, but got I
		//IL_0b6d: Expected O, but got I
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Expected O, but got Unknown
		//IL_04eb: Expected O, but got Ref
		//IL_054f: Expected O, but got Ref
		//IL_058d: Expected O, but got I
		//IL_0743: Expected O, but got Ref
		//IL_07e5: Expected O, but got Ref
		//IL_098c: Expected O, but got I
		//IL_099c: Expected O, but got I
		//IL_09ac: Expected O, but got I
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Expected O, but got Unknown
		//IL_062f: Expected O, but got Ref
		//IL_0798: Expected O, but got Ref
		//IL_0409: Expected O, but got Ref
		//IL_0447: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rax_v5 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		this.metaMpb = metaMpb;
		mtx = Draw.matrix;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rcx_v5 (Il2CppStaticFields<Shapes.Draw>)+98]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rcx_v5 (Il2CppStaticFields<Shapes.Draw>)+A8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v100 @ rcx_v5 (Il2CppStaticFields<Shapes.Draw>)+B8]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+120]");
		bool flag;
		if ((nint)0 == 0)
		{
			flag = false;
		}
		else
		{
			ShapesConfig instance = ShapesConfig.Instance;
			flag = instance.useImmediateModeInstancing;
		}
		bool flag2 = !flag;
		bool flag3 = !flag2;
		this.allowInstancing = flag3;
		if (DrawCommand.drawCommandWriteNestLevel <= 0)
		{
			object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
			obj3 = sourceMesh;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+110]");
			_ = 0;
			bool flag4 = !metaMpb.initialized;
			_ = mtx;
			if (!flag4)
			{
				if (metaMpb.instanceCount < 1023)
				{
					ShapeDrawState shapeDrawState = (ShapeDrawState)(metaMpb + 24);
					_ = drawState;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+18]");
					_ = 0;
					ShapeDrawState other = (ShapeDrawState)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
					if (((ShapeDrawState*)shapeDrawState)->CompatibleWith(other))
					{
						goto IL_01f4;
					}
				}
				Debug.LogError("Somehow PreAppendCheck failed for this draw");
				goto IL_0abb;
			}
			metaMpb.drawState = drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+18]");
			_ = 0;
			metaMpb.initialized = true;
			goto IL_01f4;
		}
		Shader shader = sourceMat.shader;
		Draw.style = (DrawStyle)shader;
		nint num3 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v672 @ rax_v31 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num4 = 0;
		_ = 0;
		if (!matKeywords.TryGetValue(sourceMat, out System.Runtime.CompilerServices.Unsafe.As<object, string[]>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240))))
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D8A9B0");
			string[] value = default(string[]);
			matKeywords.set_Item(sourceMat, value);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+F0]");
		_ = 0;
		nint num5 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1202 @ rax_v39 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
		object obj7;
		if ((nint)0 == 4)
		{
			_ = 1;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
			object obj4 = -2;
			bool flag5 = obj4 == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
			object obj5 = -1;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
			bool flag6 = (nint)0 == 1;
			if (!flag6)
			{
				object obj6 = obj5 - 1;
				if (flag6)
				{
					Material material = UnityEngine.Object.Instantiate(sourceMat);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+10]");
					ApplyGlobalPropertiesTMP((Material)0);
					DrawCommand currentWritingCommandBuffer = DrawCommand.CurrentWritingCommandBuffer;
					List<UnityEngine.Object> cachedAssets = currentWritingCommandBuffer.cachedAssets;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+10]");
					cachedAssets.Add((UnityEngine.Object)0);
					obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
					goto IL_08c8;
				}
				object obj8 = obj6 - 1;
				if (!flag6)
				{
					if ((nint)obj8 != 1)
					{
						nint num7 = (nint)typeof(Draw);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1422 @ rsi_v14 (Il2CppClass<Shapes.Draw>)+B8]");
						RenderState renderState = (RenderState)((nint)0 + (nint)200);
						_ = 0;
						ref Material value2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Material>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 240));
						RenderState key = (RenderState)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
						_ = renderState.shader;
						_ = renderState.isTextMaterial;
						_ = renderState.colorMask;
						if (!IMMaterialPool.pool.TryGetValue(key, out value2))
						{
							Material value3 = ((RenderState*)renderState)->CreateMaterial();
							RenderState key2 = (RenderState)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
							_ = renderState.shader;
							_ = renderState.isTextMaterial;
							_ = renderState.colorMask;
							IMMaterialPool.pool.Add(key2, value3);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+F0]");
						_ = 0;
						obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
						if ((nint)0 != 2)
						{
							goto IL_0b99;
						}
						goto IL_08c8;
					}
				}
				else
				{
					DrawCommand currentWritingCommandBuffer2 = DrawCommand.CurrentWritingCommandBuffer;
					int item = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 288));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+128]");
					_ = 0;
					currentWritingCommandBuffer2.cachedTextIds.Add(item);
				}
			}
		}
		obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref this, 8));
		goto IL_0b99;
		IL_09e0:
		int instanceCount = metaMpb.instanceCount;
		Matrix4x4[] matrices = metaMpb.matrices;
		int instanceCount2 = metaMpb.instanceCount + 1;
		metaMpb.instanceCount = instanceCount2;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+40]");
		object obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+30]");
		object obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+50]");
		object obj11 = 0;
		goto IL_0a54;
		IL_01f4:
		Matrix4x4[] matrices2 = metaMpb.matrices;
		int instanceCount3 = metaMpb.instanceCount + 1;
		metaMpb.instanceCount = instanceCount3;
		int num8 = metaMpb.instanceCount << 6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+50]");
		_ = 0;
		goto IL_0abb;
		IL_0ba6:
		metaMpbPrevious = metaMpb;
		return;
		IL_0b99:
		obj7 = sourceMesh;
		goto IL_0392;
		IL_0a54:
		int num9 = instanceCount << 6;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
		_ = 0;
		goto IL_0ba6;
		IL_0392:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+110]");
		_ = 0;
		if (metaMpbPrevious != metaMpb && metaMpbPrevious != null)
		{
			MetaMpb metaMpb2 = metaMpbPrevious;
			if (metaMpb2.initialized)
			{
				DrawCommand currentWritingCommandBuffer3 = DrawCommand.CurrentWritingCommandBuffer;
				ShapeDrawCall shapeDrawCall = metaMpbPrevious.ExtractDrawCall();
				ShapeDrawCall item2 = (ShapeDrawCall)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
				_ = shapeDrawCall.drawState;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rax_v133 (Shapes.ShapeDrawCall)+10]");
				_ = 0;
				_ = shapeDrawCall.usingOverrideMpb;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rax_v133 (Shapes.ShapeDrawCall)+30]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rax_v133 (Shapes.ShapeDrawCall)+40]");
				obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rax_v133 (Shapes.ShapeDrawCall)+50]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v632 @ rax_v133 (Shapes.ShapeDrawCall)+60]");
				_ = 0;
				_ = shapeDrawCall.instanced;
				currentWritingCommandBuffer3.drawCalls.Add(item2);
			}
		}
		bool flag7 = !metaMpb.initialized;
		_ = mtx;
		if (!flag7)
		{
			ShapeDrawState shapeDrawState3 = default(ShapeDrawState);
			if (metaMpb.instanceCount < 1023)
			{
				ShapeDrawState shapeDrawState2 = (ShapeDrawState)(metaMpb + 24);
				if (((ShapeDrawState*)shapeDrawState2)->CompatibleWith((ShapeDrawState)(&shapeDrawState3)))
				{
					goto IL_09e0;
				}
			}
			ShapeDrawCall shapeDrawCall2 = metaMpb.ExtractDrawCall();
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1633 @ rax_v110 (Shapes.ShapeDrawCall)+30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1633 @ rax_v110 (Shapes.ShapeDrawCall)+60]");
			_ = 0;
			_ = shapeDrawCall2.instanced;
			DrawCommand currentWritingCommandBuffer4 = DrawCommand.CurrentWritingCommandBuffer;
			ShapeDrawCall item3 = (ShapeDrawCall)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
			_ = shapeDrawCall2.drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1633 @ rax_v110 (Shapes.ShapeDrawCall)+10]");
			_ = 0;
			_ = shapeDrawCall2.usingOverrideMpb;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1633 @ rax_v110 (Shapes.ShapeDrawCall)+40]");
			obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1633 @ rax_v110 (Shapes.ShapeDrawCall)+50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+F0]");
			_ = 0;
			currentWritingCommandBuffer4.drawCalls.Add(item3);
			bool flag8 = !metaMpb.initialized;
			_ = mtx;
			if (!flag8)
			{
				if (metaMpb.instanceCount < 1023)
				{
					ShapeDrawState shapeDrawState4 = (ShapeDrawState)(metaMpb + 24);
					if (((ShapeDrawState*)shapeDrawState4)->CompatibleWith((ShapeDrawState)(&shapeDrawState3)))
					{
						goto IL_093d;
					}
				}
				Debug.LogWarning("MetaMpb somehow not ready to be initialized");
				goto IL_0ba6;
			}
			metaMpb.drawState = drawState;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+18]");
			_ = 0;
			metaMpb.initialized = true;
			goto IL_093d;
		}
		metaMpb.drawState = drawState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+18]");
		_ = 0;
		metaMpb.initialized = true;
		goto IL_09e0;
		IL_093d:
		instanceCount = metaMpb.instanceCount;
		matrices = metaMpb.matrices;
		int instanceCount4 = metaMpb.instanceCount + 1;
		metaMpb.instanceCount = instanceCount4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+40]");
		obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+30]");
		obj10 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+50]");
		obj11 = 0;
		goto IL_0a54;
		IL_08c8:
		Mesh mesh = UnityEngine.Object.Instantiate(sourceMesh);
		obj7 = mesh;
		DrawCommand currentWritingCommandBuffer5 = DrawCommand.CurrentWritingCommandBuffer;
		currentWritingCommandBuffer5.cachedAssets.Add((UnityEngine.Object)drawState);
		goto IL_0392;
		IL_0abb:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
		if ((nint)0 != 1)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+10]");
			ApplyGlobalProperties((Material)0);
		}
	}

	private static void ApplyGlobalProperties(Material m)
	{
		//IL_0040: Expected I, but got O
		//IL_006a: Expected F4, but got I
		//IL_0092: Expected I, but got O
		//IL_0146: Expected F4, but got I
		//IL_00a5: Expected I, but got O
		//IL_0175: Expected F4, but got I
		//IL_00b8: Expected I, but got O
		//IL_00cb: Expected I, but got O
		//IL_01d3: Expected F4, but got I
		//IL_00de: Expected I, but got O
		//IL_0202: Expected F4, but got I
		//IL_00f1: Expected I, but got O
		//IL_0231: Expected F4, but got I
		//IL_0104: Expected I, but got O
		//IL_0260: Expected F4, but got I
		//IL_0117: Expected I, but got O
		//IL_028f: Expected F4, but got I
		if (DrawCommand.drawCommandWriteNestLevel <= 0)
		{
			nint num = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rax_v14 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num2 = 0;
			int propZTest = ShapesMaterialUtils.propZTest;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v226 @ rcx_v11 (Il2CppStaticFields<Shapes.Draw>)+DC]");
			m.SetFloat(propZTest, 0f);
			nint num3 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v262 @ rax_v19 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num4 = 0;
			int propZOffsetFactor = ShapesMaterialUtils.propZOffsetFactor;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ rax_v20 (Il2CppStaticFields<Shapes.Draw>)+E0]");
			m.SetFloat(propZOffsetFactor, 0f);
			nint num5 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v302 @ rax_v25 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num6 = 0;
			int propZOffsetUnits = ShapesMaterialUtils.propZOffsetUnits;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v305 @ rax_v26 (Il2CppStaticFields<Shapes.Draw>)+E4]");
			m.SetFloat(propZOffsetUnits, 0f);
			nint num7 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ r8_v3 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num8 = 0;
			int propColorMask = ShapesMaterialUtils.propColorMask;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v347 @ r8_v4 (Il2CppStaticFields<Shapes.Draw>)+E8]");
			m.SetInt(propColorMask, 0);
			nint num9 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v384 @ rax_v35 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num10 = 0;
			int propStencilComp = ShapesMaterialUtils.propStencilComp;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v387 @ rax_v36 (Il2CppStaticFields<Shapes.Draw>)+EC]");
			m.SetFloat(propStencilComp, 0f);
			nint num11 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v425 @ rax_v41 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num12 = 0;
			int propStencilOpPass = ShapesMaterialUtils.propStencilOpPass;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v428 @ rax_v42 (Il2CppStaticFields<Shapes.Draw>)+F0]");
			m.SetFloat(propStencilOpPass, 0f);
			nint num13 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v466 @ rax_v47 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num14 = 0;
			int propStencilID = ShapesMaterialUtils.propStencilID;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v469 @ rax_v48 (Il2CppStaticFields<Shapes.Draw>)+F4]");
			m.SetFloat(propStencilID, 0f);
			nint num15 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v508 @ rax_v53 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num16 = 0;
			int propStencilReadMask = ShapesMaterialUtils.propStencilReadMask;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v511 @ rax_v54 (Il2CppStaticFields<Shapes.Draw>)+F5]");
			m.SetFloat(propStencilReadMask, 0f);
			nint num17 = (nint)typeof(Draw);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v538 @ rax_v59 (Il2CppClass<Shapes.Draw>)+B8]");
			nint num18 = 0;
			int propStencilWriteMask = ShapesMaterialUtils.propStencilWriteMask;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v541 @ rax_v60 (Il2CppStaticFields<Shapes.Draw>)+F6]");
			m.SetFloat(propStencilWriteMask, 0f);
		}
	}

	private static void ApplyGlobalPropertiesTMP(Material m)
	{
		//IL_0018: Expected I, but got O
		//IL_005f: Expected I, but got O
		//IL_0072: Expected I, but got O
		//IL_0085: Expected I, but got O
		//IL_0098: Expected I, but got O
		//IL_00ab: Expected I, but got O
		//IL_00be: Expected I, but got O
		nint num = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v9 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num2 = 0;
		int propZTestTMP = ShapesMaterialUtils.propZTestTMP;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v107 @ r8_v1 (Il2CppStaticFields<Shapes.Draw>)+DC]");
		m.SetInt(propZTestTMP, 0);
		nint num3 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v178 @ r8_v4 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num4 = 0;
		int propColorMask = ShapesMaterialUtils.propColorMask;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v182 @ r8_v5 (Il2CppStaticFields<Shapes.Draw>)+E8]");
		m.SetInt(propColorMask, 0);
		nint num5 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v219 @ r8_v8 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num6 = 0;
		int propStencilComp = ShapesMaterialUtils.propStencilComp;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ r8_v9 (Il2CppStaticFields<Shapes.Draw>)+EC]");
		m.SetInt(propStencilComp, 0);
		nint num7 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ r8_v12 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num8 = 0;
		int propStencilOpPass = ShapesMaterialUtils.propStencilOpPass;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v264 @ r8_v13 (Il2CppStaticFields<Shapes.Draw>)+F0]");
		m.SetInt(propStencilOpPass, 0);
		nint num9 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v301 @ rax_v26 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num10 = 0;
		int propStencilIDTMP = ShapesMaterialUtils.propStencilIDTMP;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rax_v27 (Il2CppStaticFields<Shapes.Draw>)+F4]");
		m.SetInt(propStencilIDTMP, 0);
		nint num11 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v341 @ rax_v32 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num12 = 0;
		int propStencilReadMask = ShapesMaterialUtils.propStencilReadMask;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v344 @ rax_v33 (Il2CppStaticFields<Shapes.Draw>)+F5]");
		m.SetInt(propStencilReadMask, 0);
		nint num13 = (nint)typeof(Draw);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rax_v38 (Il2CppClass<Shapes.Draw>)+B8]");
		nint num14 = 0;
		int propStencilWriteMask = ShapesMaterialUtils.propStencilWriteMask;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v373 @ rax_v39 (Il2CppStaticFields<Shapes.Draw>)+F6]");
		m.SetInt(propStencilWriteMask, 0);
	}

	public unsafe void Dispose()
	{
		//IL_00d7: Expected O, but got I4
		//IL_0106: Expected O, but got I
		//IL_0135: Expected O, but got Ref
		//IL_0081: Expected O, but got Ref
		Matrix4x4 matrix4x = default(Matrix4x4);
		if (DrawCommand.drawCommandWriteNestLevel > 0)
		{
			if (!allowInstancing)
			{
				ShapeDrawCall shapeDrawCall = this.metaMpb.ExtractDrawCall();
				DrawCommand currentWritingCommandBuffer = DrawCommand.CurrentWritingCommandBuffer;
				currentWritingCommandBuffer.drawCalls.Add((ShapeDrawCall)(&matrix4x));
			}
			return;
		}
		MetaMpb metaMpb = this.metaMpb;
		metaMpb.directMaterialApply = true;
		metaMpb.TransferAllProperties();
		metaMpb.directMaterialApply = false;
		metaMpb.initialized = false;
		metaMpb.drawState = (ShapeDrawState)0;
		_ = 0;
		metaMpb.instanceCount = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+10]");
		bool flag = ((Material)0).SetPass(0);
		ShapeDrawState mesh = drawState;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Shapes.IMDrawer)+18]");
		Graphics.DrawMeshNow((Mesh)mesh, (Matrix4x4)(&matrix4x), 0);
	}

	static IMDrawer()
	{
		Dictionary<Material, string[]> dictionary = new Dictionary<Material, string[]>();
		matKeywords = dictionary;
	}
}
