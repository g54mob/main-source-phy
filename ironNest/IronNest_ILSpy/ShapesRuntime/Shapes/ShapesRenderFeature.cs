using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Shapes;

public class ShapesRenderFeature : ScriptableRendererFeature
{
	public override void Create()
	{
	}

	public unsafe override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		//IL_00f3: Expected O, but got Ref
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		CameraData cameraData = (CameraData)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref renderingData, 8));
		ref Camera camera = ref ((CameraData*)cameraData)->camera;
		if (!DrawCommand.cBuffersRendering.TryGetValue(camera, out var _))
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<DrawCommand>.Enumerator enumerator = default(List<DrawCommand>.Enumerator);
		ScriptableRenderPass scriptableRenderPass = default(ScriptableRenderPass);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CDF10");
				if (scriptableRenderPass == null)
				{
					break;
				}
				object obj = scriptableRenderPass + 96;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v95 @ stack_-50+48]");
				scriptableRenderPass._003CrenderPassEvent_003Ek__BackingField = RenderPassEvent.BeforeRendering;
				renderer.EnqueuePass(scriptableRenderPass);
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}
}
