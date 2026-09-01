using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace INab.ToonDetailer.URP;

public class DepthMaskPass : ScriptableRenderPass
{
	private class PassData
	{
		public RendererListHandle rendererListHandle;

		public TextureHandle destination;

		public Material material;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static BaseRenderFunc<PassData, RasterGraphContext> _003C_003E9__7_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal unsafe void _003CRecordRenderGraph_003Eb__7_0(PassData data, RasterGraphContext context)
		{
			//IL_000f: Expected O, but got Ref
			//IL_002a: Expected O, but got Ref
			RendererListHandle rendererListHandle = default(RendererListHandle);
			RendererList rendererList = (RendererListHandle)(&rendererListHandle);
			context.cmd.DrawRendererList((RendererList)(&rendererListHandle));
		}
	}

	private static List<ShaderTagId> m_ShaderTagIdList;

	private LayerMask m_LayerMask;

	private Material m_Material;

	public DepthMaskPass(string passName)
	{
		ProfilingSampler profilingSampler = new ProfilingSampler(passName);
		base.profilingSampler = profilingSampler;
	}

	public void Setup(ref Material material, ref LayerMask layerMask)
	{
		m_Material = material;
		m_LayerMask = layerMask;
	}

	private unsafe void InitRendererLists(ContextContainer frameData, ref PassData passData, RenderGraph renderGraph)
	{
		//IL_0008: Expected O, but got Ref
		//IL_04d8: Expected O, but got Ref
		//IL_04f0: Expected O, but got Ref
		//IL_005a: Expected O, but got Ref
		//IL_0099: Expected I4, but got I8
		//IL_0099: Expected O, but got Ref
		//IL_00bd: Expected O, but got I4
		//IL_00cf: Expected O, but got Ref
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_014f: Expected O, but got I
		//IL_0188: Expected O, but got Ref
		//IL_0196: Expected O, but got Ref
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Expected O, but got Unknown
		//IL_0217: Expected O, but got I
		//IL_025e: Expected O, but got Ref
		//IL_027b: Expected O, but got Ref
		//IL_0298: Expected O, but got Ref
		//IL_02a6: Expected O, but got Ref
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Expected O, but got Unknown
		//IL_0327: Expected O, but got I
		//IL_036e: Expected O, but got Ref
		//IL_0381: Expected O, but got Ref
		//IL_038f: Expected O, but got Ref
		//IL_03a5: Expected O, but got Ref
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Expected O, but got Unknown
		//IL_041e: Expected O, but got I
		//IL_0460: Expected O, but got Ref
		//IL_047b: Expected O, but got Ref
		//IL_0476: Expected native int or pointer, but got O
		//IL_04bf: Expected O, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		object obj3 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18036C070");
		UniversalRenderingData renderingData = frameData.Get<UniversalRenderingData>();
		UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
		UniversalLightData universalLightData = frameData.Get<UniversalLightData>();
		RenderQueueRange all = RenderQueueRange.all;
		RenderQueueRange value = (RenderQueueRange)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 952));
		RenderQueueRange? renderQueueRange = value;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		RenderQueueRange? renderQueueRange2 = default(RenderQueueRange?);
		int layerMask = default(int);
		int num = default(int);
		FilteringSettings filteringSettings = new FilteringSettings((RenderQueueRange?)(object)(&renderQueueRange2), layerMask, 4294967295u, num);
		SortingCriteria sortingCriteria = default(SortingCriteria);
		DrawingSettings drawingSettings = RenderingUtils.CreateDrawingSettings(m_ShaderTagIdList, renderingData, cameraData, (UniversalLightData)num, sortingCriteria);
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
		object obj6 = obj5 + 128;
		_ = drawingSettings.m_SortingSettings;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+50]");
		_ = 0;
		_ = drawingSettings.shaderPassNames;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+80]");
		obj6 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ rax_v19 (UnityEngine.Rendering.DrawingSettings)+90]");
		_ = 0;
		_ = drawingSettings.m_PerObjectData;
		_ = drawingSettings.m_OverrideMaterialInstanceId;
		_ = drawingSettings.m_UseSrpBatcher;
		object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
		object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		obj8 = obj7;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+60]");
		_ = 0;
		object obj9 = obj8 + 128;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+80]");
		obj9 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+90]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+A0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+B0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v369 @ rax_v21+C0]");
		_ = 0;
		DrawingSettings drawingSettings2 = (DrawingSettings)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		((DrawingSettings*)drawingSettings2)->overrideMaterial = m_Material;
		object obj10 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A8CFA0");
		object obj11 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 80));
		object obj12 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
		obj12 = obj11;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+60]");
		_ = 0;
		object obj13 = obj12 + 128;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+80]");
		obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+90]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+A0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+B0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v399 @ rax_v26+C0]");
		_ = 0;
		object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 128));
		object obj15 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 336));
		FilteringSettings filteringSettings2 = (FilteringSettings)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 112));
		obj15 = obj14;
		DrawingSettings drawSettings = (DrawingSettings)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 336));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+20]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+30]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+40]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+50]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+60]");
		_ = 0;
		object obj16 = obj15 + 128;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+70]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+80]");
		obj16 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+90]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+A0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+B0]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v431 @ rax_v29+C0]");
		_ = 0;
		RendererListParams rendererListParams = (RendererListParams)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544));
		_ = 0;
		*(RendererListParams*)(nint)rendererListParams = new RendererListParams((CullingResults)(&renderQueueRange2), drawSettings, filteringSettings2);
		PassData passData2 = passData;
		RendererListHandle rendererListHandle = renderGraph.CreateRendererList(ref System.Runtime.CompilerServices.Unsafe.As<object, RendererListParams>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 544)));
		passData2.rendererListHandle = (RendererListHandle)rendererListHandle.type;
		_ = rendererListHandle._003Chandle_003Ek__BackingField;
	}

	public unsafe override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
	{
		//IL_0133: Expected O, but got Ref
		//IL_014e: Expected O, but got Ref
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Expected O, but got Unknown
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Expected O, but got Unknown
		//IL_0244: Expected I, but got O
		//IL_027c: Expected O, but got I
		//IL_0285: Expected O, but got I4
		//IL_0425: Expected O, but got I
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Expected O, but got Unknown
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Expected O, but got Unknown
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Expected O, but got Unknown
		//IL_030d: Expected O, but got Ref
		//IL_0326: Expected I, but got O
		//IL_03b8: Expected O, but got I
		//IL_035e: Expected O, but got I
		//IL_0367: Expected O, but got I4
		//IL_047d: Expected O, but got I
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Expected O, but got Unknown
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Expected O, but got Unknown
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807415F0");
		PassData passData = default(PassData);
		TextureHandle textureHandle2 = default(TextureHandle);
		IRenderAttachmentRenderGraphBuilder renderAttachmentRenderGraphBuilder = default(IRenderAttachmentRenderGraphBuilder);
		object obj10;
		if (frameData != null)
		{
			UniversalResourceData universalResourceData = frameData.Get<UniversalResourceData>();
			InitRendererLists(frameData, ref passData, renderGraph);
			if (passData != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v220 @ stack_8_v5 (INab.ToonDetailer.URP.DepthMaskPass+PassData)+14]");
				if ((nint)0 != 0)
				{
					bool flag = frameData.Contains<ToonDetailer.TextureRefData>();
					ToonDetailer.TextureRefData orCreate = frameData.GetOrCreate<ToonDetailer.TextureRefData>();
					UniversalCameraData universalCameraData = frameData.Get<UniversalCameraData>();
					if (universalCameraData != null)
					{
						RenderTextureDescriptor renderTextureDescriptor = default(RenderTextureDescriptor);
						renderTextureDescriptor.depthBufferBits = 0;
						renderTextureDescriptor.colorFormat = RenderTextureFormat.RFloat;
						RenderTextureDescriptor renderTextureDescriptor2 = default(RenderTextureDescriptor);
						bool clear = default(bool);
						FilterMode filterMode = default(FilterMode);
						TextureWrapMode wrapMode = default(TextureWrapMode);
						TextureHandle textureHandle = UniversalRenderer.CreateRenderGraphTexture(renderGraph, (RenderTextureDescriptor)(&renderTextureDescriptor2), "_DepthMaskToonDetailer", clear, filterMode, wrapMode);
						bool flag2 = passData == null;
						RenderGraph renderGraph2 = (RenderGraph)(&textureHandle2);
						if (!flag2)
						{
							passData.destination = (TextureHandle)textureHandle.handle;
							if (passData != null)
							{
								passData.material = m_Material;
								renderGraph2 = (RenderGraph)(passData + 48);
								if (passData != null)
								{
									if (orCreate != null)
									{
										orCreate.depthMaskTexture = passData.destination;
										if (passData != null)
										{
											object obj = passData + 16;
											if (renderAttachmentRenderGraphBuilder != null)
											{
												nint num = (nint)renderAttachmentRenderGraphBuilder;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v762 @ r10_v12 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.RenderGraph>)+12E]");
												if ((nint)0 >= (nint)0)
												{
													goto IL_02bc;
												}
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v762 @ r10_v12 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.RenderGraph>)+B0]");
												object obj2 = 0;
												object obj3 = 0;
												while (true)
												{
													object obj4 = obj3 + obj3;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v800 @ r8_v37+v827 @ rax_v98*8]");
													if (0 == (nint)typeof(IBaseRenderGraphBuilder))
													{
														break;
													}
													obj3++;
													object obj5 = obj3;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v762 @ r10_v12 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.RenderGraph>)+12E]");
													if ((nint)obj5 < 0)
													{
														continue;
													}
													goto IL_02bc;
												}
												object obj6 = obj3 + obj3;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v800 @ r8_v37+8+v859 @ rcx_v63*8]");
												object obj7 = (nint)0 + (nint)9;
												object obj8 = obj7 << 4;
												object obj9 = obj8 + 312;
												obj10 = obj9 + num;
												goto IL_061e;
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						throw new NullReferenceException();
					}
					throw new NullReferenceException();
				}
				if (renderAttachmentRenderGraphBuilder != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				}
				return;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
		IL_039e:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r14_v17 (Il2CppMethodInfo)+50]");
		object obj11 = 0;
		goto IL_03bd;
		IL_03bd:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FC0");
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v1207 @ rax_v61+8] (should have been resolved before IL gen)");
		if (renderAttachmentRenderGraphBuilder != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		}
		return;
		IL_061e:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v867 @ r8_v18] (should have been resolved before IL gen)");
		if (passData != null)
		{
			if (renderAttachmentRenderGraphBuilder != null)
			{
				renderAttachmentRenderGraphBuilder.SetRenderAttachment((TextureHandle)(&textureHandle2), 0);
				BaseRenderFunc<PassData, RasterGraphContext> baseRenderFunc = _003C_003Ec._003C_003E9__7_0;
				if (_003C_003Ec._003C_003E9__7_0 == null)
				{
					baseRenderFunc = (_003C_003Ec._003C_003E9__7_0 = delegate(PassData data, RasterGraphContext context)
					{
						//IL_000f: Expected O, but got Ref
						//IL_002a: Expected O, but got Ref
						RendererListHandle rendererListHandle = default(RendererListHandle);
						RendererList rendererList = (RendererListHandle)(&rendererListHandle);
						context.cmd.DrawRendererList((RendererList)(&rendererListHandle));
					});
				}
				nint num2 = 0;
				nint num3 = (nint)renderAttachmentRenderGraphBuilder;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1115 @ r9_v21 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_039e;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1115 @ r9_v21 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+B0]");
				obj11 = 0;
				object obj12 = 0;
				while (true)
				{
					object obj13 = obj12 + obj12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1191 @ r8_v26+v1157 @ rax_v69*8]");
					if ((nint)0 == 0)
					{
						break;
					}
					obj12++;
					object obj14 = obj12;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1115 @ r9_v21 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
					if ((nint)obj14 < 0)
					{
						continue;
					}
					goto IL_039e;
				}
				object obj15 = obj12 + obj12;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1191 @ r8_v26+8+v1190 @ rdx_v39*8]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v486 @ r14_v17 (Il2CppMethodInfo)+50]");
				object obj16 = num4 + 0;
				object obj17 = obj16 << 4;
				object obj18 = obj17 + 312;
				object obj19 = obj18 + num3;
				goto IL_03bd;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
		IL_02bc:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		object obj20 = default(object);
		obj10 = obj20;
		goto IL_061e;
	}

	private unsafe static void ExecutePass(PassData data, RasterGraphContext context)
	{
		//IL_000a: Expected O, but got Ref
		//IL_0025: Expected O, but got Ref
		RendererListHandle rendererListHandle = default(RendererListHandle);
		RendererList rendererList = (RendererListHandle)(&rendererListHandle);
		context.cmd.DrawRendererList((RendererList)(&rendererListHandle));
	}

	public void Dispose()
	{
	}

	unsafe static DepthMaskPass()
	{
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Expected O, but got Unknown
		//IL_028c: Expected native int or pointer, but got O
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Expected O, but got Unknown
		//IL_004a: Expected native int or pointer, but got O
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Expected O, but got Unknown
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_0099: Expected native int or pointer, but got O
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Expected O, but got Unknown
		//IL_00e8: Expected native int or pointer, but got O
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Expected O, but got Unknown
		//IL_0137: Expected native int or pointer, but got O
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Expected O, but got Unknown
		//IL_0186: Expected native int or pointer, but got O
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01d5: Expected native int or pointer, but got O
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Expected O, but got Unknown
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Expected O, but got Unknown
		//IL_0224: Expected native int or pointer, but got O
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Expected O, but got Unknown
		List<ShaderTagId> list = new List<ShaderTagId>();
		object obj = default(object);
		ShaderTagId shaderTagId = (ShaderTagId)(obj + 40);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId = new ShaderTagId("UniversalForward");
		ShaderTagId item = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+28]");
		_ = 0;
		list.Add(item);
		ShaderTagId shaderTagId2 = (ShaderTagId)(obj + 48);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId2 = new ShaderTagId("UniversalForwardOnly");
		ShaderTagId item2 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+30]");
		_ = 0;
		list.Add(item2);
		ShaderTagId shaderTagId3 = (ShaderTagId)(obj + 56);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId3 = new ShaderTagId("LightweightForward");
		ShaderTagId item3 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp+38]");
		_ = 0;
		list.Add(item3);
		ShaderTagId shaderTagId4 = (ShaderTagId)(obj - 32);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId4 = new ShaderTagId("SRPDefaultUnlit");
		ShaderTagId item4 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-20]");
		_ = 0;
		list.Add(item4);
		ShaderTagId shaderTagId5 = (ShaderTagId)(obj - 28);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId5 = new ShaderTagId("DepthOnly");
		ShaderTagId item5 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-1C]");
		_ = 0;
		list.Add(item5);
		ShaderTagId shaderTagId6 = (ShaderTagId)(obj - 24);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId6 = new ShaderTagId("UniversalGBuffer");
		ShaderTagId item6 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-18]");
		_ = 0;
		list.Add(item6);
		ShaderTagId shaderTagId7 = (ShaderTagId)(obj - 20);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId7 = new ShaderTagId("DepthNormalsOnly");
		ShaderTagId item7 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-14]");
		_ = 0;
		list.Add(item7);
		ShaderTagId shaderTagId8 = (ShaderTagId)(obj - 16);
		_ = 0;
		*(ShaderTagId*)(nint)shaderTagId8 = new ShaderTagId("Universal2D");
		ShaderTagId item8 = (ShaderTagId)(obj - 8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v7 @ rsp-10]");
		_ = 0;
		list.Add(item8);
		m_ShaderTagIdList = list;
	}
}
