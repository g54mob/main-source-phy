using System;
using Cpp2ILInjected;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

namespace Shapes;

internal class ShapesRenderPass : ScriptableRenderPass
{
	private class PassData
	{
		public DrawCommand drawCommand;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static BaseRenderFunc<PassData, RasterGraphContext> _003C_003E9__4_0;

		static _003C_003Ec()
		{
			_003C_003Ec obj = new _003C_003Ec();
			_003C_003E9 = obj;
		}

		internal void _003CRecordRenderGraph_003Eb__4_0(PassData dataParam, RasterGraphContext context)
		{
			dataParam.drawCommand.AppendToBuffer(context.cmd);
		}
	}

	private DrawCommand drawCommand;

	private readonly CommandBuffer cmdBuf;

	public ShapesRenderPass Init(DrawCommand drawCommand)
	{
		this.drawCommand = drawCommand;
		if (drawCommand != null)
		{
			base._003CrenderPassEvent_003Ek__BackingField = drawCommand.camEvt;
			return this;
		}
		return (ShapesRenderPass)(object)new NullReferenceException();
	}

	public unsafe override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_006e: Expected I, but got O
		//IL_00a6: Expected O, but got I
		//IL_00af: Expected O, but got I4
		//IL_0271: Expected O, but got I
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Expected O, but got Unknown
		//IL_014b: Expected O, but got Ref
		//IL_0176: Expected O, but got Ref
		//IL_018f: Expected I, but got O
		//IL_0221: Expected O, but got I
		//IL_01c7: Expected O, but got I
		//IL_01d0: Expected O, but got I4
		//IL_02c9: Expected O, but got I
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Expected O, but got Unknown
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807415F0");
		ContextContainer contextContainer = default(ContextContainer);
		IRenderAttachmentRenderGraphBuilder renderAttachmentRenderGraphBuilder = default(IRenderAttachmentRenderGraphBuilder);
		object obj9;
		if (contextContainer != null)
		{
			contextContainer.m_Items = (ContextContainer.Item[])(object)drawCommand;
			ContextContainer contextContainer2 = (ContextContainer)(contextContainer + 16);
			if (renderAttachmentRenderGraphBuilder != null)
			{
				nint num = (nint)renderAttachmentRenderGraphBuilder;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r10_v4 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
				if ((nint)0 >= (nint)0)
				{
					goto IL_00e6;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r10_v4 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+B0]");
				object obj = 0;
				object obj2 = 0;
				while (true)
				{
					object obj3 = obj2 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ r8_v25+v289 @ rax_v68*8]");
					if (0 == (nint)typeof(IBaseRenderGraphBuilder))
					{
						break;
					}
					obj2++;
					object obj4 = obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r10_v4 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
					if ((nint)obj4 < 0)
					{
						continue;
					}
					goto IL_00e6;
				}
				object obj5 = obj2 + obj2;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v286 @ r8_v25+8+v349 @ rcx_v46*8]");
				object obj6 = (nint)0 + (nint)11;
				object obj7 = obj6 << 4;
				object obj8 = obj7 + 312;
				obj9 = obj8 + num;
				goto IL_03f7;
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
		IL_03f7:
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v357 @ r8_v5] (should have been resolved before IL gen)");
		object obj10;
		if (frameData != null)
		{
			UniversalResourceData universalResourceData = frameData.Get<UniversalResourceData>();
			if (universalResourceData != null)
			{
				TextureHandle cameraColor = universalResourceData.cameraColor;
				bool flag = renderAttachmentRenderGraphBuilder == null;
				UnityEngine.Rendering.RenderGraphModule.ResourceHandle resourceHandle = default(UnityEngine.Rendering.RenderGraphModule.ResourceHandle);
				ContextContainer contextContainer2 = (ContextContainer)(&resourceHandle);
				if (!flag)
				{
					renderAttachmentRenderGraphBuilder.SetRenderAttachment((TextureHandle)(&resourceHandle), 0);
					BaseRenderFunc<PassData, RasterGraphContext> baseRenderFunc = _003C_003Ec._003C_003E9__4_0;
					if (_003C_003Ec._003C_003E9__4_0 == null)
					{
						baseRenderFunc = (_003C_003Ec._003C_003E9__4_0 = delegate(PassData dataParam, RasterGraphContext context)
						{
							dataParam.drawCommand.AppendToBuffer(context.cmd);
						});
					}
					nint num2 = 0;
					nint num3 = (nint)renderAttachmentRenderGraphBuilder;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v651 @ r9_v12 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
					if ((nint)0 >= (nint)0)
					{
						goto IL_0207;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v651 @ r9_v12 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+B0]");
					obj10 = 0;
					object obj11 = 0;
					while (true)
					{
						object obj12 = obj11 + obj11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v728 @ r8_v15+v671 @ rax_v39*8]");
						if ((nint)0 == 0)
						{
							break;
						}
						obj11++;
						object obj13 = obj11;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v651 @ r9_v12 (Il2CppClass<UnityEngine.Rendering.RenderGraphModule.IRenderAttachmentRenderGraphBuilder>)+12E]");
						if ((nint)obj13 < 0)
						{
							continue;
						}
						goto IL_0207;
					}
					object obj14 = obj11 + obj11;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v728 @ r8_v15+8+v727 @ rdx_v22*8]");
					nint num4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ r14_v9 (Il2CppMethodInfo)+50]");
					object obj15 = num4 + 0;
					object obj16 = obj15 << 4;
					object obj17 = obj16 + 312;
					object obj18 = obj17 + num3;
					goto IL_0226;
				}
				throw new NullReferenceException();
			}
			throw new NullReferenceException();
		}
		throw new NullReferenceException();
		IL_00e6:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		object obj19 = default(object);
		obj9 = obj19;
		goto IL_03f7;
		IL_0207:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ r14_v9 (Il2CppMethodInfo)+50]");
		obj10 = 0;
		goto IL_0226;
		IL_0226:
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FC0");
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v734 @ rax_v32+8] (should have been resolved before IL gen)");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1804ADBC0");
	}

	public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
	{
		drawCommand.AppendToBuffer(cmdBuf);
		ScriptableRenderContext scriptableRenderContext = default(ScriptableRenderContext);
		scriptableRenderContext.ExecuteCommandBuffer(cmdBuf);
		cmdBuf.Clear();
	}

	public override void FrameCleanup(CommandBuffer cmd)
	{
		DrawCommand.OnCommandRendered(drawCommand);
		drawCommand = null;
		ObjectPool<ShapesRenderPass>.Free(this);
	}

	public ShapesRenderPass()
	{
		CommandBuffer commandBuffer = new CommandBuffer();
		cmdBuf = commandBuffer;
		base._002Ector();
	}
}
